using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using OfficeOutlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
namespace Outlook.CustomAttachment
{
    public partial class ThisAddIn
    {
        /// <summary>
        /// The Ribbon UI item.
        /// </summary>
        private AttachRibbonItem attachRibbonItem;

        /// <summary>
        /// A boolean indicating whether to insert the attachment as a copy.
        /// </summary>
        private bool insertAsCopy;

        /// <summary>
        /// A boolean indicating whether to insert the attachment as a link.
        /// </summary>
        private bool insertAsLink;

        /// <summary>
        /// The control that downloads documents to the TEMP folder before attaching.
        /// </summary>
        private Downloading downloadDialog;

        /// <summary>
        /// Returns an object that implements the Microsoft.Office.Core.IRibbonExtensibility interface.
        /// </summary>
        /// <returns>
        /// An object that implements the Microsoft.Office.Core.IRibbonExtensibility interface.
        /// </returns>
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            this.attachRibbonItem = new AttachRibbonItem();
            this.attachRibbonItem.Attach_Clicked += new EventHandler(this.AttachRibbonItem_Attach_Clicked);
            return this.attachRibbonItem;
        }
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Inserts the document attachments.
        /// </summary>
        /// <param name="documents">The documents.</param>
        private void InsertDocumentAttachments(IEnumerable<Utilities.PreferredFileInformation> documents)
        {
            this.downloadDialog = new Downloading();
            this.downloadDialog.Show();
            this.downloadDialog.DocumentsDownloaded += new Downloading.AttachmentInformation(this.DownloadDialog_DocumentsDownloaded);
            this.downloadDialog.DownloadDocumentsAsync(documents);
        }

               /// <summary>
        /// Occurs when the documents have been downloaded.
        /// </summary>
        /// <param name="sender">The documents.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DownloadDialog_DocumentsDownloaded(List<Utilities.PreferredFileInformation> sender, EventArgs e)
        {
            try
            {
                int numberFailedAttachments = this.PerformAttach(sender);

                if (numberFailedAttachments > 0)
                {
                    MessageBox.Show("One or more of the attachments exceed the allowable limit.  Some attachments might not have been added successfully.  Please check your message carefully.", "Attachments are too big", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                this.downloadDialog.Close();
                this.downloadDialog.Dispose();
            }
        }

        private void AttachRibbonItem_Attach_Clicked(object sender, EventArgs e)
        {
            using (NewAttachDialog dialog = new NewAttachDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    List<Utilities.PreferredFileInformation> selectedDocuments = dialog.SelectedFiles;
                    if (selectedDocuments != null && selectedDocuments.Count > 0)
                    {
                        this.insertAsCopy = dialog.InsertAsCopy;
                        this.insertAsLink = dialog.InsertAsLink;
                        this.InsertDocumentAttachments(selectedDocuments);
                    }
                }
            }
        }

        private const string attachmentWarning = "The attachment size exceeds the allowable limit.";

        /// <summary>
        /// Attaches the files to the email, changes behaviour depending on the email format
        /// </summary>
        /// <param name="attachments">The attachments.</param>
        /// <returns>The number of failed attachments as a result of mail size issues.  This will be 0 if all attachments were successful.</returns>
        private int PerformAttach(List<Utilities.PreferredFileInformation> attachments)
        {
            int numberFailedAttachments = 0;
            OfficeOutlook.Inspector inspector = this.Application.ActiveInspector();
            if (inspector.CurrentItem == null)
            {
                return 0;
            }

            OfficeOutlook.MailItem mailItem = inspector.CurrentItem as OfficeOutlook.MailItem;
            if ((mailItem == null) || (mailItem.MessageClass != "IPM.Note"))
            {
                return 0;
            }

            Word.Document document = (Word.Document)inspector.WordEditor;
            Word.Application app = document.Parent as Word.Application;
            Word.Range range = app.Selection.Range;
            int positionBeforeAdd = range.End;
            int positionAfterAdd = positionBeforeAdd;

            switch (mailItem.BodyFormat)
            {
                case OfficeOutlook.OlBodyFormat.olFormatRichText:
                    {
                        positionAfterAdd = InsertDetailsAsRichText(attachments, range, mailItem, out numberFailedAttachments);
                        break;
                    }

                case OfficeOutlook.OlBodyFormat.olFormatHTML:
                    {
                        positionAfterAdd = InsertDetailsAsHtml(attachments, range, positionBeforeAdd, mailItem, out numberFailedAttachments);
                        break;
                    }

                default:
                    {
                        InsertDetailsAsDefault(attachments, range, mailItem, out numberFailedAttachments);
                        break;
                    }
            }

            // Update the document range so the caret is at the end of the selection 
            app.Selection.SetRange(positionAfterAdd, positionAfterAdd);
            app.Selection.Range.Select();

            return numberFailedAttachments;
        }

        private void InsertDetailsAsDefault(List<Utilities.PreferredFileInformation> attachments, Word.Range range, OfficeOutlook.MailItem mailItem, out int numberFailedAttachments)
        {
            numberFailedAttachments = 0;
            for (int n = 0; n < attachments.Count; n++)
            {
                Utilities.PreferredFileInformation fileInfo = attachments[n];

                int endIndex = range.End;

                if (this.insertAsCopy)
                {
                    try
                    {
                        mailItem.Attachments.Add(fileInfo.LocalPath, OfficeOutlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        if (ex.Message.ToUpperInvariant() == attachmentWarning.ToUpperInvariant())
                        {
                            numberFailedAttachments++;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                if (this.insertAsLink)
                {
                    range.SetRange(endIndex, endIndex);
                    range.InsertAfter(fileInfo.Url);
                    range.InsertAfter(Environment.NewLine);
                }
            }
        }


        /// <summary>
        /// Inserts the details as HTML.
        /// </summary>
        /// <param name="attachments">The attachments.</param>
        /// <param name="range">The range.</param>
        /// <param name="positionBeforeAdd">The position before add.</param>
        /// <param name="mailItem">The mail item.</param>
        /// <param name="numberFailedAttachments">The number failed attachments.</param>
        /// <returns></returns>
        private int InsertDetailsAsHtml(List<Utilities.PreferredFileInformation> attachments, Word.Range range, int positionBeforeAdd, OfficeOutlook.MailItem mailItem, out int numberFailedAttachments)
        {
            numberFailedAttachments = 0;
            if (this.insertAsCopy)
            {
                for (int n = 0; n < attachments.Count; n++)
                {
                    Utilities.PreferredFileInformation fileInfo = attachments[n];
                    try
                    {
                        mailItem.Attachments.Add(fileInfo.LocalPath, OfficeOutlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        if (ex.Message.ToUpperInvariant() == attachmentWarning.ToUpperInvariant())
                        {
                            numberFailedAttachments++;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            if (this.insertAsLink)
            {
                // Adding the attachments resets the Range to the top of the document, so we need to move the range
                // to were the user's cursor was when they started the process
                range.SetRange(positionBeforeAdd, positionBeforeAdd);

                for (int n = 0; n < attachments.Count; n++)
                {
                    Utilities.PreferredFileInformation fileInfo = attachments[n];
                    Word.Hyperlink hyperlink = range.Hyperlinks.Add(range, fileInfo.Url, Type.Missing, Type.Missing, fileInfo.ToString());
                    range.SetRange(hyperlink.Range.End, hyperlink.Range.End);
                    range.InsertAfter(Environment.NewLine);
                    range.SetRange(range.End, range.End);
                }

                return range.End;
            }

            return 0;
        }

        /// <summary>
        /// Inserts the details as rich text.
        /// </summary>
        /// <param name="attachments">The attachments.</param>
        /// <param name="range">The range.</param>
        /// <param name="mailItem">The mail item.</param>
        /// <param name="numberFailedAttachments">The number failed attachments.</param>
        /// <returns></returns>
        private int InsertDetailsAsRichText(List<Utilities.PreferredFileInformation> attachments, Word.Range range, OfficeOutlook.MailItem mailItem, out int numberFailedAttachments)
        {
            List<KeyValuePair<int, int>> ranges = new List<KeyValuePair<int, int>>();
            int startIndex = range.Start;
            int endIndex = range.End;

            range.SetRange(endIndex, endIndex);
            int itemCounter = 0;
            numberFailedAttachments = 0;
            range.InsertAfter("\n");
            range.InsertAfter("\n");

            foreach (Utilities.PreferredFileInformation fileInfo in attachments)
            {
                endIndex = range.End;

                if (this.insertAsCopy)
                {
                    range.InsertAfter("\n");
                    endIndex = range.End;

                    try
                    {
                        OfficeOutlook.Attachment attachment = mailItem.Attachments.Add(
                            fileInfo.LocalPath,
                            OfficeOutlook.OlAttachmentType.olByValue,
                            endIndex,
                            Type.Missing);

                        endIndex = attachment.Position + 30;
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        if (ex.Message.ToUpperInvariant() == attachmentWarning.ToUpperInvariant())
                        {
                            numberFailedAttachments++;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                range.SetRange(endIndex, endIndex);
                endIndex = range.End;

                if (this.insertAsLink)
                {
                    range.InsertAfter("\n");

                    int tempIndexStart = range.End;
                    range.InsertAfter(fileInfo.NameNoExtension);

                    int tempIndexEnd = range.End;
                    endIndex = tempIndexEnd;

                    range.SetRange(tempIndexStart, tempIndexEnd);
                    ranges.Add(new KeyValuePair<int, int>(range.Start, range.End));
                }

                itemCounter++;

                if (itemCounter < attachments.Count)
                {
                    range.InsertAfter("\n");
                    range.InsertAfter("\n");
                    range.InsertAfter("\n");
                    endIndex = range.End;
                }
            }

            if (this.insertAsLink)
            {
                int counter = 0;

                List<Utilities.PreferredFileInformation> pairs = new List<Utilities.PreferredFileInformation>();

                foreach (Utilities.PreferredFileInformation value in attachments)
                {
                    pairs.Insert(pairs.Count, value);
                }

                pairs.Reverse();
                ranges.Reverse();

                foreach (Utilities.PreferredFileInformation value in pairs)
                {
                    range.SetRange(ranges[counter].Key, ranges[counter].Value);
                    string s = range.Text;
                    Word.Hyperlink hyperlink = range.Hyperlinks.Add(range, value.Url, Type.Missing, Type.Missing, value.ToString());
                    endIndex = hyperlink.Range.End;
                    counter++;
                }
            }

            return endIndex;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
