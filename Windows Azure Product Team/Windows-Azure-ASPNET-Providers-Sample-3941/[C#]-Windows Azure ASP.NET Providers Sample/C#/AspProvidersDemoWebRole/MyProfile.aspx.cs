// -----------------------------------------------------------------------
// <copyright file="MyProfile.aspx.cs" company="Microsoft">
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
    /// CodeBehind for the page to display user Profile and Role information.
    /// </summary>
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserProfile p = (UserProfile)this.Context.Profile;
            Country.Text = p.Country;
            Gender.Text = p.Gender;
            Age.Text = p.Age.ToString();

            RoleList.DataSource = Roles.GetRolesForUser(User.Identity.Name);
            RoleList.DataBind();
        }
    }
}