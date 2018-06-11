using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace RetrieveComments
{
  class Program
  {
    // Alter this path as necessary for your own demonstration:
    private const string DEMOPATH = @"I:\Samples\Comments.docx";

    static void Main(string[] args)
    {
      // Retrieve an XDocument containing comments:
      var comments = WDRetrieveComments(DEMOPATH);
      if (comments != null)
        Console.WriteLine(comments.ToString());
    }

    // Retrieve the comments content from the document, as an XDocument.
    public static XDocument WDRetrieveComments(string fileName)
    {
      XDocument comments = null;

      using (var document = WordprocessingDocument.Open(fileName, false))
      {
        // Retrieve the document part:
        var docPart = document.MainDocumentPart;
        if (docPart != null)
        {
          // Retrieve the comments part:
          var commentsPart = docPart.WordprocessingCommentsPart;
          if (commentsPart != null)
          {
            // Load an XDocument with the comments content:
            using (Stream stm = commentsPart.GetStream(FileMode.Open, FileAccess.Read))
            {
              comments = XDocument.Load(XmlReader.Create(stm));
            }
          }
        }
      }
      return comments;
    }
  }
}
