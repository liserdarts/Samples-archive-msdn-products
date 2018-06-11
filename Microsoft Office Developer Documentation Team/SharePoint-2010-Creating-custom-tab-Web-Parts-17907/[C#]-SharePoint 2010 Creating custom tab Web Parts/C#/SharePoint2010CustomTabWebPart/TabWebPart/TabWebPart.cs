using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace SharePoint2010CustomTabWebPart.TabWebPart
{
    [ToolboxItemAttribute(false)]
    [CSSDependecy("STYLES/jQuery-Tab/jquery-ui-1.8.custom.css", Priority = 0)]
    [ScriptDependecy("STYLES/jQuery-Tab/jquery-1.4.1.min.js", Priority = 2)]
    [ScriptDependecy("STYLES/jQuery-Tab/jquery-ui-1.8.custom.min.js", Priority = 20)]
    public class TabWebPart : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/SharePoint2010CustomTabWebPart/TabWebPart/TabWebPartUserControl.ascx";

        const string resourceBasePath = "/_layouts/";
        private string tabsConfigurationString;

        [WebBrowsable(true),
        Category("Data"),
        Personalizable(PersonalizationScope.Shared),
        WebDisplayName("Configure Tabs"),
        Description(@"XML configuration for tabs. <tabs><tab name=""tab1""><webPart title=""Web Part""></webPart></tab><tab name=""tab2""><webPart title=""A Web Part""></webPart><webPart title=""B Web Part""></webPart></tab></tabs>")]
        public string ConfigureTabs { get { return tabsConfigurationString; } set { tabsConfigurationString = value; } }

        #region Tabbed Methods
        /// <summary>
        /// Constructor for the web part
        /// </summary>
        public TabWebPart()
        {
            XElement xElement = new XElement("tabs",
                new XElement("tab", new XAttribute("name", "tab1"),
                    new XElement("webPart", new XAttribute("title", "Web Part"))),
                new XElement("tab", new XAttribute("name", "tab2"),
                    new XElement("webPart", new XAttribute("title", "A Web Part")),
                    new XElement("webPart", new XAttribute("title", "B Web Part")))
                );
            tabsConfigurationString = xElement.ToString();
        }

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            TabWebPartUserControl tabUserControl = control as TabWebPartUserControl;
            if (tabUserControl != null)
            {
                tabUserControl.tab = this;
            }

            Controls.Add(control);
        }

        /// <summary>
        /// This tab web part allows multiple instances on one page. jQuery is used for the tab UI. Attributes are used to ensure each js file is loaded once on a page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            IEnumerable<CSSDependecyAttribute> cssDependencies = this.GetType().GetCustomAttributes(typeof(CSSDependecyAttribute), true).OfType<CSSDependecyAttribute>().OrderBy(attr => attr.Priority);
            foreach (CSSDependecyAttribute attribute in cssDependencies)
                if (Page.Items[attribute.Path] == null && Page.Header != null)
                {
                    Page.Items[attribute.Path] = new Object();
                    Page.Header.Controls.Add(new Literal { Text = string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />", Path.Combine(resourceBasePath, attribute.Path)) });
                }
            IEnumerable<ScriptDependecyAttribute> scriptDependencies = this.GetType().GetCustomAttributes(typeof(ScriptDependecyAttribute), true).OfType<ScriptDependecyAttribute>().OrderBy(attr => attr.Priority);
            foreach (ScriptDependecyAttribute attribute in scriptDependencies)
                if (Page.Items[attribute.Path] == null && Page.Header != null)
                {
                    Page.Items[attribute.Path] = new Object();
                    Page.Header.Controls.Add(new Literal { Text = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", Path.Combine(resourceBasePath, attribute.Path)) });
                }
        }

        #endregion
    }
}
