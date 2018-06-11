using System;
using System.Threading;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;
using Microsoft.Rtc.Collaboration.Sample.Common;

namespace Microsoft.Rtc.Collaboration.Sample.B2BCall
{
    // This application sets up a BackToBackCall instance that connects an inbound AudioVideoCall from a customer 
    // to an outbound AudioVideoCall to an agent.
    // This application logs in as the user listed in UserName1, in App.Config.  
    // When this application starts, it creates, but does not establish, an AudioVideoCall instance.
    // When a remote user (the customer) calls this application, a delegate creates and establishes 
    // the BackToBackCall instance that is used.
    // At this point the customer and agent are connected, and can carry on a conversation.
    // At the conclusing of the conversation, the application tears down the platform and ends, and then pauses
    // so allow the log messages to be viewed in the console.
    // 
    // This application requires the credentials of three users who can log in to  
    // Microsoft Lync Server 2010, and who are enabled for voice.

    public class UCMAB2BCallExample
    {
        // Some necessary instance variables
        private UCMASampleHelper _helper;
        private AudioVideoCall _inboundAVCall;
        private AudioVideoCall _outboundAVCall;
        private UserEndpoint _userEndpoint;

        // The information for the conversation and the far end participant.
        // The target of the call (agent) in the format sip:user@host (should be logged on when the application is run). This could also be in the format tel:+1XXXYYYZZZZ
        private static String _calledParty;

        // Conversation subject that appears at the top of the Lync 2010 conversation window for the inbound caller.
        private static String _outConversationSubject = "UCMA to agent conversation leg"; 
        
        private static String _conversationPriority = ConversationPriority.Urgent;

        // The conversations.
        // _inboundConversation is the conversation between the customer and the UCMA application.
        private Conversation _inboundConversation;
        // _outboundConversation is the conversation between the UCMA application and the agent.
        private Conversation _outboundConversation;

        // BackToBackCall and associated fields.
        private BackToBackCall _b2bCall;
        // _outboundCallLeg contains the settings for the leg from the UCMA application to the agent.
        private BackToBackCallSettings _outboundCallLeg;
        // _inboundCallLeg contains the settings for the leg from the customer to the UCMA application.
        private BackToBackCallSettings _inboundCallLeg;

        
        // Wait handles are used to synchronize the main thread and the worker thread that is
        // used for callbacks and event handlers.
        private AutoResetEvent _waitForConversationToTerminate = new AutoResetEvent(false);
        private AutoResetEvent _waitForCallToEstablish = new AutoResetEvent(false);
        private AutoResetEvent _waitForB2BCallToEstablish = new AutoResetEvent(false);
        private AutoResetEvent _waitUntilOneUserHangsUp = new AutoResetEvent(false);
        private AutoResetEvent _waitForB2BCallToTerminate = new AutoResetEvent(false);
        
        
        static void Main(string[] args)
        {
            UCMAB2BCallExample BasicB2BCallCall = new UCMAB2BCallExample();
            BasicB2BCallCall.Run();
        }

        public void Run()
        {
                                   
            // Initialize and register the endpoint, using the credentials of the user the application will be acting as.
            _helper = new UCMASampleHelper();
            _userEndpoint = _helper.CreateEstablishedUserEndpoint("B2BCall Sample User" /*endpointFriendlyName*/);
            _userEndpoint.RegisterForIncomingCall<AudioVideoCall>(inboundAVCall_CallReceived);

            // Conversation settings for the outbound call (to the agent).
            ConversationSettings outConvSettings = new ConversationSettings();
            outConvSettings.Priority = _conversationPriority;
            outConvSettings.Subject = _outConversationSubject;
 
            // Create the Conversation instance between UCMA and the agent.
            _outboundConversation = new Conversation(_userEndpoint, outConvSettings);

            // Create the outbound call between UCMA and the agent.
            _outboundAVCall = new AudioVideoCall(_outboundConversation);

            // Register for notification of the StateChanged event on the outbound call. 
            _outboundAVCall.StateChanged += new EventHandler<CallStateChangedEventArgs>(outboundAVCall_StateChanged);

            // Prompt for called party - the agent.
            _calledParty = UCMASampleHelper.PromptUser("Enter the URI of the called party, in sip:User@Host form or tel:+1XXXYYYZZZZ form => ", "RemoteUserURI1");

            _outboundCallLeg = new BackToBackCallSettings(_outboundAVCall, _calledParty);

            // Pause the main thread until both calls, the BackToBackCall, both conversations,
            // and the platform are shut down.
            _waitUntilOneUserHangsUp.WaitOne();

            // Pause the console to allow for easier viewing of logs.
            Console.WriteLine("Press any key to end the sample.");
            Console.ReadKey();
        }

        // Shut down the BackToBackCall, both Conversations, and the platform.
        void FinishShutdown()
        {
            _b2bCall.BeginTerminate(B2BTerminateCB, _b2bCall);
            _waitForB2BCallToTerminate.WaitOne();

            _outboundAVCall.Conversation.BeginTerminate(TerminateConversationCB, _outboundAVCall.Conversation);
            _inboundAVCall.Conversation.BeginTerminate(TerminateConversationCB, _inboundAVCall.Conversation);

            Console.WriteLine("Waiting for the conversation to be terminated...");
            _waitForConversationToTerminate.WaitOne();

            // Now, clean up by shutting down the platform.
            Console.WriteLine("Shutting down the platform...");
            _helper.ShutdownPlatform();
            _waitUntilOneUserHangsUp.Set();
        }

        #region EVENT HANDLERS

        // Handler for the StateChanged event on the inbound call. 
        void inboundAVCall_StateChanged(object sender, CallStateChangedEventArgs e)
        {

            Console.WriteLine("Inbound call - state change.\nPrevious state: "
                + e.PreviousState + "\nCurrent state: " + e.State
                + "\nTransitionReason: " + e.TransitionReason + "\n"); 
            if (e.TransitionReason == CallStateTransitionReason.TerminatedRemotely)
            {
                // If one call has been terminated remotely, unregister for 
                // notification of the StateChanged event.
                _outboundAVCall.StateChanged -= outboundAVCall_StateChanged;
                _inboundAVCall.StateChanged -= inboundAVCall_StateChanged;
                _inboundAVCall.BeginTerminate(TerminateCallCB, _inboundAVCall);
                
                if (_outboundAVCall != null)
                {
                    Console.WriteLine("Terminating the inbound call...");
                    _outboundAVCall.BeginTerminate(TerminateCallCB, _outboundAVCall);
                }

                FinishShutdown(); 
            }
        }

        // Handler for the StateChanged event on the outbound call.
        void outboundAVCall_StateChanged(object sender, CallStateChangedEventArgs e)
        {
            Console.WriteLine("Outbound call - state change.\nPrevious state: " 
                + e.PreviousState + "\nCurrent state: " + e.State
                + "\nTransitionReason: " + e.TransitionReason + "\n");
            if (e.TransitionReason == CallStateTransitionReason.TerminatedRemotely)
            {
                // If one call has been terminated remotely, unregister for 
                // notification of the StateChanged event.
                _inboundAVCall.StateChanged -= inboundAVCall_StateChanged;
                _outboundAVCall.StateChanged -= outboundAVCall_StateChanged;
                _outboundAVCall.BeginTerminate(TerminateCallCB, _outboundAVCall);
                
                if (_inboundAVCall != null)
                {
                    Console.WriteLine("Terminating the inbound call...");
                    _inboundAVCall.BeginTerminate(TerminateCallCB, _inboundAVCall);
                }

                FinishShutdown(); 
            }
        }

        
        /* private void audioVideoFlow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
        {
            Console.WriteLine("Flow state changed from " + e.PreviousState + " to " + e.State);

            // When the flow is active, media operations can begin.
            if (e.State == MediaFlowState.Active)
            {
                // Other samples demonstrate uses for an active flow.
            }
        } */

        // The delegate to be called when the inbound call arrives (the call from a customer). 
        private void inboundAVCall_CallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> e)
        {
            _inboundAVCall = e.Call;
            // Register for notification of the StateChanged event on the incoming call.
            _inboundAVCall.StateChanged += new EventHandler<CallStateChangedEventArgs>(inboundAVCall_StateChanged);

            // Create a new conversation for the incoming call leg.
            _inboundConversation = new Conversation(_userEndpoint);
            
            _inboundCallLeg = new BackToBackCallSettings(_inboundAVCall);
            
            // Create the back-to-back call instance.
            // Note that you need a Destination URI for the outgoing call leg, but not for the incoming call leg.
            _b2bCall = new BackToBackCall(_inboundCallLeg, _outboundCallLeg);
                              
            // Begin the back-to-back session; provide a destination.
            try
            {
                 IAsyncResult result = _b2bCall.BeginEstablish(BeginEstablishCB, _b2bCall);
                /* IAsyncResult result = _b2bCall.BeginEstablish(
                    delegate(IAsyncResult ar)
                {
                    _b2bCall.EndEstablish(ar);
                    _waitForB2BCallToEstablish.Set();
                }, _b2bCall);*/
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine("_b2bCall must be in the Idle state." + ioe.Message.ToString());
            }
            _waitForB2BCallToEstablish.WaitOne();
        }

        #endregion

        #region CALLBACKs

        // Callback for BeginTerminate on a call.
        private void TerminateCallCB(IAsyncResult ar)
        {
            AudioVideoCall audioVideoCall = ar.AsyncState as AudioVideoCall;

            // Finish terminating the call.
            audioVideoCall.EndTerminate(ar);
        }

        // Callback for BeginTerminate on the Conversation object.
        private void TerminateConversationCB(IAsyncResult ar)
        {
            Conversation conv = ar.AsyncState as Conversation;

            // Finish terminating the conversation.
            conv.EndTerminate(ar);

            // Synchronize the main and worker threads.
            _waitForConversationToTerminate.Set();
        }
        
        
        // Callback for BeginEstablish on the BackToBackCall instance.
         private void BeginEstablishCB(IAsyncResult ar)
        {
            _b2bCall.EndEstablish(ar);
            _waitForB2BCallToEstablish.Set();
        } 

        // Callback for BeginTerminate on the BackToBackCall instance.
        private void B2BTerminateCB(IAsyncResult ar)
        {
            BackToBackCall b2bCall = ar as BackToBackCall;
            if (b2bCall != null)
            {
                b2bCall.EndEstablish(ar);
            }
            _waitForB2BCallToTerminate.Set();
        }

        #endregion

    }
}
