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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.PerformancePoint.Scorecards;
using Microsoft.PerformancePoint.Scorecards.Analytics;
using Microsoft.PerformancePoint.Scorecards.OlapReportViews;
using Microsoft.PerformancePoint.Scorecards.ServerCommon;
using Microsoft.PerformancePoint.Scorecards.Store;

namespace ChangeDataSourceButton.Layouts.ChangeDataSource
{
    public partial class ChangeDataSource : LayoutsPageBase
    {
        // Create a collection to store the selected analytic reports.
        ReportViewCollection selectedAnalyticReports = new ReportViewCollection();
        string siteUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateSelectionsTextBoxes();
                if (selectedAnalyticReports.Count > 0)
                {
                    PopulateDataSourceDropDownList();
                }
                else
                {
                    btnChangeDataSource.Enabled = false;
                    lblMessage.Text = "You must select at least one analytic report to change (Analytic Chart or Analytic Grid).\n";
                }

                // Save the collections across postbacks.
                ViewState["selectedAnalyticReports"] = selectedAnalyticReports;
            }
        }

        // Populate the textboxes with the names of the selected items (analytic reports and non-analytic reports).
        protected void PopulateSelectionsTextBoxes()
        {

            // Get the list and item identifiers from the query string.
            NameValueCollection queryStringColl = HttpUtility.ParseQueryString(this.ClientQueryString);
            string listUrl = GetListUrl(queryStringColl["listId"]);
            string itemIds = queryStringColl["itemIds"];
            string [] selectedItemIds = itemIds.Split(new char[] { ',' });

            string notAnalyticReports = CategorizeSelectedItems(listUrl, selectedItemIds);
            string reportsToChange = "";

            for (int i = 0; i < selectedAnalyticReports.Count; i++)
            {
                reportsToChange += selectedAnalyticReports[i].Name.Text + "\n";
            }

            txtBoxReportsToChange.Text = reportsToChange;
            if (!string.IsNullOrEmpty(notAnalyticReports))
            {
                lblNotAnalyticReports.Visible = true;
                txtBoxNotAnalyticReports.Visible = true;
                txtBoxNotAnalyticReports.Text = notAnalyticReports;
            }
        }

        protected void btnChangeDataSource_Click(object sender, EventArgs e)
        {
            ChangeReportDataSource();
        }

        public string GetListUrl(string listId)
        {
            Guid listGuid = new Guid(listId);
            SPList list = null;
            SPListCollection lists = null;

            using (SPWeb site = SPContext.Current.Site.RootWeb)
            {
                // Look for the list in the root site.
                lists = site.Lists;
                for (int i = 0; i < lists.Count; i++)
                {
                    if (lists[i].ID == listGuid)
                    {
                        list = lists.GetList(listGuid, false);
                        siteUrl = list.ParentWebUrl;
                        return list.RootFolder.ServerRelativeUrl;
                    }
                }

                // If the list is not in the root site, look in the subsites.
                foreach (SPWeb subSite in site.GetSubwebsForCurrentUser())
                {
                    try
                    {
                        lists = subSite.Lists;
                        for (int i = 0; i < lists.Count; i++)
                        {
                            if (lists[i].ID == listGuid)
                            {
                                list = lists.GetList(listGuid, false);
                                siteUrl = list.ParentWebUrl;
                                return list.RootFolder.ServerRelativeUrl;
                            }
                        }
                    }
                    finally
                    {
                        if (subSite != null)
                            subSite.Dispose();
                    }
                }
            }
            return "";
        }

        protected string CategorizeSelectedItems(string listUrl, string[] selectedItemIds)
        {

            // Get the item identifiers into the ItemUrl format.
            StringCollection itemUrls = new StringCollection();
            for (var i = 0; i < selectedItemIds.Length; i++)
            {
                if (!string.IsNullOrEmpty(selectedItemIds[i]))
                {
                    string itemUrl = listUrl + "/" + selectedItemIds[i] + "_.000";
                    itemUrls.Add(itemUrl);
                }
            }

            // Get the all items in the list and filter for analytic reports. 
            // Getting the entire collection because we do not know in advance whether all selected items are analytic reports,
            // and we want to inform users which items are not and therefore will not be changed.
            FirstClassElementCollection fcos = SPDataStore.GlobalDataStore.GetListItems(listUrl);

            string notAnalyticReports = "";

            // Find each selected item in the returned collection.
            foreach (var itemUrl in itemUrls)
            {
                foreach (var fco in fcos)
                {
                    if (fco.Location.ItemUrl == itemUrl)
                    {
                        if (fco.ContentType == FCOContentType.PpsReportView)
                        {
                            ReportView report = (ReportView)fco.Clone();

                            // Add the selected analytic reports to the selectedAnalyticReports collection.
                            if (report.SubTypeId == ReportViewNames.AnalyticChart || report.SubTypeId == ReportViewNames.OLAPGrid)
                            {
                                selectedAnalyticReports.Add(report);
                                break;
                            }
                            
                            // Add the names of non-analytic reports to the notAnalyticReports string.
                            else
                            {
                                notAnalyticReports += fco.Name.Text + "\n";
                                break;
                            }
                        }

                        // Add the names of selected items that are not reports to the notAnalyticReports string.
                        else
                        {
                            notAnalyticReports += fco.Name.Text + "\n";
                        }
                    }
                }
            }
            return notAnalyticReports;
        }

        // Populate the data source dropdown with OLAP data sources from PerformancePoint Data Connections Libraries within the site.
        protected void PopulateDataSourceDropDownList()
        {
            Microsoft.PerformancePoint.Scorecards.DataSourceCollection avaliableDataSources = new Microsoft.PerformancePoint.Scorecards.DataSourceCollection();
            using (SPWeb site = SPContext.Current.Site.OpenWeb(siteUrl))
            {
                // Get available OLAP data sources in the site.
                foreach (SPList list in site.Lists)
                {
                    if (list.BaseType == SPBaseType.DocumentLibrary)
                    {
                        int baseTemplateValue = (int)list.BaseTemplate;
                        if ((baseTemplateValue == 470) && list.DoesUserHavePermissions(site.CurrentUser, SPBasePermissions.AddListItems))
                        {
                            FirstClassElementCollection dataSourceFcos = SPDataStore.GlobalDataStore.GetListItems(list.RootFolder.ServerRelativeUrl);
                            foreach (var ds in dataSourceFcos)
                            {
                                DataSource dataSource = (DataSource)ds;
                                if ((dataSource.SubTypeId == DataSourceNames.Adomd) || (dataSource.IsGemini))
                                {
                                    avaliableDataSources.Add(dataSource);
                                }
                            }
                        }
                    }
                }
            }

            Dictionary<string, string> dataSourcesForDropDown = new Dictionary<string, string>();
            foreach (var dataSource in avaliableDataSources)
            {
                string dataSourceDisplayString = dataSource.Location.GetListUrl() + "/" + dataSource.Name.Text + " (" + dataSource.ServerName + ")";
                dataSourcesForDropDown.Add(dataSourceDisplayString, dataSource.Location.ItemUrl);
            }

            ddlDataSources.DataSource = dataSourcesForDropDown;
            ddlDataSources.DataTextField = "Key";
            ddlDataSources.DataValueField = "Value";
            ddlDataSources.DataBind();
        }

        protected void ChangeReportDataSource()
        {
            // Retrieve the selected reports from ViewState.
            selectedAnalyticReports = (ReportViewCollection)ViewState["selectedAnalyticReports"];

            // Get the selected data source.
            DataSource dataSource = BIMonitoringServiceApplicationProxy.Default.GetDataSource(new RepositoryLocation(ddlDataSources.SelectedValue));

            foreach  (ReportView report in selectedAnalyticReports)
            {
                try 
                {
                    // Change the reference to the data source location for the report.
                    // Analytic reports store the data source location in the CustomData property.
                    string queryMDX = BIMonitoringServiceApplicationProxy.Default.GetMdx(report.Location);

                    // Get the report's OLAPReportViewData object, which is the core of an analytic view.
                    OLAPReportViewData olapViewData = report.GetOlapReportViewData();

                    // Set up the data source in the query state so the "Revert to Design mode" option in
                    // Dashboard Designer works correctly.
                    olapViewData.QueryState.DataSourceLocation = dataSource.Location;

                    // Use an OLAPQueryData object to store the data source location and the MDX query.
                    olapViewData.QueryData.DataSourceLocation = dataSource.Location;
                    olapViewData.QueryData.TokenizedMDX = queryMDX;

                    // Set the CustomData property to the serialized OLAPReportViewData object.
                    report.CustomData = OLAPReportViewData.Serialize(olapViewData);

                    SPDataStore.GlobalDataStore.UpdateReportView(report);
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    // TODO: Handle failures and report them to the user.
                }
                btnChangeDataSource.Enabled = false;
                lblMessage.Visible = true;
                lblMessage.Text = "The data source has been changed to the \'" + dataSource.Name.Text + "\' cube on the \'" + dataSource.ServerName + "\' server.";
            }
        }
    }
}
