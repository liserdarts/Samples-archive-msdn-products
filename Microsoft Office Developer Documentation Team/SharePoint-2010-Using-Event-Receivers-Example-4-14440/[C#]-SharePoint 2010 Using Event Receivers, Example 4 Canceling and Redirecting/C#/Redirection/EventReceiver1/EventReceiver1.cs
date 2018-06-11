using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Redirection.EventReceiver1
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class EventReceiver1 : SPWebEventReceiver
    {
       /// <summary>
       /// A site is being moved.
       /// </summary>
       public override void WebMoving(SPWebEventProperties properties)
       {
           properties.Status = SPEventReceiverStatus.CancelWithRedirectUrl;
           properties.RedirectUrl = properties.Web.ServerRelativeUrl + "/_layouts/settings.aspx";
       }

       public override void WebAdding(SPWebEventProperties properties)
       {
           properties.Status = SPEventReceiverStatus.CancelWithRedirectUrl;
           properties.RedirectUrl = properties.Web.ServerRelativeUrl + "/_layouts/settings.aspx";
       }
    }
}
