using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OSCProvider.Schema
{
    /// <summary>
    /// Represents an activity such as a status update for a user. Instances of this class should be added to the ActivityFeed.Activities collection
    /// </summary>
    public class Activity:SchemaObject 
    {

        private string m_activityID;
        private Person m_owner;
        private DateTime m_publishDate;
        private List<TemplateVariable> m_templateVariables = new List<TemplateVariable>();
        private long m_applicationID;
        private long m_templateID;
        private string m_ownerID;
        private ActivityTypes? m_activityType;

        /// <summary>
        /// Used to denote a unique string idenfitifying this activity. Used for duplicate detection (required)
        /// </summary>
        public string ActivityID
        {
            get
            {
                return m_activityID;
            }
            set
            {
                m_activityID = value;
            }
        }
        
        /// <summary>
        /// Used to denote the user id of the user generating this activity (required)
        /// </summary>
        public string OwnerID
        {
            get
            {
                return m_ownerID;
            }
            set
            {
                m_ownerID = value;
            }
        }

        /// <summary>
        /// Date on which this feed item was published (required)
        /// </summary>
        public DateTime PublishDate
        {
            get
            {
                return m_publishDate;
            }
            set
            {
                m_publishDate = value;
            }
        }

        /// <summary>
        /// Variables included with the feed item (required) 
        /// </summary>
        public List<TemplateVariable> TemplateVariables
        {
            get
            {
                return m_templateVariables;
                
            }
            
        }

        /// <summary>
        /// Used to denote an activity feed update template, such as a blog post or profile change (required)
        /// </summary>
        public long ApplicationId
        {
            get
            {
                return m_applicationID;
                
            }
            set
            {
                m_applicationID = value;
            }
        }

        /// <summary>
        /// Used to denote the template type, such as multiple profile change types (required)
        /// </summary>
        public long TemplateId
        {
            get
            {
                return m_templateID;
            }
            set
            {
                m_templateID = value;
            }
        }

        /// <summary>
        /// Used to denote the user generating this activity (Owner or OwnerID property are required)
        /// </summary>
        public Person Owner
        {
            get
            {
                return m_owner;
                
            }
            set
            {
                m_owner = value;
                if (string.IsNullOrEmpty(m_ownerID) && value != null && !string.IsNullOrEmpty(value.UserID))
                {
                    m_ownerID = value.UserID;
                }
            }
        }

        /// <summary>
        /// Used for denoting a status, photo or document related activity feed item (optional)
        /// </summary>
        public ActivityTypes? ActivityType
        {
            get
            {
                return m_activityType;
            }
            set
            {
                m_activityType = value;
            }
        }


        internal static string ActivityTypeString(ActivityTypes actType)
        {

            if (actType != ActivityTypes.Unspecified)
            {
                if (actType == ActivityTypes.StatusUpdate)
                {
                    return "Status Update";
                }
                else
                {
                    return actType.ToString();
                }
            }
            return null;

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
                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement activity = xdoc.CreateElement("activityDetails", XmlHelper.GetSchemaUrl(SchemaVersion));

                XmlHelper.AddStringElement(activity, "ownerID", OwnerID,true);
                XmlHelper.AddStringElement(activity, "objectID", ActivityID,true);
                XmlHelper.AddStringElement(activity, "applicationID", ApplicationId,true);
                XmlHelper.AddStringElement(activity, "templateID", TemplateId,true);
                XmlHelper.AddStringElement(activity, "publishDate", PublishDate,true);
                if(ActivityType.HasValue) XmlHelper.AddStringElement(activity, "type", ActivityTypeString(ActivityType.Value));
                if (TemplateVariables.Count > 0)
                {
                    XmlElement templateVars = xdoc.CreateElement("templateVariables", XmlHelper.GetSchemaUrl(SchemaVersion));

                    foreach (TemplateVariable tv in TemplateVariables)
                    {
                        tv.SchemaVersion = SchemaVersion;
                        templateVars.AppendChild(xdoc.ImportNode(tv.XmlEx,true));
                    }

                    activity.AppendChild(templateVars);
                }
                else
                {
                    throw new ApplicationException("At least one template variable must be set.");
                }
                

                xdoc.AppendChild(activity);
                return xdoc.DocumentElement;
            }
        }

    }

    /// <summary>
    /// Activity types (Only status, photo and document updates are specially recognized )
    /// </summary>
    public enum ActivityTypes
    {
        Unspecified,
        StatusUpdate,
        Photo,
        Document,
        Other
    }
}
