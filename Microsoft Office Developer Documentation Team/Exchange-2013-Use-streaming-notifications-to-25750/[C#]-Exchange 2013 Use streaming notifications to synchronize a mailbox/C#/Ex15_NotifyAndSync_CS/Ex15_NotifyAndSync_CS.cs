using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Exchange101;
using Microsoft.Exchange.WebServices.Data;
using System.Xml;

// TODO: This solution uses the Exchange101 namespace and the authentication 
// methods in the Exchange 2013: Authenticate with EWS sample. 
// You must add the Authenticate with EWS sample to this solution, 
// and/or reference the Ex15_Authentication_CS.dll from this project.

namespace Microsoft.Exchange.Samples.NotifyAndSync
{
    public class NotifyAndSync
    {

        #region Private members
        private static string ContentsSyncState;
        private static string FolderSyncState;
        private static ChangeCollection<ItemChange> ItemChangeCollection;
        private static ChangeCollection<FolderChange> FolderChangeCollection;
        private static AutoResetEvent Signal;
        private static StreamingSubscription StreamingSubscription;

        // Set the root folder to sync. This must be one of the WellKnownFolderName values.
        private static FolderId RootSyncFolder = WellKnownFolderName.Drafts;

        // Set the subscription connection time, between 1 and 30 minutes.
        private static int WaitTime = 3;
        #endregion

        static ExchangeService service = Service.ConnectToService(UserDataFromConsole.GetUserData(), new TraceListener());

        static void Main(string[] args)
        {

            // Retrieve the initial contents of the rootSyncFolder.
            ContentsSyncState = SyncContents(service, null, RootSyncFolder);

            // Retrieve the initial folder hierarchy, using rootSyncFolder as the root folder.
            FolderSyncState = SyncFolders(service, null, RootSyncFolder);

            // After the initial sync, wait for notifications of new or changed items.
            StreamingSubscription = SetStreamingNotifications(service, ContentsSyncState, RootSyncFolder);

            Signal = new AutoResetEvent(false);

            // Wait for the application to exit.
            Signal.WaitOne();

        }

        // Subscribe to streaming notifications for all folder and item events in the root folder. 
        static StreamingSubscription SetStreamingNotifications(ExchangeService service, string cSyncState, FolderId rootSyncFolder)
        {
            // Subscribe to streaming notifications on the rootSyncFolder and listen 
            // for events. 
            StreamingSubscription = service.SubscribeToStreamingNotifications(
                new FolderId[] { rootSyncFolder },
                EventType.NewMail,
                EventType.Created,
                EventType.Deleted,
                EventType.Modified,
                EventType.Moved,
                EventType.Copied
                );

            StreamingSubscriptionConnection connection = new StreamingSubscriptionConnection(service, WaitTime);
            connection.AddSubscription(StreamingSubscription);
            connection.OnNotificationEvent += OnNotificationEvent;
            connection.OnDisconnect += OnDisconnect;
            connection.Open();

            Console.WriteLine("Now waiting for notifications on the following folder");
            Console.WriteLine("FolderId: {0}", rootSyncFolder);
            Console.WriteLine("--------");

            return StreamingSubscription;

        }

        // Synchronize the folders in the specified root folder.
        static string SyncFolders(ExchangeService service, string FolderSyncState, FolderId rootSyncFolder)
        {
            Console.WriteLine("Starting folder sync on the following folder");
            Console.WriteLine("FolderId: {0}", rootSyncFolder);
            Console.WriteLine("--------");

            // Get a list of all folders under the rootSyncFolder by calling SyncFolderHierarchy.
            // The folderId parameter is the root folder to synchronize. 
            // The propertySet parameter is set to IdOnly to reduce calls to the Exchange database,
            // because any additional properties result in another call to the Exchange database. 
            // The syncState parameter is set to the folder sync state. When this method is called 
            // on a new or empty mailbox, the value is null. If the method has been called before, 
            // the FolderSyncState value contains the value previously returned by the server.
            FolderChangeCollection = service.SyncFolderHierarchy(rootSyncFolder, PropertySet.IdOnly, FolderSyncState);

            // Save the sync state for use in future SyncFolderItems requests.
            // The sync state is used by the server to determine what changes to report
            // to the client.
            FolderSyncState = FolderChangeCollection.SyncState;

            // If the count of changes is zero, there are no changes to synchronize.
            if (FolderChangeCollection.Count == 0)
            {
                Console.WriteLine("There are no new folders to synchronize.");
                Console.WriteLine("--------");
            }

            // Otherwise, write all the changes included in the response 
            // to the console. 
            // For the initial synchronization, all the changes will be of type
            // ChangeType.Create.
            else
            {
                foreach (FolderChange fc in FolderChangeCollection)
                {
                    Console.WriteLine("FolderChange.ChangeType: " + fc.ChangeType.ToString());
                    Console.WriteLine("FolderChange.FolderId: " + fc.FolderId);
                    Console.WriteLine("--------");
                }

            }

            // Send the FolderChangeCollection to ProcessItems to actually retrieve the messages and changes.
            ProcessFolders(FolderChangeCollection);

            Console.WriteLine("Finished folder sync on the following folder");
            Console.WriteLine("FolderId: {0}", rootSyncFolder);
            Console.WriteLine("--------");

            return FolderSyncState;

        }

        // Synchronize the contents of the rootSyncFolder folder.
        static string SyncContents(ExchangeService service, string cSyncState, FolderId rootSyncFolder)
        {
            Console.WriteLine("--------");
            Console.WriteLine("Starting content sync on the following folder");
            Console.WriteLine("FolderId: {0}", rootSyncFolder);
            Console.WriteLine("--------");

            bool moreChangesAvailable = false;

            do
            {

                // Get a change collection of all items in the rootSyncFolder by calling SyncFolderItems 
                // repeatedly until no more changes are available.
                // The folderId parameter must be set to the same folder ID as the previous synchronization call. 
                // The propertySet parameter is set to IdOnly to reduce calls to the Exchange database,
                // because any additional properties result in another call to the Exchange database. 
                // The ignoredItemIds parameter is set to null, so no items are ignored.
                // The maxChangesReturned parameter is set to return a maximum of 500 items (512 is the default).
                // The syncScope parameter is set to Normal items, so associated items will not be returned.
                // The syncState parameter is set to the content sync state. When this method is called 
                // on a new or empty mailbox, the value is null. If the method has been called before, 
                // the cSyncState value contains the value previously returned by the server.
                ItemChangeCollection = service.SyncFolderItems(rootSyncFolder, PropertySet.IdOnly, null, 500, SyncFolderItemsScope.NormalItems, cSyncState);

                // Save the sync state for use in future SyncFolderItems requests.
                // The sync state is used by the server to determine what changes to report
                // to the client.
                cSyncState = ItemChangeCollection.SyncState;

                // If the count of changes is zero, there are no changes to synchronize.
                if (ItemChangeCollection.Count == 0)
                {
                    Console.WriteLine("There are no items to synchronize.");
                    Console.WriteLine("--------");
                }

                // Otherwise, write all the changes included in the response 
                // to the console.
                else
                {

                    foreach (ItemChange ic in ItemChangeCollection)
                    {
                        Console.WriteLine("ItemChange.ChangeType: " + ic.ChangeType.ToString());
                        Console.WriteLine("ItemChange.ItemId: " + ic.ItemId.UniqueId);
                        Console.WriteLine("--------");

                    }
                }

                // Determine whether more changes are available on the server.
                moreChangesAvailable = ItemChangeCollection.MoreChangesAvailable;

                // Send the ItemChangeCollection to ProcessItems to actually retrieve the messages and changes.
                ProcessItems(ItemChangeCollection);
            }

            while (moreChangesAvailable);

            Console.WriteLine("Finished content sync on the following folder");
            Console.WriteLine("FolderId: {0}", rootSyncFolder);
            Console.WriteLine("--------");

            return cSyncState;
        }

        // Get the items and changes in the root folder.
        static void ProcessItems(ChangeCollection<ItemChange> icc)
        {

            // Create a variable for all the new items in the ItemChangeCollection.
            var newItems = from ic in icc
                           where ic.ChangeType == ChangeType.Create
                           select ic.Item;

            // Get the properties for each item, so that they can be created on the client.
            foreach (var item in newItems)
            {
                service.LoadPropertiesForItems(newItems, PropertySet.FirstClassProperties);
                Console.WriteLine("New item, all properties loaded");
                Console.WriteLine("Subject: {0}", item.Subject);
                Console.WriteLine("ParentFolderId: {0}", item.ParentFolderId);
                Console.WriteLine("DateTimeCreated: {0}", item.DateTimeCreated);
                Console.WriteLine("--------");
                // TODO: Create the messages on the client by using the properties 
                // returned from LoadPropertiesForItems.
            }

            // Create a variable for all the deleted items in the ItemChangeCollection.
            var deleteItems = from ic in icc
                              where ic.ChangeType == ChangeType.Delete
                              select ic.ItemId;

            // Display the ItemIds to be deleted on the client.
            foreach (var item in deleteItems)
            {
                Console.WriteLine("Delete ItemId: {0}", item);
                Console.WriteLine("--------");
                // TODO: Delete messages on the client by using the ItemIds in deleteItems.   
            }

            // Create a variable for all the items with read state changes in the ItemChangeCollection.
            var readFlagChangeItems = from ic in icc
                                      where ic.ChangeType == ChangeType.ReadFlagChange
                                      select ic.ItemId;

            // Display the ItemIds that have read state changes.
            foreach (var item in readFlagChangeItems)
            {
                Console.WriteLine("Read flag change to ItemId: {0}", item);
                Console.WriteLine("--------");
                // TODO: Change the read state of the item saved on the client 
                // by using the ItemIds in readFlagChangeItems.

            }

            // Create a variable for all the updated items in the ItemChangeCollection.
            var updateItems = from ic in icc
                              where ic.ChangeType == ChangeType.Update
                              select ic.Item;

            // Display the subject for each updated item.
            foreach (var item in updateItems)
            {
                service.LoadPropertiesForItems(updateItems, PropertySet.FirstClassProperties);
                Console.WriteLine("Updated item, all properties loaded");
                Console.WriteLine("Subject: {0}", item.Subject);
                Console.WriteLine("ParentFolderId: {0}", item.ParentFolderId);
                Console.WriteLine("DateTimeCreated: {0}", item.DateTimeCreated);
                Console.WriteLine("--------");
                // TODO: Compare the properties retrieved from LoadPropertiesForItems to the 
                // properties on the client and update the client properties accordingly.
            }
        }

        // Get the folder changes in the root folder.
        static void ProcessFolders(ChangeCollection<FolderChange> fcc)
        {

            // Create a variable for all the new folders in the FolderChange ChangeCollection.
            var newFolders = from fc in fcc
                             where fc.ChangeType == ChangeType.Create
                             select fc.FolderId;

            // Get the properties for each folder, so that they can be created on the client.
            foreach (var item in newFolders)
            {
                Folder newFolder = new Folder(service);
                newFolder = Folder.Bind(service, item);
                newFolder.Load();
                Console.WriteLine("New folder, all properties loaded");
                Console.WriteLine("Display name: {0}", newFolder.DisplayName);
                Console.WriteLine("Parent Folder Id: {0}", newFolder.ParentFolderId);
                Console.WriteLine("--------");

                // TODO: Create the folder on the client by using the properties 
                // returned from Load.

                // Call SyncContents and SetStreamingNotifications on the new child folders.
                string ccSyncState = SyncContents(service, null, newFolder.Id);
                SetStreamingNotifications(service, ccSyncState, newFolder.Id);

            }

            // Create a variable for all the deleted folders in the FolderChange ChangeCollection.
            var deleteFolders = from fc in fcc
                                where fc.ChangeType == ChangeType.Delete
                                select fc.FolderId;

            // Display the FolderIds to be deleted on the client.
            foreach (var item in deleteFolders)
            {
                Console.WriteLine("Delete FolderId: {0}", item);
                Console.WriteLine("--------");
                // TODO: Delete messages on the client by using the FolderIds in deleteFolders.
            }

            // Create a variable for all the updated folders in the FolderChange ChangeCollection.
            var updateFolders = from fc in fcc
                                where fc.ChangeType == ChangeType.Update
                                select fc.Folder;

            // Display the display name for each updated folder.
            foreach (var item in updateFolders)
            {
                Folder folder = Folder.Bind(service, item.Id);
                folder.Load();
                Console.WriteLine("Updated folder, all properties loaded.");
                Console.WriteLine("Parent Folder Id: {0}", item.ParentFolderId);
                Console.WriteLine("Display name: {0}", item.DisplayName);
                Console.WriteLine("--------");
                // TODO: Compare the properties retrieved from Load to the 
                // properties on the client and update the client properties accordingly.
            }
        }

        // When the subscription connection expires, determine whether the subscription should be kept open.
        static private void OnDisconnect(object sender, SubscriptionErrorEventArgs args)
        {
            // Cast the sender as a StreamingSubscriptionConnection object.           
            StreamingSubscriptionConnection connection = (StreamingSubscriptionConnection)sender;

            // Ask the user if they want to reconnect or close the subscription. 
            ConsoleKeyInfo cki;
            Console.WriteLine("The StreamingSubscriptionConnection has expired.");
            Console.WriteLine("\r\n");
            Console.WriteLine("Do you want to reconnect to the subscription? Enter Y or N");
            Console.WriteLine("\r\n");
            while (true)
            {
                cki = Console.ReadKey(true);
                {
                    if (cki.Key == ConsoleKey.Y)
                    {
                        connection.Open();
                        Console.WriteLine("Connection open.");
                        Console.WriteLine("\r\n");
                        break;
                    }
                    else if (cki.Key == ConsoleKey.N)
                    {
                        Signal.Set();

                        // Unsubscribe from the notification subscription.
                        StreamingSubscription.Unsubscribe();

                        // Close the connection.
                        connection.Close();
                        break;
                    }
                }
            }
        }

        // Catch and display notification events.
        static void OnNotificationEvent(object sender, NotificationEventArgs args)
        {
            bool callSyncContents = false;
            bool callSyncFolders = false;

            StreamingSubscription subscription = args.Subscription;

            // Loop through all item-related events. 
            foreach (NotificationEvent notification in args.Events)
            {

                switch (notification.EventType)
                {
                    case EventType.NewMail:
                        Console.WriteLine("Notification: New mail created");
                        break;
                    case EventType.Created:
                        Console.WriteLine("Notification: Item or folder created");
                        break;
                    case EventType.Deleted:
                        Console.WriteLine("Notification: Item or folder deleted");
                        break;
                    case EventType.Modified:
                        Console.WriteLine("Notification: Item or folder modified");
                        break;
                    case EventType.Moved:
                        Console.WriteLine("Notification: Item or folder moved");
                        break;
                    case EventType.Copied:
                        Console.WriteLine("Notification: Item or folder copied");
                        break;
                }
                // Display the notification identifier. 
                if (notification is ItemEvent)
                {
                    ItemEvent itemEvent = (ItemEvent)notification;
                    Console.WriteLine("Notification ItemId: " + itemEvent.ItemId.UniqueId);
                    Console.WriteLine("--------");
                    callSyncContents = true;
                }
                else
                {
                    FolderEvent folderEvent = (FolderEvent)notification;
                    Console.WriteLine("Notification FolderId: " + folderEvent.FolderId.UniqueId);
                    Console.WriteLine("--------");
                    callSyncFolders = true;
                }
            }

            // Delay calling SyncContents and SyncFolders until all notifications have been processed. 
            // This will reduce the number of calls to the Exchange database by batching more changes
            // into a single sync call.
            if (callSyncContents == true)
            {
                ContentsSyncState = SyncContents(service, ContentsSyncState, RootSyncFolder);
            }

            if (callSyncFolders == true)
            {
                FolderSyncState = SyncFolders(service, FolderSyncState, RootSyncFolder);
            }
        }

        // Catch and display errors.
        static void OnError(object sender, SubscriptionErrorEventArgs args)
        {
            // Handle error conditions. 
            Exception e = args.Exception;
            Console.WriteLine("\n-------------Error ---" + e.Message + "-------------");
        }

    }

}
