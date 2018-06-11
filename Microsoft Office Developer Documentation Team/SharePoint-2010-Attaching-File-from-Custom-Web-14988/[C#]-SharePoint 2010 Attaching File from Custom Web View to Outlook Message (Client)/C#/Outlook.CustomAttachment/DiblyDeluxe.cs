// <copyright file="DiblyDeluxe.cs" company="Microsoft">
// Copyright Microsoft 2010
// </copyright>
namespace Outlook.CustomAttachment
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Permissions;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a UserControl to select multiple files from Recent Docs and document libraries in SharePoint 
    /// </summary>
    public partial class DiblyDeluxe : UserControl
    {
        /// <summary>
        /// List of selected files and folders
        /// </summary>
        private List<Utilities.PreferredFileInformation> selectedFilesAndFolders;

        /// <summary>
        /// The URL of the current library loaded into the web browser
        /// </summary>
        private string currentLibraryUrl = string.Empty;

        /// <summary>
        /// Determines whether to allow multiselect of documents.
        /// </summary>
        private bool multiselectEnabled = true;

        /// <summary>
        /// Determines whether to allow versions to be selected.
        /// </summary>
        private bool versionsEnabled = true;

        /// <summary>
        /// Determines whether to allow attachments to be selected.
        /// </summary>
        private bool attachmentsEnabled = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiblyDeluxe"/> class.
        /// </summary>
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public DiblyDeluxe()
        {
            this.InitializeComponent();
            this.selectedFilesAndFolders = new List<Utilities.PreferredFileInformation>();
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser1_DocumentCompleted);
        }

        /// <summary>
        /// Occurs when a file is double clicked in the picker.  Check the SelectedFiles property to see which file was selected.
        /// </summary>
        public event EventHandler FileDoubleClicked;

        /// <summary>
        /// Occurs when the SelectedFiles collection is changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Gets or sets a value indicating whether [multiselect enabled].
        /// </summary>
        /// <value><c>true</c> if [multiselect enabled]; otherwise, <c>false</c>.</value>
        public bool MultiselectEnabled
        {
            get { return this.multiselectEnabled; }
            set { this.multiselectEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [versions enabled].
        /// </summary>
        /// <value><c>true</c> if [versions enabled]; otherwise, <c>false</c>.</value>
        public bool VersionsEnabled
        {
            get { return this.versionsEnabled; }
            set { this.versionsEnabled = value; }
        }


        /// <summary>
        /// Gets a List of PreferredFileInformation for each currently selected file
        /// </summary>
        public List<Utilities.PreferredFileInformation> SelectedFiles
        {
            get
            {
                // TODO: Would like to use this Linq but it just returns null :(
                // List<Utilities.PreferredFileInformation> files = selectedFiles.Where(p => !p.IsFolder) as List<Utilities.PreferredFileInformation>;
                List<Utilities.PreferredFileInformation> filesToReturn = new List<Utilities.PreferredFileInformation>();
                foreach (Utilities.PreferredFileInformation file in this.selectedFilesAndFolders)
                {
                    if (!file.IsFolder)
                    {
                        filesToReturn.Add(file);
                    }
                }

                return filesToReturn;
            }
        }

        /// <summary>
        /// Gets the extensions property to be used when calling the dialog.
        /// </summary>
        /// <value>The extensions.</value>
        private string Extensions
        {
            get
            {
                string multiselect = this.MultiselectEnabled ? "multiselect;" : String.Empty;
                string versions = this.VersionsEnabled ? "versions;" : String.Empty;

                string extensions = multiselect + versions;
                if (extensions.LastIndexOf(';') == extensions.Length - 1)
                {
                    extensions = extensions.Remove(extensions.LastIndexOf(';'));
                }

                return extensions;
            }
        }

        /// <summary>
        /// Navigates to the library or folder
        /// </summary>
        /// <param name="libraryOrFolderUrl">The library or folder URL.</param>
        public void NavigateTo(string libraryOrFolderUrl)
        {
            Debug.WriteLine("NavigateTo(string libraryOrFolderUrl) called with " + libraryOrFolderUrl);
            this.NavigateTo(libraryOrFolderUrl, string.Empty);
        }

        /// <summary>
        /// Navigates to the library or folder with sorting information
        /// </summary>
        /// <param name="libraryURL">The library URL.</param>
        /// <param name="sortingFields">The sorting fields querystring.</param>
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public void NavigateTo(string libraryURL, string sortingFields)
        {
            Debug.WriteLine("NavigateTo(string libraryURL, string sortingFields) called with " + libraryURL + "; " + sortingFields);

            if (webBrowser1.IsBusy)
            {
                Debug.WriteLine("Cancelling NavigateTo() as the browser is busy.  This means other events have yet to fire (which will themselves likely cause a navigation)");
                return;
            }

            this.selectedFilesAndFolders.Clear();
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(null, null);
            }

            this.progressBar1.Visible = true;
            this.currentLibraryUrl = libraryURL;

            string webURL = Utilities.WebURLFromAnyURL(libraryURL);
            Debug.WriteLine("webURL is " + webURL);

            string location = string.Empty;
            if (libraryURL.TrimEnd('/').ToUpperInvariant() == webURL.TrimEnd('/').ToUpperInvariant())
            {
                // this address is a site
                // If a site URL was passed in, libraryName will be empty, but that's fine
            }
            else
            {
                location = libraryURL.Substring(webURL.Length);
            }

            Debug.WriteLine("location is " + location);

            string fullPath = string.Format("{0}_vti_bin/owssvr.dll?dialogview=FileOpen&FileDialogFilterValue=*.*&Extensions={1}&location={2}&{3}", webURL, this.Extensions, location, sortingFields);
            Debug.WriteLine("fullPath is " + fullPath);

            this.webBrowser1.Navigate(fullPath);
            Debug.WriteLine("called webBrowser1.Navigate(fullPath)");
        }

        /// <summary>
        /// Raised when a new web page is loaded so events can be attached
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

                // Bind to each content item TR
                HtmlElement fileDialogViewTable = this.webBrowser1.Document.GetElementById("FileDialogViewTable");
                if (fileDialogViewTable != null)
                {
                    foreach (HtmlElement tr in fileDialogViewTable.GetElementsByTagName("tr"))
                    {
                        if (tr.Id != "headerTr")
                        {
                            tr.Click += new HtmlElementEventHandler(this.ContentItem_Click);
                            tr.DoubleClick += new HtmlElementEventHandler(this.ContentItem_DoubleClick);
                        }
                    }
                }

                // Bind to each of the column headers so we can postback a sort
                HtmlElement headerTh = this.webBrowser1.Document.GetElementById("headerTr");
                if (headerTh != null)
                {
                    foreach (HtmlElement a in headerTh.GetElementsByTagName("a"))
                    {
                        a.Click += new HtmlElementEventHandler(this.SortColumn_Click);
                    }
                }

                // Bind to each of the tabs so we can postback a sort
                HtmlElement tabContainer = this.webBrowser1.Document.GetElementById("tab-container");
                if (tabContainer != null)
                {
                    foreach (HtmlElement a in tabContainer.GetElementsByTagName("a"))
                    {
                        a.Click += new HtmlElementEventHandler(this.SortColumn_Click);
                    }
                }

                // Ensure if the 'select all' checkbox is ticked we update our selection
                HtmlElement selectAll = this.webBrowser1.Document.GetElementById("selectAll");
                if (selectAll != null)
                {
                    selectAll.Click += new HtmlElementEventHandler(this.ContentItem_Click);
                }

                this.progressBar1.Visible = false;

                this.webBrowser1.Focus();
                this.webBrowser1.Navigate("javascript:document.getElementById('Browse').focus();");
        }

        /// <summary>
        /// Called when a TR in the file dialog table in the webpage is clicked
        /// </summary>
        /// <param name="sender">The TR which has raised the event</param>
        /// <param name="e">The parameter is not used.</param>
        private void ContentItem_Click(object sender, HtmlElementEventArgs e)
        {
                // We may need to respond to this event in one of two ways.  In OutlookLibrary view, the page contains a DiblyCue
                // element containing all of the selected item data.  In standard CFD view, the item information is on the TR itsel

            HtmlElement tr = (HtmlElement)sender;

            //if the user clicked on a folder get that information
            if (tr.TagName.ToUpper() == "TR" && tr.GetAttribute("fileattribute").ToUpper() == "FOLDER")
            {
                this.selectedFilesAndFolders = Utilities.PreferredFileInformationListFromRawMultiselectUrls(tr.GetAttribute("PreferredFileInformation"));
            }
            else //otherwise get the list of selected folders
            {
                HtmlElement diblyCue = ((HtmlElement)sender).Document.GetElementById("DiblyCue");
                if (!string.IsNullOrEmpty(diblyCue.InnerText))
                {
                    string rawUrls = diblyCue.InnerText;
                    this.selectedFilesAndFolders = Utilities.PreferredFileInformationListFromRawMultiselectUrls(rawUrls);
                }
            }

            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Called when a TR in the file dialog table in the webpage is double clicked
        /// </summary>
        /// <param name="sender">The TR which has raised the event</param>
        /// <param name="e">This parameter is not used.</param>
        private void ContentItem_DoubleClick(object sender, HtmlElementEventArgs e)
        {

                this.ContentItem_Click(sender, e);

                if (this.selectedFilesAndFolders.Count == 1 && this.selectedFilesAndFolders[0].IsFolder)
                {
                    // A list or folder is selected - reload the new location
                    this.NavigateTo(this.selectedFilesAndFolders[0].Url, string.Empty);
                }
                else if (this.selectedFilesAndFolders.Count == 1 && !this.selectedFilesAndFolders[0].IsFolder)
                {
                    // A single file is selected - raise the doubleclick to commit the selection
                    if (this.FileDoubleClicked != null)
                    {
                        this.FileDoubleClicked(null, null);
                    }
                }
        }

        /// <summary>
        /// Called when an A tag in the file dialog table header in the webpage is clicked
        /// </summary>
        /// <param name="sender">The A tag which has raised the event</param>
        /// <param name="e">This parameter is not used.</param>
        private void SortColumn_Click(object sender, HtmlElementEventArgs e)
        {
                HtmlElement el = (HtmlElement)sender;
                string sortingFields = el.GetAttribute("SortingFields");
                this.NavigateTo(this.currentLibraryUrl, sortingFields);
        }
    }
}
