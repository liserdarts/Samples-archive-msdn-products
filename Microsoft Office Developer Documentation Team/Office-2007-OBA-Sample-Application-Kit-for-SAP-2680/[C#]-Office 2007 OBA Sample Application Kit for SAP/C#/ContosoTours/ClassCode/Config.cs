using System;
using System.IO;
using System.Xml;
using System.Configuration;

namespace Microsoft.SAPSK.ContosoTours
{
    public static class Config
    {
        public const string _dbConnectionName = "ContosoToursDB";

        public const string _dbSeedConnectionName = "SeedDB";
        
        private static string _keyDefault = "keyDefault";

        public static string _keySeedData = "SeedData";

        public static string _keyShowSeedDataForm = "ShowSeedDataForm";

        public static string _keySeedDataLimit = "SeedDataLimit";

        public static string _keyDateLastSeed = "DateLastSeed";

        public static string _keyShowReseedDataForm = "ShowReseedDataForm";

        private static string _keySeedTravelAgency = "SeedTravelAgency";

        private static string _keySeedDataExpiration = "SeedDataExpiration";

        private static string _keySAPProcess = "SAPProcess";

        private static string _keySAPProcessFile = "SAPProcessFile";

        private static string _sapUserName;

        private static string _sapPassword;

        public static string SAPUserName
        {
            get
            {
                return _sapUserName;
            }
            set
            {
                _sapUserName = value;
            }
        }

        public static string SAPPassword
        {
            get
            {
                return _sapPassword;
            }
            set
            {
                _sapPassword = value;
            }
        }

        public static string KeyDefault
        {
            get
            {
                return GetString(_keyDefault);
            }
        }

        public static string TempPPTPath
        {
            get
            {
                return Path.GetTempPath();
            }
        }

        public static bool SeedData
        {
            get
            {
                return Convert.ToBoolean(
                    GetString(_keySeedData).ToLower());
            }
        }

        public static bool ShowSeedDataForm
        {
            get
            {
                return Convert.ToBoolean(
                    GetString(_keyShowSeedDataForm).ToLower());
            }
        }

        public static int SeedDataLimit
        {
            get
            {
                return Convert.ToInt32(GetString(_keySeedDataLimit));
            }
        }

        public static string SeedTravelAgency
        {
            get
            {
                return GetString(_keySeedTravelAgency);
            }
        }

        public static DateTime DateLastSeed
        {
            get
            {
                return Convert.ToDateTime(GetString(_keyDateLastSeed));
            }
        }

        public static long SeedDataExpiration
        {
            get
            {
                return Convert.ToInt64(GetString(_keySeedDataExpiration));
            }
        }

        public static bool ShowReseedDataForm
        {
            get
            {
                return Convert.ToBoolean(GetString(_keyShowReseedDataForm));
            }
        }

        public static string SAPProcess
        {
            get
            {
                return GetString(_keySAPProcess);
            }
        }

        public static string SAPProcessFile
        {
            get
            {
                return GetString(_keySAPProcessFile);
            }
        }

        private static string GetString(string key)
        {
            string retVal = String.Empty;

            if (key.Trim().Length > 0)
            {
                string temp = ConfigurationManager.AppSettings.Get(key);

                if (temp == null)
                {
                    return retVal;
                }

                if (temp.Trim().Length > 0)
                {
                    retVal = temp;
                }
            }

            return retVal;
        }

        public static void UpdateKey(string key, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            if (!IsExists(xmlDoc,key))
            {
                return;
            }

            XmlNode appsettings =
               xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode node in appsettings)
            {
                if (node.Attributes["key"].Value == key)
                {
                    node.Attributes["value"].Value = value;
                    break;
                }
            }

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        private static bool IsExists(XmlDocument xmlDoc, string key)
        {
            XmlNode appsettings =
              xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode node in appsettings)
            {
                if (node.Attributes["key"].Value == key)
                { 
                    return true; 
                }
            }
            return false;
        }
    }
}
