using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Text;

namespace PTCEventLogger.EventReceiver1
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class EventReceiver1 : SPWebEventReceiver
    {
       /// <summary>
       /// A site collection is being deleted.
       /// </summary>
       public override void SiteDeleting(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site is being deleted.
       /// </summary>
       public override void WebDeleting(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site is being moved.
       /// </summary>
       public override void WebMoving(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site is being provisioned.
       /// </summary>
       public override void WebAdding(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site collection was deleted.
       /// </summary>
       public override void SiteDeleted(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site was deleted.
       /// </summary>
       public override void WebDeleted(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site was moved.
       /// </summary>
       public override void WebMoved(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

       /// <summary>
       /// A site was provisioned.
       /// </summary>
       public override void WebProvisioned(SPWebEventProperties properties)
       {
           this.LogWebEventProperties(properties);
       }

        /// <summary>
        /// Log web event properties
        /// </summary>
        /// <param name="properties"></param>
        private void LogWebEventProperties(SPWebEventProperties properties)
        {
            //  Specify log list name
            string listName = "WebEventLogger";

            //  Create string builder object
            StringBuilder sb = new StringBuilder();

            //  Add properties the don't throw exceptions
            sb.AppendFormat("Cancel: {0}\n", properties.Cancel);
            sb.AppendFormat("ErrorMessage: {0}\n", properties.ErrorMessage);
            sb.AppendFormat("EventType: {0}\n", properties.EventType);
            sb.AppendFormat("FullUrl: {0}\n", properties.FullUrl);
            sb.AppendFormat("NewServerRelativeUrl: {0}\n", properties.NewServerRelativeUrl);
            sb.AppendFormat("ParentWebId: {0}\n", properties.ParentWebId);
            sb.AppendFormat("ReceiverData: {0}\n", properties.ReceiverData);
            sb.AppendFormat("RedirectUrl: {0}\n", properties.RedirectUrl);
            sb.AppendFormat("ServerRelativeUrl: {0}\n", properties.ServerRelativeUrl);
            sb.AppendFormat("SiteId: {0}\n", properties.SiteId);
            sb.AppendFormat("Status: {0}\n", properties.Status);
            sb.AppendFormat("UserDisplayName: {0}\n", properties.UserDisplayName);
            sb.AppendFormat("UserLoginName: {0}\n", properties.UserLoginName);
            sb.AppendFormat("WebId: {0}\n", properties.WebId);
            sb.AppendFormat("Web: {0}\n", properties.Web);

            //  Log the event to the list
            this.EventFiringEnabled = false;
            Common.LogEvent(properties.Web, listName, properties.EventType, sb.ToString());
            this.EventFiringEnabled = true;
        }
    }
}
