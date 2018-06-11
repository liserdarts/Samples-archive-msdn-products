using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

using Microsoft.Office.Word.Server.Conversions;

namespace ConvertWordToPDF.ConvertWordToPDFEventReceiver
{
  /// <summary>
  /// List Item Events
  /// </summary>
  public class ConvertWordToPDFEventReceiver : SPItemEventReceiver
  {
    /// <summary>
    /// An item was added.
    /// </summary>
    public override void ItemAdded(SPItemEventProperties properties)
    {
      base.ItemAdded(properties);

      //Verify the document added is a Word Document
      //  before starting the conversion.
      if (properties.ListItem.Name.Contains(".docx")
        || properties.ListItem.Name.Contains(".doc"))
      {
        //Variables used by the sample code.
        ConversionJobSettings jobSettings;
        ConversionJob pdfConversion;
        string wordFile;
        string pdfFile;

        //Initialize the conversion settings.
        jobSettings = new ConversionJobSettings();
        jobSettings.OutputFormat = SaveFormat.PDF;

        //Create the conversion job using the settings.
        pdfConversion =
          new ConversionJob("Word Automation Services", jobSettings);

        //Set the credentials to use when running the conversion job.
        pdfConversion.UserToken = properties.Web.CurrentUser.UserToken;

        //Set the file names to use for the source Word document
        //  and the destination PDF document.
        wordFile = properties.WebUrl + "/" + properties.ListItem.Url;
        if (properties.ListItem.Name.Contains(".docx"))
        {
          pdfFile = wordFile.Replace(".docx", ".pdf");
        }
        else
        {
          pdfFile = wordFile.Replace(".doc", ".pdf");
        }

        //Add the file conversion to the Conversion Job.
        pdfConversion.AddFile(wordFile, pdfFile);

        //Add the Conversion Job to the Word Automation Services 
        //  conversion job queue.
        //The conversion will not take place immeditately but
        //  will be processed during the next run of the 
        //  Document Conversion job.
        pdfConversion.Start();

      }
    }
  }
}
