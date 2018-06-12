using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfServiceProvisioningWeb.Pages
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            bool hasManageWebPerms = false;

            //get SharePoint context
            var spContext = Util.ContextUtil.Current;
            using (var clientContext = TokenHelper.GetClientContextWithContextToken(spContext.ContextDetails.AppWebUrl, spContext.ContextDetails.ContextTokenString, Request.Url.Authority))
            {
                //check if the user has ManageWeb permissions from app web
                BasePermissions perms = new BasePermissions();
                perms.Set(PermissionKind.ManageWeb);
                ClientResult<bool> result = clientContext.Web.DoesUserHavePermissions(perms);
                clientContext.ExecuteQuery();
                hasManageWebPerms = result.Value;
            }

            //define initial script
            string script = @"
            function chromeLoaded() {
                $('body').show();
            }
            //function callback to render chrome after SP.UI.Controls.js loads
            function renderSPChrome() {
                //Get the host site logo url from the SPHostLogoUrl parameter
                var hostlogourl = decodeURIComponent(getQueryStringParameter('SPHostLogoUrl'));

                var links = [{
                    'linkUrl': 'mailto:ridize@microsoft.com',
                    'displayName': 'Contact us'
                }];
                ";

            //add link to settings if the current user has ManageWeb permissions
            if (hasManageWebPerms)
            {
                script += @"links.push({
                        'linkUrl': '" + spContext.ContextDetails.AppWebUrl + @"/SSConfig',
                        'displayName': 'Settings'
                    });";
            }

            //add remainder of script
            script += @"
                //Set the chrome options for launching Help, Account, and Contact pages
                var options = {
                    'appIconUrl': hostlogourl,
                    'appTitle': document.title,
                    'settingsLinks': links,
                    'onCssLoaded': 'chromeLoaded()'
                };

                //Load the Chrome Control in the divSPChrome element of the page
                var chromeNavigation = new SP.UI.Controls.Navigation('divSPChrome', options);
                chromeNavigation.setVisible(true);

            }";

            //register script in page
            Page.ClientScript.RegisterClientScriptBlock(typeof(BasePage), "BasePageScript", script, true);

            //call base onload
            base.OnLoad(e);
        }
    }
}