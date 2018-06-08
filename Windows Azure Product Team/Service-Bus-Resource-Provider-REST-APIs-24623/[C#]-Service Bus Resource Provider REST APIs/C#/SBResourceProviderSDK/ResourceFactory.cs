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
    using System;
    using System.Security.Cryptography.X509Certificates;

    public enum SBRestResourceType
    {
        Namespace,
        Queue,
        Topic,
        Subscription,
        NotificationHub
    }

    public sealed class ResourceFactory
    {
        /// <param name="subscriptionId">https://Windows.Azure.com</param>
        /// <param name="managementCertificate">Add a management certificate to the Windows Azure Subscription http://msdn.microsoft.com/en-us/library/gg551726.aspx</param>
        public static SBResourceManager Get(
            string subscriptionId, 
            X509Certificate2 managementCertificate,
            SBRestResourceType restResourceType,
            EntityDescription entityDescription)
        {
            if (string.IsNullOrEmpty(subscriptionId))
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (managementCertificate == null)
            {
                throw new ArgumentNullException("managementCertificate");
            }

            if (entityDescription == null)
            {
                throw new ArgumentNullException("entityDescription");
            }

            switch (restResourceType)
            {
                case SBRestResourceType.Namespace:
                    return new SBNamespaceManager(subscriptionId, managementCertificate, entityDescription as NamespaceDescription);
                    break;
                case SBRestResourceType.Queue:
                    return new SBQueueManager(subscriptionId, managementCertificate, entityDescription);
                    break;
                case SBRestResourceType.Topic:
                    return new SBTopicManager(subscriptionId, managementCertificate, entityDescription);
                    break;
                case SBRestResourceType.Subscription:
                    return new SBSubscriptionManager(subscriptionId, managementCertificate, entityDescription);
                    break;
                case SBRestResourceType.NotificationHub:
                    return new SBNotificationHubManager(subscriptionId, managementCertificate, entityDescription);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
