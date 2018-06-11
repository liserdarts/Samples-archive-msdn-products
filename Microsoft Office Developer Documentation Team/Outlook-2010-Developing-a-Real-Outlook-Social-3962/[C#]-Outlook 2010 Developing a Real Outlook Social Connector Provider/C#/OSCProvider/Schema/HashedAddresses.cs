using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OSCProvider.Schema
{

    /*
    <hashedAddresses>
        <personAddresses index='0'>
            <hashedAddress>7dfe7827bbb846b9d6185541a6ffea88</hashedAddress>
        </personAddresses>
        <personAddresses index='1'>
            <hashedAddress>56e9d981fdb4aecae83ecda56abea9b6</hashedAddress>
        </personAddresses>
    </hashedAddresses>
    */

    public class HashedAddresses:SchemaObject
    {


        private List<PersonAddresses> m_PersonAddresses = new List<PersonAddresses>();
        public List<PersonAddresses> PersonAddresses
        {
            get { return m_PersonAddresses; }
        }
        public override string Xml
        {
            get { return XmlEx.OuterXml; }
        }

        internal override System.Xml.XmlElement XmlEx
        {
            get {
                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement xhashedAddresses = xdoc.CreateElement("hashedAddresses", XmlHelper.GetSchemaUrl(SchemaVersion));

                foreach (PersonAddresses pa in PersonAddresses)
                {
                    pa.SchemaVersion = SchemaVersion;
                    xhashedAddresses.AppendChild(xdoc.ImportNode(pa.XmlEx, true));
                }

                return xdoc.DocumentElement;
            }
        }

        public static HashedAddresses Create(string hashedAddressesXml,ProviderSchemaVersion schemaVersion)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(hashedAddressesXml);
            HashedAddresses ha = new HashedAddresses();

            XmlNodeList xnlPersonAddresses = xdoc.DocumentElement.SelectNodes("personAddresses");
            foreach (XmlElement xelPA in xnlPersonAddresses)
            {
                PersonAddresses pa = new PersonAddresses();
                pa.Index = int.Parse(xelPA.GetAttribute("index"));
                XmlNodeList xnlHashedAddresses = xelPA.SelectNodes("hashedAddress");
                foreach (XmlElement xelHA in xnlHashedAddresses)
                {
                    pa.HashedAddresses.Add(xelHA.InnerText);
                }

                ha.PersonAddresses.Add(pa);

            }


            return ha;
        }
    }

    public class PersonAddresses : SchemaObject
    {

        private List<string> m_hashedAddresses = new List<string>();

        public List<string> HashedAddresses
        {
            get { return m_hashedAddresses; }
        }
        public int Index { get; set; }

        public override string Xml
        {
            get { return XmlEx.OuterXml; }
        }

        internal override System.Xml.XmlElement XmlEx
        {
            get {
                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement personAddresses = xdoc.CreateElement("personAddresses", XmlHelper.GetSchemaUrl(SchemaVersion));
               xdoc.AppendChild(personAddresses);
               personAddresses.SetAttribute("index", Index.ToString());
                foreach (string sHashAddress in HashedAddresses)
                {
                    XmlHelper.AddStringElement(personAddresses, "hashAddress", sHashAddress.ToLowerInvariant());
                }

                return xdoc.DocumentElement;
            }
        }
    }

}
