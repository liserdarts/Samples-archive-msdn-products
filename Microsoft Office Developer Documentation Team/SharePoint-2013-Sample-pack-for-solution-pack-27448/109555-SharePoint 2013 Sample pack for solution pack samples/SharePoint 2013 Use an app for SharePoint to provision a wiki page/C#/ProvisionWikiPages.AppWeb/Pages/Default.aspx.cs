using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contoso.Provision.Pages.AppWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Uri redirectUrl;
            switch (SharePointContextProvider.CheckRedirectionStatus(Context, out redirectUrl))
            {
                case RedirectionStatus.Ok:
                    return;
                case RedirectionStatus.ShouldRedirect:
                    Response.Redirect(redirectUrl.AbsoluteUri, endResponse: true);
                    break;
                case RedirectionStatus.CanNotRedirect:
                    Response.Write("An error occurred while processing your request.");
                    Response.End();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // define initial script, needed to render the chrome control
            string script = @"
            function chromeLoaded() {
                $('body').show();
            }

            //function callback to render chrome after SP.UI.Controls.js loads
            function renderSPChrome() {
                //Set the chrome options for launching Help, Account, and Contact pages
                var options = {
                    'appTitle': document.title,
                    'onCssLoaded': 'chromeLoaded()'
                };

                //Load the Chrome Control in the divSPChrome element of the page
                var chromeNavigation = new SP.UI.Controls.Navigation('divSPChrome', options);
                chromeNavigation.setVisible(true);
            }";

            //register script in page
            Page.ClientScript.RegisterClientScriptBlock(typeof(Default), "BasePageScript", script, true);

            // The following code gets the client context and Title property by using TokenHelper.
            // To access other properties, the app may need to request permissions on the host web.
            //var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);

            //using (var clientContext = spContext.CreateUserClientContextForSPHost())
            //{
            //    clientContext.Load(clientContext.Web, web => web.Title);
            //    clientContext.ExecuteQuery();
            //    Response.Write(clientContext.Web.Title);
            //}
        }

        private string SharePointUrlParameters()
        {
            return HttpUtility.ParseQueryString(this.Context.Request.Url.Query).ToString();
        }

        protected void btnScenario1_Click(object sender, EventArgs e)
        {
            ClientSideSharePointService csomService = new ClientSideSharePointService(this.Context);
            string scenario1Page = String.Format("scenario1-{0}.aspx", DateTime.Now.Ticks);
            string scenario1PageUrl = csomService.AddWikiPage("Site Pages", scenario1Page);
            csomService.AddHtmlToWikiPage("SitePages", txtHtml.Text, scenario1Page);
            this.hplScenario1.NavigateUrl = string.Format("{0}/{1}", Request.QueryString["SPHostUrl"], scenario1PageUrl);
        }

        protected void btnScenario2_Click(object sender, EventArgs e)
        {
            ClientSideSharePointService csomService = new ClientSideSharePointService(this.Context);
            
            if (csomService.AddList(170, new Guid("192efa95-e50c-475e-87ab-361cede5dd7f"), "Links", false))
            {
                csomService.AddPromotedSiteLink("Links", "Gapps on codebox", "http://codebox/gapps");
                csomService.AddPromotedSiteLink("Links", "Bing", "http://www.bing.com");
            }            
            
            string scenario2Page = String.Format("scenario2-{0}.aspx", DateTime.Now.Ticks);
            string scenario2PageUrl = csomService.AddWikiPage("Site Pages", scenario2Page);

            bool twoColumnsOrMore = false;
            bool header = false;
            switch (drpLayouts.SelectedValue)
            {
                case "OneColumn":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.OneColumn, scenario2Page);
                    break;
                case "OneColumnSideBar":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.OneColumnSideBar, scenario2Page);
                    break;
                case "TwoColumns":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.TwoColumns, scenario2Page);
                    twoColumnsOrMore = true;
                    break;
                case "TwoColumnsHeader":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.TwoColumnsHeader, scenario2Page);
                    twoColumnsOrMore = true;
                    header = true;
                    break;
                case "TwoColumnsHeaderFooter":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.TwoColumnsHeaderFooter, scenario2Page);
                    twoColumnsOrMore = true;
                    header = true;
                    break;
                case "ThreeColumns":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.ThreeColumns, scenario2Page);
                    twoColumnsOrMore = true;
                    break;
                case "ThreeColumnsHeader":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.ThreeColumnsHeader, scenario2Page);
                    twoColumnsOrMore = true;
                    header = true;
                    break;
                case "ThreeColumnsHeaderFooter":
                    csomService.AddLayoutToWikiPage("SitePages", WikiPageLayout.ThreeColumnsHeaderFooter, scenario2Page);
                    twoColumnsOrMore = true;
                    header = true;
                    break;
                default:
                    break;
            }

            Guid linksID = csomService.GetListID("Links");
            WebPartEntity wp2 = new WebPartEntity();
            wp2.WebPartXml = csomService.WpPromotedLinks(linksID, string.Format("{0}/Lists/{1}", Request.QueryString["SPHostUrl"], "Links"), string.Format("{0}/{1}", Request.QueryString["SPHostUrl"], scenario2PageUrl), "$Resources:core,linksList");
            wp2.WebPartIndex = 1;
            wp2.WebPartTitle = "Links";

            int webpartRow = 1;
            if (header)
            {
                webpartRow = 2;
            }

            csomService.AddWebPartToWikiPage("SitePages", wp2, scenario2Page, webpartRow, 1, false);
            Session.Add("LastPageName", scenario2Page);

            if (twoColumnsOrMore)
            {
                csomService.AddHtmlToWikiPage("SitePages", txtHtml.Text, scenario2Page, webpartRow, 2);
            }

            this.hplScenario2.NavigateUrl = string.Format("{0}/{1}", Request.QueryString["SPHostUrl"], scenario2PageUrl);
            this.btnScenario2Remove.Enabled = true;
        }

        protected void btnScenario2Remove_Click(object sender, EventArgs e)
        {
            ClientSideSharePointService csomService = new ClientSideSharePointService(this.Context);
            csomService.DeleteWebPart("SitePages", "Links", Session["LastPageName"].ToString());
            this.btnScenario2Remove.Enabled = false;
        }

        protected void btnCleanup_Click(object sender, EventArgs e)
        {
            ClientSideSharePointService csomService = new ClientSideSharePointService(this.Context);
            csomService.DeleteDemoPages("SitePages");
            this.hplScenario1.NavigateUrl = "";
            this.hplScenario2.NavigateUrl = "";
            this.btnScenario2Remove.Enabled = false;
        }



    }
}