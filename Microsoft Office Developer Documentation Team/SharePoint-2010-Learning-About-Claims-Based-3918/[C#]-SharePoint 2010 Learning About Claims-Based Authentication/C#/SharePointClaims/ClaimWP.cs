using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Claims;
using System.Web;
using System.Collections;
using System.Diagnostics;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;

namespace SharePointClaims
{
    public class ClaimWP : System.Web.UI.WebControls.WebParts.WebPart
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            try
            {
                using (SPMonitoredScope ClaimsScope = 
                    new SPMonitoredScope("SharePointClaims.ClaimWP"))
                {
                    //Look for the claims identity.
                    IClaimsPrincipal cp = Page.User as IClaimsPrincipal;

                    if (cp != null)
                    {
                        StringBuilder claimsInfo = new StringBuilder(2048);

                        //Get the claims identity so we can enumerate claims.
                        IClaimsIdentity ci = (IClaimsIdentity)cp.Identity;

                        Table tb = new Table();
                       
                        TableRow tr = new TableRow();
                        TableCell tc = GetCell("The following claims were found in this request:");
                        tc.ColumnSpan = 2;
                        tc.Font.Bold = true;
                        tr.Cells.Add(tc);
                        tb.Rows.Add(tr);

                        //Enumerate all claims.
                        foreach (Claim c in ci.Claims)
                        {
                            tr = new TableRow();
                            tr.Cells.Add(GetCell(c.ClaimType));
                            tr.Cells.Add(GetCell(c.Value));
                            tb.Rows.Add(tr);
                        }

                        tb.RenderControl(writer);
                    }
                }
            }
            catch (Exception ex)
            {
                writer.Write("<b>There was an error rendering claims: " +
                    ex.Message + "</b>");
            }
        }

        private TableCell GetCell(string Value)
        {
            TableCell tc = new TableCell();
            tc.BorderStyle = BorderStyle.Solid;
            tc.BorderWidth = Unit.Pixel(1);
            tc.Text = Value;
            return tc;
        }
    }
}
