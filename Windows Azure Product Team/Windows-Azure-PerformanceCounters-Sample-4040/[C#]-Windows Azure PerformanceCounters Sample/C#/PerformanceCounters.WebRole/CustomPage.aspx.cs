// -----------------------------------------------------------------------
// <copyright file="CustomPage.aspx.cs" company="Microsoft">
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
    using System.Diagnostics;

    /// <summary>
    /// The codebehind class for the Custom Performance Counters page
    /// </summary>
    public partial class CustomPage : System.Web.UI.Page
    {
        /// <summary>
        /// The PerformanceCounter for recording Button 1 activity.
        /// </summary>
        private static PerformanceCounter button1Counter;

        /// <summary>
        /// The PerformanceCounter for recording Button 2 activity.
        /// </summary>
        private static PerformanceCounter button2Counter;

        /// <summary>
        /// Initializes static members of the CustomPage class.
        /// </summary>
        static CustomPage()
        {
            button1Counter = new PerformanceCounter(
                WebRole.CustomCounterCategory,
                WebRole.CustomCounter1Name,
                string.Empty,
                false);

            button2Counter = new PerformanceCounter(
                WebRole.CustomCounterCategory,
                WebRole.CustomCounter2Name,
                string.Empty,
                false);
        }

        /// <summary>
        /// The page load event handler.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Refresh page every few seconds.
            Response.AddHeader("Refresh", "30");

            this.LabelButton1.Text = this.CounterValue(WebRole.CustomCounter1);
            this.LabelButton2.Text = this.CounterValue(WebRole.CustomCounter2);
            this.LabelLocal1.Text = button1Counter.RawValue.ToString();
            this.LabelLocal2.Text = button2Counter.RawValue.ToString();
        }

        /// <summary>
        /// The value of the most recent available entry in Azure storage for the 
        /// specified performance counter, or "unavailable" if none returned.
        /// </summary>
        /// <param name="specifier">The performance counter data specifier.</param>
        /// <returns>The value in the CounterValue field for the specified performance counter.</returns>
        protected string CounterValue(string specifier)
        {
            PerformanceCountersEntity entry = PerformanceCountersDataSource.Latest(specifier);

            string result = entry != null ? entry.CounterValue.ToString() : "unavailable";

            return result;
        }

        /// <summary>
        /// The event handler for Button 1 increments the Button 1 custom performance counter.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            button1Counter.Increment();
            this.LabelLocal1.Text = button1Counter.RawValue.ToString();
        }

        /// <summary>
        /// The event handler for Button 2 increments the Button 2 custom performance counter.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            button2Counter.Increment();
            this.LabelLocal2.Text = button2Counter.RawValue.ToString();
        }
    }
}