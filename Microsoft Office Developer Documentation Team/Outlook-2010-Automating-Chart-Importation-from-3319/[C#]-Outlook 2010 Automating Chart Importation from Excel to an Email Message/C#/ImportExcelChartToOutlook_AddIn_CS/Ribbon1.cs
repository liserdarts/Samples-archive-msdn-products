using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;

using System.Diagnostics;
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon1();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


namespace ImportExcelChartToOutlook_AddIn_CS
{
    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        //You should remove the line below if you're not using it
        //as well as any other code that is not actually used.
        //private Office.IRibbonUI ribbon;

        private Outlook.Application olApplication;

        // Override constructor to pass a trusted Outlook Application object from ThisAddIn to this ribbon class.
        public Ribbon1(Outlook.Application outlookApplication)
        {
            olApplication = outlookApplication as Outlook.Application;
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            string customUI = string.Empty;
            Debug.WriteLine(ribbonID);
            switch (ribbonID)
            {
                case "Microsoft.Outlook.Mail.Compose":
                    customUI = GetResourceText("ImportExcelChartToOutlook_AddIn_CS.Ribbon1.xml");
                    return customUI;
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, select the Ribbon XML item in Solution Explorer and then press F1.

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            ThisAddIn.m_Ribbon = ribbonUI;
        }

        // Only show MyTab when Inspector is in compose mode.
        public bool MyTabInspector_GetVisible(Office.IRibbonControl control)
        {
            if (control.Context is Outlook.Inspector)
            {
                Outlook.Inspector oInsp =
                    control.Context as Outlook.Inspector;
                if (oInsp.CurrentItem is Outlook.MailItem)
                {
                    Outlook.MailItem oMail =
                        oInsp.CurrentItem as Outlook.MailItem;
                    if (oMail.Sent == false)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // OnMyButtonClick routine handles click events
        // of the Copy Excel Chart button.
        public void OnMyButtonClick(Office.IRibbonControl control)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWb;
            Excel.Worksheet xlWs;
            Excel.Chart xlChart;
            Word.Document wdDoc;
            Word.Range wdRange; 

            if (control.Context is Outlook.Inspector)
            {
                // Use a try-catch block for error handling.
                try
                {
                    // Check whether there is an Excel process running.
                    if (Process.GetProcessesByName("EXCEL").Count() > 0)
                    {

                        // If so, use the GetActiveObject method to obtain the process and 
                        // cast it to an Application object.
                        xlApp = Marshal.GetActiveObject("Excel.Application") as Excel.Application;
                    }
                    else
                    {
                        // If not, create a new instance of Excel.
                        xlApp = new Excel.Application();
                    }

                    if (xlApp == null)
                    {
                        Console.WriteLine(
                            "EXCEL could not be started. Check that your office installation and project references are correct.");
                        return;
                    }

                    xlApp.Visible = true;

                    // Obtain workbook from user.
                    OpenFileDialog formOpenWorkbook = new OpenFileDialog();
                    formOpenWorkbook.AddExtension = true;
                    formOpenWorkbook.DefaultExt = ".xlsx";
                    formOpenWorkbook.Title = "Open Workbook";
                    formOpenWorkbook.ValidateNames = true;
                    formOpenWorkbook.Filter = "Workbook (*.xlsx;*.xls)|*.xlsx;*.xls";
                    formOpenWorkbook.FilterIndex = 1;
                    formOpenWorkbook.CheckFileExists = true;
                    formOpenWorkbook.CheckPathExists = true;

                    if (formOpenWorkbook.ShowDialog() == DialogResult.OK)
                    {

                        // If the workbook is already open, set the Notify parameter to false  
                        // so to open workbook read-only and to avoid error.
                        xlWb = xlApp.Workbooks.Open(formOpenWorkbook.FileName, 0, false, 5, 
                            "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);

                        // Copy first chart on the first worksheet, assuming the chart already has 
                        // the intended data. Alternatively, use the Excel object model to connect 
                        // to a database, update the chart, and then copy the chart.
                        xlWs = xlWb.Worksheets.Item[1];
                        xlChart = xlWs.ChartObjects(1).Chart;
                        
                        xlChart.CopyPicture();

                        Outlook.Inspector insp =
                            control.Context as Outlook.Inspector;

                        // Obtain the Range object that represents the email body
                        // in the current inspector.
                        wdDoc = insp.WordEditor;
                        wdRange = wdDoc.Range();

                        // Paste the chart to the beginning of the message in the Word editor in 
                        // Outlook. Alternatively, use the Range object in the Word object model 
                        // to specify the insertion point for the chart.
                        wdRange.Paste();                                     
                    }
                    else
                        MessageBox.Show("Specify an existing worksheet.", 
                            "ImportExcelChartToOutlook_Addin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message, "ImportExcelChartToOutlook_Addin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
