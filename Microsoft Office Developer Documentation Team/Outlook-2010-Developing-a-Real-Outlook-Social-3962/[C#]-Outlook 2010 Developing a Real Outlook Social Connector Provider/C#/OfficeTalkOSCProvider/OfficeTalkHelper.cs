using System;
using System.Collections.Generic;

using System.Globalization;
using System.Security.Permissions;

//Using statements for the OSC Provider Proxy Library.
using OSCProvider;
using OSCProvider.Schema;

//////Using statements for the social network.
////using OfficeTalkAPI;

namespace OfficeTalkOSCProvider
{
    //Contains methods for interfacing with the OfficeTalk API.
    class OfficeTalkHelper
    {
        //The OfficeTalkAPI is not publicly available; all code communicating 
        //    with OfficeTalk is disabled.
        //Please review the code below to get an idea of the types of activities
        //    you will need to perform when developing an OSC provider.

        //////Returns a reference to the OfficeTalk client.
        ////private static OfficeTalkClient officeTalkClient = null;
        ////internal static OfficeTalkClient GetOfficeTalkClient()
        ////{
        ////    if (officeTalkClient == null)
        ////    {
        ////        officeTalkClient =
        ////          new OfficeTalkClient(OTProvider.API_URL);
        ////        OfficeTalkClient.UserAgent =
        ////          @"OfficeTalkOSC/" + OTProvider.API_VERSION;
        ////    }
        ////    return officeTalkClient;
        ////}

        //The OfficeTalkAPI is not publicly available; all code communicating 
        //    with OfficeTalk is disabled.
        //Please review the code below to get an idea of the types of activities
        //    you will need to perform when developing an OSC provider.

        //////Converts an Office Talk User to an OSC Provider Proxy Library Person.
        ////internal static Person ConvertUserToPerson(OfficeTalkAPI.OTUser user)
        ////{
        ////    //Create the OSC Provider Proxy Library Person.
        ////    Person person = new Person();

        ////    //Map the User properties to the Person properties.
        ////    person.FullName = user.name;
        ////    person.Email = user.email;
        ////    person.Company = user.department;
        ////    person.UserID = user.id.ToString(CultureInfo.InvariantCulture);
        ////    person.Title = user.title;
        ////    person.CreationTime = user.created_atAsDateTime;

        ////    //FriendStatus is based on whether the user is being followed 
        ////    //  by the currently logged-on user.
        ////    person.FriendStatus =
        ////        user.following ? FriendStatus.friend : FriendStatus.notfriend;

        ////    //Set the PictureUrl if a profile picture is loaded in OfficeTalk.
        ////    if (user.image_url != null)
        ////    {
        ////        person.PictureUrl = new Uri(OTProvider.API_URL + user.image_url);
        ////    }

        ////    //WebProfilePage is set to the user's home page in OfficeTalk.
        ////    person.WebProfilePage =
        ////        OTProvider.API_URL + @"/Home/index/" + user.alias + "#User";

        ////    return person;
        ////}
    }
}
