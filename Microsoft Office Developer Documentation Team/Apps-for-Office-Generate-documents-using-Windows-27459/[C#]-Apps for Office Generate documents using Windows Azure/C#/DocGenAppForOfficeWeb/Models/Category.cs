using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DocGenWebAPI.Models
{
    public class Category : TableEntity
    {
        /// <summary>
        /// Parameter-less constructor is required by Azure Tables
        /// Sets partition key to the default partition
        /// </summary>
        public Category()
        {
            this.PartitionKey = "Category"; // required, partition key keeps records together on a node if possible
        }

        /// <summary>
        /// Creates Category entity in a single call
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Category(string name)
        {
            this.PartitionKey = "Category";
            this.RowKey = name; // using name as RowKey has the advantage that it's possible for a human to enter as url and
                                // it improves query performance when searching for a category by name
        }

        public string Description { get; set; }

    }
}