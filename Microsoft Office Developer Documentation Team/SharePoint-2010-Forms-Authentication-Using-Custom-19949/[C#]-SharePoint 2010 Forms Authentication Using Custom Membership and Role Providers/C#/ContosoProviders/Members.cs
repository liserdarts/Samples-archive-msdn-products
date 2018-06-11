using System.Web.Security;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Linq;


namespace ContosoProviders
{

    public sealed class Members : MembershipProvider
    {
      //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public override void Initialize(string name, NameValueCollection config)
        {
            //
            // Initialize values from web.config.
            //


            // Initialize the abstract base class.
            base.Initialize(name, config);


        }


  

        //
        // System.Web.Security.MembershipProvider properties.
        //


        private string pApplicationName = "";
        private bool pEnablePasswordReset = false;
        private bool pEnablePasswordRetrieval = false;
        private bool pRequiresQuestionAndAnswer = false;
        private bool pRequiresUniqueEmail = false;
        private int pMaxInvalidPasswordAttempts = 0;
        private int pPasswordAttemptWindow = 0;
        private MembershipPasswordFormat pPasswordFormat = MembershipPasswordFormat.Clear;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return pEnablePasswordReset; }
        }


        public override bool EnablePasswordRetrieval
        {
            get { return pEnablePasswordRetrieval; }
        }


        public override bool RequiresQuestionAndAnswer
        {
            get { return pRequiresQuestionAndAnswer; }
        }


        public override bool RequiresUniqueEmail
        {
            get { return pRequiresUniqueEmail; }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return pMaxInvalidPasswordAttempts; }
        }


        public override int PasswordAttemptWindow
        {
            get { return pPasswordAttemptWindow; }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get { return pPasswordFormat; }
        }

        private int pMinRequiredNonAlphanumericCharacters = 0;

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return pMinRequiredNonAlphanumericCharacters; }
        }

        private int pMinRequiredPasswordLength = 0;

        public override int MinRequiredPasswordLength
        {
            get { return pMinRequiredPasswordLength; }
        }

        private string pPasswordStrengthRegularExpression = "";

        public override string PasswordStrengthRegularExpression
        {
            get { return pPasswordStrengthRegularExpression; }
        }

        //
        // System.Web.Security.MembershipProvider methods.
        //

        //
        // MembershipProvider.ChangePassword
        //

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            throw new NotImplementedException();
        }



        //
        // MembershipProvider.ChangePasswordQuestionAndAnswer
        //

        public override bool ChangePasswordQuestionAndAnswer(string username,
                      string password,
                      string newPwdQuestion,
                      string newPwdAnswer)
        {
            throw new NotImplementedException();

        }



        //
        // MembershipProvider.CreateUser
        //

        public override MembershipUser CreateUser(string username,
                 string password,
                 string email,
                 string passwordQuestion,
                 string passwordAnswer,
                 bool isApproved,
                 object providerUserKey,
                 out MembershipCreateStatus status)
        {
            throw new NotImplementedException();

        }



        //
        // MembershipProvider.DeleteUser
        //

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }



        //
        // MembershipProvider.GetAllUsers
        //

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection users = new MembershipUserCollection();


            List<string> allnames = GetAllUserNames();

            foreach (string name in allnames)
            {
                MembershipUser user = new MembershipUser(this.Name,
                                name,
                                name,
                                name + "@contoso.com",
                                "",
                                "",
                                true,
                                false,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today);
                users.Add(user);
            }
            totalRecords = users.Count;
            return users;
        }


        //
        // MembershipProvider.GetNumberOfUsersOnline
        //

        public override int GetNumberOfUsersOnline()
        {


            throw new NotImplementedException();
        }



        //
        // MembershipProvider.GetPassword
        //

        public override string GetPassword(string username, string answer)
        {

            throw new NotImplementedException();
        }



        //
        // MembershipProvider.GetUser(string, bool)
        //

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {

            MembershipUser u = null;
            List<string> allnames = GetAllUserNames();
            var query = allnames.Where(name => name.Equals(username, StringComparison.CurrentCultureIgnoreCase)).Select(name => name);
            if (query.Count() == 1)
            {
                var user = query.First();
                u = new MembershipUser(this.Name,
                                user,
                                user,
                                user + "@contoso.com",
                                "",
                                "",
                                true,
                                false,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today);
                return u;
            }
            else
                return null;                
        }


        //
        // MembershipProvider.GetUser(object, bool)
        //

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {

            MembershipUser u = null;
            List<string> allnames = GetAllUserNames();
            var query = allnames.Where(name => name.Equals(providerUserKey.ToString(), StringComparison.CurrentCultureIgnoreCase)).Select(name => name);

            if (query.Count() == 1)
            {
                var user = query.First();
                u = new MembershipUser(this.Name,
                                user,
                                user,
                                user + "@contoso.com",
                                "",
                                "",
                                true,
                                false,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today);
                return u;
            }
            else
                return null;
        }


        //
        // MembershipProvider.UnlockUser
        //

        public override bool UnlockUser(string username)
        {

            throw new NotImplementedException();
        }


        //
        // MembershipProvider.GetUserNameByEmail
        //

        public override string GetUserNameByEmail(string email)
        {
            MembershipUserCollection users = new MembershipUserCollection();
            List<string> allemails = GetAllUserEmails();
            var query = allemails.Where(useremail => useremail.Equals(email, StringComparison.CurrentCultureIgnoreCase)).Select(useremail => useremail).First();


            return email.Substring(0, email.IndexOf('@'));

        }




        //
        // MembershipProvider.ResetPassword
        //

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();

        }


        //
        // MembershipProvider.UpdateUser
        //

        public override void UpdateUser(MembershipUser user)
        {

            throw new NotImplementedException();
        }


        //
        // MembershipProvider.ValidateUser
        //

        public override bool ValidateUser(string username, string password)
        {
            //Implement your username and password validation logic here.
            //In this demo, we simply verify the user's existence.
            if (GetAllUserNames().Where(user => user.Equals(username, StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                return true;
            else
                return false;
 
        }


        //
        // UpdateFailureCount
        // A helper method that performs the checks and updates associated with
        // password failure tracking.
        //

        private void UpdateFailureCount(string username, string failureType)
        {
            throw new NotImplementedException();
        }


        //
        // CheckPassword
        // Compares password values based on the MembershipPasswordFormat.
        //

        private bool CheckPassword(string password, string dbpassword)
        {

            throw new NotImplementedException();
        }


        //
        // EncodePassword
        // Encrypts, hashes, or leaves the password clear based on the PasswordFormat.
        //

        private string EncodePassword(string password)
        {
            throw new NotImplementedException();
        }


        //
        // UnEncodePassword
        // Decrypts or leaves the password clear based on the PasswordFormat.
        //

        private string UnEncodePassword(string encodedPassword)
        {

            throw new NotImplementedException();
        }



        //
        // MembershipProvider.FindUsersByName
        //

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection users = new MembershipUserCollection();
            List<string> allnames = GetAllUserNames();
            var query = allnames.Where(name => name.IndexOf(usernameToMatch, StringComparison.CurrentCultureIgnoreCase) >=0).Select(name => name);

            foreach (var name in query)
            {
                MembershipUser user = new MembershipUser(this.Name,
                                name,
                                name,
                                name + "@contoso.com",
                                "",
                                "",
                                true,
                                false,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today);
                users.Add(user);
            }

            totalRecords = query.Count();
            return users;
        }

        //
        // MembershipProvider.FindUsersByEmail
        //

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            if (!emailToMatch.Contains("@"))
            {
                totalRecords = 0;
                return null;
            }

            MembershipUserCollection users = new MembershipUserCollection();
            List<string> allemails = GetAllUserEmails();
            var query = allemails.Where(email => email.IndexOf(emailToMatch, StringComparison.CurrentCultureIgnoreCase) >= 0).Select(email => email);

            foreach (var email in query)
            {
                MembershipUser user = new MembershipUser(this.Name,
                                email.Substring(0, email.IndexOf('@') -1 ),
                                email.Substring(0, email.IndexOf('@') -1 ),
                                email,
                                "",
                                "",
                                true,
                                false,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today,
                                DateTime.Today);
                users.Add(user);
            }

            totalRecords = query.Count();
            return users;
        }


        private static string[] UserDB = {
                                   "user1:user1@contoso.com",
                                   "user2:user2@contoso.com",
                                   "user3:user3@contoso.com",
                                   "user4:user4@contoso.com",
                                   "user5:user5@contoso.com",
                                   "user6:user6@contoso.com"
                                  };

        public static List<string> GetAllUserNames()
        {
            List<string> usernames = new List<string>();
            foreach (string strUserInfo in UserDB)
            {
                string[] userinfo = strUserInfo.Split(new char[]{':'}, StringSplitOptions.RemoveEmptyEntries);
                usernames.Add(userinfo[0]);
            }
            return usernames;
        }


        public static List<string> GetAllUserEmails()
        {
            List<string> useremails = new List<string>();
            foreach (string strUserInfo in UserDB)
            {
                string[] userinfo = strUserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                useremails.Add(userinfo[1]);
            }
            return useremails;
        }

    }
}
