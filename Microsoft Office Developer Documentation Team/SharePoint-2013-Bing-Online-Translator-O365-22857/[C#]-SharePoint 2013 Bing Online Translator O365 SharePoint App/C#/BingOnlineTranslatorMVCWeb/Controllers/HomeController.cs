using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BingOnlineTranslatorMVCWeb.Models;

namespace BingOnlineTranslatorMVCWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Translator()
        {
            List<Language> languages = new TranslatorModels().GetLanguages();
            ViewBag.toSelList = languages;

            Language[] copy = new Language[languages.Count];
            languages.CopyTo(copy);
            List<Language> sourceLanguages = copy.ToList<Language>();
            sourceLanguages.Insert(0, new Language { LanguageName = "Auto", LanguageCode = "auto" });
            ViewBag.sourceSelList = new SelectList(sourceLanguages, "LanguageCode", "LanguageName");

            return View();
        }

        [HttpPost]
        public string Translate(string from, string to, string text)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(new TranslatorModels().Translate(from, to, text));
        }

        public ActionResult Speak(string text, string language)
        {
            return File(new TranslatorModels().Speak(text, language), "audio/mp3");
        }
    }
}
