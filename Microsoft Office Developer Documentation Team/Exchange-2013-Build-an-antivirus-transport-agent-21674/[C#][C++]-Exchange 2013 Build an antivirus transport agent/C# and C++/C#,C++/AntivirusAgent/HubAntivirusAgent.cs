// ***************************************************************
// <copyright file="HubAntivirusAgent.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Sends incoming messages to an out-of-process COM server, which
// asynchronously examines the message and returns a modified
// version of the message.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Antivirus
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Routing;
    using Microsoft.Exchange.Samples.Antivirus;

    /// <summary>
    /// The agent factory for the ComInterop sample.
    /// </summary>
    public class AntivirusAgentFactory : RoutingAgentFactory
    {
        /// <summary>
        /// Creates a new AntivirusAgent.
        /// </summary>
        /// <param name="server">An Exchange gateway server.</param>
        /// <returns>A new AntivirusAgent.</returns>
        public override RoutingAgent CreateAgent(SmtpServer server)
        {
            return new AntivirusAgent();
        }
    }

    /// <summary>
    /// SmtpReceiveAgent for the ComInterop sample.
    /// </summary>
    public class AntivirusAgent : RoutingAgent, IComCallback
    {
        /// <summary>
        /// The class ID for the unmanaged service.
        /// </summary>
        private static Guid unmanagedClsid = new Guid("B71FEE9E-25EF-4e50-A1D2-545361C90E88");

        /// <summary>
        /// The context to allow Exchange to continue processing a message.
        /// </summary>
        private AgentAsyncContext agentAsyncContext;

        /// <summary>
        /// The current MailItem.
        /// </summary>
        private MailItem mailItem;

        /// <summary>
        /// Virus scanner.
        /// </summary>
        IComInvoke virusScanner;

        /// <summary>
        /// The constructor registers an end-of-data event handler.
        /// </summary>
        public AntivirusAgent()
        {
            Debug.WriteLine("[AntivirusAgent] agent constructor");
            this.OnSubmittedMessage += new SubmittedMessageEventHandler(this.SubmittedMessageHandler);
        }

        /// <summary>
        /// Invoked by Exchange when a message has been submitted.
        /// </summary>
        /// <param name="source">The source of this event.</param>
        /// <param name="args">Arguments for this event.</param>
        void SubmittedMessageHandler(SubmittedMessageEventSource source, QueuedMessageEventArgs args)
        {
            Debug.WriteLine("[AntivirusAgent] Invoking the COM service");

            try
            {
                // Create the virus scanner COM object.
                Guid classGuid = new Guid("B71FEE9E-25EF-4e50-A1D2-545361C90E88");
                Guid interfaceGuid = new Guid("7578C871-D9B3-455a-8371-A82F7D864D0D");

                object virusScannerObject = UnsafeNativeMethods.CoCreateInstance(
                    classGuid,
                    null,
                    4, // CLSCTX_LOCAL_SERVER,
                    interfaceGuid);

                this.virusScanner = (IComInvoke)virusScannerObject;

                // GetAgentAsyncContext causes Exchange to wait for this agent
                // to invoke the returned callback before continuing to 
                // process the current message.
                this.agentAsyncContext = this.GetAgentAsyncContext();

                this.mailItem = args.MailItem;

                // Invoke the virus scanner.
                this.virusScanner.BeginVirusScan((IComCallback)this);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.WriteLine("[AntivirusAgent] " + ex.ToString());
                if (this.agentAsyncContext != null)
                {
                    this.agentAsyncContext.Complete();
                }
            }

            return;
        }

        #region IComCallback Members

        /// <summary>
        /// This will be invoked by the COM server when it has finished 
        /// processing the message.
        /// </summary>
        public void VirusScanCompleted()
        {
            Debug.WriteLine("[AntivirusAgent] Callback from the COM service");

            // Restores the state for the thread.
            this.agentAsyncContext.Resume();

            // Release the COM server resources.
            this.virusScanner = null;

            // This allows Exchange to continue processing the message.
            this.agentAsyncContext.Complete();
        }

        /// <summary>
        /// Replaces the MailItem's MimeStream with the given stream.
        /// </summary>
        /// <param name="stream">A stream that contains new content for the message.</param>
        public void SetContentStream(object stream)
        {
            using (Stream newContent = new DotNetStream((ComInterop.IStream)stream))
            {
                using (Stream writeStream = this.mailItem.GetMimeWriteStream())
                {
                    int bytesRead = 0;
                    byte[] buffer = new byte[4000];
                    while ((bytesRead = newContent.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                    }
                    writeStream.Flush();
                }
            }
        }

        /// <summary>
        /// Returns the MailItem's MimeStream.
        /// </summary>
        /// <param name="stream">A stream that contains the message's current content.</param>
        public void GetContentStream(out ComInterop.IStream stream)
        {
            stream = new ComStream(this.mailItem.GetMimeReadStream());
        }

        #endregion
    }

    /// <summary>
    /// P/Invoke support.
    /// </summary>
    internal sealed class UnsafeNativeMethods
    {
        /// <summary>
        /// P/Invoke to ole32!CoCreateInstance, in order to create instance by using CLSCTX.
        /// </summary>
        [DllImport("ole32.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.IUnknown)]
        internal static extern object CoCreateInstance(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
            uint dwClsContext,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);
    }
}

