// -----------------------------------------------------------------------
// <copyright file="CountersPage.aspx.cs" company="Microsoft">
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
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.Diagnostics.Management;
    using Microsoft.WindowsAzure.ServiceRuntime;

    /// <summary>
    /// The codebehind class for the Performance Counters page.
    /// </summary>
    public partial class CountersPage : System.Web.UI.Page
    {
        /// <summary>
        /// The page load event handler.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Refresh page every few seconds.
            Response.AddHeader("Refresh", "30");

            this.LabelSampleRate.Text = 
                WebRole.GetConfigurationSettingValue(WebRole.WebRoleSampleRateName);
            this.LabelTransferInterval.Text =
                WebRole.GetConfigurationSettingValue(WebRole.WebRolePeriodName);
        }
    }
}
