//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure AppFabric SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------


namespace Microsoft.Samples.MessagingWithQueues
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    public class Sender
    {
        const string queueName = "IssueTrackingQueue";
        static string ServiceNamespace;
        static string IssuerName;
        static string IssuerKey;

        static void Main(string[] args)
        {
            GetUserCredentials();    
            TokenProvider credentials = 
                TokenProvider.CreateSharedSecretTokenProvider(IssuerName, IssuerKey);
            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", ServiceNamespace, string.Empty);

            //*****************************************************************************************************
            //                                   Management Operations
            //*****************************************************************************************************           
            NamespaceManager namespaceClient = new NamespaceManager(serviceUri, credentials);

            Console.WriteLine("\nCreating Queue 'IssueTrackingQueue'...");
    
            // Delete if exists
            if (namespaceClient.QueueExists(queueName))
            {
                namespaceClient.DeleteQueue(queueName);
            }

            namespaceClient.CreateQueue(queueName);

            //*****************************************************************************************************
            //                                   Runtime Operations
            //*****************************************************************************************************
            MessagingFactory factory = MessagingFactory.Create(serviceUri, credentials);
            try
            {
                QueueClient myQueueClient = factory.CreateQueueClient(queueName);

                //*****************************************************************************************************
                //                                   Sending messages to a Queue
                //*****************************************************************************************************
                List<BrokeredMessage> messageList = new List<BrokeredMessage>();
                messageList.Add(CreateIssueMessage("1", "Package lost"));
                messageList.Add(CreateIssueMessage("2", "Package damaged"));
                messageList.Add(CreateIssueMessage("3", "Package defective"));

                Console.WriteLine("\nSending messages to queue...");

                foreach (BrokeredMessage message in messageList)
                {
                    myQueueClient.Send(message);
                    Console.WriteLine(
                        string.Format("Message sent: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                }

                Console.WriteLine("\nFinished sending messages, press ENTER to clean up and exit.");
                Console.ReadLine();

                // Closing factory close all entities created from the factory.
                factory.Close();
            }
            catch (Exception)
            {
                factory.Abort();
                throw;
            }

            namespaceClient.DeleteQueue(queueName);
        }

        static void GetUserCredentials()
        {
            Console.Write("Please provide the service namespace to use: ");
            ServiceNamespace = Console.ReadLine();

            Console.Write("Please provide the issuer name to use: ");
            IssuerName = Console.ReadLine();

            Console.Write("Please provide the issuer key to use: ");
            IssuerKey = Console.ReadLine();
        }

        private static BrokeredMessage CreateIssueMessage(string issueId, string issueBody)
        {            
            BrokeredMessage message = new BrokeredMessage(issueBody);
            message.MessageId = issueId;

            return message;
        }
    }
}
