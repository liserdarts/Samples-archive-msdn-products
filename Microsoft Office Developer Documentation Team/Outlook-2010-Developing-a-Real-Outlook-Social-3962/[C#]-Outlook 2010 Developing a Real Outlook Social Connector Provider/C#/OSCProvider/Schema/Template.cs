using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OSCProvider.Schema
{
    /// <summary>
    /// Represents an instance of an activity template. Instances of this class should be added to the ActivityFeed.Templates collection during the GetActivities method.
    /// </summary>
    public class Template:SchemaObject
    {
        private long m_appID;
        private long m_templateID;
        private ActivityTypes? m_type;
        private string m_title;
        private string m_data;
        private string m_icon;

        /// <summary>
        /// Used to denote an activity feed update template, such as a blog post or profile change (required)
        /// </summary>
        public long ApplicationId { get { return m_appID; } set { m_appID = value; } }

        /// <summary>
        /// Used to denote the template type, such as multiple profile change types (required)
        /// </summary>
        public long TemplateId { get { return m_templateID; } set { m_templateID = value; } }

        /// <summary>
        /// Used for denoting a status, photo or document related activity feed item (optional)
        /// </summary>
        public ActivityTypes? TemplateType { get { return m_type; } set { m_type = value; } }

        /// <summary>
        /// Title used for displaying activity feed item (required)
        /// </summary>
        public string Title { get { return m_title; } set { m_title = value; } }

        /// <summary>
        /// Extra information displayed with activity feed item (optional)
        /// </summary>
        public string Data { get { return m_data; } set { m_data = value; } }

        /// <summary>
        /// Icon used for displaying activity feed item (required)
        /// </summary>
        public Uri IconUrl { set { m_icon = value.ToString(); } }

        /// <summary>
        /// Icon used for displaying activity feed item (required)
        /// </summary>
        public string Icon { get { return m_icon; } set { m_icon = value; } }
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
                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement xtemplate = xdoc.CreateElement("activityTemplateContainer", XmlHelper.GetSchemaUrl(SchemaVersion));

                XmlHelper.AddStringElement(xtemplate, "applicationID", ApplicationId);
                XmlHelper.AddStringElement(xtemplate, "templateID", TemplateId);

                XmlElement xactTemplate = xdoc.CreateElement("activityTemplate", XmlHelper.GetSchemaUrl(SchemaVersion));
                xtemplate.AppendChild(xactTemplate);

                if(TemplateType.HasValue)XmlHelper.AddStringElement(xactTemplate, "type", Activity.ActivityTypeString(TemplateType.Value));
                XmlHelper.AddStringElement(xactTemplate, "title", Title,true);
                XmlHelper.AddStringElement(xactTemplate, "data", Data, true);
                XmlHelper.AddStringElement(xactTemplate, "icon", Icon, true);

                xdoc.AppendChild(xtemplate);
                return xdoc.DocumentElement;
            }
        }


    }
}
