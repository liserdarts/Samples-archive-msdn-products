using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WinStoreUcwaAppIM
{
    public class UcwaAppOnlineMeetings
    {
        UcwaApp ucwaApp;
        UcwaResource _resMyOnlineMeetings;
        public UcwaAppOnlineMeetings(UcwaApp app)
        {
            this.ucwaApp = app;
            this.ucwaApp.OnEventNotificationsReceived += ProcessEventsData;
            //this.ucwaApp.OnEventsReceived += new UcwaAppEventsReceivedEventHandler()
            this.ucwaApp.OnErrorReported += ProcessError;
        }
        
        void ProcessEventsData(UcwaEventsData eventsData)
        {

        }
        void ProcessError(Exception ex)
        {

        }
        public async Task<string> CreateMeeting()
        {
            // create onlineMeetings
            var meetingUri = this.ucwaApp.ApplicationResource.GetEmbeddedResourceUri("onlineMeetings");
            var myOnlineMeetingsUri = this.ucwaApp.ApplicationResource.GetEmbeddedResource("onlineMeetings").GetEmbeddedResourceUri("myOnlineMeetings");
            var meetingInput = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                     "<input xmlns=\"http://schemas.microsoft.com/rtc/2012/03/ucwa\">" +
                //" <property name=\"accessLevel\">Locked</property>" +
                     " <propertyList name=\"attendees\">" +
                     "   <item>sip:kdeding@microsoft.com</item>" +
                     "   <item>sip:toshm@metio.ms</item>" +
                     " </propertyList>" +
                     " <property name=\"automaticLeaderAssignment\">Disabled</property>" +
                     " <property name=\"description\">We'll be meeting to review the sales numbers for this past quarter and discuss projections for the next two quarters.</property>" +
                //" <property name=\"entryExitAnnouncement\">Unsupported</property>"+
                //" <property name=\"expirationTime\">2014-03-09T16:34:29.9597697-07:00</property>"+
                //" <propertyList name=\"leaders\">"+
                //"   <item>sip:kellyk@metio.ms</item>"+
                //" </propertyList>"+
                //" <property name=\"lobbyBypassForPhoneUsers\">Disabled</property>"+
                //" <property name=\"phoneUserAdmission\">Disabled</property>"+
                     " <property name=\"subject\">Test myOnlineMeetings</property>" +
                     "</input>";
            var opResult = await this.ucwaApp.Transport.PostResourceAsync(myOnlineMeetingsUri, meetingInput);
            string joinUrl = null;
            if (opResult.StatusCode == HttpStatusCode.OK)
            {
                this._resMyOnlineMeetings = opResult.Resource;
                joinUrl = _resMyOnlineMeetings.GetPropertyValue("joinUrl");  // used by attendees to join the meeting with a PUT request
            }
            return joinUrl;
        }
    }
}
