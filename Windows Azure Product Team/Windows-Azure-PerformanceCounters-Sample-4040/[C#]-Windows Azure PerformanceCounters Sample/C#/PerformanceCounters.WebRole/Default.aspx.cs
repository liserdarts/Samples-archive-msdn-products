// -----------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Microsoft">
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
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics.Management;
    using Microsoft.WindowsAzure.ServiceRuntime;

    /// <summary>
    /// The codebehind class for the Default page.
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// The page load event handler.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadListBox();
        }

        /// <summary>
        /// Load the page list box with the current set of configured performance counters.
        /// </summary>
        protected void LoadListBox()
        {
            try
            {
                this.ListBox1.Items.Clear();

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(
                    WebRole.GetConfigurationSettingValue(WebRole.WADConnectionString));

                var roleInstanceDiagnosticManager = cloudStorageAccount.CreateRoleInstanceDiagnosticManager(
                    RoleEnvironment.DeploymentId,
                    RoleEnvironment.CurrentRoleInstance.Role.Name,
                    RoleEnvironment.CurrentRoleInstance.Id);

                var roleDiagnosticMonitorConfiguration = roleInstanceDiagnosticManager.GetCurrentConfiguration();
                var performanceCounterSources = roleDiagnosticMonitorConfiguration.PerformanceCounters.DataSources;

                foreach (var configuration in performanceCounterSources)
                {
                    this.ListBox1.Items.Add(configuration.CounterSpecifier);
                }
            }
            catch (RoleEnvironmentException rex)
            {
                // The connection string was missing or invalid.
                System.Diagnostics.Trace.WriteLine("WebRole environment diagnostics error: " + rex.Message);
            }
            catch (InvalidOperationException iox)
            {
                // Parse of the connection string failed.
                System.Diagnostics.Trace.WriteLine("WebRole environment diagnostics error: " + iox.Message);
            }
        }
    }
}
