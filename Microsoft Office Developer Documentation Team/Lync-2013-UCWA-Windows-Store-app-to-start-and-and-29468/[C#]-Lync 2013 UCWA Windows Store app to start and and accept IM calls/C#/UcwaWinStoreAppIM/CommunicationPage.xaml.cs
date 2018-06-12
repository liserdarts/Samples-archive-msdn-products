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
using Windows.UI.Popups;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WinStoreUcwaAppIM
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CommunicationsPage : Page
    {
        UcwaApp ucwaApp;   // app-specific

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


        public CommunicationsPage()
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
            sendButton.IsEnabled = false;
            this.ucwaApp = e.NavigationParameter as UcwaApp;
            if (this.ucwaApp != null && this.ucwaApp.Communication != null)
            {
                this.ucwaApp.OnEventNotificationsReceived += communication_ShowEvents;
                this.ucwaApp.Communication.OnResourceStateChanged += communication_OnResourceStateChanged;
                this.ucwaApp.Communication.OnMessageReceived += Communication_OnMessageReceived;
                this.ucwaApp.Communication.OnErrorReported += communication_OnErrorReported;
                this.ucwaApp.Communication.OnProgressReported += Communication_OnProgressReported;
                this.ucwaApp.Communication.OnMessagingInviteReceived += Communication_OnMessagingInviteReceived;
            }
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("status"))
                textBlockStatus.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["status"].ToString();
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("events"))
                textboxEvents.Text = Windows.Storage.ApplicationData.Current.LocalSettings.Values["events"].ToString();

        }
        List<string> handledMessagingInvites = new List<string>();
        public async Task HandleMessagingInvite(string subject, string importance, string fromUri, string firstMessage, string threadId)
        {
            if (handledMessagingInvites.Contains(threadId))
                return;

            handledMessagingInvites.Add(threadId);
            var msg = string.Format("Caller:\t{0}\r\nSubject:\t{1}\r\nImportance:\t{2}\r\nMessage:\t{3}\r\n", fromUri, subject, importance, firstMessage);
            var title = "You've got an incoming call!";
            // display a popup window to inform the user of the incoming invitation;
            var msgDialog = new Windows.UI.Popups.MessageDialog(msg, title);

            msgDialog.Commands.Add(new UICommand("Accept", async (command) =>
            {
                var result = await this.ucwaApp.Communication.AcceptInvite();
                if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent)
                {
                    sendButton.IsEnabled = true;
                    textBlockStatus.Text += "Incoming call accepted. ";
                }
                else
                {
                    textBlockStatus.Text += "Error while accepting call. ";
                }
            }));
            msgDialog.Commands.Add(new UICommand("Decline", async (command) =>
            {
                var result = await this.ucwaApp.Communication.DeclineInvite();
                if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent)
                {
                    sendButton.IsEnabled = false;
                    textBlockStatus.Text += "Incoming call declined. ";
                }
                else
                {
                    textBlockStatus.Text += "Error while declining call. " + result.Exception.Message;
                }

            }));
            msgDialog.DefaultCommandIndex = 1;
            await msgDialog.ShowAsync();

        }
        async void Communication_OnMessagingInviteReceived(string subject, string importance, string fromUri, string firstMessage, string threadId)
        {
            await HandleMessagingInvite(subject, importance, fromUri, firstMessage, threadId);
        }
        void Communication_OnMessageReceived(string status, string timestamp, string plainMsg, string participant)
        {
            textBlockImOutput.Text += timestamp + "\r\n\t" + participant + "\r\n\t\t" + plainMsg + "\r\n";
        }
        void Communication_OnProgressReported(string message, HttpStatusCode status = HttpStatusCode.OK)
        {
            textBlockStatus.Text += message + "\r\n";
        }
        void communication_ShowEvents(UcwaEventsData rawEvents)
        {
            textboxEvents.Text += rawEvents.OuterXml ;
            textboxEvents.Text += Environment.NewLine + Environment.NewLine;
        }
        void communication_OnErrorReported(Exception e)
        {
            textBlockStatus.Text += e.Message + "\r\n";
        }

        void communication_OnResourceStateChanged(string state, string resource)
        {
            textBlockStatus.Text += resource + " state changed to " + state +"\r\n";
            if (resource == "conversation")
            {
                if (state.ToLower() == "connected")
                    sendButton.IsEnabled = true;
                else if (state.ToLower() == "deleted")
                {
                    textBlockStatus.Text += "Conversation has ended.";
                    sendButton.IsEnabled = false;
                }
                else if (state.ToLower() == "disconnected")
                    textBlockStatus.Text += "Conversation is disconnected.";
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
            if (this.ucwaApp.Communication != null)
            {
                this.ucwaApp.Communication.OnMessageReceived -= Communication_OnMessageReceived;
                this.ucwaApp.Communication.OnResourceStateChanged -= communication_OnResourceStateChanged;
                this.ucwaApp.Communication.OnErrorReported -= communication_OnErrorReported;
            }
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["status"] = textBlockStatus.Text;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["events"] = textboxEvents.Text;
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

        private async void buttonImInvite_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ucwaApp.Communication.StartIM(textBoxCalleeUri.Text, "Test subject");

        } 

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            UcwaAppOperationResult result = null;
            string msg = textBoxImInput.Text.Trim();
            if (msg.StartsWith("<") && msg.EndsWith(">"))
            {
                result = await this.ucwaApp.Communication.SendMessage(msg, "text/html");
            }
            else
            result = await this.ucwaApp.Communication.SendMessage(msg, "text/plain");
            if (result == null)
                textBlockStatus.Text = "ERROR: SendMessage returned null result.";
            else if (result.Resource != null)
                textBlockStatus.Text += Environment.NewLine + result.Resource.OuterXml;
            else
                textBlockStatus.Text += Environment.NewLine + "SendMessage: " + result.StatusCode.ToString();

            // Show the sent message to self
            Communication_OnMessageReceived("OK", DateTime.Now.ToString(), msg, this.ucwaApp.Me.DisplayName);
        }
    }
}
