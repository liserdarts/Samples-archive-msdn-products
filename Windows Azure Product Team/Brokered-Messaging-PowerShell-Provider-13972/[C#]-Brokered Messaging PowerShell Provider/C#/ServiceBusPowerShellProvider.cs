//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using System.Management.Automation.Provider;
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// This PS provider navigates the ServiceBus Namespace 
    /// </summary>    
    [CmdletProvider("ServiceBusPowerShellProvider", ProviderCapabilities.None)]
    public class ServiceBusPowerShellProvider : NavigationCmdletProvider
    {
        
        NamespaceManager serviceBusNamespaceClient;

        public NamespaceManager ServiceBusNamespaceClient
        {
            get
            {
                if (this.serviceBusNamespaceClient == null)
                {
                    this.serviceBusNamespaceClient = Context.GetServiceBusNamespaceClient(this.SessionState);
                }

                return this.serviceBusNamespaceClient;
            }
        }

        /// <summary>
        /// This override creates a new drive.  
        /// </summary>        
        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            // Check if the drive object is null.
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                           new ArgumentNullException("drive"),
                           "NullDrive",
                           ErrorCategory.InvalidArgument,
                           null));

                return null;
            }

            // Check if the drive root is not null or empty.
            if (string.IsNullOrEmpty(drive.Root))
            {
                WriteError(new ErrorRecord(
                           new ArgumentException("drive.Root"),
                           "NoRoot",
                           ErrorCategory.InvalidArgument,
                           drive));

                return null;
            }

            return drive;
        }

        /// <summary>
        /// This override removes a drive from the provider.
        /// </summary>
        /// <param name="drive">The drive to remove.</param>
        /// <returns>The drive removed.</returns>
        protected override PSDriveInfo RemoveDrive(PSDriveInfo drive)
        {
            // Check if drive object is null
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                           new ArgumentNullException("drive"),
                           "NullDrive",
                           ErrorCategory.InvalidArgument,
                           drive));

                return null;
            }

            return drive;
        }

        /// <summary>
        /// This override Retrieves an item using the specified path.
        /// </summary>
        /// <param name="path">The path to the item to return.</param>
        protected override void GetItem(string path)
        {
            // Check if the specified path is a drive.
            if (this.PathIsDrive(path))
            {
                WriteItemObject(this.PSDriveInfo, path, true);
                return;
            }

            // Split the path into chunks using the ChunkPath helper method.
            WriteItemObject(this.GetItemAt(path), path, true);
        }

        protected override void SetItem(string path, object value)
        {
            throw new NotSupportedException("Updating entities is not supported therefore this provider doesn't support Set-Item");
        }

        /// <summary>
        /// This override checks to see if the specified item exists.
        /// </summary>
        /// <param name="path">The path to the item to verify.</param>
        /// <returns>True if the item is found.</returns>
        protected override bool ItemExists(string path)
        {
            // Checks if the path is null or empty.
            if (string.IsNullOrEmpty(path))
            {
                WriteError(
                    new ErrorRecord(new ArgumentException("path"), "NoPath", ErrorCategory.InvalidArgument, path));
                return false;
            }

            if (this.PathIsDrive(path))
            {
                return true;
            }
            if (this.GetItemAt(path) != null)
            {
                return true;
            }

            return false;
        }

        protected override bool IsValidPath(string path)
        {
            bool result = true;

            // Checks if the path is null or empty.
            if (string.IsNullOrEmpty(path))
            {
                result = false;
            }

            // Converts all separators in the path to a uniform one.
            path = this.NormalizePath(path);

            // split the path into individual chunks
            string[] pathChunks = path.Split(Context.PathSeparator.ToCharArray());

            foreach (string pathChunk in pathChunks)
            {
                if (string.IsNullOrEmpty(pathChunk))
                {
                    result = false;
                }
            }
            return result;
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            if (this.PathIsDrive(path))
            {
                foreach (string resource in this.GetRootResources())
                {
                    WriteItemObject(resource, path, true);
                }
            }
            else
            {
                // Splits the path into chunks using the ChunkPath helper method. 
                foreach (object obj in this.GetChildrenAt(path))
                {
                    string name = Context.PathSeparator + this.GetName(obj);
                    WriteItemObject(obj, path + name, false);
                }
            }
        }        
        
        /// <summary>
        /// This override gets child item names, returning the names of all child items.
        /// </summary>
        /// <param name="path">The root path.</param>
        /// <param name="returnAllContainers">Not used.</param>
        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            if (this.PathIsDrive(path))
            {
                foreach (string resource in this.GetRootResources())
                {
                    WriteItemObject(resource, path, true);
                }
            }
            else
            {
                foreach (object obj in this.GetChildrenAt(path))
                {
                    WriteItemObject(this.GetName(obj), path, false);
                }
            }
        }

        /// <summary>
        /// This override determines if the specified path has child items.
        /// </summary>
        /// <param name="path">The path to examine.</param>
        /// <returns>
        /// True if the specified path has child items.
        /// </returns>
        protected override bool HasChildItems(string path)
        {
            if (this.PathIsDrive(path))
            {
                return true;
            }

            string[] parts = Context.ChunkPath(this.PSDriveInfo, path);
            if (string.Compare(parts[0], "Queues", true) == 0 && parts.Length <= 1)
            {
                return true;
            }
            
            if (string.Compare(parts[0], "Topics", true) == 0 && parts.Length <= 6)
            {
                return true;
            }

            return false;
        }

        protected override void NewItem(string path, string type, object newItemValue)
        {
            string[] pathChunks = Context.ChunkPath(this.PSDriveInfo, path);
            string name = pathChunks[pathChunks.Length - 1];
            string parentPath = path.Substring(0, path.LastIndexOf('\\'));
            pathChunks = Context.ChunkPath(this.PSDriveInfo, parentPath);            

            // Find the root resource specified in the path
            switch (pathChunks.Length)
            {                
                case 1:
                    if (string.Compare(pathChunks[0], "Queues", true) == 0)
                    {
                        this.ServiceBusNamespaceClient.CreateQueue(name);
                    }
                    else if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        this.ServiceBusNamespaceClient.CreateTopic(name);
                    }

                    break;
                case 2:
                    if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        TopicDescription topic = (TopicDescription)this.GetItemAt(parentPath);
                        this.ServiceBusNamespaceClient.CreateSubscription(topic.Path, name);                     
                    }

                    break;
                case 3:
                    if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        MessagingFactory factory = Context.GetMessagingFactory(this.SessionState);
                        SubscriptionDescription subscription = (SubscriptionDescription)this.GetItemAt(parentPath);                        
                        SubscriptionClient client = factory.CreateSubscriptionClient(subscription.TopicPath, subscription.Name);
                        client.AddRule(name, new Messaging.TrueFilter());
                    }

                    break;
            }

            Context.Cache.Remove(this.GetPathTillElement(path, pathChunks.Length));
        }

        protected override void ClearItem(string path)
        {
            string[] pathChunks = Context.ChunkPath(this.PSDriveInfo, path);
            string name = pathChunks[pathChunks.Length - 1];
            string parentPath = path.Substring(0, path.LastIndexOf('\\'));
            pathChunks = Context.ChunkPath(this.PSDriveInfo, parentPath);

            switch (pathChunks.Length)
            {   
                case 1:
                    if (string.Compare(pathChunks[0], "Queues", true) == 0)
                    {
                        this.ServiceBusNamespaceClient.DeleteQueue(name);
                    }
                    else if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        this.ServiceBusNamespaceClient.DeleteTopic(name);
                    }

                    break;
                case 2:
                    if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        TopicDescription topic = (TopicDescription)this.GetItemAt(parentPath);
                        this.ServiceBusNamespaceClient.DeleteSubscription(topic.Path, name);
                    }

                    break;
                case 3:
                    if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        MessagingFactory factory = Context.GetMessagingFactory(this.SessionState);
                        SubscriptionDescription subscription = (SubscriptionDescription)this.GetItemAt(parentPath);
                        SubscriptionClient client = factory.CreateSubscriptionClient(subscription.TopicPath, subscription.Name);
                        client.RemoveRule(name);
                    }

                    break;
            }

            Context.Cache.Remove(this.GetPathTillElement(path, pathChunks.Length));
        }

        /// <summary>
        /// This override determine if the path specified is that of a container.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>True if the path specifies a container.</returns>
        protected override bool IsItemContainer(string path)
        {
            return this.HasChildItems(path);
        }

        public IEnumerable<T> GetCollectionAt<T>(string path)
        {
            return (IEnumerable<T>)GetCollectionAt(path);
        }

        public IEnumerable GetCollectionAt(string path)
        {
            string[] pathChunks = Context.ChunkPath(this.PSDriveInfo, path);
            switch (pathChunks.Length)
            {   
                case 1:
                    if (string.Compare(pathChunks[0], "Queues", true) == 0)
                    {
                        if (!this.DoesValidCacheExist(path))
                        {
                            Context.Cache[path] = new ServiceBusCollection(this.ServiceBusNamespaceClient.GetQueues());
                        }
                        
                        return Context.Cache[path].Resources;
                    }
                    else if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        if (!this.DoesValidCacheExist(path))
                        {
                            Context.Cache[path] = new ServiceBusCollection(this.ServiceBusNamespaceClient.GetTopics());                            
                        }
                    
                        return Context.Cache[path].Resources;
                    }

                    break;                    
                case 2:
                    if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        TopicDescription topic = (TopicDescription)this.GetItemAt(GetPathTillElement(path, 2));
                        if (!this.DoesValidCacheExist(topic.Path))
                        {
                            Context.Cache[topic.Path] = new ServiceBusCollection(this.ServiceBusNamespaceClient.GetSubscriptions(topic.Path));
                        }
                        
                        return Context.Cache[topic.Path].Resources;
                    }

                    break;                
                case 3:     
                    if (string.Compare(pathChunks[0], "Topics", true) == 0)
                    {
                        SubscriptionDescription subscription = (SubscriptionDescription)GetItemAt(GetPathTillElement(path, 3));
                        if (!this.DoesValidCacheExist(subscription.Name))
                        {
                            Context.Cache[subscription.Name] = 
                                new ServiceBusCollection(this.ServiceBusNamespaceClient.GetRules(subscription.TopicPath, subscription.Name));                            
                        }
            
                        return Context.Cache[subscription.Name].Resources;
                    }

                    break;
            }

            return null;
        }

        bool DoesValidCacheExist(string path)
        {
            ServiceBusCollection collection;
            if (Context.Cache.TryGetValue(path, out collection))
            {
                if (collection.IsExpired())
                {
                    WriteProgress(new ProgressRecord(0, "GetResources", "No Cache info available getting resources for path " + path));
                    return false;
                }
            }
            else
            {
                WriteProgress(new ProgressRecord(0, "GetResources", "No Cache info available getting resources for path " + path));
                return false;
            }

            return true;
        }

        public object GetItemAt(string path)
        {
            string[] pathChunks = Context.ChunkPath(this.PSDriveInfo, path);
            var env = this.GetRootResources().First(r => string.Compare(r, pathChunks[0], true) == 0);
            string parentPath = path.Substring(0, path.LastIndexOf('\\'));

            switch (pathChunks.Length)
            {
                case 1: return env;
                case 2:
                    if (String.Compare(pathChunks[0], "Queues", true) == 0)
                    {
                        return GetCollectionAt<QueueDescription>(parentPath).SingleOrDefault(t => pathChunks[1].Equals(t.Path, StringComparison.InvariantCultureIgnoreCase));
                    }
                    else if (String.Compare(env, "Topics", true) == 0)
                    {
                        return GetCollectionAt<TopicDescription>(parentPath).SingleOrDefault(t => pathChunks[1].Equals(t.Path, StringComparison.InvariantCultureIgnoreCase));
                    }

                    break;                
                case 3:
                    if (String.Compare(env, "Topics", true) == 0)
                    {
                        return GetCollectionAt<SubscriptionDescription>(parentPath).SingleOrDefault(t => pathChunks[2].Equals(t.Name, StringComparison.InvariantCultureIgnoreCase));                         
                    }

                    break;                
                case 4:
                    if (String.Compare(env, "Topics", true) == 0)
                    {
                        return GetCollectionAt<RuleDescription>(parentPath).SingleOrDefault(t => pathChunks[3].Equals(t.Name, StringComparison.InvariantCultureIgnoreCase));                                                 
                    }

                    break;
            }

            return null;
        }

        ArrayList GetChildrenAt(string path)
        {
            string[] pathChunks = Context.ChunkPath(this.PSDriveInfo, path);
            var env = GetRootResources().First(r => String.Compare(r, pathChunks[0], true) == 0);
            // "/Topics/mytopic/subscriptions/subs1/rules/rule1"
            ArrayList list = new ArrayList();
            switch (pathChunks.Length)
            {
                case 1:
                    if (String.Compare(env, "Queues", true) == 0)
                    {
                        list.AddRange(GetCollectionAt<QueueDescription>(path).ToList());
                    }
                    else if (String.Compare(env, "Topics", true) == 0)
                    {
                        list.AddRange(GetCollectionAt<TopicDescription>(path).ToList());
                    }

                    break;                
                case 2:
                    if (String.Compare(env, "Topics", true) == 0)
                    {
                        list.AddRange(GetCollectionAt<SubscriptionDescription>(path).ToList());                        
                    }

                    break;
                case 3:                    
                    if (String.Compare(env, "Topics", true) == 0)
                    {
                        list.AddRange(GetCollectionAt<RuleDescription>(path).ToList());
                    }

                    break;
            }

            return list;
        }

        public IEnumerable<string> GetRootResources()
        {
            return new string[] { "Queues", "Topics" };
        }

        /// <summary>
        /// Adapts the path, making sure the correct path separator
        /// character is used.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string NormalizePath(string path)
        {
            string result = path;

            if (!string.IsNullOrEmpty(path))
            {
                result = path.Replace("/", Context.PathSeparator);
            }

            return result;
        }

        private string GetPathTillElement(string path, int element)
        {
            string subPath = this.PSDriveInfo.Root;
            string[] chunks = Context.ChunkPath(this.PSDriveInfo, path);
            for (int i = 0; i < element; i++)
            {
                subPath += Context.PathSeparator + chunks[i];
            }

            return subPath;
        }

        private bool PathIsDrive(string path)
        {
            // Remove the drive name and first path separator.  If the 
            // path is reduced to nothing, it is a drive. Also if its
            // just a drive then there wont be any path separators
            return (string.IsNullOrEmpty(path.Replace(this.PSDriveInfo.Root, string.Empty)) ||
                    string.IsNullOrEmpty(path.Replace(this.PSDriveInfo.Root + Context.PathSeparator, string.Empty)));
        }

        string GetName(object obj)
        {
            string name = String.Empty;
            try
            {
                if (obj is TopicDescription)
                {
                    name = ((TopicDescription)obj).Path;
                }
                else if (obj is Microsoft.ServiceBus.Messaging.QueueDescription)
                {
                    name = ((Microsoft.ServiceBus.Messaging.QueueDescription)obj).Path;
                }
                else if (obj is SubscriptionDescription)
                {
                    name = ((SubscriptionDescription)obj).Name;
                }
                else if (obj is RuleDescription)
                {
                    name = ((RuleDescription)obj).Name;
                }
            }
            catch (NullReferenceException) { }

            return name;
        }
    }
}

