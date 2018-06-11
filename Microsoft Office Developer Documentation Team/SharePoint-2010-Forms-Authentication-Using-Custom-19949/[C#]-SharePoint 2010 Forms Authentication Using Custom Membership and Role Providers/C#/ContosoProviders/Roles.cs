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
using System.Linq;


namespace ContosoProviders
{

    public sealed class Roles : RoleProvider
    {



        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }



        //
        // System.Configuration.Provider.ProviderBase.Initialize method.
        //

        public override void Initialize(string name, NameValueCollection config)
        {

 

            // Initialize the abstract base class.
            base.Initialize(name, config);


         }



        //
        // System.Web.Security.RoleProvider properties.
        //


        private string pApplicationName = "";


        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        //
        // System.Web.Security.RoleProvider methods.
        //

        //
        // RoleProvider.AddUsersToRoles
        //

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {

            throw new NotImplementedException();
        }


        //
        // RoleProvider.CreateRole
        //

        public override void CreateRole(string rolename)
        {
            throw new NotImplementedException();
        }


        //
        // RoleProvider.DeleteRole
        //

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }


        //
        // RoleProvider.GetAllRoles
        //

        public override string[] GetAllRoles()
        {
            return FindAllRoles().ToArray();
        }


        //
        // RoleProvider.GetRolesForUser
        //

        public override string[] GetRolesForUser(string username)
        {
            return FindRolesForUser(username).ToArray();
        }


        //
        // RoleProvider.GetUsersInRole
        //

        public override string[] GetUsersInRole(string rolename)
        {
            List<string> users = new List<string>();
            foreach (string user in GetAllUsers())
            {
                if (FindRolesForUser(user).Where(role => role.Equals(rolename, StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                {
                    users.Add(user);
                }
            }
            return users.ToArray();
        }


        //
        // RoleProvider.IsUserInRole
        //

        public override bool IsUserInRole(string username, string rolename)
        {
            List<string> rolesForUser = FindRolesForUser(username);
            if (rolesForUser.Where(role => role.Equals(rolename, StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                return true;
            else
                return false;
 
        }


        //
        // RoleProvider.RemoveUsersFromRoles
        //

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {

            throw new NotImplementedException();
        }


        //
        // RoleProvider.RoleExists
        //

        public override bool RoleExists(string rolename)
        {

            if (FindAllRoles().Where(role => role.Equals(rolename, StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                return true;
            else
                return false;
        }

        //
        // RoleProvider.FindUsersInRole
        //

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            List<string> allUsersInRole = new List<string>(GetUsersInRole(rolename));
            List<string> results = new List<string>();
            foreach (string name in allUsersInRole)
            {
                if(name.IndexOf(usernameToMatch,  StringComparison.CurrentCultureIgnoreCase) > 0)
                    results.Add(name);
            }
            return results.ToArray();
        }


        private static string[] UserRoleDB = {
                                   "user1:Role1:Role2:Role3",
                                   "user2:Role2:Role4",
                                   "user3:Role3:Role1:Role4",
                                   "user4:Role4:Role1:Role2",
                                   "user5:Role2:Role1",
                                   "user6:Role1:Role4"
                                  };

        private static string[] RoleDB = {
                                      "Role1", "Role2", "Role3", "Role4"
                                  };


        private static List<string> GetAllUsers()
        {
            return Members.GetAllUserNames();
        }

        private static List<string> FindAllRoles()
        {
            List<string> allRoles = new List<string>();

            foreach (string role in RoleDB)
            {
                allRoles.Add(role);
            }
            return allRoles;
        }

        private List<string> FindRolesForUser(string username)
        {
            List<string> rolesForUser = new List<string>();
            foreach (string userRoleInfo in UserRoleDB)
            {
                string[] roles = userRoleInfo.Split(new char[]{':'},  StringSplitOptions.RemoveEmptyEntries);
                if (string.Compare(username, roles[0], true) == 0)
                {
                    for(int i=1; i< roles.Length; i++)
                    {
                        rolesForUser.Add(roles[i]);
                    }
                }
            }
            return rolesForUser;
        }

        
    }
}
