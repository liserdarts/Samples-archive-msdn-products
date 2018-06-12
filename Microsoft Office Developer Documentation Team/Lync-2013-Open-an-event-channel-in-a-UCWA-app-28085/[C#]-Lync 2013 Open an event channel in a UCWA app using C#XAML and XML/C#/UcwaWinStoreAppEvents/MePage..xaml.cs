using WinStoreUcwaAppEvents.Common;
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

using Windows.UI.Core;
using System.Net;
using Windows.System.Threading;
using Windows.ApplicationModel.Core;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WinStoreUcwaAppEvents
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MePage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        UcwaApp UcwaApp;
        //UcwaAppEventChannel UcwaAppEventChannel;
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

        public MePage()
        {

            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            textBoxEvents.Text = "";
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
        private async /*modified*/ void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.UcwaApp = e.NavigationParameter as UcwaApp;
            if (this.UcwaApp == null || this.UcwaApp.IsSignedIn == false)
            {
                ReportError(new Exception("The app is not signed in yet."));
                return;
            }

            this.UcwaApp.Me.OnEventNotificationsReceived += this.ProcessEventNotifications;
            this.UcwaApp.Me.OnErrorReported += this.ReportError;
            this.UcwaApp.Me.OnProgressReported += ReportProgress;

            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("status"))
                textBlockDebug.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["status"].ToString();
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("events"))
                textBoxEvents.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["events"].ToString();

            // Show some local user info.
            textBlockMyName.Text = this.UcwaApp.Me.DisplayName +
                ", " + this.UcwaApp.Me.Title + ", " + this.UcwaApp.Me.Department + ", " + this.UcwaApp.Me.Uri;

            textBoxNote.Text = await this.UcwaApp.Me.GetNoteMessage();
            textBoxPresence.Text = await this.UcwaApp.Me.GetPresenceAvailability();
            var phones = await this.UcwaApp.Me.GetPhoneLines();
            foreach (var phone in phones)
                textBoxPhones.Text += (string.IsNullOrEmpty(textBoxPhones.Text) ? "" : ", ") + phone.Type + ":" + phone.Number;
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
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["status"] = textBlockDebug.Text;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["events"] = textBoxEvents.Text;
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

        void ProcessEventNotifications(UcwaEventsData events)
        {
            ShowEventNotifications(events);
            var meEvents = events.GetEventsBySender("me");
            if (meEvents != null)
                ProcessMeEvents(meEvents);
        }
        void ShowEventNotifications(UcwaEventsData events)
        {
            textBoxEvents.Text = DateTime.Now.ToString() + "\r\n" + events.OuterXml + "\r\n";
        }
        async void ProcessMeEvents(IEnumerable<UcwaEvent> events)
        {
            foreach (var e in events)
                {
                    var eType = e.Type.ToLower();
                    switch (e.Name)
                    {
                        case "me":
                            await this.UcwaApp.Me.Refresh(e.Uri);
                            this.textBlockMyName.Text = this.UcwaApp.Me.DisplayName + ", " + this.UcwaApp.Me.Title;
                            break;
                        case "note":
                            //await this.UcwaApp.Me.GetNote(e.Uri);
                            if (eType == "updated" || eType == "added")
                                textBoxNote.Text = await this.UcwaApp.Me.GetNoteMessage(e.Uri);
                            break;
                        case "presence":
                            //await this.UcwaApp.Me.GetPresence(e.Uri);
                            if (eType == "updated" || eType == "added")
                                textBoxPresence.Text = await this.UcwaApp.Me.GetPresenceAvailability(e.Uri);
                            break;
                        case "photo":
                            //var phStream = this.UcwaApp.Me.Photo;
                            //Windows.UI.Xaml.Media.Imaging.BitmapImage bitmap = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                            //Windows.Storage.StorageFile sf =  ; .Streams.RandomAccessStream ras = ;
                            //bitmap.SetSource(phStream);
                            //imagePhoto.Source = phStream; 
                            //await this.UcwaApp.Me.GetPhoto(e.Uri);
                            //var p = this.UcwaApp.Me.Photo;
                            break;
                        case "location":                            
                            textBoxLocation.Text = await this.UcwaApp.Me.GetLocationCoordinates(e.Uri);
                            break;
                        case "phones":                            
                            var phones = await this.UcwaApp.Me.GetPhoneLines(e.Uri);
                            foreach (var phone in phones)
                                textBoxPhones.Text += (string.IsNullOrEmpty(textBoxPhones.Text) ? "" : ", ") + phone.Type + ":"+phone.Number;

                            break;
                    }
                }

        }
        void ReportProgress(string msg, HttpStatusCode status=HttpStatusCode.OK)
        {
            textBlockDebug.Text += DateTime.Now + ": [" + status.ToString() + "] " + msg + Environment.NewLine;

        }
        void ReportError(Exception e)
        {
            textBlockDebug.Text += DateTime.Now + ": " + e.Message + "\r\n" + e.StackTrace + "\r\n";
        }

        private async void PublishNote(object sender, RoutedEventArgs e)
        {
            
            var statusCode = await this.UcwaApp.Me.SetNote(textBoxNote.Text);
            ReportProgress("PublishNote executed", statusCode);
        }

        private async void PublishPresence(object sender, RoutedEventArgs e)
        {
            var statusCode = await this.UcwaApp.Me.SetPresence(textBoxPresence.Text);
            ReportProgress("Set Presence executed.", statusCode);
        }

        private void StartIM(object sender, RoutedEventArgs e)
        {

        }

    }
}
