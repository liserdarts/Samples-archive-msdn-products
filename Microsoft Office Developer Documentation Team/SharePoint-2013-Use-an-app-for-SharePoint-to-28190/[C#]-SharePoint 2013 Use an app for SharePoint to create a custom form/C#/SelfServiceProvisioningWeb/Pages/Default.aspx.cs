using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SelfServiceProvisioningWeb.Pages
{
    public partial class Default : BasePage
    {
        private List<SSConfig> configList;
        private const string SHAREPOINT_PID = "00000003-0000-0ff1-ce00-000000000000";
        private const string TENANT_ADMIN_URL = "https://richdizzcom-admin.sharepoint.com";
        protected void Page_Load(object sender, EventArgs e)
        {
            //get SharePoint context
            var spContext = Util.ContextUtil.Current;
            using (var clientContext = TokenHelper.GetClientContextWithContextToken(spContext.ContextDetails.AppWebUrl, spContext.ContextDetails.ContextTokenString, Request.Url.Authority))
            {
                //populate the badges control
                List list = clientContext.Web.Lists.GetByTitle("SSConfig");
                CamlQuery query = new CamlQuery()
                {
                    ViewXml = "<View><ViewFields><FieldRef Name='Title' /><FieldRef Name='SiteTemplate' /><FieldRef Name='BasePath' /><FieldRef Name='SiteType' /><FieldRef Name='MasterPageUrl' /><FieldRef Name='StorageMaximumLevel' /><FieldRef Name='UserCodeMaximumLevel' /></ViewFields></View>"
                };
                var items = list.GetItems(query);
                clientContext.Load(items, i => i.IncludeWithDefaultProperties(j => j.DisplayName));
                clientContext.ExecuteQuery();
                configList = items.ToList(spContext.ContextDetails.AppWebUrl, "SSConfig");
            }

            if (!this.IsPostBack)
            {
                //bind repeater
                repeaterTemplate.DataSource = configList;
                repeaterTemplate.DataBind();

                //configure buttons based on display type
                if (Page.Request["IsDlg"] == "1")
                    btnCancel.Attributes.Add("onclick", "javascript:closeDialog();return false;");
                else
                    btnCancel.Click += btnCancel_Click;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request["SPHostUrl"]);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //get the selected config
            SSConfig selectedConfig = configList.FirstOrDefault(i => i.Title.Equals(hdnSelectedTemplate.Value));
            if (selectedConfig != null)
            {
                string webUrl = "";
                if (selectedConfig.SiteType.Equals("Site Collection", StringComparison.CurrentCultureIgnoreCase))
                    webUrl = CreateSiteCollection(selectedConfig);
                else
                    webUrl = CreateSubsite(selectedConfig);

                //redirect to new site
                ClientScript.RegisterStartupScript(typeof(Default), "RedirectToSite", "navigateParent('" + webUrl + "');", true);
            }

        }

        private byte[] GetMasterPageFile(string masterUrl)
        {
            byte[] mpBytes = null;

            //get the siteurl of the masterpage
            string siteUrl = masterUrl.Substring(0, masterUrl.IndexOf("/_catalogs"));

            var siteUri = new Uri(siteUrl);  //static for my tenant
            var token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, siteUri.Authority, null).AccessToken;
            using (var clientContext = TokenHelper.GetClientContextWithAccessToken(siteUri.ToString(), token))
            {
                string relativeMasterUrl = masterUrl.Substring(8);
                relativeMasterUrl = relativeMasterUrl.Substring(relativeMasterUrl.IndexOf("/"));
                File file = clientContext.Web.GetFileByServerRelativeUrl(relativeMasterUrl);
                var stream = file.OpenBinaryStream();
                clientContext.ExecuteQuery();
                using (stream.Value)
                {
                    mpBytes = new Byte[stream.Value.Length];
                    stream.Value.Read(mpBytes, 0, mpBytes.Length);
                }
            }

            return mpBytes;
        }

        private string CreateSiteCollection(SSConfig selectedConfig)
        {
            string webUrl = "";

            //create site collection using the Tenant object
            var tenantAdminUri = new Uri(TENANT_ADMIN_URL);  //static for my tenant
            var token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, tenantAdminUri.Authority, null).AccessToken;
            using (var clientContext = TokenHelper.GetClientContextWithAccessToken(tenantAdminUri.ToString(), token))
            {
                var tenant = new Tenant(clientContext);
                webUrl = String.Format("{0}{1}", selectedConfig.BasePath, txtUrl.Text);
                var properties = new SiteCreationProperties()
                {
                    Url = webUrl,
                    Owner = "ridize@richdizzcom.onmicrosoft.com",
                    Title = txtTitle.Text,
                    Template = selectedConfig.SiteTemplate,
                    StorageMaximumLevel = Convert.ToInt32(selectedConfig.StorageMaximumLevel),
                    UserCodeMaximumLevel = Convert.ToDouble(selectedConfig.UserCodeMaximumLevel)
                };
                SpoOperation op = tenant.CreateSite(properties);
                clientContext.Load(tenant);
                clientContext.Load(op, i => i.IsComplete);
                clientContext.ExecuteQuery();

                //check if site creation operation is complete
                while (!op.IsComplete)
                {
                    //wait 30seconds and try again
                    System.Threading.Thread.Sleep(30000);
                    op.RefreshLoad();
                    clientContext.ExecuteQuery();
                }
            }

            //get the newly created site collection
            var siteUri = new Uri(webUrl);  //static for my tenant
            token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, siteUri.Authority, null).AccessToken;
            using (var clientContext = TokenHelper.GetClientContextWithAccessToken(siteUri.ToString(), token))
            {
                var newWeb = clientContext.Web;
                clientContext.Load(newWeb);
                clientContext.ExecuteQuery();

                //update description
                newWeb.Description = txtDescription.Text;

                //TODO: set additional site collection administrators

                //apply the masterpage to the site (if applicable)
                if (!String.IsNullOrEmpty(selectedConfig.MasterUrl))
                {
                    //get the the masterpage bytes from it's existing location
                    byte[] masterBytes = GetMasterPageFile(selectedConfig.MasterUrl);
                    string newMasterUrl = String.Format("{0}{1}/_catalogs/masterpage/ssp.master", selectedConfig.BasePath, txtUrl.Text);
                    
                    //upload to masterpage gallery of new web and set
                    List list = newWeb.Lists.GetByTitle("Master Page Gallery");
                    clientContext.Load(list, i => i.RootFolder);
                    clientContext.ExecuteQuery();
                    FileCreationInformation fileInfo = new FileCreationInformation();
                    fileInfo.Content = masterBytes;
                    fileInfo.Url = newMasterUrl;
                    Microsoft.SharePoint.Client.File masterPage = list.RootFolder.Files.Add(fileInfo);
                    string relativeMasterUrl = newMasterUrl.Substring(8);
                    relativeMasterUrl = relativeMasterUrl.Substring(relativeMasterUrl.IndexOf("/"));

                    //we can finally set the masterurls on the newWeb
                    newWeb.MasterUrl = relativeMasterUrl;
                    newWeb.CustomMasterUrl = relativeMasterUrl;
                }


                /**************************************************************************************/
                /*   Placeholder area for updating additional settings and features on the new site   */
                /**************************************************************************************/

                //update the web with the new settings
                newWeb.Update();
                clientContext.ExecuteQuery();
            }

            return webUrl;
        }

        private string CreateSubsite(SSConfig selectedConfig)
        {
            string webUrl = selectedConfig.BasePath + txtUrl.Text;

            //create subsite
            var parentSite = new Uri(selectedConfig.BasePath);  //static for my tenant
            var token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, parentSite.Authority, null).AccessToken;
            using (var clientContext = TokenHelper.GetClientContextWithAccessToken(parentSite.ToString(), token))
            {
                var properties = new WebCreationInformation()
                {
                    Url = txtUrl.Text,
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    WebTemplate = selectedConfig.SiteTemplate,
                    UseSamePermissionsAsParentSite = false
                };

                //create and load the new web
                Web newWeb = clientContext.Web.Webs.Add(properties);
                clientContext.Load(newWeb, w => w.Title);
                clientContext.ExecuteQuery();

                //TODO: set additional owners

                //apply the masterpage to the site (if applicable)
                if (!String.IsNullOrEmpty(selectedConfig.MasterUrl))
                {
                    newWeb.MasterUrl = selectedConfig.MasterUrl;
                    newWeb.CustomMasterUrl = selectedConfig.MasterUrl;
                }

                /**************************************************************************************/
                /*   Placeholder area for updating additional settings and features on the new site   */
                /**************************************************************************************/

                //update the web with the new settings
                newWeb.Update();
                clientContext.ExecuteQuery();
            }

            return webUrl;
        }
    }
}