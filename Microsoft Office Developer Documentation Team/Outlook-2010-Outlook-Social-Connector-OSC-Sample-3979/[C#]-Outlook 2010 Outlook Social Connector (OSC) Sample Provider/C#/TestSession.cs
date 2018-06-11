using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OutlookSocialProvider;
using System.IO;

namespace TestProvider
{
    public class TestSession : ISocialSession, ISocialSession2
    {
        private TestProfile loggedOnUser;
        private string username, password;
        private string siteUrl = "http://www.contoso.com/";
        private string networkIdentifier = "TestProvider";
        private bool loggedIn = false;
   
        private Dictionary<string, TestPerson> people = new Dictionary<string, TestPerson>();

        #region ISocialSession Members

        public string FindPerson(string userID)
        {
            TestPerson person = (TestPerson)this.GetPerson(userID);
            return people[userID].GetDetails();
        }

        public void FollowPerson(string emailAddress)
        {
            Debug.WriteLine("FollowPerson called for " + emailAddress);
        }

        // This method is never called in OSC 1.1 and above
        public string GetActivities(string[] emailAddresses, DateTime startTime)
        {

            //In this example, we enumerate the emailAddresses
            Debug.WriteLine("ISocialSession::GetActivities called for emailAddresses:");
            for (int i = 0; i < emailAddresses.GetLength(0); i++)
            {
                Debug.WriteLine(emailAddresses.GetValue(i));
            }

            //In this example, we create a dummy set of activities that are static
            //In your provider code, lookup activities based on emailAddresses
            //string result = Properties.Resources.ActivityFeed;
            string result = Properties.Resources.activityFeed;

            Debug.WriteLine(result);
            return result;
        }

        public ISocialProfile GetLoggedOnUser()
        {
            return this.loggedOnUser;//
        }

        public string GetLogonUrl()
        {
            return siteUrl;
        }

        public string GetNetworkIdentifier()
        {
            return networkIdentifier;
        }

        public ISocialPerson GetPerson(string userID)
        {
            if (!people.ContainsKey(userID))
            {
                people.Add(userID, new TestPerson(this, userID));
            }

            return people[userID];
        }


        public void Logon(string username, string password)
        {
            Debug.WriteLine("Logon called with username: " + username + ", password: " + password);
            this.username = username;
            this.password = password;

            this.loggedOnUser = new TestProfile(this, this.username);
        }

        public void LogonWeb(string connectIn, out string connectOut)
        {
            if (!string.IsNullOrEmpty(connectIn))
            {
                Debug.WriteLine("LogonWeb called with connectIn: " + connectIn);
            }

            if (!loggedIn)
            {
                loggedIn = true;
                throw new COMException("Authorization failed.", Convert.ToInt32(HelperMethods.OSC_E_AUTH_ERROR));
            }
            this.loggedOnUser = new TestProfile(this, this.username);
            connectOut = "http://www.contoso.com/authorize";
        }

        public string LoggedOnUserID
        {
            get
            {
                return this.loggedOnUser.UserID;
            }
        }

        public string LoggedOnUserName
        {
            get
            {
                //return this.loggedOnUser.Name;
                return "randyb";
            }
        }

        public string SiteUrl
        {
            set
            {
                this.siteUrl = value;
            }
        }

        public void UnFollowPerson(string userID)
        {
            Debug.WriteLine("UnFollowPerson called for " + userID);
        }

        #endregion

        #region ISocialSession2 members
        public void FollowPersonEx(string[] emailAddresses, string displayName)
        {
            Debug.WriteLine("ISocialSession2::FollowPersonEx called for displayName = " + displayName);
            //In this example, enumerate the emailAddresses array
            Debug.WriteLine("ISocialSession2::FollowPersonEx called for emailAddresses:");
            for (int i = 0; i < emailAddresses.GetLength(0); i++)
            {
                Debug.WriteLine(emailAddresses.GetValue(i));
            }
        }

        public string GetActivitiesEx(string[] hashedAddresses, DateTime startTime)
        {
            Debug.WriteLine("ISocialSession2::GetActivitiesEx called for hashedAddresses:");
            for (int i = 0; i < hashedAddresses.GetLength(0); i++)
            {
                Debug.WriteLine(hashedAddresses.GetValue(i));
            }
            return null;
        }

        public string GetPeopleDetails(string personsAddresses)
        {
            Debug.WriteLine("ISocialSession2::GetPeopleDetails called for personsAddresses: " + personsAddresses);
            //
            return Properties.Resources.friends;
        }

        public void LogonCached(string connectIn, string userName, string password, out string connectOut)
        {
            if (!string.IsNullOrEmpty(connectIn))
            {
                Debug.WriteLine("LogonCached called with connectIn: " + connectIn);
            }
            else
            {
                Debug.WriteLine("LogonCached called with empty connectIn.");
                this.username = userName;
                this.password = password;
            }

            if (!loggedIn)
            {
                loggedIn = true;
                throw new COMException("Authorization failed.", Convert.ToInt32(HelperMethods.OSC_E_AUTH_ERROR));
            }
            connectOut = "http://www.contoso.com/authorize";
        }
        #endregion

        public List<TestPerson> Friends
        {
            get
            {
                if (this.people.Count == 0)
                {
                    this.people.Add("My First Friend", new TestPerson(this, "My First Friend"));
                }
                return this.people.Values.ToList();
            }
        }
    }
}
