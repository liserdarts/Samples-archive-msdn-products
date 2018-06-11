using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using Word = Microsoft.Office.Interop.Word;

namespace SearchHighlighting
{
    [ComVisible(true)]
    public class SearchRibbon : Office.IRibbonExtensibility
    {
        /// <summary>A reference to the custom ribbon UI object.</summary>
        private Office.IRibbonUI ribbonUI;

        /// <summary>The cached search term.</summary>
        internal string currentSearchText = string.Empty;

        public SearchRibbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            if (ribbonID == "Microsoft.Outlook.Explorer")
            {
                return GetResourceText("SearchHighlighting.SearchRibbon.xml");
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Properties

        /// <summary>Accesses the Application property of the add-in.</summary>
        private Outlook.Application Application
        {
            get { return Globals.ThisAddIn.Application; }
        }

        /// <summary>Accesses the active explorer of the application.</summary>
        private Outlook.Explorer ActiveExplorer
        {
            get { return Globals.ThisAddIn.Application.ActiveExplorer(); }
        }

        /// <summary>Gets or sets the cached search text.</summary>
        internal string SearchText
        {
            get { return (currentSearchText); }
            set { currentSearchText = value; }
        }

        #endregion

        #region Ribbon Callbacks

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            ThisAddIn.ribbonUI = ribbonUI;
            this.ribbonUI = ribbonUI;
        }

        /// <summary>Handles the onChange event of the CustomSearch edit box.
        /// </summary>
        /// <param name="control">The source of the event.</param>
        /// <param name="text">The updated text of the EditBox.</param>
        public void TextChanged(Office.IRibbonControl control, string text)
        {
            Outlook.Explorer explorer = ActiveExplorer;
            if (string.IsNullOrWhiteSpace(text))
            {
                currentSearchText = null;
                explorer.ClearSearch();
            }
            else
            {
                currentSearchText = text.Trim();

                explorer.Search(currentSearchText,
                    Outlook.OlSearchScope.olSearchScopeCurrentFolder);

                ribbonUI.ActivateTabMso("TabMail");
            }
        }

        /// <summary>Handles the getText callback of the CustomSearch edit box.
        /// </summary>
        /// <param name="control">The source control of the callback.</param>
        /// <returns>The text to place in the edit box control.</returns>
        public string GetText(Office.IRibbonControl control)
        {
            return currentSearchText;
        }

        /// <summary>Handles the onAction event of the OpenMessage button.
        /// </summary>
        /// <param name="control">The source control of the event.</param>
        public void OpenMessage(Office.IRibbonControl control)
        {
            if (!string.IsNullOrWhiteSpace(currentSearchText))
            {
                // Obtain a reference to selection.
                Outlook.Selection selection = ActiveExplorer.Selection;
                if (selection.Count == 0) return;

                // Open the first item that is a supported Outlook item.
                // The selection may contain various item types.
                IEnumerator e = ActiveExplorer.Selection.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current is Outlook.MailItem)
                    {
                        Outlook.MailItem item = e.Current as Outlook.MailItem;
                        item.Display();
                        FindInInspector(item.GetInspector, currentSearchText);
                        break;
                    }
                    else if (e.Current is Outlook.MeetingItem)
                    {
                        Outlook.MeetingItem item =
                            e.Current as Outlook.MeetingItem;
                        item.Display();
                        FindInInspector(item.GetInspector, currentSearchText);
                        break;
                    }
                }
            }
        }

        #endregion

        /// <summary>Performs search highlighting in an open mail item.
        /// </summary>
        /// <param name="inspector">The inspector for the open item.</param>
        /// <param name="searchText">The text to highlight.</param>
        private void FindInInspector(Outlook.Inspector inspector, string term)
        {
            if (inspector == null || string.IsNullOrWhiteSpace(term)) return;

            if (inspector.IsWordMail())
            {
                Word.Document doc = inspector.WordEditor as Word.Document;

                Word.Range wdRange = doc.Application.Selection.Range;

                Word.Find wdFind = wdRange.Find;
                wdFind.Format = false;
                wdFind.MatchCase = false;
                wdFind.MatchWholeWord = false;

                wdFind.HitHighlight(term,
                    HighlightColor: Word.WdColor.wdColorYellow);
            }
        }

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
