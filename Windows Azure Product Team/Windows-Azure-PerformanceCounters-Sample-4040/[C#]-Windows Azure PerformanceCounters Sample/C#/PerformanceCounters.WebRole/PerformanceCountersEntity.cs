// -----------------------------------------------------------------------
// <copyright file="PerformanceCountersEntity.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
//     This code is licensed under the Microsoft Public License.
//     THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//     ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//     IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//     PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace PerformanceCounters.WebRole
{
    using Microsoft.WindowsAzure.StorageClient;

    /// <summary>
    /// Class used to represent a row in the WADPerformanceCountersTable.
    /// The TableServiceEntity base class has properties for the PartitionKey, 
    /// RowKey, and Timestamp.
    /// </summary>
    public class PerformanceCountersEntity : TableServiceEntity
    {
        /// <summary>
        /// Gets or sets the EventTickCount entry value.
        /// </summary>
        public long EventTickCount { get; set; }

        /// <summary>
        /// Gets or sets the DeploymentId entry value.
        /// </summary>
        public string DeploymentId { get; set; }

        /// <summary>
        /// Gets or sets the Role entry value.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the RoleInstance entry value.
        /// </summary>
        public string RoleInstance { get; set; }

        /// <summary>
        /// Gets or sets the CounterName entry value.
        /// </summary>
        public string CounterName { get; set; }

        /// <summary>
        /// Gets or sets the CounterValue entry value.
        /// </summary>
        public string CounterValue { get; set; }
    }
}