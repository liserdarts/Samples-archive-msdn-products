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
using System.Windows.Shapes;
using SP = Microsoft.SharePoint.Client;

namespace ProductBrowser
{
    /// <summary>
    /// Interaction logic for ProductEditor.xaml
    /// </summary>
    public partial class ProductEditor : Window
    {
        public ProductEditor()
        {
            this.InitializeComponent();

            ProductCategoryComboBox.ItemsSource = Globals.ProductCategories;
        }

        public void SetForUpdate()
        {
            OKButton.Content = "Update";
        }

        public void SetForAdd()
        {
            OKButton.Content = "Add";
            ProductCategoryComboBox.SelectedIndex = 0;
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SP.ListItem product = DataContext as SP.ListItem;

            for (int i = 0; i <= ProductCategoryComboBox.Items.Count - 1; i++)
            {
                SP.ListItem li = ProductCategoryComboBox.Items[i] as SP.ListItem;
                if (li["Title"].ToString() == ((SP.FieldLookupValue)product["Category"]).LookupValue)
                {
                    ProductCategoryComboBox.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}