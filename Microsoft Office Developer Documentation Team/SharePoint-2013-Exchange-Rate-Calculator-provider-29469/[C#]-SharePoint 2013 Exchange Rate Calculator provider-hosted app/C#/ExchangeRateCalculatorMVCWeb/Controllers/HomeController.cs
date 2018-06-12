using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExchangeRateCalculatorMVCWeb.Models;

namespace ExchangeRateCalculatorMVCWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exchange(string id, string txtBase)
        {
            string baseValue = !string.IsNullOrEmpty(txtBase) ? txtBase : "1";
            ViewBag.id = !string.IsNullOrEmpty(id) ? id : "USDEUR";
            return View(new ExchangeRateCalculator().GetExchangeRate(ViewBag.id, baseValue));
        }
    }
}
