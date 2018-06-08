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
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

using IServiceProvider = System.IServiceProvider;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;


namespace Vsip.TBEdit
{
    public partial class TBEditor : UserControl, 
        IVsWindowPane,
        IOleCommandTarget,
        IVsMultiViewDocumentView
    {
        private IOleServiceProvider vsServiceProvider = null;
        private IVsTextLines vsTextLines = null;
 
        public TBEditor()
        {
            InitializeComponent();
        }

        public TBEditor(IVsTextLines buffer)
        {
            vsTextLines = buffer;
            InitializeComponent();
        }

        #region IVsWindowPane Members

        public int ClosePane()
        {
            return VSConstants.S_OK;
        }

        public int CreatePaneWindow(IntPtr hwndParent, int x, int y, int cx, int cy, out IntPtr hwnd)
        {
            Win32Methods.SetParent(Handle, hwndParent);
            hwnd = Handle;
            Size = new System.Drawing.Size(cx - x, cy - y);

            // Note, logical views map to the tab control panes.

            return VSConstants.S_OK;
        }

        public int GetDefaultSize(SIZE[] pSize)
        {
            if (pSize.Length >= 1)
            {
                pSize[0].cx = Size.Width;
                pSize[0].cy = Size.Height;
            }
            return VSConstants.S_OK;
        }

        public int LoadViewState(IStream pStream)
        {
            return VSConstants.S_OK;
        }

        public int SaveViewState(IStream pStream)
        {
            return VSConstants.S_OK;
        }

        public int SetSite(Microsoft.VisualStudio.OLE.Interop.IServiceProvider psp)
        {
            vsServiceProvider = psp;
            return VSConstants.S_OK;
        }

        public int TranslateAccelerator(MSG[] lpmsg)
        {
            // todo: add keyboard accelerator handling here
            return VSConstants.S_FALSE;
        }

        #endregion

        #region IOleCommandTarget Members

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            return (int)Microsoft.VisualStudio.OLE.Interop.Constants.OLECMDERR_E_NOTSUPPORTED;
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            return (int)Microsoft.VisualStudio.OLE.Interop.Constants.OLECMDERR_E_NOTSUPPORTED;
        }

        #endregion

        #region IVsMultiViewDocumentView Members

        public int ActivateLogicalView(ref Guid rguidLogicalView)
        {
            if (rguidLogicalView == VSConstants.LOGVIEWID_Designer ||
                rguidLogicalView == VSConstants.LOGVIEWID_Primary)
                tabControl.SelectedIndex = 0;
            else if (rguidLogicalView == VSConstants.LOGVIEWID_Code)
                tabControl.SelectedIndex = 1;
            else if (rguidLogicalView == VSConstants.LOGVIEWID_TextView)
                tabControl.SelectedIndex = 2;
            else
                return VSConstants.E_INVALIDARG;

            return VSConstants.S_OK;
        }

        public int GetActiveLogicalView(out Guid pguidLogicalView)
        {
            pguidLogicalView = Guid.Empty;

            switch (tabControl.SelectedIndex)
            {
                case 0: // design
                    pguidLogicalView = VSConstants.LOGVIEWID_Designer;
                    return VSConstants.S_OK;
                case 1: // code
                    pguidLogicalView = GuidList.LOGVIEWID_Layout;
                    return VSConstants.S_OK;
                case 2: // text
                    pguidLogicalView = GuidList.LOGVIEWID_Preview;
                    return VSConstants.S_OK;
            }
            return VSConstants.E_FAIL;
        }

        public int IsLogicalViewActive(ref Guid rguidLogicalView, out int pIsActive)
        {
            pIsActive = 0;

            switch (tabControl.SelectedIndex)
            {
                case 0: // design
                    if (rguidLogicalView == VSConstants.LOGVIEWID_Primary ||
                        rguidLogicalView == VSConstants.LOGVIEWID_Designer)
                        pIsActive = -1;
                    break;
                case 1: // layout
                    if (rguidLogicalView == GuidList.LOGVIEWID_Layout)
                        pIsActive = -1;
                    break;
                case 2: // preview
                    if (rguidLogicalView == GuidList.LOGVIEWID_Preview)
                        pIsActive = -1;
                    break;
            }
            return VSConstants.S_OK;
        }

        #endregion
    }
}
