//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Threading;
    using System.Management.Automation;
    using Microsoft.ServiceBus.Messaging;

    [Cmdlet("Receive", "Message")]
    public class ReceiveMessageCmdlet : ServiceBusCmdletBase
    {
        [Parameter(
        Mandatory = false,
        HelpMessage = "PeekLock")]
        public SwitchParameter PeekLock { get; set; }

        [Parameter(
        Mandatory = true,
        HelpMessage = "Name of Subscription or queue")]
        public string From
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            string[] chunks = Context.ChunkPath(this.SessionState.Path.CurrentLocation);
            ReceiveMode mode = this.PeekLock.IsPresent ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete;

            string path = this.From;
            if (chunks[0] == "Topics")
            {
                path = SubscriptionClient.FormatSubscriptionPath(chunks[1], this.From);
            }

            BrokeredMessage message = null;
            MessageReceiver receiver = this.Factory.CreateMessageReceiver(path, mode);
            try
            {
                message = receiver.Receive(TimeSpan.FromSeconds(10));
            }
            finally
            {
                // Setting a timer to close the receiver after a minute. User has to complete the message in this time window
                new Timer(CloseReceiver, (object)receiver, TimeSpan.FromMinutes(1), TimeSpan.FromMilliseconds(-1));
            }

            if (message != null)
            {
                message.Properties.Add("Body", message.GetBody<string>());
            }

            this.WriteObject(message);
        }

        static void CloseReceiver(object state)
        {
            try
            {
                MessageReceiver receiver = state as MessageReceiver;
                if (receiver != null)
                {
                    receiver.Close();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
