using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;

namespace WindowsFormsApplication3
{
    static class Program
    {
        
        // The main entry point for the application.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class SignIn
    {
        private LyncClient _LyncClient;
        string password;
        public SendMessage _SendMessage;
        public bool SigningOut;    //flag to identify whether the user is signing out.
        
        //Constructor
        //Called from a click event on Form1
        public SignIn(string passwrd, bool SigningOut)
        {
            password = passwrd;

            try
            {
                _LyncClient = LyncClient.GetClient();

                if (_LyncClient == null)
                {
                    throw new Exception("Unable to obtain client interface");
                }
               
                //Register event handlers right after getting the client object
                _LyncClient.SignInDelayed += new EventHandler<SignInDelayedEventArgs>(_LyncClient_SignInDelayed);
                _LyncClient.StateChanged += new EventHandler<ClientStateChangedEventArgs>(_LyncClient_StateChanged);
                _LyncClient.CredentialRequested += new EventHandler<CredentialRequestedEventArgs>(_LyncClient_CredentialRequested);
                

                if (_LyncClient.InSuppressedMode == true)
                {

                    if (_LyncClient.State == ClientState.Uninitialized)
                    {
                        MessageBox.Show("Lync is uninitialized");
                        Object[] _asyncState = { _LyncClient };
                        _LyncClient.BeginInitialize(InitializeCallback, _asyncState);
                    }
                }
            }
            catch (NotStartedByUserException h)
            {
                throw new Exception("Lync is not running: " + h.Message );
            }
            catch (Exception ex)
            {
                throw new Exception("General Exception: " + ex.Message);
            }
        }

        //CredentialRequested event handler
        private void _LyncClient_CredentialRequested(object sender, CredentialRequestedEventArgs e)
        {
            if (e.Type == CredentialRequestedType.SignIn)
            {
                //You should collect a new password here.
                e.Submit(ConfigurationManager.AppSettings["UserDomainAndUserName"], "yourPassword", false);
            }
            
            
        }

        //SignInDelayed event handler
        private void _LyncClient_SignInDelayed(object sender, SignInDelayedEventArgs e)
        {   
            MessageBox.Show("SignInDelayed event handler called.");
        }

        //StateChanged event handler
        private void _LyncClient_StateChanged(object sender, ClientStateChangedEventArgs e)
        {

            if (e.NewState == ClientState.SignedIn)
            {
                _SendMessage = new SendMessage();

                _LyncClient.ConversationManager.ConversationAdded += new EventHandler<ConversationManagerEventArgs>(_SendMessage.ConversationManager_ConversationAdded);
            }
            if (e.NewState == ClientState.SignedOut & !SigningOut)  //SigningOut is passed in as parameter to the ShutDown and SignIn methods
            {
                MessageBox.Show("Lync is beginning to sign in");

                System.Collections.Generic.List<string> credentials = new List<string>();
                credentials.Add(ConfigurationManager.AppSettings["UserURI1"]);
                credentials.Add(ConfigurationManager.AppSettings["UserDomainAndUserName"]);
                string userURI = credentials[0].ToString();
                string userDomain = credentials[1].ToString();

                ((LyncClient)sender).BeginSignIn(userURI, userDomain, password, SigninCallback, sender);
            }
        }

        //BeginSignIn method callback
        private void SigninCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {

                try
                {
                    ((LyncClient)ar.AsyncState).EndSignIn(ar);
                    MessageBox.Show("Sign in completed: " + _LyncClient.State.ToString());
                }
                catch (RequestCanceledException re)
                {
                    MessageBox.Show("Request canceled exception: " + re.Message);
                }
            }

        }

        //BeginInitialize method callback
        private void InitializeCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {
                object[] asyncState = (object[])ar.AsyncState;
                ((LyncClient)asyncState[0]).EndInitialize(ar);

                //_ThisInitializedLync is part of application state and is 
                //a class Boolean field that is set to true if this process
                //initialized Lync.
                MessageBox.Show("Lync is initialized");
            }
            else
            {
                MessageBox.Show("Lync is NOT initialized");
            }

        }
    }

    public class SendMessage
    {
        private LyncClient _LyncClient;
        private string myRemoteParticipantUri;
        private Conversation _Conversation;
        
        public SendMessage()
        {
            _LyncClient = LyncClient.GetClient();
            if (_LyncClient.State == ClientState.SignedIn)
            {
                MessageBox.Show("Lync is signed in");
            }
            else 
            {
                MessageBox.Show("Lync is not signed in");
            }
        }

        public void StartIMConversation()
        {
            System.Collections.Generic.List<string> participantsList = new List<string>();
            participantsList.Add(ConfigurationManager.AppSettings["UserURI2"]);
            myRemoteParticipantUri = participantsList[0];

            if (_LyncClient.State == ClientState.SignedIn)
            {
                _Conversation = _LyncClient.ConversationManager.AddConversation();
            }

            else
            {
                MessageBox.Show("Lync is not signed in");
            }
        }

        //Called from the ParticipantAdded event handler
        private void SendIM()
        {
            try
            {
                if (((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).CanInvoke(ModalityAction.SendInstantMessage))
                {
                    string textMessage = "IM from Lync 2010 SDK Model API";
                    ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).BeginSendMessage(
                        textMessage
                        , SendMessageCallback
                        , null);
                }
            }
            catch (LyncClientException e)
            {
                MessageBox.Show("Client Platform Exception: " + e.Message);
            }
        }

        //ConversationAdded event handler
        internal void ConversationManager_ConversationAdded(object sender, ConversationManagerEventArgs e)
        {
            // Register for Conversation state changed events.
            e.Conversation.ParticipantAdded += new EventHandler<ParticipantCollectionChangedEventArgs>(Conversation_ParticipantAdded);
            e.Conversation.StateChanged += new EventHandler<ConversationStateChangedEventArgs>(Conversation_StateChanged);

            // Add a remote participant.
            if (_LyncClient.ContactManager.GetContactByUri(this.myRemoteParticipantUri) != null)
            {
                e.Conversation.AddParticipant(_LyncClient.ContactManager.GetContactByUri(this.myRemoteParticipantUri));
            }
        }
        
        //StateChanged event handler
        private void Conversation_StateChanged(object sender, ConversationStateChangedEventArgs e)
        {
            MessageBox.Show("Conversation_StateChanged: " + e.NewState);
        }

        //ParticipantAdded event handler
        private void Conversation_ParticipantAdded(object sender, ParticipantCollectionChangedEventArgs e)
        {
            if (e.Participant.IsSelf == false)
            {
                if (((Conversation)sender).Modalities.ContainsKey(ModalityTypes.InstantMessage))
                {
                    ((InstantMessageModality)e.Participant.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived += new EventHandler<MessageSentEventArgs>(InstantMessageReceived);
                    SendIM();
                }
            }
        }

        //InstantMessageReceived event handler
        private void InstantMessageReceived(object sender, MessageSentEventArgs e)
        {
            MessageBox.Show("InstantMessageReceived event");
        }

        private void ComposingCallback(IAsyncResult ar)
        { MessageBox.Show("ComposingCallback"); }

        private void SendMessageCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {
                try
                {
                    ((InstantMessageModality)_Conversation.Modalities[ModalityTypes.InstantMessage]).EndSendMessage(ar);
                }
                catch (LyncClientException lce)
                {
                    MessageBox.Show("Lync Client Exception on EndSendMessage " + lce.Message);
                }
            }
        }
    }

    //sign out and shut down
    public class ShutDown
    {
        private LyncClient _Client;
        private SignIn _SignIn;
        
        //constructor
        public ShutDown(SignIn signin)
        {
            _Client = LyncClient.GetClient();
            _SignIn = signin;
            _SignIn.SigningOut = true;      //set flag to indicate we're now signing out
            

            if (_Client.State == ClientState.SignedIn)
            {
                Object[] _asyncState = { _Client };
                _Client.BeginSignOut(SignOutCallback, _asyncState);
                MessageBox.Show("BeginSignOut method");
            }
        }

        //SignOut callback method
        // Called asynchronously by client instance after Signout()
        private void SignOutCallback(IAsyncResult ar)
        {
            MessageBox.Show("SignOutCallback method");
            _Client.EndSignOut(ar);

            _Client.ConversationManager.ConversationAdded -= new EventHandler<ConversationManagerEventArgs>(_SignIn._SendMessage.ConversationManager_ConversationAdded);
            
            if (_Client.InSuppressedMode == true)
            {
                if (_Client.State == ClientState.SignedOut)
                {
                    _Client.BeginShutdown(ShutDownCallback, null);
                    MessageBox.Show("BeginShutdown method");
                }
            }
        }

        //Shutdown callback method
        private void ShutDownCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted == true)
            {
                _Client.EndShutdown(ar);
                _Client = null;
                MessageBox.Show("ShutDownCallback method");
            }
        }
    }
}
