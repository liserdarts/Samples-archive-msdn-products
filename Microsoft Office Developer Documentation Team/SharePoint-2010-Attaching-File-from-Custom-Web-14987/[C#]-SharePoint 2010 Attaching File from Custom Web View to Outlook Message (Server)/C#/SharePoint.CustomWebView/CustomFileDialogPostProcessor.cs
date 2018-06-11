using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Reflection;

namespace SharePoint.CustomWebView
{
    /// <summary>
    /// implements the interface for providing content to the CFD
    /// </summary>
    [Guid("33B06720-F08A-4070-8A47-760BBB7234F6")]
    public class CustomFileDialogPostProcessor : MarshalByRefObject, IFileDialogPostProcessor2, IFileDialogPostProcessor
    {
        /// <summary>
        /// Not used.  Required for compatibility to the interface.
        /// </summary>
        /// <param name="siteId">The parameter is not used.</param>
        /// <param name="webId">The parameter is not used.</param>
        /// <param name="listId">The parameter is not used.</param>
        /// <param name="type">The parameter is not used.</param>
        /// <param name="location">The parameter is not used.</param>
        /// <param name="url">The parameter is not used.</param>
        /// <param name="alternatePresentation">The parameter is not used.</param>
        [SharePointPermission(SecurityAction.Demand, ObjectModel = false)]
        [Obsolete("Should not be used.  Required only for compatibility to the IFileDialogPostProcessor interface.")]
        public void Process(Guid siteId, Guid webId, Guid listId, WffRequestType type, string location, string url, ref string alternatePresentation)
        {
        }

        /// <summary>
        /// Processes the specified request for CFD content and returns the alternatePresentation string of HTML to deliver to the client.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="webId">The web id.</param>
        /// <param name="listId">The list id.</param>
        /// <param name="type">The type of request - Open or Save</param>
        /// <param name="location">The location within the web</param>
        /// <param name="url">The URL of the entire request</param>
        /// <param name="largeListThrottled">The parameter is not used.</param>
        /// <param name="lcid">The lcid for the culture of the request</param>
        /// <param name="userAgent">The browser user agent string</param>
        /// <param name="webProperties">The properties for the Web</param>
        /// <param name="defaultPresentation">The default presentation through the OOB page</param>
        /// <param name="alternatePresentation">The alternate presentation which will be delivered to the client</param>
        public void Process(Guid siteId, Guid webId, Guid listId, WffRequestType type, string location, string url, bool largeListThrottled, int lcid, string userAgent, Hashtable webProperties, string defaultPresentation, ref string alternatePresentation)
        {

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(defaultPresentation) || string.IsNullOrEmpty(userAgent))
            {
                return;
            }

            NameValueCollection queryString = HttpUtility.ParseQueryString(url.Substring(url.IndexOf("?", StringComparison.OrdinalIgnoreCase)));
            string extensions = string.Empty;
            //identify whether this is a request from outlook or not
            bool outlook = false;
            if (!string.IsNullOrEmpty(queryString["Extensions"]))
            {
                extensions = queryString["Extensions"].ToLowerInvariant();
                outlook = true;
            }

            if (extensions.Contains("oob"))
            {
                alternatePresentation = defaultPresentation.Replace("</h1>", "</h1>(Custom dialog turned off due to querystring)");
                return;
            }
            else
            {

                try
                {
                    //is the user trying to open a document or save one
                    string action = "Open";
                    if (type == WffRequestType.Save)
                    {
                        action = "Save";
                    }

                    using (SPSite site = new SPSite(siteId))
                    {
                        using (SPWeb web = site.OpenWeb(webId))
                        {
                            List<string> filters = CustomFileDialogPostProcessor.ParseFileFormatFilterString(queryString["filedialogfiltervalue"]);
                            XmlDocument content = null;
                            Data data = new Data();
                            //If we have an id we are currently looking at a list
                            if (listId.CompareTo(Guid.Empty) != 0)
                            {
                                SPDocumentLibrary library = web.Lists[listId] as SPDocumentLibrary;
                                SPFolder folder = web.GetFolder(location);
                                //get a data representation for that list
                                content = data.GetListData(library, folder, outlook, action, "Modified", filters);
                                //transform the list into Html for the cfd
                            }
                            else //assume website
                            {
                                content = data.GetSiteData(web, outlook, action, "Modified", filters);
                            }

                            XsltArgumentList args = new XsltArgumentList();
                            foreach (string key in queryString.AllKeys)
                            {
                                if (!string.IsNullOrEmpty(key))
                                {
                                    args.AddParam("qs-" + key.ToLowerInvariant(), string.Empty, queryString[key]);
                                }
                            }

                            string xsltPath = RootSiteFromAny(web.Url) + "/Style%20Library/SingleAndMultiselect.xslt";

                            alternatePresentation = GetTransformedData(content, xsltPath, args);
                        }
                    }



                   // alternatePresentation = XmlHelpers.GetTransformedData(content, Concat(SharePointPathManipulation.RootSiteFromAny(pd.SiteUrl), stylesheet), args);

                }
                catch (Exception ex)
                {
                    alternatePresentation = defaultPresentation.Replace("</h1>", "</h1>(Custom dialog not available due to an error.  You can try to navigate using the standard dialog below.  If you see this message repeatedly, try closing and reopening your editing application (e.g. Word))<!--" + ex.Message + ex.StackTrace + "-->");
                }
            }
        }

        /// <summary>
        /// Returns a string of the transformed XML
        /// </summary>
        /// <param name="doc">The document to transform</param>
        /// <param name="xslt">The XSLT to use</param>
        /// <param name="args">Arguments to pass into the transform</param>
        /// <returns>String of transformed XML</returns>
        public static string GetTransformedData(IXPathNavigable doc, string xslt, XsltArgumentList args)
        {
            if (doc == null)
            {
                return null;
            }

            string content = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                XmlUrlResolver resolver = new XmlUrlResolver();
                resolver.Credentials = CredentialCache.DefaultCredentials;
                XsltSettings settings = new XsltSettings(true, true);
                transform.Load(xslt, settings, resolver);
                transform.Transform(doc.CreateNavigator(), args, stream);
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                content = reader.ReadToEnd();
            }

            return content;
        }

        /// <summary>
        /// Root Site from any SharePoint URL
        /// </summary>
        /// <param name="rootSite">Full URL including http[s]://</param>
        /// <returns>The root SPSite URL</returns>
        public static string RootSiteFromAny(string rootSite)
        {
            if (rootSite == null)
            {
                throw new ArgumentException("root site path cannot be null");
            }

            // start the index at position 9 to get the https:// out of the way.  THe next / is the end of the server name
            string returnVal = rootSite.Substring(0, rootSite.IndexOf('/', 9) + 1);
            if (string.IsNullOrEmpty(returnVal))
            {
                // there weren't there / in the URL - the original value must have been the siteurl
                returnVal = rootSite;
            }

            return returnVal;
        }
        /// <summary>
        /// Creates a List of file extensions from a compound string
        /// </summary>
        /// <param name="filedialogfiltervalue">String of file extensions to filter in the format *.abc;*.def;</param>
        /// <returns>List of file extensions</returns>
        private static List<string> ParseFileFormatFilterString(string filedialogfiltervalue)
        {
            List<string> filters = new List<string>();

            if (string.IsNullOrEmpty(filedialogfiltervalue) ||
                filedialogfiltervalue == "*.*;" ||
                filedialogfiltervalue == "*.*")
            {
                // Leave the filter collection blank
            }
            else
            {
                filters = filedialogfiltervalue.TrimEnd('*').Replace("*.", String.Empty).Split(';').ToList<string>();

                // The last item in the split might be empty
                filters.Remove(string.Empty);
            }

            return filters;
        }
    }
}
