using System;
using System.Globalization;
using System.Collections.Generic;

//Using statements for the OSC Provider Proxy Library.
using OSCProvider;
using OSCProvider.Schema;

//////Using statements for the social network.
////using OfficeTalkAPI;

namespace OfficeTalkOSCProvider
{
    public partial class OTProvider
    {
        //OSC Proxy Library method that returns information for a group of people.
        //This method is called by the ISocialSession GetPeopleDetails method.
        public override Friends GetPeopleDetails(HashedAddresses hashedAddresses)
        {
            Friends returnValue = new Friends();

            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.

            ////if (hashedAddresses != null)
            ////{
            ////    //Get a client for interacting with OfficeTalk.
            ////    OfficeTalkClient otClient = OfficeTalkHelper.GetOfficeTalkClient();

            ////    //For each person in the HashedAddresses collection.
            ////    foreach (PersonAddresses addresses in hashedAddresses.PersonAddresses)
            ////    {
            ////        //For each email address assigned to the person.
            ////        foreach (string hashedEmail in addresses.HashedAddresses)
            ////        {
            ////            OTUser otUser = 
            ////                otClient.GetUserFromHash(hashedEmail, Format.JSON);

            ////            //If a user was found, add them to the list of people.
            ////            if (otUser != null)
            ////            {
            ////                Person p = OfficeTalkHelper.ConvertUserToPerson(otUser);

            ////                //Set the index value for the person to the same index 
            ////                //    position they had in the hashedAddresses list.
            ////                p.Index = addresses.Index;

            ////                returnValue.People.Add(p);
            ////                break;
            ////            }
            ////        }
            ////    }
            ////}

            return returnValue;
        }

        //OSC Proxy Library method used to return friends of the current user.
        //Called by multiple interface methods that are dependent on friends:
        //  ISocialPerson GetFriendsAndColleages 
        //  ISocialPerson GetFriendsAndColleaguesIDs
        //  ISocialProfile AreFriendsOrColleagues
        //  ISocialProfile GetActivitiesOfFriendsAndColleagues
        public override Friends GetFriends()
        {
            Friends returnValue = new Friends();

            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.
            
            //////Get a client for interacting with OfficeTalk.
            ////OfficeTalkClient otClient = OfficeTalkHelper.GetOfficeTalkClient();

            //////Get the list of users whom the currently logged-on user is following.
            ////List<OTUser> following =
            ////  otClient.GetFollowing(System.Environment.UserName, Format.JSON);

            //////Convert the OfficeTalk Users to OSC People and set 
            //////	  their FriendStatus property.
            ////foreach (OTUser otUser in following)
            ////{
            ////    Person p = OfficeTalkHelper.ConvertUserToPerson(otUser);
            ////    p.FriendStatus = FriendStatus.friend;
            ////    returnValue.People.Add(p);
            ////}

            return returnValue;
        }

        //OSC Proxy Library method used to mark a person as a friend.
        //This method is called by the ISocialSession2 FollowPersonEx method.
        public override void FollowPersonEx(
            string[] emailAddresses, 
            string displayName)
        {
            //Verify that email addresses have been passed in.
            if (emailAddresses == null || emailAddresses.Length == 0)
            {
                return;
            }

            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.

            //////Get a client for interacting with OfficeTalk.
            ////OfficeTalkClient otClient = OfficeTalkHelper.GetOfficeTalkClient();

            //////Try to find a user based on one of the email addresses.
            ////foreach (string emailAddress in emailAddresses)
            ////{
            ////    //Hash the Email address to find the user in OfficeTalk.
            ////    string hashedEmailAddress =
            ////        OSCProvider.Helpers.Hash(emailAddress, HashFunction.SHA1);

            ////    OTUser otUser = otClient.GetUserFromHash(hashedEmailAddress, Format.JSON);

            ////    //If the user was found and is not already being followed, 
            ////    //    call the OfficeTalk API to mark the user as being followed.
            ////    if (otUser != null && !otUser.following)
            ////    {
            ////        otClient.Follow(otUser.alias, Format.JSON);
            ////        return;
            ////    }
            ////}
        }

        //OSC Proxy Library method used to unmark a person as a friend.
        //This method is called by the ISocialSession UnFollowPerson method.
        public override void UnFollowPerson(string userID)
        {
            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.
            
            //////Get a client for interacting with OfficeTalk.
            ////OfficeTalkClient otClient = OfficeTalkHelper.GetOfficeTalkClient();

            //////OfficeTalk does not support finding a user by the userID.
            //////Search for the userID in the list of OfficeTalk users being 
            //////    followed by the currently logged-on user.
            ////List<OTUser> following = 
            ////    otClient.GetFollowing(System.Environment.UserName, Format.JSON);

            //////Find the OfficeTalk User with the userID.
            ////foreach (OTUser otUser in following)
            ////{
            ////    if (otUser.id.ToString() == userID)
            ////    {
            ////        //If the user is found, call the OfficeTalk API to unmark
            ////        //    the user as being followed.
            ////        otClient.UnFollow(otUser.alias, Format.JSON);
            ////        break;
            ////    }
            ////}
        }
    }
}
