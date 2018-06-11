using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutlookSocialProvider;

namespace OSCProvider_CS
{
    class OSCProfile : OSCPerson, OutlookSocialProvider.ISocialProfile 
    {
        #region ISocialProfile Members

        public bool[] AreFriendsOrColleagues(string[] userIDs)
        {
            //Not supported in OSC version 1.0 and version 1.1
            return null;
        }

        public string GetActivitiesOfFriendsAndColleagues(DateTime startTime)
        {
            //To-do: Implement GetActivitiesOfFriendsAndColleagues
            return string.Empty;
        }

        public void SetStatus(string Status)
        {
            //Not supported in OSC version 1.0 and version 1.1
        }

        #endregion

        #region ISocialPerson members
        public new string GetActivities(DateTime startTime)
        {
            return string.Empty;
        }


        public new string GetDetails()
        {
            //To-do: Implement GetDetails
            return string.Empty;
        }

        public new string GetFriendsAndColleagues()
        {
            //To-do: Implement GetFriendsAndColleagues
            return string.Empty;
        }

        public new string[] GetFriendsAndColleaguesIDs()
        {
            //To-do: Implement GetFriendsAndColleaguesIDs
            return null;
        }

        public new byte[] GetPicture()
        {
            //To-do: Implement GetPicture
            return null;
        }

        public new string GetProfileUrl()
        {
            //To-do: Implement GetDetails
            return string.Empty;
        }

        public new string GetStatus()
        {
            //Not supported in OSC version 1.0 and version 1.1
            throw new NotImplementedException();
        }


        #endregion
    }
}
