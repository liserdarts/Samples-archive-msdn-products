/ ***************************************************************
// <copyright file="SmtpLogging.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A simple agent that logs messages on a Mailbox server that come
// through the SMTP protocol.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.SmtpLogging
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;

    public class MessageLoggerFactory : SmtpReceiveAgentFactory
    {
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            return new MessageLogger();
        }
    }

    public class MessageLogger : SmtpReceiveAgent
    {
        // The agent uses the fileLock object to synchronize access to the log file.
        private object fileLock = new object();        

        // The agent uses the agentAsyncContext object when the agent uses asynchronous execution.
        // The AgentAsyncContext.Complete() method must be invoked
        // before the server will continue processing a message.
        private AgentAsyncContext agentAsyncContext;

        public MessageLogger()
        {
            // Register an OnEndOfData event handler.
            this.OnEndOfData += new EndOfDataEventHandler(this.OnEndOfDataHandler);
        }

        // The OnEndOfDataHandler method is invoked when the entire message has been received.
        public void OnEndOfDataHandler(ReceiveEventSource source, EndOfDataEventArgs eodArgs)
        {
            // GetAgentAsyncContext causes the server to wait for this agent
            // to invoke the returned callback before continuing to 
            // process the current message.
            this.agentAsyncContext = this.GetAgentAsyncContext();

            // Begin a background thread that will save the message to disk.
            System.Threading.ThreadPool.QueueUserWorkItem(this.LogMessage, eodArgs.MailItem.GetMimeReadStream());

            return;
        }

        // Append the given stream to the message log.
        public void LogMessage(object state)
        {

            // This allows Transport poison detection to correclty handle this message
            // if there is a crash on this thread.
            this.agentAsyncContext.Resume();

            Stream messageStream = state as Stream;

            lock (fileLock)
            {
                try
                {
                    string logDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Log";
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
                            char[] chars = new char[decoder.GetCharCount(buffer,0,bufferSize)];
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

            this.agentAsyncContext = null;
            this.agentAsyncContext.Complete();
        }
    }
}
