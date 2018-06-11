using System;
using System.Windows;

namespace MSDNArticleIM
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CredentialForm : Window, IDisposable
    {
        public string userSIP
        {
            get
            {
                return this.txt_UserUri.Text;
            }
        }
        public string userDomain
        {
            get
            {
                return this.txt_Domain.Text;
            }
        }
        public string userPassword
        {
            get
            {
                return this.txt_Password.Text;
            }
        }

        public CredentialForm()
        {
            InitializeComponent();
        }

        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            
        }

        #endregion
    }
}
