using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;

namespace ScheduleTracking_AddIn
{
    [ComVisible(true)]
    public class ScheduleTrackingRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("ScheduleTracking_AddIn.ScheduleTrackingRibbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        // Create callback methods here. For more information about adding 
        // callback methods, select the Ribbon XML item in Solution Explorer 
        // and then press F1

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public void GenerateReportButton_Clicked(Office.IRibbonControl control)
        {
            RequestSummaryForm form = new RequestSummaryForm();
            form.Show();
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
