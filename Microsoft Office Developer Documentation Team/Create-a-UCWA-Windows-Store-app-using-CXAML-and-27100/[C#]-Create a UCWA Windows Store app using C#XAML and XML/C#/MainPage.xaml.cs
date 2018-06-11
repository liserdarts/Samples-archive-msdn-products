using UcwaWinStoreHello.Common;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace UcwaWinStoreHello
{
    //public sealed class SignInParameter
    //{
    //    internal string UserName { get; private set; }
    //    internal string Password { get; private set; }
    //    public SignInParameter(string name, string pass)
    //    {
    //        this.UserName = name;
    //        this.Password = pass;
    //    }
    //}
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // app-specific
        string introText =  "A simple UCWA Windows Store app to demonstrate programming UCWA using C#/XAML." + 
                            " To procede, please sign in using a valid user account";
            
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
            //textBlockIntro.Text = introText;
            // Restore values stored in session state
            if (e.PageState == null)
                textBlockIntro.Text = introText;
            else if (e.PageState.ContainsKey("introText"))
                textBlockIntro.Text = e.PageState["introText"].ToString();

            // Restore values stored in app data.
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
            e.PageState["introText"] = textBlockIntro.Text;

            if (checkBoxSaveUserInfo.IsChecked == true)
                SaveUserInfo_Checked(this, null);
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
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values.Remove("userName") ;
            if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey("saveUserInfo"))
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values.Remove("saveUserInfo");
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("password"))
                Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove("password");
        }

        private void buttonSignIn_Cliked(object sender, RoutedEventArgs e)
        {
            var userName = textBoxUserName.Text;
            var password = passwordBox.Password;

            // Navigate to the MePage
            this.Frame.Navigate(typeof(MePage), new SignInParameter(userName, password, AuthenticationTypes.Password));
        }
    }
}
