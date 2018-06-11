// <copyright file="NewAttachDialog.cs" company="Microsoft">
// Copyright Microsoft 2010
// </copyright>
namespace Outlook.CustomAttachment
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <summary>
    /// The form used to add attachments.
    /// </summary>
    public partial class NewAttachDialog : Form
    {
        /// <summary>
        /// A collection of selected files for attaching.
        /// </summary>
        private List<Utilities.PreferredFileInformation> selectedFiles = new List<Utilities.PreferredFileInformation>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NewAttachDialog"/> class.
        /// </summary>
        public NewAttachDialog()
        {
                this.InitializeComponent();
        }

        /// <summary>
        /// Gets a value indicating whether [insert as link].
        /// </summary>
        /// <value><c>true</c> if [insert as link]; otherwise, <c>false</c>.</value>
        public bool InsertAsLink
        {
            get { return this.insertAsLink.Checked; }
        }

        /// <summary>
        /// Gets a value indicating whether [insert as copy].
        /// </summary>
        /// <value><c>true</c> if [insert as copy]; otherwise, <c>false</c>.</value>
        public bool InsertAsCopy
        {
            get { return this.insertAsCopy.Checked; }
        }

        /// <summary>
        /// Gets or sets the selected files.
        /// </summary>
        /// <value>The selected files.</value>
        public List<Utilities.PreferredFileInformation> SelectedFiles
        {
            get
            { 
                return this.selectedFiles; 
            }

            set
            { 
                this.selectedFiles.Clear(); 
                this.selectedFiles = value; 
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
                Close();
        }

        /// <summary>
        /// Handles the FileDoubleClicked event of the DiblyDeluxe control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DiblyDeluxe_FileDoubleClicked(object sender, EventArgs e)
        {
                this.GetFiles();
                this.BtnOK_Click(sender, e);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the DiblyDeluxe control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DiblyDeluxe_SelectionChanged(object sender, EventArgs e)
        {
                this.GetFiles();
        }

        /// <summary>
        /// Handles the Load event of the NewAttachDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewAttachDialog_Load(object sender, EventArgs e)
        {
                this.diblyDeluxe.NavigateTo(Utilities.RootSharePointSite());
        }

        /// <summary>
        /// Gets the selected files from the Dibly.
        /// </summary>
        private void GetFiles()
        {
            this.SelectedFiles = this.diblyDeluxe.SelectedFiles;
        }
    }
}
