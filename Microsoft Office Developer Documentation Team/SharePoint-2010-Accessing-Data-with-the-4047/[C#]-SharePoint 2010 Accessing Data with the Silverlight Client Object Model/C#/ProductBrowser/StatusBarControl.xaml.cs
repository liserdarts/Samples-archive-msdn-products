using System;
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
	public partial class StatusBarControl : UserControl
	{
		public StatusBarControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        public void BeginUpdateMessage(string message)
        {
            this.StatusTextBlock.Text = message;
        }
        public void EndUpdateMessage()
        {
            this.StatusTextBlock.Text = "Ready.";
        }
    }
}