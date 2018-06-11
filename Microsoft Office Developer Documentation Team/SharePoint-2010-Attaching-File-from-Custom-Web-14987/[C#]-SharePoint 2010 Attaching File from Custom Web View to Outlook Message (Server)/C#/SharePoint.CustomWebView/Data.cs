using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.SharePoint;
using System.Globalization;
using System.Data;

namespace SharePoint.CustomWebView
{
    /// <summary>
    /// class for contructing a data representation of a lists content used by the xslt to construct a html representation which is understood by the CFD
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets the list data.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="content">The content to be rendered</param>
        /// <param name="isOutlook">if set to <c>true</c> [is outlook].</param>
        /// <param name="action">The action ie opening or saving documents</param>
        /// <param name="listName">Name of the list.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="filterValues">The filter values.</param>
        /// <returns></returns>
        public XmlDocument GetListData(SPDocumentLibrary library, SPFolder content, bool isOutlook, string action, string sortField, List<string> filterValues)
        {
            XmlDocument doc = GetDefaultStructure(isOutlook, action, library.Title, library.ParentWebUrl);
            AddListHeaders(doc);
            AddListContent(library,filterValues, sortField, content, isOutlook, doc);
            return doc;
        }

        /// <summary>
        /// Gets the site data.
        /// </summary>
        /// <param name="parentWeb">The parent web.</param>
        /// <param name="isOutlook">if set to <c>true</c> [is outlook].</param>
        /// <param name="action">The action.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="filterValues">The filter values.</param>
        /// <returns></returns>
        public XmlDocument GetSiteData(SPWeb parentWeb, bool isOutlook, string action, string sortField, List<string> filterValues)
        {
            XmlDocument doc = GetDefaultStructure(isOutlook, action, parentWeb.Title,parentWeb.Url);
            AddListHeaders(doc);
            AddWebContent(parentWeb, filterValues, sortField, isOutlook, doc);
            return doc;
        }

        /// <summary>
        /// Gets the basic structure for the page data - this is used by the xslt to render html in a form the CFD understands
        /// </summary>
        /// <param name="outlookRequest">if set to <c>true</c> [outlook request].</param>
        /// <param name="action">The action, opening or closing.</param>
        /// <param name="name">The name of the library.</param>
        /// <returns></returns>
        private XmlDocument GetDefaultStructure(bool outlookRequest, string action, string name, string siteUrl)
        {
            StringBuilder sb = new StringBuilder("<PageData><Description>" + name + "</Description><Action>");
            sb.Append(action);
            sb.Append("</Action><Extensions>");
            if (outlookRequest)
            {
                //add some info for the xslt to know to display version information
                sb.Append("multiselect;versions");
            }
            sb.Append("</Extensions><ServerRelativeUrl>/</ServerRelativeUrl><SiteUrl>" + siteUrl + "</SiteUrl></PageData>");
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(sb.ToString());
            return doc;
        }

        /// <summary>
        /// Adds the content of the list.
        /// </summary>
        /// <param name="web">The web.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="outlookRequest">if set to <c>true</c> [outlook request].</param>
        /// <param name="parentDocument">The parent document.</param>
        private void AddWebContent(SPWeb web, List<string> filters, string sortField, bool outlookRequest, XmlDocument parentDocument)
        {
            XmlNode listContent = parentDocument.CreateElement("ListContent");
            parentDocument.SelectSingleNode("/PageData").AppendChild(listContent);

            foreach (SPList list in web.Lists)
            {
                if (list.BaseType == SPBaseType.DocumentLibrary && list.BaseTemplate == SPListTemplateType.DocumentLibrary && list.OnQuickLaunch == true)
                {
                    string listUrl = list.DefaultViewUrl.Substring(0, list.DefaultViewUrl.IndexOf("/Forms", StringComparison.OrdinalIgnoreCase));
                    listUrl = listUrl.Substring(web.ServerRelativeUrl.Length);
                    listContent.AppendChild(CreateListItemEntry(parentDocument, list.Title, web.Url + "/" + listUrl, "Folder", "", "", "", "itdl.png", "", ""));
                }
            }
        }

        /// <summary>
        /// Adds the content of the list.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="content">The content within the list.</param>
        /// <param name="outlookRequest">if set to <c>true</c> [outlook request].</param>
        /// <param name="parentDocument">The parent document.</param>
        private void AddListContent(SPDocumentLibrary library, List<string>filters, string sortField, SPFolder content, bool outlookRequest, XmlDocument parentDocument)
        {
            XmlNode listContent = parentDocument.CreateElement("ListContent");
            parentDocument.SelectSingleNode("/PageData").AppendChild(listContent);

            foreach (SPFolder folder in content.SubFolders)
            {
                listContent.AppendChild(CreateListItemEntry(parentDocument,folder.Name ,folder.ParentWeb.Url + "/" +  folder.Url, "folder", "", "", "", "folder.gif", "", ""));
            }

            SPQuery query = new SPQuery();
            query.Query = Data.GetCamlNew(filters, sortField, true);
            query.Folder = content;
            SPListItemCollection col = library.GetItems(query);
            
            AddListItems(parentDocument, col, listContent, outlookRequest);
        }

        /// <summary>
        /// Adds the list items.
        /// </summary>
        /// <param name="parentDocument">The parent document.</param>
        /// <param name="col">The col.</param>
        /// <param name="listContent">Content of the list.</param>
        /// <param name="outlookRequest">if set to <c>true</c> [outlook request].</param>
        private void AddListItems(XmlDocument parentDocument, SPListItemCollection col, XmlNode listContent, bool outlookRequest)
        {
            foreach (SPListItem item in col)
            {
                SPFile file = item.File;
                string editor = item["Editor"].ToString();
                int editorId = int.Parse(editor.Substring(0, editor.IndexOf(";", StringComparison.Ordinal)), CultureInfo.InvariantCulture);

                string docId = string.Empty;

                if (item.ParentList.Fields.ContainsField("ows__dlc_DocId"))
                {
                    docId = item["ows__dlc_DocId"].ToString();
                }

                XmlNode listItem = listContent.AppendChild(CreateListItemEntry(parentDocument, file.Name, file.Web.Url + "/" + file.Url, "file", file.Web.AllUsers.GetByID(editorId).Name, file.TimeLastModified.ToString(), file.Web.Title, file.IconUrl, docId, file.UIVersionLabel));

                if (outlookRequest)
                {
                    AddVersions(file, listItem, parentDocument);
                }
            }
        }

        /// <summary>
        /// Adds the versions.
        /// </summary>
        /// <param name="currentFile">The current file.</param>
        /// <param name="listItem">The list item.</param>
        /// <param name="parentDocument">The parent document.</param>
        private void AddVersions(SPFile currentFile, XmlNode listItem, XmlDocument parentDocument)
        {
            XmlNode versions = listItem.AppendChild(parentDocument.CreateElement("Versions"));

            foreach(SPFileVersion version in currentFile.Versions)
            {
                XmlNode versionNode = versions.AppendChild(parentDocument.CreateElement("Version"));
                XmlAttribute url = versionNode.Attributes.Append(parentDocument.CreateAttribute("Url"));
                url.InnerText = currentFile.Web.Url + "/" + version.Url;
                XmlAttribute versionLabel = versionNode.Attributes.Append(parentDocument.CreateAttribute("VersionLabel"));
                versionLabel.InnerText = version.VersionLabel;
                XmlAttribute created = versionNode.Attributes.Append(parentDocument.CreateAttribute("Created"));
                created.InnerText = version.Created.ToString();
                XmlAttribute createdBy = versionNode.Attributes.Append(parentDocument.CreateAttribute("CreatedBy"));
                createdBy.InnerText = version.CreatedBy.ToString();
                XmlAttribute checkinComment = versionNode.Attributes.Append(parentDocument.CreateAttribute("CheckInComment"));
                checkinComment.InnerText = version.CheckInComment;                
            }
        }

        /// <summary>
        /// Creates the list item entry.
        /// </summary>
        /// <param name="parentDoc">The parent doc.</param>
        /// <param name="name">The name.</param>
        /// <param name="fullUrl">The full URL.</param>
        /// <param name="fileAttribute">The file attribute.</param>
        /// <param name="editor">The editor.</param>
        /// <param name="lastModified">The last modified.</param>
        /// <param name="parentFolderName">Name of the parent folder.</param>
        /// <param name="docIcon">The doc icon.</param>
        /// <param name="docId">The doc id.</param>
        /// <param name="version">The version.</param>
        /// <returns>the list item entry</returns>
        private XmlNode CreateListItemEntry(XmlDocument parentDoc,string name, string fullUrl, string fileAttribute,string editor, string lastModified, string parentFolderName, string docIcon, string docId, string version)
        {
            XmlNode listItem = parentDoc.CreateElement("ContentItem");
            XmlNode fileUrl = listItem.AppendChild(parentDoc.CreateElement("FileUrl"));
            fileUrl.InnerText = fullUrl;
            XmlNode fileAtt = listItem.AppendChild(parentDoc.CreateElement("FileAttribute"));
            fileAtt.InnerText = fileAttribute;

            listItem.AppendChild(AddProperty("Name", name, parentDoc));
            listItem.AppendChild(AddProperty("Editor", editor, parentDoc));
            listItem.AppendChild(AddProperty("Last_x0020_Modified", lastModified, parentDoc));
            listItem.AppendChild(AddProperty("ParentFolderName", parentFolderName, parentDoc));
            listItem.AppendChild(AddProperty("DocIcon", docIcon, parentDoc));
            listItem.AppendChild(AddProperty("ows__dlc_DocId", docId, parentDoc));
            listItem.AppendChild(AddProperty("ows__UIVersionString", version, parentDoc));

            return listItem;
        }

        /// <summary>
        /// Adds a property to a list entry.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="parentDocument">The parent document.</param>
        /// <returns></returns>
        private XmlNode AddProperty(string propertyName, string propertyValue, XmlDocument parentDocument)
        {
            XmlNode prop = parentDocument.CreateElement("Property");
            XmlAttribute name = prop.Attributes.Append(parentDocument.CreateAttribute("Name"));
            name.InnerText = propertyName;
            XmlAttribute value = prop.Attributes.Append(parentDocument.CreateAttribute("Value"));
            value.InnerText = propertyValue;

            return prop;
        }

        /// <summary>
        /// Adds the headers if the request is for list data.
        /// </summary>
        /// <param name="doc">The xml document to append to.</param>
        /// <returns>the header node</returns>
        private void AddListHeaders(XmlDocument doc)
        {
            XmlNode headers = doc.CreateElement("Headers");
            doc.SelectSingleNode("/PageData").AppendChild(headers);

            headers.AppendChild(CreateHeader("","IconUrl",doc));
            headers.AppendChild(CreateHeader("Name", "Name", doc));
            headers.AppendChild(CreateHeader("Document ID", "ows__dlc_DocId", doc));
            headers.AppendChild(CreateHeader("Version", "ows__UIVersionString", doc));
            headers.AppendChild(CreateHeader("Modified By", "Editor", doc));
            headers.AppendChild(CreateHeader("Modified", "Last_x0020_Modified", doc));
        }

        /// <summary>
        /// Adds a header node to the list content.
        /// </summary>
        /// <param name="headerTitle">The header title.</param>
        /// <param name="headerId">The header id.</param>
        /// <param name="parentDoc">The parent doc to create from.</param>
        /// <returns></returns>
        private XmlNode CreateHeader(string headerTitle,string headerId, XmlDocument parentDoc)
        {
            XmlNode Header = parentDoc.CreateElement("Header");
            XmlNode headerTitleNode = Header.AppendChild(parentDoc.CreateElement("ColumnTitle"));
            headerTitleNode.InnerText = headerTitle;
            XmlNode headerIdNode = Header.AppendChild(parentDoc.CreateElement("ColumnId"));

            return Header;
        }

        /// <summary>
        /// Constructs CAML to filter by file extention
        /// </summary>
        /// <param name="filterExtensions">List of file extensions by which to restrict results</param>
        /// <param name="sortFieldCAML">The SharePoint field to use in a CAML Order By Clause.  If null, the results are ordered by Modified Date.</param>
        /// <param name="sortAsc">Used with sortFieldCAML, True to sort the results Ascending, False to sort Descending</param>
        /// <returns>An SPQuery to restrict file extensions by filterExtensions and order by sortFieldCAML</returns>
        private static string GetCamlNew(List<string> filterExtensions, string sortFieldCAML, bool sortAsc)
        {
            string query = string.Empty;
            // Exmaple incoming filterExtenions string is "*.docx;*.docm;*.dotx;*.dotm;*.doc;*.dot;*.htm;*.html;*.rtf;*.mht;*.mhtml;*.xml;*.odt" 

            // Sample outgoing Where clause is a bit specialist
            // <Where>
            //    <Or>
            //        <Or>
            //            <Or>
            //                <Or>
            //                    <Eq>
            //                        <FieldRef Name="DocIcon"/>
            //                        <Value Type="Computed">BLAH</Value>
            //                    </Eq>
            //                    <Eq>
            //                        <FieldRef Name="DocIcon"/>
            //                        <Value Type="Computed">BLAH2</Value>
            //                    </Eq>
            //                </Or>
            //                <Eq>
            //                    <FieldRef Name="DocIcon"/>
            //                    <Value Type="Computed">BLAH3</Value>
            //                </Eq>
            //            </Or>
            //            <Eq>
            //                <FieldRef Name="DocIcon"/>
            //                <Value Type="Computed">BLAH4</Value>
            //            </Eq>
            //        </Or>
            //        <Eq>
            //            <FieldRef Name="DocIcon"/>
            //            <Value Type="Computed">BLAH5</Value>
            //        </Eq>
            //    </Or>
            // </Where>

            // List of requested filter extensions

            // Formula for string for n extensions is therefore
            // <Where> { <Or> x n-1 } { <Eq, inc fieldref bit />  </Or (except first time) >} </Where>
            if (filterExtensions.Count > 0)
            {
                string whereClause = string.Empty;
                for (int i = 1; i < filterExtensions.Count; i++)
                {
                    whereClause += "<Or>";
                }

                bool firstClause = true;
                foreach (string extention in filterExtensions)
                {
                    // Some filters, such as from Excel, include a wildcard in the extension.  In this case, we trim off the wildcard
                    // and filter with <BeginsWith> rather than <Eq>
                    if (extention.Substring(extention.Length - 1) == "*")
                    {
                        string trimmedExtension = extention.TrimEnd('*');
                        whereClause += string.Format(CultureInfo.InvariantCulture, "<BeginsWith><FieldRef Name=\"DocIcon\"/><Value Type=\"Computed\">{0}</Value></BeginsWith>", trimmedExtension);
                    }
                    else
                    {
                        whereClause += string.Format(CultureInfo.InvariantCulture, "<Eq><FieldRef Name=\"DocIcon\"/><Value Type=\"Computed\">{0}</Value></Eq>", extention);
                    }

                    if (firstClause)
                    {
                        firstClause = false;
                    }
                    else
                    {
                        whereClause += "</Or>";
                    }
                }

                query = string.Format(CultureInfo.InvariantCulture, "<Where>{0}</Where>", whereClause);
            }

            if (string.IsNullOrEmpty(sortFieldCAML))
            {
                query += "<OrderBy><FieldRef Name=\"Modified\" Ascending=\"False\" /></OrderBy>";
            }
            else
            {
                query += string.Format(CultureInfo.InvariantCulture, "<OrderBy><FieldRef Name=\"{0}\" Ascending=\"{1}\" /></OrderBy>", sortFieldCAML, sortAsc.ToString());
            }

            return query;
        }
    }
}
