using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OutlookSocialProvider;


#region Readme for OSC TestProvider
// Test Provider for the Microsoft Outlook Social Connector (OSC) version 1.1
// Copyright © 2009-2010 Microsoft Corporation
// 
// To use TestProvider:
//      1. Launch Visual Studio 2008 or Visual Studio 2010 using the Run As Adminstrator command.
//      2. Open TestProvider.sln in ..\Projects\TestProvider folder.
//      3. Point to Build, then Build TestProvider
//      4. Open registerProvider.reg to add the correct registry entries for TestProvider.
//      5. registerProvider.reg also turns on DebugProviders and ShowDebugButtons DWORD values
//          so that Debug buttons will appear on the Explorer command bar (Outlook 2003-2007) 
//          or Ribbon Add-ins group (Outlook 2010).
//      6. Select TestProvider properties on the Project menu, and then click the Debug tab.
//      7. Select the Start External Program option, and adjust the path to the version of Outlook
//          running on your machine. Current setting is to run on Outlook 2010:
//          C:\Program Files\Microsoft Office\Office14\OUTLOOK.EXE
//      8. Under the References node in the Solution Explorer, ensure that the reference is correct
//          for OutlookSocialProvider.dll. OutlookSocialProvider.dll is the interop assembly built
//          from socialprovider.dll using tlbimp tool.
//      9. On the Debug menu, point to Start Debugging (F5).
//      10.Outlook will launch. Use the Add command in the people pane to add TestProvider.
//      11.When TestProvider is loaded, breakpoints will be hit in the provider code. 
//          Use the breakpoints to learn about the calling sequence for the provider.
//          You can also remove the breakpoints  
//
// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// 
#endregion


namespace TestProvider
{
    public class TestProvider : ISocialProvider
    {
        private const string networkName = "TestProvider";
        private const string providerVersion = "1.1";
        // Immutable Guid for TestProvider
        private string networkGuid = "{E65F5D4E-1E03-40db-92E3-93455D174FEB}";

        #region ISocialProvider Members

        public string[] DefaultSiteUrls
        {
            get
            {
                return new string[] { "http://www.contoso.com/" };
            }
        }

        public ISocialSession GetAutoConfiguredSession()
        {
            return new TestSession();
        }

        public string GetCapabilities()
        {
            // If you implement capabilities using classes defined by xsd tool,
            // you must observe the correct schema sequence.
            //OSCSchema.capabilities capabilities = new OSCSchema.capabilities();
            // OSC 1.0 capabilities
            //capabilities.getFriends = true;
            //capabilities.cacheFriends = true;
            //capabilities.followPerson = true;
            //capabilities.doNotFollowPerson = false;
            //capabilities.dynamicActivitiesLookup = false;
            //capabilities.dynamicActivitiesLookupSpecified = true;
            //capabilities.getActivities = true;
            //capabilities.cacheActivities = true;
            //capabilities.displayUrl = false;
            //capabilities.useLogonWebAuth = false;
            //capabilities.hideHyperlinks = false;
            //capabilities.hideHyperlinksSpecified = true;
            //capabilities.supportsAutoConfigure = false;
            //capabilities.supportsAutoConfigureSpecified = true;
            //capabilities.contactSyncRestartInterval = 60;
            //capabilities.contactSyncRestartIntervalSpecified = true;

            // OSC 1.1 capabilities
            //capabilities.dynamicActivitiesLookupEx = false;
            //capabilities.dynamicActivitiesLookupExSpecified = true;
            //capabilities.dynamicContactsLookup = false;
            //capabilities.dynamicContactsLookupSpecified = true;
            //capabilities.useLogonCached = false;
            //capabilities.useLogonCachedSpecified = true;
            //capabilities.hideRememberMyPassword = false;
            //capabilities.hideRememberMyPasswordSpecified = true;
            //capabilities.createAccountUrl = "http://contoso.com/createAccount";
            //capabilities.forgotPasswordUrl = "http://contoso.com/forgotPassword";
            //capabilities.showOnDemandActivitiesWhenMinimized = false;
            //capabilities.showOnDemandActivitiesWhenMinimizedSpecified = true;
            //capabilities.showOnDemandContactsWhenMinimized = false;
            //capabilities.showOnDemandContactsWhenMinimizedSpecified = true;
            //capabilities.hashFunction = "MD5";

            //string result = HelperMethods.SerializeObjectToString(capabilities);

            //Return capabilities for TestProvider
            string result = Properties.Resources.capabilities;
            Debug.Write(result);
            return result;
        }

        public ISocialSession GetSession()
        {
            return new TestSession();
        }

        public void GetStatusSettings(out string statusDefault, out int maxStatusLength)
        {
            //Not supported in OSC version 1.0 and version 1.1
            throw new NotImplementedException();
        }

        public void Load(string socialProviderInterfaceVersion, string languageTag)
        {
            Debug.WriteLine("Load called with socialProviderInterfaceVersion=" + socialProviderInterfaceVersion + " and languageTag=" + languageTag);
        }

        public byte[] SocialNetworkIcon
        {
            get
            {
                byte[] icon = HelperMethods.GetProviderPicture();
                return icon;
            }
        }

        public Guid SocialNetworkGuid
        {
            get
            {
                Debug.WriteLine("SocialNetworkGuid: " + networkGuid);
                //return the immutable Guid for TestProvider
                return new Guid(networkGuid);
            }
        }

        public string SocialNetworkName
        {
            get
            {
                Debug.WriteLine("SocialNetworkName: " + networkName);
                return networkName;
            }
        }

        public string Version
        {
            get
            {
                Debug.WriteLine("Provider Version: " + providerVersion);
                return providerVersion;
            }
        }

        #endregion
    }
}
