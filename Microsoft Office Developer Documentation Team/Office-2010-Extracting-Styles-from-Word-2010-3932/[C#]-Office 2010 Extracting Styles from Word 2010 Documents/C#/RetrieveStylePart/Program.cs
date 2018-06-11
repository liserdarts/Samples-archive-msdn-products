using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;


namespace RetrieveStylePart
{
  class Program
  {
    static void Main(string[] args)
    {
      // Retrieve the StylesWithEffects part. You could pass false in the 
      // second parameter to retrieve the Styles part instead:
      var styles = WDExtractStyles(@"C:\temp\StylesFrom.docx", true);
      if (styles != null)
        Console.WriteLine(styles.ToString());
    }


    // Extract the styles or stylesWithEffects part from a document as an XDocument instance.
    public static XDocument WDExtractStyles(
      string fileName, 
      bool getStylesWithEffectsPart = true)
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
            // Create the XDocument:
            styles = XDocument.Load(reader);
          }
        }
      }
      return styles;
    }
   }
}
