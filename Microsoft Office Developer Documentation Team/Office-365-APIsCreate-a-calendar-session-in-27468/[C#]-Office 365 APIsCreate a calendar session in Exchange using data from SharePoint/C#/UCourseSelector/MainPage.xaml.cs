using Windows.Data.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using UCourseSelector.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using UCourseSelector.Office365;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace UCourseSelector
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : UCourseSelector.Common.LayoutAwarePage
    {
        private string CalendarTitleString = "Your Calendar:";
        private Session DraggedSession = null;

        public MainPage()
        {
            this.InitializeComponent();

            DisplayName.DataContext = "";
            Mail.DataContext = "";

            CalendarTitle.DataContext = this.CalendarTitleString;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void DragSessionStart(object sender, DragItemsStartingEventArgs e)
        {
            DraggedSession = e.Items[0] as Session;
        }

        private async void DropSessionEnd(object sender, DragEventArgs e)
        {
            try
            {
                if (null != DraggedSession)
                {
                    CalendarEvents calEvents = await CalendarEvents.CreateCalendarInstance();
                    await calEvents.AddOrUpdateCalendarEvent(DraggedSession.ToCalendarEvent());

                    DisplayCalendarEventsForDay(System.Convert.ToDateTime(DraggedSession.Start));
                }
            }
            catch { }
        }

        private void refreshButtonClick(object sender, RoutedEventArgs e)
        {
            GetSessionList();
        }

        private async void removeCalButtonClick(object sender, RoutedEventArgs e)
        {
            if (EventsListView.SelectedItems.Count <= 0)
            {
                return;
            }

            CalendarEvent item = EventsListView.SelectedItem as CalendarEvent;

            CalendarEvents calEvents = await CalendarEvents.CreateCalendarInstance();
            await calEvents.DeleteCalendarEvent(item.Id);

            DisplayCalendarEventsForDay(System.Convert.ToDateTime(item.Start));
        }

        private async void addCalButtonClick(object sender, RoutedEventArgs e)
        {
            if (SessionListView.SelectedItems.Count <= 0)
            {
                return;
            }

            Session item = SessionListView.SelectedItem as Session;

            CalendarEvents calEvents = await CalendarEvents.CreateCalendarInstance();

            CalendarEvent calendarEvent = item.ToCalendarEvent();
            await calEvents.AddOrUpdateCalendarEvent(calendarEvent);

            DisplayCalendarEventsForDay(System.Convert.ToDateTime(item.Start));
        }

        private void sendMailButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ParentedPopup.IsOpen) { ParentedPopup.IsOpen = true; }
            ToBox.Text = SubjectBox.Text = BodyBox.Text = "";

            if (EventsListView.SelectedItems.Count > 0)
            {
                CalendarEvent calEvent = EventsListView.SelectedItem as CalendarEvent;
                SubjectBox.Text = string.Format("Attending {0}", calEvent.Subject);
                BodyBox.Text = string.Format("{0} - Session Start:{1} - Session End:{2}",
                    ((calEvent.Location == null) ? "n/a" : calEvent.Location.DisplayName),
                    calEvent.StartTime, 
                    calEvent.EndTime);
            }
        }

        private async void SendMailClicked(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.ToRecipients.Add(new Recipient("Test", ToBox.Text));
            message.Subject = SubjectBox.Text;
            message.Body = new Body(BodyBox.Text);

            await UCourseSelector.Mail.SendMail(message);

            Popup p = this.ParentedPopup as Popup;
            p.IsOpen = false;

        }

        private void SessionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = SessionListView.SelectedItem as Session;

            // Show CalendarView for the day of selected session
            if (null != item)
            {
                DisplayCalendarEventsForDay(System.Convert.ToDateTime(item.Start));
            }
        }

        private async void DisplayCalendarEventsForDay(DateTime day)
        {
            CalendarEvents calEvents = await CalendarEvents.CreateCalendarInstance();
            var calResult =
                 from t in (await calEvents.GetCalendarEvents(day))
                 group t by t.StartDateTime into g
                 orderby g.Key
                 select g;

            EventsInfoCollection.Source = calResult;

            CalendarTitleString = string.Format("Your day on: {0}", day.ToString("d"));
            CalendarTitle.DataContext = CalendarTitleString;
        }

        private async void GetSessionList()
        {
            SessionList sessionList = new SessionList();
            var sessionResult =
                from t in (await sessionList.GetSessions())
                group t by t.Start into g
                orderby g.Key
                select g;

            SessionCollection.Source = sessionResult;
        }

        private async void loginClick(object sender, RoutedEventArgs e)
        {
            await Office365Helper.ClearSession();
            UserProfile userProfile = await UserProfileInfo.GetUserProfileRequest();

            if (userProfile == null)
            {
                return;
            }

            DisplayName.DataContext = userProfile.DisplayName;
            Mail.DataContext = userProfile.EMail;

            CalendarTitle.DataContext = this.CalendarTitleString;

            loginButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            logoutButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            addCalButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            removeCalButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            refreshButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            sendMailButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

            SessionListView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            EventsListView.Visibility = Windows.UI.Xaml.Visibility.Visible;

            GetSessionList();
        }

        private async void logoutClick(object sender, RoutedEventArgs e)
        {
            DisplayName.DataContext = "";
            Mail.DataContext = "";

            CalendarTitle.DataContext = this.CalendarTitleString;

            loginButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            logoutButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            addCalButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            removeCalButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            refreshButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            sendMailButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            SessionListView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            EventsListView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            await Office365Helper.ClearSession();
        }

        private async void DisplayErrorWhenAcquireTokenFails(AuthenticationResult result)
        {
            MessageDialog dialog;

            switch (result.Error)
            {
                case "authentication_canceled":
                    // User cancelled, no need to display a message
                    break;
                case "temporarily_unavailable":
                case "server_error":
                    dialog = new MessageDialog("Please retry the operation. If the error continues, please contact your administrator.", "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
                default:
                    // An error occurred when acquiring a token, show the error description in a MessageDialog 
                    dialog = new MessageDialog(string.Format("If the error continues, please contact your administrator.\n\nError: {0}\n\nError Description:\n\n{1}", result.Error, result.ErrorDescription), "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
            }
        }
    }
}
