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
using Newtonsoft.Json.Linq;

namespace UCourseSelector
{
    [DataContract]
    class Session
    {
        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Start")]
        public string Start { get; set; }

        [DataMember(Name = "End")]
        public string End { get; set; }

        [DataMember(Name = "Code")]
        public string Sessioncode { get; set; }

        [DataMember(Name = "Room")]
        public string Room { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Id")]
        public string Id { get; set; }

        public string Day
        {
            get
            {
                return System.Convert.ToDateTime(Start).ToLocalTime().ToString("d");
            }
        }

        public string StartTime
        {
            get
            {
                return System.Convert.ToDateTime(Start).ToLocalTime().ToString("t");
            }
        }

        public string EndTime
        {
            get
            {
                return System.Convert.ToDateTime(End).ToLocalTime().ToString("t");
            }
        }

        public CalendarEvent ToCalendarEvent()
        {
            CalendarEvent calEvent = new CalendarEvent();
            calEvent.Spid = this.Id;
            calEvent.Start = this.Start;
            calEvent.End = this.End;
            calEvent.Subject = string.Format("Session {0}: Sessioncode is [{1}] - SPid:[{2}]", this.Title, this.Sessioncode, this.Id);
            calEvent.Location = new CalendarLocation();
            calEvent.Location.DisplayName = string.Format("Location: {0}", this.Room, this.Id);

            return calEvent;
        }
    }

    class SessionList
    {
        public async Task<List<Session>> GetSessions()
        {
            List<Session> sessionList = new List<Session>();
            string errorMessage = String.Empty;

            try
            {
                // Obtain information for communicating with the service:
                Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetSharePointServiceInfoAsync(
                    AppSettings.SharePointHostResourceId,
                    AppSettings.SharePointSessionListUri);

                if (!serviceInfo.HasValidAccessToken)
                {
                    throw new Exception("Unable to get SharePoint ServiceInfo");
                }

                // Create a URL for retrieving the data:
                string[] queryParameters = new string[] 
                            {
                                "select=title,start,end,description,code"
                            };

                string requestUrl = String.Format(CultureInfo.InvariantCulture,
                    "{0}/items?{1}",
                    serviceInfo.ApiEndpoint,
                    String.Join("&", queryParameters)); 

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
                        serviceInfo, client, requestCreator))
                    {
                        // Read the response and deserialize the data:
                        string responseString = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            await Office365Helper.ShowErrorMessageAsync(serviceInfo, responseString);
                            return sessionList;
                        }

                        //sessionList = JObject.Parse(responseString)["d"]["results"].ToObject<List<Session>>();
                        JsonValue jsonResponse = JsonValue.Parse(responseString);
                        JsonObject d = jsonResponse.GetObject().GetNamedObject("d");
                        JsonArray sessionsArray = d.GetObject().GetNamedArray("results");
                        if (sessionsArray.Count > 0)
                        {
                            foreach (JsonValue o in sessionsArray)
                            {
                                Session s = CommonCode.Deserialize<Session>(o.Stringify());
                                sessionList.Add(s);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            if (!sessionList.Any())
            {
                await Office365Helper.ShowErrorMessageAsync("No Sessions Available.", errorMessage);
            }

            return sessionList;
        }
    }

}
