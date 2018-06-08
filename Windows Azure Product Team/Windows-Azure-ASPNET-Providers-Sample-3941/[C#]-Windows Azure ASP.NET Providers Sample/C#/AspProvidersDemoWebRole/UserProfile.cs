// -----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Microsoft">
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
    using System.Web.Profile;
    using System.Web.Security;

    /// <summary>
    /// Class to store application-specific user Profile information.
    /// </summary>
    public class UserProfile : ProfileBase
    {
        [SettingsAllowAnonymous(false)]
        public string Country
        {
            get { return base["Country"] as string; }
            set { base["Country"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public string Gender
        {
            get { return base["Gender"] as string; }
            set { base["Gender"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public int Age
        {
            get { return (int)base["Age"]; }
            set { base["Age"] = value; }
        }

        public static UserProfile GetUserProfile(string userName)
        {
            return Create(userName) as UserProfile;
        }

        public static UserProfile GetUserProfile()
        {
            return Create(Membership.GetUser().UserName) as UserProfile;
        }
    }
}