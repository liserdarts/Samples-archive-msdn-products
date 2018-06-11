// ***************************************************************
// <copyright file="MailFilterAgent.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// An agent that maintains and enforces a list of temporarily blocked SMTP 
// sender domains.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.MailFilterAgent
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    using Microsoft.Exchange.Data;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;
    using Microsoft.Exchange.Data.Mime;


    /// <summary>
    /// Agent class for creating and maintaining a list of temporarily blocked senders.
    /// </summary>
    public class MailFilterAgent : SmtpReceiveAgent
    {
        #region Class Variables

        /// <summary>
        /// The error message that will be sent to the client if
        /// you want to temporarily reject the message.
        /// </summary>
        private static readonly SmtpResponse DelayResponseMessage = new SmtpResponse(
                        "451",
                        "4.7.1",
                        "Please try again later.");

        /// <summary>
        /// An instantiation of a class that can be used to convert
        /// strings to arrays of bytes. This is used in hash
        /// calculations.
        /// </summary>
        private static ASCIIEncoding asciiEncoding = new ASCIIEncoding();

        /// <summary>
        /// A reference to the server object.
        /// </summary>
        private SmtpServer server;

        /// <summary>
        /// A reference to a MailFilter settings object.
        /// </summary>
        private MailFilterSettings settings;

        /// <summary>
        /// A flag that you will use to remember whether you want to
        /// run your algorithm after end of headers instead of RCPTCommand.
        /// </summary>
        private bool testOnEndOfHeaders;

        /// <summary>
        /// The IP address of the sending SMTP host as
        /// recorded in the SMTP envelope.
        /// </summary>
        private IPAddress senderIP;

        /// <summary>
        /// The email address of the message sender as
        /// recorded in the SMTP envelope.
        /// </summary>
        private RoutingAddress senderAddress;

        /// <summary>
        /// The email address of the message recipient as
        /// recorded in the SMTP envelope.
        /// </summary> 
        private RoutingAddress recipientAddress;

        /// <summary>
        /// The database of verified entries.
        /// </summary>
        private MailFilterDatabase verifiedDatabase;

        /// <summary>
        /// The database of unverified entries.
        /// </summary>
        private MailFilterDatabase unverifiedDatabase;

        /// <summary>
        /// A hash code generator that will be used to
        /// calculate the hash values of triplets.
        /// </summary>
        private SHA256Managed hashManager;

        #endregion Class Variables

        #region Constructor
        /// <summary>
        /// The constructor registers all event handlers and creates
        /// local references to the session database tables. It should only be called
        /// from the MailFilterAgentFactory class.
        /// </summary>
        /// <param name="settings">A reference to a settings object.</param>
        /// <param name="verifiedDatabase">A reference to the table of verified entries.</param>
        /// <param name="unverifiedDatabase">A reference to the table of unverified entries.</param>
        /// <param name="hashManager">A reference to the hash class instance in the factory.</param>
        /// <param name="server">A reference to the SMTP server, passed from the factory.</param>
        public MailFilterAgent(
                             MailFilterSettings settings,
                             MailFilterDatabase verifiedDatabase,
                             MailFilterDatabase unverifiedDatabase,
                             SHA256Managed hashManager,
                             SmtpServer server)
        {
            // Initialize instance variables.
            this.settings = settings;
            this.server = server;
            this.verifiedDatabase = verifiedDatabase;
            this.unverifiedDatabase = unverifiedDatabase;
            this.testOnEndOfHeaders = false;
            this.hashManager = hashManager;

            // Set up the hooks to have your functions called when certain events occur.
            this.OnRcptCommand += new RcptCommandEventHandler(this.OnRcptCommandHandler);
            this.OnEndOfHeaders += new EndOfHeadersEventHandler(this.OnEndOfHeaderHandler);
        }
        #endregion Constructor

        #region Hooked Methods

        #region RCPT Command
        /// <summary>
        /// Invoked by Exchange when an SMTP RCPT command is sent.
        /// This is the command that gives you the recipient
        /// address in the SMTP envelope.
        /// </summary>
        /// <param name="source">The source of this event.</param>
        /// <param name="rcptArgs">Arguments for this event.</param>
        public void OnRcptCommandHandler(ReceiveCommandEventSource source, RcptCommandEventArgs rcptArgs)
        {
            // Check the parameter values.
            if (source == null || rcptArgs == null)
            {
                return;
            }

            // Skip filtering for internal mail.
            if (!rcptArgs.SmtpSession.IsExternalConnection)
            {
                return;
            }

            // Retrieve data used to identify this message.
            this.senderAddress = rcptArgs.MailItem.FromAddress;
            this.senderIP = rcptArgs.SmtpSession.RemoteEndPoint.Address;
            this.recipientAddress = rcptArgs.RecipientAddress;

            // If the sender domain is null, you will have to wait until
            // after the EndOfData event to check the message.
            if (RoutingAddress.NullReversePath.Equals(this.senderAddress))
            {
                this.testOnEndOfHeaders = true;
                return;
            }

            // Skip temporary blocking for safe senders.
            if (this.ShouldBypassFilter(this.senderAddress, this.recipientAddress, this.server))
            {
                return;
            }

            // Check the database to determine whether the message should be rejected 
            // or let through.
            if (!this.VerifyTriplet(this.senderIP, this.senderAddress, this.recipientAddress))
            {
                source.RejectCommand(DelayResponseMessage);
            }

            // Finally, check a few rows
            // for expired entries that need to be cleaned up.
            this.verifiedDatabase.RetireOldEntries(
                                                   this.settings.CleanRowCount,
                                                   this.settings.VerifiedExpirationTime);

            this.unverifiedDatabase.RetireOldEntries(
                                                     this.settings.CleanRowCount,
                                                     this.settings.UnverifiedExpirationTime);
        }
        #endregion RCPT Command

        #region End of Headers
        /// <summary>
        /// This method is called if the sender address provided by the
        /// mail from command was null.
        ///
        /// Because at this point individual recipients cannot be rejected,
        /// if any recipient should be rejected, all recipients will be rejected.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="eodArgs">The arguments passed to the event.</param>
        public void OnEndOfHeaderHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs eodArgs)
        {
            if (this.testOnEndOfHeaders)
            {
                // Reset the flag.
                this.testOnEndOfHeaders = false;

                // Get the sender address from the message header.
                Header fromAddress = eodArgs.Headers.FindFirst(HeaderId.From);
                if (fromAddress != null)
                {
                    this.senderAddress = new RoutingAddress(fromAddress.Value);
                }
                else
                {
                    // No sender address, reject the message.
                    source.RejectMessage(DelayResponseMessage);
                    return;
                }

                // Determine whether any of the recipients should be rejected, and if so, reject them all.
                bool rejectAll = false;
                foreach (EnvelopeRecipient currentRecipient in eodArgs.MailItem.Recipients)
                {
                    if (!this.ShouldBypassFilter(this.senderAddress, currentRecipient.Address, this.server) &&
                        !this.VerifyTriplet(this.senderIP, this.senderAddress, currentRecipient.Address))
                    {
                        rejectAll = true;
                    }
                }

                if (rejectAll)
                {
                    source.RejectMessage(DelayResponseMessage);
                }
            }
        }
        #endregion End of Headers

        #endregion Hooked Methods

        #region Other Methods

        /// <summary>
        /// The core of the MailFilter algorithm. Determines whether 
        /// a triplet matches a known sender/recipient relationship and should
        /// be accepted.
        /// </summary>
        /// <param name="remoteIP">The remote host's IP address.</param>
        /// <param name="sender">The sender's address.</param>
        /// <param name="recipient">The recipient's address.</param>
        /// <returns>Whether the triplet is verified by the MailFilter.</returns>
        private bool VerifyTriplet(IPAddress remoteIP, RoutingAddress sender, RoutingAddress recipient)
        {
            // Create a MailFilterEntry object for the current session.
            // This code uses senderAddress.DomainPart to truncate
            // the sender's address to use only the domain. To use the full
            // address, use ToString() as with recipient.
            UInt64 tripletHash = this.HashTriplet(
                                                  remoteIP,
                                                  sender.DomainPart,
                                                  recipient.ToString());

            MailFilterEntry currentEntry = new MailFilterEntry(tripletHash);

            // Determine whether a matching entry is in the verified database.
            // If it is, save with an updated time stamp.
            // This ensures that the most recent entries are
            // at the start of the bucket lists in the array tables.
            if (this.verifiedDatabase.GetEntry(currentEntry.TripletHash) != null)
            {
                currentEntry.TimeStamp = DateTime.UtcNow;
                this.verifiedDatabase.SaveEntry(currentEntry);
                return true;
            }

            // If the entry is in the unverified table passed in
            // the initial blocking period, remove it.
            MailFilterEntry entry = this.unverifiedDatabase.GetEntry(currentEntry.TripletHash);
            if (entry != null)
            {
                if (entry.IsPastPeriod(this.settings.InitialBlockingPeriod))
                {
                    this.verifiedDatabase.SaveEntry(currentEntry);
                    this.unverifiedDatabase.DeleteEntry(currentEntry);
                    return true;
                }
                else
                {
                    // The entry was in the table of unverified entries,
                    // but the blocking period has not passed yet.
                    return false;
                }
            }
            else
            {
                // The triplet is not in either database. Send a rejection and
                // put it in the unverified database.
                this.unverifiedDatabase.SaveEntry(currentEntry);
                return false;
            }
        }

        /// <summary>
        /// Determines whether there are any recipient-specific settings that
        /// mean the message should not go through mail filtering.
        /// </summary>
        /// <param name="sender">The address of the sender.</param>
        /// <param name="recipient">The address of the recipient.</param>
        /// <param name="server">The server instance.</param>
        /// <returns>Whether the sender is a safe sender.</returns>
        private bool ShouldBypassFilter(RoutingAddress sender, RoutingAddress recipient, SmtpServer server)
        {
            if (server == null || sender == null || recipient == null)
            {
                return false;
            }

            AddressBook addressBook = server.AddressBook;
            if (addressBook != null)
            {
                AddressBookEntry addressBookEntry = addressBook.Find(recipient);
                if (addressBookEntry != null)
                {
                    if (addressBookEntry.AntispamBypass ||
                        addressBookEntry.IsSafeSender(sender) ||
                        addressBookEntry.IsSafeRecipient(recipient))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates a 64-bit hash of the triplet.
        /// </summary>
        /// <param name="senderIP">The IP of the sender.</param>
        /// <param name="senderDomain">The domain of the sender. This is the only parameter that can be null.</param>
        /// <param name="rcptAddress">The address of the recipient.</param>
        /// <returns>The calculated hash code.</returns>
        private UInt64 HashTriplet(IPAddress senderIP, string senderDomain, string rcptAddress)
        {
            // A string that will contain an ASCII value of the triplet.
            string tripletString = String.Empty;

            // Append the IP address onto the triplet string.
            if (senderIP != null)
            {
                tripletString = string.Concat(tripletString, senderIP.ToString());
            }

            // Append the recipient's address onto the triplet string.
            if (rcptAddress != null)
            {
                tripletString = string.Concat(tripletString, rcptAddress);
            }

            // Append the sender's domain onto the triplet string.
            if (senderDomain != null)
            {
                tripletString = string.Concat(tripletString, senderDomain);
            }

            // Convert the string to lowercase and get its value as a byte[].
            byte[] hashInput = asciiEncoding.GetBytes(tripletString.ToLowerInvariant());

            // Calculate the SHA256 hash.
            byte[] hashResult;
            lock (this.hashManager)
            {
                hashResult = this.hashManager.ComputeHash(hashInput);
            }

            // Copy the result into a UInt64.
            return BitConverter.ToUInt64(hashResult, 0);
        }

        /// <summary>
        /// Tests a triplet verification. Delete after testing.
        /// </summary>
        /// <param name="senderIP">The sender IP.</param>
        /// <param name="sender">The sender address.</param>
        /// <param name="recipient">The recipient address.</param>
        /// <returns></returns>
        public bool TestVerify(IPAddress senderIP, RoutingAddress sender, RoutingAddress recipient)
        {
            return this.VerifyTriplet(senderIP, sender, recipient);
        }

        #endregion Other Methods
    }
}