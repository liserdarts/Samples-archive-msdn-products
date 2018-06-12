using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.IO;
using System.Net;

namespace WinStoreUcwaAppIM
{
    public class UcwaEventsData : UcwaResource
    {
        public UcwaEventsData(string xmlBlock) : base(xmlBlock) { }
        public UcwaEventsData(XElement xElem) : base(xElem) {}
        public UcwaEventsData(Stream xmlStream) : base(xmlStream) { }
        public string EventsHref { get { return this.Uri; } }
        public IEnumerable<string> SenderNames { get { return from res in this.SenderElements select res.Attribute("rel").Value; } }
        public IEnumerable<XElement> SenderElements {get { return xElem.Elements().Where(r => r.Name.LocalName == "sender"); }}
        public string GetSenderUri(string senderName)
        {
            var uri = GetLinkUri(senderName);
            if (string.IsNullOrEmpty(uri))
                uri = (from res in this.EmbeddedResourceElements where res.Attribute("rel").Value == senderName select res.Attribute("href").Value)
                    .FirstOrDefault();
            return uri;
        }
        public IEnumerable<XElement> GetSenderElements(string resourceName)
        {
            return (from res in this.SenderElements where res.Attribute("rel").Value == resourceName select res);
        }
        public IEnumerable<UcwaEvent> Items
        {
            get
            {
                List<UcwaEvent> eventList = new List<UcwaEvent>();
                foreach(var sender in this.SenderNames)
                {
                    eventList.AddRange(this.GetEventsBySender(sender));
                }
                return eventList.AsEnumerable();
            }
        }
        public IEnumerable<UcwaEvent> GetEventsBySender(string senderName)
        {
            List<UcwaEvent> eventList = new List<UcwaEvent>();
            //bool stop = false;
            //if (senderName.ToLower() == "conversation")
            //    stop = true;

            foreach(var sender in this.GetSenderElements(senderName))
            {
                foreach(var e in sender.Elements()) //.Where(e=>e.Name.LocalName=="added" || e.Name.LocalName == "updated" || e.Name.LocalName == "deleted" || e.Name.LocalName=="completed"))
                {
                    eventList.Add(new UcwaEvent(e, senderName, this.Uri));
                }
            }                
            return eventList.AsEnumerable();
        }
    }

    public class UcwaEvent
    {
        XElement xElem = null;
        string sender = null;
        public UcwaEvent(XElement xElem, string senderName, string eventsHref)
        {
            this.xElem = xElem;
            this.sender = senderName;
            this.EventsHref = eventsHref;
        }

        public string EventsHref { get; private set; }
        public string Type { get { return xElem.Name.LocalName; } }
        public string Name { get { return xElem.Attributes("rel").Select(s => s.Value).FirstOrDefault(); } }
        public string Uri { get { return xElem.Attributes("href").Select(s => s.Value).FirstOrDefault(); } }
        // title is an optional attribute for participant event
        //public string Title { get { return xElem.Attributes("title").Select(s => s.Value).FirstOrDefault(); } }
        public string OuterXml { get { return xElem.ToString(); } }
        public string Sender { get { return sender; } }
        public UcwaResource In
        {
            get
            {
                if (this.Resource.PropertyNames.Contains("in"))
                {
                    return new UcwaResource(this.Resource.GetPropertyValue("in"));
                }
                else
                    return null;
            }
        }
        public UcwaResource Resource
        {
            get
            {
                List<UcwaResource> resList = new List<UcwaResource>();
                foreach(var res in xElem.Elements().Where(e => e.Name.LocalName == "resource"))
                {
                    resList.Add(new UcwaResource(res));
                }
                return resList.FirstOrDefault();
            }
        }
    }

    public class UcwaEventResource
    {
        UcwaResource resource;
        public UcwaEventResource(UcwaResource res)
        {
            this.resource = res;
        }
    }

    public class UcwaResourceCommunication : UcwaResource
    {
        public UcwaResourceCommunication(UcwaResource resource) : base(resource.OuterXml) { }
        public UcwaResourceCommunication(UcwaEvent e) : base(e.Resource.OuterXml) { }
        public string ActiveModalities { get { return this.GetPropertyValue("activeModalities"); } }
        public string AudienceMessaging { get { return this.GetPropertyValue("audienceMessaging"); } }
        public string AudienceMute { get { return this.GetPropertyValue("audienceMute"); } }
        public string Created { get { return this.GetPropertyValue("created"); } }
        public string ExpirationTime { get { return this.GetPropertyValue("expirationTime"); } }
        public string Importance { get { return this.GetPropertyValue("importance"); } }
        public string ParticipantCount { get { return this.GetPropertyValue("participantCount"); } }
        public string ReadLocally { get { return this.GetPropertyValue("readLocally"); } }
        public string Recording { get { return this.GetPropertyValue("recording"); } }
        public string State { get { return this.GetPropertyValue("state"); } }
        public string Subject { get { return this.GetPropertyValue("subject"); } }
        public string ThreadId { get { return this.GetPropertyValue("threadId"); } }
        public string Uri { get { return this.GetLinkUri("self"); } }
        public string addParticipantUri { get { return this.GetLinkUri("addParticipant"); } }
        public string applicationSharingUri { get { return this.GetLinkUri("applicationSharing"); } }
        public string attendeesUri { get { return this.GetLinkUri("attendees"); } }
        public string audioVideoUri { get { return this.GetLinkUri("audioVideo"); } }
        public string dataCollaborationUri { get { return this.GetLinkUri("dataCollaboration"); } }
        public string disableAudienceMessagingUri { get { return this.GetLinkUri("disableAudienceMessaging"); } }
        public string disableAudienceMuteLockUri { get { return this.GetLinkUri("disableAudienceMuteLock"); } }
        public string enableAudienceMessagingUri { get { return this.GetLinkUri("enableAudienceMessaging"); } }
        public string enableAudienceMuteLockUri { get { return this.GetLinkUri("enableAudienceMuteLock"); } }
        public string enableLeadersUri { get { return this.GetLinkUri("leaders"); } }
        public string lobbyUri { get { return this.GetLinkUri("lobby"); } }
        public string localParticipantUri { get { return this.GetLinkUri("localParticipant"); } }
        public string messagingUri { get { return this.GetLinkUri("messaging"); } }
        public string onlineMeetingUri { get { return this.GetLinkUri("onlineMeeting"); } }
        public string phoneAudioUri { get { return this.GetLinkUri("phoneAudio"); } }

    }
    public class UcwaResourceMessaging : UcwaResource
    {
        public UcwaResourceMessaging(UcwaResource eventResource) : base(eventResource.OuterXml){}

        public UcwaResourceMessaging(UcwaEvent e) : base(e.Resource.OuterXml) { }
        public string State { get { return this.GetPropertyValue("state"); } }

        public string addMessagingUri { get { return this.GetLinkUri("addMessaging"); } }
        public string conversationUri { get { return this.GetLinkUri("conversation"); } }
        public string sendMessageUri { get { return this.GetLinkUri("sendMessage"); } }
        public string setIsTypingUri { get { return this.GetLinkUri("setIsTyping"); } }
        public string stopMessagingUri { get { return this.GetLinkUri("stopMessaging"); } }
        public string typingParticipantsUri { get { return this.GetLinkUri("typingParticipants"); } }

    }
    public class UcwaResourceMessage : UcwaResource
    {
        public UcwaResourceMessage(UcwaResource eventResource) : base(eventResource.OuterXml) { }

        public UcwaResourceMessage(UcwaEvent e) : base(e.Resource.OuterXml) { }
        public string Direction { get { return this.GetPropertyValue("direction"); } }
        public string OperationId { get { return this.GetPropertyValue("operationId"); } }
        public string Status { get { return this.GetPropertyValue("status"); } }
        public string TimeStamp { get { return this.GetPropertyValue("timeStamp"); } }

        public string PlainMessage { get { return this.LinkNames.Contains("plainMessage") ? this.GetLinkUri("plainMessage").Split(',').Last() : ""; } }
        public string HtmlMessage { get { return this.LinkNames.Contains("htmlMessage") ? this.GetLinkUri("htmlMessage").Split(',').Last() : ""; } }
        public string contactResourceUri { get { return this.GetLinkUri("contact"); } }
        public string failedDeliveryParticipantResourceUri { get { return this.GetLinkUri("failedDeliveryParticipant"); } }
        public string messagingResourceUri { get { return this.GetLinkUri("messaging"); } }
        public string participantResourceUri { get { return this.GetLinkUri("participant"); } }

    }
    public class UcwaResourceParticipant : UcwaResource
    {
        public UcwaResourceParticipant(UcwaResource eventResource) : base(eventResource.OuterXml) { }
        
        public UcwaResourceParticipant(UcwaEvent e) : base(e.Resource ==null? e.OuterXml : e.Resource.OuterXml) 
        {
            if (e.Resource == null)
                ; //debug the hack
        }

        public string Title { get { return xElem.Attributes("title").Select(s => s.Value).FirstOrDefault(); } }
        public string Anonymous { get { return this.GetPropertyValue("anonymous"); } }
        public string DisplayName { get { return this.GetPropertyValue("name"); } }
        public string Organizer { get { return this.GetPropertyValue("organizer"); } }
        public string OtherPhoneNumber { get { return this.GetPropertyValue("otherPhoneNumber"); } }
        public string Role { get { return this.GetPropertyValue("role"); } }
        public string SourceNetwork { get { return this.GetPropertyValue("sourceNetwork"); } }
        public string SipUri { get { return this.GetPropertyValue("uri"); } }
        public string WorkPhoneNumber { get { return this.GetPropertyValue("workPhoneNumber"); } }

        public string admitResourceUri { get { return this.GetLinkUri("admit"); } }
        public string contactResourceUri { get { return this.GetLinkUri("contact"); } }
        public string contactPhotoResourceUri { get { return this.GetLinkUri("contactPhoto"); } }
        public string contactPresenceResourceUri { get { return this.GetLinkUri("contactPresence"); } }
        public string conversationResourceUri { get { return this.GetLinkUri("conversation"); } }
        public string demoteResourceUri { get { return this.GetLinkUri("demote"); } }
        public string ejectResourceUri { get { return this.GetLinkUri("eject"); } }
        public string meResourceUri { get { return this.GetLinkUri("me"); } }
        public string promoteResourceUri { get { return this.GetLinkUri("promote"); } }
        public string rejectResourceUri { get { return this.GetLinkUri("reject"); } }

    }
    public class UcwaResourceConversation : UcwaResource
    {
        //public UcwaResourceConversation(UcwaEvent e) : base (e.Resource.OuterXml) {}
        public UcwaResourceConversation(UcwaResource r) : base(r.OuterXml) { }

        public string ThreadId { get { return this.GetPropertyValue("threadId"); } }
        public string State { get { return this.GetPropertyValue("state"); } }
        public string Subject { get { return this.GetPropertyValue("subject"); } }
        public bool Recording { get { return this.GetPropertyValue("recording") == "true"; } }
        public bool ReadLocally { get { return this.GetPropertyValue("readLocally") == "true"; } }
        public int ParticipantCount { get { int c; return int.TryParse(this.GetPropertyValue("participantCount"), out c) ? c : 0; } }
        public string Importance { get { return this.GetPropertyValue("importance"); } }
        public string ExpirationTime { get { return this.GetPropertyValue("expirationTime"); } }
        public bool AudienceMessaging { get { return this.GetPropertyValue("audienceMessaging") == "true"; } }
        public bool AudienceMute { get { return this.GetPropertyValue("audienceMute") == "true"; } }
        //public object ActivityModalities { get { return this.GetPropertyValue("activeModalities"); } }
        //public DateTime Created { get { return new DateTime(this.GetPropertyValue("created")); } }

        public string addparticipantUri { get { return this.GetLinkUri("addParticipant"); } }
        public string applicationSharingUri { get { return this.GetLinkUri("applicationSharing"); } }
        public string attendeesUri { get { return this.GetLinkUri("attendees"); } }
        public string audioVideoUri { get { return this.GetLinkUri("audioVideo"); } }
        public string dataCollaborationUri { get { return this.GetLinkUri("dataCollaboration"); } }
        public string disableAudienceMessagingUri { get { return this.GetLinkUri("disableAudienceMessaging"); } }
        public string disableAudienceMuteLockUri { get { return this.GetLinkUri("disableAudienceMuteLock"); } }
        public string enableAudienceMessagingUri { get { return this.GetLinkUri("enableAudienceMessaging"); } }
        public string enableAudienceMuteLockUri { get { return this.GetLinkUri("enableAudienceMuteLock"); } }
        public string leadersUri { get { return this.GetLinkUri("leaders"); } }
        public string lobbyUri { get { return this.GetLinkUri("lobby"); } }
        public string localParticipantUri { get { return this.GetLinkUri("localParticipant"); } }
        public string messagingUri { get { return this.GetLinkUri("messaging"); } }
        public string onlineMeetingUri { get { return this.GetLinkUri("onlineMeeting"); } }
        public string phoneAudioUri { get { return this.GetLinkUri("phoneAudio"); } }
    }

    public class UcwaResourceMessageInvitation : UcwaResource
    {
        public UcwaResourceMessageInvitation(UcwaResource r) : base(r.OuterXml) { }
        public UcwaResourceMessageInvitation(UcwaEvent e) : base(e.Resource.OuterXml) { }

        
        public string ThreadId { get { return this.GetPropertyValue("threadId"); } }
        public string State { get { return this.GetPropertyValue("state"); } }
        public string Subject { get { return this.GetPropertyValue("subject"); } }
        public string To { get { return this.GetPropertyValue("to"); } }
        public string Direction { get { return this.GetPropertyValue("direction"); } }
        private string message = null;
        public string Message 
        { 
            get 
            {
                if (message == null)
                {
                    var utf8Msg = this.GetLinkUri("message").Split(',').Last();
                    var htmlText = WebUtility.UrlDecode(utf8Msg);
                    message = Windows.Data.Html.HtmlUtilities.ConvertToText(htmlText);
                }
                return message; 
            } 
        }
        public string Importance { get { return this.GetPropertyValue("importance"); } }
        public string OperationId { get { return this.GetPropertyValue("operationId"); } }
        public string Status { get { return this.GetPropertyValue("status"); } }

        public string acceptUri { get { return this.GetLinkUri("accept"); } }
        public string acceptByContactUri { get { return this.GetLinkUri("acceptByContact"); } }
        public string cancelUri { get { return this.GetLinkUri("cancel"); } }
        public string conversationUri { get { return this.GetLinkUri("conversation"); } }
        public string declineUri { get { return this.GetLinkUri("decline"); } }
        public string derivedMessaging { get { return this.GetLinkUri("derivedMessaging"); } }
        public string fromUri { get { return this.GetLinkUri("from"); } }
        public string messagingUri { get { return this.GetLinkUri("messaging"); } }
        public string onBehalfOfUri { get { return this.GetLinkUri("onBehalfOf"); } }
        public string toUri { get { return this.GetLinkUri("to"); } }
        public string acceptedByParticipantUri { get { return this.GetLinkUri("acceptedByParticipant"); } }
    }
    
}
