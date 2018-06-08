// -----------------------------------------------------------------------
// <copyright file="ManageRoles.aspx.cs" company="Microsoft">
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace AspProvidersDemoWebRole
{
    using System;
    using System.Web.Security;

    /// <summary>
    /// CodeBehind for the page to manage the available Roles.
    /// </summary>
    public partial class ManageRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonNewRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBoxNewRole.Text) && !string.IsNullOrEmpty(TextBoxNewRole.Text.Trim()))
                {
                    Roles.CreateRole(TextBoxNewRole.Text);
                    ErrorLabel.Text = string.Empty;
                }
                else
                {
                    ErrorLabel.Text = "Role names must not be null or empty!";
                }
            }
            catch (ArgumentException aex)
            {
                ErrorLabel.Text = "Error in Role name. Error: " + aex.Message;
            }
            catch (InvalidOperationException ex)
            {
                ErrorLabel.Text = "Error accessing the data store. Error: " + ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Databind list of roles in the role manager system to a listbox in the page
            AvailableRoles.DataSource = Roles.GetAllRoles();
            AvailableRoles.DataBind();
        }
    }
}