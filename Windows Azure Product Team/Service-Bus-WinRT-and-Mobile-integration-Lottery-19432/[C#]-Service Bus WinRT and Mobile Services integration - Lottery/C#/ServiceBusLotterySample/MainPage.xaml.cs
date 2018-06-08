using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.Messaging;

namespace ServiceBusLotterySample
{    
    public class Prizes
    {
        public int Id { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        private IMobileServiceTable<Prizes> prizesTable = App.MobileService.GetTable<Prizes>();

        private Queue prizesQ = null;

        public MainPage()
        {
            this.InitializeComponent();

            // this code connects to the Service Bus queue.
            prizesQ = new Queue("<queue name>", "<service bus connection string>");
            var uiDispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

            ReceivePrizes(uiDispatcher);
        }

        // the Receive Prizes method
        private async void ReceivePrizes(CoreDispatcher uiDispatcher)
        {
            while (true)
            {
                try
                {
                    String prize = await prizesQ.ReceiveAsync<String>();
                    uiDispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                          () => this.ListItems.Items.Add(new Prizes {Text = prize}));
                } catch (MessagingException)
                {
                    // we need to catch exception thrown when no message is retrieved.
                }
            }
        }

        private async void InsertTodoItem(Prizes todoItem)
        {
            // This code inserts a new Prize into the database. The Insert script on the Mobile Services back end will send a new message to the Service Bus queue.
            await prizesTable.InsertAsync(todoItem);
        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var todoItem = new Prizes { Text = TextInput.Text };
            InsertTodoItem(todoItem);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
