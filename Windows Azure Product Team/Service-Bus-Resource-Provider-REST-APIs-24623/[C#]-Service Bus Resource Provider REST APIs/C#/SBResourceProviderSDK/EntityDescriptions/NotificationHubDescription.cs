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

    [DataContract(Name = SerializationStrings.NotificationHubDescription, Namespace = SerializationStrings.Namespace)]
    public sealed class NotificationHubDescription : EntityDescription
    {
        public NotificationHubDescription(string name = null, EntityDescription parentEntity = null)
        {
            Name = name;
            ParentEntity = parentEntity;
        }

        public override bool IsActive()
        {
            throw new NotImplementedException();
        }
    }
}
