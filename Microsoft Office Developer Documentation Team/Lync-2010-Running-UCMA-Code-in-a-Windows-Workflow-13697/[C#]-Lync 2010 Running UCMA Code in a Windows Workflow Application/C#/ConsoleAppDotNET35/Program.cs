using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Rtc;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Collaboration.Sample.Common;
using System.Threading;
using System.Configuration;

namespace ConsoleAppDotNET35
{
    public class UCProgram
    {
        string conversationSubject = "UCMA running in a workflow";
        string messageText;
        InstantMessagingCall ImCall;
        InstantMessagingFlow ImFlow;
        UserEndpoint userEndpoint;
        UCMASampleHelper helper;
        AutoResetEvent completedEvent = new AutoResetEvent(false);

        //Class entry point.
        static void Main(string[] args)
        {
            UCProgram program = new UCProgram();
        }

        //Launches an instant message. Text of the message depends on the parameter value.
        public void UcmaIM(Int32 choice)
        {
            helper = new UCMASampleHelper();
            userEndpoint = helper.CreateEstablishedUserEndpoint("SampleUser");
            Console.WriteLine("endpoint owned by " + userEndpoint.OwnerUri + " is established/registered");

            ConversationSettings conversationSettings = new ConversationSettings();
            conversationSettings.Subject = conversationSubject;
            Conversation conversation = new Conversation(userEndpoint, conversationSettings);

            ImCall = new InstantMessagingCall(conversation);
            ImCall.InstantMessagingFlowConfigurationRequested += new EventHandler<InstantMessagingFlowConfigurationRequestedEventArgs>(ImCall_InstantMessagingFlowConfigurationRequested);

            Int32 helloChoice = 1;
            Int32 goodbyeChoice = 2;
            if (choice == helloChoice)
                messageText = "hello";
            else if (choice == goodbyeChoice)
                messageText = "goodbye";
            else
                messageText = "press 1 or press 2";

            string targetURI = ConfigurationManager.AppSettings["UserURI1"];
            ImCall.BeginEstablish(targetURI, null, CallEstablishCompleted, null);
            completedEvent.WaitOne();
        }

        //Handler for the InstantMessagingCall.InstantMessagingFlowConfigurationRequested event.
        void ImCall_InstantMessagingFlowConfigurationRequested(object sender, InstantMessagingFlowConfigurationRequestedEventArgs e)
        {
            ImFlow = e.Flow;
            ImFlow.StateChanged += new EventHandler<MediaFlowStateChangedEventArgs>(ImFlow_StateChanged);
        }


        //Handler for the InstantMessagingFlow.StateChanged event.
        void ImFlow_StateChanged(object sender, MediaFlowStateChangedEventArgs e)
        {
            if (e.State == MediaFlowState.Active)
                ImFlow.BeginSendInstantMessage(messageText, SendMessageCompleted, ImFlow);
        }

        //Callback method for the InstantMessagingCall.BeginEstablish method.
        private void CallEstablishCompleted(IAsyncResult res)
        {
            ImCall.EndEstablish(res);
        }

        //Callback method for the InstantMessagingFlow.BeginSendInstantMessage method.
        private void SendMessageCompleted(IAsyncResult res)
        {
            ImFlow.EndSendInstantMessage(res);
        }
    }
}