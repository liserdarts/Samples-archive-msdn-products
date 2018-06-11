using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace XLGetDefinedNames
{
  class Program
  {
    static void Main(string[] args)
    {
      var result = XLGetDefinedNames(@"C:\temp\definednames.xlsx");
      foreach (var dn in result)
        Console.WriteLine("{0} {1}", dn.Key, dn.Value);
    }

    public static Dictionary<String, String> 
      XLGetDefinedNames(String fileName)
    {
      // Given a workbook name, return a dictionary of defined names.
      // The pairs include the range name and a string representing the range.

      var returnValue = new Dictionary<String, String>();
        using (SpreadsheetDocument document = 
        SpreadsheetDocument.Open(fileName, false))
      {
        var wbPart = document.WorkbookPart;
        DefinedNames definedNames = wbPart.Workbook.DefinedNames;
        if (definedNames != null)
        {
          foreach (DefinedName dn in definedNames)
             returnValue.Add(dn.Name.Value, dn.Text);
        }
      }
      return returnValue;
    }
  }
}
