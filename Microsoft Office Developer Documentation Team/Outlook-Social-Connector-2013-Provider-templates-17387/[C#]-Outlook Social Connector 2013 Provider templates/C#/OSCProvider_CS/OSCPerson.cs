using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutlookSocialProvider;

namespace OSCProvider_CS
{
    class OSCPerson : OutlookSocialProvider.ISocialPerson 
    {
        #region ISocialPerson Members

        public string GetActivities(DateTime startTime)
        {
            //Not supported since OSC 2013
            return string.Empty;
        }

        public string GetDetails()
        {
            //To-Do: Implement GetActivities
            return string.Empty;
        }

        public string GetFriendsAndColleagues()
        {
            //To-Do: Implement GetFriendAndColleagues
            return string.Empty;
        }

        public string[] GetFriendsAndColleaguesIDs()
        {
            //To-Do: Implement GetFriendAndColleaguesIDs
            string[] result = { "" };
            return result;

        }

        public byte[] GetPicture()
        {
            //To-Do: Implement GetPicture
            return null;
        }

        public string GetProfileUrl()
        {
            //To-Do: Implement GetProfileUrl
            return string.Empty;
        }

        public string GetStatus()
        {
            //Not supported
            return string.Empty;
        }

        #endregion
    }
}
