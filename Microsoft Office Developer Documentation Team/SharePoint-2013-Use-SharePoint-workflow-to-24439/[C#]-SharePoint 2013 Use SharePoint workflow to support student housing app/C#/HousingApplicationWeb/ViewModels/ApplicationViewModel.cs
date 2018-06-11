using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HousingApplicationWeb.Models;

namespace HousingApplicationWeb.ViewModels
{
    public class ApplicationViewModel
    {
        public Dictionary<int,string> Facilities { get; set; }
        public string AppWebUrl { get; set; }
        public string HostWebUrl { get; set; }
        public string StudentId { get; set; }
    }
}