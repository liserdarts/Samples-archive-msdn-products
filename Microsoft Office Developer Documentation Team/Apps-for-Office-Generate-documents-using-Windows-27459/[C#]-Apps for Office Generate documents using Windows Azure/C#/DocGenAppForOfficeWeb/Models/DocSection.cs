using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocGenWebAPI.Models
{
    public class DocSection : TableEntity
    {
        public DocSection() { }

        public DocSection(string category, string name) 
        {
            this.PartitionKey = category;
            this.RowKey = name;
        }

        public DocSection(string category, string name, string contentHTML, string contentOOXML)
        {
            this.PartitionKey = category;
            this.RowKey = name;
            this.ContentHTML = contentHTML;
            this.ContentOOXML = contentOOXML;
        }

        public string ContentHtmlBlobName { get; set; }
        public string ContentOoxmlBlobName { get; set; }

        public string ContentHTML { get; set; }
        public string ContentOOXML { get; set; }

    }
}