using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.Helper;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class ReseedDataForm : Form
    {
        public ReseedDataForm()
        {
            InitializeComponent();
        }

        private void linkLabelClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkBoxNoShow.Checked)
            {
                Config.UpdateKey(Config._keyShowReseedDataForm, "false");
            }

            Close();
        }

        private void linkLabelSeedDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Visible = false;

            Config.UpdateKey(Config._keyShowReseedDataForm, "false");

            PseudoProgressForm progress = new PseudoProgressForm(Config.SeedDataLimit / 2);
            progress.ProgressLabel = "Creating seed data...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork +=
                delegate(object obj, DoWorkEventArgs eventArgs)
                {
                    SeedDataHelper.CreateSeedData();
                };

            backgroundWorker.RunWorkerCompleted +=
                delegate(object obj, RunWorkerCompletedEventArgs eventArgs)
                {
                    progress.Close();
                    Close();
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog(this);

        }

        
    }
}
