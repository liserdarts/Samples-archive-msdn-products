using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OSCProvider.Schema
{
    /// <summary>
    /// Base class of the template variables.
    /// </summary>
    public abstract class TemplateVariable:SchemaObject
    {

        public abstract string VariableType{get;}
        private string m_name;
        private string m_id;
        private string m_namehint;
        private string m_email;
        private string m_profileUrl;
        private string m_text;
        private string m_value;
        private string m_altText;
        private Uri m_href;
        private List<SimpleTemplateVariable> m_listItems = new List<SimpleTemplateVariable>();

        /// <summary>
        /// Specifies the name of the variable. Required.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        //publisher/entity
        protected virtual string ID_ { get { return m_id; } set { m_id = value; } }

        protected virtual string NameHint_ { get { return m_namehint; } set { m_namehint = value; } }

        protected virtual string EmailAddress_ { get { return m_email; } set { m_email = value; } }

        protected virtual string ProfileUrl_ { get { return m_profileUrl; } set { m_profileUrl = value; } }

        protected virtual string Text_ { get { return m_text; } set { m_text = value; } }

        protected virtual string Value_ { get { return m_value; } set { m_value = value; } }

        protected virtual string AltText_ { get { return m_altText; } set { m_altText = value; } }

        protected virtual Uri Href_ { get { return m_href; } set { m_href = value; } }

        protected virtual List<SimpleTemplateVariable> ListItems_ { get { return m_listItems; } set { m_listItems = value; } }

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
                XmlElement templateVar = xdoc.CreateElement("templateVariable", XmlHelper.GetSchemaUrl(SchemaVersion));
                templateVar.SetAttribute("type", VariableType);

                XmlHelper.AddStringElement(templateVar, "name", Name,true);
                XmlHelper.AddStringElement(templateVar, "id", ID_);
                XmlHelper.AddStringElement(templateVar, "nameHint", NameHint_);
                XmlHelper.AddStringElement(templateVar, "emailAddress", EmailAddress_);
                XmlHelper.AddStringElement(templateVar, "profileUrl", ProfileUrl_);
                XmlHelper.AddStringElement(templateVar, "text", Text_);
                XmlHelper.AddStringElement(templateVar, "value", Value_);
                XmlHelper.AddStringElement(templateVar, "altText", AltText_);
                XmlHelper.AddStringElement(templateVar, "href", Href_);

                if (ListItems_.Count > 0)
                {
                    XmlElement xlistItems = xdoc.CreateElement("listItems", XmlHelper.GetSchemaUrl(SchemaVersion));
                    foreach (SimpleTemplateVariable sVar in ListItems_)
                    {
                        sVar.SchemaVersion = SchemaVersion;
                        xlistItems.AppendChild(xdoc.ImportNode(sVar.XmlEx,true));
                    }

                    templateVar.AppendChild(xlistItems);
                }

                xdoc.AppendChild(templateVar);
                return xdoc.DocumentElement ;
            }
        }
    }
    public class EntityVariable : TemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "entityVariable"; } }

        /// <summary>
        /// The Id value is a unique id of the person or entity (required).
        /// </summary>
        public string ID { get { return ID_; } set { ID_ = value; } }

        /// <summary>
        /// The NameHint value is the name to be displayed in the formatted activity
        /// </summary>
        public string NameHint { get { return NameHint_; } set { NameHint_ = value; } }

        /// <summary>
        /// The EmailAddress value is the SMTP address associated with the person being mentioned in the feed  
        /// </summary>
        public string EmailAddress { get { return EmailAddress_; } set { EmailAddress_ = value; } }

        /// <summary>
        /// The ProfileURL value is the URI which points to the user's profile page
        /// </summary>
        public string ProfileURL { get { return ProfileUrl_; } set { ProfileUrl_ = value; } }
        
    }
    public class PublisherVariable : EntityVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "publisherVariable"; } }
    }
    public class LinkVariable : TemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "linkVariable"; } }

        /// <summary>
        /// Used for Link nodes to denote the text the link should display (optional for the Link node, if not specified the URI will be used as the display text)
        /// </summary>
        public string Text { get { return Text_; } set { Text_ = value; } }

        /// <summary>
        /// The URI of the desired link (required)
        /// </summary>
        public Uri Value { get { return new Uri(Value_); } set { Value_ = Uri.EscapeUriString(value.ToString()); } }
    }
    public class PictureVariable : TemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "pictureVariable"; } }

        /// <summary>
        /// Used to denote the URI of the picture location (required)
        /// </summary>
        public Uri Value { get { return new Uri(Value_); } set { Value_ = value.ToString(); } }

        /// <summary>
        /// Used for the Picture node to denote alternate text for the picture and/or a link where the user is taken when they click the picture (optional)
        /// </summary>
        public string AltText { get { return AltText_; } set { AltText_ = value; } }

        /// <summary>
        /// Url for a link where the user is taken when they click the picture (optional)
        /// </summary>
        public Uri Href { get { return Href_; } set { Href_ = value; } }
    }
    public class ListVariable : TemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "listVariable"; } }

        /// <summary>
        /// Used as a container of the SimpleTemplateVariables 
        /// </summary>
        public List<SimpleTemplateVariable> ListItems { get { return ListItems_; } }
    }
    public class TextVariable : TemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "textVariable"; } }

        /// <summary>
        /// Text to display
        /// </summary>
        public string Text { get { return Value_; } set { Value_ = value; } }
    }

}
