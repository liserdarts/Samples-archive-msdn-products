using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MusicBrainzSharePoint.SandboxedVisualWebPart1.vstemplate
{
    [ToolboxItem(false)]
    public partial class SandboxedVisualWebPart1 : System.Web.UI.WebControls.WebParts.WebPart
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
