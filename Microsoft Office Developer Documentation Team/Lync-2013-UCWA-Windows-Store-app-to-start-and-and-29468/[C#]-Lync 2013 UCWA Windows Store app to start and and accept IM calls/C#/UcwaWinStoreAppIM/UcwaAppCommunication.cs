using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace WinStoreUcwaAppIM
{
    public class UcwaAppCommunication
    {
        UcwaApp ucwaApp;
        Dictionary<string, string> participantNames = new Dictionary<string, string>();   

        public event UcwaAppResourceStateChangedEventHandler OnResourceStateChanged;
        public event UcwaAppMessageReceivedEventhandler OnMessageReceived;
        public event UcwaAppErrorReportEventHandler OnErrorReported;
        public event UcwaAppProgressReportEventHandler OnProgressReported;
        public event UcwaAppMessagingInviteReceivedEventHandler OnMessagingInviteReceived;

        public UcwaResourceCommunication Resource { get; private set; }
        public UcwaAppCommunication(UcwaApp app)
        {
            this.ucwaApp = app;
            this.Resource = new UcwaResourceCommunication(
                this.ucwaApp.ApplicationResource.GetEmbeddedResource("communication"));
            this.ucwaApp.OnEventsReceived += new UcwaAppEventsReceivedEventHandler(ProcessEvents);            
        }
        void ProcessEvents(string sender, IEnumerable<UcwaEvent> events)
        {
            switch (sender)
            {
                case "communication":
                    ProcessCommunicationEvents(events);
                    break;
                case "conversation":
                    ProcessConversationEvents(events);
                    break;
                default:
                    break;
            }
        }
        async void ProcessConversationEvents(IEnumerable<UcwaEvent> events)
        {
            this.ShowProcessProgress("\r\nevents href='" + events.First().EventsHref);
            foreach (var e in events)
            {
                this.ShowProcessProgress(e.Name + e.Type + " event by " + e.Sender + ":");
                switch (e.Name)
                {
                    case "message":
                        var msgRes = new UcwaResourceMessage(e);
                        ShowProcessProgress("\tDirection:"+msgRes.Direction +"\r\n\tparticipantUri:"+msgRes.participantResourceUri);
                        if (msgRes.Direction.ToLower() == "incoming")
                        {
                            var status = msgRes.Status;
                            if (!participantNames.ContainsKey(msgRes.participantResourceUri))
                            {
                                var dn = await GetContactName(msgRes.contactResourceUri);
                                if (!string.IsNullOrEmpty(dn))
                                {
                                    participantNames.Add(msgRes.participantResourceUri, dn);
                                }
                            }
                            var participantName = participantNames[msgRes.participantResourceUri];
                            string timeStamp;
                            DateTime dateTimeStamp;
                            if (DateTime.TryParse(msgRes.TimeStamp, out dateTimeStamp))
                                timeStamp = dateTimeStamp.ToString();
                            else 
                                timeStamp="";
                            string msgText = ParseMessageText(msgRes);
                            NotifyReceivedMessage(status, timeStamp, msgText, participantName);
                        }
                        break;
                    case "participant":
                        ShowProcessProgress(e.Type + " participant, " + e.Uri);

                        if (e.Type.ToLower() == "deleted")
                        {
                            if (participantNames.ContainsKey(e.Uri))
                                participantNames.Remove(e.Uri);
                            else
                                ShowProcessProgress("\tNot in cache.");
                        }
                        else 
                        {
                            if (!participantNames.ContainsKey(e.Uri))
                            {
                                var pName = await GetParticipantName(e.Uri);
                                if (!string.IsNullOrEmpty(pName) && !participantNames.ContainsKey(e.Uri))
                                    participantNames.Add(e.Uri, pName);
                                else
                                    ShowProcessProgress("\tNot added to cache. " + pName);
                            }
                        }                        
                        break;
                    case "messaging":
                        messaging = new UcwaResourceMessaging(e);
                        NotifyResourceStateChange(messaging.State.ToLower(), messaging.Name);

                        ShowProcessProgress("convUri:" + messaging.conversationUri
                            + "\r\n\tsendMessageUri:" + messaging.sendMessageUri + "\r\n\tsetIsTypingUri:" + messaging.setIsTypingUri
                            + "\r\n\ttypingParticipantUri:" + messaging.typingParticipantsUri + "\r\n\tstopMessagingUri:" + messaging.stopMessagingUri
                            + "\r\n\tmessagingUri:" + messaging.Uri + "\r\n\tState:" + messaging.State
                            );

                        if (messaging.State.ToLower() == "connected")
                        {
                            var result = await this.ucwaApp.Transport.GetResourceAsync(messaging.conversationUri);
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                this.conversation = new UcwaResourceConversation(result.Resource);
                                NotifyResourceStateChange(conversation.State.ToLower(), conversation.Name);
                            }
                            else
                            {
                                this.ShowProcessProgress("Failed to GET conversation after a successful messaging invitation: " + result.Exception.Message);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        string eventMessageFormat="EVENT: {0} {1} sent from {2} ";
        UcwaResourceMessaging messaging;
        UcwaResourceConversation conversation;
        UcwaResourceMessageInvitation messagingInvite;
        string messagingInviteAcceptUri, messagingInviteDeclineUri;
        async void ProcessCommunicationEvents(IEnumerable<UcwaEvent> events)
        {
            foreach (var e in events)
            {
                ShowProcessProgress(string.Format(eventMessageFormat, e.Name, e.Type, e.Sender));
                switch (e.Name)
                {
                    case "communication":
                        this.Resource = new UcwaResourceCommunication(e.Resource);
                        ShowProcessProgress("\tResource: "+ this.Resource.Name + "\r\n\tState =" + this.Resource.State + "\r\n\t" + this.Resource.ThreadId);
                        break;
                    case "messagingInvitation":
                        this.messagingInvite = new UcwaResourceMessageInvitation(e);
                        NotifyResourceStateChange(messagingInvite.State.ToLower(), messagingInvite.Name);

                        //ShowProcessProgress("\tResource: " + this.messagingInvite.Name + "\r\n\tState: " + this.messagingInvite.State
                        //    + "\r\n\tDirection: " + messagingInvite.Direction + "\r\n\tThreadId: " + messagingInvite.ThreadId
                        //    + "\r\n\tmessagingUri:" + messagingInvite.messagingUri + "\r\n\tOperationId:" + messagingInvite.OperationId
                        //    + "\r\n\ttoUri:" + messagingInvite.toUri + "\r\n\tfromUri: " + messagingInvite.fromUri
                        //    + "\r\n\tStatus: " + messagingInvite.Status + "" 
                        //    );

                        if (messagingInvite.Direction.ToLower() == "incoming")
                        {
                            // Must cache the following Uris, otherwise, they may not be available on later messagingInvitation events
                            if (messagingInvite.acceptUri != null) messagingInviteAcceptUri = messagingInvite.acceptUri;
                            if (messagingInvite.declineUri != null) messagingInviteDeclineUri = messagingInvite.declineUri;
                            if (messagingInvite.ThreadId != null) threadId = messagingInvite.ThreadId;
                            if (messagingInvite.ContainsResource("from"))
                            {
                                var from = new UcwaResourceParticipant(
                                    await messagingInvite.GetContainedResource("from", ucwaApp.Transport));
                                NotifyMessagingInvite(this.messagingInvite.Subject, this.messagingInvite.Importance,
                                        from.DisplayName, this.messagingInvite.Message, this.messagingInvite.ThreadId);
                           }
                            else
                                ShowProcessProgress("WARN: messagingInvite does not specify valid 'from' resource");
                        }
                        break;
                    case "conversation":
                        if (e.Type.ToLower() == "deleted")
                            NotifyResourceStateChange("deleted", e.Name);
                        else
                            if (e.Resource != null)
                            {
                                var conv = new UcwaResourceConversation(e.Resource);
                                {
                                    this.conversation = conv;
                                    NotifyResourceStateChange(conversation.State.ToLower(), conversation.Name);
                                }
                                ShowProcessProgress("\tThreadId: " + conv.ThreadId + "\r\n\tUri: " + conv.Uri + "" + conv.State
                                    + "\r\n\tParticipanCount: " + conv.ParticipantCount + "\r\n\tmessagingUri: " + conv.messagingUri);
                            }
                            else
                                this.ShowProcessProgress("WARN: conversation added event does not contain the corresponding resource.");
                        break;
                    default:
                        break;
                }
            }
        }
        async Task<string> GetParticipantName(string participantResourceUri)
        {
            if (!string.IsNullOrEmpty(participantResourceUri))
            {
                var res = await this.ucwaApp.Transport.GetResourceAsync(participantResourceUri);
                if (res.StatusCode == HttpStatusCode.OK)
                    return await GetParticipantName(new UcwaResourceParticipant(res.Resource));
            }
            return null;
        }
        async Task<string> GetParticipantName(UcwaResourceParticipant p)
        {
            if (p != null)
            {
                return await GetContactName(p.contactResourceUri);
            }
            return null;
        }
        async Task<string> GetContactName(string contactResourceUri)
        {
            if (!string.IsNullOrEmpty(contactResourceUri))
            {
                var res = await this.ucwaApp.Transport.GetResourceAsync(contactResourceUri);
                if (res.StatusCode == HttpStatusCode.OK)
                    return res.Resource.GetPropertyValue("name");
            }
            return null;
        }
        string ParseMessageText(UcwaResourceMessage msgRes)
        {
            if (msgRes.HtmlMessage != null)
                return Windows.Data.Html.HtmlUtilities.ConvertToText(
                   WebUtility.UrlDecode(msgRes.HtmlMessage));           
            return WebUtility.UrlDecode(msgRes.PlainMessage);
        }
        async void NotifyMessagingInvite(string subject, string importance, string fromUri, string message, string threadId)
        {
            if (OnMessagingInviteReceived != null)
                await UcwaAppUtils.DispatchEventToUI(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnMessagingInviteReceived(subject, importance, fromUri, message, threadId); }));
        }
        async void ShowMissingEvents(string sender)
        {
            if (this.OnErrorReported != null)
                await UcwaAppUtils.DispatchEventToUI(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnErrorReported(new Exception("Events by " + sender + "ignored.")); }));
        }
        async void ShowProcessProgress(string msg)
        {
            if (this.OnProgressReported != null)
            {
                await UcwaAppUtils.DispatchEventToUI(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnProgressReported(msg); }));
            }

        }
        async void NotifyResourceStateChange(string state, string resourceName)
        {
            if (OnResourceStateChanged != null)
            {
                await UcwaAppUtils.DispatchEventToUI(CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnResourceStateChanged(state, resourceName); }));
            }
        }
        async void NotifyReceivedMessage(string status, string timestamp, string plainMsg, string participant)
        {
            if (OnMessageReceived != null)
            {
                await UcwaAppUtils.DispatchEventToUI(CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnMessageReceived(status, timestamp, plainMsg, participant); }));
            }
        }
        async void ShowEventsBySener(string sender, UcwaEvent eventData)
        {
            if (this.OnProgressReported != null)
            {
                string msg = "Events raised by " + sender + ":\r\n";
                msg += "\tName=" + eventData.Name + " Type=" + eventData.Type + "\r\n";
                msg += "\tUri=" + eventData.Uri + "\r\n";
                await UcwaAppUtils.DispatchEventToUI(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnProgressReported(msg); }));
            }
        }
        public async Task<UcwaAppOperationResult> AcceptInvite()
        {
            if (string.IsNullOrEmpty(messagingInviteAcceptUri))
                return new UcwaAppOperationResult(HttpStatusCode.NotFound, new Exception("Invalid messagingInvite Accept Uri"));
            return await this.ucwaApp.Transport.PostResourceAsync(messagingInviteAcceptUri, null as System.Net.Http.HttpContent);
        }
        public async Task<UcwaAppOperationResult> DeclineInvite()
        {
            if (string.IsNullOrEmpty(messagingInviteDeclineUri))
                return new UcwaAppOperationResult(HttpStatusCode.NotFound, new Exception("Invalid messagingInvite Decline Uri"));
            return await this.ucwaApp.Transport.PostResourceAsync(messagingInviteDeclineUri, null as System.Net.Http.HttpContent);
        }
        public async Task<UcwaAppOperationResult> SendMessage(string msg = "Hello, this is a test.", string contentType = "text/plain")
        {
            if (operationId == null) operationId = Guid.NewGuid().ToString();
            UcwaAppOperationResult result = null;
            if (messaging!= null && messaging.sendMessageUri != null)
            {
                var uri = messaging.sendMessageUri + "?OperationId="+  operationId;
                result = await this.ucwaApp.Transport.PostResourceAsync(uri, msg, contentType);
            }
            if (messaging != null && messaging.sendMessageUri == null)
            {
                result = new UcwaAppOperationResult(HttpStatusCode.BadRequest, new Exception("null sendMessageUri received/cached."));
            }
            else if (messaging == null)
            {
                result = new UcwaAppOperationResult(HttpStatusCode.BadRequest, new Exception("null messaging received/cached."));
            }
            
            return result;
        }
        string startMessagingInputFormat = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<input xmlns=\"http://schemas.microsoft.com/rtc/2012/03/ucwa\">" +
            "  <property name=\"operationId\">{0}</property>" +
            "  <property name=\"to\">{1}</property>" +
            "  <property name=\"importance\">{2}</property>" +
            "  <property name=\"subject\">{3}</property>" +
            "  <property name=\"threadId\">{4}</property>" +
            "  <link rel=\"message\" href=\"data:text/plain,{5}\" />" +
            "</input>";

        string operationId = Guid.NewGuid().ToString();
        string threadId = Guid.NewGuid().ToString();
        public async Task<UcwaAppOperationResult> StartIM(string toUri, string subject, string importance = "normal")
        {
            var startMessagingUri = this.Resource.GetLinkUri("startMessaging");
            
            ShowProcessProgress("startMessaging Uri = " + startMessagingUri);
            string firstMessage = "Hi!";
            string requestInput = string.Format(startMessagingInputFormat, operationId, toUri, importance, subject, threadId, firstMessage);
            var result = await this.ucwaApp.Transport.PostResourceAsync(startMessagingUri, requestInput);
            if (result.StatusCode != HttpStatusCode.OK && result.StatusCode != HttpStatusCode.NoContent && result.StatusCode != HttpStatusCode.Created)
                ShowProcessProgress("startMessaging request failed. \r\n" + result.Resource.OuterXml);
            return result;
        }
        public async Task<UcwaAppOperationResult> StopIM()
        {

            if (messaging != null && messaging.stopMessagingUri != null)
            {
                var result = await this.ucwaApp.Transport.PostResourceAsync(messaging.stopMessagingUri, null as System.Net.Http.HttpContent);
                return result;
            }
            else
                return new UcwaAppOperationResult(HttpStatusCode.NotFound, new Exception("stopMessagingUri is null."));
        }
    }
}
