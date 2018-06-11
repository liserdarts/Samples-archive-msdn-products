using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Text;

namespace PTCEventLogger.EventReceiver2
{
    /// <summary>
    /// List Events
    /// </summary>
    public class EventReceiver2 : SPListEventReceiver
    {
       /// <summary>
       /// A field was added.
       /// </summary>
       public override void FieldAdded(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A field is being added.
       /// </summary>
       public override void FieldAdding(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A field was removed.
       /// </summary>
       public override void FieldDeleted(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A field is being removed.
       /// </summary>
       public override void FieldDeleting(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A field was updated.
       /// </summary>
       public override void FieldUpdated(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A field is being updated.
       /// </summary>
       public override void FieldUpdating(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A list is being added.
       /// </summary>
       public override void ListAdding(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A list is being deleted.
       /// </summary>
       public override void ListDeleting(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A list was added.
       /// </summary>
       public override void ListAdded(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// A list was deleted.
       /// </summary>
       public override void ListDeleted(SPListEventProperties properties)
       {
           this.LogListEventProperties(properties);
       }

       /// <summary>
       /// Log list event properties
       /// </summary>
       /// <param name="properties"></param>
        private void LogListEventProperties(SPListEventProperties properties)
        {
            //  Specify log list name
            string listName = "ListEventLogger";

            //  Create the string builder object
            StringBuilder sb = new StringBuilder();

            //  Add properties the don't throw exceptions
            sb.AppendFormat("Cancel: {0}\n", properties.Cancel);
            sb.AppendFormat("ErrorMessage: {0}\n", properties.ErrorMessage);
            sb.AppendFormat("EventType: {0}\n", properties.EventType);
            sb.AppendFormat("FeatureId: {0}\n", properties.FeatureId);
            sb.AppendFormat("FieldName: {0}\n", properties.FieldName);
            sb.AppendFormat("FieldXml: {0}\n", properties.FieldXml);
            sb.AppendFormat("ListId: {0}\n", properties.ListId);
            sb.AppendFormat("ListTitle: {0}\n", properties.ListTitle);
            sb.AppendFormat("ReceiverData: {0}\n", properties.ReceiverData);
            sb.AppendFormat("RedirectUrl: {0}\n", properties.RedirectUrl);
            sb.AppendFormat("SiteId: {0}\n", properties.SiteId);
            sb.AppendFormat("Status: {0}\n", properties.Status);
            sb.AppendFormat("TemplateId: {0}\n", properties.TemplateId);
            sb.AppendFormat("UserDisplayName: {0}\n", properties.UserDisplayName);
            sb.AppendFormat("UserLoginName: {0}\n", properties.UserLoginName);
            sb.AppendFormat("WebId: {0}\n", properties.WebId);
            sb.AppendFormat("WebUrl: {0}\n", properties.WebUrl);
            sb.AppendFormat("Web: {0}\n", properties.Web);
            sb.AppendFormat("List: {0}\n", properties.List);

            //  Add properties that might throw an exception
            try
            {
                sb.AppendFormat("Field: {0}\n", properties.Field);
            }
            catch (Exception e)
            {
                sb.AppendFormat("\nError accessing properties.Field:\n\n {0}", e);
            }

            //  Log the event to the list
            this.EventFiringEnabled = false;
            Common.LogEvent(properties.Web, listName, properties.EventType, sb.ToString());
            this.EventFiringEnabled = true;
        }
    }
}
