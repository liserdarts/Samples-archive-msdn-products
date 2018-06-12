using O365APIsWin8Sample.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Office365.OAuth;
using Microsoft.Office365.ActiveDirectory;
using System.Threading.Tasks;
using Windows.UI.Popups;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace O365APIsWin8Sample
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ActiveDirectorySamplePage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private AadGraphClient client;
        private string userId;
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
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private async Task EnsureClientCreated()
        {
            var authenticator = new Authenticator();
            var result = await authenticator.AuthenticateAsync("https://graph.windows.net/");

            this.userId = result.IdToken.UPN;

            // Create a client proxy:
            this.client = new AadGraphClient(new Uri("https://graph.windows.net/" + result.IdToken.TenantId), result.GetAccessToken);
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                User profile = await GetActiveDirectoryProfile();

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
                await ShowErrorMessageAsync(exception.Message);
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }


        private async Task<User> GetActiveDirectoryProfile()
        {

            await EnsureClientCreated();

            // Obtain data:
            var profile = await (from i in this.client.DirectoryObjects.OfType<User>()
                                 where i.ObjectId == this.userId
                                 select i).ExecuteSingleAsync();

            return profile;
        }

        private async void SignoutButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // TODO: When incorporating the sample code into your app, you will want to include the
            //       "Office365Helper.ClearSession()" call as part of your application's regular
            //      signout routine, rather than a separate button on each page.
            await new Authenticator().LogoutAsync();
            this.DefaultViewModel["Items"] = null;
        }

        private static async Task ShowErrorMessageAsync(string message)
        {
            // TODO: You can customize this method to write the error to a log file, 
            //       and/or to display a different error UI.
            await new MessageDialog(message, "Error").ShowAsync();
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
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
