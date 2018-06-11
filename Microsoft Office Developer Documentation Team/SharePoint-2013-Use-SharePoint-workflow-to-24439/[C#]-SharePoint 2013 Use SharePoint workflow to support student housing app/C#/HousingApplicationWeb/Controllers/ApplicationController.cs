using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using HousingApplicationWeb.ViewModels;
using HousingApplicationWeb.Models;

namespace HousingApplicationWeb.Controllers
{
    public class ApplicationController : Controller
    {

        public ActionResult Index(string SPHostUrl, string SPAppWebUrl)
        {
            ApplicationViewModel viewModel = new ApplicationViewModel();
            viewModel.Facilities = new Dictionary<int, string>();

            string appWebUrl = SPAppWebUrl;
            Uri hostWeb = new Uri(SPHostUrl);
            viewModel.AppWebUrl = appWebUrl;
            viewModel.HostWebUrl = SPHostUrl;

            using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWeb, Request.LogonUserIdentity))
            {
                ctx.Load(ctx.Web, w => w.CurrentUser);
                ctx.ExecuteQuery();
                viewModel.StudentId = ctx.Web.CurrentUser.Id.ToString();
            }

            using (ClientContext ctx = new ClientContext(appWebUrl))
            {
                List list = ctx.Web.Lists.GetByTitle("Housing");
                ctx.Load(list);
                CamlQuery query = CamlQuery.CreateAllItemsQuery();
                ListItemCollection items = list.GetItems(query);
                ctx.Load(items);
                ctx.ExecuteQuery();

                foreach (ListItem item in items)
                {
                    viewModel.Facilities.Add(int.Parse(item["ID"].ToString()), item["Title"].ToString());
                }
            }
            return View(viewModel);
        }

        public ActionResult Status(string SPHostUrl, string SPAppWebUrl)
        {
            StatusViewModel viewModel = new StatusViewModel();
            string appWebUrl = SPAppWebUrl;
            Uri hostWeb = new Uri(SPHostUrl);

            using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWeb, Request.LogonUserIdentity))
            {
                ctx.Load(ctx.Web, w => w.CurrentUser);
                ctx.ExecuteQuery();
                viewModel.StudentId = ctx.Web.CurrentUser.Id.ToString();
            }

            using (ClientContext ctx = new ClientContext(appWebUrl))
            {
                List list = ctx.Web.Lists.GetByTitle("Application");
                ctx.Load(list);
                CamlQuery query = new CamlQuery();
                query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Title'/><Value Type='Text'>" +
                    viewModel.StudentId +
                    "</Value></Eq></Where></Query><ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='Status'/></ViewFields></View>";
                ListItemCollection items = list.GetItems(query);
                ctx.Load(items);
                ctx.ExecuteQuery();

                foreach (ListItem item in items)
                {
                    viewModel.Status = item["Status"].ToString();
                }
            }

            
            return View(viewModel);
        }

        public ActionResult SubmitApplication(string SPHostUrl, string SPAppWebUrl, string Id, string LastName, string FirstName, string Request)
        {
            try
            {
                using (ClientContext ctx = new ClientContext(SPAppWebUrl))
                {
                    List list = ctx.Web.Lists.GetByTitle("Application");
                    ListItemCreationInformation info = new ListItemCreationInformation();
                    ListItem newItem = list.AddItem(info);
                    newItem["Title"] = Id;
                    newItem["FName"] = FirstName;
                    newItem["LName"] = LastName;
                    newItem["Request"] = Request;
                    newItem["Status"] = "Submitted";
                    newItem.Update();
                    ctx.ExecuteQuery();

                }
                return RedirectToAction("Status", "Application", new { SPHostUrl = SPHostUrl, SPAppWebUrl = SPAppWebUrl });
            }
            catch (Exception x)
            {
                var m = x.Message;
                return View();
            }
        }
    }
}
