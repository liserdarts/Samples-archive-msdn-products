// <copyright file="Downloading.cs" company="Microsoft">
// Copyright Microsoft 2010
// </copyright>
namespace Outlook.CustomAttachment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

    /// <summary>
    /// Used to download documents.
    /// </summary>
    public partial class Downloading : Form
    {
        /// <summary>
        /// Used to store the list of documents to download.
        /// </summary>
        private List<Utilities.PreferredFileInformation> documents = new List<Utilities.PreferredFileInformation>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Downloading"/> class.
        /// </summary>
        public Downloading()
        {
                this.InitializeComponent();
        }

        /// <summary>
        /// Used in the downloading event.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public delegate void AttachmentInformation(List<Utilities.PreferredFileInformation> sender, EventArgs e);

        /// <summary>
        /// Occurs when [documents downloaded].
        /// </summary>
        public event AttachmentInformation DocumentsDownloaded;

        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        /// <value>The documents.</value>
        public List<Utilities.PreferredFileInformation> Documents
        {
            get { return this.documents; }
            set { this.documents = value; }
        }

        /// <summary>
        /// Runs the asynchronous process to download the documents.
        /// </summary>
        /// <param name="documents">The documents.</param>
        public void DownloadDocumentsAsync(IEnumerable<Utilities.PreferredFileInformation> documents)
        {
            this.downloadBackgroundWorker.RunWorkerAsync(documents);
        }

        /// <summary>
        /// Gets the file version to attach.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <param name="newFileName">New name of the file.</param>
        /// <returns>The local path of the downloaded file.</returns>
        private static string GetFileVersionAttach(string fileUrl, string newFileName)
        {
            string newLocation = Path.Combine(Path.GetTempPath(), newFileName);
            
            using (WebClient webClient = new WebClient())
            {
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.DownloadFile(fileUrl, newLocation);
            }

            return newLocation;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the DownloadBackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void DownloadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                this.DocumentsDownloaded(this.Documents, null);
        }

        /// <summary>
        /// Handles the DoWork event of the DownloadBackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void DownloadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
                IEnumerable<Utilities.PreferredFileInformation> docs = (IEnumerable<Utilities.PreferredFileInformation>)e.Argument;

                foreach (Utilities.PreferredFileInformation document in docs)
                {
                    string localFileName = document.ToString();
                    string attachmentLocalPath = GetFileVersionAttach(document.Url, localFileName);

                    if (this.Documents.Exists(p => p.LocalPath == attachmentLocalPath) == false)
                    {
                        document.LocalPath = attachmentLocalPath;
                        this.Documents.Add(document);
                    }
                }
        }
    }
}
