using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutlookSocialProvider;

#region Readme for OSC Sample Provider 

 //C# Provider Template for the Microsoft Outlook Social Connector (OSC)
 //Copyright © 2009-2010 Microsoft Corporation
 //Use this template to develop a provider for the OSC

 //Instructions:
 //  1. Change the project name and namespace to your project name and namespace identifiers.
 //  2. Modify AssemblyInfo to use the correct assembly information.
 //  2. Implement the interface members marked as To-Do and add additional dependencies/references as required.
 //  3. Build the Project.
 //  4. The provider assembly ProgID must be listed as a key under
 //  HKEY_CURRENT_USER\Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders
 //  5. To distribute the setup project, create a setup project in Visual Studio or the setup tool of your choice.
 //  6. Your setup project should COM register your assembly and also create the ProgID key listed in step 4.

 //THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
 //KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 //IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

#endregion


namespace OSCProvider_CS
{
    public class OSCProvider : OutlookSocialProvider.ISocialProvider
    {
        private const string NETWORK_NAME = "Your Social Network Name";
        private const string VERSION = "1.1";
        //To-do: Use Tools > Create Guid to create an immutable Guid for your provider
        string networkGuidString = "{HHHHHHHH-HHHH-HHHH-HHHH-HHHHHHHHHHHH}";

        #region ISocialProvider Members

        public string GetCapabilities()
        {
            //To-do: Implement GetCapabilities

            //If you implement capabilities using class defined by xsd tool,
            //you should observe the correct schema sequence.
            //capabilities capabilities = new capabilities();
            //capabilities.getFriends = true;
            //capabilities.cacheFriends = true;
            //Additional schema values in sequence
            //return(HelperMethods.SerializeObjectToString(capabilities));

            //Do not return empty string in your production code
            return string.Empty;
        }

        public OutlookSocialProvider.ISocialSession GetAutoConfiguredSession()
        {
            return new OSCSession();
        }

        public ISocialSession GetSession()
        {
            return new OSCSession();
        }

        public void GetStatusSettings(out string statusDefault, out int maxStatusLength)
        {
            //Not supported in OSC version 1.0 and version 1.1
            statusDefault = "statusDefault";
            maxStatusLength = 140;
        }

        public void Load(string socialProviderInterfaceVersion, string languageTag)
        {
            //To-do: Implement Load
        }

        public byte[] SocialNetworkIcon
        {
            get
            {
                //To-do: Implement SocialNetworkIcon
                byte[] icon = HelperMethods.GetProviderJpeg();
                return icon;
            }
        }

        public string SocialNetworkName
        {
            get
            {
                return NETWORK_NAME;
            }
        }

        public Guid SocialNetworkGuid
        {
            get
            {
                //return the immutable Guid for your provider
                //To-do: create a unique Guid for your provider
                return new Guid(networkGuidString);
            }
        }

        public string[] DefaultSiteUrls
        {
            get
            {
                //To-Do: Implement DefaultSiteUrls
                return new string[] { "http://www.contoso.com" };
            }
        }

        public string Version
        {
            get
            {
                return VERSION;
            }
        }

        #endregion

    }
}
