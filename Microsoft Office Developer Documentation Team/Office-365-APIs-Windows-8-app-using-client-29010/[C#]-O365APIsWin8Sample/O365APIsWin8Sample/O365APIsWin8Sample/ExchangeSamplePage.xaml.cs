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
using Microsoft.Office365.Exchange;
using Microsoft.Office365.OAuth;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Globalization;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace O365APIsWin8Sample
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ExchangeSamplePage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private ExchangeClient client;


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
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private async Task EnsureClientCreated()
        {
            var authenticator = new Authenticator();
            var result = await authenticator.AuthenticateAsync("https://outlook.office365.com/");

            // Create a client proxy:
            this.client = new ExchangeClient(new Uri("https://outlook.office365.com/ews/odata"), result.GetAccessToken);
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

        private async void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Subtitle"] = "Upcoming events";
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                IOrderedEnumerable<IEvent> events = await GetCalendarEvents();

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
                await ShowErrorMessageAsync(exception.Message);
            }
        }

        private async Task<IOrderedEnumerable<IEvent>> GetCalendarEvents()
        {
            await EnsureClientCreated();
            // Obtain calendar event data
            var eventsResults = await (from i in client.Me.Events
                                       where i.End >= DateTimeOffset.UtcNow
                                       select i).Take(10).ExecuteAsync();

            var events = eventsResults.CurrentPage.OrderBy(e => e.Start);
            return events;
        }

        private async void MailButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Subtitle"] = "Recent emails";
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                IEnumerable<IMessage> messages = await GetMessages();

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
                await ShowErrorMessageAsync(exception.Message);
            }
        }

        private async Task<IEnumerable<IMessage>> GetMessages()
        {
            await EnsureClientCreated();

            // Obtain data:
            var messageRequest = await (from i in this.client.Me.Inbox.Messages
                                        orderby i.DateTimeSent descending
                                        select i).Take(10).ExecuteAsync();
            var messages = messageRequest.CurrentPage;
            return messages;
        }

        private async void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            Exception exception = null;

            try
            {
                this.DefaultViewModel["Subtitle"] = "Contacts";
                this.DefaultViewModel["Items"] = new[] { new { Primary = "Loading...", Secondary = "Please wait..." } };

                IEnumerable<IContact> contacts = await GetContacts();

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
                await ShowErrorMessageAsync(exception.Message);
            }
        }

        private async Task<IEnumerable<IContact>> GetContacts()
        {
            await EnsureClientCreated();

            // Obtain data:
            var contactsRequest = await (from i in this.client.Me.Contacts
                                         orderby i.DisplayName
                                         select i).ExecuteAsync();
            var contacts = contactsRequest.CurrentPage;
            return contacts;
        }

    private static string ToLocalTimeString(DateTimeOffset? dateTime)
        {
            return (dateTime.HasValue) ? ToLocalTimeString(dateTime.Value) : String.Empty;
        }

    private static string ToLocalTimeString(DateTimeOffset dateTime)
    {
        return dateTime.ToLocalTime().ToString("g", CultureInfo.CurrentCulture);
    }
    private async void SignoutButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
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
