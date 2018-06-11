using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinACommunityRemoteWeb.Models
{
    public class Question
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}