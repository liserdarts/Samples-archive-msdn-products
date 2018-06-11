using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Microsoft.Office.Client.TranslationServices;
using TranslateDocRemoteWeb.Models;
using TranslateDocRemoteWeb.ViewModels;

namespace TranslateDocRemoteWeb.Controllers
{
    public class TranslateController : Controller
    {

        public ActionResult Index()
        {
            Uri hostWebUri = new Uri(Request.QueryString["SPHostUrl"]);
            Uri appWebUri = new Uri(Request.QueryString["SPAppWebUrl"]);

            List<SupportedLanguage> languages = new List<SupportedLanguage>();
            List<Setting> settings = new List<Setting>();

            //Get supported cultures
            using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWebUri, Request.LogonUserIdentity))
            {
                IEnumerable<string> cultures = TranslationJob.EnumerateSupportedLanguages(ctx);
                ctx.ExecuteQuery();

                foreach (string culture in cultures)
                {
                    languages.Add(new SupportedLanguage()
                    {
                        Culture = culture,
                        DisplayName = CultureInfo.GetCultureInfo(culture).DisplayName,
                        Selected = false
                    });
                }

            }

            //Get the app settings
            using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(appWebUri, Request.LogonUserIdentity))
            {
                List settingsList = ctx.Web.Lists.GetByTitle("Settings");
                ctx.Load(settingsList);

                ListItemCollection settingItems = settingsList.GetItems(CamlQuery.CreateAllItemsQuery());
                ctx.Load(settingItems);

                ctx.ExecuteQuery();

                foreach (ListItem settingItem in settingItems)
                {
                    settings.Add(new Setting()
                    {
                        Title = settingItem["Title"].ToString(),
                        Value = settingItem["Value"].ToString(),
                    });
                }

            }

            //Set up the ViewModel
            LanguagesViewModel viewModel = new LanguagesViewModel();
            viewModel.SelectedCulture = settings.Where<Setting>(s => s.Title == "Culture").FirstOrDefault().Value;
            viewModel.Destination = settings.Where<Setting>(s => s.Title == "Destination").FirstOrDefault().Value;
            viewModel.Languages = languages;
            viewModel.AppWebUrl = Request.QueryString["SPAppWebUrl"]; //Save the AppWebUrl for future use
            return View(viewModel);
        }

        public ActionResult File()
        {
            try
            {
                Uri hostWebUri = new Uri(Request.QueryString["SPHostUrl"]);
                Uri appWebUri = new Uri(Request.QueryString["SPAppWebUrl"]);

                string listId = Request.QueryString["SPListId"];
                string listItemId = Request.QueryString["SPListItemId"];

                string culture = string.Empty;
                string destination = string.Empty;

                //Get the settings
                List<Setting> settings = new List<Setting>();

                using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(appWebUri, Request.LogonUserIdentity))
                {
                    List settingsList = ctx.Web.Lists.GetByTitle("Settings");
                    ctx.Load(settingsList);

                    ListItemCollection settingItems = settingsList.GetItems(CamlQuery.CreateAllItemsQuery());
                    ctx.Load(settingItems);

                    ctx.ExecuteQuery();

                    foreach (ListItem settingItem in settingItems)
                    {
                        settings.Add(new Setting()
                        {
                            Title = settingItem["Title"].ToString(),
                            Value = settingItem["Value"].ToString(),
                        });
                    }

                }

                culture = settings.Where<Setting>(s => s.Title == "Culture").FirstOrDefault().Value;
                destination = settings.Where<Setting>(s => s.Title == "Destination").FirstOrDefault().Value;

                //Translate file synchronously
                using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWebUri, Request.LogonUserIdentity))
                {

                    //Get the file to translate
                    ListItem listItem = ctx.Web.Lists.GetById(new Guid(listId)).GetItemById(listItemId);
                    ctx.Load(listItem, i => i.File);
                    ctx.ExecuteQuery();

                    //Get the destination library
                    Folder destinationFolder = ctx.Web.Lists.GetByTitle(destination).RootFolder;
                    ctx.Load(destinationFolder, f => f.ServerRelativeUrl);
                    ctx.ExecuteQuery();

                    string ext = listItem.File.Name.Substring(listItem.File.Name.LastIndexOf("."));
                    string inPath = hostWebUri.Scheme + "://" + hostWebUri.Authority + ":" + hostWebUri.Port + listItem.File.ServerRelativeUrl;
                    string outPath = hostWebUri.Scheme + "://" + hostWebUri.Authority + ":" + hostWebUri.Port + destinationFolder.ServerRelativeUrl + "/" + listItem.File.Name;
                    string returnPath = hostWebUri.Scheme + "://" + hostWebUri.Authority + ":" + hostWebUri.Port + destinationFolder.ServerRelativeUrl;
                    ViewBag.ReturnPath = returnPath;

                    //Check if extension is supported
                    ClientResult<bool> isSupported = TranslationJob.IsFileExtensionSupported(ctx, ext.Substring(1));
                    ctx.ExecuteQuery();
                    if (!isSupported.Value)
                        throw new Exception("File extension is not supported.");

                    //Translate
                    SyncTranslator job = new SyncTranslator(ctx, culture);
                    job.OutputSaveBehavior = SaveBehavior.AlwaysOverwrite;
                    ClientResult<TranslationItemInfo> cr = job.Translate(inPath, outPath);
                    ctx.ExecuteQuery();

                    if (!cr.Value.Succeeded)
                        throw new Exception(cr.Value.ErrorMessage);

                    //Return to library
                    Response.Redirect(returnPath);

                }
            }
            catch (Exception x)
            {
                ViewBag.Message = x.Message;
            }

            return View();
        }

        public ActionResult Library()
        {
            try
            {
                Uri hostWebUri = new Uri(Request.QueryString["SPHostUrl"]);
                Uri appWebUri = new Uri(Request.QueryString["SPAppWebUrl"]);

                string listId = Request.QueryString["SPListId"];
                string listItemId = Request.QueryString["SPListItemId"];

                string culture = string.Empty;
                string destination = string.Empty;

                List<Setting> settings = new List<Setting>();

                //Get the settings
                using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(appWebUri, Request.LogonUserIdentity))
                {
                    List settingsList = ctx.Web.Lists.GetByTitle("Settings");
                    ctx.Load(settingsList);

                    ListItemCollection settingItems = settingsList.GetItems(CamlQuery.CreateAllItemsQuery());
                    ctx.Load(settingItems);

                    ctx.ExecuteQuery();

                    foreach (ListItem settingItem in settingItems)
                    {
                        settings.Add(new Setting()
                        {
                            Title = settingItem["Title"].ToString(),
                            Value = settingItem["Value"].ToString(),
                        });
                    }

                }

                culture = settings.Where<Setting>(s => s.Title == "Culture").FirstOrDefault().Value;
                destination = settings.Where<Setting>(s => s.Title == "Destination").FirstOrDefault().Value;

                //Translate library asynchronously
                using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWebUri, Request.LogonUserIdentity))
                {

                    //Get library to translate
                    Folder rootFolder = ctx.Web.Lists.GetById(new Guid(listId)).RootFolder;
                    ctx.Load(rootFolder, f => f.ServerRelativeUrl);

                    //Get destination library
                    Folder destinationFolder = ctx.Web.Lists.GetByTitle(destination).RootFolder;
                    ctx.Load(destinationFolder, f => f.ServerRelativeUrl);
                    ctx.ExecuteQuery();


                    TranslationJob job = new TranslationJob(ctx, culture);
                    job.AddFolder(rootFolder, destinationFolder, true);
                    job.Name = "LibraryTranslation";
                    job.Start();
                    ctx.Load(job);
                    ctx.ExecuteQuery();

                    string returnPath = hostWebUri.Scheme + "://" + hostWebUri.Authority + ":" + hostWebUri.Port + destinationFolder.ServerRelativeUrl;
                    ViewBag.ReturnPath = returnPath;
                    ViewBag.Message = "The translation job has been scheduled.";

                }
            }
            catch (Exception x)
            {
                ViewBag.Message = x.Message;
            }

            return View();
        }

        public ActionResult SaveSetting(string appWebUrl, string title, string value)
        {
            Uri appWebUri = new Uri(appWebUrl);

            using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(appWebUri, Request.LogonUserIdentity))
            {
                List settingsList = ctx.Web.Lists.GetByTitle("Settings");
                ctx.Load(settingsList);

                StringBuilder caml = new StringBuilder();
                caml.Append("<View><Query><Where><Eq><FieldRef Name='Title'/><Value Type='Text'>");
                caml.Append(title);
                caml.Append("</Value></Eq></Where><OrderBy/></Query>");
                caml.Append("<ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='Value'/></ViewFields></View>");
                CamlQuery query = new CamlQuery();
                query.ViewXml = caml.ToString();

                ListItemCollection settingItems = settingsList.GetItems(query);
                ctx.Load(settingItems);
                ctx.ExecuteQuery();

                ListItem settingItem = settingItems[0];
                settingItem["Value"] = value;
                settingItem.Update();
                ctx.ExecuteQuery();
            }

            return Json(new { result = "Settings saved." });
        }

    }
}
