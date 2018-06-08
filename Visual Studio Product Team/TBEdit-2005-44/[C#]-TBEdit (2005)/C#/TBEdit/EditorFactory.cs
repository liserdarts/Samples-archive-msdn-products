using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell;

using IServiceProvider = System.IServiceProvider;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Vsip.TBEdit
{
    [Guid("2FF5847E-B607-4ed1-8A9D-4584803C3D47")]
    class EditorFactory : IVsEditorFactory, IVsEditorFactoryNotify
    {
        private IOleServiceProvider vsServiceProvider = null;

        #region IVsEditorFactory Members

        public int Close()
        {
            return VSConstants.S_OK;
        }

        public int CreateEditorInstance(uint grfCreateDoc, string pszMkDocument, string pszPhysicalView, IVsHierarchy pvHier, uint itemid, IntPtr punkDocDataExisting, out IntPtr ppunkDocView, out IntPtr ppunkDocData, out string pbstrEditorCaption, out Guid pguidCmdUI, out int pgrfCDW)
        {
            ppunkDocView = new System.IntPtr();
            ppunkDocData = new System.IntPtr();
            pguidCmdUI = Guid.Empty;
            pgrfCDW = 0;
            pbstrEditorCaption = null;

            // validate inputs
            if ((grfCreateDoc & (VSConstants.CEF_OPENFILE | VSConstants.CEF_SILENT)) == 0)
            {
                Debug.Assert(false, "Only Open or Silent is valid");
                return VSConstants.E_INVALIDARG;
            }

            if (punkDocDataExisting != IntPtr.Zero)
            {
                return VSConstants.VS_E_INCOMPATIBLEDOCDATA;
            }

            // create VsTextBuffer object
            Guid guidTextLines = typeof(IVsTextLines).GUID;
            Guid clsidTextBuffer = typeof(VsTextBufferClass).GUID;
            IVsTextLines vsTextLines = (IVsTextLines)TBEdit.Instance.CreateInstance(ref clsidTextBuffer, ref guidTextLines, typeof(IVsTextLines));

            // site it, so it can qs for various services
            IObjectWithSite ows = (IObjectWithSite)vsTextLines;
            ows.SetSite(vsServiceProvider);

            // create the tabbed multi-view editor 
            TBEditor newEditor= new TBEditor(vsTextLines);
            ppunkDocData = Marshal.GetIUnknownForObject(vsTextLines);
            ppunkDocView = Marshal.GetIUnknownForObject(newEditor);
            pbstrEditorCaption = "";

            return VSConstants.S_OK;
        }

        public int MapLogicalView(ref Guid rguidLogicalView, out string pbstrPhysicalView)
        {
            // map all logical views to our one and only physical view
            pbstrPhysicalView = "";
            return VSConstants.S_OK;
        }

        public int SetSite(Microsoft.VisualStudio.OLE.Interop.IServiceProvider psp)
        {
            vsServiceProvider = psp;
            return VSConstants.S_OK;
        }

        #endregion

        #region IVsEditorFactoryNotify Members

        public int NotifyDependentItemSaved(IVsHierarchy pHier, uint itemidParent, string pszMkDocumentParent, uint itemidDpendent, string pszMkDocumentDependent)
        {
            Debug.WriteLine("EditorFactory's NotifyDependentItemSaved called!!!");
            return VSConstants.S_OK;
        }

        public int NotifyItemAdded(uint grfEFN, IVsHierarchy pHier, uint itemid, string pszMkDocument)
        {
            Debug.WriteLine("EditorFactory's NotifyItemAdded called!!!");
            return VSConstants.S_OK;
        }

        public int NotifyItemRenamed(IVsHierarchy pHier, uint itemid, string pszMkDocumentOld, string pszMkDocumentNew)
        {
            Debug.WriteLine("EditorFactory's NOtifyItemRenamed called!!!");
            return VSConstants.S_OK;
        }

        #endregion
    }
}
