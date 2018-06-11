using System;
using System.IO;
using System.Configuration;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public static class Config
    {
        public const string _dbConnectionName = "ContosoToursDB";

        private static string _sapUserName = String.Empty;

        private static string _sapPassword = String.Empty;

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

        public static string PPTTemplate
        {
            get
            {
                return Path.GetTempPath();
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
    }
}
