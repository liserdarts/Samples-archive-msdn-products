using DocGenWebAPI.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using DocGenWebAPI.Controllers;

namespace DocGenAppForOfficeWeb.Controllers
{
    public class DocSectionsController : ApiController
    {

        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;
        private CloudTable docSections;
        private CloudBlobClient blobClient;
        private CloudBlobContainer docSectionsContainer;

        public DocSectionsController()
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            // set up Table Storage and BLOB containers
            tableClient = storageAccount.CreateCloudTableClient();
            docSections = tableClient.GetTableReference("docSections");
            docSections.CreateIfNotExists();

            blobClient = storageAccount.CreateCloudBlobClient();
            docSectionsContainer = blobClient.GetContainerReference("docsections");
            docSectionsContainer.CreateIfNotExists();
            //limit BLOB access to those who have the account key
            docSectionsContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
        }

        [HttpGet]
        [Route("api/docsections")]
        public IEnumerable<DocSection> GetAllDocSections()
        {
            TableQuery<DocSection> query = new TableQuery<DocSection>();
            return docSections.ExecuteQuery(query).AsQueryable<DocSection>();
        }

        [HttpGet]
        [Route("api/categories/{categoryId}/docsections/{sectionId}")]
        public IHttpActionResult Get(string categoryId, string sectionId)
        {
            TableResult retrievedResult = RetrieveDocSection(categoryId.Trim(), sectionId.Trim());
            if (retrievedResult.Result == null)
            {
                return NotFound();
            }
            DocSection result = (DocSection)retrievedResult.Result;
            result.ContentOOXML = RetrieveDocSectionBlob(result.ContentOoxmlBlobName);
            result.ContentHTML = RetrieveDocSectionBlob(result.ContentHtmlBlobName);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/categories/{categoryId}/docsections")]
        public IHttpActionResult PostSectionInCategory([FromBody]DocSection docSection, string categoryId)
        {
            TableResult retrievedResult = RetrieveDocSection(categoryId.Trim(), docSection.RowKey.Trim());
            DocSection updateEntity = (DocSection)retrievedResult.Result;
            if (updateEntity == null)
            {
                DocSection insertEntity = new DocSection(categoryId, docSection.RowKey);
                insertEntity.ContentHTML = docSection.ContentHTML;
                insertEntity.ContentOOXML = docSection.ContentOOXML;
                TableOperation insertOperation = TableOperation.Insert(insertEntity);
                // this may fail if the category has been updated since it was retrieved.
                // To increase robustness for in that case, the category should be retrieved 
                // again and the changes reapplied.
                docSections.Execute(insertOperation);
                CategoriesController controller = new CategoriesController();
                controller.Put(new Category(categoryId));
                return Ok();
            }
            else
            {
                // if doc section already exists, return HTTP status 409 Conflict
                return Conflict();
            }
        }

        [HttpPost]
        [Route("api/docsections")]
        public IHttpActionResult Post([FromBody]DocSection docSection)
        {
            string partitionKey = docSection.PartitionKey.Trim();
            string rowKey = docSection.RowKey.Trim();

            TableResult retrievedResult = RetrieveDocSection(partitionKey, rowKey);
            DocSection updateEntity = (DocSection)retrievedResult.Result;
            if (updateEntity == null)
            {
                DocSection insertEntity = new DocSection(partitionKey, rowKey);
                string htmlBlobName = partitionKey + "_" + rowKey + ".HTML";
                if (docSection.ContentHTML == null) docSection.ContentHTML = "<HTML><HEAD></HEAD><BODY>No Preview available</BODY></HTML>";
                insertEntity.ContentHtmlBlobName = SaveBLOB(htmlBlobName, docSection.ContentHTML, "text/html");

                string ooxmlBlobName = partitionKey + "_" + rowKey + ".OOXML";
                insertEntity.ContentOoxmlBlobName = SaveBLOB(ooxmlBlobName, docSection.ContentOOXML, "text/xml");

                TableOperation insertOperation = TableOperation.Insert(insertEntity);
                // this may fail if the category has been updated since it was retrieved.
                // To increase robustness for in that case, the category should be retrieved 
                // again and the changes reapplied.
                try
                {
                    docSections.Execute(insertOperation);
                    CategoriesController controller = new CategoriesController();
                    controller.Put(new Category(insertEntity.PartitionKey));
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                // if doc section already exists, return HTTP status 409 Conflict
                return Conflict();
            }
        }

        [HttpPut]
        [Route("api/categories/{categoryId}/docsections/{sectionId}")]
        public void Put(int id, [FromBody]DocSection docSection, string categoryId, string sectionId)
        {
            TableResult retrievedResult = RetrieveDocSection(categoryId.Trim(), sectionId.Trim());
            DocSection updateEntity = (DocSection)retrievedResult.Result;
            if (updateEntity != null)
            {
                updateEntity.ContentHTML = docSection.ContentHTML;
                updateEntity.ContentOOXML = docSection.ContentOOXML;
                TableOperation updateOperation = TableOperation.Replace(updateEntity);
                // this may fail if the category has been updated since it was retrieved.
                // To increase robustness for in that case, the category should be retrieved 
                // again and the changes reapplied.
                docSections.Execute(updateOperation);
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/categories/{categoryId}/docsections/{sectionId}")]
        public void Delete(string categoryId, string sectionId)
        {
            TableResult retrievedResult = RetrieveDocSection(categoryId.Trim(), sectionId.Trim());
            DocSection deleteEntity = (DocSection)retrievedResult.Result;
            if (deleteEntity != null)
            {
                // delete the blobs
                DeleteDocSectionBlob(deleteEntity.ContentOoxmlBlobName);
                DeleteDocSectionBlob(deleteEntity.ContentHtmlBlobName);
                //delete the entity
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                docSections.Execute(deleteOperation);
            }
        }

        private TableResult RetrieveDocSection(string categoryId, string docSectionId)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<DocSection>(categoryId.Trim(), docSectionId.Trim());
            return docSections.Execute(retrieveOperation);
        }

        private string SaveBLOB(string name, string content, string contentType)
        {
            CloudBlockBlob blob = docSectionsContainer.GetBlockBlobReference(name);
            blob.Properties.ContentType = contentType;
            blob.UploadText(content);
            return blob.Name;
        }
        private string RetrieveDocSectionBlob(string name)
        {
            CloudBlockBlob blob = docSectionsContainer.GetBlockBlobReference(name);
            return blob.DownloadText();
        }

        private void DeleteDocSectionBlob(string name)
        {
            CloudBlockBlob blob = docSectionsContainer.GetBlockBlobReference(name);
            blob.Delete();
        }
    }
}