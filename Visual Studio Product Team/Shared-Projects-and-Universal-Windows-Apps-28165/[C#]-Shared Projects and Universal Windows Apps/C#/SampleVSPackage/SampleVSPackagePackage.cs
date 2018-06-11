using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace Company.SampleVSPackage
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
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidSampleVSPackagePkgString)]
    public sealed class SampleVSPackagePackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public SampleVSPackagePackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidSampleVSPackageCmdSet, (int)PkgCmdIDList.cmdidMyCommand);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID );
                mcs.AddCommand( menuItem );
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var sharedHier = this.FindSharedProject();
            string sharedCaption = HierarchyUtilities.GetHierarchyProperty<string>(sharedHier, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Caption);
            this.Output(string.Format("Found shared project: {0}\n", sharedCaption));

            var activePlatformHier = this.GetActiveProjectContext(sharedHier);
            string activeCaption = HierarchyUtilities.GetHierarchyProperty<string>(activePlatformHier, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Caption);
            this.Output(string.Format("The active platform project: {0}\n", activeCaption));

            this.Output("Platform projects:\n");
            foreach (IVsHierarchy platformHier in this.EnumImportingProjects(sharedHier))
            {
                string platformCaption = HierarchyUtilities.GetHierarchyProperty<string>(platformHier, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Caption);
                this.Output(string.Format(" * {0}\n", platformCaption));
            }

            this.Output("Walk the active platform project:\n");
            var sharedItemIds = new List<uint>();
            this.InspectHierarchyItems(activePlatformHier, (uint)VSConstants.VSITEMID.Root, 1, sharedItemIds);

            var sharedItemId = sharedItemIds[0];
            string fullPath;
            ErrorHandler.ThrowOnFailure(((IVsProject)activePlatformHier).GetMkDocument(sharedItemId, out fullPath));
            this.Output(string.Format("Shared item full path: {0}\n", fullPath));

            var dte = (EnvDTE.DTE)this.GetService(typeof(EnvDTE.DTE));
            var dteEvents = (EnvDTE80.Events2)dte.Events;
            dteEvents.ProjectItemsEvents.ItemRenamed += this.OnItemRenamed;
            HierarchyUtilities.TryGetHierarchyProperty(activePlatformHier, sharedItemId, (int)__VSHPROPID7.VSHPROPID_SharedProjectHierarchy, out sharedHier);

            uint itemIdInSharedHier;
            int found;
            VSDOCUMENTPRIORITY[] priority = new VSDOCUMENTPRIORITY[1];
            if (ErrorHandler.Succeeded(((IVsProject)sharedHier).IsDocumentInProject(fullPath, out found, priority, out itemIdInSharedHier))
                && found != 0)
            {
                var newName = DateTime.Now.Ticks.ToString() + Path.GetExtension(fullPath);
                ErrorHandler.ThrowOnFailure(sharedHier.SetProperty(itemIdInSharedHier, (int)__VSHPROPID.VSHPROPID_EditLabel, newName));
                this.Output(string.Format("Renamed {0} to {1}\n", fullPath, newName));
            }

            dteEvents.ProjectItemsEvents.ItemRenamed -= this.OnItemRenamed;
        }

        private void Output(string text)
        {
            var output = (IVsOutputWindowPane)this.GetService(typeof(SVsGeneralOutputWindowPane));
            output.OutputStringThreadSafe(text);
        }

        private IVsHierarchy FindSharedProject()
        {
            var sln = (IVsSolution)this.GetService(typeof(SVsSolution));
            Guid empty = Guid.Empty;
            IEnumHierarchies enumHiers;
            ErrorHandler.ThrowOnFailure(sln.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION, ref empty, out enumHiers));

            foreach (IVsHierarchy hier in ComUtilities.EnumerableFrom(enumHiers))
            {
                if (PackageUtilities.IsCapabilityMatch(hier, "SharedAssetsProject"))
                {
                    return hier;
                }
            }

            return null;
        }

        public IVsHierarchy GetActiveProjectContext(IVsHierarchy hierarchy)
        {
            IVsHierarchy activeProjectContext;
            if (HierarchyUtilities.TryGetHierarchyProperty(hierarchy, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID7.VSHPROPID_SharedItemContextHierarchy, out activeProjectContext))
            {
                return activeProjectContext;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<IVsHierarchy> EnumImportingProjects(IVsHierarchy hierarchy)
        {
            IVsSharedAssetsProject sharedAssetsProject;
            if (HierarchyUtilities.TryGetHierarchyProperty(hierarchy, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID7.VSHPROPID_SharedAssetsProject, out sharedAssetsProject)
                && sharedAssetsProject != null)
            {
                foreach (IVsHierarchy importingProject in sharedAssetsProject.EnumImportingProjects())
                {
                    yield return importingProject;
                }
            }
        }

        private void InspectHierarchyItems(IVsHierarchy hier, uint itemid, int level, List<uint> sharedItemIds)
        {
            string caption = HierarchyUtilities.GetHierarchyProperty<string>(hier, itemid, (int)__VSHPROPID.VSHPROPID_Caption);
            this.Output(string.Format("{0}{1}\n", new string('\t', level), caption));

            bool isSharedItem;
            if (HierarchyUtilities.TryGetHierarchyProperty(hier, itemid, (int)__VSHPROPID7.VSHPROPID_IsSharedItem, out isSharedItem)
                && isSharedItem)
            {
                sharedItemIds.Add(itemid);
            }

            uint child;
            if (HierarchyUtilities.TryGetHierarchyProperty(hier, itemid, (int)__VSHPROPID.VSHPROPID_FirstChild, Unbox.AsUInt32, out child)
                && child != (uint)VSConstants.VSITEMID.Nil)
            {
                this.InspectHierarchyItems(hier, child, level + 1, sharedItemIds);

                while (HierarchyUtilities.TryGetHierarchyProperty(hier, child, (int)__VSHPROPID.VSHPROPID_NextSibling, Unbox.AsUInt32, out child)
                    && child != (uint)VSConstants.VSITEMID.Nil)
                {
                    this.InspectHierarchyItems(hier, child, level + 1, sharedItemIds);
                }
            }
        }

        private void OnItemRenamed(EnvDTE.ProjectItem projItem, string oldName)
        {
            this.Output(string.Format("[Event] Renamed {0} to {1} in project {2}\n", oldName, Path.GetFileName(projItem.get_FileNames(1)), projItem.ContainingProject.Name));
        }
    }
}
