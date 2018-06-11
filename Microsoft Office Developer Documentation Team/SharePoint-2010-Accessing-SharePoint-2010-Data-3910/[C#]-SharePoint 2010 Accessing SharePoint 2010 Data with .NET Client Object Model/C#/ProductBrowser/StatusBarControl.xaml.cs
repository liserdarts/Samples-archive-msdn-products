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

namespace ProductBrowser
{
	/// <summary>
	/// Interaction logic for StatusBarControl.xaml
	/// </summary>
	public partial class StatusBarControl : UserControl
	{
		public StatusBarControl()
		{
			this.InitializeComponent();
		}

        public void BeginUpdateMessage(string message)
        {
            this.StatusTextBlock.Text = message;
        }

        public void EndUpdateMessage()
        {
            this.StatusTextBlock.Text = "Ready...";
        }
	}
}