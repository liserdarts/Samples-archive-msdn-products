using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ReportService2010;

namespace ManagePermission
{
    /// <summary>
    /// Class used to extend the default Reporting Services proxy class to enable cookie management, because 
    /// SQL Azure Reporting uses forms authentication.
    /// </summary>
    public partial class SQLReportingAdminPage : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            RSClient rs = null;
            Role[] roles = null;
            try
            {
                if (!IsPostBack)
                {
                    // Create an instance of RSClient
                    rs = new RSClient();

                    // Sets Url and credentials for the endpoint. 
                    rs.Url = String.Format("https://{0}:443/ReportServer/ReportService2010.asmx", ConfigurationManager.AppSettings["RSSERVER_NAME"]);
                    NetworkCredential cred = new NetworkCredential(
                        ConfigurationManager.AppSettings["RSUSERNAME"],
                        ConfigurationManager.AppSettings["RSPASSWORD"],
                        ConfigurationManager.AppSettings["RSSERVER_NAME"]);
                    rs.Credentials = cred;
                    rs.LogonUser(cred.UserName, cred.Password, cred.Domain);
                    
                    // Checks if the user is authenticated
                    if (rs.CheckAuthenticated())
                    {
                        // Displaying Logged in user on the web page
                        lblLoggedInUser.Text = "Logged In User: " + ConfigurationManager.AppSettings["RSUSERNAME"].ToString();

                        // Retrieves all the reports items recursively.
                        gvListChildren.DataSource = rs.ListChildren("/", true);
                        gvListChildren.DataBind();
                        
                        // Roles stored in a session object
                        roles = new Role[1];
                        roles = rs.ListRoles("Catalog", null);
                        Session["roles"] = roles;

                        // The function adds columns to the GridView, and creates a DataTable
                        AddColumns();
                        AddTable();
                    }                   
                }
            }
            catch
            {
                if (rs != null)
                    rs.Dispose();
                throw;
            }
            if (rs != null)
                rs.Dispose();
        }

        #region "Methods"
        // Retrive policies for a specific report item.
        protected Policy[] GetPoliciesForItem(string path)
        {
            RSClient rs = null;
            Boolean val = true;
            Policy[] curPolicy = null;
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
                
                // Checks if the user is authenticated
                if (rs.CheckAuthenticated())
                {
                    curPolicy = new Policy[1];
                    curPolicy = rs.GetPolicies(path, out val);
                }

                // Returns the Policy (users and associated roles) for the specified report item path.
                return curPolicy;
            }
            catch (SoapException ex)
            {
                if (ex.Message.Contains("The permissions granted to user"))                
                    lblException.Text = string.Format("The permissions granted to user '{0}' are insufficient for modifying permissions on the report item path: {1}", ConfigurationManager.AppSettings["RSUSERNAME"].ToString(), path);                    
                
                return null;
            }
            catch (Exception Ex)
            {
                lblException.Text = Ex.Message;
                return null;
            }
            
            finally
            {
                if (rs != null)
                    rs.Dispose();             
            }
        }
       
        // Adds columns to the data grid that displays users with assigned roles.
        protected void AddColumns()
        {
            // Retrieve roles from session object.
            Role[] roles = (Role[])Session["roles"];
            
            // Adding Columns to Grid
            BoundField userName = new BoundField();
            userName.HeaderText = "Group/User";
            userName.DataField = "GroupName";
            gvItemPermissions.Columns.Add(userName);
            
            foreach (Role currentRole in roles)
            {
                CheckBoxField roleName = new CheckBoxField();
                roleName.HeaderText = currentRole.Name;
                roleName.DataField = currentRole.Name;
                roleName.ReadOnly = false;                
                gvItemPermissions.Columns.Add(roleName);             
            }            
        }

        // Creates a table with users and corresponding roles assigned.
        protected void AddTable()
        {
            // Retrieve roles from session object.
            Role[] roles = (Role[])Session["roles"];

            DataTable m_CatalogRoles = new DataTable();

            // Adding Columns to DataTable
            DataColumn dcUserName = new DataColumn();
            dcUserName.ColumnName = "GroupName";
            dcUserName.DataType = System.Type.GetType("System.String");
            dcUserName.Caption = "Group/User";
            m_CatalogRoles.Columns.Add(dcUserName);
            foreach (Role currentRole in roles)
            {
                DataColumn dcRole = new DataColumn();
                dcRole.ColumnName = currentRole.Name;
                dcRole.DataType = System.Type.GetType("System.Boolean");
                m_CatalogRoles.Columns.Add(dcRole);
            }

            // Session variable for datatable. The table has the user and the roles assigned.
            Session["CatalogRoles"] = m_CatalogRoles;
        }

        // Saves the users-permissions
        private void SavePolicies()
        {
            try
            {
                //Retrieve the table from the session object.
                DataTable dt = (DataTable)Session["ItemPermissions"];

                int i = 0;
                int j;
                int rolesindex = 0;
                int usersindex = 0;
                bool dbroundtrip = false;

                
                // Identify the users with atleast one role selected.
                foreach (GridViewRow gvr in gvItemPermissions.Rows)
                {
                    rolesindex = 0;
                    usersindex++;
                    for (int cells = 2; cells < gvr.Cells.Count; cells++)
                    {
                        if (((CheckBox)(gvr.Cells[cells].Controls[0])).Checked)
                        {
                            rolesindex++;
                        }
                    }
                    if (rolesindex == 0)
                    {
                        usersindex--;
                    }
                }

                // Based on number of users, create an array of policies.
                Policy[] newPolicy = new Policy[gvItemPermissions.Rows.Count];
                if (usersindex < gvItemPermissions.Rows.Count)
                {
                    dbroundtrip = true;
                }
                

                // Update users, and the corresponding permissions.
                foreach (GridViewRow gvr in gvItemPermissions.Rows)
                {
                    Policy currentPolicy = new Policy();
                    currentPolicy.GroupUserName = gvr.Cells[1].Text.ToString();

                    rolesindex = 0;
                    for (int cells = 2; cells < gvr.Cells.Count; cells++)
                    {
                        Role currentRole = new Role();
                        if (((CheckBox)(gvr.Cells[cells].Controls[0])).Checked)
                        {
                            rolesindex++;
                        }
                    }
                    if (rolesindex != 0)
                    {
                        Role[] roles = new Role[rolesindex];
                        j = 0;
                        for (int cells = 2; cells < gvr.Cells.Count; cells++)
                        {
                            Role currentRole = new Role();
                            if (((CheckBox)(gvr.Cells[cells].Controls[0])).Checked)
                            {
                                currentRole.Name = ((System.Web.UI.WebControls.DataControlFieldCell)(gvr.Cells[cells])).ContainingField.ToString();
                                roles[j] = currentRole;
                                j++;
                            }
                        }
                        currentPolicy.Roles = roles;
                        newPolicy[i] = currentPolicy;
                        i++;
                    }
                    // Let the user be associated with at least a single role. Do not allow deleting users.
                    else
                    {
                        lblInfo.Text = string.Format("At least a single role must be selected for the user '{0}'.", currentPolicy.GroupUserName);
                        ModalPopupExtender1.Show();
                        return;                       
                    }
                }

                // Below code snippet reloads the users, and associated roles by making a round trip to the report server catalog db.
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

                    if (rs.CheckAuthenticated())
                    {
                        // Update policies to the Report Server database
                        rs.SetPolicies(Session["Path"].ToString(), newPolicy);
                        if (dbroundtrip == true)
                        {
                            gvListChildren.DataSource = rs.ListChildren("/", true);
                            gvListChildren.DataBind();
                        }
                    }
                }
                catch
                {
                    if (rs != null)
                        rs.Dispose();
                    throw;
                }
                if (rs != null)
                    rs.Dispose();
            }
            catch
            {
                throw;
            }
        }

        // Binds data to the GridView control.
        private void BindData()
        {
            gvItemPermissions.DataSource = Session["ItemPermissions"];
            gvItemPermissions.DataBind();
        }

        #endregion

        #region "events"

        protected void gvItemPermissions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Sets the edit index.
            gvItemPermissions.EditIndex = e.NewEditIndex;
            //Binds data to the GridView control.
            BindData();
            // Shows the ModalPopupExtender control.
            ModalPopupExtender1.Show();
            btnOk.Enabled = false;
            // Disable the user/group name field.
            for(int i=0;i<gvItemPermissions.Rows.Count;i++)
                gvItemPermissions.Rows[i].Cells[1].Enabled = false;
        }

        protected void gvItemPermissions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemPermissions.PageIndex = e.NewPageIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void gvItemPermissions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            gvItemPermissions.EditIndex = -1;
            //Bind data to the GridView control.
            BindData();
            // Shows the ModalPopupExtender control.
            ModalPopupExtender1.Show();
            btnOk.Enabled = true;
        }

        protected void gvItemPermissions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Retrieve the table from the session object.
            DataTable dt = (DataTable)Session["ItemPermissions"];

            //Update the values.
            GridViewRow row = gvItemPermissions.Rows[e.RowIndex];
            //Update the table with the values from grid view control
            for(int col=1;col<dt.Columns.Count;col++)
            {
                dt.Rows[row.DataItemIndex][col] = ((CheckBox)(row.Cells[col+1].Controls[0])).Checked;              
            }
            //Reset the edit index.
            gvItemPermissions.EditIndex = -1;

            //Bind data to the GridView control.
            BindData();
            // Shows the ModalPopupExtender control.
            ModalPopupExtender1.Show();
            btnOk.Enabled = true;
        }
        
        // Event to save the policies
        protected void btnOk_Click(object sender, EventArgs e)
        {
            SavePolicies();
            BindData();
        }

        protected void btnItemPermissions_Click(object sender, EventArgs e)
        {
            // Return of no rows are selected
            if (gvListChildren.SelectedIndex == -1)
                return;

            // Read policy for the selected item
            Policy[] m_reportPolicy = new Policy[1];
            m_reportPolicy = GetPoliciesForItem(gvListChildren.SelectedRow.Cells[2].Text);
            if (m_reportPolicy == null) {
                ModalPopupExtender2.Show();
                return;
                }

            Session["Path"] = gvListChildren.SelectedRow.Cells[2].Text;

            //Clear table
            DataTable m_CatalogRoles = new DataTable();
            m_CatalogRoles = (DataTable)Session["CatalogRoles"];
            m_CatalogRoles.Rows.Clear();

            foreach (Policy curPolicy in m_reportPolicy)
            {
                DataRow dr = m_CatalogRoles.NewRow();
                dr["GroupName"] = curPolicy.GroupUserName;

                foreach (Role curRole in curPolicy.Roles)
                {
                    dr[curRole.Name] = true;
                }
                m_CatalogRoles.Rows.Add(dr);
            }

            Session["ItemPermissions"] = m_CatalogRoles;

            // Bind data to the grid
            BindData();
            ModalPopupExtender1.Show();

            // clear the labels
            lblInfo.Text = string.Empty;
            lblException.Text = string.Empty;
        }
    
        #endregion
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
