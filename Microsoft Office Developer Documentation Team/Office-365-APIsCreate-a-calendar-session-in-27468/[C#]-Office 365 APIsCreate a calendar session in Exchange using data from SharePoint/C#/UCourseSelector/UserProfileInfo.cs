using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Net.Http.Headers;
using Windows.UI.Xaml.Data;
using System.Globalization;
using UCourseSelector.Office365;
using Newtonsoft.Json;

namespace UCourseSelector
{
    [DataContract]
    public class UserProfile
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "givenName")]
        public string GivenName { get; set; }

        [DataMember(Name = "surname")]
        public string Surname { get; set; }

        [DataMember(Name = "department")]
        public string Department { get; set; }

        [DataMember(Name = "mail")]
        public string EMail { get; set; }
    }

    public static class UserProfileInfo
    {
        public static async Task<UserProfile> GetUserProfileRequest()
        {
            UserProfile userProfile = null;
            string errorMessage = string.Empty;

            try
            {
                // Obtain information for communicating with the service:
                Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetActiveDirectoryServiceInfoAsync();
                if (!serviceInfo.HasValidAccessToken)
                {
                    throw new Exception("Unable to get AAD ServiceInfo");
                }

                // Create a URL for retrieving the data:
                string[] queryParameters = { "api-version=2013-11-08" };
                string requestUrl = String.Format(CultureInfo.InvariantCulture,
                    "{0}/me?{1}",
                    serviceInfo.ApiEndpoint,
                    String.Join("&", queryParameters));

                // Prepare the HTTP request:
                using (HttpClient client = new HttpClient())
                {
                    Func<HttpRequestMessage> requestCreator = () =>
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                        request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                        return request;
                    };

                    using (HttpResponseMessage response = await Office365Helper.SendRequestAsync(
                        serviceInfo, client, requestCreator))
                    {
                        // Read the response and deserialize the data:
                        string responseString = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            await Office365Helper.ShowErrorMessageAsync(serviceInfo, responseString);
                            return userProfile;
                        }

                        userProfile = JsonConvert.DeserializeObject<UserProfile>(responseString);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return userProfile;
        }
    }
}
