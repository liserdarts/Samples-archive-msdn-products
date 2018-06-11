using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Interop.Excel;

namespace Excel2010Slicers
{
  class Program
  {
    static void Main(string[] args)
    {
      // Declare variables that hold references to excel objects
      Application eApplication = null;
      Workbook eWorkbook = null;
      Worksheet sheet = null;
      PivotTable pivotTable = null;
      Range pivotData = null;
      Range pivotDestination = null;
      PivotField salesRegion = null;
      PivotField salesAmount = null;
      ChartObjects chartObjects = null;
      ChartObject pivotChart = null;
      SlicerCache salesTypeSlicer = null;
      SlicerCache salesRegionSlicer = null;
      SlicerCache salesPersonSlicer = null;

      // Declare helper variables
      string workBookName = @"C:\temp\Excel2010Slicers.xlsx";
      string pivotTableName = @"Sales By Type";
      string workSheetName = @"Quarterly Sales";

      try
      {
        // Create an instance of Excel
        eApplication = new Application();

        //Create a workbook and add a worksheet.
        eWorkbook = eApplication.Workbooks.Add(
            XlWBATemplate.xlWBATWorksheet);
        sheet = (Worksheet)(eWorkbook.Worksheets[1]);
        sheet.Name = workSheetName;

        //Add Data to the worksheet.
        //The custom SetRow helper function located at the end of this 
        //  source file is used to add a row of values to the worksheet.
        SetRow(sheet, 1,
          "Sales Region", "Sales Person", "Sales Type", "Sales Amount");
        SetRow(sheet, 2, "West", "Joe", "Wholesale", "123");
        SetRow(sheet, 3, "West", "Joe", "Retail", "432");
        SetRow(sheet, 4, "West", "Joe", "Government", "111");
        SetRow(sheet, 5, "East", "Robert", "Wholesale", "564");
        SetRow(sheet, 6, "East", "Robert", "Retail", "234");
        SetRow(sheet, 7, "East", "Robert", "Government", "321");
        SetRow(sheet, 8, "East", "Michelle", "Wholesale", "940");
        SetRow(sheet, 9, "East", "Michelle", "Retail", "892");
        SetRow(sheet, 10, "East", "Michelle", "Government", "10");
        SetRow(sheet, 11, "West", "Erich", "Wholesale", "120");
        SetRow(sheet, 12, "West", "Erich", "Retail", "45");
        SetRow(sheet, 13, "West", "Erich", "Government", "410");
        SetRow(sheet, 14, "West", "Dafna", "Wholesale", "800");
        SetRow(sheet, 15, "West", "Dafna", "Retail", "3409");
        SetRow(sheet, 16, "West", "Dafna", "Government", "123");
        SetRow(sheet, 17, "East", "Rob", "Wholesale", "777");
        SetRow(sheet, 18, "East", "Rob", "Retail", "450");
        SetRow(sheet, 19, "East", "Rob", "Government", "900");
        sheet.Columns.AutoFit();

        // Select a range of data for the Pivot Table.
        pivotData = sheet.get_Range("A1", "D19");

        // Select location of the Pivot Table.
        pivotDestination = sheet.get_Range("F2");

        // Add a pivot table to the worksheet.
        sheet.PivotTableWizard(
            XlPivotTableSourceType.xlDatabase,
            pivotData,
            pivotDestination,
            pivotTableName
            );

        // Set variables used to manipulate the Pivot Table.
        pivotTable =
          (PivotTable)sheet.PivotTables(pivotTableName);
        salesRegion = ((PivotField)pivotTable.PivotFields(3));
        salesAmount = ((PivotField)pivotTable.PivotFields(4));

        // Format the Pivot Table.
        pivotTable.TableStyle2 = "PivotStyleLight16";
        pivotTable.InGridDropZones = false;

        // Set Sales Region as a Row Field.
        salesRegion.Orientation =
          XlPivotFieldOrientation.xlRowField;

        // Set Sum of Sales Amount as a Value Field.
        salesAmount.Orientation =
          XlPivotFieldOrientation.xlDataField;
        salesAmount.Function = XlConsolidationFunction.xlSum;

        //Add a pivot chart to the work sheet.
        chartObjects = (ChartObjects)sheet.ChartObjects();
        pivotChart = chartObjects.Add(310, 100, 225, 175);
        //Format the pivot chart.
        pivotChart.Chart.ChartWizard(pivotData,
          XlChartType.xlColumnClustered,
          Title: "Sales",
          HasLegend: false,
          CategoryLabels: 3,
          SeriesLabels: 0);

        //Add slicers to the pivot table.
        salesTypeSlicer =
          eWorkbook.SlicerCaches.Add(pivotTable, "Sales Type");
        salesTypeSlicer.Slicers.Add(sheet,
          Top: 10, Left: 540, Width: 100, Height: 100);
        salesRegionSlicer =
          eWorkbook.SlicerCaches.Add(pivotTable, "Sales Region");
        salesRegionSlicer.Slicers.Add(sheet,
          Top: 120, Left: 540, Width: 100, Height: 100);
        salesPersonSlicer =
          eWorkbook.SlicerCaches.Add(pivotTable, "Sales Person");
        salesPersonSlicer.Slicers.Add(sheet,
          Top: 10, Left: 645, Width: 100, Height: 200);

        // Save the workbook.
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
        salesAmount = null;
        salesRegion = null;
        pivotDestination = null;
        pivotData = null;
        pivotChart = null;
        chartObjects = null;
        pivotTable = null;
        salesTypeSlicer = null;
        salesRegionSlicer = null;
        salesPersonSlicer = null;
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