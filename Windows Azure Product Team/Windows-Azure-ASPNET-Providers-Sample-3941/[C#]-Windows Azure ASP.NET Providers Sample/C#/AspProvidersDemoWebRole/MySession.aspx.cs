// -----------------------------------------------------------------------
// <copyright file="MySession.aspx.cs" company="Microsoft">
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
    using System.Web;

    /// <summary>
    /// CodeBehind for the page to display and add Session state.
    /// </summary>
    public partial class MySession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonNewSessionItem_Click(object sender, EventArgs e)
        {
            Session[TextBoxNewName.Text] = TextBoxNewValue.Text;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            SessionList.Items.Clear();
            foreach (string key in HttpContext.Current.Session.Keys)
            {
                SessionList.Items.Add(
                    string.Format("Key={0}; Value={1}", key, HttpContext.Current.Session[key]));
            }
        }
    }
}