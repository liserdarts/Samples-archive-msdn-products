using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OSCProvider.Schema
{
    public abstract class SimpleTemplateVariable:SchemaObject
    {


        private string m_name;
        private string m_text;
        private Uri m_value;
        private string m_alttext;
        private Uri m_href;

        /// <summary>
        /// Must be overridden by derived class. Gets the type of variable.
        /// </summary>
        public abstract string VariableType { get; }

        /// <summary>
        /// Get or set the name of this variable.
        /// </summary>
        public string Name { get { return m_name; } set { m_name = value; } }

        protected virtual string Text_ { get { return m_text; } set { m_text = value; } }

        protected virtual Uri Value_ { get { return m_value; } set { m_value = value; } }

        protected virtual string AltText_ { get { return m_alttext; } set { m_alttext = value; } }

        protected virtual Uri Href_ { get { return m_href; } set { m_href = value; } }


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
                XmlElement stempVar = xdoc.CreateElement("simpleTemplateVariable", XmlHelper.GetSchemaUrl(SchemaVersion));
                stempVar.SetAttribute("type", VariableType);
                XmlHelper.AddStringElement(stempVar, "name", Name,true);
                XmlHelper.AddStringElement(stempVar, "text", Text_ );
                XmlHelper.AddStringElement(stempVar, "value", Value_);
                XmlHelper.AddStringElement(stempVar, "altText", AltText_);
                XmlHelper.AddStringElement(stempVar, "href", Href_);
                xdoc.AppendChild(stempVar);
                return xdoc.DocumentElement;
            }
        }
    }

    /// <summary>
    /// A simpleLinkVariable is a link variable used in a list variable on the Data item of the Template
    /// </summary>
    public class SimpleLinkVariable : SimpleTemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "linkVariable"; } }

        /// <summary>
        /// Get or set the text value to be displayed.
        /// </summary>
        public string Text { get { return Text_; } set { Text_ = value; } }
        
        /// <summary>
        /// Get or set the href of the hyperlink.
        /// </summary>
        public Uri Href { get { return Href_; } set { Href_ = Value_ = value; } }
    }

    /// <summary>
    /// A simplePictureVariable is a picture variable used in a list variable on the Data item of the Template
    /// </summary>
    public class SimplePictureVariable : SimpleTemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "pictureVariable"; } }
        
        /// <summary>
        /// Specifies the hyperlink to use when the user clicks the picture, if the desired target is not the picture URL specified by the value element. Optional.
        /// </summary>
        public Uri Href { get { return Href_; } set { Href_ = value; } }
        
        /// <summary>
        /// Specifies the URL for the picture. Required. Must be JPEG, BMP, or PNG format.
        /// </summary>
        public Uri Value { get { return Value_; } set { Value_ = value; } }
        
        /// <summary>
        /// Specifies the alternate text to display for accessibility and when the user moves the mouse pointer over the picture. Optional.
        /// </summary>
        public string AltText { get { return AltText_; } set { AltText_ = value; } }
    }

    /// <summary>
    /// A simpleTextVariable is a text variable used in a list variable on the Data item of the Template
    /// </summary>
    public class SimpleTextVariable : SimpleTemplateVariable
    {
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public override string VariableType { get { return "textVariable"; } }
  
        /// <summary>
        /// Get or set the text value to be displayed.
        /// </summary>
        public string Text { get { return Text_; } set { Text_ = value; } }
        

    }

}
