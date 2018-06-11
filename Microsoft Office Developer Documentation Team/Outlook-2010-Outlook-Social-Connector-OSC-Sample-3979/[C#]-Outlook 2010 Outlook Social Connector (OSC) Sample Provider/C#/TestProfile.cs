using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OutlookSocialProvider;
using System.IO;

namespace TestProvider
{
    public class TestProfile : TestPerson, ISocialProfile
    {
        public TestProfile(TestSession session, string userid)
            : base(session, userid)
        {
            
        }

        #region ISocialProfile Members

        public bool[] AreFriendsOrColleagues(string[] UserIDs)
        {
            //Not supported in OSC version 1.0 and version 1.1
            throw new NotImplementedException();
        }

        public string GetActivitiesOfFriendsAndColleagues(DateTime startTime)
        {
            //List<OSCSchema.activityDetailsType> activities = new List<OSCSchema.activityDetailsType>();
            //foreach (TestPerson person in this.session.Friends)
            //{
            //    activities.AddRange(person.CreateActivities());
            //}

            //OSCSchema.activityFeedType allActivities = new OSCSchema.activityFeedType();
            //allActivities.activities = activities.ToArray();

            //string result = HelperMethods.SerializeObjectToString(allActivities);

            Debug.WriteLine("ISocialProfile::GetActivitiesOfFriendsAndColleagues called startTime = " + startTime.ToString("g"));
            string result = Properties.Resources.activityFeed;
            Debug.WriteLine(result);
            return result;
        }

        public void SetStatus(string Status)
        {
            //Not supported in OSC version 1.0 and version 1.1
            throw new NotImplementedException();
        }

        #endregion

        #region ISocialPerson members
        public new string GetActivities(DateTime startTime)
        {
            //OSCSchema.activityFeedType activities = new OSCSchema.activityFeedType();
            //activities.activities = this.CreateActivities();
            //activities.network = "TestProvider";
            //string result = HelperMethods.SerializeObjectToString(activities);

            string result = Properties.Resources.activityFeed;
            Debug.WriteLine(result);

            return result;
        }


        public new string GetDetails()
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

        public new string GetFriendsAndColleagues()
        {
            string result = Properties.Resources.friends;
            return result;
        }

        public new string[] GetFriendsAndColleaguesIDs()
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

        public new byte[] GetPicture()
        {
            byte[] icon = HelperMethods.GetPersonPicture();
            return icon;
        }

        public new string GetProfileUrl()
        {
            return "http://www.contoso.com/" + this.UserID;
        }

        public new string GetStatus()
        {
            //Not supported in OSC version 1.0 and version 1.1
            throw new NotImplementedException();
        }


        #endregion
    }
}
