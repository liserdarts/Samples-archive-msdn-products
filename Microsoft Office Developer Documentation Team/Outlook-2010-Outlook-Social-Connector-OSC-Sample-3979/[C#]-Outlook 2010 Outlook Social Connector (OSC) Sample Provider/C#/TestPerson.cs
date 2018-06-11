using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OutlookSocialProvider;

namespace TestProvider
{
    public class TestPerson : ISocialPerson
    {
        protected string name, userid;
        protected TestSession session;

        #region Constructor

        public TestPerson(TestSession session, string userid)
        {
            this.session = session;

            this.name = userid;
            this.userid = userid;
        }

        #endregion

        #region ISocialPerson Members

        public string GetActivities(DateTime startTime)
        {
            //OSCSchema.activityFeedType activities = new OSCSchema.activityFeedType();
            //activities.activities = this.CreateActivities();
            //activities.network = "TestProvider";
            //string result = HelperMethods.SerializeObjectToString(activities);

            string result = Properties.Resources.activityFeed;
            Debug.WriteLine(result);

            return result;
        }


        public string GetDetails()
        {
            //TestProvider only has two friends
            string result = "";
            if (this.userid.Equals("4667647"))
            { result = Properties.Resources.friend1; }
            if (this.userid.Equals("5015012"))
            { result = Properties.Resources.friend2; }
            Debug.WriteLine(result);

            return result;
        }

        public string GetFriendsAndColleagues()
        {
            string result = Properties.Resources.friends;
            return result;
        }

        public string[] GetFriendsAndColleaguesIDs()
        {
            string[] friendsIds = new string[this.session.Friends.Count];

            int i = 0;
            foreach (TestPerson person in this.session.Friends)
            {
                friendsIds[i] = person.UserID;
                i++;
            }

            return friendsIds;
        }

        public byte[] GetPicture()
        {
            byte[] icon = HelperMethods.GetPersonPicture();
            return icon;
        }

        public string GetProfileUrl()
        {
            return "http://www.contoso.com/" + this.UserID;
        }

        public string GetStatus()
        {
            ////Not supported in OSC version 1.0 and version 1.1
            throw new NotImplementedException();
        }

        #endregion

        #region Private members
        private OSCSchema.activityDetailsType[] CreateActivities()
        {
            Random rand = new Random();

            OSCSchema.activityDetailsType[] createdActivities = new OSCSchema.activityDetailsType[3];
            for (int i = 0; i < 3; i++)
            {
                createdActivities[i] = new OSCSchema.activityDetailsType();
                createdActivities[i].applicationID = (ulong)rand.Next(100000, 999999);
                createdActivities[i].publishDate = DateTime.Now.Subtract(TimeSpan.FromDays(i));
                createdActivities[i].templateID = (ulong)rand.Next(100000, 999999);

                createdActivities[i].templateVariables = new OSCSchema.templateVariableType[1];
                createdActivities[i].templateVariables[0] = new OSCSchema.templateVariableType();
                createdActivities[i].templateVariables[0].name = "This is activity " + i;
                createdActivities[i].templateVariables[0].type = OSCSchema.templateTypeRestrictionType.textVariable;
            }

            return createdActivities;
        }

        private OSCSchema.personType GetPersonDetails()
        {
            OSCSchema.personType personDetails = new OSCSchema.personType();

            personDetails.userID = this.userid;
            personDetails.firstName = "firstName for " + this.Name;
            personDetails.lastName = "lastName for " + this.Name;
            //
            return personDetails;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string UserID
        {
            get
            {
                return this.userid;
            }
        }
        #endregion
    }
}
