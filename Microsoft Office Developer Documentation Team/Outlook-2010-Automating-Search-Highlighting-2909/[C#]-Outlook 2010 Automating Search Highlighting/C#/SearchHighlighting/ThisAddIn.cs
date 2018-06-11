using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SearchHighlighting
{
    public partial class ThisAddIn
    {
        /// <summary>A reference to the custom ribbon UI object.</summary>
        /// <remarks>This field is set by the SearchRibbon object's Load
        /// event handler.</remarks>
        internal static Office.IRibbonUI ribbonUI;

        /// <summary>A reference to the SearchRibbon object.</summary>
        private SearchRibbon ribbon;

        private Outlook.Explorer explorer;

        /// <summary>Handles the start up event of the add-in.</summary>
        /// <remarks>Subscribes to the folder switch event of the active
        /// explorer.</remarks>
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            explorer = Application.ActiveExplorer();
            explorer.FolderSwitch +=
                new Outlook.ExplorerEvents_10_FolderSwitchEventHandler(
                    explorer_FolderSwitch);
        }

        /// <summary>Handles the shut down event of the add-in.</summary>
        /// <remarks>Unsubscribes from the folder switch event.</remarks>
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            explorer.FolderSwitch +=
                new Outlook.ExplorerEvents_10_FolderSwitchEventHandler(
                    explorer_FolderSwitch);
            explorer = null;
            ribbonUI = null;
        }

        /// <summary>Handles the folder switch event of an explorer.</summary>
        void explorer_FolderSwitch()
        {
            //Set the Search string
            if (ribbon.SearchText != null && ribbon.SearchText != string.Empty)
            {
                ribbon.SearchText = string.Empty;
                if (ribbonUI != null)
                {
                    ribbonUI.Invalidate();
                }
            }
        }

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return ribbon = new SearchRibbon();
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
