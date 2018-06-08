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
    using System.Security.Cryptography.X509Certificates;

    public sealed class SBQueueManager : SBResourceManager
    {
        internal SBQueueManager(
            string subscriptionId,
            X509Certificate2 managementCertificate,
            EntityDescription entityInfo)
        {
            this.EntityDescription = entityInfo;
            this.AzureSubscriptionID = subscriptionId;
            this.ManagementCertificate = managementCertificate;
        }

        protected override string RequestURI
        {
            get
            {
                return string.Format(
                    RestConstants.QueueURIFormat, 
                    this.AzureSubscriptionID, 
                    this.EntityDescription.ParentEntity.Name,
                    this.EntityDescription.Name);
            }
        }
    }
}
