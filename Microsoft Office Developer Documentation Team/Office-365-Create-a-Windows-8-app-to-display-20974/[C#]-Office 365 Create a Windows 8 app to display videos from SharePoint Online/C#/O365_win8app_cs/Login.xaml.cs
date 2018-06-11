// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using O365_Win8App.Data;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Security; 



namespace O365_Win8App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
            VideoDataSource.RefreshVideos();
        }

        /// <summary>
        /// Take the user's Office 365 credentials and attempt to sign into SharePoint Online
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await SharePointOnlineLoginHelper.InitAuthObj(new Uri(this.siteUrlInput.Text),
                    this.usernameInput.Text,
                    this.passwordInput.Password,
                    this.librayNameInput.Text);
            }
            catch
            {
                this.messageOutput.Visibility = Visibility.Visible;
                return;
            }

            if (SharePointOnlineLoginHelper.AuthObj!= null)
            {
                // If signin was successful store the user's O365 creds in the password vault and remember the site url in the apps roaming settings.
                this.Frame.Navigate(typeof(VideoListPage));
            }
        }
    }
}
