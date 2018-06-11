using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq;

using Microsoft.SharePoint.WebPartPages;
using Microsoft.SharePoint;

namespace SharePoint2010CustomTabWebPart.TabWebPart
{
    public partial class TabWebPartUserControl : UserControl
    {
        public TabWebPart tab { get; set; }
        public List<string> tabList = new List<string>();
        string tabsConfiguration = string.Empty;
        TextReader textReader = null;
        XDocument xDocument = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            tabsConfiguration = this.tab.ConfigureTabs;
            if (string.IsNullOrEmpty(tabsConfiguration))
            {
                tabsConfiguration = @"<tabs><tab name=""tab1""><webPart title=""HTML Form Web Part""></webPart></tab></tabs>";
            }
            try
            {
                textReader = new StringReader(tabsConfiguration);
                xDocument = XDocument.Load(textReader);

                var tabNames = from t in xDocument.Descendants("tab")
                               select (string)t.Attribute("name");

                foreach (string tabName in tabNames)
                {
                    tabList.Add(tabName);
                }
            }
            catch
            {
                tabList.Add("FirstTab");
            }

            // For tab titles
            this.TabRepeater.DataSource = tabList;
            this.TabRepeater.DataBind();
            // For tab contents
            this.TabContainerRepeater.DataSource = tabList;
            this.TabContainerRepeater.DataBind();
        }

        protected void TabContainerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Panel is the place holder to show web parts in a tab.
            Panel panel = (Panel)e.Item.FindControl("TabContainer");

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (panel != null)
                {
                    using (SPLimitedWebPartManager wpManager = SPContext.Current.File.GetLimitedWebPartManager(PersonalizationScope.Shared))
                    {
                        try
                        {
                            // Elevated previleges required for EXPORT and IMPORT. Else Users with normal read access will get errors.
                            SPSecurity.RunWithElevatedPrivileges(delegate()
                            {
                                // Retrieve the web part titles in the ConfigureTabs XML string for this tab.
                                var webPartTitles = from t in xDocument.Descendants("webPart")
                                                    where (string)t.Parent.Attribute("name") == (string)e.Item.DataItem
                                                    select (string)t.Attribute("title");

                                foreach (string wpTitle in webPartTitles)
                                {
                                    foreach (System.Web.UI.WebControls.WebParts.WebPart webPart in wpManager.WebParts)
                                    {
                                        // Find the matched closed web part in WebParts collection
                                        if (webPart.Title == wpTitle && webPart.IsClosed == true)
                                        {
                                            string errorMessage;
                                            MemoryStream stream = new MemoryStream();
                                            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);

                                            // Export the closed webpart to a memory stream.
                                            wpManager.ExportWebPart(webPart, writer);

                                            writer.Flush();
                                            stream.Position = 0;

                                            XmlTextReader reader = new XmlTextReader(stream);

                                            // Import the exported webpart.
                                            System.Web.UI.WebControls.WebParts.WebPart newWebPart = wpManager.ImportWebPart(reader, out errorMessage);

                                            reader.Close();
                                            writer.Close();

                                            // Show the imported webpart.
                                            panel.Controls.Add(newWebPart);
                                            break;
                                        }
                                    }
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            // For debugging use only.
                            Label label = new Label();
                            label.Text = "Please check your XML configuration for error. " + Environment.NewLine + ex.Message;
                            panel.Controls.Add(label);
                        }

                    }
                }
            }
        }
    }
}
