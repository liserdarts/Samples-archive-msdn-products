using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SignIn signIn;
        ShutDown shutDown;
        SendMessage sendMessage;
        bool SigningOut = false;    //flag to identify whether the user is signing out

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSendIM_Click(object sender, EventArgs e)
        {
            //sendMessage = new SendMessage();
            signIn._SendMessage.StartIMConversation();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text.Length > 0)
            {
                signIn = new SignIn(txtPwd.Text, SigningOut);
            }
            else
                MessageBox.Show("enter a password and try again");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            shutDown = new ShutDown(signIn);
        }
    }
}
