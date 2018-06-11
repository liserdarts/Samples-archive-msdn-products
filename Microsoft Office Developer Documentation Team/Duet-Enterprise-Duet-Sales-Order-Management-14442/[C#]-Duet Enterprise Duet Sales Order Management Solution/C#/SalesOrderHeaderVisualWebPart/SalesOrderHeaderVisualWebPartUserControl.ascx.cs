using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using DuetSalesOrderSolution.Customizations;

namespace DuetSalesOrderSolution.SalesOrderHeaderVisualWebPart
{
    public partial class SalesOrderHeaderVisualWebPartUserControl : UserControl
    {
        public SalesOrderHeaderVisualWebPartUserControl()
        {
            this.PreRender += Page_PreRender;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SPContext.Current.FormContext.OnSaveHandler += FormSaveHandler;
            
            PopulateCurrenciesDropDown();

            // Prevent updating of read-only fields.
            if (SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
            {
                ffldSalesOrderNumber.ControlMode = SPControlMode.Display;
                ffldNetValue.ControlMode = SPControlMode.Display;
            }
        }

        /// <summary>
        /// Populate currency drop-down listbox with the items from list.
        /// </summary>
        private void PopulateCurrenciesDropDown()
        {
            SPList list = SPContext.Current.Web.Lists.TryGetList(CurrencyList.ListTitle);

            if (list != null && dropDownCurrency.Items.Count != list.Items.Count)
            {
                dropDownCurrency.Items.Clear();

                foreach (SPListItem listItem in list.Items)
                {
                    ListItem item = new ListItem(listItem[CurrencyList.ColumnNames[0]].ToString(),
                            listItem[CurrencyList.ColumnNames[1]].ToString());
                    dropDownCurrency.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Make appropriate currency selection based on the data from LOB.
        /// </summary>
        private void MakeCurrencySelection()
        {
            if (ffldCurrency != null && ffldCurrency.Value != null && !String.IsNullOrEmpty(ffldCurrency.Value.ToString()) 
                    && ffldCurrency.Value.ToString() != dropDownCurrency.SelectedValue)
            {
                dropDownCurrency.SelectedValue = ffldCurrency.Value.ToString();
            }
        }

        /// <summary>
        /// Make appropriate delivery date selection based on the data from LOB.
        /// </summary>
        private void MakeDeliveryDateSelection()
        {
            if (ffldDeliveryDate != null && ffldDeliveryDate.Value != null && !String.IsNullOrEmpty(ffldDeliveryDate.Value.ToString()) 
                    && ffldDeliveryDate.Value.ToString() != dateTimeDeliveryDate.SelectedDate.ToString())
            {
                DateTime dateTime = DateTime.Parse(ffldDeliveryDate.Value.ToString());
                dateTimeDeliveryDate.SelectedDate = dateTime;
            }
        }

        /// <summary>
        /// Invoked before a page is rendered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
                {
                    MakeCurrencySelection();
                    MakeDeliveryDateSelection();
                }

                // Control visibility for drop downs and the corresponding fields.
                if (SPContext.Current.FormContext.FormMode 
                    == Microsoft.SharePoint.WebControls.SPControlMode.Display)
                {
                    dropDownCurrency.Visible = false;
                    dateTimeDeliveryDate.Visible = false;
                }
                else
                {
                    ffldDeliveryDate.Visible = false;
                    ffldCurrency.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles save event for form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void FormSaveHandler(object sender, EventArgs args)
        {
            SPContext.Current.Item["Currency"] = dropDownCurrency.SelectedValue;
            SPContext.Current.Item["DeliveryDate"] = dateTimeDeliveryDate.SelectedDate.ToString();
            
            SaveButton.SaveItem(SPContext.Current, false, String.Empty);
        }
    }
}
