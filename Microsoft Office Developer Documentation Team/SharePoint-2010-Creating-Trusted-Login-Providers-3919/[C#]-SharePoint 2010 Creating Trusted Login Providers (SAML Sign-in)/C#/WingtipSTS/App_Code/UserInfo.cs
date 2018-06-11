using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Claims;

public class WingtipClaimTypes
{
    // System.IdentityModel.Claims.ClaimTypes.Email;
    public static string EmailAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    public static string Title = "http://schemas.wingtip.com/sharepoint/2009/08/claims/title";
}

public class UserInfo
{
    //Email address is used as the UserID.
    //Every user has two claims: title and email address. SharePoint will pick up the email and treat it as identity.
    private static string[] userDB = 
       {
        "user1@wingtip.com:Title:Engineer", 
        "user1@wingtip.com:Email:user1@wingtip.com",
        "user2@wingtip.com:Title:Manager",
        "user2@wingtip.com:Email:user2@wingtip.com",
        "user3@wingtip.com:Title:CEO",
        "user3@wingtip.com:Email:user3@wingtip.com",
       };

    //Manually construct a list of user. In a real production environment
    //You should look up a directory serivce or database to retrieve the user information.
    public static List<string> GetAllUsers()
    {
        List<string> allUsers = new List<string>();
        //adding forms-based users.
        allUsers.Add("user1@wingtip.com");
        allUsers.Add("user2@wingtip.com");
        allUsers.Add("user3@wingtip.com");
        return allUsers;
    }

    public static bool AuthenticateUser(string username, string password)
    {
        //Your authentication logic goes here.
        return true;
    }

    /// <summary>
    /// A real implementation should look up a directory service or database. 
    /// to retrieve user's claim. The code below is used only for demostration purpose.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public static List<Claim> GetClaimsForUser(string username)
    {
        List<Claim> userClaims = new List<Claim>();
        foreach (string userInfo in userDB)
        {
            string[] claims = userInfo.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            if (username == claims[0])
            {
                userClaims.Add(new Claim(GetClaimTypeForRole(claims[1]), claims[2], Microsoft.IdentityModel.Claims.ClaimValueTypes.String));
            }
        }

        return userClaims;
    }

    public static string GetClaimTypeForRole(string roleName)
    {
        if (roleName.Equals("Title", StringComparison.OrdinalIgnoreCase))
            return WingtipClaimTypes.Title;
        else if (roleName.Equals("Email", StringComparison.OrdinalIgnoreCase))
            return WingtipClaimTypes.EmailAddress;
        else
            throw new Exception("Claim Type not found!");
    }

}
