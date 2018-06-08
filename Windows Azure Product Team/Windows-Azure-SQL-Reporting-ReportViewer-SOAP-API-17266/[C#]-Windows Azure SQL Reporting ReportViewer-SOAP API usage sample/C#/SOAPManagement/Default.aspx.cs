//-----------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Security.Principal;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ReportService2010; // The namespace of the Web service proxy in ReportingService2010.cs.

namespace SOAPManagement
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Use our custom RSClient proxy (an extension of ReportingService2010), which has cookie management.
            RSClient rs = null;

            try
            {
                rs = new RSClient();

                // Sets Url and credentials for the endpoint.
                rs.Url = String.Format("https://{0}:443/ReportServer/ReportService2010.asmx", ConfigurationManager.AppSettings["RSSERVER_NAME"]);
                NetworkCredential cred = new NetworkCredential(
                    ConfigurationManager.AppSettings["RSUSERNAME"],
                    ConfigurationManager.AppSettings["RSPASSWORD"],
                    ConfigurationManager.AppSettings["RSSERVER_NAME"]);
                rs.Credentials = cred;
                rs.LogonUser(cred.UserName, cred.Password, cred.Domain);

                // Initialize the ReportViewer control properties
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(String.Format("https://{0}/ReportServer", ConfigurationManager.AppSettings["RSSERVER_NAME"]));
                ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerCredentials(cred.UserName, cred.Password, cred.Domain);

                if (rs.CheckAuthenticated())
                {
                    GridViewRSItems.DataSource = rs.ListChildren("/", true);
                    GridViewRSItems.DataBind();
                }
            }
            catch (Exception)
            {
                if (rs != null)
                    rs.Dispose();
                throw;
            }

            if (rs != null)
                rs.Dispose();
        }

        /// <summary>
        /// Adds item information to each row in the GridView control. For each item: path, type, and size. 
        /// If the item is a report, activate the LinkButton control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void GridViewRSItems_RowDataBound(object sender, GridViewRowEventArgs args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            if (args.Row.RowType == DataControlRowType.DataRow)
            {
                CatalogItem item = (CatalogItem)args.Row.DataItem;

                LinkButton linkItemPath = (LinkButton)args.Row.FindControl("LinkButtonItemPath");
                linkItemPath.Text = item.Path;
                linkItemPath.CommandName = "ItemClick";
                linkItemPath.CommandArgument = item.Path;
                ((Label)args.Row.FindControl("LabelType")).Text = item.TypeName;
                if (item.SizeSpecified)
                    ((Label)args.Row.FindControl("LabelSize")).Text = item.Size.ToString();

                if (String.Compare(item.TypeName, "report", true) == 0)
                {
                    linkItemPath.Enabled = true;
                    linkItemPath.ForeColor = Color.Blue;
                }
                else
                {
                    linkItemPath.Enabled = false;
                    linkItemPath.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// When a user clicks a link to a report, show the report in the ReportViewer control in the right pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void GridViewRSItems_RowCommand(object sender, GridViewCommandEventArgs args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            if (args.CommandName == "ItemClick")
                ReportViewer1.ServerReport.ReportPath = ((LinkButton)args.CommandSource).Text;
        }
    }

    /// <summary>
    /// Class used to extend the default Reporting Services proxy class to enable cookie management, because 
    /// SQL Azure Reporting uses forms authentication.
    /// </summary>
    public class RSClient : ReportingService2010
    {
        private bool m_needLogon = false;
        private string m_authCookieName;
        private Cookie m_authCookie;

        /// <summary>
        /// Gets the type of the item on the report server. Use the new modifier to hide the base implementation.
        /// This method is used to test authentication in CheckAuthenticated().
        /// </summary>
        /// <param name="item">The item path.</param>
        /// <returns>The item type.</returns>
        public new string GetItemType(string item)
        {
            string type = "Unknown";
            try
            {
                type = base.GetItemType(item);
            }
            catch (SoapException)
            {
                return "Unknown";
            }

            return type;
        }

        /// <summary>
        /// Get whether the given credentials can connect to the report server. This method handles the case of redirection 
        /// to a forms logon page.
        /// </summary>
        /// <returns>True if authenticated; otherwise, false. Other errors throw an exception.</returns>
        public bool CheckAuthenticated()
        {
            try
            {
                GetItemType("/");
            }
            catch (WebException e)
            {
                if (!(e.Response is HttpWebResponse) ||
                   ((HttpWebResponse)e.Response).StatusCode != HttpStatusCode.Unauthorized)
                {
                    throw;
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                // This condition could be caused by a redirect to a forms logon page
                Debug.WriteLine("InvalidOperationException in CheckAuthenticated");
                if (m_needLogon)
                {
                    NetworkCredential creds = Credentials as NetworkCredential;
                    if (creds != null && creds.UserName != null)
                    {
                        try
                        {
                            this.CookieContainer = new CookieContainer();
                            this.LogonUser(creds.UserName, creds.Password, null);
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        /// <summary>
        /// Overrides the GetWebRequest method to include the authentication cookie in the request.
        /// </summary>
        /// <param name="uri">The Uri of the request.</param>
        /// <returns>The Web request.</returns>
        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest request;
            request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Credentials = this.Credentials;
            request.CookieContainer = new CookieContainer();
            if (m_authCookie != null)
            {
                request.CookieContainer.Add(m_authCookie);
            }
            return request;
        }

        /// <summary>
        /// Overrides the GetWebResponse() method to save the authentication cookie or sets a flag if RSNotAuthenticated.
        /// </summary>
        /// <param name="request">The Web request.</param>
        /// <returns>The Web response.</returns>
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            string cookieName = response.Headers["RSAuthenticationHeader"];
            if (cookieName != null)
            {
                m_authCookieName = cookieName;
                HttpWebResponse webResponse = (HttpWebResponse)response;
                Cookie authCookie = webResponse.Cookies[cookieName];

                // save it away 
                m_authCookie = authCookie;
            }

            // need to call logon
            if (response.Headers["RSNotAuthenticated"] != null)
            {
                m_needLogon = true;
            }
            return response;
        }
    }

    /// <summary>
    /// Implementation of IReportServerCredentials to supply credentials to SQL Azure Reporting using GetFormsCredentials() 
    /// with custom credentials.
    /// </summary>
    public class ReportServerCredentials : IReportServerCredentials
    {
        private string _userName;
        private string _password;
        private string _domain;

        public ReportServerCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                return null;
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = _userName;
            password = _password;
            authority = _domain;
            return true;
        }
    }
}