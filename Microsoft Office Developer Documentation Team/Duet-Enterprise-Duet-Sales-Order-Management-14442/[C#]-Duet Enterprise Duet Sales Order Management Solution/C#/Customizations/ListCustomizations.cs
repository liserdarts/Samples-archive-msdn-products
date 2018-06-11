using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls.WebParts;

namespace DuetSalesOrderSolution.Customizations
{
    /// <summary>
    /// Class that enables customization for external lists.
    /// </summary>
    class ListCustomizations
    {
        // List of external list titles.
        private string[] externalListTitles = null;  

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listTitles">Array of list titles.</param>
        public ListCustomizations(string[] listTitles)
        {
            externalListTitles = listTitles;
        }

        /// <summary>
        /// Activate artifacts associated with list customization.
        /// </summary>
        /// <param name="spWeb">Web site on which customizations are to be activated.</param>
        public void Activate(SPWeb spWeb)
        {
            if (externalListTitles != null)
            {
                MakeExternalListsVisibleOnQuickLaunch(spWeb);

                //Add custom script to list view pages.
                AddCustomScriptToSalesOrderHeadersListPage(spWeb);
                AddCustomScriptToSalesOrderItemsListPage(spWeb);
            }
        }

        /// <summary>
        /// Deactivate artifacts associated with list customization.
        /// </summary>
        /// <param name="spWeb">Web site on which customizations are to be deactivated.</param>
        public void Deactivate(SPWeb spWeb)
        {
            if (externalListTitles != null)
            {
                // Remove the lists that were created.
                for (int i = 0; i < externalListTitles.Length; i++)
                {
                    SPList externalList = spWeb.Lists.TryGetList(externalListTitles[i]);

                    if (externalList != null)
                    {
                        spWeb.Lists.Delete(externalList.ID);
                    }
                }
            }
        }

        /// <summary>
        /// Programmatically sets the specified lists to show on Quick Launch.
        /// </summary>
        /// <remarks>
        /// List Instances, when they are defined declaratively (that is, using an Elements.xml file)
        /// and associated with a custom schema may not be visible on Quick Launch even when
        /// the OnQuickLaunch property for them is set to TRUE in the Elements.xml file.
        /// </remarks>
        /// <param name="spWeb">The target SharePoint Web site.</param>
        private void MakeExternalListsVisibleOnQuickLaunch(SPWeb spWeb)
        {
            for (int i = 0; i < externalListTitles.Length; i++)
            {
                SPList externalList = spWeb.Lists.TryGetList(externalListTitles[i]);
                if (externalList != null)
                {
                    externalList.OnQuickLaunch = true;
                    externalList.Update();
                }
            }
        }

        /// <summary>
        /// Adds custom HTML and Javascript to list view page.
        /// </summary>
        /// <param name="spWeb">Web site on which the changes are to be made.</param>
        /// <param name="viewPageUrl">Web application-relative URL of the list view page.</param>
        /// <param name="scriptPageUrl">Site-relative URL of the page that contains HTML and script.</param>
        private void AddCustomScriptToViewPage(SPWeb spWeb, string viewPageUrl, string scriptPageUrl)
        {
            SPFile file = spWeb.GetFile(viewPageUrl);

            using (SPLimitedWebPartManager webPartManager 
                = file.GetLimitedWebPartManager(PersonalizationScope.Shared))
            {
                ContentEditorWebPart contentEditorWebPart = new ContentEditorWebPart();

                contentEditorWebPart.AllowEdit = false;
                contentEditorWebPart.ContentLink = SPUrlUtility.CombineUrl(spWeb.Url, scriptPageUrl);
                webPartManager.AddWebPart(contentEditorWebPart, "ZoneLeft", 0);
            }
            file.Update();
        }

        /// <summary>
        /// Adds Javscript to the list view page of Sales Order Headers list.
        /// </summary>
        /// <param name="spWeb">Web site with list.</param>
        private void AddCustomScriptToSalesOrderHeadersListPage(SPWeb spWeb)
        {
            SPList externalList = spWeb.Lists.TryGetList(externalListTitles[0]);

            if (externalList != null)
            {
                AddCustomScriptToViewPage(spWeb, externalList.DefaultViewUrl, "/SalesOrderModule/SalesOrderHeaderScript.htm");
            }
        }

        /// <summary>
        /// Adds Javscript to the list view page of Sales Order Items list.
        /// </summary>
        /// <param name="spWeb">Web site with list.</param>
        private void AddCustomScriptToSalesOrderItemsListPage(SPWeb spWeb)
        {
            SPList externalList = spWeb.Lists.TryGetList(externalListTitles[1]);

            if (externalList != null)
            {
                AddCustomScriptToViewPage(spWeb, externalList.DefaultViewUrl, "/SalesOrderModule/SalesOrderItemScript.htm");
            }
        }
    }
}
