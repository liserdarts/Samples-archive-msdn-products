using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace ItemSplitting.BundleEventReceiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class BundleEventReceiver : SPItemEventReceiver
    {
       /// <summary>
       /// An item is being added.
       /// </summary>
       public override void ItemAdding(SPItemEventProperties properties)
       {
           //  Get a reference to the current list
           SPList list = properties.List;

           //  Get the "XBox360" content type
           SPContentType xboxContentType = list.ContentTypes["XBox360"];

           //  Get the "Kinect" content type
           SPContentType kinectContentType = list.ContentTypes["Kinect"];

           //  If any of the content types are null, it means they were not created
           if (xboxContentType == null || kinectContentType == null)
           {
               properties.Status = SPEventReceiverStatus.CancelWithError;
               properties.ErrorMessage = "All 3 content types must be associated with the list.";
               return;
           }

           //  Disable event firing so ItemAdding doesn't get called recursively
           this.EventFiringEnabled = false;

           //  Create the "XBox360" item
           SPListItem xboxItem = list.AddItem();
           xboxItem["Title"] = properties.AfterProperties["Title"] + " (X Box)";
           xboxItem["ContentTypeId"] = xboxContentType.Id;
           xboxItem.Update();

           //  Create the "Kinect" item
           SPListItem kinectItem = list.AddItem();
           kinectItem["Title"] = properties.AfterProperties["Title"] + " (Kinect)";
           kinectItem["ContentTypeId"] = kinectContentType.Id;
           kinectItem.Update();

           //  Re-enable event firing
           this.EventFiringEnabled = true;

           //  Now, cancel the creation of the "XBox360Bundle" item but don't throw an error
           properties.Status = SPEventReceiverStatus.CancelNoError;
       }
    }
}
