
namespace MSDN.SharePoint.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebPartPages;

    public class TabConfigurationEditorPart : EditorPart
    {
        // The deployment path of the User Control
        const string TabControlConfigurationPath = @"~/_CONTROLTEMPLATES/EditorPartTab/TabEditorWebPart/TabConfigurationEditorPartUserControl.ascx";

        // The User Control Object ID
        const string UserControlID = "OperationsUserControl";

        // Declare a reference to the User Control
        TabConfigurationEditorPartUserControl configuratorControl;

        // Declare a reference to the Tab Editor Web Part
        private TabEditorWebPart tabEditorWebPart;

        public TabConfigurationEditorPart()
        {
            this.Title = "Tab Configuration";
        }

        public List<TabData> TabList { get; set; }

        public override bool ApplyChanges()
        {
            this.tabEditorWebPart = this.WebPartToEdit as TabEditorWebPart;

            // Set the Web Part's TabList
            this.tabEditorWebPart.TabList = this.TabList;

            // Call the webpart's personalization dirty            
            this.tabEditorWebPart.SaveChanges();
            return true;
        }

        public override void SyncChanges()
        {
            // sync with the new property changes here
            EnsureChildControls();
            
            this.tabEditorWebPart = this.WebPartToEdit as TabEditorWebPart;

            // Read the TabList from the Web Part
            this.TabList = this.tabEditorWebPart.TabList;
        }

        protected override void CreateChildControls()
        {
            // Get a reference to the Edit Tool Pane.
            ToolPane pane = this.Zone as ToolPane;
            if (pane != null)
            {
                // Disable the validation on Cancel Button of ToolPane
                pane.Cancel.CausesValidation = false;
                pane.Cancel.Click += new EventHandler(Cancel_Click);
            }

            // Load the User Control and it to the Controls Collection of the Editor Part
            this.configuratorControl =
                this.Page.LoadControl(TabConfigurationEditorPart.TabControlConfigurationPath) as TabConfigurationEditorPartUserControl;
            this.configuratorControl.ID = TabConfigurationEditorPart.UserControlID;
            this.Controls.Add(configuratorControl);
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            // On Cancel rollback all the changes by restoring the Original List
            if (this.configuratorControl.OriginalTabList != null)
            {
                this.TabList = this.configuratorControl.OriginalTabList;
                this.ApplyChanges();
            }
        }
    }
}
