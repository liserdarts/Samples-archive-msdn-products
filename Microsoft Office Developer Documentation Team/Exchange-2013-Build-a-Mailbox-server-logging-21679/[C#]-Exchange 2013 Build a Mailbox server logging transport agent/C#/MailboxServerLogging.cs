// ***************************************************************
// <copyright file="MailboxServerLogging.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A simple agent that logs messages on a Mailbox server.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.MailboxServerLogging
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Routing;

    public class MessageLoggerFactory : RoutingAgentFactory
    {
        public override RoutingAgent CreateAgent(SmtpServer server)
        {
            return new MessageLogger();
        }
    }

    public class MessageLogger : RoutingAgent
    {
        // The agent uses the fileLock object to synchronize access to the log file.
        private object fileLock = new object();

        public MessageLogger()
        {
            // Register an OnRoutedMessage event handler.
            this.OnRoutedMessage += OnRoutedMessageHandler;
        }

        // The OnRoutedMessageHandler method is invoked when the entire message
        // has been received and routed to the next hop.
        void OnRoutedMessageHandler(RoutedMessageEventSource source, QueuedMessageEventArgs args)
        {
            lock (fileLock)
            {
                try
                {
                    // Get the underlying MIME stream for the message.
                    Stream messageStream = args.MailItem.GetMimeReadStream();

                    string logDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Log";
                    string logFile = logDir + @"\MessageLog.txt";

                    if (!Directory.Exists(logDir))
                    {
                        Directory.CreateDirectory(logDir);
                    }

                    if (!File.Exists(logFile))
                    {
                        File.CreateText(logFile).Close();
                    }

                    using (StreamWriter logWriter = File.AppendText(logFile))
                    {
                        logWriter.Write(Environment.NewLine + "-------------------------------------------------------------------------------" + Environment.NewLine);
                        const int bufferSize = 4000;
                        byte[] buffer = new byte[bufferSize];

                        messageStream.Position = 0;

                        while (messageStream.Position < messageStream.Length)
                        {
                            int bytesRead = messageStream.Read(buffer, 0, bufferSize);
                            Decoder decoder = Encoding.Default.GetDecoder();
                            char[] chars = new char[decoder.GetCharCount(buffer, 0, bufferSize)];
                            decoder.GetChars(buffer, 0, bufferSize, chars, 0);
                            logWriter.Write(chars);
                        }
                        logWriter.Flush();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
            return;
        }
    }
}
