using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.IO;
using System.Collections.Generic;

namespace SharePoint.Workflow.ExcelGenerator.Workflow1
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        readonly string[] excelDocumentHeaderColumns = new[] { "A", "B", "C" };
        const string sourceDocumentPath = "http://intrnate/shared documents/SalesInvoiceTemplate.xlsx";
        const string destinationSitePath = "http://extranet";
        const string destinationDocumentLibraryName = "Invoices";

        public Workflow1()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        private void createAndPublishExcelDocument_ExecuteCode(object sender, EventArgs e)
        {
SPListItem listItem = workflowProperties.Item;
            
using (SPSite sourceSite = new SPSite(workflowProperties.SiteUrl))
{
   SPFile sourceFile = sourceSite.RootWeb.GetFile(sourceDocumentPath);

   using (SPSite destinationSite = new SPSite(destinationSitePath))
   {
      using (SPWeb web = destinationSite.OpenWeb())
      {
         if (sourceFile != null)
         {
            SPFolder destinationDocumentLibrary = 
		   web.Folders[destinationDocumentLibraryName];

            using (Stream sourceFileStream = 
		   sourceFile.OpenBinaryStream())
            {
               using (SpreadsheetDocument spreadSheet = 
                  SpreadsheetDocument.Open(sourceFileStream, true))
               {
                  WorksheetPart worksheetPart = 
			   ReturnWorksheetPart(spreadSheet, "Sheet1");
                  if (worksheetPart != null)
                  {
                     Worksheet worksheet = worksheetPart.Worksheet;
                     InsertTextCellValue(worksheet, "A", 3, 	
			      listItem["Customer"].ToString());
                     InsertTextCellValue(worksheet, "A", 4, 
				listItem["Address"].ToString());
                     InsertTextCellValue(worksheet, "B", 3, 
				"Invoice: " + listItem.Title);
                     InsertTextCellValue(worksheet, "B", 4, "Invoice Date: " + DateTime.Now.ToShortDateString());
                     InsertTextCellValue(worksheet, "A", 7, 
				listItem["Description"].ToString());
                     InsertNumberCellValue(worksheet, "B", 7, 
				listItem["Amount"].ToString());
                     InsertNumberCellValue(worksheet, "B", 9, 
				listItem["Amount"].ToString());
                     double tax = 
                        ((double)listItem["Amount"] * .07);
			   InsertNumberCellValue(worksheet, "B", 10, 
				tax.ToString());
                     double total = ((double)listItem["Amount"] * 
				1.07);
                     InsertNumberCellValue(worksheet, "B", 11, 
				total.ToString());
			   destinationDocumentLibrary.Files.Add("Invoice "
 	                  + listItem.Title + ".xlsx", 
                        sourceFileStream, true);
                  }
               }
            }
         }
      }
   }
}

        }

        private static WorksheetPart ReturnWorksheetPart(SpreadsheetDocument document, string sheetName)
{
	IEnumerable<Sheet> sheets = 
	   document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
	   Elements<Sheet>().Where(s => s.Name == sheetName);
	
	   if (sheets.Count() == 0)
            {
                return null;
            }

            string id = sheets.First().Id.Value;
            WorksheetPart worksheetPart = 
	   (WorksheetPart)document.WorkbookPart.GetPartById(id);

	return worksheetPart;
}

        private void InsertTextCellValue(Worksheet worksheet, string column, uint row, string value)
{
	Cell cell = ReturnCell(worksheet, column, row);
	CellValue v = new CellValue();
	v.Text = value;
	cell.AppendChild(v);
	cell.DataType = new EnumValue<CellValues>(CellValues.String);
	worksheet.Save();
}

        private void InsertNumberCellValue(Worksheet worksheet, string column, uint row, string value)
{
	Cell cell = ReturnCell(worksheet, column, row);
	CellValue v = new CellValue();
      v.Text = value;
      cell.AppendChild(v);
      cell.DataType = new EnumValue<CellValues>(CellValues.Number);
      worksheet.Save();
}

        private static Cell ReturnCell(Worksheet worksheet, string columnName, uint row)
{
	Row targetRow = ReturnRow(worksheet, row);

	if (targetRow == null)
	return null;

	return targetRow.Elements<Cell>().Where(c => 
	   string.Compare(c.CellReference.Value, columnName + row, 
	   true) == 0).First();
}

        private static Row ReturnRow(Worksheet worksheet, uint row)
{
	return worksheet.GetFirstChild<SheetData>().
	Elements<Row>().Where(r => r.RowIndex == row).First();
}

    }
}
