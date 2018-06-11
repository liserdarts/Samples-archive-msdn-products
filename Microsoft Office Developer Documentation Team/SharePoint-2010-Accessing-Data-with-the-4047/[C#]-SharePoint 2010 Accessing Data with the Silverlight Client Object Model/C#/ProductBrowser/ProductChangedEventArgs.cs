using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MSDN.SharePoint.Samples.ProductBrowser
{
    public delegate void ProductChangedEventHandler(object sender, ProductChangedEventArgs e);

    public class ProductChangedEventArgs : EventArgs
    {
        private string _beforeTitle;
        private string _afterTitle;

        private string _beforeCategory;
        private string _afterCategory;

        public ProductChangedEventArgs(string beforeTitle, string afterTitle, 
            string beforeCategory, string afterCategory)
        {
            _beforeTitle = beforeTitle;
            _afterTitle = afterTitle;

            _beforeCategory = beforeCategory;
            _afterCategory = afterCategory;
        }

        public string BeforeTitle
        {
            get { return _beforeTitle; }
        }
        public string AfterTitle
        {
            get { return _afterTitle; }
        }
        public string BeforeCategory
        {
            get { return _beforeCategory; }
        }
        public string AfterCategory
        {
            get { return _afterCategory; }
        }
    }
}
