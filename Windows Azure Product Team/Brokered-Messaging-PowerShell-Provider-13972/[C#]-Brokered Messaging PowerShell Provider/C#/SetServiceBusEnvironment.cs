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

    [Cmdlet("Set", "ServiceBusEnvironment")]
    public class SetServiceBusEnvironmentCmdlet : PSCmdlet
    {
        [Parameter(
        Mandatory = false,
        HelpMessage = "Sets the environment to run the cmdlets against")]
        public string Environment
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            switch (this.Environment.ToUpper())
            {
                case "BVT":
                case "INT":
                case "PPE":
                    System.Environment.SetEnvironmentVariable("RELAYENV", this.Environment);
                    break;
                default:
                    System.Environment.SetEnvironmentVariable("RELAYHOST", "servicebus.windows.net");
                    System.Environment.SetEnvironmentVariable("STSHOST", "accesscontrol.windows.net");
                    System.Environment.SetEnvironmentVariable("ACMHOST", "accesscontrol.windows.net");
                    System.Environment.SetEnvironmentVariable("RELAYENV", "CUSTOM");
                    System.Environment.SetEnvironmentVariable("RELAYHTTPPORT", "80");
                    System.Environment.SetEnvironmentVariable("RELAYHTTPSPORT", "443");
                    System.Environment.SetEnvironmentVariable("STSHTTPPORT", "80");
                    System.Environment.SetEnvironmentVariable("STSHTTPSPORT", "443");
                    break;
            }
            
            WriteVerbose(string.Format("Set Environment {0} ", this.Environment));
            Context.ClearContext();
        }
    }
}
