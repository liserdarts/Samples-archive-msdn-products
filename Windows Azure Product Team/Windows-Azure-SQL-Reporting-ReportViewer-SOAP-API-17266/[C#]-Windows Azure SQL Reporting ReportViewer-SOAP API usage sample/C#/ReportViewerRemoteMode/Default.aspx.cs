//-----------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ReportViewerRemoteMode
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(String.Format("https://{0}/reportserver", ConfigurationManager.AppSettings["RSSERVER_NAME"]));
            ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["RSREPORT_PATH"];

            // Create a new instance of an IReportServerCredentials implementation.
            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerCredentials(
                ConfigurationManager.AppSettings["RSUSERNAME"],
                ConfigurationManager.AppSettings["RSPASSWORD"],
                ConfigurationManager.AppSettings["RSSERVER_NAME"]);
        }
    }

    /// <summary>
    /// Implementation of IReportServerCredentials to supply forms credentials to SQL Azure Reporting using GetFormsCredentials() 
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

        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            authCookie = null;
            user = _userName;
            password = _password;
            authority = _domain;
            return true;
        }
    }
}