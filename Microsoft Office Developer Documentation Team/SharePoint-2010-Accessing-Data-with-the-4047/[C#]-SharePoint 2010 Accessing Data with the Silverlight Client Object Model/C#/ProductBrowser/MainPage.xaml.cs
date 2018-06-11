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
using System.Linq;

namespace MSDN.SharePoint.Samples.ProductBrowser
{
    public partial class MainPage : UserControl
    {
        SynchronizationContext _syncContext;
        ListItemCollection _products;

        public MainPage()
        {
            // Required to initialize variables
            InitializeComponent();

            // init the application
            StatusBar.BeginUpdateMessage("Initalizing application...");
            _syncContext = SynchronizationContext.Current;

            // establish link to the status panel with the details panel
            ProductDetails.StatusBarPanel = StatusBar;

            // load product categories
            StatusBar.BeginUpdateMessage("Loading product categories...");
            LoadProductCategories();
        }

        // get a list of all the categories
        private void LoadProductCategories()
        {
            List categoryList = Global.GetClientContext().Web.Lists.GetByTitle("Product Categories");

            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><OrderBy><FieldRef Name='Title' /></OrderBy></Query></View>";
            Global.ProductCategories = categoryList.GetItems(query);

            Global.GetClientContext().Load(Global.ProductCategories);
            Global.GetClientContext().ExecuteQueryAsync(OnSucceedListenerGetCategories, OnFailListener);
        }
        // methods used to update the UI when retrieving categories
        private void OnSucceedListenerGetCategories(object sender, ClientRequestEventArgs args)
        {
            _syncContext.Post(OnSucceedGetCategories, null);
        }
        private void OnSucceedGetCategories(object sender)
        {
            if (Global.ProductCategories != null)
                ProductCategoryListBox.ItemsSource = Global.ProductCategories; 

            StatusBar.EndUpdateMessage();
        }

        // get list of all products
        private void ProductCategoryListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            StatusBar.BeginUpdateMessage("Loading products for selected category...");

            ProductDetails.ResetProductDetails();

            // get the selected item
            if (ProductCategoryListBox.SelectedIndex > -1)
            {
                ListItem category = (ListItem)ProductCategoryListBox.SelectedItem;

                CamlQuery query = new CamlQuery();
                query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Category' /><Value Type='Lookup'>" + category["Title"] + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy></Query></View>";

                List productList = Global.GetClientContext().Web.Lists.GetByTitle("Products");
                _products = productList.GetItems(query);

                Global.GetClientContext().Load(_products);
                Global.GetClientContext().ExecuteQueryAsync(OnSucceededListenerGetProducts, OnFailListener);
            }
        }
        // methods used to update the UI when retrieving products
        private void OnSucceededListenerGetProducts(object sender, ClientRequestEventArgs args)
        {
            _syncContext.Post(OnSucceedGetProducts, null);
        }
        private void OnSucceedGetProducts(object sender)
        {
            if (_products != null)
                ProductListBox.ItemsSource = _products;

            StatusBar.EndUpdateMessage();
        }

        // get product details
        private void ProductListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductListBox.SelectedIndex == -1)
                return;

            StatusBar.BeginUpdateMessage("Loading selected product details...");

            // get the selected item
            ListItem selectedProduct = (ListItem)ProductListBox.SelectedItem;

            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + selectedProduct["Title"] + "</Value></Eq></Where></Query></View>";

            List productList = Global.GetClientContext().Web.Lists.GetByTitle("Products");
            _products = productList.GetItems(query);

            Global.GetClientContext().Load(_products);
            Global.GetClientContext().ExecuteQueryAsync(OnSucceededListenerGetProductDetails, OnFailListener);
        }

        // methods used to update the UI when retrieving product details
        private void OnSucceededListenerGetProductDetails(object sender, ClientRequestEventArgs args)
        {
            _syncContext.Post(OnSucceedGetProductDetails, null);
        }
        private void OnSucceedGetProductDetails(object sender)
        {
            if (_products != null && _products.Count == 1)
            {
                ProductDetails.LoadProductDetails(_products[0].Id,
                    _products[0]["Title"].ToString(),
                    _products[0]["Product_x0020_Number"].ToString(),
                    _products[0]["Price"].ToString(),
                    ((FieldLookupValue)_products[0]["Category"]).LookupValue);
            }

            StatusBar.EndUpdateMessage();
        }

        // handle when the product changed
        private void ProductDetails_ProductChanged(object sender, ProductChangedEventArgs e)
        {
            // reset the list of products
            ProductCategoryListBox_SelectionChanged(null, null);
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