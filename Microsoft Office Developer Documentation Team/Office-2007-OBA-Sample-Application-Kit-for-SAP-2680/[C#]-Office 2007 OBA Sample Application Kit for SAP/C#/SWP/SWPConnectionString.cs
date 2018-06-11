using System;
using System.Xml;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public class SWPConnectionString
    {
        private const string _nameAttributeName = "name";

        private const string _providerNameAttributeName = "providerName";

        private const string _connectionStringAttributeName = "connectionString";

        private string _name;

        private string _providerName;

        private string _connectionString;

        private SWPConnectionString(XmlElement baseNode)
        {
            XmlAttribute nameAttribute = baseNode.Attributes[_nameAttributeName];
            XmlAttribute providerNameAttribute = baseNode.Attributes[_providerNameAttributeName];
            XmlAttribute connectionStringAttribute = baseNode.Attributes[_connectionStringAttributeName];

            _name = nameAttribute.Value;
            _connectionString = connectionStringAttribute.Value;
            if (providerNameAttribute == null)
            {
                _providerName = String.Empty;
            }

            else
            {
                _providerName = providerNameAttribute.Value;
            }
        }

        private static SWPConnectionString[] GetInternal(
            string configurationFile,
            string connectionName)
        {
            XmlDocument configurationXml = new XmlDocument();
            XmlNodeList nodes;
            SWPConnectionString[] retVal;
            SWPConnectionString connectionString;
            int count = 0;
            bool findConnectionName = !String.IsNullOrEmpty(connectionName);
            string xpathQueryText = @"configuration/connectionStrings/add";

            if (findConnectionName)
            {
                // add on [@name='<connectionName>']
                xpathQueryText += @"[@" + _nameAttributeName +"='" + connectionName + "']";
            }

            configurationXml.Load(configurationFile);
            nodes = configurationXml.SelectNodes(xpathQueryText);
            if (findConnectionName)
            {
                retVal = new SWPConnectionString[1];
            }

            else
            {
                retVal = new SWPConnectionString[nodes.Count];
            }

            foreach (XmlElement addElement in nodes)
            {
                connectionString = new SWPConnectionString(addElement);
                if (findConnectionName)
                {
                    if (connectionString.Name == connectionName)
                    {
                        retVal[count] = connectionString;

                        return retVal;
                    }
                }

                else
                {
                    retVal[count] = connectionString;
                }

                count++;
            }

            if (findConnectionName)
            {
                // we have a problem b/c they wanted a specific connection and it was not found
                throw new Exception(
                    "Database connection name (" + 
                    connectionName + 
                    ") not found.");
            }

            return retVal;
        }

        public static SWPConnectionString Get(
            string configurationFile,
            string connectionName)
        {
            // we know there is only one so get the array and return element 0
            SWPConnectionString[] connectionStrings = 
                GetInternal(configurationFile, connectionName);

            return connectionStrings[0];
        }

        public static SWPConnectionString[] Get(
            string configurationFile)
        {
            return GetInternal(configurationFile, String.Empty);
        }

        public static string ConfigFilePath
        {
            get
            {
                return (string)AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE");
            }
        } 

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string ProviderName
        {
            get
            {
                return _providerName;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
    }
}
