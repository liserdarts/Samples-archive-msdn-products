using System.IO;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace ReplaceStylePart
{
  class Program
  {
    static void Main(string[] args)
    {
      const string fromDoc = @"C:\Temp\StylesFrom.docx";
      const string toDoc = @"C:\Temp\StylesTo.docx";

      // Handle the styles part.
      var node = WDExtractStyles(fromDoc, false);
      if (node != null)
        WDReplaceStyles(toDoc, node, false);
      
      // Handle the stylesWithEffects part. To fully support 
      // round-tripping from Word 2010 to Word 2007, you should 
      // replace this part, as well.
      node = WDExtractStyles(fromDoc);
      if (node != null)
        WDReplaceStyles(toDoc, node);

    }

    // Extract the styles or stylesWithEffects part from a document as an XDocument instance.
    public static XDocument WDExtractStyles(string fileName, bool getStylesWithEffectsPart = true)
    {
      XDocument styles = null;

      using (var document = WordprocessingDocument.Open(fileName, false))
      {
        var docPart = document.MainDocumentPart;

        StylesPart stylesPart = null;
        if (getStylesWithEffectsPart)
          stylesPart = docPart.StylesWithEffectsPart;
        else
          stylesPart = docPart.StyleDefinitionsPart;

        if (stylesPart != null)
        {
          using (var reader = XmlNodeReader.Create(
            stylesPart.GetStream(FileMode.Open, FileAccess.Read)))
          {
            styles = XDocument.Load(reader);
          }
        }
      }
      return styles;
    }

    // Given a file and an XDocument containing a full styles or stylesWithEffects part,  
    // replace the styles in the document with the new styles.
    public static void WDReplaceStyles(string fileName, XDocument newStyles, 
      bool setStylesWithEffectsPart = true)
    {
      using (var document = WordprocessingDocument.Open(fileName, true))
      {
        var docPart = document.MainDocumentPart;

        StylesPart stylesPart = null;
        if (setStylesWithEffectsPart)
          stylesPart = docPart.StylesWithEffectsPart;
        else
          stylesPart = docPart.StyleDefinitionsPart;

        if (stylesPart != null)
        {
          newStyles.Save(new StreamWriter(stylesPart.GetStream(
            FileMode.Create, FileAccess.Write)));
        }
      }
    }
  }
}
