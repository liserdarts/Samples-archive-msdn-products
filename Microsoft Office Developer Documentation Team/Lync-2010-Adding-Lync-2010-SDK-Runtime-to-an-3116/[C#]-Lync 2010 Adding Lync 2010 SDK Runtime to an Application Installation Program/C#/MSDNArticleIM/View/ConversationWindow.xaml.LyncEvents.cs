using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model;
using System.Windows.Media;

namespace MSDNArticleIM
{
    /// <summary>
    /// This part of the ConversationWindow.xaml class is responsible for event handlers and
    /// callbacks for Lync API events.
    /// </summary>
    public partial class ConversationWindow : Window
    {
        /// <summary>
        /// Called by Platform when set composing operation completes
        /// </summary>
        /// <param name="ar"></param>
        private void ComposingCallback(System.IAsyncResult ar)
        {
            try
            {
                ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).EndSetComposing(ar);
            }
            catch (LyncClientException) { }
        }


        /// <summary>
        /// Raised when Lync client network connectivity is broken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _LyncClient_ClientDisconnected(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Client State Change: Disconnected from network" });
            //close this conversation form
            this.Dispatcher.Invoke(FormActor, new object[] { FormActions.CloseForm, this, null });
        }


        /// <summary>
        /// Handles LyncClient state changes for SignedIn and SignedOut
        /// </summary>
        /// <param name="sender">object. The LyncClient</param>
        /// <param name="e">ClientStateChangedEventArgs. The event state.</param>
        void _LyncClient_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
            //
            LyncClient client = (LyncClient)sender;

            switch (e.NewState)
            {
                case ClientState.SigningOut:
                    //************************************
                    //You must unregister for these events in the SigningOut state. Otherwise, you continue
                    //receive events while signing out. If your event handlers for these events call
                    //Lync API methods under this condition, the SignedOutException is raised.
                    //************************************
                    if (_remoteContact != null)
                    {
                        _remoteContact.ContactInformationChanged -= _remoteContact_ContactInformationChanged;
                        _ClientModel._LyncClient.ConversationManager.ConversationAdded -= ConversationsManager_ConversationAdded;
                        if ((InstantMessageModality)(_Conversation.Modalities[ModalityTypes.InstantMessage]) != null)
                        {
                            ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived -= myInstantMessageModality_MessageReceived;
                            ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).IsTypingChanged -= myInstantMessageModality_ComposingChanged;
                            _Conversation.Modalities[ModalityTypes.InstantMessage].ModalityStateChanged -= myInstantMessageModality_ModalityStateChanged;
                        }
                        if (_RemoteIMModality != null)
                        {
                            _RemoteIMModality.InstantMessageReceived -= myInstantMessageModality_MessageReceived;
                            _RemoteIMModality.IsTypingChanged -= myInstantMessageModality_ComposingChanged;
                            _RemoteIMModality.ModalityStateChanged -= myInstantMessageModality_ModalityStateChanged;
                        }
                    }
                    break;
                case ClientState.SignedOut:
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Sign-in State Change: Signed out" });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.EnableButton, SignIn_Button, null });
                    break;
                case ClientState.ShuttingDown:
                    break;
                case ClientState.SignedIn:
                    _ClientModel._LyncClient.ConversationManager.ConversationAdded += ConversationsManager_ConversationAdded;
                    if (_Conversation == null)
                    {
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Sign-in State Change: Signed in." });
                    }
                    else
                    {
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Sign-in State Change: Signed in. Conversation State: " + _Conversation.State.ToString() });
                        if (_Conversation.State == ConversationState.Active)
                        {
                            this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.CadetBlue });
                        }
                    }
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.DisableButton, SignIn_Button, null });
                    break;
            }
        }


        /// <summary>
        /// Handles ConversationAdded state change event raised on ConversationsManager
        /// </summary>
        /// <param name="source">ConversationsManager The source of the event.</param>
        /// <param name="data">ConversationsManagerEventArgs The event data. The incoming Conversation is obtained here.</param>
        void ConversationsManager_ConversationAdded(Object source, ConversationManagerEventArgs data)
        {
            if (_targetUri == null || _targetUri.Length == 0)
            {
                //***********************************************
                //User has not specified the SIP address of a remote user.
                //***********************************************
                return;
            }

            //***********************************************
            //Register for Conversation state changed events.
            //***********************************************
            data.Conversation.ParticipantAdded += Conversation_ParticipantAdded;
            data.Conversation.StateChanged += Conversation_StateChangedEvent;
            try
            {

                //***********************************************
                //Get a Contact instance using the SIP address of a 
                //remote user. _targetUri is a class field that is initiated 
                //with the contents of the SIP address text entry box
                //on the conversation form. 
                //***********************************************
                _remoteContact = _ClientModel._LyncClient.ContactManager.GetContactByUri(_targetUri);

                //***********************************************
                //Register for contact information changed events to catch changes in contact availablity before and during a conversation.
                //***********************************************
                _remoteContact.ContactInformationChanged += new EventHandler<ContactInformationChangedEventArgs>(_remoteContact_ContactInformationChanged);

                //***********************************************
                //Set conversation window title to current state of conversation
                //***********************************************
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is " + data.Conversation.State.ToString() });

                //**************************************
                //Call helper method to add the contact to the conversation.
                //**************************************
                if (AddContactToConversation(data.Conversation, _remoteContact) == false)
                {
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, ActivityText_Label, "ConversationAdded Event: Participant not added" });
                }
            }
            catch (ItemNotFoundException)
            {
                data.Conversation.End();
            }
            catch (ItemAlreadyExistException) { }

        }

        /// <summary>
        /// Handles event raised when the state of an active conversation has changed. 
        /// </summary>
        /// <param name="source">Conversation. The active conversation that raised the state change event.</param>
        /// <param name="data">ConversationStateChangedEventArgs. Event data containing state change data</param>
        void Conversation_StateChangedEvent(Object source, ConversationStateChangedEventArgs data)
        {
            switch (data.NewState)
            {
                case ConversationState.Terminated:
                    //*******************************************************************
                    //update form status label text with current state of conversation.
                    //*******************************************************************
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.AntiqueWhite });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.DisableButton, EndConversation_Button, null });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation Ended" });
                    break;
                case ConversationState.Active:
                    //*******************************************************************
                    //update form status label text with current state of conversation.
                    //*******************************************************************
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.CadetBlue });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is Active" });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.EnableButton, EndConversation_Button, null });
                    break;
                case ConversationState.Inactive:
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.AntiqueWhite });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is inactive" });
                    break;
                case ConversationState.Invalid:
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.AntiqueWhite });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is invalid" });
                    break;
            }
        }

        /// <summary>
        /// ParticipantAdded callback handles ParticpantAdded event raised by Conversation
        /// </summary>
        /// <param name="source">Conversation Source conversation.</param>
        /// <param name="data">ParticpantCollectionEventArgs Event data</param>
        void Conversation_ParticipantAdded(Object source, ParticipantCollectionChangedEventArgs data)
        {

            if (data.Participant.IsSelf == false)
            {

                //******************************************************************************
                //wantedInformation is a dictionary that contains the types of contact information
                //that this sample application is interested in. You must specify which contact
                //information you want updates for in order to receive those updates.
                //
                //You can declare one dictionary for all contacts or a unique instance of the dictionary
                //for each contact you are interested in. With a unique dictionary, you can specify
                //different contact information types for individual contacts.
                //******************************************************************************
                List<ContactInformationType> wantedInformation = new List<ContactInformationType>();
                wantedInformation.Add(ContactInformationType.SourceNetwork);
                wantedInformation.Add(ContactInformationType.Capabilities);
                wantedInformation.Add(ContactInformationType.Availability);



                //***********************************************************
                //canReceiveFormattedIM is a helper method in this class that
                //looks at the published Capabilities information of a contact
                //to see if that contact is capable of getting and sending IM.
                //***********************************************************
                _FormatMessageAsText = canReceiveFormattedIM(data.Participant.Contact.GetContactInformation(wantedInformation));

                if (((Conversation)source).Modalities.ContainsKey(ModalityTypes.InstantMessage))
                {

                    data.Participant.ActionAvailabilityChanged += new EventHandler<ParticipantActionAvailabilityChangedEventArgs>(Participant_ActionAvailabilityChanged);
                    _RemoteIMModality = data.Participant.Modalities[ModalityTypes.InstantMessage] as InstantMessageModality;
                    _RemoteIMModality.InstantMessageReceived += myInstantMessageModality_MessageReceived;
                    _RemoteIMModality.IsTypingChanged += myInstantMessageModality_ComposingChanged;
                    _RemoteIMModality.ActionAvailabilityChanged += new EventHandler<ModalityActionAvailabilityChangedEventArgs>(_RemoteIMModality_ActionAvailabilityChanged);

                    _Conversation.Modalities[ModalityTypes.InstantMessage].ActionAvailabilityChanged += _RemoteIMModality_ActionAvailabilityChanged;

                    _RemoteIMModality.ModalityStateChanged += myInstantMessageModality_ModalityStateChanged;
                    _Conversation.Modalities[ModalityTypes.InstantMessage].ModalityStateChanged += myInstantMessageModality_ModalityStateChanged;

                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, this.ActivityText_Label, "Participant Added" });
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.EnableButton, Send_Button, null });

                }
            }
            else
            {
                _LocalIMModality = data.Participant.Modalities[ModalityTypes.InstantMessage] as InstantMessageModality;
                _LocalIMModality.InstantMessageReceived += myInstantMessageModality_MessageReceived;
            }
        }

        /// <summary>
        /// Handles the event raised when contact information is updated for a contact
        /// </summary>
        /// <param name="sender">object. The Contact whose information has changed.</param>
        /// <param name="e">ContactInformationChangedEventArgs. The event state. Provides collection of change contact information types.</param>
        void _remoteContact_ContactInformationChanged(object sender, ContactInformationChangedEventArgs e)
        {

            //**************************************
            //This event handler performs any of 4 actions based on contact availability:
            // 1: Sets the background color of the form based on contact availability
            // 2: Sets the conversation window title to current conversation state and contact availability
            // 3: Enables or disables the SendMessage_Button
            // 4: Adds the _remoteContact to the conversation if it is available to join the conversation. 
            //
            //Note: An conversation state 
            //**************************************

            //**************************************
            //Has the contact availability changed?
            //**************************************
            if (e.ChangedContactInformation.Contains(ContactInformationType.Availability))
            {
                ContactAvailability currentAvailability = (ContactAvailability)((Contact)sender).GetContactInformation(ContactInformationType.Availability);
                switch (currentAvailability)
                {
                    case ContactAvailability.DoNotDisturb:
                        //**************************************
                        //Contact is not available.
                        //**************************************

                        //**************************************
                        //Update the window title text with the current conversation state and contact availability.
                        //**************************************
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is " + _Conversation.State.ToString() + ". Contact is " + currentAvailability.ToString() });
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.AntiqueWhite });

                        //**************************************
                        //Disable the send message button on conversation form.
                        //**************************************
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.DisableButton, Send_Button, null });
                        break;
                    case ContactAvailability.Offline:
                        //**************************************
                        //Contact is not available.
                        //**************************************

                        //**************************************
                        //Change form color to white, indicating the remote participant cannot accept Im text.
                        //**************************************
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.AntiqueWhite });
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is " + _Conversation.State.ToString() + ". Contact is " + currentAvailability.ToString() });

                        //**************************************
                        //Disable the send message button on conversation form.
                        //**************************************
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.DisableButton, Send_Button, null });
                        break;
                    default:
                        //**************************************
                        //If the contact is now available to join a conversation...
                        //**************************************

                        //**************************************
                        //Call helper method to add the contact to the conversation.
                        //**************************************
                        AddContactToConversation(_Conversation, (Contact)sender);

                        //**************************************
                        //Change the conversation form color to blue if
                        //conversation state is active and contact is available.
                        //**************************************
                        if (_Conversation.State == ConversationState.Active)
                        {
                            this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetFormBackColor, this, Brushes.CadetBlue });
                        }
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateWindowTitle, this, "Conversation is " + _Conversation.State.ToString() + ". Contact is " + currentAvailability.ToString() });

                        //**************************************
                        //enable send message button on conversation form
                        //**************************************
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.EnableButton, Send_Button, null });
                        break;
                }
            }
        }


        void Participant_ActionAvailabilityChanged(object sender, ParticipantActionAvailabilityChangedEventArgs e)
        {
            MessageBox.Show(((Participant)sender).Contact.GetContactInformation(ContactInformationType.DisplayName) + ": Action availability changed " + e.Action.ToString() + " is " + e.IsAvailable.ToString());
        }


        /// <summary>
        /// Handles the event raised when an IM is sent on the conversation.
        /// </summary>
        /// <param name="source">InstantMessageModality Modality </param>
        /// <param name="data">SendMessageEventArgs The new message.</param>
        void myInstantMessageModality_MessageReceived(Object source, MessageSentEventArgs data)
        {
            IDictionary<InstantMessageContentType, string> messageFormatProperty = data.Contents;
            string Sender = (string)((InstantMessageModality)source).Participant.Contact.GetContactInformation(ContactInformationType.DisplayName);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string receivedMessage;
            if (data.Contents.TryGetValue(InstantMessageContentType.Html, out receivedMessage))
            {
                //****************************************************************
                //Add newest message to conversation history list box data source.
                //****************************************************************
                _ConversationHistory.Add(receivedMessage);
            }
            else if (data.Contents.TryGetValue(InstantMessageContentType.PlainText, out receivedMessage))
            {
                //****************************************************************
                //Wrap plain text in DIV tags for consumption by the WebBrowser control.
                //****************************************************************
                receivedMessage = "<DIV style=\"font-size:" +
                    "10pt;font-family:" +
                    "MS Shell Dlg 2;color: #463939;direction: ltr\">" +
                    receivedMessage +
                    "</DIV>";
                _ConversationHistory.Add(receivedMessage);
            }
            else
            {
                //****************************************************************
                //Wrap message text in DIV tags for consumption by the WebBrowser control.
                //****************************************************************
                receivedMessage = "<DIV style=\"font-size:" +
                    "18pt;font-family:" +
                    "MS Shell Dlg 2;color: #463939;direction: ltr\">" +
                    data.Text +
                    "</DIV>";
                _ConversationHistory.Add(receivedMessage);
            }

            //****************************************************************
            //Invoke delegate to update conversation history list box on UI thread.
            //****************************************************************
            object[] actionObjectArray = { Sender, _ConversationHistory };
            this.Dispatcher.Invoke(FormActor, new object[] { FormActions.SetListContents, History_WebBrowser, actionObjectArray });

        }

        void _RemoteIMModality_ActionAvailabilityChanged(object sender, ModalityActionAvailabilityChangedEventArgs e)
        {
            if (e.Action == ModalityAction.SendInstantMessage && e.IsAvailable == true)
            {
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.EnableButton, Send_Button, null });
            }
        }

        /// <summary>
        /// Handles event raised when conversation participant is entering a message to be sent.
        /// </summary>
        /// <param name="source">InstantMessageModality. The IM modality owned by the composing participant.</param>
        /// <param name="data">ComposingChangeEventArgs. Event argument data.</param>
        void myInstantMessageModality_ComposingChanged(Object source, IsTypingChangedEventArgs data)
        {
            if (data.IsTyping == true)
            {
                //****************************************************************
                //Get the name of the conversation participant whose typing status has changed.
                //****************************************************************
                string ParticipantName = ((InstantMessageModality)source).Participant.Contact.GetContactInformation(ContactInformationType.DisplayName).ToString();

                //****************************************************************
                //Invoke delegate to update the typing status label contents on the conversation form.
                //****************************************************************
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, ActivityText_Label, ParticipantName + " is typing" });

                //****************************************************************
                //Start the DispatcherTimer clock to tick in 5 seconds.
                //****************************************************************
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.StartTimer, _DispatcherTimer_DisplayComposing, null });
            }
        }

        /// <summary>
        /// Handles event raised when IM modality state changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myInstantMessageModality_ModalityStateChanged(object sender, ModalityStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case ModalityState.Connected:
                    if ((Modality)sender == _Conversation.Modalities[ModalityTypes.InstantMessage])
                    {
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, ActivityText_Label, "Modality is " + e.NewState.ToString() });
                    }
                    break;
                case ModalityState.ConnectingToCaller:
                    break;
                case ModalityState.Disconnected:
                    break;
                case ModalityState.Joining:
                    if ((Modality)sender == _Conversation.Modalities[ModalityTypes.InstantMessage])
                    {
                        this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, ActivityText_Label, "Modality is " + e.NewState.ToString() });
                    }
                    break;
            }
        }

        /// Close form once client is signed in
        /// 
        /// </summary>
        void _ClientModel_ClientStateChangedEvent(bool signedIn)
        {
            //if client was signed in and is no longer signed in
            if (signedIn != true)
            {
                //clear search form; need to start over
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.UpdateLabel, this.lblFindStatus, string.Empty });
            }
            else
            {
                FormActionDelegate e = new FormActionDelegate(TakeFormAction);
                this.Dispatcher.Invoke(e, new object[] { FormActions.DisplayDefaultCursor, null, null });

                //close sign in form if it is open
                if (this._signInForm != null)
                {
                    this.Dispatcher.Invoke(FormActor, new object[] { FormActions.CloseForm, this._signInForm });
                }
            }
        }

        void _ClientModel_ClientShutdownEvent()
        {
            MessageBox.Show("Lync is shutting down. This application will shut down", "Lync Critical Exception");

            this.Dispatcher.Invoke(FormActor, new object[] { FormActions.CloseForm, this, null });

            if (this._signInForm != null)
            {
                this.Dispatcher.Invoke(FormActor, new object[] { FormActions.CloseForm, this._signInForm, null });
            }

        }

        /// <summary>
        /// Event handler called on LyncClient thread when Lync is initialized.
        /// </summary>
        void _ClientModel_ClientInitializedEvent()
        {
            SignUserIn();
        }

    }
}
