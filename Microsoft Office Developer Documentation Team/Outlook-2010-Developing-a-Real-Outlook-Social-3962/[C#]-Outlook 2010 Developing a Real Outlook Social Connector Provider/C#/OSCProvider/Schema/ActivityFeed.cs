using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;

namespace OSCProvider.Schema
{

    /// <summary>
    /// Represents an activity feed for a set of users. 
    /// </summary>
    public class ActivityFeed:SchemaObject
    {

        private string m_networkName;
        private List<Activity> m_activities = new List<Activity>();
        private List<Template> m_templates = new List<Template>();

        /// <summary>
        /// Network where the activity feed items originated (required)
        /// </summary>
        public string NetworkName { get { return m_networkName; } set { m_networkName = value; } }

        /// <summary>
        /// Container for activity feed items 
        /// </summary>
        public List<Activity> Activities { get { return m_activities; } }

        /// <summary>
        /// Container for feed item display templates 
        /// </summary>
        public List<Template> Templates { get { return m_templates; } }


        public override string Xml
        {
            get
            {
                return XmlEx.OuterXml;
            }
        }

        internal override XmlElement XmlEx
        {
            get {
                if (Activities.Count == 0)
                {
                    throw new OSCException("No updates.", OSCExceptions.OSC_E_NO_CHANGES);
                }

                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement actFeed = xdoc.CreateElement("activityFeed", XmlHelper.GetSchemaUrl(SchemaVersion));

                XmlHelper.AddStringElement(actFeed, "network", NetworkName,true);

                XmlElement xactivities = xdoc.CreateElement("activities", XmlHelper.GetSchemaUrl(SchemaVersion));
                foreach (Activity act in Activities)
                {
                    act.SchemaVersion = SchemaVersion;
                    xactivities.AppendChild(xdoc.ImportNode(act.XmlEx,true));
                }
                actFeed.AppendChild(xactivities);

                XmlElement xtemplates = xdoc.CreateElement("templates", XmlHelper.GetSchemaUrl(SchemaVersion));
                foreach (Template t in Templates)
                {
                    t.SchemaVersion = SchemaVersion;
                    xtemplates.AppendChild(xdoc.ImportNode(t.XmlEx,true));
                }
                actFeed.AppendChild(xtemplates);

                xdoc.AppendChild(actFeed);
                return xdoc.DocumentElement ;
            }
        }



    }
}
