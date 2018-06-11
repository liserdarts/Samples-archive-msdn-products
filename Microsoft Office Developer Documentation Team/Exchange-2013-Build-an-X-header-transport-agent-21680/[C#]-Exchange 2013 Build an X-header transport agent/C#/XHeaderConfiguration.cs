// ***************************************************************
// <copyright file="XHeaderConfiguration.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// This agent reads configuration information to determine rules.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.XHeader
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;
    using System.Xml.Serialization;

    // Actions that can be taken for a particular X-header.
    public enum Action
    {
        Ignore,
        Remove,
        Reject,
    }

    // Top-level configuration object - an array of XHeaderRule objects.
    [XmlRoot(ElementName = "Configuration")]
    public class XHeaderAgentConfiguration
    {
        private XHeaderRule[] rules;

        // Public accessors for the rules, for XML serialization.
        [XmlArray(ElementName = "Rules")]
        public XHeaderRule[] Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        public static XHeaderRule[] Load()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);
            path = Path.Combine(path, "configuration.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(XHeaderAgentConfiguration));

            XHeaderAgentConfiguration configuration = null;

            // Deserialize the rules.
            try
            {
                Debug.WriteLine("Reading rules from " + path);
                using (Stream stream = File.OpenRead(path))
                {
                    configuration = serializer.Deserialize(stream) as XHeaderAgentConfiguration;
                }
            }
            catch (System.IO.IOException ex)
            {
                // File not found, or cannot be opened.
                Debug.WriteLine(ex.ToString());
            }
            catch (System.Xml.XmlException ex)
            {
                // Malformed XML.
                Debug.WriteLine(ex.ToString());
            }
            catch (System.InvalidOperationException ex)
            {
                // Failed conversion from XML text to enumeration constant.
                Debug.WriteLine(ex.ToString());
            }

            // If the configuration was not loaded, return an empty rule collection.
            if ((null == configuration) || (null == configuration.Rules))
            {
                return new XHeaderRule[0];
            }

            return configuration.Rules;
        }
    }

    // A rule indicates what action should be taken for a particular X-header.
    public class XHeaderRule
    {
        private string name;
        private Action action;

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlAttribute]
        public Action Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}
