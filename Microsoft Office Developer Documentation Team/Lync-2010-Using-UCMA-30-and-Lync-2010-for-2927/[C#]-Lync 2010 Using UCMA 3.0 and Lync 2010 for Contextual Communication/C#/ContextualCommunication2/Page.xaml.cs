using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using Microsoft.Lync.Model.Conversation;

namespace ContextCommunication
{
    public partial class MainPage : UserControl
    {
        string _appId = "{3271E259-E508-4D39-B044-445855591E79}";
        Conversation _conversation;
        String _itemChosen = "No item chosen";
        // Boolean _isInitialContextData = true;

        public MainPage()
        {
            InitializeComponent();
            Initialize();
        }

        // Get the hosting Conversation object and application data, and register for event notification.
        private void Initialize()
        {
            String appData;
            try
            {
                _conversation = (Conversation)Microsoft.Lync.Model.LyncClient.GetHostingConversation();
            }
            catch (LyncClientException ex)
            {
                Logger("LyncClientException error: " + ex);
            }

            catch (Exception ex)
            {
                Logger("Other conversation initialization error: " + ex);
            }

            _conversation.ContextDataReceived += OnContextDataReceived;
            _conversation.InitialContextReceived += OnInitialContextReceived;
            _conversation.ContextDataSent += OnContextDataSent;
            
            appData = _conversation.GetApplicationData(_appId);
            Logger("Application data: " + appData);
            
            if (appData.Contains("open"))
            {
                // _isInitialContextData = false;
                channelStatus.Foreground = new SolidColorBrush(Colors.Green);
                channelStatus.Text = "Ready";
            }
        }

 
        // Display a string in the Logger textbox.
        private void Logger(string text)
        {
            LoggerTextBox.Text += text + "\n";
        }

        // Handler for InitialContextReceived event.
        void OnInitialContextReceived(object sender, InitialContextEventArgs args)
        {
           
           channelStatus.Foreground = new SolidColorBrush(Colors.Green);
           channelStatus.Text = "Ready";
           Logger("InitialContextReceived event raised. Data received: " + args.ApplicationData);
        }

        // Handler for the ContextDataReceived event on the Conversation object.
        // This handler displays the data sent to it from the UCMA application.
        public void OnContextDataReceived(object sender, ContextEventArgs args)
        {
            if ((args != null) && (args.ContextData.Length != 0))
            {
                Logger("OnContextDataReceived:" + args.ContextData);
                string str = args.ContextData;
                string[] substr = str.Split(new char[] { ','});

                // Populate the three data boxes.
                ModelBox.Text = substr[0];
                PriceBox.Text = substr[1];
                AvailabilityBox.Text = substr[2];
            }
            else
            {
                Logger("OnContextDataReceived called with no data.");
            }
        }

        // Handler for the ContextDataSent event on the Conversation object. 
        public void OnContextDataSent(object sender, ContextEventArgs args)
        {
            try
            {
                Logger("OnContextDataSent\n" + 
                    "DataType: " + args.ContextDataType +
                    " Data sent: " + args.ContextData);
            }
            catch (Exception ex)
            {
                Logger("OnContextDataSent error: " + ex);
            }
        }

        // Handler for the Click event on the SendData button.
        private void SendAdditionalData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger("Sending context: Query item: " + _itemChosen);

                _conversation.BeginSendContextData(_appId, @"plain/text", _itemChosen, SendAdditionalDataCallBack, null);
            }
            catch (Exception ex)
            {
                Logger("SendAdditionalData error: " + ex);
            }

        }

        // Callback for the BeginSendContextData method.
        private void SendAdditionalDataCallBack(IAsyncResult asyncResult)
        {
            if (asyncResult.IsCompleted)
            {
                _conversation.EndSendContextData(asyncResult);

                Logger("Additional context sent successfully.");
            }
            else
            {
                Logger("Could not send additional context: " + asyncResult.AsyncState);
            }
        }

        // Handlers for the Click event on the three radio buttons.
        // Each handler sets a global variable with a string that identifies the radio button that was selected.
        private void radioButton_Click1(object sender, RoutedEventArgs e)
        {
            _itemChosen = "Camera";
        }

        private void radioButton_Click2(object sender, RoutedEventArgs e)
        {
            _itemChosen = "Smartphone";
        }

        private void radioButton_Click3(object sender, RoutedEventArgs e)
        {
            _itemChosen = "GPS";
        }
    }

}
