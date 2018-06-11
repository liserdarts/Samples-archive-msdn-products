namespace MSDN.SharePoint.Samples
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    [ToolboxItemAttribute(false)]
    public class TabEditorWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        // A Div to display the Content of the Tab.
        private HtmlGenericControl _divTabContent;

        // Collection of Tabs
        private List<TabData> _tabList;

        // Property to hold the collection of Tabs.
        // Set the Personalizable attribute to True, 
        // to allow for personalization of tabs by users.
        [Personalizable(true)]
        public List<TabData> TabList { 
            get
            {
                if (this._tabList == null)
                {
                    // Return an empty collection if null.
                    this._tabList = new List<TabData>();
                }

                return this._tabList;
            }  
            
            set
            {
                this._tabList = value;
            } 
        }

        public override object WebBrowsableObject
        {
            // Return a reference to the Web Part instance.
            get { return this; }
        }

        public override EditorPartCollection CreateEditorParts()
        {
            TabConfigurationEditorPart editorPart = new TabConfigurationEditorPart();

            // The ID of the Editor part should be unique. So prefix it with the ID of the Web Part.
            editorPart.ID = this.ID + "_TabConfigurationEditorPart";

            // Create a collection of Editor Parts and add them to the Editor Part collection.
            List<EditorPart> editors = new List<EditorPart> { editorPart };
            return new EditorPartCollection(editors);
        }

        public void SaveChanges()
        {
            // This method sets a flag indicating that the personalization data has changed.
            // This will allow the changes to the Web Part properties from outside the Web Part class.
            this.SetPersonalizationDirty();
        }

        protected override void CreateChildControls()
        {
            this._divTabContent = new HtmlGenericControl("div");
            this._divTabContent.ID = "divTabContent";

            // Render the Tabs if the Count is more than zero.
            if (this.TabList.Count > 0)
            {
                Menu tabMenu = new Menu();
               
                tabMenu.Orientation = Orientation.Horizontal;
                tabMenu.MenuItemClick += new MenuEventHandler(TabMenu_MenuItemClick);
                foreach (TabData tab in this.TabList)
                {
                    tabMenu.Items.Add(new MenuItem(tab.Title));
                }

                this.Controls.Add(tabMenu);

                // Set the text in div to the Content of the first Tab.
                this._divTabContent.InnerHtml = this.TabList[0].Content;
            }
            else
            {
                // If not tabs exist, Set the div text to,
                // suggest the user to Edit the Web Part and add Tabs.
                this._divTabContent.InnerHtml = "Edit this Web Part to add Tabs.";
            }

            this.Controls.Add(_divTabContent);
        }

        private void TabMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            // Display the content related to the Tab that was clicked.
            string tabTitle = e.Item.Text;

            // Find the Tab based on the Title clicked, 
            // and set the Div to the corresponding content.
            this._divTabContent.InnerHtml = this.TabList.Find(t => t.Title.Equals(tabTitle)).Content;
        }
    }
}
