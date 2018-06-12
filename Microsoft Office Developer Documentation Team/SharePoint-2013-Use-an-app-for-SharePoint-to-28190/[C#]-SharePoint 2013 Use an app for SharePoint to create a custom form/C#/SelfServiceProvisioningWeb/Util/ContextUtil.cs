using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace SelfServiceProvisioningWeb.Util
{
    public class ContextUtil
    {
        
        public SharePointContextToken ContextToken { get; set; }
        public SPContext ContextDetails { get; set; }
        public bool IsValid
        {
            get { return !(ContextToken == null); }
        }

        /// <summary>
        /// Constructor that uses the HttpRequest to set context variables and saves to session
        /// </summary>
        /// <param name="request"></param>
        public ContextUtil(HttpRequest request)
        {
            ContextDetails = new SPContext()
            {
                ServerUrl = request.Url.Authority,
                HostWebUrl = HttpContext.Current.Request["SPHostUrl"],
                AppWebUrl = HttpContext.Current.Request["SPAppWebUrl"],
                ContextTokenString = TokenHelper.GetContextTokenFromRequest(request)
            };
            
            if (ContextToken == null)
            {
                try
                {
                    ContextToken = TokenHelper.ReadAndValidateContextToken(ContextDetails.ContextTokenString, ContextDetails.ServerUrl);
                }
                catch (Exception)
                {
                    ContextToken = null;
                }
            }

            //this is a new context so we will add it as cookie
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            HttpCookie cookie = new HttpCookie("SPContext", serializer.Serialize(ContextDetails));
            cookie.Expires = DateTime.Now.AddHours(12);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public ContextUtil(SPContext context)
        {
            ContextDetails = context;
            try
            {
                ContextToken = TokenHelper.ReadAndValidateContextToken(ContextDetails.ContextTokenString, ContextDetails.ServerUrl);
            }
            catch (Exception)
            {
                ContextToken = null;
            }
        }

        public ContextUtil()
        {
        }

        /// <summary>
        /// Gets the current context from HttpRequest
        /// </summary>
        public static ContextUtil Current
        {
            get
            {
                ContextUtil spContext = null;
                if (HttpContext.Current.Request.Cookies["SPContext"] != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    spContext = new ContextUtil((SPContext)serializer.Deserialize(HttpContext.Current.Request.Cookies["SPContext"].Value, typeof(SPContext)));
                }
                if (spContext == null || !spContext.IsValid)
                    spContext = new ContextUtil(HttpContext.Current.Request);

                if (spContext.IsValid)
                    return spContext;
                else
                {
                    HttpContext.Current.Response.Redirect(GetRedirectUrl());
                    return null;
                }
            }
        }

        /// <summary>
        /// Builds a redirect 
        /// </summary>
        /// <returns></returns>
        private static string GetRedirectUrl()
        {
            string hostWebUrl = HttpContext.Current.Request["SPHostUrl"];
            return TokenHelper.GetAppContextTokenRequestUrl(hostWebUrl, HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));
        }
    }

    public class SPContext
    {
        public string HostWebUrl { get; set; }
        public string AppWebUrl { get; set; }
        public string ContextTokenString { get; set; }
        public string ServerUrl { get; set; }
    }
}