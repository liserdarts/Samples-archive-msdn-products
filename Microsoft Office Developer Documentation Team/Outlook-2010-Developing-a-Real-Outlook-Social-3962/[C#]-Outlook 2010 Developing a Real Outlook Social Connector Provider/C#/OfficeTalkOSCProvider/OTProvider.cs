using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;

//Using statements for the OSC Provider Proxy Library.
using OSCProvider;
using OSCProvider.Schema;

//////Using statements for the social network.
////using OfficeTalkAPI;

namespace OfficeTalkOSCProvider
{
    //SubClass of the OSC Provider Proxy Library OSCProvider
    //   used to create a custom OSC provider.
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class OTProvider : OSCProvider.OSCProvider
    {
        //Constants for the OfficeTalk OSC provider.
        internal static string NETWORK_NAME = @"OfficeTalk";
        internal static string NETWORK_GUID = @"{EE4109EE-1906-4B24-A7EA-CFC89967FFDC}";
        internal static string API_VERSION = @"1.2";
        internal static string API_URL = @"http://yoururl";
        internal static OSCProvider.ProviderSchemaVersion SCHEMA_VERSION =
            ProviderSchemaVersion.v1_1;

        //Returns a ProviderData object with information about the OSC Provider.
        public override ProviderData GetProviderData()
        {
            //The ProviderData contains information about the social network and is 
            //    used by the the OSC ISocialProvider members to return information.
            ProviderData providerData = new ProviderData();

            //Friendly name of the social network to display in Outlook.
            providerData.NetworkName = NETWORK_NAME;

            //GUID that represents the social network.
            //This GUID should not change between versions.
            providerData.NetworkGuid = new Guid(NETWORK_GUID);

            //Version of the social network provider.
            providerData.Version = API_VERSION;

            //Array of URLs that the social network provider uses.
            //The default URL should be the first item in the array.
            providerData.Urls = new string[] { API_URL };

            //The icon of the social network to display in Outlook.
            Byte[] icon = null;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream imageStream =
                assembly.GetManifestResourceStream("OfficeTalkOSCProvider.OTIcon16.bmp"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Image socialNetworkIcon = Image.FromStream(imageStream))
                    {
                        socialNetworkIcon.Save(memoryStream, ImageFormat.Bmp);
                        icon = memoryStream.ToArray();
                    }
                }
            }
            providerData.Icon = icon;

            //Determine which Schema Version to use for the XML strings returned by the OSC Proxy Library.
            //There's a chance this method will get called before the ISocialProvider Load
            //    method is called.
            //The default value is v1_1.
            if (!string.IsNullOrEmpty(this.SocialProviderInterfaceVersion))
            {
                double version = double.Parse(this.SocialProviderInterfaceVersion, CultureInfo.InvariantCulture);
                //A version of 1.5113 and greater should be 1.1, anything less should be 1.0.
                SCHEMA_VERSION = (version >= 1.5113 ? ProviderSchemaVersion.v1_1 : ProviderSchemaVersion.v1_0);
            }
            providerData.SchemaVersion = SCHEMA_VERSION;

            //Define the capabilities for the provider.
            //The Capabilities object will generate the appropriate XML string.
            Capabilities capabilities = new Capabilities(SCHEMA_VERSION);
            capabilities.CapabilityFlags =
                //OSC should call the GetAutoConfiguredSession method to get a 
                //    configured Session for the user.
                Capabilities.CAP_SUPPORTSAUTOCONFIGURE |

                //OSC should hide all links in the Account configuration dialog box.
                Capabilities.CAP_HIDEHYPERLINKS |
                Capabilities.CAP_HIDEREMEMBERMYPASSWORD |

                //The following activity settings identify that Activities uses
                //    hybrid synchronization.
                //OSC will store activities for friends in a hidden folder and 
                //    activities for non-friends in memory.
                Capabilities.CAP_GETACTIVITIES |
                Capabilities.CAP_DYNAMICACTIVITIESLOOKUP |
                Capabilities.CAP_DYNAMICACTIVITIESLOOKUPEX |
                Capabilities.CAP_CACHEACTIVITIES |

                //The following Friends settings identify that Friend information
                //    uses hybrid synchronization.
                //OSC will call the GetPeopleDetails method every time the People Pane 
                //    is refreshed to ensure the latest user information is displayed.
                Capabilities.CAP_GETFRIENDS |
                Capabilities.CAP_DYNAMICCONTACTSLOOKUP |
                Capabilities.CAP_CACHEFRIENDS |

                //The following Friends settings identify that OfficeTalks supports
                //    the FollowPerson and UnFollowPerson calls.
                Capabilities.CAP_DONOTFOLLOWPERSON |
                Capabilities.CAP_FOLLOWPERSON;

            //Set the Email HashFunction.
            //Setting the EmailHashFunction is required if CAP_DYNAMICCONTACTSLOOKUP
            //    or CAP_DYNAMICACTIVITIESLOOKUPEX are set.
            capabilities.EmailHashFunction = HashFunction.SHA1;

            //Set the capabilities property on the providerData object.
            providerData.ProviderCapabilities = capabilities;

            return providerData;
        }

        //OSC Proxy Library override method used to return information 
        //   for the current user.
        public override Person GetMe()
        {
            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.

            //////Get a reference to the OfficeTalk client.
            ////OfficeTalkClient officeTalkClient =
            ////    OfficeTalkHelper.GetOfficeTalkClient();

            //////Look up the user based on credentials used to log on to Windows.
            ////OTUser user =
            ////    officeTalkClient.GetUser(System.Environment.UserName, Format.JSON);

            //////Convert the OfficeTalk User to an OSC Provider Proxy Person.
            ////Person p = OfficeTalkHelper.ConvertUserToPerson(user);

            //////Set the UserName property.
            //////This is used only by the Person that the GetMe method returns to
            //////    support the OSC ISocialSession.LoggedOnUserName property.
            ////p.UserName = System.Environment.UserName;

            return new Person();
        }

        //OSC Proxy Library method that returns information for a specific person.
        //Called by the ISocialSession FindPerson and GetPerson methods.
        //userID can be a social network user Id, SMTP address, or display name.
        public override Person GetPerson(string userId)
        {
            Person returnValue = null;

            //The OfficeTalkAPI is not publicly available; all code communicating 
            //    with OfficeTalk is disabled.
            //Please review the code below to get an idea of the types of activities
            //    you will need to perform when developing an OSC provider.

            //////Get a client for interacting with OfficeTalk.
            ////OfficeTalkClient otClient = OfficeTalkHelper.GetOfficeTalkClient();

            //////Search for the user in OfficeTalk
            ////List<OTUser> otUsers = otClient.SearchUsers(userId, Format.JSON);

            //////Return the first user if any users were found
            ////if (otUsers.Count > 1)
            ////{
            ////    returnValue = OfficeTalkHelper.ConvertUserToPerson(otUsers[1]);
            ////}
            ////else
            ////{
            ////    //Return an OSC_E_NOTFOUND error to the OSC
            ////    throw new OSCException(string.Format(CultureInfo.InvariantCulture, "Couldn't find user '{0}'", userId), OSCExceptions.OSC_E_NOT_FOUND);
            ////}

            return returnValue;
        }
    }
}
