using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Message.AssignErrorHandlers();
            StartSAPEnvironment();

            #region VSTO generated code
            //this.Application = (Excel.Application)Microsoft.Office.Tools.Excel.ExcelLocale1033Proxy.Wrap(this.Application);
            #endregion
        }

        private void StartSAPEnvironment()
        {
            PseudoProgressForm progress = new PseudoProgressForm(Config.SeedDataLimit / 2);

            progress.ProgressLabel = "Setting the SAP Environment...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            Process[] processes = Process.GetProcessesByName(Config.SAPProcess);

            if (processes == null || processes.Length == 0)
            {
                backgroundWorker.DoWork +=
                    delegate(object obj, DoWorkEventArgs eventArgs)
                    {
                        Process sapProcess = new Process();
                        
                        ProcessStartInfo sapInfo = new ProcessStartInfo(Config.SAPProcessFile);

                        sapInfo.WindowStyle = ProcessWindowStyle.Normal;
                        sapProcess.StartInfo = sapInfo;
                        sapProcess.Start();
                        sapProcess.WaitForExit();
                    };

                backgroundWorker.RunWorkerCompleted +=
                    delegate(object obj, RunWorkerCompletedEventArgs eventArgs)
                    {
                        progress.Close();
                     };

                backgroundWorker.RunWorkerAsync();
                progress.ShowDialog();          
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

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
