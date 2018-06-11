using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools;
using Office = Microsoft.Office.Core;

namespace BCSPowerPointAddin
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        private Dictionary<Presentation, CustomTaskPane> TaskPaneLookup = new Dictionary<Presentation, CustomTaskPane>();
        public Ribbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("BCSPowerPointAddin.Ribbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, select the Ribbon XML item in Solution Explorer and then press F1

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }
        /// <summary>
        /// Callback for BCS button.
        /// </summary>
        /// <param name="ctrl"></param>
        public void OnClick(Office.IRibbonControl ctrl)
        {
            CustomTaskPane taskpane;
            if (TaskPaneLookup.ContainsKey(Globals.ThisAddIn.Application.ActivePresentation))
                taskpane = TaskPaneLookup[Globals.ThisAddIn.Application.ActivePresentation];
            else
            {
                taskpane = Globals.ThisAddIn.CustomTaskPanes.Add(new BCSChartInfo(), "ChartInfo");
                TaskPaneLookup[Globals.ThisAddIn.Application.ActivePresentation] = taskpane;
                taskpane.VisibleChanged += new EventHandler(taskpane_VisibleChanged);
                taskpane.Visible = true;
                taskpane.Width = 350;
                BCSChartInfo ChartInfo = taskpane.Control as BCSChartInfo;
                ChartInfo.m_InsertChart += Globals.ThisAddIn.OnInsertChart;
                ChartInfo.m_UpdateChart += Globals.ThisAddIn.OnUpdateChart;
                ChartInfo.PopulateControls();

            }
        }
        void taskpane_VisibleChanged(object sender, EventArgs e)
        {

            CustomTaskPane taskpane = sender as CustomTaskPane;
            if (taskpane != null && taskpane.Visible == false)
                Globals.ThisAddIn.CustomTaskPanes.Remove(taskpane);
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
