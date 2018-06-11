using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;
using Microsoft.IdentityModel.Claims;
using System.Collections;


namespace ContosoClaimProviders
{
    public class CRMUserInfo
    {
        /// <summary>
        /// A real implementation should look up a directory service or databasee 
        /// to retrieve user's claim. The code below is used only for demostration purpose.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static List<Claim> GetClaimsForUser(string username)
        {
             List<Claim> userClaims = new List<Claim>();
            foreach(string userInfo in userDB)
            {
                string[] claims = userInfo.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (username == claims[0])
                {
                    userClaims.Add(new Claim(GetClaimTypeForRole(claims[1]), claims[2], Microsoft.IdentityModel.Claims.ClaimValueTypes.String));
                }
            }

            return userClaims;
        }


        //Manually construct a list of user. In a real production environment
        //you should look up a directory service or database to retrieve the user information.
        public static List<string> GetAllUsers()
        {
            List<string> allUsers = new List<string>();
            //Add forms users.
            allUsers.Add("bob");
            allUsers.Add("mary");
            allUsers.Add("jack");
            allUsers.Add("admin1");
            //Add Windows domain user if you want this claims provider to augment those claims.
            allUsers.Add("contoso\\andy");
            return allUsers;
        }

        /// <summary>
        /// This function returns all the known claims from the CRM System so that
        /// the claims provider is able to search and resolve the claims in the People Picker.
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetAllClaims()
        {
            Hashtable knownClaims = new Hashtable();
            foreach(string claimItem in claimsDB)
            {
                string[] claim = claimItem.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                knownClaims.Add(claim[1].ToLower(), claim[0].ToLower());
            }
            return knownClaims;
        }


        public static string GetClaimTypeForRole(string roleName)
        {
            if (roleName.Equals("CRMRole", StringComparison.OrdinalIgnoreCase))
                return CRMClaimType.Role;
            else if (roleName.Equals("CRMRegion", StringComparison.OrdinalIgnoreCase))
                return CRMClaimType.Region;
            else
                throw new Exception("CRM Claim Type not found!");
        }

        private static string[] userDB = 
           {
            "bob:CRMRole:Reader", 
            "bob:CRMRole:SalesRepresentative",
            "bob:CRMRegion:NorthWest",
            "mary:CRMRole:Reader",
            "mary:CRMRole:SalesManager",
            "mary:CRMRegion:East",
            "jack:CRMRole:Reader",
            "jack:CRMRole:Executive",
            "jack:CRMRegion:East",
            "admin1:CRMRole:Administrator",
            "contoso\\andy:CRMRole:SalesManager"
           };

        private static string[] claimsDB = 
           {"CRMRole:Reader", 
            "CRMRole:SalesRepresentative",
            "CRMRole:SalesManager",
            "CRMRole:Executive",
            "CRMRole:Administrator",
            "CRMRegion:NorthWest",
            "CRMRegion:East",
            };
    }


}
