// ***************************************************************
// <copyright file="XHeaderAgent.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// This agent takes action based on X-headers in messages.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.XHeader
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Microsoft.Exchange.Data.Mime;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;

    public class XHeaderAgentFactory : SmtpReceiveAgentFactory
    {
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            return new XHeaderAgent();
        }
    }

    public class XHeaderAgent : SmtpReceiveAgent
    {
        private static XHeaderRule[] rules;

        static XHeaderAgent()
        {
            rules = XHeaderAgentConfiguration.Load();
        }

        public XHeaderAgent()
        {
            Debug.WriteLine("[XHeaderAgent] Agent constructor");
            this.OnEndOfHeaders += new EndOfHeadersEventHandler(this.OnEndOfHeadersHandler);
        }

        public void OnEndOfHeadersHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs args)
        {
            string messageId = String.Empty;
            string rejectReason = String.Empty;
            List<Header> headersToRemove = new List<Header>();

            // Compare the message's headers with the rules.
            foreach (Header header in args.Headers)
            {
                if (header.HeaderId == HeaderId.MessageId)
                {
                    messageId = header.Value;
                }

                foreach (XHeaderRule rule in XHeaderAgent.rules)
                {
                    if (String.Equals(rule.Name, header.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        switch (rule.Action)
                        {
                            case Action.Reject:
                                rejectReason = header.Name;
                                break;

                            case Action.Remove:
                                headersToRemove.Add(header);
                                break;
                        }
                    }
                }
            }

            // Begin preparing a debug message.
            StringBuilder builder = new StringBuilder();

            builder.Append("XHeaderAgent: Message-Id \"");
            builder.Append(messageId);
            builder.Append("\", ");

            // Take action.
            if (!String.IsNullOrEmpty(rejectReason))
            {
                builder.Append("Rejected");
                builder.Append("Contains forbidden X-header");
                builder.Append(rejectReason);

                source.RejectMessage(SmtpResponse.InvalidContent);
                source.Disconnect();
            }
            else if (0 != headersToRemove.Count)
            {
                builder.Append("Removed headers");
                foreach (Header header in headersToRemove)
                {
                    builder.Append(" ");
                    builder.Append(header.Name);
                    args.Headers.RemoveChild(header);
                }
            }
            else
            {
                builder.Append("No action");
            }

            // Write the debug message.
            Debug.WriteLine(builder.ToString());

            return;
        }
    }
}
