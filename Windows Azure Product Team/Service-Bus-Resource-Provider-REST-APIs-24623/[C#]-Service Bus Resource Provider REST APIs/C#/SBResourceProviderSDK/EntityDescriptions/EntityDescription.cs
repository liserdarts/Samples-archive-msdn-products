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

namespace Microsoft.Samples.ServiceBus.ResourceProviderSDK
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class EntityDescription
    {
        /// <summary>
        /// Using ParentEntity - we preserve the Entity Hierarchy.
        /// Namespace.ParentEntity is Set to 'Null' - as ServiceBus Namespace is Root of the ServiceBus Rest Model
        /// Topic.ParentEntity = Namespace
        /// Subscription.ParentEntity = Topic
        /// Queue.ParentEntity = Namespace
        /// NotificationHub.ParentEntity = Namespace
        /// </summary>
        private EntityDescription parentEntity = null;
        public EntityDescription ParentEntity
        {
            get { return parentEntity; }
            internal set { parentEntity = value; }
        }

        private string name = string.Concat("Entity", Guid.NewGuid().ToString("N"));
        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }
        
        public EntityDescription(EntityDescription parentEntity = null)
        {
            this.ParentEntity = parentEntity;
        }

        public abstract bool IsActive();

        public new virtual string ToString()
        {
            return string.Format("Name: {0}", this.Name);
        }
    }
}
