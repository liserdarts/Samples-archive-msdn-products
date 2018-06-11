using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace XLGetAllSheets
{
  class Program
  {

    const string DEMOFILE = @"C:\Samples\SampleWorkbook.xlsx";

    static void Main(string[] args)
    {
      var results = XLGetAllSheets(DEMOFILE);
      foreach (Sheet item in results)
      {
        Console.WriteLine(item.Name);
      }
    }

    // Retrieve a List of all the sheets in a workbook.
    // The OpenXml SDK 2.0 makes this really easy: The Sheets
    // class contains a collection of OpenXmlElement objects,
    // each representing one of the sheets.
    public static Sheets XLGetAllSheets(string fileName)
    {
        Sheets theSheets = null;

        using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
        {
          WorkbookPart wbPart = document.WorkbookPart;
          theSheets = wbPart.Workbook.Sheets;
        }
        return theSheets;
    }

  }
}
