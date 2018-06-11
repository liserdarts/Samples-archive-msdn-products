using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutlookSocialProvider;

namespace OSCProvider_CS
{
    class OSCSession : OutlookSocialProvider.ISocialSession, OutlookSocialProvider.ISocialSession2 
    {

        #region ISocialSession Members

        public string GetActivities(string[] emailAddresses, DateTime startTime)
        {
            //Not supported since OSC 1.1
            return string.Empty;
        }
        public string FindPerson(string userID)
        {
            //To-Do: Implement FindPerson
            return string.Empty;
        }

        public ISocialProfile GetLoggedOnUser()
        {
            return new OSCProfile();
        }

        public string GetLogonUrl()
        {
            //To-Do: Implement GetLogonUrl
            return string.Empty;
        }

        public string GetNetworkIdentifier()
        {
            //To-Do: Implement GetNetworkIdentifier
            return string.Empty;
        }

        public ISocialPerson GetPerson(string userID)
        {
            //To-Do: Implement GetPerson
            return new OSCPerson();
        }

        public void FollowPerson(string emailAddress)
        {
            //To-Do: Implement FollowPerson
        }

        public void UnFollowPerson(string userID)
        {
            //To-Do: Implement UnFollowPerson
        }

        public void Logon(string username, string password)
        {
            //To-do: Implement LogonWeb or Logon depending on supported auth model
        }

        public void LogonWeb(string connectIn, out string connectOut)
        {
            //To-do: Implement LogonWeb or Logon depending on supported auth model
            connectOut = "http:\\www.contoso.com";
        }

        public string LoggedOnUserID
        {
            get
            {
                //To-do: Implement LoggedOnUserID
                return string.Empty;
            }
        }

        public string LoggedOnUserName
        {
            get
            {
                //To-do: Implement LoggedOnUserName
                return string.Empty;
            }
        }

        public string SiteUrl
        {
            set
            {
                //To-Do: Implement SiteUrl
            }
        }
        #endregion

        #region ISocialSession2 members
        public void FollowPersonEx(string[] emailAddresses, string displayName)
        {
            //To-Do: Implement FollowPersonEx
        }

        public string GetActivitiesEx(string[] hashedAddresses, DateTime startTime)
        {
            //To-Do: Implement GetActivitiesEx
            return string.Empty;
        }

        public string GetPeopleDetails(string personsAddresses)
        {
            //To-Do: Implement GetPeopleDetails
            return string.Empty;
        }

        public void LogonCached(string connectIn, string userName, string password, out string connectOut)
        {
            connectOut = string.Empty;
            //To-Do: Implement LogonCached
        }
        #endregion
    }
}
