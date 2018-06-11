using Newtonsoft.Json.Linq;
using O365APIsWin8Sample.Common;
using O365APIsWin8Sample.Office365;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace O365APIsWin8Sample
{
    // Sample data structures for deserializing JSON data returned by Exchange:
    namespace SampleModel.Exchange
    {
        public class CalendarEvent
        {
            public string Subject { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public class Message
        {
            public DateTime DateTimeReceived { get; set; }
            public Recipient From { get; set; }
            public string Subject { get; set; }
        }

        public class Recipient
        {
            public string Name { get; set; }
        }

        public class Contact
        {
            public string DisplayName { get; set; }
            public string EmailAddress1 { get; set; }
        }
    }
    
    public sealed partial class ExchangeSamplePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ExchangeSamplePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
        }

        private async void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Subtitle"] = "Upcoming events";
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                SampleModel.Exchange.CalendarEvent[] events = await GetCalendarEvents();

                if (events == null)
                {
                    this.DefaultViewModel["Items"] = null;
                } 
                else 
                {
                    this.DefaultViewModel["Items"] = events.Select(calendarEvent => new
                    {
                        Primary = calendarEvent.Subject,
                        Secondary = ToLocalTimeString(calendarEvent.Start) + " to " + ToLocalTimeString(calendarEvent.End)
                    });
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                await Office365Helper.ShowErrorMessageAsync("An unexpected error has occurred", null);
            }
        }

        private static async Task<SampleModel.Exchange.CalendarEvent[]> GetCalendarEvents()
        {
            // Obtain information for communicating with the service:
            Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetExchangeServiceInfoAsync();
            if (!serviceInfo.HasValidAccessToken)
            {
                return null;
            }

            // Create a URL for retrieving the data:
            string[] queryParameters = 
            {
                String.Format(CultureInfo.InvariantCulture, "$filter=End ge {0}Z", DateTime.UtcNow.ToString("s")),
                "$top=10",
                "$select=Subject,Start,End"
            };
            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Calendar/Events?{1}",
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
                        return null;
                    }

                    var events = JObject.Parse(responseString)["value"].ToObject<SampleModel.Exchange.CalendarEvent[]>();
                    events = events.OrderBy(e => e.Start).ToArray();
                    return events;
                }
            }
        }

        private async void MailButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Subtitle"] = "Recent emails";
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                SampleModel.Exchange.Message[] messages = await GetMessages();

                if (messages == null)
                {
                    this.DefaultViewModel["Items"] = null;           
                }
                else
                {
                    this.DefaultViewModel["Items"] = messages.Select(message => new 
                    { 
                        Primary = message.Subject,
                        Secondary = "Received " + ToLocalTimeString(message.DateTimeReceived) + " from " + message.From.Name
                    });
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                await Office365Helper.ShowErrorMessageAsync("An unexpected error has occurred", null);
            }
        }

        private static async Task<SampleModel.Exchange.Message[]> GetMessages()
        {
            // Obtain information for communicating with the service:
            Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetExchangeServiceInfoAsync();
            if (!serviceInfo.HasValidAccessToken)
            {
                return null;
            }

            // Create a URL for retrieving the data:
            string[] queryParameters =
            {
                "$orderby=DateTimeSent desc",
                "$top=20",
                "$select=Subject,DateTimeReceived,From"
            };
            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Inbox/Messages?{1}",
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
                        return null;
                    }

                    return JObject.Parse(responseString)["value"].ToObject<SampleModel.Exchange.Message[]>();
                }
            }
        }

        private async void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Subtitle"] = "Contacts";
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                SampleModel.Exchange.Contact[] contacts = await GetContacts();

                if (contacts == null)
                {
                    this.DefaultViewModel["Items"] = null;
                }
                else
                {
                    this.DefaultViewModel["Items"] = contacts.Select(contact => new
                    {
                        Primary = contact.DisplayName,
                        Secondary = contact.EmailAddress1
                    });
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                await Office365Helper.ShowErrorMessageAsync("An unexpected error has occurred", null);
            }
        }

        private async Task<SampleModel.Exchange.Contact[]> GetContacts()
        {
            // Obtain information for communicating with the service:
            Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetExchangeServiceInfoAsync();
            if (!serviceInfo.HasValidAccessToken)
            {
                return null;
            }

            // Create a URL for retrieving the data:
            string[] queryParameters =
            {
                "$orderby=DisplayName",
                "$select=DisplayName,EmailAddress1"
            };
            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/Me/Contacts?{1}",
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
                        return null;
                    }

                    return JObject.Parse(responseString)["value"].ToObject<SampleModel.Exchange.Contact[]>();
                }
            }
        }

        private static string ToLocalTimeString(DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToString("g", CultureInfo.CurrentCulture);
        }

        private async void SignoutButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // TODO: When incorporating the sample code into your app, you will want to include the
            //       "Office365Helper.ClearSession()" call as part of your application's regular
            //       signout routine, rather than a separate button on each page.
            await Office365Helper.ClearSession();
            this.DefaultViewModel["Items"] = null;
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="O365APIsWin8Sample.Common.NavigationHelper.LoadState"/>
        /// and <see cref="O365APIsWin8Sample.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
