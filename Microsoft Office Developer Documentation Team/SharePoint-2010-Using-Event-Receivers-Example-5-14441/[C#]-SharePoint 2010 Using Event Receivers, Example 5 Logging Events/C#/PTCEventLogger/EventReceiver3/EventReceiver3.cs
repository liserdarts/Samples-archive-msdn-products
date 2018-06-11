using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Text;
using System.Collections;

namespace PTCEventLogger.EventReceiver3
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver3 : SPItemEventReceiver
    {
       /// <summary>
       /// An item is being added.
       /// </summary>
       public override void ItemAdding(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item is being updated.
       /// </summary>
       public override void ItemUpdating(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item is being deleted.
       /// </summary>
       public override void ItemDeleting(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item is being checked in.
       /// </summary>
       public override void ItemCheckingIn(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item is being checked out.
       /// </summary>
       public override void ItemCheckingOut(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item is being unchecked out.
       /// </summary>
       public override void ItemUncheckingOut(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An attachment is being added to the item.
       /// </summary>
       public override void ItemAttachmentAdding(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An attachment is being removed from the item.
       /// </summary>
       public override void ItemAttachmentDeleting(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// A file is being moved.
       /// </summary>
       public override void ItemFileMoving(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item was added.
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item was updated.
       /// </summary>
       public override void ItemUpdated(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item was deleted.
       /// </summary>
       public override void ItemDeleted(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item was checked in.
       /// </summary>
       public override void ItemCheckedIn(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item was checked out.
       /// </summary>
       public override void ItemCheckedOut(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An item was unchecked out.
       /// </summary>
       public override void ItemUncheckedOut(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An attachment was added to the item.
       /// </summary>
       public override void ItemAttachmentAdded(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// An attachment was removed from the item.
       /// </summary>
       public override void ItemAttachmentDeleted(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// A file was moved.
       /// </summary>
       public override void ItemFileMoved(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

       /// <summary>
       /// A file was converted.
       /// </summary>
       public override void ItemFileConverted(SPItemEventProperties properties)
       {
           this.LogItemEventProperties(properties);
       }

        /// <summary>
        /// Log list item event properties 
        /// </summary>
        /// <param name="properties"></param>
        private void LogItemEventProperties(SPItemEventProperties properties)
        {
           //  Specify the Logs list name
           string listName = "ListItemEventLogger";

           //  Create the string builder object
           StringBuilder sb = new StringBuilder();

           //  Add properties that do not throw an exception
           sb.AppendFormat("AfterUrl: {0}\n", properties.AfterUrl);
           sb.AppendFormat("BeforeUrl: {0}\n", properties.BeforeUrl);
           sb.AppendFormat("Cancel: {0}\n", properties.Cancel);
           sb.AppendFormat("CurrentUserId: {0}\n", properties.CurrentUserId);
           sb.AppendFormat("ErrorMessage: {0}\n", properties.ErrorMessage);
           sb.AppendFormat("EventType: {0}\n", properties.EventType);
           sb.AppendFormat("ListId: {0}\n", properties.ListId);
           sb.AppendFormat("ListItemId: {0}\n", properties.ListItemId);
           sb.AppendFormat("ListTitle: {0}\n", properties.ListTitle);
           sb.AppendFormat("ReceiverData: {0}\n", properties.ReceiverData);
           sb.AppendFormat("RedirectUrl: {0}\n", properties.RedirectUrl);
           sb.AppendFormat("RelativeWebUrl: {0}\n", properties.RelativeWebUrl);
           sb.AppendFormat("SiteId: {0}\n", properties.SiteId);
           sb.AppendFormat("Status: {0}\n", properties.Status);
           sb.AppendFormat("UserDisplayName: {0}\n", properties.UserDisplayName);
           sb.AppendFormat("UserLoginName: {0}\n", properties.UserLoginName);
           sb.AppendFormat("Versionless: {0}\n", properties.Versionless);
           sb.AppendFormat("WebUrl: {0}\n", properties.WebUrl);
           sb.AppendFormat("Web: {0}\n", properties.Web);
           sb.AppendFormat("Zone: {0}\n", properties.Zone);
           sb.AppendFormat("Context: {0}\n", properties.Context);

           //  Add properties that might throw an excpetion
           try
           {
               sb.AppendFormat("ListItem: {0}\n", properties.ListItem);
           }
           catch (Exception e)
           {
               sb.AppendFormat("\nError accessing properties.ListItem:\n\n {0}", e);
           }

           //  Add AfterProperties property bag
           sb.AppendFormat("AfterProperties: {0}\n", properties.AfterProperties);
           IEnumerator afterProperties = properties.AfterProperties.GetEnumerator();
           int i = 0;
           while (afterProperties.MoveNext())
           {
               DictionaryEntry entry = (DictionaryEntry)afterProperties.Current;
               sb.AppendFormat("[{0}]: ({1}, {2})\n", i++, entry.Key, entry.Value);
           }
           sb.AppendFormat("AfterProperties.ChangedProperties: {0}\n", properties.AfterProperties.ChangedProperties);
           IEnumerator changedAfterProperties = properties.AfterProperties.ChangedProperties.GetEnumerator();
           i = 0;
           while (changedAfterProperties.MoveNext())
           {
               DictionaryEntry entry = (DictionaryEntry)changedAfterProperties.Current;
               sb.AppendFormat("[{0}]: ({1}, {2})\n", i++, entry.Key, entry.Value);
           }

           //  Add BeforeProperties property bag
           sb.AppendFormat("BeforeProperties: {0}\n", properties.BeforeProperties);
           IEnumerator beforeProperties = properties.BeforeProperties.GetEnumerator();
           i = 0;
           while (beforeProperties.MoveNext())
           {
               DictionaryEntry entry = (DictionaryEntry)beforeProperties.Current;
               sb.AppendFormat("[{0}]: ({1}, {2})\n", i++, entry.Key, entry.Value);
           }
           sb.AppendFormat("BeforeProperties.ChangedProperties: {0}\n", properties.BeforeProperties.ChangedProperties);
           IEnumerator changedBeforeProperties = properties.BeforeProperties.ChangedProperties.GetEnumerator();
           i = 0;
           while (changedBeforeProperties.MoveNext())
           {
               DictionaryEntry entry = (DictionaryEntry)changedBeforeProperties.Current;
               sb.AppendFormat("[{0}]: ({1}, {2})\n", i++, entry.Key, entry.Value);
           }

           //  Log the event to the list
           this.EventFiringEnabled = false;
           Common.LogEvent(properties.Web, listName, properties.EventType, sb.ToString());
           this.EventFiringEnabled = true;
       }
    }
}
