using System;
using System.Threading;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Signaling;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.AudioFormat;
using Microsoft.Rtc.Collaboration.Sample.Common;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using System.Configuration;

namespace Microsoft.Rtc.Collaboration.Sample.IM_And_SpeechReco
{

    public class UCMA_SR_IM
    {
        private UCMASampleHelper _helper;
        private UserEndpoint _userEndpoint;
        private AudioVideoCall _audioVideoCall;
        private AudioVideoFlow _audioVideoFlow;
        private string[] imMessageContent = new string[5];
        int index = 0;
        ConversationWindow cWindow;


        // Wait handles are used to keep the main and worker threads synchronized.
        private AutoResetEvent _waitForCallToBeAccepted = new AutoResetEvent(false);
        private AutoResetEvent _waitForConnectorToStop = new AutoResetEvent(false);
        private AutoResetEvent _waitForConversationToBeTerminated = new AutoResetEvent(false);
        private AutoResetEvent _waitForShutdownEventCompleted = new AutoResetEvent(false);
        private AutoResetEvent _waitForRecoCompleted = new AutoResetEvent(false);
        private AutoResetEvent _waitForXXXCompleted = new AutoResetEvent(false);
        
        static void Main(string[] args)
        {
            UCMA_SR_IM recommend = new UCMA_SR_IM();
            recommend.Run();
        }

        public void Run()
        {
            // A helper class to take care of platform and endpoint setup and cleanup. 
            _helper = new UCMASampleHelper();

            // Create a user endpoint using the network credential object. 
            _userEndpoint = _helper.CreateEstablishedUserEndpoint("Broadcast User");

            // Register a delegate to be called when an incoming audio-video call arrives.
            _userEndpoint.RegisterForIncomingCall<AudioVideoCall>(AudioVideoCall_Received);

            // Wait for the incoming call to be accepted.
            Console.WriteLine("Waiting for incoming call...");
            _waitForCallToBeAccepted.WaitOne();

            // Create a speech recognition connector and attach an AudioVideoFlow to it.
            SpeechRecognitionConnector speechRecognitionConnector = new SpeechRecognitionConnector();
            speechRecognitionConnector.AttachFlow(_audioVideoFlow);

            // Start the speech recognition connector.
            SpeechRecognitionStream stream = speechRecognitionConnector.Start();

            // Create a speech recognition engine.
            SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine();
            speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognitionEngine_SpeechRecognized);

            //Add a grammar.
            string[] recoString = { "buy", "sell", "Fabrikam", "Contoso", "maximum", "minimum", "one", "ten", "twenty", "send" };
            Choices choices = new Choices(recoString);
            speechRecognitionEngine.LoadGrammar(new Grammar(new GrammarBuilder(choices)));
            
            //Attach to audio stream to the SR engine.
            SpeechAudioFormatInfo speechAudioFormatInfo = new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Sixteen, Microsoft.Speech.AudioFormat.AudioChannel.Mono);
            speechRecognitionEngine.SetInputToAudioStream(stream, speechAudioFormatInfo);
            Console.WriteLine("\r\nGrammar loaded, say send to send IM.");

            //Prepare the SR engine to perform multiple asynchronous recognitions.
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            //Pause the main thread until recognition completes.
            _waitForConnectorToStop.WaitOne();
            speechRecognitionConnector.Stop();
            Console.WriteLine("connector stopped");

            // Detach the flow from the speech recognition connector, to prevent the flow from being kept in memory.
            speechRecognitionConnector.DetachFlow();

            // Terminate the call, the conversation, and then unregister the 
            // endpoint from receiving an incoming call. 
            _audioVideoCall.BeginTerminate(CallTerminateCB, _audioVideoCall);
            _waitForConversationToBeTerminated.WaitOne();

            // Shut down the platform.
            _helper.ShutdownPlatform();
        }

        public void SendIM()
        {
            Console.WriteLine("exiting");

            //Get a Lync client automation object.
            LyncClient client = LyncClient.GetClient();
            Automation automation = LyncClient.GetAutomation();

            //Add two URIs to the list of IM addresses.
            System.Collections.Generic.List<string> inviteeList = new System.Collections.Generic.List<string>();
            inviteeList.Add(ConfigurationManager.AppSettings["UserURI"]);
            //inviteeList.Add(ConfigurationManager.AppSettings["CallingUserURI"]);
            inviteeList.Add(ConfigurationManager.AppSettings["UserURI2"]);

            //Specify IM settings.
            System.Collections.Generic.Dictionary<AutomationModalitySettings, object> mSettings = new System.Collections.Generic.Dictionary<AutomationModalitySettings, object>();
            string messageText = ImMessageText();
            mSettings.Add(AutomationModalitySettings.FirstInstantMessage, messageText);
            mSettings.Add(AutomationModalitySettings.SendFirstInstantMessageImmediately, true);

            //Broadcast the IM messages.
            IAsyncResult ar = automation.BeginStartConversation(AutomationModalities.InstantMessage, inviteeList, mSettings, null, null);
            cWindow = automation.EndStartConversation(ar);
            AutoResetEvent completedEvent = new AutoResetEvent(false);
            completedEvent.WaitOne();
        }

        //Build the IM message string.
        string ImMessageText()
        {
            string msgTXT = "Hi this is your broker Kate Berger with an urgent recommendation to ";
            string operation = imMessageContent[0];
            string name = imMessageContent[1];
            string minORmax = imMessageContent[2];
            string price = imMessageContent[3];

            if (operation == "buy")
            {
                msgTXT = msgTXT + "BUY " + name + " with a " + minORmax + " value of " + price + ".";
            }

            else
            {
                msgTXT = msgTXT + "SELL " + name + "with a " + minORmax + " value of " + price + ".";
            }

            return msgTXT;
        }


        #region EVENT HANDLERS
       
        // Delegate that is called when an incoming AudioVideoCall arrives.
        void AudioVideoCall_Received(object sender, CallReceivedEventArgs<AudioVideoCall> e)
        {
            _audioVideoCall = e.Call;
            _audioVideoCall.AudioVideoFlowConfigurationRequested += this.AudioVideoCall_FlowConfigurationRequested;

            // For logging purposes, register for notification of the StateChanged event on the call.
            _audioVideoCall.StateChanged +=
                      new EventHandler<CallStateChangedEventArgs>(AudioVideoCall_StateChanged);

            // Remote Participant URI represents the far end (caller) in this conversation. 
            Console.WriteLine("Call received from: " + e.RemoteParticipant.Uri);

            // Now, accept the call. CallAcceptCB will run on the same thread.
            _audioVideoCall.BeginAccept(CallAcceptCB, _audioVideoCall);
        }

        // Handles the StateChanged event on the incoming audio-video call.
        void AudioVideoCall_StateChanged(object sender, CallStateChangedEventArgs e)
        {
            Console.WriteLine("Previous call state: " + e.PreviousState + "\nCurrent state: " + e.State);
        }

        // Handles the StateChanged event on the audio-video flow.
        private void AudioVideoFlow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
        {
            // When the flow is active, media operations can begin.
            if (e.State == MediaFlowState.Terminated)
            {
                // Detach the speech recognition connector, because the state of the flow is now Terminated.
                AudioVideoFlow avFlow = (AudioVideoFlow)sender;
                if (avFlow.SpeechRecognitionConnector != null)
                {
                    avFlow.SpeechRecognitionConnector.DetachFlow();
                }
            }
        }

        public void AudioVideoCall_FlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
        {
            Console.WriteLine("Flow Created.");
            _audioVideoFlow = e.Flow;

            // Now that the flow is non-null, bind a handler for the StateChanged event.
            // When the flow goes active, (as indicated by the StateChanged event) the application can take media-related actions on the flow.
            _audioVideoFlow.StateChanged += new EventHandler<MediaFlowStateChangedEventArgs>(AudioVideoFlow_StateChanged);
        }

        void SpeechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            RecognitionResult result = e.Result;
            if (result != null)
            {
                Console.WriteLine("Speech recognized: " + result.Text);

                imMessageContent[index++] = result.Text;

                if (result.Text.Equals("send"))
                {
                    SendIM();       //Call the method to send IM messages.
                    _waitForXXXCompleted.Set();
                }
            }
        }

        #endregion

        #region CALLBACK METHODS

        private void CallAcceptCB(IAsyncResult ar)
        {
            AudioVideoCall audioVideoCall = ar.AsyncState as AudioVideoCall;
            try
            {
                // Determine whether the call was accepted successfully.
                audioVideoCall.EndAccept(ar);
            }
            catch (RealTimeException exception)
            {
                // RealTimeException may be thrown on media or link-layer failures. 
                // A production application should catch additional exceptions, such as OperationTimeoutException,
                // OperationTimeoutException, and CallOperationTimeoutException.

                Console.WriteLine(exception.ToString());
            }
            finally
            {
                // Synchronize with main thread.
                _waitForCallToBeAccepted.Set();
            }
        }

        private void CallTerminateCB(IAsyncResult ar)
        {
            AudioVideoCall audioVideoCall = ar.AsyncState as AudioVideoCall;

            // Finish terminating the incoming call.
            audioVideoCall.EndTerminate(ar);

            // Unregister this event handler now that the call has been terminated.
            _audioVideoCall.StateChanged -= AudioVideoCall_StateChanged;

            // Terminate the conversation.
            _audioVideoCall.Conversation.BeginTerminate(ConversationTerminateCB, _audioVideoCall.Conversation);
        }

        private void ConversationTerminateCB(IAsyncResult ar)
        {
            Conversation conversation = ar.AsyncState as Conversation;

            // Finish terminating the conversation.
            conversation.EndTerminate(ar);

            // Unregister for incoming calls.
            _userEndpoint.UnregisterForIncomingCall<AudioVideoCall>(AudioVideoCall_Received);
            // Synchronize with main thread.
            _waitForConversationToBeTerminated.Set();
        }

        #endregion
    }
}
