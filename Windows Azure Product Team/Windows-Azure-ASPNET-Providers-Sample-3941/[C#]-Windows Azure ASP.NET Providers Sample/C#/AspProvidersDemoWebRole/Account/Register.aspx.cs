// -----------------------------------------------------------------------
// <copyright file="Register.aspx.cs" company="Microsoft">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// CodeBehind for the User registration page.
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            // Create an empty Profile for the newly created user
            UserProfile p = UserProfile.GetUserProfile(RegisterUser.UserName);

            // Populate some Profile properties off of the create user wizard
            p.Country = ((DropDownList)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Country")).SelectedValue;
            p.Gender = ((DropDownList)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Gender")).SelectedValue;
            p.Age = int.Parse(((TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Age")).Text);
            
            // Save the profile - must be done since we explicitly created this profile instance
            p.Save();
        }
    }
}
