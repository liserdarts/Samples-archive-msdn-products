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
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using System.Linq;

namespace AddressBookWebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        public static string stringAll = "All";

        // Create button links for filtering alphabetically
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ButtonRepeater.DataSource = new string[] { stringAll }.Concat(Enumerable.Range(0, 26).Select(i => ((char)('A' + i)).ToString()));
            ButtonRepeater.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Indicate that all contacts are displayed in list box. 
                this.ViewState["filter"] = stringAll;

                // Fill the list box for the first time.
                this.FillListBox(null);
            }
        }

        // Event handler for click to any of the filter buttons.
        public void ClickFilter(object sender, CommandEventArgs e)
        {
            var text = e.CommandArgument.ToString();
            this.FillListBox(text);
            ViewState["filter"] = text;
        }

        // Update the list box after an item is inserted.
        protected void frmDetail_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            this.FillListBoxPreserveFilter();
        }

        // Update the list box after an item is updated, selecting the updated record.
        protected void frmDetail_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            this.FillListBoxPreserveFilter();

            // Set the selected item to be the updated record, if possible.
            if (null != lstContactNames.Items.FindByValue(frmDetail.DataKey.Value.ToString()))
            {
                lstContactNames.SelectedValue = frmDetail.DataKey.Value.ToString();
            }
        }

        // Update the list box after an item is deleted.
        protected void frmDetail_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            this.FillListBoxPreserveFilter();
        }

        // Display the page to import a csv file.
        protected void btnImportPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Import.aspx");
        }

        // Fill the list box using data from the table.
        private void FillListBox(string prefix)
        {
            // Clear the existing data.
            lstContactNames.Items.Clear();

            try
            {
                // Retrieve list of contacts.
                List<ContactEntity> list = DataLayer.GetContactsByPrefix(prefix == stringAll ? null : prefix);

                // Add contacts to list box, using FirstName and LastName.
                foreach (var entity in list)
                {
                    ListItem nameItem = new ListItem();
                    nameItem.Text = entity.FirstName + " " + entity.LastName;
                    nameItem.Value = entity.RowKey;
                    this.lstContactNames.Items.Add(nameItem);
                }

                if (list.Count > 0)
                {
                    // Set first selection.
                    this.lstContactNames.SelectedIndex = 0;
                    this.lstContactNames.Enabled = true;
                    this.frmDetail.Visible = true;
                }
                else
                {
                    // Indicate that there's no data. 
                    ListItem noDataItem = new ListItem();
                    noDataItem.Text = "<No entries>";
                    noDataItem.Value = "0";
                    this.lstContactNames.Items.Add(noDataItem);
                    this.lstContactNames.Enabled = false;
                    this.frmDetail.Visible = false;
                }
            }
            catch (Exception e)
            {
                ListItem errorItem = new ListItem();
                errorItem.Text = "<Error: >" + e.Message;
                errorItem.Value = "0";
                this.lstContactNames.Items.Add(errorItem);
                this.lstContactNames.Enabled = false;
                this.frmDetail.Visible = false;
            }
        }

        // Fill the list box, maintaining the same filter if a filter has been specified.
        private void FillListBoxPreserveFilter()
        {
            this.FillListBox(this.ViewState["filter"] as string);
        }
    }
}
