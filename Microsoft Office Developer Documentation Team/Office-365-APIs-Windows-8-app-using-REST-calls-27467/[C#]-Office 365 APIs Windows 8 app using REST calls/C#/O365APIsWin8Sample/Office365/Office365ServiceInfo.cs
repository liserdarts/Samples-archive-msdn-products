using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace O365APIsWin8Sample.Office365
{
    public abstract class Office365ServiceInfo
    {
        /// <summary>
        /// The API endpoint, with no trailing slash.
        /// </summary>
        public string ApiEndpoint { get; set; }

        /// <summary>
        /// Access token (or null), used for authenticating to the API endpoint. 
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The Resource ID for the service, used to obtain a new access token or to retrieve an existing token from cache.
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// Checks whether the access token is valid (e.g., non-empty).
        /// Without a valid access token, any subsequent calls to the API endpoint will fail.
        /// </summary>
        public Boolean HasValidAccessToken
        {
            get
            {
                return (AccessToken != null);
            }
        }

        /// <summary>
        /// Extracts a human-readable error message from the response string. If the format is not recognized,
        /// or if the path is not of the expected form, an exception can be thrown.
        /// </summary>
        internal abstract string ParseErrorMessage(string responseString);

        ///////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////// FACTORY METHODS FOR RETRIEVING SERVICES ///////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns information about the Active Directory service, including its access token.
        /// On error, this method will display an error message to the user, and return an 
        ///     Office365ServiceInfo instance whose HasValidAccessToken property is set to "false".
        /// </summary>
        public static async Task<Office365ServiceInfo> GetActiveDirectoryServiceInfoAsync()
        {
            return await ActiveDirectoryServiceInfo.CreateAsync();
        }

        /// <summary>
        /// Returns information about the Exchange service, including its access token.
        /// On error, this method will display an error message to the user, and return an 
        ///     Office365ServiceInfo instance whose HasValidAccessToken property is set to "false".
        /// </summary>
        public static async Task<Office365ServiceInfo> GetExchangeServiceInfoAsync()
        {
            return await ExchangeServiceInfo.CreateAsync();
        }

        /// <summary>
        /// Returns information about the SharePoint service, including its cached access token.
        /// Note that for SharePoint, the resource ID and API endpoint will be different for each tenant,
        ///     so that information must be discovered via a Discovery Service before it can be cached.
        /// On error, this method will display an error message to the user, and return an 
        ///     Office365ServiceInfo instance whose HasValidAccessToken property is set to "false".
        /// </summary>
        public static async Task<Office365ServiceInfo> GetSharePointOneDriveServiceInfoAsync()
        {
            return await SharePointOneDriveServiceInfo.CreateAsync();
        }

        #region Private helper methods

        /// <summary>
        /// Determines the format of the response string, and extract a human-readable error message 
        /// from a response string.
        /// </summary>
        private static string GetErrorMessage(string responseString, string[] jsonErrorPath, string[] xmlErrorPath)
        {
            switch (responseString.TrimStart().FirstOrDefault())
            {
                case '{':
                    return ParseJsonErrorMessage(jsonErrorPath, responseString);
                case '<':
                    return ParseXmlErrorMessage(xmlErrorPath, responseString);
                default:
                    throw new ArgumentException("Unrecognized format for the response.");
            }
        }

        private static string ParseJsonErrorMessage(string[] path, string responseString)
        {
            JToken currentJsonNode = JObject.Parse(responseString);
            foreach (string nodeName in path)
            {
                currentJsonNode = currentJsonNode[nodeName];
            }
            return currentJsonNode.Value<string>();
        }

        private static string ParseXmlErrorMessage(string[] path, string responseString)
        {
            using (StringReader reader = new StringReader(responseString))
            {
                XDocument xmlDoc = XDocument.Load(reader);
                XNamespace xmlNamespace = xmlDoc.Root.Name.Namespace;
                XElement currentXmlNode = xmlDoc.Root;
                if (xmlDoc.Root.Name.LocalName != path.First())
                {
                    throw new Exception("Unexpected root node name: " + xmlDoc.Root.Name.LocalName);
                }
                foreach (string nodeName in path.Skip(1))
                {
                    currentXmlNode = currentXmlNode.Element(xmlNamespace + nodeName);
                }
                return currentXmlNode.Value;
            }
        }

        #endregion

        #region Private classes

        private class ActiveDirectoryServiceInfo : Office365ServiceInfo
        {
            /// <summary>
            /// This constructor is intentionally private, and should not be used.
            /// Instead, callers should create a new instance by calling the static CreateAsync() method
            /// </summary>
            private ActiveDirectoryServiceInfo() { }

            public static async Task<ActiveDirectoryServiceInfo> CreateAsync()
            {
                // For Active Directory, the resource ID and API Endpoint are static for the public O365 cloud.
                ActiveDirectoryServiceInfo info = new ActiveDirectoryServiceInfo
                {
                    ResourceId = "https://graph.windows.net/",
                    ApiEndpoint = "https://graph.windows.net"
                };
                info.AccessToken = await Office365Helper.GetAccessToken(info.ResourceId);
                return info;
            }

            internal override string ParseErrorMessage(string responseString)
            {
                string[] jsonErrorPath = { "odata.error", "message", "value" };
                string[] xmlErrorPath = { "error", "message" };
                return GetErrorMessage(responseString, jsonErrorPath, xmlErrorPath);
            }
        }

        private class ExchangeServiceInfo : Office365ServiceInfo
        {
            /// <summary>
            /// This constructor is intentionally private, and should not be used.
            /// Instead, callers should create a new instance by calling the static CreateAsync() method
            /// </summary>
            private ExchangeServiceInfo() { }

            internal static async Task<Office365ServiceInfo> CreateAsync()
            {
                // For Exchange, the resource ID and API Endpoint are static for the public O365 cloud.
                Office365ServiceInfo info = new ExchangeServiceInfo()
                {
                    ResourceId = "https://outlook.office365.com/",
                    ApiEndpoint = "https://outlook.office365.com/ews/odata"
                };
                info.AccessToken = await Office365Helper.GetAccessToken(info.ResourceId);
                return info;
            }

            internal override string ParseErrorMessage(string responseString)
            {
                string[] jsonErrorPath = { "error", "message" };
                string[] xmlErrorPath = { "error", "message" };
                return GetErrorMessage(responseString, jsonErrorPath, xmlErrorPath);
            }
        }

        private class SharePointOneDriveServiceInfo : Office365ServiceInfo
        {
            /// <summary>
            /// This constructor is intentionally private, and should not be used.
            /// Instead, callers should create a new instance by calling the static CreateAsync() method
            /// </summary>
            private SharePointOneDriveServiceInfo() { }

            internal async static Task<Office365ServiceInfo> CreateAsync()
            {
                // Attempt to build an Office365ServiceInfo object based on cached API endpoint & resource ID information:
                Office365ServiceInfo info = new SharePointOneDriveServiceInfo()
                {
                    ResourceId = (string) Office365Helper.GetFromCache("SharePointOneDriveResourceId"),
                    ApiEndpoint = (string)Office365Helper.GetFromCache("SharePointOneDriveApiEndpoint")
                };

                // If the cached Resource ID and API Endpoint are not empty, then use them:
                if ((info.ResourceId != null) && (info.ApiEndpoint != null))
                {
                    info.AccessToken = await Office365Helper.GetAccessToken(info.ResourceId);
                    return info;
                }

                // If did not return above, invoke the Discovery Service to obtain the resource ID and API endpoint:
                DiscoveryServiceInfo discoveryServiceInfo = await DiscoveryServiceInfo.CreateAsync();

                if (!discoveryServiceInfo.HasValidAccessToken)
                {
                    // Cannot communicated with Service Discovery, so return the empty SharePointOneDriveServiceInfo as is.
                    //     The missing access token will let the caller know that the service is not ready to be used.
                    return info;
                }

                DiscoveryResult[] results = await discoveryServiceInfo.DiscoverServicesAsync();
                DiscoveryResult myFilesEndpoint = results.First(result => result.Capability == "MyFiles");

                // Update and cache the resource ID and API endpoint:
                info.ResourceId = myFilesEndpoint.ServiceResourceId;
                // NOTE: In the initial Preview release of Service Discovery, the "MyFiles" endpoint URL will always
                //     start with something like "https://contoso-my.sharepoint.com/personal/<username>_contoso_com/_api",
                //     but the path following "/_api" may change over time.  For consistency, it is safer to manually
                //     extract the root path, and then append a call for the location of the Documents folder:
                info.ApiEndpoint = myFilesEndpoint.ServiceEndpointUri.Substring(
                    0, myFilesEndpoint.ServiceEndpointUri.IndexOf("/_api", StringComparison.Ordinal)) +
                    "/_api/web/getfolderbyserverrelativeurl('Documents')";
                Office365Helper.SaveInCache("SharePointOneDriveResourceId", info.ResourceId);
                Office365Helper.SaveInCache("SharePointOneDriveApiEndpoint", info.ApiEndpoint);
                info.AccessToken = await Office365Helper.GetAccessToken(info.ResourceId);
                return info;
            }

            internal override string ParseErrorMessage(string responseString)
            {
                string[] jsonErrorPath = { "error", "message", "value" };
                string[] xmlErrorPath = { "error", "message" };
                return GetErrorMessage(responseString, jsonErrorPath, xmlErrorPath);
            }
        }

        private class DiscoveryServiceInfo : Office365ServiceInfo
        {
            /// <summary>
            /// This constructor is intentionally private, and should not be used.
            /// Instead, callers should create a new instance by calling the static CreateAsync() method
            /// </summary>
            private DiscoveryServiceInfo() { }

            internal static async Task<DiscoveryServiceInfo> CreateAsync()
            {
                DiscoveryServiceInfo info = new DiscoveryServiceInfo()
                {
                    // In the initial Preview release of Service Discovery, you must use a temporary Resource ID
                    //     for Service Discovery ("Microsoft.SharePoint"), which will eventually be replaced with a different value.
                    // TODO: If this Resource ID ceases to work, check for an updated value at http://go.microsoft.com/fwlink/?LinkID=392944
                    ResourceId = "Microsoft.SharePoint",

                    ApiEndpoint = "https://api.office.com/discovery/me"
                };

                info.AccessToken = await Office365Helper.GetAccessToken(info.ResourceId);
                return info;
            }

            /// <summary>
            /// Returns information obtained via Discovery. Will throw an exception on error.
            /// </summary>
            internal async Task<DiscoveryResult[]> DiscoverServicesAsync()
            {
                // Create a URL for retrieving the data:
                string requestUrl = String.Format(CultureInfo.InvariantCulture,
                    "{0}/services",
                    ApiEndpoint);

                // Prepare the HTTP request:
                using (HttpClient client = new HttpClient())
                {
                    Func<HttpRequestMessage> requestCreator = () =>
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                        request.Headers.Add("Accept", "application/json;odata=verbose");
                        return request;
                    };

                    // Send the request using a helper method, which will add an authorization header to the request,
                    // and automatically retry with a new token if the existing one has expired.
                    using (HttpResponseMessage response = await Office365Helper.SendRequestAsync(
                        this, client, requestCreator))
                    {
                        // Read the response and deserialize the data:
                        string responseString = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Could not obtain discovery information. Service returned " +
                                response.StatusCode + ":\n\n" + responseString);
                        }

                        // If successful, return the discovery results
                        return JObject.Parse(responseString)["d"]["results"].ToObject<DiscoveryResult[]>();
                    }
                }
            }

            internal override string ParseErrorMessage(string responseString)
            {
                // Discovery is not a user-facing service, and should not be returning an error message. 
                return "An error occurred during service discovery.";
            }
        }

        /// <summary>
        /// A private class for de-serializing service entries returned by the Discovery Service
        /// </summary>
        private class DiscoveryResult
        {
            public string Capability { get; set; }
            public string ServiceEndpointUri { get; set; }
            public string ServiceResourceId { get; set; }
        }

        #endregion
    }
}