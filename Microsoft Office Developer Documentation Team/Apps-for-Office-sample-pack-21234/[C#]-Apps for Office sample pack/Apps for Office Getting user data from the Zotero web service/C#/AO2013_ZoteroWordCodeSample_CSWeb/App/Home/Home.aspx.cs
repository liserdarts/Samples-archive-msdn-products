using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;

namespace AO2013_ZoteroWordCodeSample_CSWeb
{
    public partial class Home : System.Web.UI.Page
    {
        //TODO: Enter your Zotero Client Key and Client Secret in the respective 
        //constants below.
        private const string ClientKey = "";
        private const string ClientSecret = "";
        private const string CallbackUrl = "~/App/Home/Home.aspx";

        private const string cookieName = "zoteroAccessCookie";
        private const string cookie_userId = "userId";
        private const string cookie_accessToken = "accessToken";
        private const string cookie_userName = "userName";

        private const string AuthStatusScriptFormat =
            "<script type=\"text/javascript\">" +
            "var IsOAuthSuccessful = {0};" +
            "</script>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Cookies[cookieName] != null)
                {
                    //Doublecheck to make sure required info is present
                    HttpCookie cookie = Request.Cookies[cookieName];
                    if (cookie.Values[cookie_userId] != null &&
                        cookie.Values[cookie_accessToken] != null)
                    {
                        authenticationScript.Text = string.Format(AuthStatusScriptFormat,"true");
                    }
                    else
                    {
                        //Delete the cookie and refresh the page to try authenticating
                        //again.
                        Response.Cookies[cookieName].Expires = DateTime.MinValue;
                        Response.Redirect(Request.RawUrl);
                    }
                }
                else if (Page.ClientQueryString.Length == 0)
                {
                    if (OpenAuth.AuthenticationClients.GetByProviderName("Zotero") == null)
                    {
                        OpenAuth.AuthenticationClients.Add("Zotero", () =>
                            new ZoteroClient(ClientKey, ClientSecret));
                    }
                    OpenAuth.RequestAuthentication("Zotero", CallbackUrl);
                }
                else
                {
                    var authResult = OpenAuth.VerifyAuthentication(CallbackUrl);
                    if (authResult.IsSuccessful)
                    {
                        var cookie = new HttpCookie(cookieName);
                        cookie.Values.Add(cookie_accessToken, authResult.ExtraData["accessToken"]);
                        cookie.Values.Add(cookie_userId, authResult.ProviderUserId);
                        cookie.Values.Add(cookie_userName, authResult.UserName);
                        cookie.Expires = DateTime.Now.AddYears(10);
                        Response.Cookies.Add(cookie);
                        authenticationScript.Text = string.Format(AuthStatusScriptFormat,"true");
                    }
                    else
                    {
                        authenticationScript.Text = string.Format(AuthStatusScriptFormat,"false");
                    }
                }
            }
        }
    }
}