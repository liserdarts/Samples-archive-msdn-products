using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace Vsip.MyErrorsPackage
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    ///
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
    /// usually implemented by the package implementer.
    ///
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
    /// implementation of the IVsWindowPane interface.
    /// </summary>
    [Guid("7383db3b-ab1f-440b-a9a7-f97020227f83")]
    public class MyToolWindow : ToolWindowPane
    {
        // This is the user control hosted by the tool window; it is exposed to the base class 
        // using the Window property. Note that, even if this class implements IDispose, we are
        // not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
        // the object returned by the Window property.
        private MyControl control;
        private ErrorListProvider errorProvider;

        public ErrorListProvider ErrorProvider
        {
            get { return errorProvider; }
        }

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public MyToolWindow() :
            base(null)
        {
            // Set the window title reading it from the resources.
            this.Caption = MyErrorsPackage.GetResourceString("ToolWindowTitle");
            // Set the image that will appear on the tab of the window frame
            // when docked with an other window
            // The resource ID correspond to the one defined in the resx file
            // while the Index is the offset in the bitmap strip. Each image in
            // the strip being 16x16.
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            control = new MyControl(this);
        }

        /// <summary>
        /// This property returns the handle to the user control that should
        /// be hosted in the Tool Window.
        /// </summary>
        override public IWin32Window Window
        {
            get
            {
                return (IWin32Window)control;
            }
        }

        // EDDO: This is a good place to create/initialize the ErrorListProvider,
        //       as we need to pass a valid IOleServiceProvider to it's constructor.
        public override void OnToolWindowCreated()
        {
            base.OnToolWindowCreated();

            if (errorProvider == null)
            {
                errorProvider = new ErrorListProvider(this);
                errorProvider.ProviderName = "My Errors";
                errorProvider.ProviderGuid = typeof(MyToolWindow).GUID;
                errorProvider.ForceShowErrors();
            }
        }

    }
}
