using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System.Xml;
using System.Xml.Linq;
using OpenXmlPowerTools;

namespace OpenXMLHtmlConverter.Helper
{
    public class HTMLConverterHelper
    {

        /// <summary>
        /// Updates the hyperlink ids in the WordML string with new ids. This is important to keep the hyperlinks working in the downloaded files.
        /// </summary>
        /// <returns>WordML string with new hyperlink ids.</returns>
        public static string UpdateHyperlinkIds(WordprocessingDocument wordDocument, string wordML, string documentId)
        {
            string wordMLString = wordML;

            foreach (var hyperlink in wordDocument.MainDocumentPart.HyperlinkRelationships)
            {
                string oldId = "\"" + hyperlink.Id + "\"";
                string newId = "\"" + documentId + hyperlink.Id + "\"";

                wordMLString = wordMLString.Replace(oldId, newId);
            }

            return wordMLString;
        }

        public static void AddHyperlinks(WordprocessingDocument wordDocument, string hyperlinksString)
        {
            XElement hyperlinksXElement = XElement.Parse(hyperlinksString);
            foreach (var hyperlink in hyperlinksXElement.Descendants("hyperlink"))
            {
                // Add the HyperlinkRelationship
                wordDocument.MainDocumentPart.AddHyperlinkRelationship(new Uri(hyperlink.Attribute("uri").Value), true, hyperlink.Attribute("id").Value);
            }
        }

        public static string GetHyperlinks(WordprocessingDocument wordDocument, string documentId)
        {
            XElement hyperlinksXElement = new XElement("hyperlinks", wordDocument.MainDocumentPart.HyperlinkRelationships.Select(a => new XElement("hyperlink", new XAttribute("uri", a.Uri.OriginalString), new XAttribute("id", documentId + a.Id))));

            return hyperlinksXElement.ToString();
        }

        /// <summary>
        /// Converts a TableCell into Html with list in <ul><li></li></ul> format.
        /// </summary>
        /// <returns>XHTML content of the table cell without <td></td> tags.</returns>
        public static string ConvertToHtml(TableCell tableCell, WordprocessingDocument wordDocument)
        {
            //XElement xElement = HTMLConverter.ConvertToHtml(XElement.Parse(tableCell.OuterXml), wordDocument);
            XElement xElement = HTMLConverterHelper.ConvertToHtml(wordDocument, XElement.Parse(tableCell.OuterXml));
            string html = xElement.ToString();
            html = ConvertToHtmlList(html);

            // remove the <td> tag
            int startIndex = html.IndexOf("<", html.IndexOf("<td") + 3);
            html = html.Substring(startIndex, html.LastIndexOf(">", html.Length - 3) - startIndex + 1);

            html = html.Replace(" class=\"Normal\"", string.Empty);
            html = html.Replace("&bull; ", "&nbsp;");

            // add target="_blank" to open the link in new windows.
            html = html.Replace("href=", "target=\"_blank\" href=");

            return html;
        }

        /// <summary>
        /// Converts into Html list in <ul><li></li></ul> format.
        /// </summary>
        /// <returns>XHTML</returns>
        private static string ConvertToHtmlList(string html)
        {
            List<string> elementLists = new List<string>();
            const string ListParagraphClassTag = "class=\"ListParagraph\"";
            const string ListParagraphStartTag = "<p class=\"ListParagraph\">";
            const string ListParagraphEndTag = "</p>";

            int startIndex = 0;
            int tagIndex = 0;
            int previousTagIndex = 0;
            int stringLength = 0;


            if (html.Contains(ListParagraphStartTag))
            {
                tagIndex = html.IndexOf(ListParagraphStartTag, startIndex);
                stringLength = tagIndex - startIndex; // the portion before the first list paragraph start tag
                elementLists.Add(html.Substring(startIndex, stringLength));

                previousTagIndex = tagIndex;
                startIndex = tagIndex + ListParagraphStartTag.Length; // next start index


                while (html.IndexOf(ListParagraphStartTag, startIndex) > 0) // next start tag exists
                {
                    tagIndex = html.IndexOf(ListParagraphStartTag, startIndex); // next start tag index
                    stringLength = tagIndex - previousTagIndex;

                    string sectionHtml = html.Substring(previousTagIndex, stringLength);
                    if (sectionHtml.IndexOf(ListParagraphEndTag) != (sectionHtml.Length - ListParagraphEndTag.Length))
                    {
                        // something more after ListParagraphEndTag
                        elementLists.Add(sectionHtml.Substring(0, sectionHtml.IndexOf(ListParagraphEndTag) + ListParagraphEndTag.Length));
                        string secondSectionHtml = sectionHtml.Substring(sectionHtml.IndexOf(ListParagraphEndTag) + ListParagraphEndTag.Length);
                        if (secondSectionHtml.Trim() != string.Empty)
                        {
                            elementLists.Add(secondSectionHtml);
                        }
                    }
                    else
                    {
                        elementLists.Add(sectionHtml);
                    }
                    previousTagIndex = tagIndex;
                    startIndex = tagIndex + ListParagraphStartTag.Length; // next start index
                }

                elementLists.Add(html.Substring(previousTagIndex)); // last piece

                for (int i = 0; i < elementLists.Count(); i++)
                {
                    if (elementLists[i].Contains(ListParagraphClassTag))
                    {
                        if (i == 0 || !elementLists[i - 1].Contains(ListParagraphClassTag)) // it's the first element or previous element is not a list paragraph.
                        {
                            // first list item, needs to add <ul>
                            html = html.Replace(elementLists[i], elementLists[i].Replace(ListParagraphStartTag, "<ul>\r\n  <li>").Replace(ListParagraphEndTag, "</li>"));
                        }
                        else
                        {
                            if (i == elementLists.Count() - 1 || !elementLists[i + 1].Contains(ListParagraphClassTag)) // it's the last list item in a block
                            {
                                html = html.Replace(elementLists[i], elementLists[i].Replace(ListParagraphStartTag, "<li>").Replace(ListParagraphEndTag, "</li>\r\n</ul>"));
                            }
                            else
                            {
                                html = html.Replace(elementLists[i], elementLists[i].Replace(ListParagraphStartTag, "<li>").Replace(ListParagraphEndTag, "</li>"));
                            }
                        }
                    }
                }
            }

            html = html.Replace("&bull; ", "&nbsp;");

            return html;
        }


        /// <summary>
        /// Converts a paragraph into Html
        /// </summary>
        /// <returns>XElement</returns>
        public static XElement ConvertToHtml(WordprocessingDocument wordDocument, XNode node)
        {
            HtmlConverterSettings settings = new HtmlConverterSettings()
            {
                PageTitle = "My Page Title",
                CssClassPrefix = "",
                Css = "",
                ConvertFormatting = false
            };

            XElement html = HtmlConverter.ConvertToHtml(wordDocument, node, settings);
            return html;
        }


        public static string ConvertToHtml(WordprocessingDocument wordDocument, TableCell tableCell)
        {
            XElement xElement = HTMLConverterHelper.ConvertToHtml(wordDocument, XElement.Parse(tableCell.OuterXml));
            string html = xElement.ToString();
            html = ConvertToHtmlList(html);

            // remove the <td> tag
            int startIndex = html.IndexOf("<", html.IndexOf("<td") + 3);
            html = html.Substring(startIndex, html.LastIndexOf(">", html.Length - 3) - startIndex + 1);

            html = html.Replace(" class=\"Normal\"", string.Empty);
            // add target="_blank" to open the link in new windows.
            html = html.Replace("href=", "target=\"_blank\" href=");

            return html;
        }



        public static XElement ConvertToHtml(WordprocessingDocument wordDocument)
        {
            HtmlConverterSettings settings = new HtmlConverterSettings()
            {
                PageTitle = "My Page Title",
                CssClassPrefix = "",
                Css = "",
                ConvertFormatting = false
            };

            XElement html = HtmlConverter.ConvertToHtml(wordDocument, settings, null);
            return html;
        }

    }
}