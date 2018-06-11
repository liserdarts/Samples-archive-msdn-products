using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//Using statements for the OSC Provider Proxy Library.
using OSCProvider;
using OSCProvider.Schema;

//////Using statements for the social network.
////using OfficeTalkAPI;

namespace OfficeTalkOSCProvider
{
    public partial class OTProvider
    {
        //OSC Proxy Library method used to return activities.
        //Called by multiple interface methods that return ActivityFeeds:
        //  ISocialSession GetActivities
        //  ISocialSession2 GetActivitiesEx
        //  ISocialPerson GetActivities
        //  ISocialProfile GetActivitiesOfFriendsAndColleagues
        public override ActivityFeed GetActivities(string[] emailAddresses, DateTime startTime, bool dynamic, bool hashed)
        {
            //Verify that email addresses have been passed in.
            if (emailAddresses == null || emailAddresses.Length == 0)
            {
                throw new OSCException(@"No emails", OSCExceptions.OSC_E_INVALIDARG);
            }

            //Initialize the Activity Feed that will be returned.
            ActivityFeed activityFeed = new ActivityFeed();
            activityFeed.NetworkName = NETWORK_NAME;

            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.
            
            //////Get a client for interacting with OfficeTalk.
            ////OfficeTalkClient otClient = OfficeTalkHelper.GetOfficeTalkClient();

            //////First create a list of unique OfficeTalk users to get activities 
            //////    for from the email addresses.
            ////List<OTUser> uniquePeople = new List<OTUser>();

            //////Foreach email address call the OfficeTalk API to get the user.
            ////foreach (string emailAddress in emailAddresses)
            ////{
            ////    OTUser otUser = null;

            ////    //Check to see if the OSC passed in email addresses already hashed.
            ////    if (!hashed)
            ////    {
            ////        //Hash the Email address to find the user in OfficeTalk.
            ////        string hashedEmailAddress =
            ////            OSCProvider.Helpers.Hash(emailAddress, HashFunction.SHA1);

            ////        otUser = otClient.GetUserFromHash(hashedEmailAddress, Format.JSON);
            ////    }
            ////    else
            ////    {
            ////        //Email is already hashed, just find the user in OfficeTalk.
            ////        otUser = otClient.GetUserFromHash(emailAddress, Format.JSON);
            ////    }

            ////    //If an OfficeTalk user was found and they are not already in the 
            ////    //    uniquePeople list add them to the list .
            ////    if (otUser != null)
            ////    {
            ////        if (!uniquePeople.Contains(otUser))
            ////        {
            ////            uniquePeople.Add(otUser);
            ////        }
            ////    }

            ////    //If the dynamic parameter was set all emailAddresses will be for 
            ////    //    one person any remaining email addresses can be ignored.
            ////    if (dynamic && uniquePeople.Count > 0)
            ////    {
            ////        break;
            ////    }
            ////}

            //////For each of the OfficeTalk users get the messages from OfficeTalk and 
            //////    add them to the activity feed.
            ////foreach (OTUser otUser in uniquePeople)
            ////{
            ////    //Call OfficeTalk to get all messages for the OfficeTalk User.
            ////    //The GetUserMessages method will return all messages created by the 
            ////    //    user as well as all messages the user has responded to.
            ////    List<OTMessage> messages =
            ////      otClient.GetUserMessages(otUser.alias, Format.JSON);

            ////    if (messages != null)
            ////    {
            ////        foreach (OTMessage message in messages)
            ////        {
            ////            //Skip any messages with a created date less than the startTime.
            ////            if (message.created_atAsDateTime < startTime)
            ////            {
            ////                continue;
            ////            }

            ////            //Check to see if this message was created by the OfficeTalk 
            ////            //    user or is a message the OfficeTalk user replied to.
            ////            if (message.user.id != otUser.id)
            ////            {
            ////                //Add the OfficeTalk reply messages to the activity feed.
            ////                AddReplyMessagesToActivityFeed(activityFeed, otUser, message);
            ////            }
            ////            else
            ////            {
            ////                //Add the OfficeTalk message to the activity feed.
            ////                AddMessageToActivityFeed(activityFeed, otUser, message);
            ////            }
            ////        }
            ////    }
            ////}

            //If there are activities to return return the activity feed.
            //If there are no activities return the OSC_E_NO_CHANGES error expected
            //    by the OSC.
            if (activityFeed.Activities.Count > 0)
            {
                return activityFeed;
            }
            else
            {
                throw new OSCException(@"No activities", OSCExceptions.OSC_E_NO_CHANGES);
            }
        }

        //The OfficeTalkAPI is not publicly available; all code communicating 
        //    with OfficeTalk is disabled.
        //Please review the code below to get an idea of the types of activities
        //    you will need to perform when developing an OSC provider.
        
        //////Checks the message for replies made by the user and adds the 
        //////    reply messages to the activity feed.
        ////private static void AddReplyMessagesToActivityFeed(
        ////  ActivityFeed activityFeed,
        ////  OTUser user,
        ////  OTMessage message)
        ////{
        ////    //The current user did not create this message but replied to it.
        ////    //Identify which of the replies to the message are from the current user.
        ////    List<OTMessage> replies = new List<OTMessage>();
        ////    foreach (OTMessage reply in message.recent_replies)
        ////    {
        ////        if (reply.user.id == user.id)
        ////        {
        ////            replies.Add(reply);
        ////        }
        ////    }

        ////    //Create an Activity Feed item for each reply created by the user.
        ////    foreach (OTMessage reply in replies)
        ////    {
        ////        try
        ////        {
        ////            //Initialize the activity feed item.
        ////            Activity activity = new Activity();
        ////            //ApplicationId is one of the two unique IDs used to match an 
        ////            //    activity feed item with its template.
        ////            activity.ApplicationId = 1;
        ////            activity.ActivityType = ActivityTypes.StatusUpdate;
        ////            activity.ActivityID = reply.id.ToString(CultureInfo.InvariantCulture);
        ////            activity.OwnerID = reply.user.id.ToString(CultureInfo.InvariantCulture);
        ////            activity.PublishDate = reply.created_atAsDateTime;

        ////            //Build the title section based on the OfficeTalk message text.
        ////            string title = System.Net.WebUtility.HtmlDecode(reply.text);
        ////            title = title.Replace(@"{", @"(").Replace(@"}", @")");

        ////            //The OSC Provider Proxy Library AutoReplace helper method will create 
        ////            //    a collection of template variables based on detecting certain types 
        ////            //    of data in the input string: Http urls, @tags, and #tags.
        ////            List<TemplateVariable> titleTemplateVars;
        ////            titleTemplateVars = AutoTemplate.AutoReplace(
        ////                OTProvider.API_URL + @"/home/index/{0}#user",
        ////                OTProvider.API_URL + @"/home/index/~{0}#user",
        ////                ref title,
        ////                SCHEMA_VERSION);

        ////            //Update the title to display who created the message this is a reply 
        ////            //    to prior to the message.
        ////            //Add information about the OfficeTalk user that posted the original 
        ////            //    message to the title Template Variables.
        ////            EntityVariable publisher = new EntityVariable();
        ////            publisher.Name = @"Publisher";
        ////            publisher.ID = message.user.id.ToString(CultureInfo.InvariantCulture);
        ////            publisher.EmailAddress = message.user.email;
        ////            publisher.NameHint = message.user.name;
        ////            publisher.ProfileURL =
        ////                OTProvider.API_URL +
        ////                string.Format(CultureInfo.InvariantCulture,
        ////                              @"/home/index/{0}#user",
        ////                              message.user.alias);

        ////            //Add the publisher to the title Template Variables.
        ////            titleTemplateVars.Add(publisher);

        ////            //Update the title format to include the publisher.
        ////            //Publisher information will be the last item in the title Template Variables.
        ////            int publisherIndex = titleTemplateVars.Count - 1;
        ////            title = @String.Format("Replied to {{{0}}}: ", publisherIndex) + title;

        ////            //Build the data section.
        ////            //The data section will display any pictures included in the OfficeTalk reply 
        ////            //    message followed by a hyperlink to the original OfficeTalk message.

        ////            //Parse any OTPic urls from the OfficeTalk reply message.
        ////            String[] otPictureUrls = GetOTPictureUrls(reply.text);
        ////            if (otPictureUrls != null && otPictureUrls.Length > 0)
        ////            {
        ////                //OSC requires that picture urls be placed in a ListVariable.
        ////                ListVariable pictures = new ListVariable();
        ////                pictures.Name = "Pictures";

        ////                //Create a SimplePictureVariable for each picture and add it to the list.
        ////                foreach (string otPictureUrl in otPictureUrls)
        ////                {
        ////                    SimplePictureVariable picture = new SimplePictureVariable();
        ////                    picture.Name = "Picture";
        ////                    picture.Value = new Uri(FixOTPictureUrl(otPictureUrl));
        ////                    picture.Href = new Uri(otPictureUrl);
        ////                    pictures.ListItems.Add(picture);
        ////                }
        ////                //Add the pictures to the activity Template Variables.
        ////                activity.TemplateVariables.Add(pictures);
        ////            }

        ////            //Build the text to display for the original OfficeTalk message 
        ////            //    hyperlink based on the number of replies.
        ////            //If there are no replies then View Post is displayed.
        ////            long numberOfReplies =
        ////                message.reply_count.HasValue ? message.reply_count.Value : 0;
        ////            string replyWord = numberOfReplies == 1 ? @"Reply" : @"Replies";
        ////            string replyText = numberOfReplies > 0 ?
        ////                string.Format(CultureInfo.CurrentCulture,
        ////                              "{0} {1}",
        ////                              numberOfReplies,
        ////                              replyWord)
        ////                : "View Post";

        ////            //Build the OfficeTalk message hyperlink.
        ////            LinkVariable messageLink = new LinkVariable();
        ////            messageLink.Name = "OriginalMessageLink";
        ////            messageLink.Text = replyText;
        ////            messageLink.Value =
        ////              new Uri(OTProvider.API_URL
        ////                      + @"/Messages/"
        ////                      + message.id.ToString(CultureInfo.InvariantCulture));

        ////            //Add the hyperlink to the data Template Variables.
        ////            List<TemplateVariable> dataTemplateVars = new List<TemplateVariable>();
        ////            dataTemplateVars.Add(messageLink);

        ////            //Build the icon section.
        ////            //The icon displayed is the same as for the social network.
        ////            List<TemplateVariable> iconTemplateVars = new List<TemplateVariable>();
        ////            LinkVariable icon = new LinkVariable();
        ////            icon.Name = "Icon";
        ////            icon.Text = "Status Update";
        ////            icon.Value =
        ////              new Uri(OTProvider.API_URL
        ////                      + @"/_layouts/OfficeTalk/content/images/logo.png");
        ////            iconTemplateVars.Add(icon);

        ////            //Call Proxy Library's CreateTemplate helper method to create the 
        ////            //    Template used to display the activity data.
        ////            Template template = AutoTemplate.CreateTemplate(
        ////              activity,           //Activity the template is being created for.
        ////              title,              //Title format string.
        ////              "{0}",              //Data format string.
        ////              "{0}",              //Icon format string.
        ////              titleTemplateVars,  //Title variables.
        ////              dataTemplateVars,   //Data variables.
        ////              iconTemplateVars,   //Icon variables.
        ////              SCHEMA_VERSION);

        ////            //If pictures are included update the Template's Data format to display 
        ////            //    the pictures prior to the hyperlink for the OfficeTalk message.
        ////            if (otPictureUrls != null && otPictureUrls.Length > 0)
        ////            {
        ////                template.Data = @"{list:Pictures({picture:Picture})}" + template.Data;
        ////            }

        ////            //Only add the activity and template if both contain valid xml.
        ////            if (!String.IsNullOrEmpty(activity.Xml) && !String.IsNullOrEmpty(template.Xml))
        ////            {
        ////                activityFeed.Activities.Add(activity);
        ////                activityFeed.Templates.Add(template);
        ////            }
        ////        }
        ////        catch (ApplicationException)
        ////        {
        ////            //If there are any errors while processing the reply message 
        ////            //    move on to the next message.
        ////            continue;
        ////        }
        ////    }
        ////}


        //The OfficeTalkAPI is not publicly available; all code communicating 
        //    with OfficeTalk is disabled.
        //Please review the code below to get an idea of the types of activities
        //    you will need to perform when developing an OSC provider.

        //////Adds the message to the activity feed.
        ////private static void AddMessageToActivityFeed(
        ////  ActivityFeed activityFeed,
        ////  OTUser user,
        ////  OTMessage message)
        ////{

        ////    try
        ////    {
        ////        //Initialize the activity item.
        ////        Activity activity = new Activity();
        ////        //ApplicationId is one of the two unique IDs used to match an 
        ////        //  activity feed item with its template.
        ////        activity.ApplicationId = 1;
        ////        activity.ActivityType = ActivityTypes.StatusUpdate;
        ////        activity.ActivityID = message.id.ToString(CultureInfo.InvariantCulture);
        ////        activity.OwnerID = user.id.ToString(CultureInfo.InvariantCulture);
        ////        activity.PublishDate = message.created_atAsDateTime;

        ////        //Build the title section based on the OfficeTalk message text.
        ////        string title;
        ////        title = System.Net.WebUtility.HtmlDecode(message.text);
        ////        title = title.Replace(@"{", @"(").Replace(@"}", @")");

        ////        //The OSC Provider Proxy Library AutoReplace helper method will create 
        ////        //  a collection of template variables based on detecting certain types 
        ////        //  of data in the input string: Http urls, @tags, and #tags.
        ////        List<TemplateVariable> titleTemplateVars;
        ////        titleTemplateVars = AutoTemplate.AutoReplace(
        ////                OTProvider.API_URL + @"/home/index/{0}#user",
        ////                OTProvider.API_URL + @"/home/index/~{0}#user",
        ////                ref title,
        ////                SCHEMA_VERSION);

        ////        //Build the data section.
        ////        //The data section will display any pictures included in the OfficeTalk message
        ////        //  followed by a hyperlink to the OfficeTalk message.

        ////        //Parse any picture urls from the OfficeTalk message.
        ////        String[] otPictureUrls = GetOTPictureUrls(message.text);
        ////        if (otPictureUrls != null && otPictureUrls.Length > 0)
        ////        {
        ////            //OSC requires that picture urls be placed in a ListVariable.
        ////            ListVariable pictures = new ListVariable();
        ////            pictures.Name = "Pictures";

        ////            //Create a SimplePictureVariable for each picture and add to the list.
        ////            foreach (string otPictureUrl in otPictureUrls)
        ////            {
        ////                SimplePictureVariable picture = new SimplePictureVariable();
        ////                picture.Name = "Picture";
        ////                picture.Value = new Uri(FixOTPictureUrl(otPictureUrl));
        ////                picture.Href = new Uri(otPictureUrl);
        ////                pictures.ListItems.Add(picture);
        ////            }

        ////            //Add the pictures to the activity Template Variables.
        ////            activity.TemplateVariables.Add(pictures);
        ////        }

        ////        //Build the text to display for the OfficeTalk message hyperlink based on 
        ////        //  the number of replies.
        ////        //If there are no replies then View Post will be displayed.
        ////        long numberOfReplies = message.reply_count.HasValue ? message.reply_count.Value : 0;
        ////        string replyWord = numberOfReplies == 1 ? @"Reply" : @"Replies";
        ////        string replyText = numberOfReplies > 0 ?
        ////             string.Format(CultureInfo.CurrentCulture, "{0} {1}", numberOfReplies, replyWord)
        ////                 : "View Post";

        ////        //Build the OfficeTalk message hyperlink.
        ////        LinkVariable messageLink = new LinkVariable();
        ////        messageLink.Name = "MessageLink";
        ////        messageLink.Text = replyText;
        ////        messageLink.Value = new Uri(OTProvider.API_URL + @"/Messages/" + message.id.ToString(CultureInfo.InvariantCulture));

        ////        //Add the hyperlink to the data Template Variables.
        ////        List<TemplateVariable> dataTemplateVars = new List<TemplateVariable>();
        ////        dataTemplateVars.Add(messageLink);

        ////        //Build the icon section.
        ////        //The icon displayed is the same as for the social network.
        ////        List<TemplateVariable> iconTemplateVars = new List<TemplateVariable>();
        ////        LinkVariable icon = new LinkVariable();
        ////        icon.Name = "Icon";
        ////        icon.Text = "Status Update";
        ////        icon.Value = new Uri(OTProvider.API_URL + @"/_layouts/OfficeTalk/content/images/logo.png");
        ////        iconTemplateVars.Add(icon);

        ////        //Call the Proxy Library's CreateTemplate helper method to create the 
        ////        //    Template used to display the activity data.
        ////        Template template = AutoTemplate.CreateTemplate(
        ////            activity,           //Activity the template is being created for.
        ////            title,              //Title format string.
        ////            "{0}",              //Data format string.
        ////            "{0}",              //Icon format string.
        ////            titleTemplateVars,  //Title variables.
        ////            dataTemplateVars,   //Data variables.
        ////            iconTemplateVars,   //Icon variable.
        ////            SCHEMA_VERSION);

        ////        //If pictures are included update the Template's Data format to display 
        ////        //    the pictures prior to the hyperlink for the OfficeTalk message.
        ////        if (otPictureUrls != null && otPictureUrls.Length > 0)
        ////        {
        ////            template.Data = "{list:Pictures({picture:Picture})}" + template.Data;
        ////        }

        ////        //Verify that the activity and template XML are valid before adding to the 
        ////        //  activity feed.
        ////        if (!String.IsNullOrEmpty(activity.Xml) && !String.IsNullOrEmpty(template.Xml))
        ////        {
        ////            activityFeed.Activities.Add(activity);
        ////            activityFeed.Templates.Add(template);
        ////        }
        ////    }
        ////    catch (ApplicationException)
        ////    {
        ////        //If there are any errors while processing the message 
        ////        //    return without adding to the activity feed.
        ////        return;
        ////    }
        ////}


        //The OfficeTalkAPI is not publicly available; all code communicating 
        //    with OfficeTalk is disabled.
        //Please review the code below to get an idea of the types of activities
        //    you will need to perform when developing an OSC provider.

        //////Returns a list of all picture urls inside an OfficeTalk message.
        ////public static string[] GetOTPictureUrls(string txt)
        ////{
        ////    string[] retVal = null;
        ////    List<string> urls = new List<string>();
        ////    if (txt.Contains(@"http://url/"))
        ////    {
        ////        Regex rx = new Regex(@"http://url/\w*");
        ////        if (rx.IsMatch(txt))
        ////        {
        ////            MatchCollection matches = rx.Matches(txt);
        ////            foreach (Match m in matches)
        ////            {
        ////                urls.Add(m.Value);
        ////            }
        ////        }
        ////    }
        ////    if (urls.Count > 0) retVal = urls.ToArray();
        ////    return retVal;
        ////}


        //The OfficeTalkAPI is not publicly available; all code communicating 
        //    with OfficeTalk is disabled.
        //Please review the code below to get an idea of the types of activities
        //    you will need to perform when developing an OSC provider.

        //////Returns a properly formatted version of the OTPicture url.
        ////public static string FixOTPictureUrl(string url)
        ////{
        ////    string returnValue;

        ////    string properUrl = @"http://url/Home/Small/{0}.jpg";
        ////    string pictureId = url.Substring(@"http://url/".Length);
        ////    pictureId = pictureId.Trim('/');

        ////    returnValue =
        ////        string.Format(CultureInfo.InvariantCulture, properUrl, pictureId);

        ////    return returnValue;
        ////}
    }
}