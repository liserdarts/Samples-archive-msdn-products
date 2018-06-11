using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using Microsoft.Lync.Model.Conversation;

namespace FlightChooser
{
    public partial class MainPage : UserControl
    {
        string _appId = "{C17C216F-04A9-4234-94C1-A2EA5F0C4873}";
        Conversation _conversation;
        ConversationWindow _conversationWindow;
        Automation _automation = LyncClient.GetAutomation();
                
               
        public MainPage()
        {
            InitializeComponent();
            Initialize();
        }

        // Get the hosting Conversation object and application data, and register for event notification
        // and update the user interface to "Ready".
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
            _conversation.ContextDataSent += OnContextDataSent;
            _conversation.InitialContextReceived += OnInitialContextReceived;
            _conversationWindow = _automation.GetConversationWindow(_conversation);

            appData = _conversation.GetApplicationData(_appId);
            Logger("Application data: " + appData);
            if (appData.Contains("open"))
            {
                channelStatus.Foreground = new SolidColorBrush(Colors.Green);
                channelStatus.Text = "Ready";
            }

        }

        // Display a string in the Logger textbox.
        private void Logger(string text)
        {
            LoggerTextBox.Text += text + "\n";
        }

        // Handler for the InitialContextReceived event. 
        public void OnInitialContextReceived(object sender, InitialContextEventArgs args)
        {
            channelStatus.Foreground = new SolidColorBrush(Colors.Green);
            channelStatus.Text = "Ready";
            
            Logger("InitialContextReceived event raised. \nData received: " + args.ApplicationData);
        }

        // Handler for the ContextDataReceived event on the Conversation object.
        // This handler splits the args.ContextData string into three substrings.
        // Semicolons divide the three substrings.
        public void OnContextDataReceived(object sender, ContextEventArgs args)
        {
            if ( (args != null) && (args.ContextData.Length != 0) )
            {
               // Split the ContextData string at the semicolons.
               string str = args.ContextData;
               string[] substr = str.Split(new char[] { ';' });

               // Populate the three data boxes.
               fltOrigination.Text = substr[0];
               fltDestination.Text = substr[1];
               fltCost.Text = substr[2];

               Logger("OnContextDataReceived: " + args.ContextData + "\nConversation ID: " + ((Conversation)sender).Properties[ConversationProperty.Id].ToString());
             }
        }

        // Handler for the ContextDataSent event on the Conversation object. 
        public void OnContextDataSent(object sender, ContextEventArgs args)
        {
            try
            {
                Logger("OnContextDataSent\n: DataType: " + args.ContextDataType +
                    " Data: " + args.ContextData);
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
                Logger("Sending additional context.");

                _conversation.BeginSendContextData(_appId, @"plain/text", "exit", SendAdditionalDataCallBack, null);
                channelStatus.Foreground = new SolidColorBrush(Colors.Red);
                channelStatus.Text = "Closed";
                fltOrigination.Text = "";
                fltDestination.Text = "";
                fltCost.Text = "";
            }

            catch (InvalidOperationException ex)
            {
                Logger("Invalid operation in SendAdditionalData handler: " + ex);
            }

            catch (Exception ex)
            {
                Logger("Other SendAdditionalData error: " + ex);
            }

        }

        // Callback for the BeginSendContextData method.
        private void SendAdditionalDataCallBack(IAsyncResult asyncResult)
        {
            try
            {
                if (asyncResult.IsCompleted)
                {
                    _conversation.EndSendContextData(asyncResult);

                    Logger("Additional context sent successfully.");
                    // Close the Extensibility window.
                    _conversationWindow.CloseExtensibilityWindow(_appId);
                }
                else
                {
                    Logger("Could not send additional context: " + asyncResult.AsyncState);
                }
            }
            catch (Exception ex)
            {
                Logger("SendAdditionalDataCallBack error: " + ex);
            }
        }

       
    }

}
