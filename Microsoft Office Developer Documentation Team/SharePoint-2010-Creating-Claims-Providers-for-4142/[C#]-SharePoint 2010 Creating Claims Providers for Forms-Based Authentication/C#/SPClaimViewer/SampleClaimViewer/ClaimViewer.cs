using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.IdentityModel.Claims;

namespace SPClaimViewer.ClaimViewer
{
    [ToolboxItemAttribute(false)]
    public class ClaimViewer : WebPart
    {
        public ClaimViewer()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                IClaimsIdentity currentIdentity = System.Threading.Thread.CurrentPrincipal.Identity as IClaimsIdentity;
                writer.Write("---Subject:" + currentIdentity.Name + "<BR/>");

                foreach (Claim claim in currentIdentity.Claims)
                {
                    writer.Write("   ClaimType: " + claim.ClaimType + "<BR/>");
                    writer.Write("   ClaimValue: " + claim.Value + "<BR/");
                    writer.Write("   ClaimValueTypes: " + claim.ValueType + "<BR/>");
                    writer.Write("   Issuer: " + claim.Issuer + "<BR/");
                    writer.Write("   OriginalIssuer: " + claim.OriginalIssuer + "<BR/>");
                    writer.Write("   Properties: " + claim.Properties.Count.ToString() + "<BR/>");
                }
            }
            catch (Exception ex)
            {
                writer.Write("exception occurred: " + ex.Message);
            }


        }
    }
}
