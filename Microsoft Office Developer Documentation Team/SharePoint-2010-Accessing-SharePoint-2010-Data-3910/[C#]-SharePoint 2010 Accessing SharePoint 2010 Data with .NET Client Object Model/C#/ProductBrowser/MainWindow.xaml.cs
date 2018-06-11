using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using SP = Microsoft.SharePoint.Client;

namespace ProductBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        SP.ListItemCollection _products;

        private bool _isConnected = false;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }
        public event EventHandler ConnectedStatusChanged;
        protected virtual void OnConnectedStatusChanged(EventArgs e)
        {
            if (ConnectedStatusChanged != null)
                ConnectedStatusChanged(this, e);
        }

        public MainWindow()
        {
            this.InitializeComponent();

            ProductDetails.StatusBarPanel = StatusBar;

            // wire up connected status changed
            this.ConnectedStatusChanged += new EventHandler(MainWindow_ConnectedStatusChanged);
        }

        // handles event when the connected status changes
        private void MainWindow_ConnectedStatusChanged(object sender, EventArgs e)
        {
            if (this.IsConnected)
            {
                // disable the url textbox
                WingtipSiteUrlTextBox.IsEnabled = false;

                StatusBar.BeginUpdateMessage("Loading product categories...");

                ThreadPool.QueueUserWorkItem(LoadProductCategories);
            }
            else
            {
                // disable the url textbox
                WingtipSiteUrlTextBox.IsEnabled = true;

                // reset & disable categoties & product list boxes
                ProductCategoriesListBox.DataContext = null;
                ProductsListBox.DataContext = null;
            }
        }

        // handles the click event when user connects to a site
        private void ConnectSiteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.IsConnected)
            {
                // disable UI and show it's working
                WingtipSiteUrlTextBox.IsEnabled = false;
                ConnectSiteButton.IsEnabled = false;
                Cursor = Cursors.Wait;


                StatusBar.BeginUpdateMessage("Connecting to remote SharePoint site...");
                ThreadPool.QueueUserWorkItem(OnConnectSiteButtonWorker, WingtipSiteUrlTextBox.Text);
            }
            else
            {
                Globals.ClientContext = null;
                this.IsConnected = false;
                ConnectSiteButton.Content = "Connect to SharePoint Site";
                StatusBar.EndUpdateMessage();
                // raise event that the connected status changed
                OnConnectedStatusChanged(new EventArgs());
            }
        }
        // establishes the connection
        private void OnConnectSiteButtonWorker(object state)
        {
            string siteUrl = (string)state;

            // setup client context & test connection
            Globals.ClientContext = new SP.ClientContext(siteUrl);

            SP.Web site = Globals.ClientContext.Web;
            Globals.ClientContext.Load(site, s => s.Title);
            Globals.ClientContext.ExecuteQuery();

            // app connected to site
            this.IsConnected = true;

            // fire the UI work on another thread
            this.Dispatcher.BeginInvoke(new Action(OnConnectSiteButtonUIUpdater), DispatcherPriority.Normal);
        }
        private void OnConnectSiteButtonUIUpdater()
        {
            Cursor = Cursors.Arrow;

            ConnectSiteButton.IsEnabled = true;
            ConnectSiteButton.Content = "Disconnect from SharePoint Site";

            StatusBar.EndUpdateMessage();

            // raise event that the connected status changed
            OnConnectedStatusChanged(new EventArgs());
        }

        // load the product categories
        private void LoadProductCategories(object state)
        {
            SP.List categoryList = Globals.ClientContext.Web.Lists.GetByTitle("Product Categories");

            SP.CamlQuery query = new SP.CamlQuery();
            query.ViewXml = "<View><Query><OrderBy><FieldRef Name='Title' /></OrderBy></Query></View>";
            Globals.ProductCategories = categoryList.GetItems(query);

            Globals.ClientContext.Load(Globals.ProductCategories);
            Globals.ClientContext.ExecuteQuery();

            // fire the UI work on another thread
            this.Dispatcher.BeginInvoke(new Action(LoadProductCategoriesUIUpdater), DispatcherPriority.Normal);
        }
        private void LoadProductCategoriesUIUpdater()
        {
            // bind the categories
            if (Globals.ProductCategories != null)
                ProductCategoriesListBox.ItemsSource = Globals.ProductCategories;

            StatusBar.EndUpdateMessage();
        }

        // handles when the user selects a category, all matching products are retrieved and displayed
        private void ProductCategoriesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductCategoriesListBox.SelectedIndex == -1)
                return;

            // no product selected
            ProductDetails.DataContext = null;

            // load all products for selected category
            StatusBar.BeginUpdateMessage("Loading products for selected category...");
            ThreadPool.QueueUserWorkItem(OnProductCategoryChangedWorker, ProductCategoriesListBox.SelectedItem);
        }
        private void OnProductCategoryChangedWorker(object selectedProductCategory)
        {
            SP.ListItem category = selectedProductCategory as SP.ListItem;

            // get all products matching this query
            SP.CamlQuery query = new SP.CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Category'/><Value Type='Lookup'>" + category["Title"] + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy></Query></View>";
            SP.List productList = Globals.ClientContext.Web.Lists.GetByTitle("Products");
            _products = productList.GetItems(query);

            Globals.ClientContext.Load(_products);
            Globals.ClientContext.ExecuteQuery();

            // fire the UI work on another thread
            this.Dispatcher.BeginInvoke(new Action(OnProductCategoryChangedUIUpdater), DispatcherPriority.Normal);
        }
        private void OnProductCategoryChangedUIUpdater()
        {
            if (_products != null)
                ProductsListBox.ItemsSource = _products;

            StatusBar.EndUpdateMessage();
        }

        // handles when the user selects a product
        private void ProductsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductsListBox.SelectedIndex == -1)
                return;

            StatusBar.BeginUpdateMessage("Loading selected product details...");
                ThreadPool.QueueUserWorkItem(OnProductChangedWorker, ProductsListBox.SelectedItem);
        }
        private void OnProductChangedWorker(object selectedProduct)
        {
            SP.ListItem product = (SP.ListItem)selectedProduct;

            // get product matching this query
            SP.CamlQuery query = new SP.CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Title'/><Value Type='Text'>" + product["Title"] + "</Value></Eq></Where></Query></View>";
            SP.List productList = Globals.ClientContext.Web.Lists.GetByTitle("Products");
            _products = productList.GetItems(query);

            Globals.ClientContext.Load(_products);
            Globals.ClientContext.ExecuteQuery();

            // fire the UI work on another thread
            this.Dispatcher.BeginInvoke(new Action(OnProductChangedUIUpdater), DispatcherPriority.Normal);
        }
        private void OnProductChangedUIUpdater()
        {
            if (_products != null && _products.Count == 1)
                ProductDetails.DataContext = _products[0];

            StatusBar.EndUpdateMessage();
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
        #endregion

        private void ProductDetails_ProductChanged(object sender, ProductChangedEventArgs e)
        {
            ProductCategoriesListBox_SelectionChanged(null, null);
        }
    }
}