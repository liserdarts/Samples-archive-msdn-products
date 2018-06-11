using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemAlertsRemoteWeb.Models;
using Microsoft.SharePoint.Client;

namespace SystemAlertsRemoteWeb.Controllers
{
    public class AlertsController : Controller
    {

        public ActionResult Index()
        {
            var contextToken = TokenHelper.GetContextTokenFromRequest(Request);
            var appWebUrl = Request["SPAppWebUrl"];

            ViewBag.AlertsListLink = appWebUrl + "/Lists/Alerts";

            using (var ctx = TokenHelper.GetClientContextWithContextToken(appWebUrl, contextToken, Request.Url.Authority))
            {
                List alertList = ctx.Web.Lists.GetByTitle("Alerts");
                ctx.Load(alertList);

                ListItemCollection alertItems = alertList.GetItems(CamlQuery.CreateAllItemsQuery());
                ctx.Load(alertItems);

                ctx.ExecuteQuery();

                List<Alert> alerts = new List<Alert>();
                foreach (ListItem alertItem in alertItems)
                {
                    Alert alert = new Alert()
                    {
                        ID = (int)alertItem["ID"],
                        Title = (alertItem["Title"] == null) ? string.Empty : alertItem["Title"].ToString(),
                        Body = (alertItem["Body"] == null) ? string.Empty : alertItem["Body"].ToString(),
                        Expires = (alertItem["Expires"] == null) ? null : (DateTime?)alertItem["Expires"]
                    };

                    alerts.Add(alert);
                }

                ViewBag.Alerts = alerts;
            }

            return View();
        }

        public ActionResult AppPart(string SPHostUrl, string SPLanguage, string SPAppWebUrl)
        {
            var contextToken = TokenHelper.GetContextTokenFromRequest(Request);
            var appWebUrl = Request["SPAppWebUrl"];

            using (var ctx = TokenHelper.GetClientContextWithContextToken(appWebUrl, contextToken, Request.Url.Authority))
            {
                List alertList = ctx.Web.Lists.GetByTitle("Alerts");
                ctx.Load(alertList);

                StringBuilder caml = new StringBuilder();
                caml.Append("<View><Query>");
                caml.Append("<Where><Gt><FieldRef Name='Expires'/><Value Type='DateTime'><Today/></Value></Gt></Where>");
                caml.Append("<OrderBy><FieldRef Name='Expires'/></OrderBy></Query>");
                caml.Append("<ViewFields><FieldRef Name='ID'/><FieldRef Name='Title'/><FieldRef Name='Body'/><FieldRef Name='Expires'/></ViewFields></View>");
                CamlQuery query = new CamlQuery();
                query.ViewXml = caml.ToString();

                ListItemCollection alertItems = alertList.GetItems(query);
                ctx.Load(alertItems);

                ctx.ExecuteQuery();

                List<Alert> alerts = new List<Alert>();
                foreach (ListItem alertItem in alertItems)
                {
                    Alert alert = new Alert()
                    {
                        ID = (int)alertItem["ID"],
                        Title = (alertItem["Title"] == null) ? string.Empty : alertItem["Title"].ToString(),
                        Body = (alertItem["Body"] == null) ? string.Empty : alertItem["Body"].ToString(),
                        Expires = (alertItem["Expires"] == null) ? null : (DateTime?)alertItem["Expires"]
                    };

                    alerts.Add(alert);
                }

                ViewBag.Alerts = alerts;
            }

            return View();
        }
    }
}

