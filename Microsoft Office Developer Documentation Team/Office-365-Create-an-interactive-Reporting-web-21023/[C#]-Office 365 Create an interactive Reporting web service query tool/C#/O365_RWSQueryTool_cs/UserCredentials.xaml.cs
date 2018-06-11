// Copyright Microsoft Corporation
// Not intended for use in a production environment
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace O365_RWSQueryTool_cs
{
    /// <summary>
    /// Interaction logic for UserCredentials.xaml
    /// </summary>
    public partial class UserCredentials : Window
    {
        public UserCredentials()
        {
            InitializeComponent();
        }

        // when they click the OK button, close the dialog. this is only called
        // once the credentials are checked by the routine below. that's why this
        // has next to nothing in the way of verification
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // same for when they clicks the cancel button. this is only called once the 
        // credentials are checked by the routine below. That's why this has next
        // to nothing in the way of verification
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // this event is called right when the user clicks on the OK button, and before
        // that button has a chance to actually DO anything. if the verification is good
        // then the button press will continue. but if a messagebox is popped up, the 
        // OK click is effectively ignored.
        private void CredsOK_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // first check whether they actually entered any user name at all
            if (credsUserName.Text.Length > 0)
            {
                // this string pattern will match an email address in the form of user@FQDN.
                string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                bool isValidEmail = Regex.IsMatch(credsUserName.Text, pattern);
                // check whether the user name is a valid email address
                if (isValidEmail == true)
                {
                    // if it's a valid e-mail address, then check to see if the password has been entered.
                    // Do not inspect the password in any way other than for its length.
                    if (credsPassword.SecurePassword.Length == 0)
                    {
                        MessageBox.Show("Passwords cannot be empty. Please enter the correct password.");
                        return;
                    }
                }
                // arrive here when the format of the email address is not recognized
                else
                {
                    MessageBox.Show("The user name is not in recognized as a valid e-mail address. \nFor example, \"someone@example.com\". Please enter a valid user name.");
                    return;
                }
            }
            // and arrive here when the user name is blank.
            else
            {
                MessageBox.Show("The user name is blank. Please enter a valid user name.");
                return;
            }
        }
    }
}
