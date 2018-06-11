using System;
using System.Threading;
using System.Configuration;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml;
using Microsoft.Rtc.Collaboration.Sample.Common;
using Microsoft.Speech.VoiceXml;
using Microsoft.Speech.VoiceXml.Common;


namespace Microsoft.Rtc.Collaboration.Sample.VoiceXmlTime
{
  // After the application is started, it waits for an incoming audio-video call. When a call arrives, 
  // the application creates and initializes a Browser instance that loads and interprets a VoiceXML dialog. 
  // The dialog prompts the user for the name of a city, reissuing the prompt if necessary.
  // If the name of the city matches a city in an SRGS grammar, the VoiceXML dialog returns 
  // the time at the requested city.
  // After the application ends the call, it shuts down the platform and ends, and then pauses, so that the 
  // user can view log messages in the console.
  // The UCMA application logs in as UserName1, given in App.config.
  
  public class VoiceXmlAVCall
  {
    // Global variables.
    private UCMASampleHelper _helper;
    private UserEndpoint _userEndpoint;
    private CollaborationPlatform _collabPlatform;
    private AudioVideoCall _audioVideoCall;
    private AudioVideoFlow _audioVideoFlow;

    // The conversation.
    private Conversation _conversation;
 
    // The VoiceXML Browser and the location of the VoiceXML start page.
    private Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml.Browser _voiceXmlBrowser;
    private String startPageURL = @"http://localhost/VoiceXmlTime/GetCityTime.vxml"; 
  
    // Wait handles to keep the main thread and worker thread synchronized.
    private AutoResetEvent _waitForCallReceived = new AutoResetEvent(false);
    private AutoResetEvent _waitForCallAccepted = new AutoResetEvent(false);
    private AutoResetEvent _waitForSessionCompleted = new AutoResetEvent(false);
    private AutoResetEvent _waitForPlatformShutdownCompleted = new AutoResetEvent(false);
    private AutoResetEvent _waitForAudioVideoCallEstablishCompleted = new AutoResetEvent(false);
    private AutoResetEvent _waitForAudioVideoFlowStateChangedToActiveCompleted = new AutoResetEvent(false);

    
    static void Main(string[] args)
    {
      VoiceXmlAVCall BasicAVCall = new VoiceXmlAVCall();
      BasicAVCall.Run();
    }

    public void Run()
    {
        // Create and establish the endpoint, using the credentials of the user the application will be acting as.
        _helper = new UCMASampleHelper();
        _userEndpoint = _helper.CreateEstablishedUserEndpoint("VoiceXML Sample User" /*endpointFriendlyName*/);
        _userEndpoint.RegisterForIncomingCall<AudioVideoCall>(inboundAVCall_CallReceived);
        
        // Pause the main thread until a call is received and then accepted.
        _waitForCallReceived.WaitOne();
        _waitForCallAccepted.WaitOne();

        InitializeVoiceXmlBrowser();
        _voiceXmlBrowser.SetAudioVideoCall(_audioVideoCall);
        Uri startPageURI = new Uri(startPageURL);
        Console.WriteLine("Browser state: " + _voiceXmlBrowser.State.ToString());
        _voiceXmlBrowser.RunAsync(startPageURI, null);
        _waitForSessionCompleted.WaitOne();
                
        _collabPlatform = _conversation.Endpoint.Platform;
        // Terminate the call.
        _audioVideoCall.BeginTerminate(CallTerminateCB, _audioVideoCall);
           
        _waitForPlatformShutdownCompleted.WaitOne();

        // Pause the console to allow the user to view logs.
        Console.WriteLine("Press any key to end the sample.");
        Console.ReadKey();  
    }

    // Initializes the Browser object and registers event handlers.
    private void InitializeVoiceXmlBrowser()
    {
        // Create a Browser instance if one doesn’t already exist.
        if (_voiceXmlBrowser == null)
        {
            // Create the browser object, and bind all associated event handlers. 
            Console.WriteLine("Call state: " + _audioVideoCall.State.ToString() + "\nMedia flow state: " + _audioVideoCall.Flow.State.ToString());

            _voiceXmlBrowser = new Microsoft.Rtc.Collaboration.AudioVideo.VoiceXml.Browser();

            _voiceXmlBrowser.Disconnecting
                += new EventHandler<DisconnectingEventArgs>(HandleDisconnecting);
            _voiceXmlBrowser.Disconnected
                += new EventHandler<DisconnectedEventArgs>(HandleDisconnected);
            _voiceXmlBrowser.SessionCompleted
                += new EventHandler<SessionCompletedEventArgs>(HandleSessionCompleted);
        }
    }

    #region EVENT HANDLERS
  
    // Handler for the StateChanged event on the incoming call.
    void audioVideoCall_StateChanged(object sender, CallStateChangedEventArgs e)
    {
      Console.WriteLine("Call has changed state.\nPrevious state: " + e.PreviousState + "\nCurrent state: " + e.State);
    }
    
    // Handler for the StateChanged event on an AudioVideoFlow instance.
    private void audioVideoFlow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
    {
        // When the flow is active, media operations can begin.
        Console.WriteLine("Previous flow state: " + e.PreviousState.ToString() + "\nNew flow state: " + e.State.ToString());
    }

    // Handler for the AudioVideoFlowConfigurationRequested event on the call.
    // This event is raised when there is a flow present to begin media operations with, and that it is no longer null.
    public void audioVideoCall_FlowConfigurationRequested(object sender, AudioVideoFlowConfigurationRequestedEventArgs e)
    {
      Console.WriteLine("Flow Created.");
      _audioVideoFlow = e.Flow;

      // Now that the flow is non-null, bind the event handler for State Changed.
      // When the flow goes active, (as indicated by the state changed event) the application can take media-related actions on the flow.
      _audioVideoFlow.StateChanged += new EventHandler<MediaFlowStateChangedEventArgs>(audioVideoFlow_StateChanged);
    }

    // The delegate to be called when the inbound call arrives (the call from a customer). 
    private void inboundAVCall_CallReceived(object sender, CallReceivedEventArgs<AudioVideoCall> e)
    {
        _waitForCallReceived.Set();
        _audioVideoCall = e.Call;
        
       _audioVideoCall.AudioVideoFlowConfigurationRequested += this.audioVideoCall_FlowConfigurationRequested;
        _audioVideoCall.StateChanged += new EventHandler<CallStateChangedEventArgs>(audioVideoCall_StateChanged);

        // Create a new conversation instance.
        _conversation = new Conversation(_userEndpoint);
        // Accept the call.
        _audioVideoCall.BeginAccept(CallAcceptCB, _audioVideoCall);
        _audioVideoFlow = _audioVideoCall.Flow;
     }

    #endregion 


    #region BROWSER EVENT HANDLERS
 
    // Handler for the SessionCompleted event on the Browser object.
    // This implementation writes the values returned by the VoiceXML dialog to the console.
    private void HandleSessionCompleted(object sender, SessionCompletedEventArgs e)
    {
        _waitForSessionCompleted.Set();
        VoiceXmlResult result = e.Result;
        String cityOffset = result.Namelist["CityOffset"].ToString();
        String utterance = result.Namelist["CityOffset$.utterance"].ToString();
        String confidence = result.Namelist["CityOffset$.confidence"].ToString();
        String requestedTime = result.Namelist["timeAtRequestedCity"].ToString();
        Console.WriteLine("Returned semantic result: " + cityOffset);
        Console.WriteLine("Utterance: " + utterance);
        Console.WriteLine("Confidence: " + confidence);
        Console.WriteLine("Requested time: " + requestedTime);
    }

    // Handler for the Disconnecting event on the Browser object.
    private void HandleDisconnecting(object sender, DisconnectingEventArgs e)
    {
        Console.WriteLine("Disconnecting.");
    }

    // Handler for the Disconnected event on the Browser object.
    private void HandleDisconnected(object sender, DisconnectedEventArgs e)
    {
        Console.WriteLine("Disconnected.");
    }
     
    #endregion

    #region CALLBACKS

    // Callback referenced in the BeginAccept method on the call.
    private void CallAcceptCB(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            _waitForCallAccepted.Set();
            Console.WriteLine("Call is now accepted.");
            _audioVideoCall.EndAccept(ar);
        }
        else
        {
            Console.WriteLine("Couldn't accept the call.");
        }
    }

    // Callback referenced in the BeginTerminate method on the call.
    private void CallTerminateCB(IAsyncResult ar)
    {
        AudioVideoCall AVCall = ar.AsyncState as AudioVideoCall;

        // Complete the termination of the incoming call.
        AVCall.EndTerminate(ar);

        // Terminate the conversation.
        IAsyncResult result = _audioVideoCall.Conversation.BeginTerminate(ConversationTerminateCB, _audioVideoCall.Conversation);
        Console.WriteLine("Waiting for the conversation to be terminated...");
    }

    // Callback referenced in the BeginTerminate method on the conversation.
    private void ConversationTerminateCB(IAsyncResult ar)
    {
        Conversation conv = ar.AsyncState as Conversation;

        // Complete the termination of the conversation.
        conv.EndTerminate(ar);

        // Now, clean up by shutting down the platform.
        Console.WriteLine("Shutting down the platform...");

        _collabPlatform.BeginShutdown(PlatformShutdownCB, _collabPlatform);
    }

    // Callback referenced in the BeginShutdown method on the platform.
    private void PlatformShutdownCB(IAsyncResult ar)
    {
        CollaborationPlatform collabPlatform = ar.AsyncState as CollaborationPlatform;
        try
        {
            // Shutdown actions will not throw.
            collabPlatform.EndShutdown(ar);
            Console.WriteLine("The platform is now shut down.");
        }
        finally
        {
            _waitForPlatformShutdownCompleted.Set();
        }
    }

    #endregion


  }
    
}
