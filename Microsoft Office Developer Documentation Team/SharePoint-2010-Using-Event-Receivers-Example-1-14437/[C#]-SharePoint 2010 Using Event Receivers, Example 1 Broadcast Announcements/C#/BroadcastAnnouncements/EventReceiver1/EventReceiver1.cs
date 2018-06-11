using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace BroasdcastAnnouncements.EventReceiver1
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver1 : SPItemEventReceiver
    {
       /// <summary>
       /// An item was added.
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           //  Get a reference to the site collection
           SPSite site = properties.OpenSite();

           //  Get a reference to the current site
           SPWeb currentWeb = properties.OpenWeb();

           //  Get a reference to the root site
           SPWeb rootWeb = site.RootWeb;

           //  Skip if the root web is the same as the current web
           if (rootWeb.Url == currentWeb.Url)
           {
               return;
           }

           //  Get the current list
           SPList currentList = properties.List;

           //  Get the announcement list on the root site
           SPList rootList = rootWeb.Lists["Announcements"];

           //  Get the list item that got added
           SPListItem currentListItem = properties.ListItem;

           //  Add the announcement item to the list on the root web
           SPListItem rootListItem = rootList.Items.Add();
           foreach (SPField field in currentList.Fields)
           {
               if (!field.ReadOnlyField)
               {
                   rootListItem[field.Id] = currentListItem[field.Id];
               }
           }

           //  Append user display name to title.
           rootListItem["Title"] += " - " + properties.UserDisplayName;

           //  Append the web url to the Body
           rootListItem["Body"] += string.Format(" This announcements was made by {0} on subweb {1}",
                                                 properties.UserLoginName, properties.WebUrl);

           rootListItem.Update();
       }
    }
}
