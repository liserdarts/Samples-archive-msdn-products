// ***************************************************************
// <copyright file="BandwidthLogging.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A transport agent that logs bandwidth used to deliver messages to specified recipients.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.BandwidthLogging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using Microsoft.Exchange.Data.Mime;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Routing;

    public class BandwidthLoggerFactory : RoutingAgentFactory
    {
        // This timer will cause the usage records to be written to disk every ten minutes.
        Timer timer = new Timer(BandwidthLogger.WriteUsageRecords, null, new TimeSpan(0, 10, 0), new TimeSpan(0, 10, 0));

        public override RoutingAgent CreateAgent(SmtpServer server)
        {
            return new BandwidthLogger(server);
        }

        // Invoked when the server is shutting down.
        public override void Close()
        {
            timer.Dispose();
            BandwidthLogger.WriteUsageRecords(null);
        }
    }

    // Keeps track of the bandwidth usage of a recipient.
    // This is the value portion of a dictionary entry, the key for which is the
    // recipient's primary address.
    internal class BandwidthUsageRecord
    {
        public long Inbound;
        public long Outbound;
    }

    public class BandwidthLogger : RoutingAgent
    {
        // This custom header will record the size of a message.
        private const string sizeHeaderName = "X-MessageSize";

        // This is prepended to logging messages.
        private const string logPrefix = "[BandwidthLogger] ";

        // Members of this recipient group will have their bandwidth logged.
        private RoutingAddress loggingGroup = (RoutingAddress)"BandwidthLogging";

        // This dictionary maps recipient primary addresses to bandwidth
        // usage records.
        internal static Dictionary<RoutingAddress, BandwidthUsageRecord> records = new Dictionary<RoutingAddress, BandwidthUsageRecord>();

        // This interface exposes the address book of the server.
        private SmtpServer server;

        public BandwidthLogger(SmtpServer server)
        {
            Debug.WriteLine(logPrefix + "Agent constructor");
            this.server = server;
            this.OnSubmittedMessage += SubmittedMessage;
            this.OnRoutedMessage += RoutedMessage;
        }

        // <summary>Add an X-header to note the size of a message as it was
        // received from the Internet.</summary>
        private void SubmittedMessage(
            SubmittedMessageEventSource source,
            QueuedMessageEventArgs e)
        {
            Debug.WriteLine(logPrefix + "Message submitted");

            if (true)
            {
                Header sizeHeader = Header.Create(sizeHeaderName);

                // Note that MailItem.GetMimeReadStream().Length will re-
                // examine the MIME DOM with every access, which is expensive.
                // It is better to use MailItem.MimeStreamLength, which
                // evaluates the message once and then caches the size unless
                // the message is changed.
                sizeHeader.Value = e.MailItem.MimeStreamLength.ToString();

                e.MailItem.Message.MimeDocument.RootPart.Headers.AppendChild(sizeHeader);
            }
        }

        // Tally the bandwidth cost for a message's sender and/or recipients.
        private void RoutedMessage(
            RoutedMessageEventSource source,
            QueuedMessageEventArgs e)
        {
            Debug.WriteLine(logPrefix + "Message routed");

            long inboundMessageSize = 0;
            long outboundMessageSize = 0;

            Header sizeHeader = e.MailItem.Message.MimeDocument.RootPart.Headers.FindFirst(sizeHeaderName);
            if (null != sizeHeader)
            {
                // For inbound messages, the size that matters is the size that
                // was measured when the message was submitted.
                if (!long.TryParse(sizeHeader.Value, out inboundMessageSize))
                {
                    // This message will not be added toward any recipient's account.
                    Debug.WriteLine(logPrefix + "Unable to parse message size header");
                }
            }

            // For outbound messages, the size that matters is the size
            // of the message during routing.
            outboundMessageSize = e.MailItem.Message.MimeDocument.WriteTo(Stream.Null);

            foreach (EnvelopeRecipient recipient in e.MailItem.Recipients)
            {
                string rcptDomain = recipient.Address.DomainPart;

                // If the recipient domain is not in the organization, the 
                // message is going out of the organization and should be added
                // to the outbound log of the sender.
                AcceptedDomain acceptedDomain = this.server.AcceptedDomains.Find(rcptDomain);
                if ((null == acceptedDomain) || (!acceptedDomain.IsInCorporation))
                {
                    // Only tally messages for members of the logging group.
                    if (!server.AddressBook.IsMemberOf(e.MailItem.FromAddress, loggingGroup))
                    {
                        continue;
                    }

                    // Get the primary address of the sender.
                    AddressBookEntry senderInfo = this.server.AddressBook.Find(e.MailItem.FromAddress);
                    if (null == senderInfo)
                    {
                        Debug.WriteLine(logPrefix + "Sender " + e.MailItem.FromAddress + " is not in the directory");
                        continue;
                    }

                    // Add the outbound size to the usage record of the sender.
                    lock (BandwidthLogger.records)
                    {
                        BandwidthUsageRecord record = this.GetUsageRecord(senderInfo.PrimaryAddress);
                        record.Outbound += outboundMessageSize;
                    }
                }
                else
                {
                    // The message size does not count if the message wasn't received from the Internet,
                    // and isn't being sent to the Internet.
                    if (0 == inboundMessageSize)
                    {
                        continue;
                    }

                    // Only tally messages for members of the logging group.
                    if (!server.AddressBook.IsMemberOf(recipient.Address, loggingGroup))
                    {
                        continue;
                    }

                    // Get the recipient primary address.
                    AddressBookEntry recipientInfo = this.server.AddressBook.Find(recipient.Address);
                    if (null == recipientInfo)
                    {
                        Debug.WriteLine(logPrefix + "Recipient " + recipient.Address + " is not in the directory");
                        continue;
                    }

                    // Add the inbound size to the recipient's usage record.
                    lock (BandwidthLogger.records)
                    {
                        BandwidthUsageRecord record = this.GetUsageRecord(recipientInfo.PrimaryAddress);
                        record.Inbound += inboundMessageSize;
                    }
                }
            }
        }

        // Get the usage record, creating it if necessary.
        /// This method assumes that the caller has locked the dictionary.
        private BandwidthUsageRecord GetUsageRecord(RoutingAddress primaryAddress)
        {
            if (!BandwidthLogger.records.ContainsKey(primaryAddress))
            {
                BandwidthLogger.records.Add(primaryAddress, new BandwidthUsageRecord());
            }

            return BandwidthLogger.records[primaryAddress];
        }

        // Write the usage log to a .csv file.
        internal static void WriteUsageRecords(object unused)
        {
            Debug.WriteLine(logPrefix + "Writing bandwidth usage records.");
            char comma = ',';

            lock (BandwidthLogger.records)
            {

                try
                {
                    string logDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Log";
                    string logFile = logDir + @"\BandwidthUsage.csv";

                    if (!Directory.Exists(logDir))
                    {
                        Directory.CreateDirectory(logDir);
                    }

                    if (!File.Exists(logFile))
                    {
                        File.CreateText(logFile).Close();
                    }

                    using (StreamWriter writer = File.AppendText(logFile))
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.Append("PrimaryAddress");
                        builder.Append(comma);
                        builder.Append("Inbound");
                        builder.Append(comma);
                        builder.Append("Outbound");
                        writer.WriteLine(builder.ToString());

                        foreach (KeyValuePair<RoutingAddress, BandwidthUsageRecord> pair in BandwidthLogger.records)
                        {
                            builder = new StringBuilder();

                            builder.Append(pair.Key);
                            builder.Append(comma);
                            builder.Append(pair.Value.Inbound);
                            builder.Append(comma);
                            builder.Append(pair.Value.Outbound);

                            writer.WriteLine(builder.ToString());
                        }
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
