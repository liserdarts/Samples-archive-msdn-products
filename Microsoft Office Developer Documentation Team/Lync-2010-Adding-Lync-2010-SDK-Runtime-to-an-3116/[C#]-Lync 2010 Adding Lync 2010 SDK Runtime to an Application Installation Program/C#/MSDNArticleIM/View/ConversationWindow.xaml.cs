using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using System.Net;

namespace MSDNArticleIM
{


    /// <summary>
    /// Interaction logic for ConversationWindow.xaml
    /// </summary>
    public partial class ConversationWindow : Window
    {
        /// <summary>
        /// Enumerates the actions that can be completed by the TakeFormAction method.
        /// </summary>
        enum FormActions
        {
            CloseForm,            //Closes a form
            SetFormBackColor,     //Set the background color of a form. 
            DisplayDefaultCursor, //set the default cursor assigned to the form.
            DisplayWaitCursor,    //Sets the form cursor to an hour glass.
            UpdateLabel,          //Updates a Label.text property.
            UpdateWindowTitle,    //Updates the window text property
            ClearText,            //Clears the text property of a given control.
            SetListContents,      //Set the contents of a listbox
            StartTimer,           //Starts display composing timer
            StopTimer,            //Stops display composing timer
            DisplayTypingStatus,  //Display the typing status of remote participants
            EnableButton,         //Enables a button.
            DisableButton         //Disables a button.
        }

        //These delegates are used call methods on the UI thread.
        private delegate void FormActionDelegate(FormActions actionToTake, object actionObject, object actionData);

        #region private class fields
        private FormActionDelegate FormActor;
        private ClientModel _ClientModel = null;
        private Conversation _Conversation;
        private InstantMessageModality _RemoteIMModality;
        private InstantMessageModality _LocalIMModality;
        private string _targetUri;
        private Contact _remoteContact;
        private ArrayList _ConversationHistory;
        private Boolean _FormatMessageAsText = true;
        private CredentialForm _CredentialForm = null;
        private System.Windows.Window _signInForm = null;
        private StringBuilder _ConversationHistoryHtml;
        private DispatcherTimer _DispatcherTimer_DisplayComposing = new DispatcherTimer();
        #endregion

        #region FormHandlers
        /// <summary>
        /// Handles the event raised when a user clicks the close button on the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            //Close the Window
            this.Close();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the event raised when user moves keyboard focus off of SIP address field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SIPEntry_TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
            //***********************************************************
            //Cast event originator to Textbox class
            //***********************************************************
            TextBox entry_TextBox = (TextBox)sender;

            if (entry_TextBox.Text.Contains("@") || entry_TextBox.Text.Contains("conf:"))
            {

                //***********************************************************
                //Set value of _targetUri class string field to SIP Uri of
                //user to invite to conversation. This class field is referenced
                //by several methods in this class 
                //***********************************************************
                _targetUri = entry_TextBox.Text;

                RemoteSIP_Label.Content = _targetUri;

                //***********************************************************
                //Call helper method that gets a Contact instance from the supplied
                //target Uri and adds a new conversation.
                //***********************************************************
                StartConversation();
            }

        }

        private void SIPEntry_TextBox_TouchLeave(object sender, TouchEventArgs e)
        {

        }

        /// <summary>
        /// Called when window is initialized. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, EventArgs e)
        {
            InitializeComponent();
            _ConversationHistory = null;
            try
            {



                //***********************************************************
                //Initialize the delegate that is responsible for all API event driven
                //updates to window controls.
                //***********************************************************
                FormActor = new FormActionDelegate(TakeFormAction);


                //***********************************************************
                //Initialize the helper class that wraps method calls and 
                //events on the Microsoft.Lync.Model.LyncClient class
                //***********************************************************
                _ClientModel = new ClientModel();

                //******************************************************************************
                //registers for event raised by client model when LyncClient has been initialized. 
                //******************************************************************************
                _ClientModel.ClientInitializedEvent += new ClientInitializedDelegate(_ClientModel_ClientInitializedEvent);


                //******************************************************************************
                //Enables the window sign in button if Lync is not already signed in. 
                //******************************************************************************
                if (_ClientModel._LyncClient.State != ClientState.SignedIn)
                {
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.EnableButton, SignIn_Button, null });
                }

                //******************************************************************************
                //Sets the window cursor to an hourglass until Lync sign in process completes. 
                //Cursor is set to Arrow in the _ClientModel_ClientInitializedEvent method that is
                //called on the Lync platform thread when client is initialized.
                //******************************************************************************
                this.Cursor = Cursors.Wait;
                _ClientModel.InitializeClient();



                //******************************************************************************
                //Sets the initial state of the window title, color, and status label. 
                //******************************************************************************
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "No Conversation" });
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.AntiqueWhite });
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, lblFindStatus, _ClientModel._LyncClient.State.ToString() });

                //******************************************************************************
                //Register for new conversations added to the conversation manager conversations collection.
                //Note that this event is raised whether a new conversation is started by the local user or
                //because the local user was invited to a conversation started by another user.
                //******************************************************************************
                _ClientModel._LyncClient.ConversationManager.ConversationAdded += ConversationsManager_ConversationAdded;

                //******************************************************************************
                //If you have not registered for client state change events elsewhere in your code, you 
                //should register here. If Lync goes off-line in the middle of a conversation, you need 
                //to both inform the user and programmatically react to the state change. 
                //When Lync is not signed in, any Lync API call you make (except BeginSignIn) returns the
                //NotSignedIn exception.
                //******************************************************************************
                _ClientModel._LyncClient.StateChanged += _LyncClient_StateChanged;
                _ClientModel._LyncClient.ClientDisconnected += _LyncClient_ClientDisconnected;
                //Update conversation form label with the SIP Uri of the user to be invited to conversation.
                Send_Button.Focusable = false;


                //******************************************************************************
                //Initialize the arraylist that is a: keeper of the IM text histor of the conversation and 
                //b: the data source of the conversation listbox in the Conversation form.
                //******************************************************************************
                _ConversationHistory = new ArrayList();

                //******************************************************************************
                //If the conversation has not been created.
                //******************************************************************************
                if (_Conversation == null)
                {
                    //******************************************************************************
                    //Call helper method that creates a new conversation.
                    //******************************************************************************
                    Message_TextBox.Text = string.Empty;
                    _ConversationHistoryHtml = new StringBuilder();
                }

                //******************************************************************************
                //Register for the tick event on the dispacher timer that regulates the sending
                //of the user typing status message.
                //Sets the interval between ticks at 5 seconds.
                //******************************************************************************
                _DispatcherTimer_DisplayComposing.Tick += new EventHandler(_DispatcherTimer_DisplayComposing_Tick);
                _DispatcherTimer_DisplayComposing.Interval = new TimeSpan(0, 0, 5);


                _ClientModel.ClientStateChangedEvent += new ClientStateChanged(_ClientModel_ClientStateChangedEvent);
                _ClientModel.ClientShutdownEvent += new ClientShutdown(_ClientModel_ClientShutdownEvent);
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message, "Host not found");
            }


        }

        void _DispatcherTimer_DisplayComposing_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(FormActor, new object[] {FormActions.UpdateLabel,ActivityText_Label, "" });
            _DispatcherTimer_DisplayComposing.Stop();
        }



        /// <summary>
        /// Handles the event raised when a user clicks the Send Message button by sending an IM to all
        /// remote conversation participants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            //**********************************************
            //If no conversation has been created then return
            //**********************************************
            if (_Conversation == null)
            {
                return;
            }

            //***********************************************************
            //If user has not entered any text in the message box then return
            //***********************************************************
            if (Message_TextBox.Text.Length == 0)
            {
                return;
            }
            try
            {
                SpellCheckStatus_String.Content = "";
                string messageString = Message_TextBox.Text;
                //*************************************************************
                //Wrap user's message text in html tags. I created a generic Html page using
                //Expression Web and then captured the resulting Html. Any valid Html tags you add
                //to an html page can be used here. 
                //*************************************************************
                string FormattedMessage = "<DIV style=\"font-size:" 
                    + Message_TextBox.FontSize 
                    + "pt;font-family:" 
                    + Message_TextBox.FontFamily.ToString()
                    + ";color: #463939;direction: ltr\">" 
                    + messageString                              //Actual message text
                    + "</DIV>";

                //*************************************************************
                //Add html content type key and html string to text message dictionary.
                //*************************************************************
                IDictionary<InstantMessageContentType, string> messageDictionary = new Dictionary<InstantMessageContentType, string>();
                messageDictionary.Add(InstantMessageContentType.Html, FormattedMessage);

                //*************************************************************
                //Send the IM message on the Conversation IMModality. You must call CanInvoke before sending the message. A remote user can
                //go off-line or into do-not-disturb status at any time during a conversation. If you send a message in that case, an exception
                //is raised by EndSendMessage.
                //
                //Note: If network connectivity is lost on a computer, conversations remain active
                //      but you cannot call BeginSendInstantMessage. When connectivity is restored,
                //      you can send messages on the active conversation again.
                //*************************************************************
                if (
                    ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).CanInvoke(ModalityAction.SendInstantMessage)
                )
                {

                    //*************************************************************
                    //SEND THE TEXT MESSAGE using the message dictionary created earlier
                    //*************************************************************
                    ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).BeginSendMessage(
                        messageDictionary
                        , SendMessageCallback
                        , ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]));
                }
                else
                {
                    MessageBox.Show("Cannot send IM now " +
                        ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).State.ToString(),
                        "Message Was Not Sent");
                }

            }
            catch (LyncClientException ex)
            {
                MessageBox.Show("Lync Exception: " + ex.Message, "Message Was Not Sent");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Contact cannot accept instant messages");
            }

        }

        /// <summary>
        /// Async callback method invoked by InstantMessageModality instance when SendMessage completes
        /// </summary>
        /// <param name="_asyncOperation">IAsyncResult The operation result</param>
        /// 
        private void SendMessageCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {
                try
                {
                    ((InstantMessageModality)ar.AsyncState).EndSendMessage(ar);

                    //*****************************************************
                    //Clear the message input textbox.
                    //*****************************************************
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.ClearText, Message_TextBox, null });

                }
                catch (LyncClientException ex)
                {
                    MessageBox.Show("Contact cannot receive instant messages. Lync Client Exception: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the event raised when a user presses the "End Conversation" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndConversation_Button_Click(object sender, RoutedEventArgs e)
        {
            //**********************************************
            //Inexpensive defensive coding. Conversation should not 
            //ever be null here.
            //**********************************************
            if (_Conversation != null)
            {

                //**********************************************
                //End() cannot be called on a terminated conversation. 
                //**********************************************
                if (_Conversation.State != ConversationState.Terminated)
                {
                    //**********************************************
                    //This terminates the local instance of the conversation.
                    //A remote user is responsible for terminating the local
                    //instance at the remote endpoint.
                    //**********************************************
                    _Conversation.End();
                }
            }
        }

        /// <summary>
        /// Handles the event raised when a user clicks the Sign In button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {//
            //**************************************
            //This private helper method signs a user into Lync
            //**************************************
            SignUserIn();      
        }

        /// <summary>
        /// Raised when user types a character in the IM entry text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Message_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //**********************************************************
            //If conversation exists and is active then...
            //**********************************************************
            if (_Conversation != null && _Conversation.State == ConversationState.Active)
            {

                //**********************************************************
                //If conversation allows typing status to be set
                //**********************************************************
                if (_Conversation.Modalities[ModalityTypes.InstantMessage].CanInvoke(ModalityAction.SetIsTyping))
                {
                    try
                    {
                        //**********************************************************
                        //Set typing status
                        //**********************************************************
                        ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).BeginSetComposing(true, ComposingCallback, null);
                    }
                    catch (OperationException oe)
                    {
                        MessageBox.Show("Cannot set composing status. Operation Exception " + oe.Message);
                    }
                    catch (NotSignedInException) { };
                }
            }

        }

        /// <summary>
        /// Handles the event raised when spell check button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpellCheck_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            SpellCheckStatus_String.Content = "Checking...";
            HttpWebRequest request = BingSpellChecker.BuildSpellRequest(Message_TextBox.Text);

            try
            {
                //***************************************
                // Send the request; display the response.
                //***************************************
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = BingSpellChecker.GetSpellResponse(response);
                if (responseString.Length > 0)
                {
                    Message_TextBox.Text = responseString;
                    SpellCheckStatus_String.Content = "Bing spelling check is complete";
                }
                else
                {
                    SpellCheckStatus_String.Content = "Bing spelling check is complete. No suggestions found";
                }
            }
            catch (WebException ex)
            {
                //***************************************
                // An exception occurred while accessing the network.
                //***************************************
                Console.WriteLine(ex.Message);
            }
            this.Cursor = Cursors.Arrow;

        }

        /// <summary>
        /// Handles the event raised when the Conversation Window is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            //********************************************************
            //If a conversation was active and a remote contact instance
            //was instantiated then events on that remote contact are unregistered
            //********************************************************
            if (_remoteContact != null)
            {
                _remoteContact.ContactInformationChanged -= _remoteContact_ContactInformationChanged;
            }

            //********************************************************
            //If local user InstantMessageModality instance exists, un-register for events on this modality.
            //********************************************************
            if ((InstantMessageModality)(_Conversation.Modalities[ModalityTypes.InstantMessage]) != null)
            {
                ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived -= myInstantMessageModality_MessageReceived;
                ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).IsTypingChanged -= myInstantMessageModality_ComposingChanged;
            }

            //********************************************************
            //If remote user modality instance exists, un-register for events on the modality.
            //********************************************************
            if (_RemoteIMModality != null)
            {
                _RemoteIMModality.InstantMessageReceived -= myInstantMessageModality_MessageReceived;
                _RemoteIMModality.IsTypingChanged -= myInstantMessageModality_ComposingChanged;
            }

            _ClientModel._LyncClient.StateChanged -= _LyncClient_StateChanged;

            _ClientModel._LyncClient.ConversationManager.ConversationAdded -= ConversationsManager_ConversationAdded;

            //********************************************************
            //un-register for events on the conversation
            //********************************************************
            _Conversation.ParticipantAdded -= Conversation_ParticipantAdded;
            _Conversation.StateChanged -= Conversation_StateChangedEvent;

            //********************************************************
            //This helper method signs out of Lync and shuts Lync down
            //IF Lync is running in UI Suppressed mode. 
            //********************************************************
            _ClientModel.Dispose();
        }

        private void SIPEntry_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        #region private helper methods
        /// <summary>
        /// Creates a new conversation.
        /// </summary>
        /// <param name="remoteSIP">string. User SIP address or conference Url.</param>
        private void StartConversation()
        {
            //***********************************************************
            //_targetUri is a class string field that is set to the SIP Uri 
            //of a remote user when user completes entry of the SIPEntry_Textbox control 
            //on this window.
            //***********************************************************
            if (_targetUri == null)
            {
                return;
            }

            //***********************************************************
            //User may have passed a conference Url to the conversation window. If
            //this is the case, the new conversation must be a conference and the
            //local user must join to participate.
            //***********************************************************
            if (_targetUri.ToUpper().Contains("conf:"))
            {
                try
                {
                    _Conversation = _ClientModel._LyncClient.ConversationManager.JoinConference(_targetUri);
                }
                catch (LyncClientException) { }
            }

            //***********************************************************
            //The new conversation is not a conference. In this case, a new conversation is created
            //and this sample program is responsible for adding contacts.
            //***********************************************************
            else if (_targetUri.ToUpper().Contains("@"))
            {
                //***********************************************************
                //Create a new instance of Conversation.
                //***********************************************************
                _Conversation = _ClientModel._LyncClient.ConversationManager.AddConversation();
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, ActivityText_Label, "Conversation added" });
            }
            else
            {
                MessageBox.Show("You have typed an invalid address. No conversation was started");
            }

        }

        /// Adds a participant to a conversation
        /// </summary>
        /// <param name="pConversation">Conversation Conversation to add contact to</param>
        /// <param name="pGroupName">string Name of group to get Contact from.</param>
        /// <param name="pContactUri">string Contact Uri.  Example: SIP:davidp@contoso.com</param>
        private Boolean AddContactToConversation(Conversation pConversation, Contact remoteContact)
        {
            Boolean returnValue = false;


            if (0 == ((ContactCapabilities)remoteContact.GetContactInformation(ContactInformationType.Capabilities) & ContactCapabilities.RenderInstantMessage))
            {
                //***********************************************************
                //Contact is not available for conversation.
                //***********************************************************
                return false;
            }


            //***********************************************************
            //If the contact is not available to join the conversation now, return a boolean false to calling code.
            //***********************************************************
            if (((ContactAvailability)remoteContact.GetContactInformation(ContactInformationType.Availability)) == ContactAvailability.DoNotDisturb
                || ((ContactAvailability)remoteContact.GetContactInformation(ContactInformationType.Availability)) == ContactAvailability.Offline)
            {
                return false;
            }
            try
            {
                //***********************************************************
                //Verify that a contact can be added to the conversation.
                //***********************************************************
                if (pConversation.CanInvoke(ConversationAction.AddParticipant))
                {

                    //***********************************************************
                    //Add the contact to the conversation
                    //***********************************************************
                    pConversation.AddParticipant(remoteContact);

                    //***********************************************************
                    //Update conversation window status label content with result of operation
                    //***********************************************************
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, ActivityText_Label, "Participant added: " + remoteContact.Uri });
                    returnValue = true;
                }
            }
            catch (ItemAlreadyExistException) { }
            return returnValue;
        }


        /// <summary>
        /// Sends an IM text message.
        /// </summary>
        /// <param name="conversation">Conversation. The conversation this message is sent to.</param>
        /// <param name="messageString">string. The text of the message to send.</param>
        internal void SendMessage()
        {
            if (_Conversation == null)
            {
                return;
            }
            if (Message_TextBox.Text.Length == 0)
            {
                return;
            }
            try
            {
                string messageString = Message_TextBox.Text;
                //*************************************************************
                //Wrap user's message text in html tags. I created a generic Html page using
                //Expresion Web and then captured the resulting Html. Any valid Html tags you add
                //to an html page can be used here. 
                //*************************************************************
                string FormattedMessage = "<DIV style=\"font-size:" +
                        "10pt;font-family:" +
                        "MS Shell Dlg 2;color: #463939;direction: ltr\">" +
                        messageString +
                        "</DIV>";

                //*************************************************************
                //Add html content type key and html string to text message dictionary.
                //*************************************************************
                IDictionary<InstantMessageContentType, string> messageDictionary = new Dictionary<InstantMessageContentType, string>();
                messageDictionary.Add(InstantMessageContentType.Html, FormattedMessage);

                //*************************************************************
                //Send the IM message on the Conversation IMModality. You must call CanInvoke before sending the message. A remote user can
                //go off-line or into do-not-disturb status at any time during a conversation. If you send a message in that case, an exception
                //is raised by EndSendMessage.
                //
                //Note: If network connectivity is lost on a computer, conversations remain active
                //      but you cannot call BeginSendInstantMessage. When connectivity is restored,
                //      you can send messages on the active conversation again.
                //*************************************************************
                if (
                    ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).CanInvoke(ModalityAction.SendInstantMessage)
                )
                {
                    //*************************************************************
                    //SEND THE TEXT MESSAGE using the message dictionary created earlier
                    //*************************************************************
                    ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).BeginSendMessage(
                        messageDictionary
                        , SendMessageCallback
                        , ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]));
                }
                else
                {
                    MessageBox.Show("Cannot send IM now " +
                        ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).State.ToString(),
                        "Message Was Not Sent");
                }

            }
            catch (LyncClientException e)
            {
                MessageBox.Show("Lync Exception: " + e.Message, "Message Was Not Sent");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Contact cannot accept instant messages");
            }
        }

        /// <summary>
        /// Updates a control property on the UI thread.
        /// </summary>
        /// <param name="actionToTake">FormActions. Enumerates the action to take on the form UI control.</param>
        /// <param name="actionObject">object. The form UI control whose property is to be updated.</param>
        /// <param name="actionData">object. The new property value of the control to be updated.</param>
        private void TakeFormAction(FormActions actionToTake, object actionObject, object actionData)
        {
            try
            {
                System.Windows.Window thisForm;
                switch (actionToTake)
                {
                    case FormActions.CloseForm: //Close conversation window
                        thisForm = (System.Windows.Window)actionObject;
                        thisForm.Close();
                        break;
                    case FormActions.DisplayDefaultCursor: //Display the default cursor
                        this.Cursor = Cursors.Arrow;
                        break;
                    case FormActions.DisplayWaitCursor:    //Display the wait cursor
                        this.Cursor = Cursors.Wait;
                        break;
                    case FormActions.EnableButton:         //Enable the specified button
                        ((Button)actionObject).IsEnabled = true;
                        //((Button)actionObject).FocusVisualStyle = FocusVisualStyle.
                        break;
                    case FormActions.DisableButton:       //Disable the specified button
                        ((Button)actionObject).IsEnabled = false;
                        break;
                    case FormActions.UpdateLabel:        //Update the text property of the specified label
                        Label labelToUpdate = (Label)actionObject;
                        labelToUpdate.Content = (string)actionData;
                        break;
                    case FormActions.UpdateWindowTitle:
                        thisForm = (System.Windows.Window)actionObject;
                        thisForm.Title = (string)actionData;
                        break;
                    case FormActions.ClearText:          //Clear the text property of the specified text box
                        System.Windows.Controls.TextBox textBoxToUpdate = (System.Windows.Controls.TextBox)actionObject;
                        textBoxToUpdate.Text = string.Empty;
                        break;
                    case FormActions.StartTimer:
                        _DispatcherTimer_DisplayComposing.Start();
                        break;
                    case FormActions.StopTimer:
                        _DispatcherTimer_DisplayComposing.Stop();
                        break;
                    case FormActions.DisplayTypingStatus:
                        ActivityText_Label.Content = "";
                        _DispatcherTimer_DisplayComposing.Start();
                        break;
                    case FormActions.SetListContents:   //clear and update the contents of the conversation history WebBrowser

                        //***************************************************
                        //cast action data to array of object.
                        //***************************************************
                        object[] actionDataArray = (object[])actionData;

                        //***************************************************
                        //Get the arraylist of conversation history out of the action data array
                        //***************************************************
                        ArrayList historyArray = (ArrayList)actionDataArray[1];

                        //***************************************************
                        //Get the name of the message sender out of first element of action data 
                        //***************************************************
                        string sender = (string)actionDataArray[0];


                        //***************************************************
                        //Get the newest message text out of the conversation history array 
                        //***************************************************
                        string receivedMessage = historyArray[historyArray.Count - 1].ToString();

                        //***************************************************
                        //Cast the object of action to the control type to be updated 
                        //***************************************************
                        WebBrowser webBrowserToUpdate = (WebBrowser)actionObject;

                        //***************************************************
                        //Concatenate the previous history to the new message 
                        //***************************************************

                        _ConversationHistoryHtml.Insert(0, sender + receivedMessage);
                        webBrowserToUpdate.NavigateToString(_ConversationHistoryHtml.ToString());

                        //***************************************************
                        //Update the history array to replace the newest message with
                        //the message sender name + the newest message text.
                        //***************************************************
                        string recMessage = sender + ": " + receivedMessage;
                        historyArray.RemoveAt(historyArray.Count - 1);
                        historyArray.Add(recMessage);

                        break;
                    case FormActions.SetFormBackColor:   //Set the background color of the form.
                        System.Windows.Window formToUpdate = (System.Windows.Window)actionObject;
                        formToUpdate.Background = (Brush)actionData;
                        break;

                }
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Invalid cast exception: " + ex.Message, "ConversationForm.TakeFormAction");
            }
        }

        /// <summary>
        /// Determines the source network type of a contact and returns true if contact can 
        /// receive formatted IM text in addition to plain text. 
        /// </summary>
        /// <param name="contactInfo">IDictionary{ContactInformationType, object}. A dictionary of contact information published by remote contact.</param>
        /// <returns></returns>
        private static bool canReceiveFormattedIM(IDictionary<ContactInformationType, object> contactInfo)
        {
            //***********************************************************
            //Formatted IM is not supported in federation with public networks 
            //like Live Messenger, Yahoo! and AOL Instant Messenger
            //***********************************************************
            if ((SourceNetworkType)contactInfo[ContactInformationType.SourceNetwork] == SourceNetworkType.FederatedPublic)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Signs a user into Lync.
        /// </summary>
        private void SignUserIn()
        {
            //********************************************************
            //If the UI of the Lync client is not suppressed then you do 
            //not collect user credentials for signing in. Instead, the
            //Windows network credentials are used.
            //********************************************************
            if (_ClientModel._LyncClient.InSuppressedMode == false && _ClientModel._LyncClient.State != ClientState.SignedIn)
            {
                //********************************************************
                //Call a helper method from the ClientModel helper class. UI
                //thread is blocked until user is signed in.
                //********************************************************
                _ClientModel.SignIn(null, null, null);

                return;
            }

            //********************************************************
            //If Lync is in UI supression mode and not signed in, collect user credentials and sign in.
            //********************************************************
            if (_ClientModel._LyncClient.InSuppressedMode == true && _ClientModel._LyncClient.State != ClientState.SignedIn)
            {
                _CredentialForm = new CredentialForm();
                _CredentialForm.ShowDialog();
                if (_CredentialForm.DialogResult == true)
                {
                    //********************************************************
                    //Client model handles the state checking and async code necessary before attempting to
                    //sign a user in.
                    //********************************************************
                    _ClientModel.SignIn(
                        _CredentialForm.userSIP,
                        _CredentialForm.userDomain,
                        _CredentialForm.userPassword);
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        #endregion

        #region constructors
        public ConversationWindow()
        {
            InitializeComponent();
        }
        #endregion

    }
}
