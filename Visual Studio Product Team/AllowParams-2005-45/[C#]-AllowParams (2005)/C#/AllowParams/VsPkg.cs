// VsPkg.cs : Implementation of AllowParams
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

namespace Vsip.AllowParams
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the registration utility (regpkg.exe) that this class needs
    // to be registered as package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // A Visual Studio component can be registered under different regitry roots; for instance
    // when you debug your package you want to register it in the experimental hive. This
    // attribute specifies the registry root to use if no one is provided to regpkg.exe with
    // the /root switch.
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\8.0")]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration(false, "#100", "#102", "1.0", IconResourceID = 400)]
    // In order be loaded inside Visual Studio in a machine that has not the VS SDK installed, 
    // package needs to have a valid load key (it can be requested at 
    // http://msdn.microsoft.com/vstudio/extend/). This attributes tells the shell that this 
    // package has a load key embedded in its resources.
    [ProvideLoadKey("Standard", "1.0", "AllowParams", "Microsoft Corp.", 1)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource(1000, 1)]

    // **********************************************************************************
    // *** These attributes ensure our package is always loaded when either the       ***
    // *** UICONTEXT_NoSolution or UICONTEXT_SolutionExists Command UI guid is active.***
    // *** Generally speaking, this is a heinous thing to do, as our package ought    ***
    // *** to only be loaded on demand.                                               ***
    // **********************************************************************************
    [ProvideAutoLoad("adfc4e64-0397-11d1-9f4e-00a0c911004f")] // UICONTEXT_NoSolution
    [ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")] // UICONTEXT_SolutionExists
    
    [ProvideOptionPage(typeof(OptionsPage), "AllowParams Package", "Settings", 103, 104, true)]
    [ProvideProfile(typeof(OptionsPage), "AllowParams Package", "Settings", 103, 104, true)]
    [Guid("10c83bb4-e36c-44a7-af3d-d1d59f30e3aa")]
    public sealed class AllowParams : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public AllowParams()
        {
        }

        /// <summary>
        /// Helper function that will load a resource string using the standard Visual Studio Resource Manager
        /// Service (SVsResourceManager). Because of the fact that it is using a service, this method can be
        /// called only after the package is sited.
        /// </summary>
        internal static string GetResourceString(string resourceName)
        {
            string resourceValue;
            IVsResourceManager resourceManager = (IVsResourceManager)GetGlobalService(typeof(SVsResourceManager));
            if (resourceManager == null)
                throw new InvalidOperationException("Could not get SVsResourceManager service. Make sure the package is Sited before calling this method.");

            Guid packageGuid = typeof(AllowParams).GUID;
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

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .ctc file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the commands using OleMenuCommand objects
                CommandID menuCommandID = new CommandID(GuidList.guidAllowParamsCmdSet, (int)PkgCmdIDList.cmdidSetParamDescription);
                OleMenuCommand menuItem = new OleMenuCommand(new EventHandler(OnSetParamDescription), menuCommandID);
                
                // Set the ParametersDescription to enable passing an argument to the command via the CommandWindow
                menuItem.ParametersDescription = "$"; // accept any argument string
                mcs.AddCommand(menuItem);

                menuCommandID = new CommandID(GuidList.guidAllowParamsCmdSet, (int)PkgCmdIDList.cmdidTestCommand);
                menuItem = new OleMenuCommand(new EventHandler(OnTestCommand), menuCommandID);
                mcs.AddCommand(menuItem);
            }
        }

        #endregion

        private void DisplayMessageBox(string strCaption, string strMessage)
        {
            IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result;
            uiShell.ShowMessageBox(0, ref clsid, strCaption, strMessage, string.Empty,
                0, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                OLEMSGICON.OLEMSGICON_INFO, 0, out result);
        }

        /// <summary>
        /// This function is the callback used to handle the cmdidSetParamDescription command.
        /// </summary>
        private void OnSetParamDescription(object sender, EventArgs e)
        {
            OleMenuCmdEventArgs eventArgs = (OleMenuCmdEventArgs)e;
            string strCommandArg = eventArgs.InValue.ToString();

            if (strCommandArg.Length > 0)
            {
                // Retrieve the command and set it's ParametersDescription property
                OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
                if (null != mcs)
                {
                    CommandID testCommandId = new CommandID(GuidList.guidAllowParamsCmdSet, (int)PkgCmdIDList.cmdidTestCommand);
                    OleMenuCommand testCommand = mcs.FindCommand(testCommandId) as OleMenuCommand;
                    if (testCommand != null)
                    {
                        // Set the ParametersDescription 
                        testCommand.ParametersDescription = strCommandArg;

                        // Update the AllowParams Options Dialog as well
                        EnvDTE.DTE dte = (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE));
                        dte.get_Properties("AllowParams Package", "Settings").Item("ParametersDescription").Value = strCommandArg;
                    }
                }
            }
            else // invoke options dialog when command invoked with no parameters
            {
                EnvDTE.DTE dte = (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE));
                dte.ExecuteCommand("Tools.Options",typeof(OptionsPage).GUID.ToString());
            }
                
        }

        /// <summary>
        /// This function is the callback used to handle the cmdidTestCommand command.
        /// </summary>
        private void OnTestCommand(object sender, EventArgs e)
        {
            OleMenuCmdEventArgs eventArgs = (OleMenuCmdEventArgs)e;
            string strMsg;

            if (eventArgs.InValue == null)
                strMsg = string.Format("OnTestCommand invoked with no arguments!");
            else
                strMsg = string.Format("OnTestCommand invoked with: {0}", eventArgs.InValue.ToString());

            DisplayMessageBox("AllowParams Sample", strMsg);
        }

    }
}