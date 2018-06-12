namespace WeatherReportMvcWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class WeatherController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                location = "New York";
            }

            return View(new Models.Weatherdata().GetWeatherdata(location));
        }

        [HttpPost]
        public ActionResult PostLocation(string LocationStr)
        {
            return RedirectToAction("Index", new { location = LocationStr });
        }
    }
}
