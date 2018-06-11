using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace Contoso.Provision.Pages.AppWeb
{

    public enum WikiPageLayout
    {
        OneColumn = 0,
        OneColumnSideBar = 1,
        TwoColumns = 2,
        TwoColumnsHeader = 3,
        TwoColumnsHeaderFooter = 4,
        ThreeColumns = 5,
        ThreeColumnsHeader = 6,
        ThreeColumnsHeaderFooter = 7
    }


    public class ClientSideSharePointService
    {
                
        private const string WikiPage_OneColumn = @"<div class=""ExternalClassC1FD57BEDB8942DC99A06C02F9A98241""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td style=""width&#58;100%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">false,false,1</span></div>";
        private const string WikiPage_OneColumnSideBar = @"<div class=""ExternalClass47565ACDF7974263AA4A556DA974B687""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td style=""width&#58;66.6%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">false,false,2</span></div>";
        private const string WikiPage_TwoColumns = @"<div class=""ExternalClass3811C839E5984CCEA4C8CF738462AFD8""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td style=""width&#58;49.95%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;49.95%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">false,false,2</span></div>";
        private const string WikiPage_TwoColumnsHeader = @"<div class=""ExternalClass850251EB51394304A07A64A05C0BB0F1""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td colspan=""2""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr><tr style=""vertical-align&#58;top;""><td style=""width&#58;49.95%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;49.95%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">true,false,2</span></div>";
        private const string WikiPage_TwoColumnsHeaderFooter = @"<div class=""ExternalClass71C5527252AD45859FA774445D4909A2""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td colspan=""2""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr><tr style=""vertical-align&#58;top;""><td style=""width&#58;49.95%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;49.95%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr><tr style=""vertical-align&#58;top;""><td colspan=""2""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">true,true,2</span></div>";
        private const string WikiPage_ThreeColumns = @"<div class=""ExternalClass833D1FA704C94892A26C4069C3FE5FE9""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">false,false,3</span></div>";
        private const string WikiPage_ThreeColumnsHeader = @"<div class=""ExternalClassD1A150D6187F449B8A6C4BEA2D4913BB""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td colspan=""3""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr><tr style=""vertical-align&#58;top;""><td style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">true,false,3</span></div>";
        private const string WikiPage_ThreeColumnsHeaderFooter = @"<div class=""ExternalClass5849C2C61FEC44E9B249C60F7B0ACA38""><table id=""layoutsTable"" style=""width&#58;100%;""><tbody><tr style=""vertical-align&#58;top;""><td colspan=""3""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr><tr style=""vertical-align&#58;top;""><td style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td><td class=""ms-wiki-columnSpacing"" style=""width&#58;33.3%;""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr><tr style=""vertical-align&#58;top;""><td colspan=""3""><div class=""ms-rte-layoutszone-outer"" style=""width&#58;100%;""><div class=""ms-rte-layoutszone-inner"" role=""textbox"" aria-haspopup=""true"" aria-autocomplete=""both"" aria-multiline=""true""></div>&#160;</div></td></tr></tbody></table><span id=""layoutsData"" style=""display&#58;none;"">true,true,3</span></div>";

        public ClientSideSharePointService(HttpContext authentication)
        {
            this.Authentication = authentication;
        }

        public HttpContext Authentication
        {
            get;
            set;
        }

        /// <summary>
        /// Wrapper to automatically use the correct ClientContext for an CSOM call
        /// </summary>
        /// <param name="action">code to be executed inside the ClientContext using block</param>
        private void UsingContext(Action<ClientContext> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (Authentication == null)
            {
                throw new ArgumentNullException("Authentication");
            }

            var spContext = SharePointContextProvider.Current.GetSharePointContext(Authentication);
            using (var appOnlyClientContext = spContext.CreateUserClientContextForSPHost())
            {
                action(appOnlyClientContext);
            }
        }

        /// <summary>
        /// List the web parts on a page
        /// </summary>
        /// <param name="properties">Information about the site that this method operates on</param>
        public void GetWebParts(string folder)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl(folder);
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                Microsoft.SharePoint.Client.File ofile = pagesLib.Files[0];
                ctx.Load(ofile);
                ctx.ExecuteQuery();

                LimitedWebPartManager limitedWebPartManager = ofile.GetLimitedWebPartManager(PersonalizationScope.Shared);
                ctx.Load(limitedWebPartManager.WebParts, wps => wps.Include(wp => wp.WebPart.Title, wp => wp.Id));
                ctx.ExecuteQuery();

                if (limitedWebPartManager.WebParts.Count >= 0)
                {
                    for (int i = 0; i < limitedWebPartManager.WebParts.Count; i++)
                    {
                        WebPart oWebPart = limitedWebPartManager.WebParts[i].WebPart;
                        Console.WriteLine(oWebPart.Title);
                        Console.WriteLine(limitedWebPartManager.WebParts[i].Id);
                    }
                }
            });
        }

        /// <summary>
        /// Inserts a web part on a web part page
        /// </summary>
        /// <param name="properties">Site to insert the web part on</param>
        /// <param name="webPart">Information about the web part to insert</param>
        /// <param name="page">Page to add the web part on</param>
        public void AddWebPartToWebPartPage(WebPartEntity webPart, string page)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                //Note: getfilebyserverrelativeurl did not work...not sure why not
                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl("");
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                Microsoft.SharePoint.Client.File webPartPage = null;

                foreach (Microsoft.SharePoint.Client.File aspxFile in pagesLib.Files)
                {
                    if (aspxFile.Name.Equals(page, StringComparison.InvariantCultureIgnoreCase))
                    {
                        webPartPage = aspxFile;
                        break;
                    }
                }

                if (webPartPage == null)
                {
                    return;
                }

                ctx.Load(webPartPage);
                ctx.ExecuteQuery();

                LimitedWebPartManager limitedWebPartManager = webPartPage.GetLimitedWebPartManager(PersonalizationScope.Shared);
                WebPartDefinition oWebPartDefinition = limitedWebPartManager.ImportWebPart(webPart.WebPartXml);

                limitedWebPartManager.AddWebPart(oWebPartDefinition.WebPart, webPart.WebPartZone, webPart.WebPartIndex);
                ctx.ExecuteQuery();

            });
        }

        /// <summary>
        /// Add web part to a wiki style page
        /// </summary>
        /// <param name="properties">Site to insert the web part on</param>
        /// <param name="webPart">Information about the web part to insert</param>
        /// <param name="page">Page to add the web part on</param>
        /// <param name="row">Row of the wiki table that should hold the inserted web part</param>
        /// <param name="col">Column of the wiki table that should hold the inserted web part</param>
        /// <param name="addSpace">Does a blank line need to be added after the web part (to space web parts)</param>
        public void AddWebPartToWikiPage(string folder, WebPartEntity webPart, string page, int row, int col, bool addSpace)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                //Note: getfilebyserverrelativeurl did not work...not sure why not
                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl(folder);
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                Microsoft.SharePoint.Client.File webPartPage = null;

                foreach (Microsoft.SharePoint.Client.File aspxFile in pagesLib.Files)
                {
                    if (aspxFile.Name.Equals(page, StringComparison.InvariantCultureIgnoreCase))
                    {
                        webPartPage = aspxFile;
                        break;
                    }
                }

                if (webPartPage == null)
                {
                    return;
                }

                ctx.Load(webPartPage);
                ctx.Load(webPartPage.ListItemAllFields);
                ctx.ExecuteQuery();

                string wikiField = (string)webPartPage.ListItemAllFields["WikiField"];

                LimitedWebPartManager limitedWebPartManager = webPartPage.GetLimitedWebPartManager(PersonalizationScope.Shared);
                WebPartDefinition oWebPartDefinition = limitedWebPartManager.ImportWebPart(webPart.WebPartXml);
                WebPartDefinition wpdNew = limitedWebPartManager.AddWebPart(oWebPartDefinition.WebPart, "wpz", 0);
                ctx.Load(wpdNew);
                ctx.ExecuteQuery();

                //HTML structure in default team site home page (W16)
                //<div class="ExternalClass284FC748CB4242F6808DE69314A7C981">
                //  <div class="ExternalClass5B1565E02FCA4F22A89640AC10DB16F3">
                //    <table id="layoutsTable" style="width&#58;100%;">
                //      <tbody>
                //        <tr style="vertical-align&#58;top;">
                //          <td colspan="2">
                //            <div class="ms-rte-layoutszone-outer" style="width&#58;100%;">
                //              <div class="ms-rte-layoutszone-inner" style="word-wrap&#58;break-word;margin&#58;0px;border&#58;0px;">
                //                <div><span><span><div class="ms-rtestate-read ms-rte-wpbox"><div class="ms-rtestate-read 9ed0c0ac-54d0-4460-9f1c-7e98655b0847" id="div_9ed0c0ac-54d0-4460-9f1c-7e98655b0847"></div><div class="ms-rtestate-read" id="vid_9ed0c0ac-54d0-4460-9f1c-7e98655b0847" style="display&#58;none;"></div></div></span></span><p> </p></div>
                //                <div class="ms-rtestate-read ms-rte-wpbox">
                //                  <div class="ms-rtestate-read c7a1f9a9-4e27-4aa3-878b-c8c6c87961c0" id="div_c7a1f9a9-4e27-4aa3-878b-c8c6c87961c0"></div>
                //                  <div class="ms-rtestate-read" id="vid_c7a1f9a9-4e27-4aa3-878b-c8c6c87961c0" style="display&#58;none;"></div>
                //                </div>
                //              </div>
                //            </div>
                //          </td>
                //        </tr>
                //        <tr style="vertical-align&#58;top;">
                //          <td style="width&#58;49.95%;">
                //            <div class="ms-rte-layoutszone-outer" style="width&#58;100%;">
                //              <div class="ms-rte-layoutszone-inner" style="word-wrap&#58;break-word;margin&#58;0px;border&#58;0px;">
                //                <div class="ms-rtestate-read ms-rte-wpbox">
                //                  <div class="ms-rtestate-read b55b18a3-8a3b-453f-a714-7e8d803f4d30" id="div_b55b18a3-8a3b-453f-a714-7e8d803f4d30"></div>
                //                  <div class="ms-rtestate-read" id="vid_b55b18a3-8a3b-453f-a714-7e8d803f4d30" style="display&#58;none;"></div>
                //                </div>
                //              </div>
                //            </div>
                //          </td>
                //          <td class="ms-wiki-columnSpacing" style="width&#58;49.95%;">
                //            <div class="ms-rte-layoutszone-outer" style="width&#58;100%;">
                //              <div class="ms-rte-layoutszone-inner" style="word-wrap&#58;break-word;margin&#58;0px;border&#58;0px;">
                //                <div class="ms-rtestate-read ms-rte-wpbox">
                //                  <div class="ms-rtestate-read 0b2f12a4-3ab5-4a59-b2eb-275bbc617f95" id="div_0b2f12a4-3ab5-4a59-b2eb-275bbc617f95"></div>
                //                  <div class="ms-rtestate-read" id="vid_0b2f12a4-3ab5-4a59-b2eb-275bbc617f95" style="display&#58;none;"></div>
                //                </div>
                //              </div>
                //            </div>
                //          </td>
                //        </tr>
                //      </tbody>
                //    </table>
                //    <span id="layoutsData" style="display&#58;none;">true,false,2</span>
                //  </div>
                //</div>

                XmlDocument xd = new XmlDocument();
                xd.PreserveWhitespace = true;
                xd.LoadXml(wikiField);

                // Sometimes the wikifield content seems to be surrounded by an additional div? 
                XmlElement layoutsTable = xd.SelectSingleNode("div/div/table") as XmlElement;
                if (layoutsTable == null)
                {
                    layoutsTable = xd.SelectSingleNode("div/table") as XmlElement;
                }

                XmlElement layoutsZoneInner = layoutsTable.SelectSingleNode(string.Format("tbody/tr[{0}]/td[{1}]/div/div", row, col)) as XmlElement;
                // - space element
                XmlElement space = xd.CreateElement("p");
                XmlText text = xd.CreateTextNode(" ");
                space.AppendChild(text);

                // - wpBoxDiv
                XmlElement wpBoxDiv = xd.CreateElement("div");
                layoutsZoneInner.AppendChild(wpBoxDiv);

                if (addSpace)
                {
                    layoutsZoneInner.AppendChild(space);
                }

                XmlAttribute attribute = xd.CreateAttribute("class");
                wpBoxDiv.Attributes.Append(attribute);
                attribute.Value = "ms-rtestate-read ms-rte-wpbox";
                attribute = xd.CreateAttribute("contentEditable");
                wpBoxDiv.Attributes.Append(attribute);
                attribute.Value = "false";
                // - div1
                XmlElement div1 = xd.CreateElement("div");
                wpBoxDiv.AppendChild(div1);
                div1.IsEmpty = false;
                attribute = xd.CreateAttribute("class");
                div1.Attributes.Append(attribute);
                attribute.Value = "ms-rtestate-read " + wpdNew.Id.ToString("D");
                attribute = xd.CreateAttribute("id");
                div1.Attributes.Append(attribute);
                attribute.Value = "div_" + wpdNew.Id.ToString("D");
                // - div2
                XmlElement div2 = xd.CreateElement("div");
                wpBoxDiv.AppendChild(div2);
                div2.IsEmpty = false;
                attribute = xd.CreateAttribute("style");
                div2.Attributes.Append(attribute);
                attribute.Value = "display:none";
                attribute = xd.CreateAttribute("id");
                div2.Attributes.Append(attribute);
                attribute.Value = "vid_" + wpdNew.Id.ToString("D");

                ListItem listItem = webPartPage.ListItemAllFields;
                listItem["WikiField"] = xd.OuterXml;
                listItem.Update();
                ctx.ExecuteQuery();

            });
        }

        public void AddLayoutToWikiPage(string folder, WikiPageLayout layout, string page)
        {
            string html = "";
            switch (layout)
            {
                case WikiPageLayout.OneColumn:
                    html = WikiPage_OneColumn;
                    break;
                case WikiPageLayout.OneColumnSideBar:
                    html = WikiPage_OneColumnSideBar;
                    break;
                case WikiPageLayout.TwoColumns:
                    html = WikiPage_TwoColumns;
                    break;
                case WikiPageLayout.TwoColumnsHeader:
                    html = WikiPage_TwoColumnsHeader;
                    break;
                case WikiPageLayout.TwoColumnsHeaderFooter:
                    html = WikiPage_TwoColumnsHeaderFooter;
                    break;
                case WikiPageLayout.ThreeColumns:
                    html = WikiPage_ThreeColumns;
                    break;
                case WikiPageLayout.ThreeColumnsHeader:
                    html = WikiPage_ThreeColumnsHeader;
                    break;
                case WikiPageLayout.ThreeColumnsHeaderFooter:
                    html = WikiPage_ThreeColumnsHeaderFooter;
                    break;
                default:
                    break;
            }

            AddHtmlToWikiPage(folder, html, page);
        }

        /// <summary>
        /// Add html to a wiki style page
        /// </summary>
        /// <param name="properties">Site to insert the html</param>
        /// <param name="webPart">The html to insert</param>
        /// <param name="page">Page to add the html on</param>
        public void AddHtmlToWikiPage(string folder, string html, string page)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                //Note: getfilebyserverrelativeurl did not work...not sure why not
                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl(folder);
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                Microsoft.SharePoint.Client.File wikiPage = null;

                foreach (Microsoft.SharePoint.Client.File aspxFile in pagesLib.Files)
                {
                    if (aspxFile.Name.Equals(page, StringComparison.InvariantCultureIgnoreCase))
                    {
                        wikiPage = aspxFile;
                        break;
                    }
                }

                if (wikiPage == null)
                {
                    return;
                }

                ctx.Load(wikiPage);
                ctx.Load(wikiPage.ListItemAllFields);
                ctx.ExecuteQuery();

                string wikiField = (string)wikiPage.ListItemAllFields["WikiField"];

                ListItem listItem = wikiPage.ListItemAllFields;
                listItem["WikiField"] = html;
                listItem.Update();
                ctx.ExecuteQuery();
            });
        }

        /// <summary>
        /// Add web part to a wiki style page
        /// </summary>
        /// <param name="html">html to be inserted</param>
        /// <param name="page">Page to add the web part on</param>
        /// <param name="row">Row of the wiki table that should hold the inserted web part</param>
        /// <param name="col">Column of the wiki table that should hold the inserted web part</param>
        public void AddHtmlToWikiPage(string folder, string html, string page, int row, int col)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                //Note: getfilebyserverrelativeurl did not work...not sure why not
                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl(folder);
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                Microsoft.SharePoint.Client.File webPartPage = null;

                foreach (Microsoft.SharePoint.Client.File aspxFile in pagesLib.Files)
                {
                    if (aspxFile.Name.Equals(page, StringComparison.InvariantCultureIgnoreCase))
                    {
                        webPartPage = aspxFile;
                        break;
                    }
                }

                if (webPartPage == null)
                {
                    return;
                }

                ctx.Load(webPartPage);
                ctx.Load(webPartPage.ListItemAllFields);
                ctx.ExecuteQuery();

                string wikiField = (string)webPartPage.ListItemAllFields["WikiField"];

                XmlDocument xd = new XmlDocument();
                xd.PreserveWhitespace = true;
                xd.LoadXml(wikiField);

                // Sometimes the wikifield content seems to be surrounded by an additional div? 
                XmlElement layoutsTable = xd.SelectSingleNode("div/div/table") as XmlElement;
                if (layoutsTable == null)
                {
                    layoutsTable = xd.SelectSingleNode("div/table") as XmlElement;
                }

                // Add the html content
                XmlElement layoutsZoneInner = layoutsTable.SelectSingleNode(string.Format("tbody/tr[{0}]/td[{1}]/div/div", row, col)) as XmlElement;
                XmlText text = xd.CreateTextNode("!!123456789!!");
                layoutsZoneInner.AppendChild(text);

                ListItem listItem = webPartPage.ListItemAllFields;
                listItem["WikiField"] = xd.OuterXml.Replace("!!123456789!!", html);
                listItem.Update();
                ctx.ExecuteQuery();

            });
        }

        /// <summary>
        /// Deletes a web part from a page
        /// </summary>
        /// <param name="properties">Site to insert the web part on</param>
        /// <param name="title">Title of the web part that needs to be deleted</param>
        /// <param name="page">Page to add the web part on</param>
        public void DeleteWebPart(string folder, string title, string page)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                //Note: getfilebyserverrelativeurl did not work...not sure why not
                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl(folder);
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                Microsoft.SharePoint.Client.File webPartPage = null;

                foreach (Microsoft.SharePoint.Client.File aspxFile in pagesLib.Files)
                {
                    if (aspxFile.Name.Equals(page, StringComparison.InvariantCultureIgnoreCase))
                    {
                        webPartPage = aspxFile;
                        break;
                    }
                }

                if (webPartPage == null)
                {
                    return;
                }

                ctx.Load(webPartPage);
                ctx.ExecuteQuery();

                LimitedWebPartManager limitedWebPartManager = webPartPage.GetLimitedWebPartManager(PersonalizationScope.Shared);
                ctx.Load(limitedWebPartManager.WebParts, wps => wps.Include(wp => wp.WebPart.Title));
                ctx.ExecuteQuery();

                if (limitedWebPartManager.WebParts.Count >= 0)
                {
                    for (int i = 0; i < limitedWebPartManager.WebParts.Count; i++)
                    {
                        WebPart oWebPart = limitedWebPartManager.WebParts[i].WebPart;
                        if (oWebPart.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase))
                        {
                            limitedWebPartManager.WebParts[i].DeleteWebPart();
                            ctx.ExecuteQuery();
                            break;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Adds a blank Wiki page to the site pages library
        /// </summary>
        /// <param name="properties">Site to operate on</param>
        /// <param name="wikiPageName">Wiki page to operate on</param>
        /// <returns>The relative URL of the added wiki page</returns>
        public string AddWikiPage(string folder, string wikiPageName)
        {
            string wikiPageUrl = "";

            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                var pageLibrary = web.Lists.GetByTitle(folder);

                ctx.Load(pageLibrary.RootFolder, f => f.ServerRelativeUrl);
                ctx.ExecuteQuery();

                var pageLibraryUrl = pageLibrary.RootFolder.ServerRelativeUrl;
                var newWikiPageUrl = pageLibraryUrl + "/" + wikiPageName;

                var currentPageFile = web.GetFileByServerRelativeUrl(newWikiPageUrl);

                ctx.Load(currentPageFile, f => f.Exists);
                ctx.ExecuteQuery();

                if (!currentPageFile.Exists)
                {
                    var newpage = pageLibrary.RootFolder.Files.AddTemplateFile(newWikiPageUrl, TemplateFileType.WikiPage);

                    ctx.Load(newpage);
                    ctx.ExecuteQuery();

                    wikiPageUrl = String.Format("sitepages/{0}", wikiPageName);
                }
            });

            return wikiPageUrl;
        }

        /// <summary>
        /// Adds a list to a site
        /// </summary>
        /// <param name="properties">Site to operate on</param>
        /// <param name="listType">Type of the list</param>
        /// <param name="featureID">Feature guid that brings this list type</param>
        /// <param name="listName">Name of the list</param>
        /// <param name="enableVersioning">Enable versioning on the list</param>
        public bool AddList(int listType, Guid featureID, string listName, bool enableVersioning)
        {
            bool created = false;

            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                ListCollection listCollection = web.Lists;
                ctx.Load(listCollection, lists => lists.Include(list => list.Title).Where(list => list.Title == listName));
                ctx.ExecuteQuery();

                if (listCollection.Count == 0)
                {
                    ListCollection listCol = web.Lists;
                    ListCreationInformation lci = new ListCreationInformation();
                    lci.Title = listName;
                    lci.TemplateFeatureId = featureID;
                    lci.TemplateType = listType;
                    List newList = listCol.Add(lci);
                    if (enableVersioning)
                    {
                        newList.EnableVersioning = true;
                        newList.EnableMinorVersions = true;
                    }
                    newList.Update();
                    ctx.Load(listCol);
                    ctx.ExecuteQuery();
                    created = true;
                }
            });

            return created;
        }

        /// <summary>
        /// Inserts an item to the promoted links list
        /// </summary>
        /// <param name="properties">Site to operate on</param>
        /// <param name="listName">List to operate on</param>
        /// <param name="title">Title of the promoted link</param>
        /// <param name="url">Url of the promoted link</param>
        public void AddPromotedSiteLink(string listName, string title, string url)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                List listToInsertTo = web.Lists.GetByTitle(listName);
                //ctx.Load(listToInsertTo, l => l.Id);
                ListItemCreationInformation lici = new ListItemCreationInformation();
                ListItem listItem = listToInsertTo.AddItem(lici);
                listItem["Title"] = title;
                listItem["LinkLocation"] = url;
                listItem["LaunchBehavior"] = "New tab";
                listItem["TileOrder"] = 1;
                listItem.Update();
                ctx.ExecuteQuery();
            });
        }

        /// <summary>
        /// Returns the GUID id of a list
        /// </summary>
        /// <param name="properties">Site to operate on</param>
        /// <param name="listName">List to operate on</param>
        public Guid GetListID(string listName)
        {
            Guid ret = Guid.NewGuid();
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                List listToQuery = web.Lists.GetByTitle(listName);
                ctx.Load(listToQuery, l => l.Id);
                ctx.ExecuteQuery();

                ret = listToQuery.Id;
            });

            return ret;
        }

        public void DeleteDemoPages(string folder)
        {
            UsingContext(ctx =>
            {
                Site site = ctx.Site;
                Web web = site.RootWeb;

                //Note: getfilebyserverrelativeurl did not work...not sure why not
                Microsoft.SharePoint.Client.Folder pagesLib = web.GetFolderByServerRelativeUrl(folder);
                ctx.Load(pagesLib.Files);
                ctx.ExecuteQuery();

                List<File> toDelete = new List<File>();

                foreach (Microsoft.SharePoint.Client.File aspxFile in pagesLib.Files)
                {
                    if (aspxFile.Name.StartsWith("scenario1", StringComparison.InvariantCultureIgnoreCase) || aspxFile.Name.StartsWith("scenario2", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //aspxFile.DeleteObject();
                        toDelete.Add(aspxFile);
                    }
                }

                for (int i = 0; i < toDelete.Count; i++ )
                {
                    toDelete[i].DeleteObject();
                }

                ctx.ExecuteQuery();

            });
        }


        /// <summary>
        /// Constructs the webpart XML needed to inject the promoted links web part to the home page of the team site
        /// </summary>
        /// <param name="listID">ID of the promoted links list</param>
        /// <param name="listUrl">URL of the list</param>
        /// <param name="pageUrl">URL of the page that will host the webpart</param>
        /// <param name="title">Title of the web part</param>
        /// <returns>The constructed web part XML</returns>
        public string WpPromotedLinks(Guid listID, string listUrl, string pageUrl, string title)
        {
            StringBuilder wp = new StringBuilder(100);
            wp.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            wp.Append("<webParts>");
            wp.Append("	<webPart xmlns=\"http://schemas.microsoft.com/WebPart/v3\">");
            wp.Append("		<metaData>");
            wp.Append("			<type name=\"Microsoft.SharePoint.WebPartPages.XsltListViewWebPart, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\" />");
            wp.Append("			<importErrorMessage>Cannot import this Web Part.</importErrorMessage>");
            wp.Append("		</metaData>");
            wp.Append("		<data>");
            wp.Append("			<properties>");
            wp.Append("				<property name=\"ShowWithSampleData\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"Default\" type=\"string\" />");
            wp.Append("				<property name=\"NoDefaultStyle\" type=\"string\" null=\"true\" />");
            wp.Append("				<property name=\"CacheXslStorage\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"ViewContentTypeId\" type=\"string\" />");
            wp.Append("				<property name=\"XmlDefinitionLink\" type=\"string\" />");
            wp.Append("				<property name=\"ManualRefresh\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"ListUrl\" type=\"string\" />");
            wp.Append(String.Format("				<property name=\"ListId\" type=\"System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">{0}</property>", listID.ToString()));
            wp.Append(String.Format("				<property name=\"TitleUrl\" type=\"string\">{0}</property>", listUrl));
            wp.Append("				<property name=\"EnableOriginalValue\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"Direction\" type=\"direction\">NotSet</property>");
            wp.Append("				<property name=\"ServerRender\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"ViewFlags\" type=\"Microsoft.SharePoint.SPViewFlags, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\">None</property>");
            wp.Append("				<property name=\"AllowConnect\" type=\"bool\">True</property>");
            wp.Append(String.Format("				<property name=\"ListName\" type=\"string\">{0}</property>", ("{" + listID.ToString().ToUpper() + "}")));
            wp.Append("				<property name=\"ListDisplayName\" type=\"string\" />");
            wp.Append("				<property name=\"AllowZoneChange\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"ChromeState\" type=\"chromestate\">Normal</property>");
            wp.Append("				<property name=\"DisableSaveAsNewViewButton\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"ViewFlag\" type=\"string\" />");
            wp.Append("				<property name=\"DataSourceID\" type=\"string\" />");
            wp.Append("				<property name=\"ExportMode\" type=\"exportmode\">All</property>");
            wp.Append("				<property name=\"AutoRefresh\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"FireInitialRow\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"AllowEdit\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"Description\" type=\"string\" />");
            wp.Append("				<property name=\"HelpMode\" type=\"helpmode\">Modeless</property>");
            wp.Append("				<property name=\"BaseXsltHashKey\" type=\"string\" null=\"true\" />");
            wp.Append("				<property name=\"AllowMinimize\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"CacheXslTimeOut\" type=\"int\">86400</property>");
            wp.Append("				<property name=\"ChromeType\" type=\"chrometype\">Default</property>");
            wp.Append("				<property name=\"Xsl\" type=\"string\" null=\"true\" />");
            wp.Append("				<property name=\"JSLink\" type=\"string\" null=\"true\" />");
            wp.Append("				<property name=\"CatalogIconImageUrl\" type=\"string\">/_layouts/15/images/itgen.png?rev=26</property>");
            wp.Append("				<property name=\"SampleData\" type=\"string\" null=\"true\" />");
            wp.Append("				<property name=\"UseSQLDataSourcePaging\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"TitleIconImageUrl\" type=\"string\" />");
            wp.Append("				<property name=\"PageSize\" type=\"int\">-1</property>");
            wp.Append("				<property name=\"ShowTimelineIfAvailable\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"Width\" type=\"string\" />");
            wp.Append("				<property name=\"DataFields\" type=\"string\" />");
            wp.Append("				<property name=\"Hidden\" type=\"bool\">False</property>");
            wp.Append(String.Format("				<property name=\"Title\" type=\"string\">{0}</property>", title));
            wp.Append("				<property name=\"PageType\" type=\"Microsoft.SharePoint.PAGETYPE, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\">PAGE_NORMALVIEW</property>");
            wp.Append("				<property name=\"DataSourcesString\" type=\"string\" />");
            wp.Append("				<property name=\"AllowClose\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"InplaceSearchEnabled\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"WebId\" type=\"System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">00000000-0000-0000-0000-000000000000</property>");
            wp.Append("				<property name=\"Height\" type=\"string\" />");
            wp.Append("				<property name=\"GhostedXslLink\" type=\"string\">main.xsl</property>");
            wp.Append("				<property name=\"DisableViewSelectorMenu\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"DisplayName\" type=\"string\" />");
            wp.Append("				<property name=\"IsClientRender\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"XmlDefinition\" type=\"string\">");
            wp.Append(string.Format("&lt;View Name=\"{1}\" Type=\"HTML\" Hidden=\"TRUE\" ReadOnly=\"TRUE\" OrderedView=\"TRUE\" DisplayName=\"\" Url=\"{0}\" Level=\"1\" BaseViewID=\"1\" ContentTypeID=\"0x\" &gt;&lt;Query&gt;&lt;OrderBy&gt;&lt;FieldRef Name=\"TileOrder\" Ascending=\"TRUE\"/&gt;&lt;FieldRef Name=\"Modified\" Ascending=\"FALSE\"/&gt;&lt;/OrderBy&gt;&lt;/Query&gt;&lt;ViewFields&gt;&lt;FieldRef Name=\"Title\"/&gt;&lt;FieldRef Name=\"BackgroundImageLocation\"/&gt;&lt;FieldRef Name=\"Description\"/&gt;&lt;FieldRef Name=\"LinkLocation\"/&gt;&lt;FieldRef Name=\"LaunchBehavior\"/&gt;&lt;FieldRef Name=\"BackgroundImageClusterX\"/&gt;&lt;FieldRef Name=\"BackgroundImageClusterY\"/&gt;&lt;/ViewFields&gt;&lt;RowLimit Paged=\"TRUE\"&gt;30&lt;/RowLimit&gt;&lt;JSLink&gt;sp.ui.tileview.js&lt;/JSLink&gt;&lt;XslLink Default=\"TRUE\"&gt;main.xsl&lt;/XslLink&gt;&lt;Toolbar Type=\"Standard\"/&gt;&lt;/View&gt;</property>", pageUrl, ("{" + Guid.NewGuid().ToString() + "}")));
            wp.Append("				<property name=\"InitialAsyncDataFetch\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"AllowHide\" type=\"bool\">True</property>");
            wp.Append("				<property name=\"ParameterBindings\" type=\"string\">");
            wp.Append("  &lt;ParameterBinding Name=\"dvt_sortdir\" Location=\"Postback;Connection\"/&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"dvt_sortfield\" Location=\"Postback;Connection\"/&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"dvt_startposition\" Location=\"Postback\" DefaultValue=\"\"/&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"dvt_firstrow\" Location=\"Postback;Connection\"/&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"OpenMenuKeyAccessible\" Location=\"Resource(wss,OpenMenuKeyAccessible)\" /&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"open_menu\" Location=\"Resource(wss,open_menu)\" /&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"select_deselect_all\" Location=\"Resource(wss,select_deselect_all)\" /&gt;");
            wp.Append("            &lt;ParameterBinding Name=\"idPresEnabled\" Location=\"Resource(wss,idPresEnabled)\" /&gt;&lt;ParameterBinding Name=\"NoAnnouncements\" Location=\"Resource(wss,noXinviewofY_LIST)\" /&gt;&lt;ParameterBinding Name=\"NoAnnouncementsHowTo\" Location=\"Resource(wss,noXinviewofY_DEFAULT)\" /&gt;</property>");
            wp.Append("				<property name=\"DataSourceMode\" type=\"Microsoft.SharePoint.WebControls.SPDataSourceMode, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c\">List</property>");
            wp.Append("				<property name=\"AutoRefreshInterval\" type=\"int\">60</property>");
            wp.Append("				<property name=\"AsyncRefresh\" type=\"bool\">False</property>");
            wp.Append("				<property name=\"HelpUrl\" type=\"string\" />");
            wp.Append("				<property name=\"MissingAssembly\" type=\"string\">Cannot import this Web Part.</property>");
            wp.Append("				<property name=\"XslLink\" type=\"string\" null=\"true\" />");
            wp.Append("				<property name=\"SelectParameters\" type=\"string\" />");
            wp.Append("			</properties>");
            wp.Append("		</data>");
            wp.Append("	</webPart>");
            wp.Append("</webParts>");
            return wp.ToString();
        }


    }
}