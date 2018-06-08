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
    public class RestConstants
    {
        public static readonly string RDFEEndpoint = "https://management.core.windows.net";
        public static readonly string NameSpaceURIFormat = string.Concat(RDFEEndpoint,"/{0}/services/ServiceBus/Namespaces/{1}");
        public static readonly string QueueURIFormat = string.Concat(RDFEEndpoint, "/{0}/services/ServiceBus/Namespaces/{1}/Queues/{2}");
        public static readonly string TopicsURIFormat = string.Concat(RDFEEndpoint, "/{0}/services/ServiceBus/Namespaces/{1}/Topics/{2}");
        public static readonly string SubscriptionURIFormat = string.Concat(RDFEEndpoint, "/{0}/services/ServiceBus/Namespaces/{1}/Topics/{2}/Subscriptions/{3}");
        public static readonly string NotificationHubsURIFormat = string.Concat(RDFEEndpoint, "/{0}/services/ServiceBus/Namespaces/{1}/NotificationHubs/{2}");
        public static readonly string RDFEHeader = "x-ms-version";
        public static readonly string RDFEHeaderValue = "2012-08-01";
        public static readonly string RestRequestBodyFormat = "<entry xmlns='http://www.w3.org/2005/Atom'><content type='application/xml'>{0}</content></entry>";
    }
}
