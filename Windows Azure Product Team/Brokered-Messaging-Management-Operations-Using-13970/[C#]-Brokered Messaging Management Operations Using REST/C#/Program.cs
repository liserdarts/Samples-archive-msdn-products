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
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;
    using System.Web;

    // This sample demonstrates how to use the Windows Azure Service Bus management service to manage Service Bus Queues and Topics.
   
    public class Program
    {
        const string sbHostName = "servicebus.windows.net";
        const string acsHostName = "accesscontrol.windows.net";

        const string ServiceBusNamespace = "YOUR-NAMESPACE";

        // Specify SAS or ACS credentials. If you use ACS, uncomment the line "string token = GetAcsToken(AcsIdentity, AcsKey);" below.
        const string SasKeyName = "RootManageSharedAccessKey";
        const string SasKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=";
        const string AcsIdentity = "owner";
        const string AcsKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=";

        static string baseAddress;

        public static void Main(string[] args)
        {
            baseAddress = "https://" + ServiceBusNamespace + "." + sbHostName + "/";

            try
            {
                string token = GetSasToken(baseAddress, SasKeyName, SasKey);
                //string token = GetAcsToken(AcsIdentity, AcsKey);

                string queueName = "Queue" + Guid.NewGuid().ToString();
                CreateQueue(queueName, token);
                SendMessage(queueName, "msg1", token);
                string msg = ReceiveAndDeleteMessage(queueName, token);

                string topicName = "Topic" + Guid.NewGuid().ToString();
                string subscriptionName = "Subscription" + Guid.NewGuid().ToString();
                CreateTopic(topicName, token);
                CreateSubscription(topicName, subscriptionName, token);
                SendMessage(topicName, "msg2", token);
                Console.WriteLine(ReceiveAndDeleteMessage(topicName + "/Subscriptions/" + subscriptionName, token));

                // Get an Atom feed with all the queues in the namespace
                Console.WriteLine(GetResources("$Resources/Queues", token));

                // Get an Atom feed with all the topics in the namespace
                Console.WriteLine(GetResources("$Resources/Topics", token));

                // Get an Atom feed with all the subscriptions for the topic we just created
                Console.WriteLine(GetResources(topicName + "/Subscriptions", token));

                // Get an Atom feed with all the rules for the topic and subscritpion we just created
                Console.WriteLine(GetResources(topicName + "/Subscriptions/" + subscriptionName + "/Rules", token));

                // Delete the queue we created
                DeleteResource(queueName, token);

                // Delete the topic we created
                DeleteResource(topicName, token);

                // Get an Atom feed with all the topics in the namespace, it shouldn't have the one we created now
                Console.WriteLine(GetResources("$Resources/Topics", token));

                // Get an Atom feed with all the queues in the namespace, it shouldn't have the one we created now
                Console.WriteLine(GetResources("$Resources/Queues", token));
            }
            catch (WebException we)
            {
                using (HttpWebResponse response = we.Response as HttpWebResponse)
                {
                    if (response != null)
                    {
                        Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
                    }
                    else
                    {
                        Console.WriteLine(we.ToString());
                    }
                }
            }

            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadLine();
        }

        public static string GetSasToken(string uri, string keyName, string key)
        {
            // Set token lifetime to 20 minutes.
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
            uint tokenExpirationTime = Convert.ToUInt32(diff.TotalSeconds) + 20 * 60;

            string stringToSign = HttpUtility.UrlEncode(uri) + "\n" + tokenExpirationTime;
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));

            string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            string token = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
                HttpUtility.UrlEncode(uri), HttpUtility.UrlEncode(signature), tokenExpirationTime, keyName);
            Console.WriteLine("Token: " + token);
            return token;
        }
        
        private static string GetAcsToken(string issuerName, string issuerSecret)
        {
            var acsEndpoint = "https://" + ServiceBusNamespace + "-sb." + acsHostName + "/WRAPv0.9/";

            // Note that the realm used when requesting a token uses the HTTP scheme, even though
            // calls to the service are always issued over HTTPS
            var realm = "http://" + ServiceBusNamespace + "." + sbHostName + "/";

            NameValueCollection values = new NameValueCollection();
            values.Add("wrap_name", issuerName);
            values.Add("wrap_password", issuerSecret);
            values.Add("wrap_scope", realm);

            WebClient webClient = new WebClient();
            byte[] response = webClient.UploadValues(acsEndpoint, values);

            string responseString = Encoding.UTF8.GetString(response);

            var responseProperties = responseString.Split('&');
            var tokenProperty = responseProperties[0].Split('=');
            var token = Uri.UnescapeDataString(tokenProperty[1]);

            return "WRAP access_token=\"" + token + "\"";
        }

        private static string CreateQueue(string queueName, string token)
        {
            // Create the URI of the new Queue, note that this uses the HTTPS scheme
            var queueAddress = baseAddress + queueName;
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;

            Console.WriteLine("\nCreating queue {0}", queueAddress);
            // Prepare the body of the create queue request
            var putData = @"<entry xmlns=""http://www.w3.org/2005/Atom"">
                                          <title type=""text"">" + queueName + @"</title>
                                          <content type=""application/xml"">
                                            <QueueDescription xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.microsoft.com/netservices/2010/10/servicebus/connect"" />
                                          </content>
                                        </entry>";

            byte[] response = webClient.UploadData(queueAddress, "PUT", Encoding.UTF8.GetBytes(putData));
            return Encoding.UTF8.GetString(response);
        }

        private static void SendMessage(string relativeAddress, string body, string token)
        {
            string fullAddress = baseAddress + relativeAddress + "/messages" + "?timeout=60";
            Console.WriteLine("\nSending message {0} - to address {1}", body, fullAddress);
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;

            webClient.UploadData(fullAddress, "POST", Encoding.UTF8.GetBytes(body));
        }

        private static string ReceiveAndDeleteMessage(string relativeAddress, string token)
        {
            string fullAddress = baseAddress + relativeAddress + "/messages/head" + "?timeout=60";
            Console.WriteLine("\nRetrieving message from {0}", fullAddress);
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;

            byte[] response = webClient.UploadData(fullAddress, "DELETE", new byte[0]);
            string responseStr = Encoding.UTF8.GetString(response);

            Console.WriteLine(responseStr);
            return responseStr;
        }

        private static string CreateTopic(string topicName, string token)
        {
            var topicAddress = baseAddress + topicName;
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;

            Console.WriteLine("\nCreating topic {0}", topicAddress);
            // Prepare the body of the create queue request
            var putData = @"<entry xmlns=""http://www.w3.org/2005/Atom"">
                                          <title type=""text"">" + topicName + @"</title>
                                          <content type=""application/xml"">
                                            <TopicDescription xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.microsoft.com/netservices/2010/10/servicebus/connect"" />
                                          </content>
                                        </entry>";

            byte[] response = webClient.UploadData(topicAddress, "PUT", Encoding.UTF8.GetBytes(putData));
            return Encoding.UTF8.GetString(response);
        }

        private static string CreateSubscription(string topicName, string subscriptionName, string token)
        {
            var subscriptionAddress = baseAddress + topicName + "/Subscriptions/" + subscriptionName;
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;

            Console.WriteLine("\nCreating subscription {0}", subscriptionAddress);
            // Prepare the body of the create queue request
            var putData = @"<entry xmlns=""http://www.w3.org/2005/Atom"">
                                          <title type=""text"">" + subscriptionName + @"</title>
                                          <content type=""application/xml"">
                                            <SubscriptionDescription xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.microsoft.com/netservices/2010/10/servicebus/connect"" />
                                          </content>
                                        </entry>";

            byte[] response = webClient.UploadData(subscriptionAddress, "PUT", Encoding.UTF8.GetBytes(putData));
            return Encoding.UTF8.GetString(response);
        }

        private static string GetResources(string relativeAddress, string token)
        {
            string fullAddress = baseAddress + relativeAddress;
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;
            Console.WriteLine("\nGetting resources from {0}", fullAddress);
            return FormatXml(webClient.DownloadString(fullAddress));
        }

        private static string DeleteResource(string topicName, string token)
        {
            string fullAddress = baseAddress + topicName;
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Authorization] = token;

            Console.WriteLine("\nDeleting resource at {0}", fullAddress);
            byte[] response = webClient.UploadData(fullAddress, "DELETE", new byte[0]);
            return Encoding.UTF8.GetString(response);
        }

        /// <summary>
        /// Formats a string conataining XML to be more human readable, it is intended for display purposes
        /// </summary>        
        private static string FormatXml(string inputXml)
        {
            XmlDocument document = new XmlDocument();
            document.Load(new StringReader(inputXml));

            StringBuilder builder = new StringBuilder();
            using (XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder)))
            {
                writer.Formatting = Formatting.Indented;
                document.Save(writer);
            }

            return builder.ToString();
        }
    }
}