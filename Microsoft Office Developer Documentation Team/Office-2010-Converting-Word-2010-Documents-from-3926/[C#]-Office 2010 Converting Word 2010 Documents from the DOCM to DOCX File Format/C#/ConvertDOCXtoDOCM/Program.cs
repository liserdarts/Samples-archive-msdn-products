using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;


namespace ConvertDOCXtoDOCM
{
  class Program
  {
    static void Main(string[] args)
    {
      WDConvertDOCMtoDOCX("C:\\Temp\\WithMacros.docm");
    }

    // Given a DOCM file (with macro storage), remove the VBA 
    // project, reset the document type, and save the document with a new name.
    public static void WDConvertDOCMtoDOCX(string fileName)
    {
      bool fileChanged = false;

      using (WordprocessingDocument document = WordprocessingDocument.Open(fileName, true))
      {
        var docPart = document.MainDocumentPart;
          // Look for the VBA part. If it's there, delete it.
        var vbaPart = docPart.VbaProjectPart;
        if (vbaPart != null)
        {
          docPart.DeletePart(vbaPart);
          docPart.Document.Save();

          // Change the document type so that it no 
          // longer thinks it is macro-enabled.
          document.ChangeDocumentType(
            WordprocessingDocumentType.Document);

          // Track that the document has been changed.
          fileChanged = true;
        }
      }

      // If anything goes wrong in this file handling,
      // the code will raise an exception back to the caller.
      if (fileChanged)
      {
        var newFileName = Path.ChangeExtension(fileName, ".docx");
        if (File.Exists(newFileName))
        {
          File.Delete(newFileName);
        }
        File.Move(fileName, newFileName);
      }
    }
  }
}
