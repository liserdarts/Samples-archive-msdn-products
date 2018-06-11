using System;
using System.Xml;
namespace OSCProvider.Schema
{
    
    public class Capabilities:SchemaObject
    {
        private int m_VersionSchemaMask = 0x0;
        public Capabilities(ProviderSchemaVersion schemaVersion) {
            SchemaVersion = schemaVersion;
            switch (schemaVersion)
            {
                case(ProviderSchemaVersion.v1_1):
                    m_VersionSchemaMask = CAP_V1_1_MASK;
                    break;
                default:
                    m_VersionSchemaMask = CAP_V1_0_MASK;
                    break;
            }
        }

        /// <summary>
        /// Provider supports GetFriends
        /// </summary>
        public const int CAP_GETFRIENDS = 0x1;

        /// <summary>
        /// Provider supports caching friends
        /// </summary>
        public const int CAP_CACHEFRIENDS = 0x2;

        /// <summary>
        /// Provider supports adding users to the user's following list
        /// </summary>
        public const int CAP_FOLLOWPERSON = 0x4;

        /// <summary>
        /// Provider supports removing users from the user's following list
        /// </summary>
        public const int CAP_DONOTFOLLOWPERSON = 0x8;

        /// <summary>
        /// Provider supports getting activity list
        /// </summary>
        public const int CAP_GETACTIVITIES = 0x10;

        /// <summary>
        /// Provider supports caching activities. The OSC will cache activities for friends.
        /// </summary>
        public const int CAP_CACHEACTIVITIES = 0x20;

        /// <summary>
        /// Provider supports dynamic activity lookup. GetActivities will be called each time the people pane is refreshed.
        /// </summary>
        public const int CAP_DYNAMICACTIVITIESLOOKUP = 0x40;

        /// <summary>
        /// Indicates if the OSC displays network url in Account config
        /// </summary>
        public const int CAP_DISPLAYURL = 0x80;

        /// <summary>
        /// Indicates if the OSC should use Forms Based Authentication (LogonWeb method)
        /// </summary>
        public const int CAP_USELOGONWEBAUTH = 0x100;

        /// <summary>
        /// Indicates if the OSC should hide "Click here to create an account" and
        /// "Forgot your password?" hyperlinks in the account setup dialog
        /// </summary>
        public const int CAP_HIDEHYPERLINKS = 0x200;

        /// <summary>
        /// Indicates if the provider supports an autoconfigured session (no logon required)
        /// </summary>
        public const int CAP_SUPPORTSAUTOCONFIGURE = 0x400;

        private const int CAP_CONTACTRESTARTINTERVAL = 0x800;

        /// <summary>
        ///  Indicates if the network supports dynamic lookup of Activities using hashed SMTP addresses
        /// </summary>
        public const int CAP_DYNAMICACTIVITIESLOOKUPEX = 0x1000;

        /// <summary>
        ///  Indicates if the network supports dynamic lookup of Contacts using hashed SMTP addresses
        /// </summary>
        public const int CAP_DYNAMICCONTACTSLOOKUP = 0x2000;

        /// <summary>
        ///  Indicates if the OSC should call LogonCached on ISocialSession2
        /// </summary>
        public const int CAP_USELOGONCACHED = 0x4000;

        /// <summary>
        ///  Indicates if the OSC should hide the Remember my password check box
        /// </summary>
        public const int CAP_HIDEREMEMBERMYPASSWORD = 0x8000;

         /// <summary>
         /// Url opens in the default browser when user clicks create account in Acct config
         /// </summary>
        private const int CAP_CREATEACCOUNTURL = 0x10000;

        /// <summary>
        ///  Url opens in the default browser when user clicks forgot pwd in Acct config
        /// </summary>
        private const int CAP_FORGOTPASSWORDURL = 0x20000;

        /// <summary>
        ///  Indicates if the OSC should sync on-demand activities when people pane is minimized
        /// </summary>
        public const int CAP_SHOWONDEMANDACTIVITIESWHENMINIMIZED = 0x40000;

        /// <summary>
        ///  Indicates if the OSC should sync on-demand contacts when people pane is minimized
        /// </summary>
        public const int CAP_SHOWONDEMANDCONTACTSWHENMINIMIZED = 0x80000;

        /// <summary>
        ///  Indicates hashing function used to hash e-mail addresses, ignored unless  
        /// dynamicActivitiesLookupEx = true or dyanamicContactsLookup = true in capabilities XML
        /// </summary>
        private const int CAP_HASHFUNCTION = 0x100000;


        private const int CAP_ALL = 0x1FFFFF;
        private const int CAP_PRIVATES = CAP_HASHFUNCTION | CAP_FORGOTPASSWORDURL | CAP_CREATEACCOUNTURL | CAP_CONTACTRESTARTINTERVAL;
        private const int CAP_PUBLICS = (CAP_ALL & ~CAP_PRIVATES);

        private const int CAP_V1_0_MASK = 0x8FF;
        private const int CAP_V1_1_MASK = 0x1FFFFF;

        private int m_capFlags = 0;
        private int? m_contactSyncRestartInterval;

        /// <summary>
        /// Get or set the capability flags
        /// </summary>
        public int CapabilityFlags
        {
            get { return (m_capFlags & CAP_PUBLICS); }
            set { m_capFlags = (value & CAP_PUBLICS & m_VersionSchemaMask);}
        }

        private int CapFlags
        {
            get { return m_capFlags; }
            set { m_capFlags = value & CAP_ALL & m_VersionSchemaMask; }
        }



        /// <summary>
        /// Get or set the ContactRestartInterval (optional)
        /// </summary>
        public int? ContactSyncRestartInterval
        {
            get { return m_contactSyncRestartInterval; }
            set
            {
                m_contactSyncRestartInterval = value;
                if (null == value)
                {
                    m_RemoveCapFlag(CAP_CONTACTRESTARTINTERVAL);
                }
                else
                {
                    m_AddCapFlag(CAP_CONTACTRESTARTINTERVAL);
                }
            }
        }

        private Uri m_createAccountUri;
        public Uri CreateAccountUrl
        {
            get { return m_createAccountUri; }
            set
            {
                m_createAccountUri = value;
                if (value == null)
                {
                    m_RemoveCapFlag(CAP_CREATEACCOUNTURL);
                }
                else
                {
                    m_AddCapFlag(CAP_CREATEACCOUNTURL);
                }
            }
        }

        private Uri m_forgotPWUrl;
        public Uri ForgotPasswordUrl
        {
            get { return m_forgotPWUrl; }
            set
            {
                m_forgotPWUrl = value;
                if (value == null)
                {
                    m_RemoveCapFlag(CAP_FORGOTPASSWORDURL);
                }
                else
                {
                    m_AddCapFlag(CAP_FORGOTPASSWORDURL);
                }
            }
        }

        private HashFunction m_hashFunction;
        public HashFunction? EmailHashFunction
        {
            get { return m_hashFunction; }
            set
            {
                if (value.HasValue)
                {
                    m_hashFunction = value.Value;
                    m_AddCapFlag(CAP_HASHFUNCTION);
                }
                else
                {
                    m_RemoveCapFlag(CAP_HASHFUNCTION);
                }
                
            }
        }



        /// <summary>
        /// Adds the provided flag to the CapabilityFlags
        /// </summary>
        /// <param name="flag">One of the Capabilities.CAP_* capability flags</param>
        /// <returns></returns>
        public int AddCapFlag(int flag)
        {
            return m_AddCapFlag(flag & CAP_PUBLICS );
        }

        private int m_AddCapFlag(int flag)
        {
            CapFlags |= (flag & CAP_ALL & m_VersionSchemaMask);
            return CapabilityFlags;
        }

        /// <summary>
        /// Removes the provided flag from the CapabilityFlags
        /// </summary>
        /// <param name="flag">One of the Capabilities.CAP_* flags</param>
        /// <returns></returns>
        public int RemoveCapFlag(int flag)
        {
            return m_RemoveCapFlag(flag & CAP_PUBLICS & m_VersionSchemaMask);
        }

        private int m_RemoveCapFlag(int flag)
        {
            CapFlags = CapabilityFlags & ~(flag & CAP_ALL & m_VersionSchemaMask);
            return CapabilityFlags;
        }

        public override string Xml
        {
            get
            {
                return XmlEx.OuterXml;
            }
        }

        internal override XmlElement XmlEx
        {
            get
            {
                string[] capabilityNodes = new string[]{
                    "getFriends",
                    "cacheFriends",
                    "followPerson",
                    "doNotFollowPerson",
                    "getActivities",
                    "cacheActivities",
                    "dynamicActivitiesLookup",
                    "displayUrl",
                    "useLogonWebAuth",
                    "hideHyperlinks",
                    "supportsAutoConfigure",
                    "contactSyncRestartInterval",
                    "dynamicActivitiesLookupEx",
                    "dynamicContactsLookup",
                    "useLogonCached",
                    "hideRememberMyPassword",
                    "createAccountUrl",
                    "forgotPasswordUrl",
                    "showOnDemandActivitiesWhenMinimized",
                    "showOnDemandContactsWhenMinimized",
                    "hashFunction"
                };

                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                xdoc.AppendChild(xdoc.CreateElement("capabilities", XmlHelper.GetSchemaUrl(SchemaVersion)));
                for (int i = 0; i < capabilityNodes.Length; i++)
                {
                    int thisFlag = (int)Math.Pow(2 , i);

                    if (thisFlag > m_VersionSchemaMask) continue; //don't include any schema elements from versions prior.

                    bool hasCap = ((CapFlags & thisFlag) > 0);
                    object capValue = null;
                    switch (thisFlag)
                    {
                        case CAP_USELOGONCACHED:
                        //case CAP_USELOGONWEBAUTH:
                            if ((CapFlags & CAP_SUPPORTSAUTOCONFIGURE) > 0) continue;
                            break;
                        //case CAP_SUPPORTSAUTOCONFIGURE:
                        //    if ((CapFlags & (CAP_USELOGONWEBAUTH | CAP_USELOGONCACHED)) > 0) continue;
                        //    break;
                        case CAP_CONTACTRESTARTINTERVAL:
                            if (!hasCap || !ContactSyncRestartInterval.HasValue)
                            {
                                continue;
                            }
                            else
                            {
                                capValue = ContactSyncRestartInterval.Value.ToString();
                            }
                            break;
                        case CAP_CREATEACCOUNTURL:
                            if (!hasCap || CreateAccountUrl == null)
                            {
                                continue;
                            }
                            else
                            {
                                capValue = CreateAccountUrl.ToString();
                            }
                            break;
                        case CAP_FORGOTPASSWORDURL:
                            if (!hasCap || ForgotPasswordUrl == null)
                            {
                                continue;
                            }
                            else
                            {
                                capValue = ForgotPasswordUrl.ToString();
                            }
                            break;
                        case CAP_HASHFUNCTION:
                            if (!hasCap || !EmailHashFunction.HasValue )
                            {
                                continue;
                            }
                            else
                            {
                                capValue = EmailHashFunction.Value.ToString();
                            }
                            break;
                        default:
                            capValue = hasCap;
                            break;
                    }
                    XmlHelper.AddStringElement(xdoc.DocumentElement, capabilityNodes[i], capValue);
               }

                //if(ContactSyncRestartInterval.HasValue)
                //    XmlHelper.AddStringElement(xdoc.DocumentElement, "contactSyncRestartInterval", ContactSyncRestartInterval.Value);

                return xdoc.DocumentElement;

            }
        }


    }

    public enum HashFunction
    {
        SHA1,
        MD5,
        CRC32MD5
    }
}

