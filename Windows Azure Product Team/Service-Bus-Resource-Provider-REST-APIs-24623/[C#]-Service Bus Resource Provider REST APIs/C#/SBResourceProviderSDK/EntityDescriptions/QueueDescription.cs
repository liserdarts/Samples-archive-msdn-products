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

    [DataContract(Name = SerializationStrings.EntityStatus, Namespace = SerializationStrings.Namespace)]
    public enum EntityStatus
    {
        [EnumMember]
        Active = 0,
        [EnumMember]
        Disabled = 1,
        [EnumMember]
        Restoring = 2,
        [EnumMember]
        SendDisabled = 3,
        [EnumMember]
        ReceiveDisabled = 4
    }

    [DataContract(Name = SerializationStrings.QueueDescription, Namespace = SerializationStrings.Namespace)]
    public sealed class QueueDescription : EntityDescription
    {
        public QueueDescription(string name = null, EntityDescription parentEntity = null)
        {
            Name = name;
            ParentEntity = parentEntity; }

        [DataMember(Name = SerializationStrings.LockDuration, IsRequired = false, Order = 1002, EmitDefaultValue = false)]
        public TimeSpan? LockDuration { get; internal set; }

        [DataMember(Name = SerializationStrings.MaxSizeInMegabytes, IsRequired = false, Order = 1004,
             EmitDefaultValue = false)]
        public long? MaxSizeInMegabytes { get; internal set; }

        [DataMember(Name = SerializationStrings.RequiresDuplicateDetection, IsRequired = false, Order = 1005, EmitDefaultValue = false)]
        public bool? RequiresDuplicateDetection { get; internal set; }

        [DataMember(Name = SerializationStrings.RequiresSession, IsRequired = false, Order = 1006, EmitDefaultValue = false)]
        public bool? RequiresSession { get; internal set; }

        [DataMember(Name = SerializationStrings.DefaultMessageTimeToLive, IsRequired = false, Order = 1007, EmitDefaultValue = false)]
        public TimeSpan? DefaultMessageTimeToLive { get; internal set; }

        [DataMember(Name = SerializationStrings.DeadLetteringOnMessageExpiration, IsRequired = false, Order = 1008, EmitDefaultValue = false)]
        public bool? EnableDeadLetteringOnMessageExpiration { get; internal set; }

        [DataMember(Name = SerializationStrings.DuplicateDetectionHistoryTimeWindow, IsRequired = false, Order = 1009, EmitDefaultValue = false)]
        public TimeSpan? DuplicateDetectionHistoryTimeWindow { get; internal set; }

        [DataMember(Name = SerializationStrings.MaxDeliveryCount, IsRequired = false, Order = 1010,
            EmitDefaultValue = false)]
        public int? MaxDeliveryCount { get; internal set; }

        [DataMember(Name = SerializationStrings.EnableBatchedOperations, IsRequired = false, Order = 1011,
            EmitDefaultValue = false)]
        public bool? EnableBatchedOperations { get; internal set; }

        [DataMember(Name = SerializationStrings.SizeInBytes, IsRequired = false, Order = 1012, EmitDefaultValue = false)]
        public long? SizeInBytes { get; internal set; }

        [DataMember(Name = SerializationStrings.MessageCount, IsRequired = false, Order = 1013, EmitDefaultValue = false)]
        public long? MessageCount { get; internal set; }

        [DataMember(Name = SerializationStrings.IsAnonymousAccessible, IsRequired = false, Order = 1014, EmitDefaultValue = false)]
        public bool? IsAnonymousAccessible { get; internal set; }

        [DataMember(Name = SerializationStrings.Status, IsRequired = false, Order = 1016, EmitDefaultValue = false)]
        public EntityStatus? Status { get; internal set; }

        [DataMember(Name = SerializationStrings.ForwardTo, IsRequired = false, Order = 1017, EmitDefaultValue = false)]
        public string ForwardTo { get; internal set; }

        [DataMember(Name = SerializationStrings.AutoDeleteOnIdle, IsRequired = false, Order = 1018, EmitDefaultValue = false)]
        public TimeSpan? AutoDeleteOnIdle { get; internal set; }

        public override bool IsActive()
        {
            return (this.Status != null && this.Status == EntityStatus.Active);
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();
            
            toStringBuilder.AppendLine(base.ToString());
            
            if (MaxSizeInMegabytes != null)
                toStringBuilder.AppendLine(string.Format("MaxSizeInMegabytes: {0}", this.MaxSizeInMegabytes));

            if (AutoDeleteOnIdle != null)
                toStringBuilder.AppendLine(string.Format("AutoDeleteOnIdle: {0}", this.AutoDeleteOnIdle));

            if (IsAnonymousAccessible != null)
                toStringBuilder.AppendLine(string.Format("IsAnonymousAccessible: {0}", this.IsAnonymousAccessible));

            if (Status != null)
                toStringBuilder.AppendLine(string.Format("Status: {0}", this.Status));

            if (MessageCount != null)
                toStringBuilder.AppendLine(string.Format("MessageCount: {0}", this.MessageCount));
            
            if (MaxDeliveryCount != null)
                toStringBuilder.AppendLine(string.Format("MaxDeliveryCount: {0}", this.MaxDeliveryCount));

            toStringBuilder.AppendLine(string.Format("ForwardTo: {0}", this.ForwardTo));

            return toStringBuilder.ToString();
        }
    }
}
