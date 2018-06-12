using WinStoreUcwaAppIM.Common;
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

using System.Net;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WinStoreUcwaAppIM
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public UcwaApp UcwaApp { get; private set; }

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


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
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
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.UcwaApp = e.NavigationParameter as UcwaApp;
            if (this.UcwaApp != null)
            {
                this.UcwaApp.OnEventNotificationsReceived += this.ProcessEventNotifications;
                this.UcwaApp.OnErrorReported += this.ReportError;
                this.UcwaApp.OnProgressReported += ReportProgress;
            }

            // Restore values stored in app data container.
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("password"))
            {
                passwordBox.Password = Windows.Storage.ApplicationData.Current.LocalSettings.Values["password"].ToString();
            }
            if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey("userName"))
            {
                textBoxUserName.Text = Windows.Storage.ApplicationData.Current.RoamingSettings.Values["userName"].ToString();
            }
            if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey("saveUserInfo") &&
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values["saveUserInfo"].ToString() == "checked")
                checkBoxSaveUserInfo.IsChecked = true;
            if (checkBoxSaveUserInfo.IsChecked != true)
            {
                SaveUserInfo_Unchecked(this, null);
            }
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("useWindowsAuth") &&
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["useWindowsAuth"].ToString() == "checked")
                checkBoxUseWindowsAuth.IsChecked = true;

            if (checkBoxUseWindowsAuth.IsChecked != true)
            {
                UseWindowsAuth_Unchecked(this, null);
            }

            if (e.PageState != null)
            {
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("status"))
                    textBlockStatus.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["status"].ToString();
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("events"))
                    textBlockEvents.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["events"].ToString();
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
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["status"] = textBlockStatus.Text;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["events"] = textBlockEvents.Text;
            if (this.UcwaApp != null)
            {
                this.UcwaApp.OnEventNotificationsReceived -= this.ProcessEventNotifications;
                this.UcwaApp.OnErrorReported -= this.ReportError;
                this.UcwaApp.OnProgressReported -= ReportProgress;
            }

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
            //// debug
            //var srcType = e.SourcePageType;
            //var content = e.Content;
            //var parameter = e.Parameter;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            //// debug
            //var srcType = e.SourcePageType; 
            //var content = e.Content;
            //var parameter = e.Parameter;
            
        }

        #endregion
        void ProcessEventNotifications(UcwaEventsData events)
        {
            var eventData = DateTime.Now.ToString() + "\r\n" + events.OuterXml + "\r\n";
            ShowEventNotifications(eventData);
        }
        void ShowEventNotifications(string msg)
        {
            this.textBlockEvents.Text = msg;
        }
        void ReportProgress(string msg, HttpStatusCode status = HttpStatusCode.OK)
        {
            var progress = DateTime.Now + ": " + "[" + status.ToString() + "] " + msg + "\r\n";
            this.ShowOpStatus(progress);

        }
        void ShowOpStatus(string msg)
        {
            textBlockStatus.Text += msg;
        }
        void ReportError(Exception e)
        {
            var opStatus = DateTime.Now + ": " + e.Message + "\r\n" + e.StackTrace + "\r\n";
            this.ShowOpStatus(opStatus);
        }


        UcwaAppAuthenticationTypes GetAuthenticationType()
        {
            if (checkBoxUseWindowsAuth.IsChecked == true)
                return UcwaAppAuthenticationTypes.Windows;
            else
                return UcwaAppAuthenticationTypes.Password;
        }
        private async void buttonSignIn_Clicked(object sender, RoutedEventArgs e)
        {
            var status = await this.UcwaApp.SignIn(textBoxUserName.Text, passwordBox.Password, GetAuthenticationType());
            ReportProgress("SignIn status: IsSignedIn=" + this.UcwaApp.IsSignedIn, status);
            
        }

        private void UserName_Changed(object sender, TextChangedEventArgs e)
        {
            if (checkBoxSaveUserInfo.IsChecked == true)
            {
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values["userName"] = textBoxUserName.Text;
            }

        }

        private void Password_Changed(object sender, RoutedEventArgs e)
        {
            if (checkBoxSaveUserInfo.IsChecked == true)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["password"] = passwordBox.Password;
            }
        }

        private void SaveUserInfo_Checked(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.RoamingSettings.Values["saveUserInfo"] = "checked";
            UserName_Changed(null, null);
            Password_Changed(null, null);
        }

        private void SaveUserInfo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey("userName"))
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values.Remove("userName");
            if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey("saveUserInfo"))
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values.Remove("saveUserInfo");
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("password"))
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove("password");
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("useWindowsAuth"))
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove("useWindowsAuth");
        }

        private void UseWindowsAuth_Checked(object sender, RoutedEventArgs e)
        {
            textBoxUserName.IsEnabled = false;
            passwordBox.IsEnabled = false;
            if (checkBoxSaveUserInfo.IsChecked == true)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["useWindowsAuth"] = "checked";
            }
        }

        private void UseWindowsAuth_Unchecked(object sender, RoutedEventArgs e)
        {
            textBoxUserName.IsEnabled = true;
            passwordBox.IsEnabled = true;
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("useWindowsAuth"))
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove("useWindowsAuth");
        }

        private void ButtonShowMe_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the MePage
            this.Frame.Navigate(typeof(MePage), this.UcwaApp);
        }

        private void ButtonComm_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CommunicationsPage), this.UcwaApp);

        }
    }
}
