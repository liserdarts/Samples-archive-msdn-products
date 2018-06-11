using System;
using System.Linq;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;

namespace WDGetCustomProperty
{
  class Program
  {
    private const string FILENAME = @"C:\Samples\DocumentProperties.docx";

    static void Main(string[] args)
    {
      DisplayProperty("Disposition");
      DisplayProperty("IsItSafe");
      DisplayProperty("Typist");
      DisplayProperty("FakeProperty");

    }

    static void DisplayProperty(string propName)
    {
      Console.WriteLine("{0} = {1}", 
        propName, WDGetCustomProperty(FILENAME, propName));
    }

    public static string WDGetCustomProperty(string fileName, string propertyName)
    {
      // Given a document name and a custom property, retrieve the value of the property.

      string returnValue = null;

      using (var document = WordprocessingDocument.Open(fileName, false))
      {
        var customProps = document.CustomFilePropertiesPart;
        if (customProps != null)
        {
          // No custom properties? Nothing to return, in that case.
          var props = customProps.Properties;
          if (props != null)
          {
            // This will trigger an exception is the property's Name property is null, but
            // if that happens, the property is damaged, and probably should raise an exception.
            var prop = props.
              Where(p => ((CustomDocumentProperty)p).Name.Value == propertyName).FirstOrDefault();
            // Does the property exist? If so, get the return value.
            if (prop != null)
            {
              returnValue = prop.InnerText;
            }
          }
        }
      }
      return returnValue;
    }
  }
}