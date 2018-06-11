using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductBrowser
{
    public delegate void ProductChangedEventHandler(object sender, ProductChangedEventArgs e);

    public class ProductChangedEventArgs:EventArgs
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
            get { return AfterCategory; }
        }
    }
}
