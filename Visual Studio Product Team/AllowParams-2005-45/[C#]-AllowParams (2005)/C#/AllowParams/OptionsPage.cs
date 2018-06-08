using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Vsip.AllowParams
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid("10B01382-6977-4075-8C75-A58252DBD659")]
    public class OptionsPage : DialogPage
    {
        private OptionsControl _ctrl;
        private string _paramDescription;

        public OptionsPage()
        {
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override IWin32Window Window
        {
            get
            {
                _ctrl = new OptionsControl(this);
                _ctrl.Location = new Point(0, 0);
                return _ctrl;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ParametersDescription
        {
            get { return _paramDescription; }
            set 
            { 
                _paramDescription = value;

                // And update ParameterDescription string on the OleMenuCommand
                OleMenuCommandService mcs = this.Site.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
                if (null != mcs)
                {
                    CommandID testCommandId = new CommandID(GuidList.guidAllowParamsCmdSet, (int)PkgCmdIDList.cmdidTestCommand);
                    OleMenuCommand testCommand = mcs.FindCommand(testCommandId) as OleMenuCommand;
                    if (testCommand != null)
                        testCommand.ParametersDescription = _paramDescription;
                }
            }
        }

        protected override void OnActivate(CancelEventArgs e)
        {
            base.OnActivate(e);
            _ctrl.tbParamDesc.Text = ParametersDescription;
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            ParametersDescription = _ctrl.tbParamDesc.Text;
            base.OnApply(e);
        }
               
    }
}
