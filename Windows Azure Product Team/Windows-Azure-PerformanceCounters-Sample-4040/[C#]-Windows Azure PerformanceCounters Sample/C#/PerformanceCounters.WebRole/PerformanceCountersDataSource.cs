// -----------------------------------------------------------------------
// <copyright file="PerformanceCountersDataSource.cs" company="Microsoft">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.StorageClient;

    /// <summary>
    /// This class is used as a data source for a web control.
    /// </summary>
    public class PerformanceCountersDataSource
    {
        /// <summary>
        /// The cloud storage account, cached for performance.
        /// </summary>
        private static CloudStorageAccount storageAccount;

        /// <summary>
        /// The table service context for the Diagnostics tables.
        /// </summary>
        private static TableServiceContext serviceContext;

        /// <summary>
        /// The role id for this instance.
        /// </summary>
        private static string roleId;

        /// <summary>
        /// Initializes static members of the PerformanceCountersDataSource class.
        /// </summary>
        static PerformanceCountersDataSource()
        {
            storageAccount = CloudStorageAccount.Parse(
                WebRole.GetConfigurationSettingValue(WebRole.WADConnectionString));

            CloudTableClient cloudTableClient = new CloudTableClient(
                storageAccount.TableEndpoint.ToString(),
                storageAccount.Credentials);

            serviceContext = cloudTableClient.GetDataServiceContext();
            serviceContext.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1.0d));

            roleId = RoleEnvironment.CurrentRoleInstance.Id;
        }

        /// <summary>
        /// Get the most recently recorded entry for the specified performance counter
        /// from the WADPerformanceCountersTable for this role instance. Returns null 
        /// if the table is unavailable or the specified counter has no entries in the table.
        /// </summary>
        /// <param name="specifier">The performance counter data specifier.</param>
        /// <returns>The performance counters table entry, or null if not found.</returns>
        public static PerformanceCountersEntity Latest(string specifier)
        {
            IQueryable<PerformanceCountersEntity> performanceCountersTable =
                serviceContext.CreateQuery<PerformanceCountersEntity>(WebRole.WADPerformanceCountersTable);

            var query = (from row in performanceCountersTable
                         where row.CounterName.Equals(specifier) && row.RoleInstance.Equals(roleId)
                         select row).AsTableServiceQuery<PerformanceCountersEntity>();
            
            // Execute the query and take the resulting final element, if any
            var counters = query.Execute();
            var result = (counters != null) ? counters.LastOrDefault() : null;

            return result;
        }

        /// <summary>
        /// Select the last five minutes' worth of performance counters entries from
        /// the WADPerformanceCountersTable for this role instance.
        /// </summary>
        /// <returns>An IEnumerable interface to the PerformanceCountersEntity results.</returns>
        public IEnumerable<PerformanceCountersEntity> Select()
        {
            IQueryable<PerformanceCountersEntity> performanceCountersTable =
                serviceContext.CreateQuery<PerformanceCountersEntity>(WebRole.WADPerformanceCountersTable);

            var selection = from row in performanceCountersTable
                            where row.EventTickCount > DateTime.UtcNow.AddMinutes(-5.0d).Ticks
                                 && row.RoleInstance.Equals(roleId)
                            select row;

            var query = selection.AsTableServiceQuery<PerformanceCountersEntity>();

            // Use the Execute commmand explicitly on the TableServiceQuery to 
            // take advantage of continuation tokens automatically and get all the data.
            var result = query.Execute();

            return result;
        }
    }
}