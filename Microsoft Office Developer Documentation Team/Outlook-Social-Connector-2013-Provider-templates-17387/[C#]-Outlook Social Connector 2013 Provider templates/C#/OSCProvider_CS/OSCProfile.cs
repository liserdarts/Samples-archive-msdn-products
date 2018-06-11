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
            //To-do: return an array indicating persons in userIDs are friends
            return null;
        }

        public string GetActivitiesOfFriendsAndColleagues(DateTime startTime)
        {
            //Not supported since OSC 2013
            return string.Empty;
        }

        public void SetStatus(string Status)
        {
            //Not supported
        }

        #endregion

        #region ISocialPerson members
        public new string GetActivities(DateTime startTime)
        {
            //Not supported since OSC 2013
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
            //Not supported 
            throw new NotImplementedException();
        }


        #endregion
    }
}
