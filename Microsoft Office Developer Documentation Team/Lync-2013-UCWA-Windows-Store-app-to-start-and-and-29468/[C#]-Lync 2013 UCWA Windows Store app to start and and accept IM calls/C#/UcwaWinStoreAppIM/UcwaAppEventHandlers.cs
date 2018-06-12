using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Windows.UI.Core;
namespace WinStoreUcwaAppIM
{
    public class UcwaAppEventHandlers
    {
        public UcwaAppEventNotificationsReceivedEventHandler OnEventNotificationsReceived;   // delegate for parsing the events on the calling (UI) thread
        public UcwaAppProgressReportEventHandler OnProgressReported; // delegate for reporting progress on the calling UI thread
        public UcwaAppErrorReportEventHandler OnErrorReported; // delegate for reporting errors on the callling UI thread

        public virtual void ForwardEventNotificationsReceived(UcwaEventsData events)
        {
            if (OnEventNotificationsReceived != null)
            {
                OnEventNotificationsReceived(events);
            }
        }
        public virtual void ForwardReportedProgress(string msg, HttpStatusCode statusCode)
        {
            if (OnProgressReported != null)
                OnProgressReported(msg, statusCode);
        }
        public virtual void ForwardReportedErrors(Exception e)
        {
            if (OnErrorReported != null)
                OnErrorReported(e);
        }
        protected virtual async void DispatchToUIThreadReceivedEventNotifications(UcwaEventsData events)
        {
            if (this.OnEventNotificationsReceived != null)
                await UcwaAppUtils.DispatchEventToUI(CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { this.OnEventNotificationsReceived(events); }));

            //foreach (var sender in eventsData.SenderNames)
            //{
            //    if (OnEventsReceived != null)
            //        await UcwaAppUtils.DispatchEventToUI(CoreDispatcherPriority.Normal,
            //            new DispatchedHandler(() => { OnEventsReceived(sender, eventsData.GetEventsBySender(sender)); }));
            //}
        }
        protected virtual async void DispatchToUIThreadErrorReport(Exception e)
        {
            if (OnErrorReported != null)
                await UcwaAppUtils.DispatchEventToUI(CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { OnErrorReported(e); }));
        }
        protected virtual async void DispatchToUIThreadProgressReport(string msg, HttpStatusCode status)
        {
            if (OnProgressReported != null)
                await UcwaAppUtils.DispatchEventToUI(CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() => { OnProgressReported(msg, status); }));
        }

    }
}
