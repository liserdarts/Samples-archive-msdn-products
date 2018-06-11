using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinACommunityRemoteWeb.Models
{
    public class Community
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SiteRelativeUrl { get; set; }
        public string FullUrl { get; set; }
        public int Membership { get; set; }
        public bool CurrentUserIsMember { get; set; }
    }
}