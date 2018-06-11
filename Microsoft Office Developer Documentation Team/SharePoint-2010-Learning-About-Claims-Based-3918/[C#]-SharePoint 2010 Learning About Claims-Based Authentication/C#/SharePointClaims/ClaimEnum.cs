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


namespace SharePointClaims
{
    public class ClaimEnum : IHttpModule
    {

        #region IHttpModule Members

        public void Dispose()
        {
            //Throw new NotImplementedException();
        }

        #endregion

        public void Init(HttpApplication context)
        {
            //Add the handler for post authentication
            context.PostAuthenticateRequest += new EventHandler(context_PostAuthenticateRequest);
        }

        void context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //Look for claims.
            try
            {
                //Get the application and context sources.
                HttpApplication application = (HttpApplication)sender;
                HttpContext context = application.Context;
 
                //Look for the claims identity.
                IClaimsPrincipal cp = context.User as IClaimsPrincipal;

                if (cp != null)
                {
                    StringBuilder claimsInfo = new StringBuilder(2048);

                    //Get the claims identity so we can enumerate claims.
                    IClaimsIdentity ci = (IClaimsIdentity)cp.Identity;

                    //See if there are claims present before running through this.
                    if (ci.Claims.Count > 0)
                    {                        
                        //start the string build
                        claimsInfo.Append("The following claims were found in this request for " + 
                        cp.Identity.Name + ": " + 
                            Environment.NewLine);

                        //Enumerate all claims.
                        foreach (Claim c in ci.Claims)
                        {
                            Debug.WriteLine(c.ClaimType + " = " + c.Value);
                            claimsInfo.Append(c.ClaimType + " = " + c.Value + Environment.NewLine);
                        }
                   
                        //write out the claim infomation to the ULS log.
                        try
                        {
                            SPDiagnosticsCategory category = new
                                SPDiagnosticsCategory("SharePoint Claims Enumeration",
                                TraceSeverity.Medium, EventSeverity.Information);
                            SPDiagnosticsService ds = SPDiagnosticsService.Local;
                            ds.WriteEvent(1963, category, EventSeverity.Information,
                                claimsInfo.ToString());
                        }
                        catch
                        {
                            //Ignore.
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Ignore.
                Debug.WriteLine(ex.Message);
            }

        }

    }
}
