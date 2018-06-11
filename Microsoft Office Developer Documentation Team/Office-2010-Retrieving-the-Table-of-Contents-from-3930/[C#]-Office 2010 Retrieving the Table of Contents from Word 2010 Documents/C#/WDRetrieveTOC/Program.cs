using System.Linq;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace WDRetrieveTOC
{
  class Program
  {
    static void Main(string[] args)
    {
      var result = WDRetrieveTOC(@"C:\temp\toc.docx");
      Console.WriteLine(result.Value.ToString());
    }

    // Given a document, return its table of contents, if it exists, as an XElement.
    public static XElement WDRetrieveTOC(string fileName)
    {
      XElement TOC = null;

      using (var document = WordprocessingDocument.Open(fileName, false))
      {
        var docPart = document.MainDocumentPart;
        var doc = docPart.Document;

        OpenXmlElement block = doc.Descendants<DocPartGallery>().
          Where(b => b.Val.HasValue && 
            (b.Val.Value == "Table of Contents")).FirstOrDefault();

        if (block != null)
        {
          // Back up to the enclosing SdtBlock and return that XML.
          while ((block != null) && (!(block is SdtBlock)))
          {
            block = block.Parent;
          }
          TOC = new XElement("TOC", block.OuterXml);
        }
      }
      return TOC;
    }
  }
}
