using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace DuetSalesOrderSolution.SalesOrderItemVisualWebPart
{   
    public partial class SalesOrderItemVisualWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SPContext.Current.FormContext.OnSaveHandler += FormSaveHandler;

            // Prevent updating of read-only fields.
            if (SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
            {
                ffldItemNumber.ControlMode = SPControlMode.Display;
            }

            // Auto-populate the Sales Order Header value when a new related item is added.
            if (!this.IsPostBack && SPContext.Current.FormContext.FormMode == SPControlMode.New)
            {
                MakeSalesOrderSclKeySelection();
            }
        }

        /// <summary>
        /// Sets the value for the related Sales Order Header.
        /// </summary>
        public void MakeSalesOrderSclKeySelection()
        {
            string salesOrderKey = this.Page.Request.QueryString["SalesOrderSclKey"];

            if (!String.IsNullOrEmpty(salesOrderKey))
            {
                computedSalesOrderHeader.Visible = true;
                ffldSalesOrderSclKey.Visible = false;
                computedSalesOrderHeader.Text = salesOrderKey;
            }
        }

        /// <summary>
        /// Handles save event for form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void FormSaveHandler(object sender, EventArgs args)
        {
            if (!ffldSalesOrderSclKey.Visible)
            {
                SPContext.Current.Item["SalesOrderSclKey"] = computedSalesOrderHeader.Text;
            }

            SaveButton.SaveItem(SPContext.Current, false, String.Empty);
        }
    }
}
