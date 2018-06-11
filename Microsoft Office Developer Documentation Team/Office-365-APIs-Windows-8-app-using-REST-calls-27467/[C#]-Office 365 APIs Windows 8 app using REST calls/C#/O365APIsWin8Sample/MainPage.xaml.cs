using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace O365APIsWin8Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void buttonPeople_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActiveDirectorySamplePage));
        }

        private void buttonDocuments_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SharePointSamplePage));
        }

        private void buttonMail_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ExchangeSamplePage));
        }
    }
}
