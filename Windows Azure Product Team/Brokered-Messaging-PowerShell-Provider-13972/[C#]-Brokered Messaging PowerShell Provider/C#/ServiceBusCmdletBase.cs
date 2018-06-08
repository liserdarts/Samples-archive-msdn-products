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
    using System.Management.Automation;          // Windows PowerShell namespace.
    using Microsoft.ServiceBus.Messaging;
    
    public class ServiceBusCmdletBase : PSCmdlet
    {
        const string PathSeparator = "\\";

        MessagingFactory factory;

        public MessagingFactory Factory
        {
            get 
            {
                if (factory == null || factory.IsClosed)
                {
                    PSVariable psVariable = this.SessionState.PSVariable.Get("MessagingFactory");
                    if (psVariable == null)
                    {
                        factory = Context.GetMessagingFactory(this.SessionState);
                        this.SessionState.PSVariable.Set("MessagingFactory", factory);
                    }
                    else
                    {
                        factory = (MessagingFactory)psVariable.Value;
                    }
                }

                return factory; 
            }            
        }
    }
}
