using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Data.Json;
using System.Globalization;
using UCourseSelector.Office365;
using Newtonsoft.Json;

namespace UCourseSelector
{

    [DataContract]
    public class Body
    {
        [DataMember(Name = "ContentType")]
        public string ContentType { get; set; }
        [DataMember(Name = "Content")]
        public string Content { get; set; }

        public Body(string content)
        {
            this.ContentType = "HTML";
            this.Content = content;
        }
    }

    [DataContract]
    public class Recipient
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Address")]
        public string Address { get; set; }

        public Recipient(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
    }

    [DataContract]
    public class Message
    {

        [DataMember(Name = "@odata.type")]
        public string EntityType { get; set; }

        [DataMember(Name = "Subject")]
        public string Subject { get; set; }

        [DataMember(Name = "Body")]
        public Body Body { get; set; }

        [DataMember(Name = "ToRecipients")]
        public List<Recipient> ToRecipients { get; set; }

        public Message()
        {
            this.EntityType = "#Microsoft.Exchange.Services.OData.Model.Message";
            ToRecipients = new List<Recipient>();

        }
    }

    public static class Mail
    {
        public static async Task SendMail(Message mailMessage)
        {
            // Obtain information for communicating with the service:
            Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetExchangeServiceInfoAsync();
            if (!serviceInfo.HasValidAccessToken)
            {
                await Office365Helper.ShowErrorMessageAsync("Unable to get Exchange ServiceInfo", string.Empty);
                return;
            }

            HttpClient client = new HttpClient();

            // Create a URL for retrieving the data:
            string[] queryParameters = new string[] 
                        {
                            "MessageDisposition=SendOnly"
                        };

            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Messages?{1}",
                serviceInfo.ApiEndpoint,
                String.Join("&", queryParameters));

            // string postData = JsonConvert.SerializeObject(mailMessage);
            string postData = CommonCode.Serialize(mailMessage);

            Func<HttpRequestMessage> requestCreator = () =>
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                request.Content = new StringContent(postData);
                request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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
                }

                // TODO maybe display a confirmation.
            }
        }
    }
}
