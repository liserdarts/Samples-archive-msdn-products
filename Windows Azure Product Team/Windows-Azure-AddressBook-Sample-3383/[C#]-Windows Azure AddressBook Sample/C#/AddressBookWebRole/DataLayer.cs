//*********************************************************
//
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace AddressBookWebRole
{
    // Provides methods to access data in the Windows Azure table. 
    public static class DataLayer
    {
        private static string connectionString;
        private static string tableName;
        private static CloudStorageAccount storageAccount;
        private static CloudTableClient tableClient;

        static DataLayer()
        {
            // Get connection string and table name from settings.
            connectionString = RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString");
            tableName = RoleEnvironment.GetConfigurationSettingValue("TableName");

            // Reference storage account from connection string. 
            storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create Table service client.
            tableClient = storageAccount.CreateCloudTableClient();
        }

        // Get contacts filtered by prefix.
        public static List<ContactEntity> GetContactsByPrefix(string prefix)
        {
            IQueryable<ContactEntity> query = tableClient.GetDataServiceContext().CreateQuery<ContactEntity>(tableName);
            if (prefix != null)
            {
                query = query.Where(c => c.PartitionKey == prefix.ToUpper());
            }
            return query.AsTableServiceQuery().ToList();
        }

        // Create new context and get contact.
        public static ContactEntity GetContact(string rowKey)
        {
            // Get data context.
            TableServiceContext context = tableClient.GetDataServiceContext();

            return GetContact(rowKey, context);
        }

        // Get contact using existing context.
        public static ContactEntity GetContact(string rowKey, TableServiceContext context)
        {
            return context.CreateQuery<ContactEntity>(tableName).Where(e => e.PartitionKey == rowKey.Substring(0, 1).ToUpper() && e.RowKey == rowKey).Single();
        }

        // Update contact data.
        public static void UpdateContact(
            string rowKey, string firstName, string lastName, string email, string cellPhone, string homePhone,
            string streetAddress, string city, string state, string zipCode)
        {
            // Update the contact if the sort position did not change.
            if (rowKey.StartsWith(firstName + "_"))
            {
                // Get data context.
                TableServiceContext context = tableClient.GetDataServiceContext();

                // Set updated values
                ContactEntity entity = GetContact(rowKey, context);

                entity.FirstName = firstName;
                entity.LastName = lastName;
                entity.Email = email;
                entity.CellPhone = cellPhone;
                entity.HomePhone = homePhone;
                entity.StreetAddress = streetAddress;
                entity.City = city;
                entity.State = state;
                entity.ZipCode = zipCode;

                // Update the object.
                context.UpdateObject(entity);

                // Write changes to the Table service.
                context.SaveChanges();
            }
            else
            {
                // Delete the contact and insert a new one with new keys
                DeleteContact(rowKey);

                InsertContact(firstName, lastName, email, cellPhone, homePhone, streetAddress, city, state, zipCode);
            }
        }

        // Insert a new contact.
        public static void InsertContact(string firstName, string lastName, string email,
            string cellPhone, string homePhone, string streetAddress, string city, string state, string zipCode)
        {
            // Get data context.
            TableServiceContext context = tableClient.GetDataServiceContext();

            // Insert the new entity.
            InsertContactInternal(context, firstName, lastName, email, cellPhone, homePhone, streetAddress, city, state, zipCode);

            // Save changes to the service.
            context.SaveChanges();
        }

        // Insert a new contact.
        private static void InsertContactInternal(TableServiceContext context, string firstName, string lastName, string email,
            string cellPhone, string homePhone, string streetAddress, string city, string state, string zipCode)
        {
            // Create the new entity.
            ContactEntity entity = new ContactEntity();

            // Partition key is first letter of contact's first name.
            entity.PartitionKey = firstName.Substring(0, 1).ToUpper();

            // Row key is value of first name, with GUID appended to avoid conflicts in case where two first names are the same.
            entity.RowKey = firstName + "_" + Guid.NewGuid().ToString();

            // Populate the other properties.
            entity.FirstName = firstName;
            entity.LastName = lastName;
            entity.Email = email;
            entity.CellPhone = cellPhone;
            entity.HomePhone = homePhone;
            entity.StreetAddress = streetAddress;
            entity.City = city;
            entity.State = state;
            entity.ZipCode = zipCode;

            // Add the entity.
            context.AddObject(tableName, entity);
        }

        // Delete a contact.
        public static void DeleteContact(string rowKey)
        {
            // Get data context.
            TableServiceContext context = tableClient.GetDataServiceContext();

            // Retrieve contact.
            ContactEntity entity = GetContact(rowKey, context);

            // Delete the entity.
            context.DeleteObject(entity);

            // Save changes to the service.
            context.SaveChanges();
        }

        // Bulk insert contacts from a DataTable object.
        public static void BulkInsertContacts(DataTable dt)
        {
            // Ensure that the data table will be filtered case-insensitively.
            dt.CaseSensitive = false;

            // Add the data to the Contacts table using batch operations. Each batch consists of the contacts
            // whose first name starts with the same letter of the alphabet (corresponding to the partition key).
            for (char c = 'A'; c <= 'Z'; c++)
            {
                // Select all rows where FirstName begins with same letter.
                DataRow[] rows = dt.Select("FirstName LIKE '" + c.ToString() + "*'", "FirstName ASC");

                // Get data context.
                TableServiceContext context = tableClient.GetDataServiceContext();

                int i = 0;

                // Create and add each entity.
                foreach (DataRow row in rows)
                {
                    // Insert the new entity for this row.
                    InsertContactInternal(
                        context, 
                        row.Field<string>("FirstName"),
                        dt.Columns.Contains("LastName") ? row.Field<string>("LastName") : string.Empty,
                        dt.Columns.Contains("Email") ? row.Field<string>("Email") : string.Empty,
                        dt.Columns.Contains("CellPhone") ? row.Field<string>("CellPhone") : string.Empty,
                        dt.Columns.Contains("HomePhone") ? row.Field<string>("HomePhone") : string.Empty,
                        dt.Columns.Contains("StreetAddress") ? row.Field<string>("StreetAddress") : string.Empty,
                        dt.Columns.Contains("City") ? row.Field<string>("City") : string.Empty,
                        dt.Columns.Contains("State") ? row.Field<string>("State") : string.Empty,
                        dt.Columns.Contains("ZipCode") ? row.Field<string>("ZipCode") : string.Empty);

                    // Increment the counter.
                    i++;

                    // Batch supports only 100 transactions at a time, so if we hit 100 records for this partition,
                    // submit the transaction and keep going.
                    if (i == 100)
                    {
                        // Save changes, using the Batch option.
                        context.SaveChanges(System.Data.Services.Client.SaveChangesOptions.Batch);

                        // Reset the counter.
                        i = 0;
                    }
                }

                // Save changes, using the Batch option.
                context.SaveChanges(System.Data.Services.Client.SaveChangesOptions.Batch);
            }
        }
    }
}