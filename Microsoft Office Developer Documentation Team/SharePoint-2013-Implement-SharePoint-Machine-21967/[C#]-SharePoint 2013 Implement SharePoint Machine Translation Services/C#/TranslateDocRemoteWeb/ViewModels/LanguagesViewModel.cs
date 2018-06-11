using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranslateDocRemoteWeb.Models;

namespace TranslateDocRemoteWeb.ViewModels
{
    public class LanguagesViewModel
    {
        public List<SupportedLanguage> Languages { get; set; }
        public string SelectedCulture { get; set; }
        public string AppWebUrl { get; set; }
        public string Destination { get; set; }
    }
}