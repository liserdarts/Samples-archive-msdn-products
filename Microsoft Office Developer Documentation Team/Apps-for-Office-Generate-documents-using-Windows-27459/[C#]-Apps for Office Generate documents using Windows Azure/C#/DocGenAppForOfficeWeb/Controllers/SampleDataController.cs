using DocGenWebAPI.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.IO;

namespace DocGenAppForOfficeWeb.Controllers
{
    public class SampleDataController : ApiController
    {
        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;
        private CloudTable categories;
        private CloudTable docSections;

        public SampleDataController()
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
            categories = tableClient.GetTableReference("categories");
            categories.CreateIfNotExists();

            docSections = tableClient.GetTableReference("docSections");
            docSections.CreateIfNotExists();
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            GenerateSampleDocSections();
            return Ok();
        }

        private void GenerateSampleDocSections()
        {
            string defaultCategory = "Basic Samples";
            string complexCategory = "Complex Samples";
            string category;

            var samplePath = HostingEnvironment.MapPath("~/SampleData");
            DirectoryInfo dir = new DirectoryInfo(samplePath);
            DocSectionsController controller = new DocSectionsController();

            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.Contains("Text"))
                    category = defaultCategory;
                else
                    category = complexCategory;

                DocSection section = new DocSection(category, FormatAsSectionName(file.Name));
                section.ContentOOXML = readFileContent(file.FullName);
                controller.Post(section);
            }
        }

        private string readFileContent(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                return null;
            }
        }

        private string FormatAsSectionName(string rawName)
        {
            return rawName.Split('.')[0];
        }
    }
}
