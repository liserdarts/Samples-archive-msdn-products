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

    [Cmdlet("Set", "ServiceBusCredentials")]
    public class SetServiceBusCredentialsCmdlet : PSCmdlet
    {
        public SetServiceBusCredentialsCmdlet()
        {
            this.IssuerName = "owner";
        }

        [Parameter(
        Mandatory = false,
        HelpMessage = "ACS IssuerName")]
        public string IssuerName
        {
            get;
            set;
        }

        [Parameter(
        Mandatory = true,
        HelpMessage = "ACS IssuerSecret")]
        public string IssuerSecret
        {
            get;
            set;
        }

        [Parameter(
        Mandatory = true,
        HelpMessage = "ServiceBus namespace")]
        public string Namespace
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            this.SessionState.PSVariable.Set(new PSVariable("IssuerName", this.IssuerName));
            this.SessionState.PSVariable.Set(new PSVariable("IssuerSecret", this.IssuerSecret));
            this.SessionState.PSVariable.Set(new PSVariable("Namespace", this.Namespace));

            WriteVerbose(string.Format("Set IssuerName {0} ", this.IssuerName));
            WriteVerbose(string.Format("Set IssuerSecret {0} ", this.IssuerSecret));
            WriteVerbose(string.Format("Set Namespace {0} ", this.Namespace));

            Context.ClearContext();
        }
    }
}
