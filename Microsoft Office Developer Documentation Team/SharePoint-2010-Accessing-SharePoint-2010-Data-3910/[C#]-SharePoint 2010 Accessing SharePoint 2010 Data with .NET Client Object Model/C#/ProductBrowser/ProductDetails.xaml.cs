using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SP = Microsoft.SharePoint.Client;
using System.Threading;
using System.Windows.Threading;

namespace ProductBrowser
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : UserControl
    {
        SP.FieldLookupValue selectedCategory;
        public StatusBarControl StatusBarPanel;

        public ProductDetails()
        {
            this.InitializeComponent();
        }

        public event ProductChangedEventHandler ProductChanged;
        protected virtual void OnProductChanged(ProductChangedEventArgs e)
        {
            if (ProductChanged != null)
                ProductChanged(this, e);
        }

        private void AddProductButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProductEditor dialog = new ProductEditor();
            dialog.Closed += new EventHandler(ProductEditorDialog_AddClosed);

            dialog.DataContext = null;

            // show the dialog
            dialog.SetForAdd();
            dialog.ShowDialog();
        }
        private void ProductEditorDialog_AddClosed(object sender, EventArgs e)
        {
            ProductEditor dialog = (ProductEditor)sender;

            // stop if they clicked cancel
            if (dialog.DialogResult == false)
                return;

            StatusBarPanel.BeginUpdateMessage("Creating new item...");

            SP.List products = Globals.ClientContext.Web.Lists.GetByTitle("Products");
            SP.ListItemCreationInformation newProductInfo = new SP.ListItemCreationInformation();

            SP.ListItem newProduct = products.AddItem(newProductInfo);
            newProduct["Title"] = dialog.ProductNameTextBox.Text;
            newProduct["Product_x0020_Number"] = dialog.ProductNumberTextBox.Text;
            newProduct["Price"] = dialog.ProductPriceTextBox.Text;
            SP.FieldLookupValue fieldValue = new SP.FieldLookupValue();
            foreach (SP.ListItem item in Globals.ProductCategories)
                if (item["Title"].ToString() == ((SP.ListItem)dialog.ProductCategoryComboBox.SelectedItem)["Title"].ToString())
                    fieldValue.LookupId = item.Id;
            newProduct["Category"] = fieldValue;

            newProduct.Update();
            Globals.ClientContext.ExecuteQuery();

            this.Dispatcher.BeginInvoke(new Action(OnProducteditorAddUIUpdater), DispatcherPriority.Normal);
        }
        private void OnProducteditorAddUIUpdater()
        {
            StatusBarPanel.EndUpdateMessage();
            OnProductChanged(new ProductChangedEventArgs(null, null, null, null));
        }

        private void UpdateProductButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProductEditor dialog = new ProductEditor();
            dialog.Closed += new EventHandler(ProductEditorDialog_UpdateClosed);

            // wire up the context of the dialog to be the same as this editor
            dialog.DataContext = this.DataContext;

            // show the dialog
            dialog.SetForUpdate();
            dialog.ShowDialog();
        }

        protected void ProductEditorDialog_UpdateClosed(object sender, EventArgs e)
        {
            ProductEditor dialog = (ProductEditor)sender;

            // stop if they clicked cancel
            if (dialog.DialogResult == false)
                return;

            StatusBarPanel.BeginUpdateMessage("Updateing item...");
            
            selectedCategory = new SP.FieldLookupValue();
            foreach (SP.ListItem item in Globals.ProductCategories)
                if (item["Title"].ToString() == ((SP.ListItem)dialog.ProductCategoryComboBox.SelectedItem)["Title"].ToString())
                    selectedCategory.LookupId = item.Id;

            ThreadPool.QueueUserWorkItem(OnProducteditorUpdateWorker, DataContext);
        }
        private void OnProducteditorUpdateWorker(object state)
        {
            SP.ListItem product = state as SP.ListItem;

            // get the category selected
            product["Category"] = selectedCategory;

            // update the field
            product.Update();
            Globals.ClientContext.ExecuteQuery();

            // fire the UI work on another thread
            this.Dispatcher.BeginInvoke(new Action(OnProducteditorUpdateUIUpdater), DispatcherPriority.Normal);
        }
        private void OnProducteditorUpdateUIUpdater()
        {
            SP.ListItem product = DataContext as SP.ListItem;
            ProductChangedEventArgs e = new ProductChangedEventArgs(
                null,
                product["Title"].ToString(),
                null,
                ((SP.FieldLookupValue)product["Category"]).LookupValue);

            StatusBarPanel.EndUpdateMessage();
            OnProductChanged(e);
        }

        private void ProductCategoryTextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
                UpdateProductButton.IsEnabled = false;
            else
                UpdateProductButton.IsEnabled = true;
        }
    }
}