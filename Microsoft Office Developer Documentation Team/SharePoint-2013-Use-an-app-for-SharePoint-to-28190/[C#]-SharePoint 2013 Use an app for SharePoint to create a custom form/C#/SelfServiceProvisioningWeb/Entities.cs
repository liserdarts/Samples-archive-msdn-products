using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfServiceProvisioningWeb
{
    public class SSConfig
    {
        public int ListItemId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string SiteTemplate { get; set; }
        public string BasePath { get; set; }
        public string SiteType { get; set; }
        public string MasterUrl { get; set; }
        public string StorageMaximumLevel { get; set; }
        public string UserCodeMaximumLevel { get; set; }
    }

    internal static class Entensions
    {
        public static List<SSConfig> ToList(this ListItemCollection items, string appWebUrl, string listName)
        {
            List<SSConfig> configItems = new List<SSConfig>();
            foreach (ListItem item in items)
                configItems.Add(item.ToSSConfig(appWebUrl, listName));
            return configItems;
        }

        private static SSConfig ToSSConfig(this ListItem item, string appWebUrl, string listName)
        {
            return new SSConfig() {
                ListItemId = item.Id,
                Title = item["Title"].ToString(),
                ImageUrl = String.Format("{0}/{1}/{2}", appWebUrl, listName, item["FileLeafRef"].ToString()),
                SiteTemplate = item["SiteTemplate"].ToString(),
                BasePath = item["BasePath"].ToString(),
                SiteType = item["SiteType"].ToString(),
                MasterUrl = (item["MasterPageUrl"] != null) ? item["MasterPageUrl"].ToString() : "",
                StorageMaximumLevel = item["StorageMaximumLevel"].ToString(),
                UserCodeMaximumLevel = item["UserCodeMaximumLevel"].ToString()
            };
        }
    }
}