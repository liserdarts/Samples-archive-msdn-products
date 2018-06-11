using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using HousingApplicationWeb.ViewModels;

namespace HousingApplicationWeb.Controllers
{
    public class WelcomeController : Controller
    {

        public ActionResult Index(string SPHostUrl)
        {
            Uri hostWeb = new Uri(SPHostUrl);
            WelcomeViewModel viewModel = new WelcomeViewModel();

            using (ClientContext ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWeb, Request.LogonUserIdentity))
            {
                ctx.Load(ctx.Web, w => w.CurrentUser);
                ctx.ExecuteQuery();
                viewModel.Message = "Welcome, " + ctx.Web.CurrentUser.Title;
                viewModel.StudentId = ctx.Web.CurrentUser.LoginName;
            }

            return View(viewModel);
        }
    }
}
