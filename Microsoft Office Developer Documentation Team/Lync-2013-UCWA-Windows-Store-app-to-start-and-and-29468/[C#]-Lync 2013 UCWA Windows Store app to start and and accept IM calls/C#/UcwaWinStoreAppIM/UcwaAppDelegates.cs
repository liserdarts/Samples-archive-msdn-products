using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Windows.Foundation;

namespace WinStoreUcwaAppIM
{
    public delegate void UcwaAppEventNotificationsReceivedEventHandler(UcwaEventsData events);
    public delegate void UcwaAppEventChannelClosedEventHandler(AsyncStatus status);
    public delegate void UcwaAppEventsReceivedEventHandler(string sender, IEnumerable<UcwaEvent> events);
    public delegate void UcwaAppProgressReportEventHandler(string message, HttpStatusCode status = HttpStatusCode.OK);
    public delegate void UcwaAppErrorReportEventHandler(Exception e);
    //public delegate void UcwaAppEventsErredHandler(string message, HttpStatusCode status);
    //public delegate void UcwaAppHttpStatusReportHandler(HttpWebResponse response);

    public delegate void UcwaAppResourceStateChangedEventHandler(string state, string resource);
    public delegate void UcwaAppMessageReceivedEventhandler(string status, string timestamp, string plainMsg, string htmlMsg);
    public delegate void UcwaAppMessagingInviteReceivedEventHandler(string subject, string importance, string fromUri, string firstMessage, string threadId);
}
