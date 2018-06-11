using DocGenWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;


namespace DocGenWebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;
        private CloudTable categories;

        public CategoriesController()
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
            categories = tableClient.GetTableReference("categories");
            categories.CreateIfNotExists();
        }

        [HttpGet]
        public IEnumerable<Category> GetAllCategories()
        {
            TableQuery<Category> query = new TableQuery<Category>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Category"));
            return categories.ExecuteQuery(query).AsQueryable<Category>();
        }

        /// <summary>
        /// Retrieve category by name
        /// Note WebAPI convention is to call parameter "id" otherwise
        /// routing won't pick it up and route to the parameter-less GET method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCategoryByName(string id)
        {
            TableResult retrievedResult = RetrieveCategory(id);
            if (retrievedResult.Result == null)
            {
                return NotFound();
            }
            return Ok(retrievedResult);
        }

        /// <summary>
        /// Create a new category with an ID chosen by the system
        /// Usage POST /categories
        /// </summary>
        /// <param name="category"></param>
        [HttpPost]
        public IHttpActionResult Post([FromBody]Category category)
        {
            string rowKey = category.RowKey.Trim();
            TableResult retrievedResult = RetrieveCategory(rowKey);
            Category updateEntity = (Category)retrievedResult.Result;
            if (updateEntity == null)
            {
                Category insertEntity = new Category(rowKey);
                TableOperation insertOperation = TableOperation.Insert(insertEntity);
                // this may fail if the category has been updated since it was retrieved.
                // To increase robustness for in that case, the category should be retrieved 
                // again and the changes reapplied.
                categories.Execute(insertOperation);
                return Ok();
            }
            else
            {
                // if category by already exists, return HTTP status 409 Conflict
                return Conflict();
            }
        }

        /// <summary>
        /// Create or update a category with a known ID
        /// Usage PUT /categories/categoryid
        /// </summary>
        /// <param name="category"></param>
        [HttpPut]
        public void Put([FromBody]Category category)
        {
            TableResult retrievedResult = RetrieveCategory(category.RowKey.Trim());
            Category updateEntity = (Category)retrievedResult.Result;
            if (updateEntity != null)
            {
                updateEntity.Description = category.Description;
                TableOperation updateOperation = TableOperation.Replace(updateEntity);
                // this may fail if the category has been updated since it was retrieved.
                // To increase robustness for in that case, the category should be retrieved 
                // again and the changes reapplied.
                categories.Execute(updateOperation);
            }
            else
            {
                TableOperation insertOperation = TableOperation.InsertOrReplace(category);
                categories.Execute(insertOperation);
            }
        }

        [HttpDelete]
        public void Delete(string id)
        {
            TableResult retrievedResult = RetrieveCategory(id);
            Category deleteEntity = (Category)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                categories.Execute(deleteOperation);
            }
        }

        /// <summary>
        /// Helper method for retrieving a single entity
        /// </summary>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        private TableResult RetrieveCategory(string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Category>("Category", rowKey);
            return categories.Execute(retrieveOperation);
        }
    }
}
