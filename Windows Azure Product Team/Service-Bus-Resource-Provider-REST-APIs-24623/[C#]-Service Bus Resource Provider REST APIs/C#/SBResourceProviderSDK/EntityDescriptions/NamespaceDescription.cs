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

    public enum ProvisioningState : int
    {
        [EnumMemberAttribute()]
        Activating = 1,

        [EnumMemberAttribute()]
        Active = 2,

        [EnumMemberAttribute()]
        Disabled = 3,

        [EnumMemberAttribute()]
        Removing = 4,
    }

    [DataContract(Name = SerializationStrings.TestNamespaceDescription, Namespace = SerializationStrings.Namespace)]
    public sealed class NamespaceDescription : EntityDescription
    {
        public NamespaceDescription(string name = null, string region = null, EntityDescription parentEntity = null) 
        { 
            ParentEntity = parentEntity;
            if (string.IsNullOrEmpty(name))
            {
                name = Guid.NewGuid().ToString("N");
            }
            if (string.IsNullOrEmpty(region))
            {
                region = "South Central US";
            }

            Name = name;
            Region = region;
        }

        [DataMember(Name = SerializationStrings.Region, IsRequired = false, Order = 101)]
        public string Region { get; internal set; }

        [DataMember(Name = SerializationStrings.DefaultKey, IsRequired = false, Order = 102, EmitDefaultValue = false)]
        public string DefaultKey { get; internal set; }

        [DataMember(Name = SerializationStrings.Status, IsRequired = false, Order = 103, EmitDefaultValue = false)]
        public ProvisioningState Status { get; internal set; }

        [DataMember(Name = SerializationStrings.CreatedAt, IsRequired = false, Order = 104, EmitDefaultValue = false)]
        public DateTime CreatedAt { get; internal set; }

        [DataMember(Name = SerializationStrings.AcsManagementEndpoint, IsRequired = false, Order = 105, EmitDefaultValue = false)]
        public Uri AcsManagementEndpoint { get; internal set; }

        [DataMember(Name = SerializationStrings.ServiceBusEndpoint, IsRequired = false, Order = 106, EmitDefaultValue = false)]
        public Uri ServiceBusEndpoint { get; internal set; }

        [DataMember(Name = SerializationStrings.ConnectionString, IsRequired = false, Order = 107, EmitDefaultValue = false)]
        public string ConnectionString { get; internal set; }

        public override bool IsActive()
        {
            return (this.Status == ProvisioningState.Active);
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();
            toStringBuilder.AppendLine(base.ToString());
            toStringBuilder.AppendLine(string.Format("Region: {0}", this.Region));
            toStringBuilder.AppendLine(string.Format("ConnectionString: {0}", this.ConnectionString));
            toStringBuilder.AppendLine(string.Format("ServiceBusEndpoint: {0}", this.ServiceBusEndpoint));
            toStringBuilder.AppendLine(string.Format("Status: {0}", this.Status));
            toStringBuilder.AppendLine(string.Format("AcsManagementEndpoint: {0}", this.AcsManagementEndpoint));
            toStringBuilder.AppendLine(string.Format("ServiceBusEndpoint: {0}", this.ServiceBusEndpoint));
            return toStringBuilder.ToString(); 
        }
    }
}
