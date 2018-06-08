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
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    public class Receiver
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
            //                                   Runtime Operations
            //*****************************************************************************************************
            MessagingFactory factory = MessagingFactory.Create(serviceUri, credentials);
            try
            {
                QueueClient myQueueClient = factory.CreateQueueClient(queueName, ReceiveMode.PeekLock);

                //*****************************************************************************************************
                //                                   Receiving messages from a Queue
                //*****************************************************************************************************

                Console.WriteLine("\nReceiving messages from queue...");
                BrokeredMessage message;
                while ((message = myQueueClient.Receive(new TimeSpan(hours: 0, minutes: 0, seconds: 5))) != null)
                {
                    Console.WriteLine(
                        string.Format(
                            "Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                    // Further custom message processing could go here...
                    message.Complete();
                }

                Console.WriteLine("\nEnd of scenario, press ENTER to exit.");
                Console.ReadLine();

                // Closing factory close all entities created from the factory.
                factory.Close();
            }
            catch (Exception)
            {
                factory.Abort();
                throw;
            }
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
    }
}
