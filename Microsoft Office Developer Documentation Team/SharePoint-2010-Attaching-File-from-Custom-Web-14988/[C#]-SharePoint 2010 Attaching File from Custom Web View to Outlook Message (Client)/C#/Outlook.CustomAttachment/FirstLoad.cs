// <copyright file="FirstLoad.cs" company="Microsoft">
// Copyright Microsoft 2010
// </copyright>
namespace Outlook.CustomAttachment
{
    using System;
    using System.Windows.Forms;
    using Microsoft.Win32;

    /// <summary>
    /// A form to display, capture and set the preferred SharePoint Server
    /// </summary>
    public partial class FirstLoad : Form
    {
        /// <summary>
        /// Initializes a new instance of the FirstLoad class.
        /// </summary>
        public FirstLoad()
        {
                this.InitializeComponent();
        }

        /// <summary>
        /// Saves the chosen SharePoint server into the registry
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\CustomAttachment", true);
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(@"Software\CustomAttachment");
                }

                key.SetValue("Root Server URL", this.comboBox1.Text.TrimEnd('/'), RegistryValueKind.String);
                Close();
        }
    }
}
