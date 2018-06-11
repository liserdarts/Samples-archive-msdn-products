using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.Threading;

namespace MSDN.SharePoint.Samples.ProductBrowser
{
    public partial class ProductDetails : UserControl
    {
        SynchronizationContext _syncContext; 
        private int _productID = -1;
        ListItem _product;

        public StatusBarControl StatusBarPanel;

        public ProductDetails()
        {
            // Required to initialize variables
            InitializeComponent();

            _syncContext = SynchronizationContext.Current;
        }

        // reset the form
        public void ResetProductDetails()
        {
            _productID = -1;

            ProductNumberTextBox.Text = string.Empty;
            ProductNameTextBox.Text = string.Empty;
            ProductPriceTextBox.Text = string.Empty;
            ProductCategortyTextBox.Text = string.Empty;

            DisableModificationButtons();
        }

        // init the form with the selected product
        public void LoadProductDetails(int id, string productName, string productNumber, string price, string category)
        {
            _productID = id;

            ProductNumberTextBox.Text = productNumber;
            ProductNameTextBox.Text = productName;
            ProductPriceTextBox.Text = price;
            ProductCategortyTextBox.Text = category;

            EnableModificationButtons();
        }

        private void EnableModificationButtons()
        {
            UpdateProductButton.IsEnabled = true;
        }
        private void DisableModificationButtons()
        {
            UpdateProductButton.IsEnabled = false;
        }

        // event that triggers when the category changes
        public event ProductChangedEventHandler ProductChanged;
        protected virtual void OnProductChanged(ProductChangedEventArgs e)
        {
            if (ProductChanged != null)
                ProductChanged(this, e);
        }

        // handlers for when the add button is clicked
        private void AddProductButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProductEditor dialog = new ProductEditor();
            dialog.SetForAdd();
            dialog.Closed += new EventHandler(ProductEditorDialog_AddClosed);
            dialog.Show();
        }
        protected void ProductEditorDialog_AddClosed(object sender, EventArgs args)
        {
            ProductEditor dialog = (ProductEditor)sender;

            // stop if they clicked cancel
            if (dialog.DialogResult == false)
                return;

            StatusBarPanel.BeginUpdateMessage("Creating new item...");

            List products = Global.GetClientContext().Web.Lists.GetByTitle("Products");
            ListItemCreationInformation newProductInfo = new ListItemCreationInformation();
            
            ListItem newProduct = products.AddItem(newProductInfo);
            newProduct["Title"] = dialog.ProductNameTextBox.Text;
            newProduct["Product_x0020_Number"] = dialog.ProductNumberTextBox.Text;
            newProduct["Price"] = dialog.ProductPriceTextBox.Text;
            FieldLookupValue fieldValue = new FieldLookupValue();
            foreach (ListItem item in Global.ProductCategories)
                if (item["Title"].ToString() == dialog.ProductCategortyComboBox.SelectedItem.ToString())
                {
                    fieldValue.LookupId = item.Id;
                }
            newProduct["Category"] = fieldValue;

            newProduct.Update();
            Global.GetClientContext().ExecuteQueryAsync(OnSucceededListenerCreateProduct, OnFailListener);
        }
        private void OnSucceededListenerCreateProduct(object sender, ClientRequestEventArgs args)
        {
            _syncContext.Post(OnSucceedCreateProduct, null);
        }
        private void OnSucceedCreateProduct(object sender)
        {
            // hide update message
            StatusBarPanel.EndUpdateMessage();

            OnProductChanged(new ProductChangedEventArgs(null,null,null,null));
        }

        // handers for when the update button is clicked
        private void UpdateProductButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProductEditor dialog = new ProductEditor();
            dialog.SetForUpdate();
            dialog.Closed += new EventHandler(ProductEditorDialog_UpdateClosed);

            // init fields
            dialog.InitFields(ProductNameTextBox.Text,
                ProductNumberTextBox.Text,
                ProductPriceTextBox.Text,
                ProductCategortyTextBox.Text);

            dialog.Show();
        }
        protected void ProductEditorDialog_UpdateClosed(object sender, EventArgs args)
        {
            ProductEditor dialog = (ProductEditor)sender;

            // stop if they clicked cancel
            if (dialog.DialogResult == false)
                return;

            StatusBarPanel.BeginUpdateMessage("Updating product details...");

            // get the item being updated
            _product = Global.GetClientContext().Web.Lists.GetByTitle("Products").GetItemById(_productID);
            _product["Title"] = dialog.ProductNameTextBox.Text;
            _product["Product_x0020_Number"] = dialog.ProductNumberTextBox.Text;
            _product["Price"] = dialog.ProductPriceTextBox.Text;

            FieldLookupValue fieldValue = new FieldLookupValue();
            foreach (ListItem item in Global.ProductCategories)
                if (item["Title"].ToString() == dialog.ProductCategortyComboBox.SelectedItem.ToString())
                {
                    fieldValue.LookupId = item.Id;
                }
            _product["Category"] = fieldValue;

            _product.Update();
            Global.GetClientContext().ExecuteQueryAsync(OnSucceededListenerUpdateProducts, OnFailListener);
        }
        private void OnSucceededListenerUpdateProducts(object sender, ClientRequestEventArgs args)
        {
            _syncContext.Post(OnSucceedUpdateProducts, null);
        }
        private void OnSucceedUpdateProducts(object sender)
        {
            // hide update message
            StatusBarPanel.EndUpdateMessage();

            ProductChangedEventArgs e = new ProductChangedEventArgs(
                ProductNameTextBox.Text,
                _product["Title"].ToString(),
                ProductCategortyTextBox.Text,
                ((FieldLookupValue)_product["Category"]).LookupValue);

            OnProductChanged(e);
        }

        // general fail listener that shows exception message in message box
        private void OnFailListener(object sender, ClientRequestFailedEventArgs args)
        {
            _syncContext.Post(OnFailUpdateUI, args.Exception);
        }
        private void OnFailUpdateUI(object sender)
        {
            Exception ex = sender as Exception;
            MessageBox.Show("Error: " + ex.Message);
        }
    }
}