using System;
using System.Collections;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public static class ExcelHelper
    {
        private static int _fileCounter = 0;

        private static Workbook wb = null;

        public static void LoadAndCloseExcelSheet(
            string name, 
            byte[] resource, 
            DataSet data, 
            string fileName)
        {
            if (File.Exists(fileName))
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                File.Delete(fileName);
            }
            LoadExcelSheet(name, resource, data, true);
            wb.Close(true, fileName, Type.Missing);
        }

        public static void LoadExcelSheet(string name, byte[] resource, DataSet data, bool isXlMinimized)
        { 
            //for faster processing, do not do screen updates yet
            Globals.ThisAddIn.Application.ScreenUpdating = false;

            _fileCounter++;

            string workbook =
                Path.Combine(
                    Path.GetTempPath(),
                    string.Format("{0}_{1}.xlsx", name, _fileCounter));

            File.WriteAllBytes(workbook, resource);

            wb = Globals.ThisAddIn.Application.Workbooks.Open(
                workbook,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing);
            if (isXlMinimized)
            {
                Globals.ThisAddIn.Application.WindowState = XlWindowState.xlMinimized;
            }

            IEnumerator xmlMaps = wb.XmlMaps.GetEnumerator();
            while (xmlMaps.MoveNext())
            {
                XmlMap map = xmlMaps.Current as XmlMap;
                if (map != null)
                {
                    map.ImportXml(data.GetXml(), true);
                    break;
                }
            }
            Globals.ThisAddIn.Application.ScreenUpdating = true;
        }

        public static void LoadExcelSheet(string name, byte[] resource, DataSet data)
        {
            LoadExcelSheet(name, resource, data, false);
        }
    }
}
