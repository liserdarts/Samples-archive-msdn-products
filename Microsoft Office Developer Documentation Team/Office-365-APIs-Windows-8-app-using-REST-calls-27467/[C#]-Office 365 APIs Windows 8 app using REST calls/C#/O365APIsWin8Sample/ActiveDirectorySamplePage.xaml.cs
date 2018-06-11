using Newtonsoft.Json;
using O365APIsWin8Sample.Common;
using O365APIsWin8Sample.Office365;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace O365APIsWin8Sample
{
    // Sample data structure for deserializing JSON data returned by Windows Azure Active Directory:
    namespace SampleModel.ActiveDirectory
    {
        public class UserProfile
        {
            public string DisplayName { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
        }
    }

    public sealed partial class ActiveDirectorySamplePage : Page
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

        public ActiveDirectorySamplePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                SampleModel.ActiveDirectory.UserProfile profile = await GetActiveDirectoryProfile();

                if (profile == null)
                {
                    this.DefaultViewModel["Items"] = null;
                }
                else
                {
                    this.DefaultViewModel["Items"] = new[] {
                        new { Primary = profile.DisplayName , Secondary = "Display Name"},
                        new { Primary = profile.GivenName, Secondary = "First Name" },
                        new { Primary = profile.Surname, Secondary = "Last Name" }
                    };
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

        private static async Task<SampleModel.ActiveDirectory.UserProfile> GetActiveDirectoryProfile()
        {
            // Obtain information for communicating with the service:
            Office365ServiceInfo serviceInfo = await Office365ServiceInfo.GetActiveDirectoryServiceInfoAsync();
            if (!serviceInfo.HasValidAccessToken)
            {
                return null;
            }

            // Create a URL for retrieving the data:
            string[] queryParameters = { "api-version=2013-11-08" };
            string requestUrl = String.Format(CultureInfo.InvariantCulture,
                "{0}/{1}/users/{2}?{3}",
                serviceInfo.ApiEndpoint,
                WebUtility.UrlEncode(Office365Helper.TenantId),
                WebUtility.UrlEncode(Office365Helper.UserId),
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

                    return JsonConvert.DeserializeObject<SampleModel.ActiveDirectory.UserProfile>(responseString);
                }
            }
        }

        private async void SignoutButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // TODO: When incorporating the sample code into your app, you will want to include the
            //       "Office365Helper.ClearSession()" call as part of your application's regular
            //      signout routine, rather than a separate button on each page.
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
