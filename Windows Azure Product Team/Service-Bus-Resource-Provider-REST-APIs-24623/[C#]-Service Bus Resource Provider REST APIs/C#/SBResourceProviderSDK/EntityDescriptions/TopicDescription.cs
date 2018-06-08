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
    using System.Text;

    [DataContract(Name = SerializationStrings.TopicDescription, Namespace = SerializationStrings.Namespace)]
    public sealed class TopicDescription : EntityDescription
    {
        public TopicDescription(string name = null, EntityDescription parentEntity = null)
        {
            Name = name;
            ParentEntity = parentEntity;
        }

        [DataMember(Name = SerializationStrings.DefaultMessageTimeToLive, IsRequired = false, Order = 1002,
            EmitDefaultValue = false)]
        public TimeSpan? DefaultMessageTimeToLive { get; set; }

        [DataMember(Name = SerializationStrings.MaxSizeInMegabytes, IsRequired = false, Order = 1004,
            EmitDefaultValue = false)]
        public long? MaxSizeInMegabytes { get; set; }

        [DataMember(Name = SerializationStrings.RequiresDuplicateDetection, IsRequired = false, Order = 1005,
            EmitDefaultValue = false)]
        public bool? RequiresDuplicateDetection { get; set; }

        [DataMember(Name = SerializationStrings.DuplicateDetectionHistoryTimeWindow, IsRequired = false, Order = 1006,
            EmitDefaultValue = false)]
        public TimeSpan? DuplicateDetectionHistoryTimeWindow { get; set; }

        [DataMember(Name = SerializationStrings.EnableBatchedOperations, IsRequired = false, Order = 1007,
            EmitDefaultValue = false)]
        public bool? EnableBatchedOperations { get; set; }

        [DataMember(Name = SerializationStrings.SizeInBytes, IsRequired = false, Order = 1008, EmitDefaultValue = false)]
        public long? SizeInBytes { get; set; }

        [DataMember(Name = SerializationStrings.FilteringMessagesBeforePublishing, IsRequired = false, Order = 1009, EmitDefaultValue = false)]
        public bool? FilteringMessagesBeforePublishing { get; set; }

        [DataMember(Name = SerializationStrings.IsAnonymousAccessible, IsRequired = false, Order = 1010, EmitDefaultValue = false)]
        public bool? IsAnonymousAccessible { get; set; }

        [DataMember(Name = SerializationStrings.Status, IsRequired = false, Order = 1012, EmitDefaultValue = false)]
        public EntityStatus? Status { get; set; }

        [DataMember(Name = SerializationStrings.ForwardTo, IsRequired = false, Order = 1013, EmitDefaultValue = false)]
        public string ForwardTo { get; set; }

        [DataMember(Name = SerializationStrings.SubscriptionCount, IsRequired = false, Order = 1025, EmitDefaultValue = false)]
        public int SubscriptionCount { get; set; }

        [DataMember(Name = SerializationStrings.AutoDeleteOnIdle, IsRequired = false, Order = 1026, EmitDefaultValue = false)]
        public TimeSpan? AutoDeleteOnIdle { get; set; }

        public override bool IsActive()
        {
            return (this.Status != null && this.Status == EntityStatus.Active);
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.AppendLine(base.ToString());
            toStringBuilder.AppendLine(string.Format("MaxSizeInMegabytes: {0}", this.MaxSizeInMegabytes));
            
            if (AutoDeleteOnIdle != null)
                toStringBuilder.AppendLine(string.Format("AutoDeleteOnIdle: {0}", this.AutoDeleteOnIdle));

            if (IsAnonymousAccessible != null)
                toStringBuilder.AppendLine(string.Format("IsAnonymousAccessible: {0}", this.IsAnonymousAccessible));

            if (Status != null)
                toStringBuilder.AppendLine(string.Format("Status: {0}", this.Status));

            toStringBuilder.AppendLine(string.Format("SubscriptionCount: {0}", this.SubscriptionCount));
            toStringBuilder.AppendLine(string.Format("ForwardTo: {0}", this.ForwardTo));

            return toStringBuilder.ToString();
        }
    }
}
