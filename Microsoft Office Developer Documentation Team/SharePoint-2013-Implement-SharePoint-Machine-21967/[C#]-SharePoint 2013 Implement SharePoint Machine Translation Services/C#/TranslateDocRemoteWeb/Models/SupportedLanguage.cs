using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslateDocRemoteWeb.Models
{
    public class SupportedLanguage
    {
        public string Culture { get; set; }
        public string DisplayName { get; set; }
        public bool Selected { get; set; }
    }
}