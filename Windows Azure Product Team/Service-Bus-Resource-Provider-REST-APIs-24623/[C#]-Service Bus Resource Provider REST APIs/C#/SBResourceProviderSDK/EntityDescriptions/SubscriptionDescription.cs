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

    [DataContract(Name = SerializationStrings.SubscriptionDescription, Namespace = SerializationStrings.Namespace)]
    public sealed class SubscriptionDescription : EntityDescription
    {
        public SubscriptionDescription(string name = null, EntityDescription parentEntity = null)
        {
            Name = name;
            ParentEntity = parentEntity;
        }

        [DataMember(Name = SerializationStrings.LockDuration, IsRequired = false, Order = 1002, EmitDefaultValue = false)]
        public TimeSpan? LockDuration { get; set; }

        [DataMember(Name = SerializationStrings.RequiresSession, IsRequired = false, Order = 1003, EmitDefaultValue = false)]
        public bool? RequiresSession { get; set; }

        [DataMember(Name = SerializationStrings.DefaultMessageTimeToLive, IsRequired = false, Order = 1004,
            EmitDefaultValue = false)]
        public TimeSpan? DefaultMessageTimeToLive { get; set; }

        [DataMember(Name = SerializationStrings.DeadLetteringOnMessageExpiration, IsRequired = false, Order = 1005,
            EmitDefaultValue = false)]
        public bool? EnableDeadLetteringOnMessageExpiration { get; set; }

        [DataMember(Name = SerializationStrings.DeadLetteringOnFilterEvaluationExceptions, IsRequired = false, Order = 1006,
            EmitDefaultValue = false)]
        public bool? EnableDeadLetteringOnFilterEvaluationExceptions { get; set; }

        [DataMember(Name = SerializationStrings.MessageCount, IsRequired = false, Order = 1009, EmitDefaultValue = false)]
        public long? MessageCount { get; set; }

        [DataMember(Name = SerializationStrings.MaxDeliveryCount, IsRequired = false, Order = 1010,
            EmitDefaultValue = false)]
        public int? MaxDeliveryCount { get; set; }

        [DataMember(Name = SerializationStrings.EnableBatchedOperations, IsRequired = false, Order = 1011,
            EmitDefaultValue = false)]
        public bool? EnableBatchedOperations { get; set; }

        [DataMember(Name = SerializationStrings.Status, IsRequired = false, Order = 1017, EmitDefaultValue = false)]
        public EntityStatus? Status { get; set; }

        [DataMember(Name = SerializationStrings.ForwardTo, IsRequired = false, Order = 1018, EmitDefaultValue = false)]
        public string ForwardTo { get; set; }

        [DataMember(Name = SerializationStrings.AutoDeleteOnIdle, IsRequired = false, Order = 1019, EmitDefaultValue = false)]
        public TimeSpan? AutoDeleteOnIdle { get; set; }

        public override bool IsActive()
        {
            return (this.Status != null && this.Status == EntityStatus.Active);
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.AppendLine(base.ToString());

            if (AutoDeleteOnIdle != null)
                toStringBuilder.AppendLine(string.Format("AutoDeleteOnIdle: {0}", this.AutoDeleteOnIdle));

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