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

namespace SBRestSample
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Samples.ServiceBus.ResourceProviderSDK;

    class Program
    {
        private const string azureCertificateThumbprint = "<---------------CertThumbprint--------------->";
        private const string SubscriptionID = "<-------------AzureSubID------------------>";
        private readonly static string SBNamespace = "SBNS" + Guid.NewGuid().ToString("N");

        // Didn't want to use all the regions in the below code - something for folks to try !
        private static readonly string[] regions = { "South Central US", "West US" };

        static void Main()
        {
            Console.WriteLine("Hitting Windows Azure Management (RDFE) Endpoint: {0}", RestConstants.RDFEEndpoint);
            X509Certificate2 managementCertificate = Util.GetManagementCertificate(azureCertificateThumbprint);

            SBNamespaceManager sbNamespace = ResourceFactory.Get(
                    SubscriptionID,
                    managementCertificate,
                    SBRestResourceType.Namespace,
                    new NamespaceDescription(SBNamespace, regions[0]))
                as SBNamespaceManager;
            
            try
            {
                Console.WriteLine("Creating Service Bus Namespace: {0}", SBNamespace);
                if (sbNamespace.Create())
                {
                    sbNamespace.WaitUntillActive();
                    Console.WriteLine("Created Service Bus Namespace: {0}", sbNamespace.LookUp().Name);
                    SBQueueManager queue = ResourceFactory.Get(
                        SubscriptionID,
                        managementCertificate,
                        SBRestResourceType.Queue,
                        new QueueDescription("IssueQueue", sbNamespace.LookUp()))
                        as SBQueueManager;

                    SBTopicManager topic = ResourceFactory.Get(
                        SubscriptionID,
                        managementCertificate,
                        SBRestResourceType.Topic,
                            new TopicDescription("CoolTopic", sbNamespace.LookUp()))
                        as SBTopicManager;

                    SBNotificationHubManager notificationHub = ResourceFactory.Get(
                        SubscriptionID,
                        managementCertificate,
                        SBRestResourceType.NotificationHub,
                            new NotificationHubDescription("NotificationHub", sbNamespace.LookUp()))
                        as SBNotificationHubManager;

                    queue.Create();
                    Console.WriteLine("Created the Queue: {0}", queue.LookUp().Name);
                    notificationHub.Create();
                    Console.WriteLine("Created Notification Hub: {0}{1}", Environment.NewLine, notificationHub.LookUp().ToString());

                    if (topic.Create())
                    {
                        topic.WaitUntillActive();
                        Console.WriteLine("Created the Topic: {0}", topic.LookUp().Name);
                        
                        SBSubscriptionManager subscription = ResourceFactory.Get(
                        SubscriptionID,
                        managementCertificate,
                        SBRestResourceType.Subscription,
                            new SubscriptionDescription("SimpleSubscription", topic.LookUp()))
                        as SBSubscriptionManager;

                        subscription.Create();
                        Console.WriteLine("Created Subscription: {0}, in Topic: {1}", subscription.LookUp().Name, subscription.LookUp().ParentEntity.Name);

                        Console.WriteLine("Namespace.GET: {0}{1}", Environment.NewLine, sbNamespace.LookUp().ToString());
                        Console.WriteLine("Topic.Get: {0}{1}", Environment.NewLine, topic.LookUp().ToString());
                        Console.WriteLine("Subscription.GET: {0}{1}", Environment.NewLine, subscription.LookUp().ToString());

                        subscription.WaitUntillActive();

                        subscription.Delete();
                        topic.Delete();
                    }

                    Console.WriteLine("Queue.GET: {0}{1}", Environment.NewLine, queue.LookUp().ToString());
                    
                    queue.Delete();
                    notificationHub.Delete();
                }
            }
            finally
            {
                if (sbNamespace != null)
                {
                    Console.WriteLine("deleting namespace: {0}", sbNamespace.LookUp().Name);
                    sbNamespace.Delete();
                }
            }
        }
    }
}
