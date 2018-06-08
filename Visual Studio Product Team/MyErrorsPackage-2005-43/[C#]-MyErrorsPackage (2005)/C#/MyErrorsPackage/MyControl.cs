using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using IServiceProvider = System.IServiceProvider;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Vsip.MyErrorsPackage
{
    /// <summary>
    /// Summary description for MyControl.
    /// </summary>
    public class MyControl : System.Windows.Forms.UserControl
    {
        private MyToolWindow toolWindow;
        private ListBox errorListbox;
        private TextBox txtErrorDesc;
        private Button btnAddError;
        private Button btnClear;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        public MyControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        public MyControl(MyToolWindow toolWindow)
        {
            InitializeComponent();
            this.toolWindow = toolWindow;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        /// <summary> 
        /// Let this control process the mnemonics.
        /// </summary>
        protected override bool ProcessDialogChar(char charCode)
        {
              // If we're the top-level form or control, we need to do the mnemonic handling
              if (charCode != ' ' && ProcessMnemonic(charCode))
              {
                    return true;
              }
              return base.ProcessDialogChar(charCode);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.errorListbox = new System.Windows.Forms.ListBox();
            this.txtErrorDesc = new System.Windows.Forms.TextBox();
            this.btnAddError = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // errorListbox
            // 
            this.errorListbox.FormattingEnabled = true;
            this.errorListbox.Location = new System.Drawing.Point(18, 22);
            this.errorListbox.Name = "errorListbox";
            this.errorListbox.Size = new System.Drawing.Size(394, 238);
            this.errorListbox.TabIndex = 0;
            // 
            // txtErrorDesc
            // 
            this.txtErrorDesc.Location = new System.Drawing.Point(18, 285);
            this.txtErrorDesc.Name = "txtErrorDesc";
            this.txtErrorDesc.Size = new System.Drawing.Size(318, 20);
            this.txtErrorDesc.TabIndex = 1;
            this.txtErrorDesc.TextChanged += new System.EventHandler(this.txtErrorDesc_TextChanged);
            // 
            // btnAddError
            // 
            this.btnAddError.Enabled = false;
            this.btnAddError.Location = new System.Drawing.Point(342, 283);
            this.btnAddError.Name = "btnAddError";
            this.btnAddError.Size = new System.Drawing.Size(70, 23);
            this.btnAddError.TabIndex = 2;
            this.btnAddError.Text = "&Add Error";
            this.btnAddError.UseVisualStyleBackColor = true;
            this.btnAddError.Click += new System.EventHandler(this.btnAddError_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(342, 313);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "&Clear Errors";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // MyControl
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddError);
            this.Controls.Add(this.txtErrorDesc);
            this.Controls.Add(this.errorListbox);
            this.Name = "MyControl";
            this.Size = new System.Drawing.Size(426, 358);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        // EDDO: Use ErrorListProvider to display Error List,
        //       and add an ErrorTask with a Navigate event handler.
        private void btnAddError_Click(object sender, EventArgs e)
        {
            toolWindow.ErrorProvider.Show();

            ErrorTask errTask = new ErrorTask();
            errTask.ErrorCategory = TaskErrorCategory.Error;
            errTask.Line = errorListbox.Items.Add(txtErrorDesc.Text);
            errTask.Text = txtErrorDesc.Text;
            errTask.Navigate += new EventHandler(errTask_Navigate);
 
            toolWindow.ErrorProvider.Tasks.Add(errTask);
            txtErrorDesc.Text = "";
        }

        // EDDO: This event will be called when ErrorTask in the 
        //       Error List is dbl clicked. This handler simply
        //       displays/activates the toolwindow and selects the
        //       corresponding error in the listbox.
        void errTask_Navigate(object sender, EventArgs e)
        {
            IVsWindowFrame toolWindowFrame = toolWindow.Frame as IVsWindowFrame;
            if (toolWindowFrame != null)
            {
                toolWindowFrame.Show();
                errorListbox.Focus();

                ErrorTask errTask = (ErrorTask)sender;
                errorListbox.SelectedIndex = errTask.Line;
            }
        }
        
        // EDDO: Clear all errors from the listbox and the ErrorProvider.
        private void btnClear_Click(object sender, EventArgs e)
        {
            errorListbox.Items.Clear();
            toolWindow.ErrorProvider.Tasks.Clear();
        }

        // EDDO: Enable/Disable the Add Error button.
        private void txtErrorDesc_TextChanged(object sender, EventArgs e)
        {
            if (txtErrorDesc.Text.Length == 0)
                btnAddError.Enabled = false;
            else
                btnAddError.Enabled = true;

        }



    }
}
