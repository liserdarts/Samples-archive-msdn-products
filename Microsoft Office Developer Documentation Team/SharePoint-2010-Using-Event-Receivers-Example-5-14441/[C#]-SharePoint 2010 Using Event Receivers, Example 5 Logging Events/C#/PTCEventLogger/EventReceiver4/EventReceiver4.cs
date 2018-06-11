using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Text;

namespace PTCEventLogger.EventReceiver4
{
    /// <summary>
    /// List Email Events
    /// </summary>
    public class EventReceiver4 : SPEmailEventReceiver
    {
       /// <summary>
       /// The list received an e-mail message.
       /// </summary>
       public override void EmailReceived(SPList list, SPEmailMessage emailMessage, String receiverData)
       {
           //  Specify log list name
           string listName = "ListEmailEventLogger";

           //  Create the string builder object
           StringBuilder sb = new StringBuilder();

           //  Add the email message properties
           sb.AppendFormat("From:\t {0}\n", emailMessage.Sender);
           sb.AppendFormat("To:\t {0}\n", emailMessage.Headers["To"]);
           sb.AppendFormat("Subject:\t {0}\n", emailMessage.Headers["Subject"]);
           sb.AppendFormat("Body:\t {0}\n", emailMessage.PlainTextBody);
           
           //  Log the event to the list
           Common.LogEvent(list.ParentWeb, listName, SPEventReceiverType.EmailReceived, sb.ToString());
       }
    }
}
