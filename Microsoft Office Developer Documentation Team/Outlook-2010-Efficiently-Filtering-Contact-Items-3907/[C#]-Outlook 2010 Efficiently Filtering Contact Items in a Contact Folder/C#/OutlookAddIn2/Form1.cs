using System;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookAddIn2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ShowDefaultReport();
        }

        /// <summary>Handles the Click event for the form's Reset button.</summary>
        private void resetButton_Click(object sender, EventArgs e)
        {
            ShowDefaultReport();
        }

        /// <summary>Displays the contact data with the default columns and
        /// does not filter the rows.</summary>
        private void ShowDefaultReport()
        {
            Outlook.Folder opportunities = Globals.ThisAddIn.OpportunitiesFolder;
            if (opportunities != null)
            {
                Outlook.Table contacts = opportunities.GetTable();

                ShowContactReport(contacts);
            }
            else
            {
                ShowContactReport(null);
            }
        }

        /// <summary>Displays the data from the specified Table object.</summary>
        /// <param name="table">The Table containing the contact data to display.
        /// </param>
        private void ShowContactReport(Outlook.Table table)
        {
            this.SuspendLayout();

            // Free memory for any old controls.
            foreach (Control c in this.reportPanel.Controls)
            {
                if (c != null)
                {
                    c.Dispose();
                }
            }

            this.reportPanel.Controls.Clear();

            if (table != null)
            {
                // Add the new control with updated contact information.
                this.reportPanel.Controls.Add(NewDataGrid(table));
            }
            else
            {
                this.reportPanel.Controls.Add(ErrorMessage());
            }

            this.ResumeLayout();
            this.Refresh();
        }

        /// <summary>Creates a DataGridView control that displays the contact
        /// information contained in the specified Table object.</summary>
        /// <param name="table">The Table containing the contact data to display.
        /// </param>
        /// <returns>The new DataGridView.</returns>
        private DataGridView NewDataGrid(Outlook.Table table)
        {
            DataGridView dataGrid = new DataGridView();

            // For each column in the table, add a column to the control. Note that the
            // Table column collection uses 1-based indexing; whereas, the DataGridView
            // column collection uses 0-based indexing.
            dataGrid.ColumnCount = table.Columns.Count;
            for (int i = 1; i <= table.Columns.Count; i++)
            {
                Outlook.Column tableColumn = table.Columns[i];
                DataGridViewColumn dataColumn = dataGrid.Columns[i - 1];

                dataColumn.Name = tableColumn.Name;
                dataColumn.HeaderText = Constants.GetDisplayName(tableColumn.Name);
                dataColumn.ValueType = Constants.GetDataType(tableColumn.Name);
                dataColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                // Format the Purchase Estimate property data as currency.
                if (dataColumn.HeaderText == Constants.purchaseEstimateDisplayName)
                {
                    dataColumn.DefaultCellStyle.Format = "C";
                }
            }

            // For each row in the table, add the contact data to the control.
            table.MoveToStart();
            while (!table.EndOfTable)
            {
                Outlook.Row contact = table.GetNextRow();
                object[] contactData = contact.GetValues();

                // The ordering of the contact property values returned by the 
                // Table's GetValues method matches the ordering of the column 
                // information returned by the Table's Columns property.
                dataGrid.Rows.Add(contactData);
            }

            // Modify the control's display and behavior properties.
            dataGrid.AutoSize = true;
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.BorderStyle = BorderStyle.FixedSingle;

            dataGrid.ReadOnly = true;

            return dataGrid;
        }

        private Control ErrorMessage()
        {
            Label message = new Label();

            message.Text = "No data to display." + Environment.NewLine +
                "The Opportunities contacts folder does not exist.";

            message.Dock = DockStyle.Fill;

            return message;
        }

        /// <summary>Handles the Click event for the form's Filter button.</summary>
        /// <remarks>Displays the contact data with the default columns and
        /// filters the rows based on the CompanyName property.</remarks>
        private void filterButton_Click(object sender, EventArgs e)
        {
            Outlook.Folder opportunities = Globals.ThisAddIn.OpportunitiesFolder;
            if (opportunities != null)
            {
                string criteria = "[CompanyName] = 'Adventure Works'";
                Outlook.Table contacts = opportunities.GetTable(criteria);

                ShowContactReport(contacts);
            }
            else
            {
                ShowContactReport(null);
            }
        }

        /// <summary>Handles the Click event for the form's Customize Columns button.
        /// </summary>
        /// <remarks>Displays the contact data with the addition of the CustomerID 
        /// built-in property and the five Sales Opportunity custom properties. Does 
        /// not filter the rows.</remarks>
        private void customizeColumnsButton_Click(object sender, EventArgs e)
        {
            Outlook.Folder opportunities = Globals.ThisAddIn.OpportunitiesFolder;
            if (opportunities != null)
            {
                Outlook.Table contacts = opportunities.GetTable();
                AddCustomColumns(contacts);

                ShowContactReport(contacts);
            }
            else
            {
                ShowContactReport(null);
            }
        }

        /// <summary>For a Table object, removes the CreationTime and LastModificationTime
        /// properties and adds the CustomerID built-in property and the five Sales
        /// Opportunity custom properties.</summary>
        private void AddCustomColumns(Outlook.Table contacts)
        {
            for (int i = contacts.Columns.Count; i > 0; i--)
            {
                if (contacts.Columns[i].Name == Constants.creationTimeDisplayName ||
                    contacts.Columns[i].Name == Constants.lastModificationTimeDisplayName)
                {
                    contacts.Columns.Remove(i);
                }
            }

            contacts.Columns.Add(Constants.customerIDProperRef);

            contacts.Columns.Add(Constants.encounterDatePropTag);
            contacts.Columns.Add(Constants.purchaseEstimatePropTag);
            contacts.Columns.Add(Constants.salesRepPropTag);
            contacts.Columns.Add(Constants.salesValuePropTag);
            contacts.Columns.Add(Constants.tradeShowPropTag);
        }

        /// <summary>Handles the Click event for the form's Customize Columns button.
        /// </summary>
        /// <remarks>Displays the contact data with the addition of the CustomerID 
        /// built-in property and the five Sales Opportunity custom properties. Filters 
        /// the rows based on the custom Sales Rep property.</remarks>
        private void filterCustomColumnsButton_Click(object sender, EventArgs e)
        {
            Outlook.Folder opportunities = Globals.ThisAddIn.OpportunitiesFolder;
            if (opportunities != null)
            {
                string criteria = string.Format(
                    "[{0}] = 'Karen Berg'", Constants.salesRepDisplayName);
                Outlook.Table contacts = opportunities.GetTable(criteria);
                AddCustomColumns(contacts);

                ShowContactReport(contacts);
            }
            else
            {
                ShowContactReport(null);
            }
        }
    }
}
