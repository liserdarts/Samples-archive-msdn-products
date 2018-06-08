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
    using System.Management.Automation;
    using Microsoft.ServiceBus.Messaging;

    [Cmdlet("Send", "Message")]
    public class SendMessageCmdlet : ServiceBusCmdletBase
    {
        [Parameter(
        Mandatory = true,
        HelpMessage = "Message to send")]
        public string Message
        {
            get;
            set;
        }

        [Parameter(
        Mandatory = true,
        HelpMessage = "Name of topic or queue")]
        public string To
        {
            get;
            set;
        }
        
        protected override void ProcessRecord()
        {
            string[] chunks = Context.ChunkPath(this.SessionState.Path.CurrentLocation);
            if (chunks.Length > 1)
            {
                this.WriteError(
                    new ErrorRecord(
                        new InvalidOperationException(
                            "Messages can only be sent for queues and topics, make sure you are in that location"),
                        "0",
                        ErrorCategory.InvalidOperation,
                        this.SessionState.Path.CurrentLocation.Path));
            }
            else
            {
                MessageSender sender = this.Factory.CreateMessageSender(this.To);
                try
                {
                    sender.Send(new BrokeredMessage(this.Message));
                }
                finally
                {
                    sender.Close();
                }
            }            
        }
    }
}

