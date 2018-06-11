using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using Office = Microsoft.Office.Core;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class Login : Form
    {
        public Login()        
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgBrush =
                new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(Width, 0),
                    Color.WhiteSmoke,
                    Color.FromArgb(30, 119, 211));

            e.Graphics.FillRectangle(
                lgBrush,
                new Rectangle(0, 0, Width, Height));

            base.OnPaint(e);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            textBoxUsername.Text = textBoxUsername.Text.Trim();
            textBoxPassword.Text = textBoxPassword.Text.Trim();
            if (textBoxUsername.Text.Length == 0)
            {
                errorProvider.SetError(textBoxUsername, "Username is required");
            }

            else if (textBoxPassword.Text.Length == 0)
            {
                errorProvider.SetError(textBoxPassword, "Password is required");
            }

            else /*if (textBoxUsername.Text.Length > 0 &&
               textBoxPassword.Text.Length > 0) */
            {
                //validate username/password here
                if (ValidateUser(textBoxUsername.Text, textBoxPassword.Text))
                {
                    Splash splash = new Splash();

                    Config.SAPPassword = textBoxPassword.Text;
                    Config.SAPUserName = textBoxUsername.Text;

                    DialogResult = DialogResult.OK;
                    Visible = false;
                    splash.ShowDialog(this);
                    Close();
                }

                else
                {
                    Message.SAPDisplayError(
                        this,
                        "Authentication failure. " +
                        Environment.NewLine +
                        "Please specify corrent username and password.");
                }
            }
        }

        //relocate this on a helper class or something better
        private bool ValidateUser(string username, string password)
        {
            return SAPCredential.ValidateUser(username, password);
        }
    }
}