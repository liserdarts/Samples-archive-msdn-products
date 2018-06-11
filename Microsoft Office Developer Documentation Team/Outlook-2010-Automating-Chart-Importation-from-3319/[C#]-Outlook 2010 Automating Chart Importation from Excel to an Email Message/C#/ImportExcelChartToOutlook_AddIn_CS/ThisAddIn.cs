using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace ImportExcelChartToOutlook_AddIn_CS
{
    public partial class ThisAddIn
    {
        #region Instance Variables
        Outlook.Application m_Application;
        Outlook.Inspectors m_Inspectors;
        internal static Office.IRibbonUI m_Ribbon;
        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // Initialize variables.
            m_Application = this.Application;
            m_Inspectors = m_Application.Inspectors;

            // Hookup the NewInspector event handler.
            m_Inspectors.NewInspector += new
                Outlook.InspectorsEvents_NewInspectorEventHandler(
                m_Inspectors_NewInspector);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Dereference objects.
            m_Ribbon = null;
            m_Inspectors = null;
            m_Application = null;
        }

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon1(m_Application);
        }

        #region Custom event handler

        /// <summary>
        /// The NewInspector event fires whenever a new Inspector is displayed. 
        /// Need to mark custom button for update, by running the getVisible callback before a Read inspector appears.
        /// </summary>
        /// <param name="Inspector"></param>
        void m_Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            m_Ribbon.Invalidate();
        }

        #endregion

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
