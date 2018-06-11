//*********************************************************
//
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.PerformancePoint.Scorecards;
using Microsoft.PerformancePoint.Scorecards.ServerCommon;
using Microsoft.PerformancePoint.Scorecards.Store;

namespace SaveReportFavoriteButton.Layouts.SaveReportFavoriteButton
{

    public partial class SaveReport : LayoutsPageBase
    {
        // Create a dictionary to store the names and IDs of the analytic reports on the dashboard page.
        Dictionary<string, string> reportsOnPage = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dictionary<string, string> perfPointContentLists = new Dictionary<string, string>();
                PopulateReportDropDown();
                PopulateTextBoxes();
                PopulatePerfPointList(perfPointContentLists);
            }
        }

        // Populate the ddlReportsOnPage dropdown list with the names of the reports on the page.
        protected void PopulateReportDropDown()
        {
            // Get the count, names, and IDs of the reports from the query string.
            NameValueCollection queryStringColl = HttpUtility.ParseQueryString(this.ClientQueryString);
            Int32 reportCount = Int32.Parse(queryStringColl["reportCount"]);

            for (int i = 0; i < reportCount; i++)
            {
                string reportId = "reportId" + i;
                if (queryStringColl[reportId] != null)
                {
                    string[] reportInfoInQueryString = queryStringColl[reportId].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    reportsOnPage.Add(reportInfoInQueryString[1], reportInfoInQueryString[0]);
                }
            }
            ddlReportsOnPage.DataSource = reportsOnPage;
            ddlReportsOnPage.DataTextField = "Key";
            ddlReportsOnPage.DataValueField = "Value";
            ddlReportsOnPage.DataBind();

            // Save the report names and IDs across postbacks.
            ViewState["reportsOnPage"] = reportsOnPage;
        }

        // Populate the txtBoxName, txtBoxDescription, and txtBoxFolder text boxes based on the selected report.
        protected void PopulateTextBoxes()
        {
            string selectedReport = ddlReportsOnPage.SelectedItem.Text;

            // Get initial values or handle the postback that is triggered by changing the selection in the
            // ddlReportsOnPage dropdown list. Ignore if postback is triggered by clicking the OK button
            // without changing the selection in the ddlReportsOnPage dropdown list.
            if ((!IsPostBack) || (IsPostBack && (selectedReport != (string)ViewState["SelectedReport"])))
            {
                ReportView report = GetReport(selectedReport);
                txtBoxName.Text = report.Name.Text;
                txtBoxDescription.Text = report.Description.Text;
                txtBoxFolder.Text = report.Folder;
                ViewState["SelectedReport"] = selectedReport;
            }
        }

        // Get the current version of the report.
        protected ReportView GetReport(string selectedReport)
        {
            if (IsPostBack)
            {
                reportsOnPage = (Dictionary<string, string>)ViewState["reportsOnPage"];
            }

            string reportId;
            reportsOnPage.TryGetValue(selectedReport, out reportId);

            // If the report has been navigated on, the report ID is a GUID. The GUID is used to
            // retrieve the navigated report from the back-end database.
            if (!reportId.Contains("/") && reportId.Contains("-"))
            {
                reportId = reportId.Substring(0, 36);
                return BIMonitoringServiceApplicationProxy.Default.GetAnalyticReportView(new RepositoryLocation(new Guid(reportId)));
            }
            else
            {
                return BIMonitoringServiceApplicationProxy.Default.GetAnalyticReportView(new RepositoryLocation(reportId));
            }
        }

        // Populate the ddlPerfPointLists collection with PerformancePoint Content Lists.
        protected void PopulatePerfPointList(Dictionary<string, string> perfPointContentLists)
        {
            using (SPWeb site = SPContext.Current.Site.RootWeb)
            {
                AddList(site, perfPointContentLists);

                foreach (SPWeb subSite in site.GetSubwebsForCurrentUser())
                {                  
                    try
                    {
                        AddList(subSite, perfPointContentLists);
                    }
                    finally
                    {
                        if (subSite != null)
                            subSite.Dispose();
                    }
                }
            }
            ddlPerfPointLists.DataSource = perfPointContentLists;
            ddlPerfPointLists.DataTextField = "Key";
            ddlPerfPointLists.DataValueField = "Value";
            ddlPerfPointLists.DataBind();
        }

        // Get available PerformancePoint Content Lists to save to.
        protected void AddList(SPWeb site, Dictionary<string, string> perfPointContentLists)
        {
            foreach (SPList list in site.Lists)
            {
                if (list.PropertiesXml.Contains("ServerTemplate=\"450\"")
                    && list.DoesUserHavePermissions(site.CurrentUser, SPBasePermissions.AddListItems))
                {
                    perfPointContentLists.Add(list.Title, list.DefaultDisplayFormUrl);
                }
            }
        }

        protected void ddlReportsOnPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateTextBoxes();
        }

        protected void btnSaveReport_Click(object sender, EventArgs e)
        {
            ReportView report = GetReport(ddlReportsOnPage.SelectedItem.Text);
            if (string.IsNullOrEmpty(txtBoxName.Text))
            {
                lblMessage.Text = "Report name cannot be blank.";
            }
            else
            {
                report.Name.Text = txtBoxName.Text;
                if (!string.IsNullOrEmpty(txtBoxDescription.Text))
                {
                    report.Description.Text = txtBoxDescription.Text;
                }
                if (!string.IsNullOrEmpty(txtBoxFolder.Text))
                {
                    report.Folder = txtBoxFolder.Text;
                }
                report.Owner.Login = Page.User.Identity.Name;

                // Clear the report location to save to the selected list.
                report.Location = RepositoryLocation.Empty();

                // Get the URL of the selected list.
                SPSite siteCollection = null;
                SPWeb site = null;
                try
                {
                    siteCollection = SPContext.Current.Site;
                    site = siteCollection.OpenWeb(ddlPerfPointLists.SelectedValue);
                    SPList list = site.GetList(siteCollection.Url + ddlPerfPointLists.SelectedValue);
                    string listUrl = list.DefaultViewUrl.Remove(list.DefaultViewUrl.LastIndexOf("/") + 1);
                    site.Dispose();
                    siteCollection.Dispose();

                    SaveAsFavorite(listUrl, report);
                }
                finally
                {
                    if (site != null)
                        site.Dispose();

                    if (siteCollection != null)
                        siteCollection.Dispose();
                }
            }
        }

        // Save the selected report to the selected list.
        protected void SaveAsFavorite(string listUrl, ReportView report)
        {
            lblMessage.Text = string.Empty;
            lblMessage.Visible = true;
            try
            {
                ServerUtils.AllowUnsafeUpdates = true;
                ReportView newReport = SPDataStore.GlobalDataStore.CreateReportView(listUrl, report);
                ServerUtils.AllowUnsafeUpdates = false;

                btnSaveReport.Enabled = false;
                lblMessage.Text = "Report \"" + newReport.Name.Text + "\" was saved to " + newReport.Location.ItemUrl + ".";
            }
            catch (Exception e)
            {
                lblMessage.Text = "Error saving report: " + e.Message; 
                throw e;
            }
        }
    }
}