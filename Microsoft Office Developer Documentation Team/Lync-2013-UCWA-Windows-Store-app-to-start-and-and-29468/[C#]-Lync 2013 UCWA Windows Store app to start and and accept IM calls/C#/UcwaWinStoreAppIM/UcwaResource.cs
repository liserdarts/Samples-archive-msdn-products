using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinStoreUcwaAppIM
{
    public class UcwaResource
    {
        #region Constructors
        protected XElement xElem = null;
        public UcwaResource(Stream xmlStream)
        {
            var xDoc = XDocument.Load(xmlStream);
            if (xDoc != null)
                xElem = xDoc.Root;
        }
        public UcwaResource(XElement xElement)
        {
            xElem = xElement;
        }
        public UcwaResource(string xmlBlock)
        {
            xElem = XElement.Parse(xmlBlock);
        }
        #endregion Constructors
        public string OuterXml 
        {
            get
            {
                return this.xElem.ToString();
            }
        }

        #region attributes of the resource element:
        private string GetAttributeValue(string attrName)
        {
            return xElem.Attributes(attrName).Select(a => a.Value).FirstOrDefault();
        }
        public string Name { get { return GetAttributeValue("rel"); } }
        public string Uri { get { return this.GetAttributeValue("href"); } }
        public string Xmlns { get { return this.GetAttributeValue("xmlns"); } }
        #endregion resource element's attributes

        #region Links of the resource
        public IEnumerable<XElement> Links
        {
            get
            {
                // return from link in xElem.Elements() where link.Name.LocalName =="link" select link;
                return xElem.Elements().Where(l => l.Name.LocalName == "link");
            }
        }
        public IEnumerable<string> LinkNames
        {
            get
            {
                //return Links.Attributes("rel").Select(a => a.Value); 
                return from link in this.Links select link.Attribute("rel").Value;
            }
        }
        public string GetLinkUri(string linkName)
        {
            return (from link in this.Links where link.Attribute("rel").Value == linkName select link.Attribute("href").Value).FirstOrDefault();
        }
        #endregion Links of the resource

        #region of the embedded resources
        public UcwaResource GetEmbeddedResource(string name)
        {
            return new UcwaResource(this.GetEmbeddedResourceElement(name));
        }
        public IEnumerable<XElement> EmbeddedResourceElements { get { return xElem.Elements().Where(r => r.Name.LocalName == "resource"); } }
        public IEnumerable<string> EmbeddedResourceNames { get { return from res in this.EmbeddedResourceElements select res.Attribute("rel").Value; } }
        public string GetEmbeddedResourceUri(string resourceName)
        {
            var uri = GetLinkUri(resourceName);
            if (string.IsNullOrEmpty(uri))
                uri = (from res in this.EmbeddedResourceElements where res.Attribute("rel").Value == resourceName select res.Attribute("href").Value)
                    .FirstOrDefault();
            return uri;
        }
        public XElement GetEmbeddedResourceElement(string resourceName)
        {
            return (from res in this.EmbeddedResourceElements where res.Attribute("rel").Value == resourceName select res).FirstOrDefault();
        }
        #endregion of the embedded resources

        /// <summary>
        /// Contained resource is either an embedded or linked resource. 
        /// </summary>
        /// <param name="name">The resource name</param>
        /// <param name="transport">A transport to get the linked resource.</param>
        /// <returns></returns>
        public async Task<UcwaResource> GetContainedResource(string name, UcwaAppTransport transport)
        {
            UcwaResource res = null;
            if (this.EmbeddedResourceNames.Contains(name))
            {
                res = this.GetEmbeddedResource(name);
            }
            else if (this.LinkNames.Contains(name) && transport != null)
            {
                var linkUri = this.GetLinkUri(name);
                var result = await transport.GetResourceAsync(linkUri);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;
                res = result.Resource;
            }
            return res;
        }
        public bool ContainsResource(string name)
        {
            return this.EmbeddedResourceNames.Contains(name) || this.LinkNames.Contains(name);
        }
        #region of Properties of the resource
        public IEnumerable<XElement> Properties { get { return xElem.Elements().Where(r => r.Name.LocalName == "property"); } }
        public IEnumerable<string> PropertyNames { get { return from prop in this.Properties select prop.Attribute("name").Value; } }
        public string GetPropertyValue(string propName)
        {
            return (from prop in this.Properties where prop.Attribute("name").Value == propName select prop.Value).FirstOrDefault();
        }
        #endregion of Properties of the resource.
    }

}
