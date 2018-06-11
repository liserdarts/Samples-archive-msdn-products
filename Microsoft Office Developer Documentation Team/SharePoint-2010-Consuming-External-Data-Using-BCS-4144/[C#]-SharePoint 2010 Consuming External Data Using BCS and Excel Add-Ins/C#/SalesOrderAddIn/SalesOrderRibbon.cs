namespace SalesOrderAddIn
{
    using System;
    using System.Data;
    using Microsoft.Office.Tools.Excel;
    using Microsoft.Office.Tools.Ribbon;
    using Excel = Microsoft.Office.Interop.Excel;

    /// <summary>
    /// Implements functionality for Ribbon using which enduser
    /// will interact with the BCS
    /// </summary>
    public partial class SalesOrderRibbon
    {
        SalesDataManager lobData = null;
        DataTable salesOrderTable = null;
        DataTable salesOrderLinesTable = null;
        Worksheet worksheet = null;
        ListObject listSalesOrderHeaders = null;

        /// <summary>
        /// Called when Ribbon is loaded. Create Catalog and initializes
        /// table structures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesOrderRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            try
            {
                // if CheckDependentDataSolution = true, 
                // the expected DependentDataSolution is installed properly 
                if (SalesDataManager.CheckDependentDataSolution())
                {

                    lobData = new SalesDataManager();

                    salesOrderTable = lobData.GetSalesOrderHeaderItems();

                    PopulateSalesOrderNumbers();

                }
                // if CheckDependentDataSolution = false, 
                // the expected DependentDataSolution is not instaled properly 
                // this addin should not use any data from BCS
                else
                {
                    UpdateLines.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "An error occured while loading the ribbon. Stacktrace: " +
                    ex.StackTrace);
            }
        }

        /// <summary>
        /// Bind the SalesOrderLines instances to the Excel Sheet sheet1
        /// </summary>
        /// <param name="orderHeaders"></param>
        public void BindSalesOrderLines(DataTable orderHeaders)
        {
            if (listSalesOrderHeaders == null)
            {
                Excel.Worksheet tmpSheet =
                    (Excel.Worksheet)Globals.ThisAddIn.Application.Worksheets[1];

                // Get the inner excel worksheet object
                worksheet = Worksheet.GetVstoObject(tmpSheet);
                listSalesOrderHeaders = worksheet.Controls.AddListObject(
                    worksheet.Range["$A$1", "$B$1"], "salesorderlines");

                this.listSalesOrderHeaders.AutoSetDataBoundColumnHeaders = true;
                this.listSalesOrderHeaders.ShowAutoFilter = false;
            }

            if (!IsSheetInEditMode())
            {
                this.listSalesOrderHeaders.SetDataBinding(orderHeaders);
                this.worksheet.Columns.AutoFit();
            }
        }

        /// <summary>
        /// Checks if cell is in edit mode. Will not rebind unlesss changes commited.
        /// </summary>
        /// <returns>true if in edit mode, else false.</returns>
        private bool IsSheetInEditMode()
        {
            Microsoft.Office.Core.CommandBarControl menu =
                Globals.ThisAddIn.Application.CommandBars["Worksheet Menu Bar"].FindControl(
                1,
                18,
                System.Type.Missing,
                System.Type.Missing,
                true);

            if (menu != null)
            {
                if (!menu.Enabled)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "WorkSheet is in edit mode. Commit your changes by using Enter key using Ribbon controls.");
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;    
        }
        /// <summary>
        /// Updates the SalesOrderLines for an order and refreshes the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLineItems(object sender, RibbonControlEventArgs e)
        {
            // Updates lines
            lobData.UpdateSalesOrderLineItems();

            // Get the updated data from backend and refreshes the UI
            int orderId = GetSalesOrderId(cmbSalesOrderNumbers.Text);
            salesOrderLinesTable = lobData.GetSalesOrderLineItems(orderId);
            this.BindSalesOrderLines(salesOrderLinesTable);
            salesOrderTable = lobData.GetSalesOrderHeaderItems();
            txtOrderAmount.Text =
                GetSalesOrderTotalAmount(cmbSalesOrderNumbers.Text);
        }

        /// <summary>
        /// Bind the SalesOrderLines for a SalesOrder to the Excel sheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSalesOrderDetails(object sender, RibbonControlEventArgs e)
        {
            RibbonComboBox control = (RibbonComboBox)sender;

            if (sender != null)
            {
                txtOrderAmount.Text = GetSalesOrderTotalAmount(control.Text);

                int orderId = GetSalesOrderId(control.Text);
                salesOrderLinesTable =
                    lobData.GetSalesOrderLineItems(orderId);
                this.BindSalesOrderLines(salesOrderLinesTable);
            }
        }

        /// <summary>
        /// Populate Combo with SalesOrder Numbers
        /// </summary>
        private void PopulateSalesOrderNumbers()
        {
            RibbonDropDownItem item = null;
            this.cmbSalesOrderNumbers.Items.Clear();

            for (int i = 0; i < salesOrderTable.Rows.Count; i++)
            {
                item = new RibbonDropDownItem();
                item.Label =
                    salesOrderTable.Rows[i]["SalesOrderNumber"].ToString();
                this.cmbSalesOrderNumbers.Items.Add(item);
            }
        }

        /// <summary>
        /// Helper to get the SubTotal column for a SalesOrderOrder
        /// </summary>
        /// <param name="salesOrderNumber">Sales Order Number</param>
        /// <returns></returns>
        private string GetSalesOrderTotalAmount(string salesOrderNumber)
        {
            DataRow[] rows =
                salesOrderTable.Select(
                "SalesOrderNumber = '" + salesOrderNumber + "'");

            if (rows.Length > 0)
            {
                double amount = Convert.ToDouble(rows[0]["SubTotal"]);

                return Math.Round(amount, 2).ToString();
            }

            return String.Empty;
        }

        /// <summary>
        /// Gets the SalesOrderId from SalesOrderNumber for a SalesOrder
        /// </summary>
        /// <param name="salesOrderNumber">Sales Order Number</param>
        /// <returns></returns>
        private int GetSalesOrderId(string salesOrderNumber)
        {
            DataRow[] rows =
                salesOrderTable.Select(
                "SalesOrderNumber = '" + salesOrderNumber + "'");

            if (rows.Length > 0)
            {
                return Convert.ToInt32(rows[0]["SalesOrderId"]);
            }

            return 0;
        }
    }
}
