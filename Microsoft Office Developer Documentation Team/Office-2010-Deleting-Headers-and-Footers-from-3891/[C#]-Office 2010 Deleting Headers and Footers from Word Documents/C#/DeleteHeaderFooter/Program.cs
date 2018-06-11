using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DeleteHeaderFooter
{
	class Program
	{
		static void Main(string[] args)
		{
      WDRemoveHeadersFooters(@"C:\Temp\Headers.docx");
		}

    // Delete headers and footers from a document.
    public static void WDRemoveHeadersFooters(string docName)
    {
      // Given a document name, remove all headers and footers.
      using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(docName, true))
      {
        var docPart = wdDoc.MainDocumentPart;
        if (docPart.HeaderParts.Count() > 0 || docPart.FooterParts.Count() > 0)
        {
          // Remove header and footer parts.
          docPart.DeleteParts(docPart.HeaderParts);
          docPart.DeleteParts(docPart.FooterParts);

          Document doc = docPart.Document;

          // Remove references to the headers and footers.
          // This requires digging into the XML content
          // of the document:
          var headers =
            doc.Descendants<HeaderReference>().ToList();
          foreach (var header in headers)
          {
            header.Remove();
          }

          var footers =
            doc.Descendants<FooterReference>().ToList();
          foreach (var footer in footers)
          {
            footer.Remove();
          }
          doc.Save();
        }
      }
    }
	}
}
