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

namespace Microsoft.Samples.ServiceBus.ResourceProviderSDK
{
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public sealed class SBNotificationHubManager : SBResourceManager
    {
        internal SBNotificationHubManager(
            string subscriptionId,
            X509Certificate2 managementCertificate,
            EntityDescription entityDescription)
        {
            this.EntityDescription = entityDescription;
            this.AzureSubscriptionID = subscriptionId;
            this.ManagementCertificate = managementCertificate;
        }

        protected override string RequestURI
        {
            get
            {
                return string.Format(
                    RestConstants.NotificationHubsURIFormat,
                    this.AzureSubscriptionID,
                    this.EntityDescription.ParentEntity.Name,
                    this.EntityDescription.Name);
            }
        }
    }
}
