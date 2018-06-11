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
using UCourseSelector.Office365;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace UCourseSelector
{
    public class DateUTCToDateLocal : IValueConverter
    {
        #region IValueConverter Members
        // Define the Convert method to change a DateTime object to 
        // a month string.
        public object Convert(object value, Type targetType,

        object parameter, string language)
        {
            // The value parameter is the data from the source object.
            DateTime thisutcdate = System.Convert.ToDateTime(value);
            return thisutcdate.ToLocalTime().ToString();
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
        object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [DataContract]
    public class CalendarLocation
    {
        [DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }
    }

    [DataContract]
    public class CalendarEvent
    {
        [DataMember(Name = "@odata.type")]
        public string EntityType { get; set; }

        [DataMember(Name = "Subject")]
        public string Subject { get; set; }

        [DataMember(Name = "Start")]
        public string Start { get; set; }

        [DataMember(Name = "End")]
        public string End { get; set; }

        [DataMember(Name = "Location")]
        public CalendarLocation Location { get; set; }

        public DateTime StartDateTime
        {
            get
            {
                DateTime thisutcdate = System.Convert.ToDateTime(Start);
                return thisutcdate;
            }
        }

        public string StartTime
        {
            get
            {
                DateTime thisutcdate = System.Convert.ToDateTime(Start);
                return thisutcdate.ToLocalTime().ToString("t");
            }
        }
        public string EndTime
        {
            get
            {
                DateTime thisutcdate = System.Convert.ToDateTime(End);
                return thisutcdate.ToLocalTime().ToString("t");
            }
        }

        public CalendarEvent()
        {
            this.EntityType = "#Microsoft.Exchange.Services.OData.Model.Event";
        }

        public string Id { get; set; } // Calendar Event ID

        public string Spid { get; set; } // Share Point ID
    }

    class CalendarEvents
    {
        private string _calendarItemIdForUpdate;

        private List<CalendarEvent> _events = new List<CalendarEvent>();

        Office365ServiceInfo _serviceInfo;
        HttpClient _httpClient;

        public static async Task<CalendarEvents> CreateCalendarInstance()
        {
            CalendarEvents calendarEvents = new CalendarEvents();

            // Obtain information for communicating with the service:
            calendarEvents._serviceInfo = await Office365ServiceInfo.GetExchangeServiceInfoAsync();
            if (!calendarEvents._serviceInfo.HasValidAccessToken)
            {
                await Office365Helper.ShowErrorMessageAsync("Unable to get Exchange ServiceInfo", string.Empty);
                return null;
            }

            calendarEvents._httpClient = new HttpClient();

            return calendarEvents;
        }

        private CalendarEvents() { }

        public async Task<List<CalendarEvent>> GetCalendarEvents(DateTime eventsForDay)
        {
            DateTime startUtc = new DateTime(eventsForDay.Year, eventsForDay.Month, eventsForDay.Day).ToUniversalTime();
            DateTime endUtc = startUtc.Add(new TimeSpan(24, 0, 0));

            // Get OData ...
            int pageSize = 5;
            int pageCounter = pageSize;
            bool firstPage = true;

            string[] queryParameters =
            {
                String.Format(CultureInfo.InvariantCulture, "$filter=End ge {0}Z and Start le {1}Z", 
                                                            startUtc.ToString("s"),
                                                            endUtc.ToString("s")),
                String.Format(CultureInfo.InvariantCulture, "$top={0}",  
                                                            System.Convert.ToString(pageCounter)),
                                                            "$select=Subject,Start,End,Location",

            };

            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Calendar/Events?{1}",
                _serviceInfo.ApiEndpoint,
                String.Join("&", queryParameters));

            while ((await GetCalendarEventsRequest(requestUrl, firstPage)) > 0)
            {
                queryParameters[1] = String.Format(CultureInfo.InvariantCulture, "$skip={0}",  System.Convert.ToString(pageCounter));

                requestUrl = String.Format(CultureInfo.InvariantCulture,
                    "{0}/Me/Calendar/Events?{1}",
                    _serviceInfo.ApiEndpoint,
                    String.Join("&", queryParameters));

                pageCounter += pageSize;
                firstPage = false;
            }

            return _events;
        }

        private async Task<int> GetCalendarEventsRequest(string requestUrl, bool firstPage)
        {
            string errorMessage = string.Empty;

            try
            {
                Func<HttpRequestMessage> requestCreator = () =>
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                    request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                    return request;
                };

                HttpResponseMessage response = await Office365Helper.SendRequestAsync(_serviceInfo, _httpClient, requestCreator);
                string responseString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    await Office365Helper.ShowErrorMessageAsync(_serviceInfo, responseString);
                    return 0;
                }

                JsonValue jsonResponse = JsonValue.Parse(responseString);
                JsonArray events = jsonResponse.GetObject().GetNamedArray("value");
                if (events.Count > 0)
                {
                    foreach (JsonValue o in events)
                    {
                        //CalendarEvent c = JsonConvert.DeserializeObject<CalendarEvent>(o.Stringify());
                        CalendarEvent c = CommonCode.Deserialize<CalendarEvent>(o.Stringify());
                        JsonObject json = JsonObject.Parse(o.Stringify());
                        c.Id = json.GetNamedString("Id");
                        _events.Add(c);

                        _calendarItemIdForUpdate = c.Id;
                    }
                    return events.Count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            await Office365Helper.ShowErrorMessageAsync("Unexpected error retrieving events", errorMessage);

            return 0;
        }

        public async Task<bool> DeleteCalendarEvent(string Id)
        {
            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Calendar/Events('{1}')",
                _serviceInfo.ApiEndpoint,
                Id);

            Func<HttpRequestMessage> requestCreator = () =>
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
                request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                return request;
            };

            HttpResponseMessage response = await Office365Helper.SendRequestAsync(_serviceInfo, _httpClient, requestCreator);
            string responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                await Office365Helper.ShowErrorMessageAsync(_serviceInfo, responseString);
                return false;
            }

            return true;
        }

        public async Task<CalendarEvent> UpdateCalendarEvent(CalendarEvent calendarEvent)
        {
            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Calendar/Events('{1}')",
                _serviceInfo.ApiEndpoint,
                _calendarItemIdForUpdate);

            // string postData = JsonConvert.SerializeObject(calendarEvent);
            string postData = CommonCode.Serialize(calendarEvent);

            Func<HttpRequestMessage> requestCreator = () =>
            {
                var patch = new HttpMethod("PATCH");
                HttpRequestMessage request = new HttpRequestMessage(patch, requestUrl);
                request.Content = new StringContent(postData);
                request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return request;
            };

            HttpResponseMessage response = await Office365Helper.SendRequestAsync(_serviceInfo, _httpClient, requestCreator);
            string responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                await Office365Helper.ShowErrorMessageAsync(_serviceInfo, responseString);
                return null;
            }

            // CalendarEvent c = JsonConvert.DeserializeObject<CalendarEvent>(responseString);
            CalendarEvent c = CommonCode.Deserialize<CalendarEvent>(responseString);

            return c;
        }

        public async Task<CalendarEvent> AddOrUpdateCalendarEvent(CalendarEvent calendarEvent)
        {
            // Query to see if event already exists. If so, do Update instead.
            string[] queryParameters =
            {
                String.Format(CultureInfo.InvariantCulture, "$filter=contains(Subject,'Spid:[{0}]')", 
                                                            calendarEvent.Spid),
            };

            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Calendar/Events?{1}",
                _serviceInfo.ApiEndpoint,
                String.Join("&", queryParameters));

            int calendarItemCount = (await GetCalendarEventsRequest(requestUrl, true));
            if (calendarItemCount > 1)
            {
                return null;
            }
            if (calendarItemCount == 1)
            {
                return (await UpdateCalendarEvent(calendarEvent));
            }

            requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Calendar/Events",
                _serviceInfo.ApiEndpoint);

            // string postData = JsonConvert.SerializeObject(calendarEvent);
            string postData = CommonCode.Serialize(calendarEvent);

            Func<HttpRequestMessage> requestCreator = () =>
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                request.Content = new StringContent(postData);
                request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return request;
            };

            HttpResponseMessage response = await Office365Helper.SendRequestAsync(_serviceInfo, _httpClient, requestCreator);
            string responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                await Office365Helper.ShowErrorMessageAsync(_serviceInfo, responseString);
                return null;
            }

            // CalendarEvent c = JsonConvert.DeserializeObject<CalendarEvent>(responseString);
            CalendarEvent c = CommonCode.Deserialize<CalendarEvent>(responseString);

            return c;
        }
    }
}
