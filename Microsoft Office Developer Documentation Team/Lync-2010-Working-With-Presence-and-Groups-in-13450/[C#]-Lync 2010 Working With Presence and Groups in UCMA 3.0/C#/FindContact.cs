// .NET namespaces
using System;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;

// UCMA namespaces
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Collaboration.Presence;
using Microsoft.Rtc.Collaboration.ContactsGroups;
using Microsoft.Rtc.Signaling;

// UCMA samples namespaces
using Microsoft.Rtc.Collaboration.Sample.Common;

namespace Microsoft.Rtc.Collaboration.Sample.FindContact
{
    // GroupID constants for the two Lync contact groups that are used in this application.
    enum GroupID
    {
      SERVICE_GROUP = 3, // Service Department group.
      SALES_GROUP = 4    // Sales Department group.
    };

    public class BasicIMCallHandling
    {
        #region Globals
        private UCMASampleHelper _helper;
        private UserEndpoint _userEndpoint;
        private Uri _remoteContactUri;
        private InstantMessagingCall _instantMessagingCall;
        private RemotePresenceView _remotePresenceView;
        private bool _isFirstMessage = true;
        private Conversation _incomingConversation;
        private ConferenceSession _conferenceSession;
        private GroupID _groupID;
        

        // Wait handles are used to keep the main thread and worker thread synchronized.
        private AutoResetEvent _waitUntilIncomingCallIsAccepted = new AutoResetEvent(false);
        private AutoResetEvent _waitUntilConferenceInvitationIsDelivered = new AutoResetEvent(false);
        private AutoResetEvent _waitForAvailableTarget = new AutoResetEvent(false);
        private AutoResetEvent _waitUntilConversationIsTerminated = new AutoResetEvent(false);

        #endregion

        /// <summary>
        /// Instantiate and run the ServiceGroup application.
        /// </summary>
        /// <param name="args">unused</param>
        public static void Main(string[] args)
        {
            BasicIMCallHandling basicCallHandling = new BasicIMCallHandling();
            basicCallHandling.Run();
        }
        
        private void Run()
        {
            // A helper class to take care of platform and endpoint setup and cleanup. 
            _helper = new UCMASampleHelper();

            // Create and establish a user endpoint using the user’s network credentials. 
            _userEndpoint = _helper.CreateEstablishedUserEndpoint(
                "FindContact Sample User" /* endpointFriendlyName */);

            // Register a delegate to be called when an incoming InstantMessagingCall arrives.
            _userEndpoint.RegisterForIncomingCall<InstantMessagingCall>(InstantMessagingCall_Received);
            
            Console.WriteLine("Waiting for an incoming instant messaging call...");
            int ThreadID = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Main thread: ID " + ThreadID);
            // Pause main thread until an incoming call arrives and is accepted.
            _waitUntilIncomingCallIsAccepted.WaitOne();

            InstantMessagingFlow imFlow = _instantMessagingCall.Flow;

            imFlow.BeginSendInstantMessage("Press 1 for Service Department.\n" +
                    "Press 2 for Sales Department.", CallSendInstantMessageCB, _instantMessagingCall);
            imFlow.MessageReceived += new EventHandler<InstantMessageReceivedEventArgs>(IMFlow_MessageReceived);
            _waitForAvailableTarget.WaitOne();
            
            if (_remoteContactUri != null)
            {
                imFlow.BeginSendInstantMessage("Contact found: " + _remoteContactUri.ToString(), CallSendInstantMessageCB, _instantMessagingCall);
                // Join the conversation to the IM MCU.
                _conferenceSession = _incomingConversation.ConferenceSession;
                ConferenceJoinOptions confJoinOptions = new ConferenceJoinOptions();
                confJoinOptions.JoinMode = JoinMode.Default;
                _conferenceSession.BeginJoin(confJoinOptions, ConferenceJoinCB, _conferenceSession);

                ThreadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Main thread: ID " + ThreadID);
                _waitUntilConferenceInvitationIsDelivered.WaitOne();
            }
            else
            {
                Console.WriteLine("Could not find an available contact.");
                imFlow.BeginSendInstantMessage("Could not find an available contact.\nPlease call again later.", CallSendInstantMessageCB, _instantMessagingCall);
            }
            // Unregister for notification of the MessageReceived event.
            imFlow.MessageReceived -= new EventHandler<InstantMessageReceivedEventArgs>(IMFlow_MessageReceived);
            // Cancel the subscription to the presence session by unsubscribing.
            _userEndpoint.ContactGroupServices.BeginUnsubscribe(ContactGroupUnsubscribeCB, _userEndpoint.ContactGroupServices);
            _remotePresenceView.BeginTerminate(ViewTerminateCB, _remotePresenceView);                
            UCMASampleHelper.PauseBeforeContinuing("Press ENTER to shut down and exit.");

            // Terminate the call, the conversation, and then unregister the 
            // endpoint from receiving an incoming call. Terminating these 
            // additional objects individually is made redundant by shutting down
            // the platform right after, but in the multiple call case, this is 
            // needed for object hygiene. Terminating a Conversation terminates 
            // all its associated calls, and terminating an endpoint  
            // terminates all conversations on that endpoint.
            _instantMessagingCall.BeginTerminate(CallTerminateCB, _instantMessagingCall);
            _waitUntilConversationIsTerminated.WaitOne();
            _userEndpoint.UnregisterForIncomingCall<InstantMessagingCall>(InstantMessagingCall_Received);

            // Clean up by shutting down the platform.
            _helper.ShutdownPlatform();
        }

        // Helper method that is called by the imFlow_MessageReceived delegate.
        // This method returns the URI of the first available contact in a particular group.
        // The group is determined in the handler for the NotificationReceived event, 
        // the ContactGroupServices_NotificationReceived delegate.
        private Uri GetFirstAvailableContact()
        {
            int ThreadID = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In getFirstAvailable - thread ID: " + ThreadID);
            RemotePresenceViewSettings presenceViewSettings = new RemotePresenceViewSettings();
            presenceViewSettings.SubscriptionMode = RemotePresenceViewSubscriptionMode.Persistent;
            _remotePresenceView = new RemotePresenceView(_userEndpoint, presenceViewSettings);
            
            _remotePresenceView.PresenceNotificationReceived += new EventHandler<
                RemotePresentitiesNotificationEventArgs>(RemotePresenceView_PresenceNotificationReceived);
            _userEndpoint.ContactGroupServices.NotificationReceived += new EventHandler
                    <Microsoft.Rtc.Collaboration.ContactsGroups.ContactGroupNotificationEventArgs>(
                    ContactGroupServices_NotificationReceived);
            Console.WriteLine("In getFirstAvailable, ContactGroupServices state: {0}", _userEndpoint.ContactGroupServices.CurrentState.ToString());
            _userEndpoint.ContactGroupServices.BeginSubscribe(ContactGroupSubscribeCB,
                _userEndpoint.ContactGroupServices);
            return _remoteContactUri;
        }
        

        #region Event Handlers
        void InstantMessagingCall_Received(object sender, CallReceivedEventArgs<InstantMessagingCall> e)
        {
            // Type checking was done by the platform; no risk of this being any 
            // type other than the type expected.
            _instantMessagingCall = e.Call;
            _incomingConversation = _instantMessagingCall.Conversation;

            // Call: StateChanged: Hooked up for logging, to show callstate transitions.
            _instantMessagingCall.StateChanged += 
                new EventHandler<CallStateChangedEventArgs>(InstantMessagingCall_StateChanged);
            
            // RemoteParticipantUri is the URI of the remote caller in this 
            // conversation. Toast is the message set by the caller as the 
            // 'greet' message for this call. 
            Console.WriteLine("Call Received! From: " + e.RemoteParticipant.Uri + " Toast is: " + 
                                                e.ToastMessage.Message);
            
            // Accept the call. 
            _instantMessagingCall.BeginAccept(CallAcceptCB, _instantMessagingCall);
        }

     
        // Record the ConferenceSession state transitions in the console.
        void ConferenceSession_StateChanged(object sender, StateChangedEventArgs<ConferenceSessionState> e)
        {
            ConferenceSession confSession = sender as ConferenceSession;

            Console.WriteLine("The conference session with Local Participant: " +
                confSession.Conversation.LocalParticipant +
                " has changed state. The previous conference state was: " + e.PreviousState +
                " and the current state is: " + e.State);
        }

        // Record the state transitions in the console.
        void InstantMessagingCall_StateChanged(object sender, CallStateChangedEventArgs e)
        {
            Console.WriteLine("Call has changed state. \nPrevious state: " + e.PreviousState); 
            Console.WriteLine("Current state: " + e.State);
        }

        // Delegate that is invoked when an incoming InstantMessageCall arrives.
        void IMFlow_MessageReceived(object sender, InstantMessageReceivedEventArgs e)
        {
            // If this is the first message, set _isFirstMessage to false and return.
            if (_isFirstMessage)
            {
              _isFirstMessage = false;
              Console.WriteLine("First message has arrived.");
              return;
            } 
                      
            // _isFirstMessage must be false, so look for a menu choice of 1 or 2.
            string choice = e.TextBody;
            Uri contact = null;
            
            if (choice.Equals("1"))
            {
                // Contact someone in the Service Dept.
                _groupID = GroupID.SERVICE_GROUP;
                
            }
            else if (choice.Equals("2"))
            {
                // Contact someone in the Sales Dept.
                _groupID = GroupID.SALES_GROUP;
            }
            contact = GetFirstAvailableContact();
        }

        // Handler for the PresenceNotificationReceived event.
        private void RemotePresenceView_PresenceNotificationReceived(object sender,
                  RemotePresentitiesNotificationEventArgs e)
        {
            // The Notifications property contains all notifications for one user.
            foreach (RemotePresentityNotification notification in e.Notifications)
            {
                if (notification.AggregatedPresenceState != null && (notification.AggregatedPresenceState.Availability == PresenceAvailability.Online))
                {
                  _remoteContactUri = new Uri(notification.PresentityUri);
                  Console.WriteLine("Remote target URI: " + _remoteContactUri.ToString());
                  // Can break out of loop after an available contact is found.
                  break;
                }
            }
            _waitForAvailableTarget.Set();
        }

        // Handler for the NotificationsReceived event on the ContactGroupServices class.
        private void ContactGroupServices_NotificationReceived(object sender,
            ContactsGroups.ContactGroupNotificationEventArgs e)
        {
            List<RemotePresentitySubscriptionTarget> targets = new List<RemotePresentitySubscriptionTarget>();
            foreach (NotificationItem<ContactsGroups.Contact> contactNotification in e.Contacts)
            {
              if (contactNotification.Operation == PublishOperation.Add)
              {
                foreach (int id in contactNotification.Item.GroupIds)
                {
                  if (id == (int)_groupID)
                  {
                    targets.Add(new RemotePresentitySubscriptionTarget(contactNotification.Item.Uri));
                  }
                }
              }
            } 
            _remotePresenceView.StartSubscribingToPresentities(targets);
        }

        
        // Handler for the StateChanged event on the ConferenceInvitation class.
        void conferenceInvite_StateChanged(object sender, ConferenceInvitationStateChangedEventArgs e)
        {
            Console.WriteLine("ConferenceInvitation state changed from {0} to {1}.",
                e.PreviousState,
                e.State);
        }

        #endregion

        #region Callbacks
        // Callback method referred to in the call to BeginAccept on the InstantMessagingCall instance.
        private void CallAcceptCB(IAsyncResult ar)
        {
            InstantMessagingCall instantMessagingCall = ar.AsyncState as InstantMessagingCall;
            try
            {
                // Determine whether the IM Call was accepted successfully.
                instantMessagingCall.EndAccept(ar);                    
            }
            catch (RealTimeException exception)
            {
                // RealTimeException can be thrown on media or link-layer failures. 
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                // Synchronize with main thread.
                _waitUntilIncomingCallIsAccepted.Set();
            }
        }

        // Callback method referred to in the call to BeginSendInstantMessage on the InstantMessagingFlow instance.
        private void CallSendInstantMessageCB(IAsyncResult ar)
        {
            InstantMessagingCall imCall = ar.AsyncState as InstantMessagingCall;
            try
            {
                imCall.Flow.EndSendInstantMessage(ar);
            }
            // A production application should have catch blocks for a number of
            // other exceptions, including FailureResponseException, ServerPolicyException, 
            // and OperationTimeoutException.
            catch (RealTimeException exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }


        // Callback method referred to in the call to BeginSubscribe on the ContactGroupServices instance.
        private void ContactGroupSubscribeCB(IAsyncResult ar)
        {
            ContactGroupServices services = ar.AsyncState as ContactGroupServices;
            try
            {
                services.EndSubscribe(ar);
                Console.WriteLine("In ContactGroupSubscribeCB, state is {0}", services.CurrentState.ToString());
            }

            // A production application should have catch blocks for a number 
            // of other exceptions, including OperationFailureException and PublishSubscribeException.
            catch (RealTimeException exception)
            {
                Console.WriteLine("Contact Group Subscription failed due to exception: {0}",
                    exception.ToString());
            }
        }

        // Callback method referred to in the call to BeginUnsubscribe on the ContactGroupServices instance.
        private void ContactGroupUnsubscribeCB(IAsyncResult ar)
        {
            ContactGroupServices services = ar.AsyncState as ContactGroupServices;
            try
            {
                services.EndUnsubscribe(ar);
            }

            // A production application should have catch blocks for a number 
            // of other exceptions, including InvalidOperationException and PublishSubscribeException.
            catch (RealTimeException exception)
            {
                Console.WriteLine("Contact Group Unsubscription failed due to exception: {0}",
                    exception.ToString());
            }
        }


        // Callback method referred to in the call to BeginJoin on the ConferenceSession instance.
        private void ConferenceJoinCB(IAsyncResult ar)
        {
            ConferenceSession conferenceSession = ar.AsyncState as ConferenceSession;
            try
            {
                conferenceSession.EndJoin(ar);
                Console.WriteLine("Conversation state: " + _incomingConversation.State.ToString());
                Console.WriteLine("Conference session state: " + _conferenceSession.State.ToString());
                _incomingConversation.BeginEscalateToConference(ConferenceEscalateCB, _incomingConversation);
                int ThreadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Conf join callback thread: ID " + ThreadID);
            }

            // A production application should have catch blocks for a number 
            // of other exceptions, including ConferenceFailureException, FailureRequestException,
            // and OperationFailureException.
            catch (RealTimeException exception)
            {
                Console.WriteLine(exception.ToString());
            }
                      
        }


        // Callback method referred to in the call to BeginEscalate on the Conference instance.
        private void ConferenceEscalateCB(IAsyncResult ar)
        {
            Conversation conversation = ar.AsyncState as Conversation;
            try
            {
                conversation.EndEscalateToConference(ar);
            }

            // A production application should have catch blocks for a number 
            // of other exceptions, including OperationTimeoutException and
            // OperationFailureException.
            catch (RealTimeException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            // Synchronize with main thread.
            // _waitUntilConferenceIsEscalated.Set();
            
            ConferenceInvitation conferenceInvite = new ConferenceInvitation(_incomingConversation);
            conferenceInvite.StateChanged += new EventHandler<ConferenceInvitationStateChangedEventArgs>(conferenceInvite_StateChanged);
            ConferenceInvitationDeliverOptions confDeliverOptions = new ConferenceInvitationDeliverOptions();
            confDeliverOptions.ToastMessage = new ToastMessage("Join the conference!");
            conferenceInvite.BeginDeliver(_remoteContactUri.ToString(), confDeliverOptions, InvitationDeliverCB, conferenceInvite);
        }

        // Callback method referred to in the call to BeginDeliver on the ConferenceInvitation instance.
        private void InvitationDeliverCB(IAsyncResult ar)
        {
            ConferenceInvitation conferenceInvite = ar.AsyncState as ConferenceInvitation;
            try
            {
                conferenceInvite.EndDeliver(ar);
            }

            // A production application should have catch blocks for a number 
            // of other exceptions, including FailureResponseException, OperationTimeoutException,
            // and OperationFailureException.
            catch (RealTimeException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            // Synchronize with main thread.
            _waitUntilConferenceInvitationIsDelivered.Set();
        }
        
        // Callback method referred to in the call to BeginTerminate on the RemotePresenceView instance.
        private void ViewTerminateCB(IAsyncResult ar)
        {
          RemotePresenceView view = ar.AsyncState as RemotePresenceView;
          view.EndTerminate(ar);
        }

        // Callback method referred to in the call to BeginTerminate on the InstantMessagingCall instance.
        private void CallTerminateCB(IAsyncResult ar)
        {
            InstantMessagingCall instantMessagingCall = ar.AsyncState as InstantMessagingCall;

            // Finish the termination of the incoming call.
            instantMessagingCall.EndTerminate(ar);

            // The call has been terminated, so remove the handler for the StateChanged event.
            _instantMessagingCall.StateChanged -= InstantMessagingCall_StateChanged;

            // Terminate the conversation.
            _instantMessagingCall.Conversation.BeginTerminate(ConversationTerminateCB,
                                            _instantMessagingCall.Conversation);
        }

        // Callback method referred to in the call to BeginTerminate on the Conversation instance.
        private void ConversationTerminateCB(IAsyncResult ar)
        {
            Conversation conversation = ar.AsyncState as Conversation;

            // Finish terminating the conversation.
            conversation.EndTerminate(ar);

            // Synchronize with main thread.
            _waitUntilConversationIsTerminated.Set();
        }
        #endregion
    }
}
