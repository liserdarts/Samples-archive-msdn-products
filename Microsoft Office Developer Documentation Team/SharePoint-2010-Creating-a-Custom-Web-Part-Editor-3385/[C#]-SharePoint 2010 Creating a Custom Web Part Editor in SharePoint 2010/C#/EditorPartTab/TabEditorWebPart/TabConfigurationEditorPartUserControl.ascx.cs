namespace MSDN.SharePoint.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class TabConfigurationEditorPartUserControl : System.Web.UI.UserControl
    {
        private TabConfigurationEditorPart parentEditorPart;
        
        public List<TabData> TabList;

        const string TabStorageViewStateId = "TabStorageViewState";

        public List<TabData> OriginalTabList
        {
            get
            {
                // Retrieve the Original Tab List from the ViewState
                List<TabData> retValue = null;
                retValue = this.ViewState[TabStorageViewStateId] as List<TabData>;
                return retValue;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.parentEditorPart = this.Parent as TabConfigurationEditorPart;

            // Call Sync Changes on the Editor Part, of it to read the Tab List from the Web Part
            this.parentEditorPart.SyncChanges();
            this.TabList = this.parentEditorPart.TabList;

            // Check if this is the first Page_Load of the control
            if (this.hiddenFieldDetectRequest.Value == "0")
            {
                this.hiddenFieldDetectRequest.Value = "1";

                // Save the Original Tab List to the Control's ViewState
                this.SaveOriginalTabListToViewState();
                
                // Bind the Tab List to the drop down
                this.PopulateConfiguredTabsDropDown();
            }
        }

        protected void dropDownConfiguredTabs_OnTextChanged(object sender, EventArgs e)
        {
            this.panelTabItem.Visible = false;

            // Enable the Edit and Delete buttons if any Tab is selected in the dropdown
            if(dropDownConfiguredTabs.SelectedIndex == 0)
            {
                this.buttonDeleteTab.Enabled = false;
                this.buttonEditTab.Enabled = false;
            }
            else
            {
                this.buttonDeleteTab.Enabled = true;
                this.buttonEditTab.Enabled = true;
            }
        }
        
        protected void ButtonAddNewTab_OnClick(object sender, EventArgs e)
        {
            // Show the Panel and Clear Title and Content
            this.panelTabItem.Visible = true;
            this.textBoxTitle.Text = string.Empty;
            this.htmlBody.Text = string.Empty;
            this.buttonSave.Visible = true;
            this.buttonSaveOnEdit.Visible = false;
        }

        protected void ButtonSaveOnEdit_Click(object sender, EventArgs e)
        {
            // If the Selected Item's text in the tab list drop down is not equal to 
            // the value in the Title Text box, then the Title has been modified. 
            // Check if the modified Title already exists.
            if((this.dropDownConfiguredTabs.SelectedItem.Text != this.textBoxTitle.Text) &&
                (this.TabList.Where(tab => tab.Title == this.textBoxTitle.Text).Count() > 0 ))
            {
                // If the edited Title of the existing Tab matches with an already existing one, fail the validation.
                this.customValidatorTextBoxTitle.IsValid = false;
            }
            else
            {
                // If the Title wasn't edited remove the existing tab.
                // This line wouldn't do anything if a tab with the edited Title doesn't already exist.
                this.TabList.RemoveAll(link => link.Title == this.dropDownConfiguredTabs.SelectedItem.Text);

                // Create a new Tab with the edited Title and Content and add it to the collection
                this.SaveTab();

                // Rebind the Dropdown
                this.PopulateConfiguredTabsDropDown();

                // Call Apply Changes to display the changes on the Web Part
                this.ApplyChanges();
            }
        }

        protected void ButtonEditTab_OnClick(object sender, EventArgs e)
        {
            // Hide the Save button and Show the SaveOnEdit button.
            this.buttonSave.Visible = false;
            this.buttonSaveOnEdit.Visible = true;

            // Show the Tab panel
            this.panelTabItem.Visible = true;

            // Find the Tab selected to Edit, and set the Title and Content Text box values.
            TabData selectedLink = this.TabList.First(tab => tab.Title == this.dropDownConfiguredTabs.SelectedItem.Text);
            this.textBoxTitle.Text = selectedLink.Title;
            this.htmlBody.Text = selectedLink.Content;
        }

        protected void ButtonDeleteTab_OnClick(object sender, EventArgs e)
        {
            // Remove the Title from the Drop Down
            this.TabList.RemoveAll(tab => tab.Title == this.dropDownConfiguredTabs.SelectedItem.Text);
            
            // Re-bind the Drop down
            this.PopulateConfiguredTabsDropDown();

            // Apply the changes to the Web Part
            this.ApplyChanges();
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            // Hide the Tab panel
            this.panelTabItem.Visible = false;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            // Check if a Tab with same Title already exists.
            IEnumerable<TabData> matches = this.TabList.Where(tab => (tab.Title == this.textBoxTitle.Text));

            if (matches.Count() > 0)
            {
                // Fail the validation if a Tab with the same Title already exists
                this.customValidatorTextBoxTitle.IsValid = false;
            }
            else
            {
                // If no Tab exists with the same Title, Save and Apply the changes.
                this.SaveTab();
                this.PopulateConfiguredTabsDropDown();
                this.ApplyChanges();
            }
        }

        private void PopulateConfiguredTabsDropDown()
        {
            this.dropDownConfiguredTabs.Items.Clear();
            this.dropDownConfiguredTabs.Items.Add("Select one");

            // Add the Tab titles to the Tab list drop down.
            if (this.TabList != null)
            {
                for (int index = 0; index < TabList.Count; index++)
                {
                    this.dropDownConfiguredTabs.Items.Add(TabList[index].Title.ToString());
                }
            }

            // Hide the Edit and Delete buttons, and Tab panel
            this.buttonDeleteTab.Enabled = false;
            this.buttonEditTab.Enabled = false;
            this.panelTabItem.Visible = false;
        }

        private void SaveTab()
        {
            // Create a new Tab, and save it to the Tab list collection
            TabData tab = new TabData();
            tab.Title = this.textBoxTitle.Text;
            tab.Content = this.htmlBody.Text;
            this.TabList.Add(tab);
        }

        private void ApplyChanges()
        {
            // Call the ApplyChanges method of the Parent Editor Part class.
            this.parentEditorPart.ApplyChanges();
        }

        private void SaveOriginalTabListToViewState()
        {
            // Save the Tab list that was already retrieved 
            // from the Web Part storage to View State.
            if (this.TabList != null)
            {
                this.ViewState[TabStorageViewStateId] = this.TabList;
            }
        }

        //private void SaveOriginalTabListToViewState()
        //{
        //    // Retrieve the Tab list from the Web Part,
        //    // by calling the Parent Editor Part class's SyncChanges method.
        //    this.parentEditorPart.SyncChanges();

        //    // Save it to View State.
        //    if (parentEditorPart.TabList != null)
        //    {
        //        this.ViewState[TabStorageViewStateId] = this.TabList;
        //    }
        //}
    }
}
