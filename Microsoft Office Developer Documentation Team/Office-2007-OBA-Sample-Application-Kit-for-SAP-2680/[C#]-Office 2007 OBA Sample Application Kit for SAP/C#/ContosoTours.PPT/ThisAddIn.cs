using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.IO;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Globals.ThisAddIn.Application.PresentationOpen += 
                new PowerPoint.EApplication_PresentationOpenEventHandler(Application_PresentationOpen);
        }

        void Application_PresentationOpen(Microsoft.Office.Interop.PowerPoint.Presentation Pres)
        {
            if (Pres.Name.ToLower().StartsWith("package"))
            {
                // parse packag id
                string number = Pres.Name.Substring(7, 5);
                int packageID;
                if (Int32.TryParse(number, out packageID))
                {
                    if (Pres.Name.ToLower().Length < 13)
                    {
                        return;
                    }

                    string presName = Pres.Name.Substring(13);
                    if (presName.IndexOf("_") == -1)
                    {
                        return;
                    }
                    string userName = presName.Substring(0,presName.IndexOf("_"));

                    presName = presName.Substring(userName.Length);

                    if (!presName.EndsWith(".pptx") && presName.Length < 5)
                    {
                        return;
                    }
                    string passWord = presName.Substring(0,presName.Length - 5);
                    passWord = AuthenticateSAP.DecryptData(passWord.Substring(1));
                    if (AuthenticateSAP.ValidateUser(userName, passWord))
                    {
                        CreatePresentation pres = new CreatePresentation();
                        pres._fullName = Path.GetFullPath(Pres.FullName);
                        pres._directoryPath = Path.GetDirectoryName(Pres.FullName);
                        pres._xlsxLocationFile = pres._fullName.Replace(".pptx", ".xlsx");
                        pres.GeneratePresentation(packageID);
                    }
                }
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            if (Directory.Exists(Config.PPTTemplate))
            {
                foreach (string fileName in Directory.GetFileSystemEntries(Config.PPTTemplate))
                {
                    FileInfo file = new FileInfo(fileName);
                    if (file.Extension.ToLower() == ".pptx")
                    {
                        File.Delete(fileName);
                    }
                }
            }
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
