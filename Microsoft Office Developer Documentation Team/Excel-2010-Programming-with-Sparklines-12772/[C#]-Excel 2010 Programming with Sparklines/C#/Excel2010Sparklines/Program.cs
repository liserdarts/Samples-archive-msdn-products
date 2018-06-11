using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Interop.Excel;

namespace Excel2010Sparklines
{
  class Program
  {
    static void Main(string[] args)
    {
      //Declare variables that hold references to Excel objects.
      Application eApplication = null;
      Workbook eWorkbook = null;
      Worksheet sheet = null;

      Range sparklineLocation;
      SparklineGroup sparkline;

      //Declare helper variables.
      string workBookName = @"C:\temp\Excel2010Sparklines.xlsx";
      string workSheetName = @"Book Sales";

      try
      {
        //Create an instance of Excel.
        eApplication = new Application();

        //Create a workbook and add a worksheet.
        eWorkbook = eApplication.Workbooks.Add(
            XlWBATemplate.xlWBATWorksheet);
        sheet = (Worksheet)(eWorkbook.Worksheets[1]);
        sheet.Name = workSheetName;

        //Add trending data to the worksheet.
        //The custom SetRow helper function located at the end of this 
        //  source file is used to add a row of values to the worksheet.
        SetRow(sheet, 1, "Book Category",
          "Sales 2008", "Sales 2007", "Sales 2006", "Sales 2005");
        SetRow(sheet, 2, "Fiction", "702", "312", "1170", "1123.2");
        SetRow(sheet, 3, "Nonfiction", "789", "741", "592", "62");
        SetRow(sheet, 4, "Technical", "2607", "2261", "1104", "1776");
        SetRow(sheet, 5, "Business", "990", "1045", "935", "693");
        SetRow(sheet, 6, "Childrens", "490", "420", "352", "368");
        sheet.Columns.AutoFit();

        //Add a Sparkline to the cell at the end of the first row.
        sparklineLocation = sheet.get_Range("F2");
        sparkline = sparklineLocation.SparklineGroups.Add(
          XlSparkType.xlSparkLine, "B2:E2");

        //Format the Sparkline by defining a color theme.
        sparkline.SeriesColor.ThemeColor = 5;

        //Display a mark for the high and low data point.
        sparkline.Points.Highpoint.Visible = true;
        sparkline.Points.Lowpoint.Visible = true;

        //Copy the Sparkline to the end of the remaining rows.
        sparklineLocation.Copy(sheet.get_Range("F3:F6"));

        //Save the workbook.
        sheet.get_Range("A1").Activate();

        eWorkbook.SaveAs(workBookName,
          AccessMode: XlSaveAsAccessMode.xlNoChange);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        //Release the references to the Excel objects.
        sparklineLocation = null;
        sparkline = null;
        sheet = null;

        //Release the Workbook object.
        if (eWorkbook != null)
          eWorkbook = null;

        //Release the ApplicationClass object.
        if (eApplication != null)
        {
          eApplication.Quit();
          eApplication = null;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        GC.WaitForPendingFinalizers();
      }
    }
    /// <summary>
    /// Helper method to set values for a row of cells.
    /// </summary>
    static void SetRow(Worksheet sheet, int row, params string[] values)
    {
      for (int x = 0; x < values.Length; x++)
      {
        sheet.Cells[row, x + 1] = values[x];
      }
    }
  }
}
