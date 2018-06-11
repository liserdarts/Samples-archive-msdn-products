using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.Helper;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class SeedDataForm : Form
    {
        public SeedDataForm()
        {
            InitializeComponent();
        }

        private void linkLabelClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(checkBoxNoShow.Checked)
            {
                Config.UpdateKey(Config._keyShowSeedDataForm, "false");
            }

            Close();
        }

        private void linkLabelSeed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Visible = false;

            Config.UpdateKey(Config._keyShowSeedDataForm, "false");
            Config.UpdateKey(Config._keySeedData, "true");
            Config.UpdateKey(Config._keyDateLastSeed, DateTime.Now.ToString());

            PseudoProgressForm progress = new PseudoProgressForm(Config.SeedDataLimit/2);
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