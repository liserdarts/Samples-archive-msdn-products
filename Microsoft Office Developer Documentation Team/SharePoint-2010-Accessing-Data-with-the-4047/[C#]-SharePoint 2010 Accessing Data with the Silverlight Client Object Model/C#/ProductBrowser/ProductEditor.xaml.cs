using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.Threading;

namespace MSDN.SharePoint.Samples.ProductBrowser
{
    public partial class ProductEditor : ChildWindow
    {
        SynchronizationContext _syncContext;
        
        public ProductEditor()
        {
            InitializeComponent();

            _syncContext = SynchronizationContext.Current;

            ProductCategortyComboBox.ItemsSource = Global.ProductCategories;
        }

        public void InitFields(string productName, string productNumber, string price, string category)
        {
            ProductNumberTextBox.Text = productNumber;
            ProductNameTextBox.Text = productName;
            ProductPriceTextBox.Text = price;

            for (int i = 0; i <= ProductCategortyComboBox.Items.Count - 1; i++)
            {
                ListItem li = (ListItem)ProductCategortyComboBox.Items[i];
                if (li["Title"].ToString() == category)
                {
                    ProductCategortyComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        // methods for setting the label on the OK button
        public void SetForUpdate()
        {
            OKButton.Content = "Update";
        }
        public void SetForAdd()
        {
            OKButton.Content = "Add";
            ProductCategortyComboBox.SelectedIndex = 0;
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}