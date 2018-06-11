using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAlertsRemoteWeb.Models
{
    public class Alert
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? Expires { get; set;}
        public string ExpiresFormatted
        {
            get {if (this.Expires == null)
                return string.Empty;
            else
                return ((DateTime)this.Expires).ToShortDateString();}
        }
    }
}