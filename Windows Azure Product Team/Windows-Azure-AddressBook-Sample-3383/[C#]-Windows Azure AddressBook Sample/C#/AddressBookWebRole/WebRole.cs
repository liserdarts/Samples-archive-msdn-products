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
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace AddressBookWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // Get connection string and table name from the role's configuration settings.
            string connectionString = RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString");
            string tableName = RoleEnvironment.GetConfigurationSettingValue("TableName");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it does not exist.
            tableClient.CreateTableIfNotExist(tableName);

            return base.OnStart();
        }
    }
}
