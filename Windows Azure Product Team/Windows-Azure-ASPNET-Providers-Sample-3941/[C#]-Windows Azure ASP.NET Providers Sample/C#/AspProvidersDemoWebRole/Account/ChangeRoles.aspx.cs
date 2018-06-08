// -----------------------------------------------------------------------
// <copyright file="ChangeRoles.aspx.cs" company="Microsoft">
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace AspProvidersDemoWebRole.Account
{
    using System;
    using System.Linq;
    using System.Web.Security;

    /// <summary>
    /// CodeBehind for page to change the user's role associations
    /// </summary>
    public partial class ChangeRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Databind list of roles in the role manager system to a listbox in the page
                AvailableRoles.DataSource = Roles.GetAllRoles();
                AvailableRoles.DataBind();
                this.UpdateSelectedRoles();
            }
        }

        protected void ChangeRolesButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Add user to all selected roles from the roles listbox
                for (int i = 0; i < AvailableRoles.Items.Count; i++)
                {
                    if (AvailableRoles.Items[i].Selected == true)
                    {
                        Roles.AddUserToRole(User.Identity.Name, AvailableRoles.Items[i].Value);
                    }
                    else if (Roles.IsUserInRole(User.Identity.Name, AvailableRoles.Items[i].Value))
                    {
                        Roles.RemoveUserFromRole(User.Identity.Name, AvailableRoles.Items[i].Value);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                ErrorLabel.Text = "Error accessing the data store. Error: " + ex.Message;
            }
        }

        protected void RevertRolesButton_Click(object sender, EventArgs e)
        {
            this.UpdateSelectedRoles();
        }

        protected void UpdateSelectedRoles()
        {
            string[] userRoles = Roles.GetRolesForUser(User.Identity.Name);

            for (int i = 0; i < AvailableRoles.Items.Count; i++)
            {
                AvailableRoles.Items[i].Selected = userRoles.Contains(AvailableRoles.Items[i].Value);
            }
        }
    }
}