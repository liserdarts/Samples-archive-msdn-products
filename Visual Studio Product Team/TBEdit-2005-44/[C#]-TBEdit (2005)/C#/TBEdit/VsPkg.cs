// VsPkg.cs : Implementation of TBEdit
//

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

[assembly:ComVisible(true)]

namespace Vsip.TBEdit
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\8.0")]
    [InstalledProductRegistration(false, "#100", "#102", "1.0", IconResourceID = 400)]
    [ProvideLoadKey("Standard", "1.0", "TBEdit Package", "Microsoft Corp.", 1)]
    [ProvideMenuResource(1000, 1)]
    [ProvideEditorExtension(typeof(EditorFactory), ".tbedit", 50, EditorFactoryNotify = true, ProjectGuid = "{FAE04EC0-301F-11d3-BF4B-00C04F79EFBC}")]
    [ProvideEditorLogicalView(typeof(EditorFactory), "{7651A702-06E5-11D1-8EBD-00A0C90F26EA}")] // LOGVIEWID_Designer
    [ProvideEditorLogicalView(typeof(EditorFactory), "{A15FBF08-C315-4713-B768-4D184331A410}")] // LOGVIEWID_Layout
    [ProvideEditorLogicalView(typeof(EditorFactory), "{E3F49F61-8AC3-48f9-BA64-5614F0B246A3}")] // LOGVIEWID_Preview
    [Guid("3555bbe4-ac8f-4b43-9648-475decf67408")]
    public sealed class TBEdit : Package
    {
        private EditorFactory editorFactory = null;
        public static TBEdit Instance = null;

        public TBEdit()
        {
            Instance = this;
        }

        internal static string GetResourceString(string resourceName)
        {
            string resourceValue;
            IVsResourceManager resourceManager = (IVsResourceManager)GetGlobalService(typeof(SVsResourceManager));
            if (resourceManager == null)
            {
                throw new InvalidOperationException("Could not get SVsResourceManager service. Make sure the package is Sited before calling this method.");
            }
            Guid packageGuid = typeof(TBEdit).GUID;
            int hr = resourceManager.LoadResourceString(ref packageGuid, -1, resourceName, out resourceValue);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            return resourceValue;
        }

        internal static string GetResourceString(int resourceID)
        {
            return GetResourceString(string.Format("@{0}", resourceID));
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation
        #region Package Members

        protected override void Initialize()
        {
            base.Initialize();

            // create/register editor factory
            editorFactory = new EditorFactory();
            base.RegisterEditorFactory(editorFactory);

            // Add our command handlers for menu (commands must exist in the .ctc file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidTBEditCmdSet, (int)PkgCmdIDList.cmdidMyCommand);
                MenuCommand menuItem = new MenuCommand( new EventHandler(MenuItemCallback), menuCommandID );
                mcs.AddCommand( menuItem );
            }
        }

        #endregion

        private void MenuItemCallback(object sender, EventArgs e)
        {
            // Show a Message Box to prove we were here
            IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result;
            uiShell.ShowMessageBox(
                       0,
                       ref clsid,
                       "TBEdit Package",
                       string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
                       string.Empty,
                       0,
                       OLEMSGBUTTON.OLEMSGBUTTON_OK,
                       OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                       OLEMSGICON.OLEMSGICON_INFO,
                       0,        // false
                       out result);
        }

    }
}