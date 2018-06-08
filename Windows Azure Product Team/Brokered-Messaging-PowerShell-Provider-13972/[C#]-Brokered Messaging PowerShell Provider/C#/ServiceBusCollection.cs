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
    using System.Collections.Generic;

    /// <summary>
    /// This class is intented to cache entities for 1 minute so the shell is more responsive
    /// </summary>
    public class ServiceBusCollection
    {
        IEnumerable<Object> resources;
        DateTime setTime;

        public ServiceBusCollection(IEnumerable<Object> collection)
        {
            setTime = DateTime.Now;
            resources = collection;
        }

        public IEnumerable<Object> Resources
        {
            get { return resources; }
        }

        public bool IsExpired()
        {
            return setTime < DateTime.Now.AddMinutes(-1);
        }
    }
}
