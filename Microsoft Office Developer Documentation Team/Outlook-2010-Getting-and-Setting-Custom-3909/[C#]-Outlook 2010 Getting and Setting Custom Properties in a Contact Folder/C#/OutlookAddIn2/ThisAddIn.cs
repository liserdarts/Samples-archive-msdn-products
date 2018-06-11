using System;
using System.Diagnostics;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookAddIn2
{
    public partial class ThisAddIn
    {
        private static readonly string opportunitiesFolderName = "Opportunities";

        /// <summary>Initializes the add-in.</summary>
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Outlook.Folder opportunitiesFolder = OpportunitiesFolder;

            // Create the contacts folder titled Opportunities, if the folder does 
            // not already exist.
            if (opportunitiesFolder == null)
            {
                opportunitiesFolder = CreateOpportunitiesFolder();
            }
        }

        /// <summary>Gets the root contacts folder.</summary>
        private Outlook.Folder RootContactsFolder
        {
            get
            {
                // Use the MAPI namespace to get the root contacts folder.
                Outlook.NameSpace outlookNameSpace = this.Application.GetNamespace("MAPI");

                return outlookNameSpace.GetDefaultFolder( Outlook.OlDefaultFolders.olFolderContacts) as Outlook.Folder;
            }
        }

        /// <summary>Gets the contacts folder titled Opportunities.</summary>
        internal Outlook.Folder OpportunitiesFolder
        {
            get
            {
                // Get and return the Opportunities contacts folder, if it exists.
                foreach (Outlook.Folder folder in RootContactsFolder.Folders)
                {
                    if (folder.Name.Equals(opportunitiesFolderName))
                    {
                        return folder;
                    }
                }

                // Otherwise, return null.
                return null;
            }
        }

        /// <summary>Creates a contacts folder titled Opportunities.</summary>
        private Outlook.Folder CreateOpportunitiesFolder()
        {
            try
            {
                // Create the folder.
                Outlook.Folder opportunitiesFolder =
                    RootContactsFolder.Folders.Add(opportunitiesFolderName,
                    Outlook.OlDefaultFolders.olFolderContacts) as Outlook.Folder;

                // Set the default form for this folder to the custom form, so that new 
                // contacts in this folder will use this form.

                // Set the custom form message class property on the folder.
                opportunitiesFolder.PropertyAccessor.SetProperty(
                    Constants.MessageClassID, Constants.MessageClassOpportunities);

                // Set the custom form display name property on the folder.
                opportunitiesFolder.PropertyAccessor.SetProperty(
                    Constants.MessageClassDisplayNameID, Constants.SalesFormDisplayName);

                DefineUserProperties(opportunitiesFolder);

                // Make the Opportunities folder visible.
                opportunitiesFolder.GetExplorer().NavigationPane.CurrentModule.Visible = true;

                return opportunitiesFolder;
            }
            catch (Exception ex)
            {
                // Dump exception information to the debugger.
                Debug.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
                return null;
            }
        }

        /// <summary>Defines the sales user properties for a folder.</summary>
        /// <param name="opportunitiesFolder">The folder in which to define the 
        /// sales user properties.</param>
        private void DefineUserProperties(Outlook.Folder opportunitiesFolder)
        {
            opportunitiesFolder.UserDefinedProperties.Add(
                Constants.encounterDateDisplayName,
                Outlook.OlUserPropertyType.olDateTime,
                true, Outlook.OlFormatDateTime.OlFormatDateTimeLongDayDate);

            opportunitiesFolder.UserDefinedProperties.Add(
                Constants.purchaseEstimateDisplayName,
                Outlook.OlUserPropertyType.olCurrency,
                true, Outlook.OlFormatCurrency.olFormatCurrencyDecimal);

            opportunitiesFolder.UserDefinedProperties.Add(
                Constants.salesRepDisplayName, Outlook.OlUserPropertyType.olText);

            opportunitiesFolder.UserDefinedProperties.Add(
                Constants.salesValuedisplayName,
                Outlook.OlUserPropertyType.olInteger,
                true, Outlook.OlFormatInteger.olFormatIntegerPlain);

            opportunitiesFolder.UserDefinedProperties.Add(
                Constants.tradeShowDisplayName,
                Outlook.OlUserPropertyType.olYesNo,
                true, Outlook.OlFormatYesNo.olFormatYesNoTrueFalse);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return Globals.Factory.GetRibbonFactory().CreateRibbonManager(
                new Microsoft.Office.Tools.Ribbon.IRibbonExtension[] {
                    new OpportunitiesRibbon() });
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
