'*********************************************************
'
'    Copyright (c) Microsoft. All rights reserved.
'    This code is licensed under the Microsoft Public License.
'    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
'    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
'    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
'    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************

Imports Microsoft.WindowsAzure
Imports Microsoft.WindowsAzure.Diagnostics
Imports Microsoft.WindowsAzure.ServiceRuntime
Imports Microsoft.WindowsAzure.StorageClient

Public Class WebRole
    Inherits RoleEntryPoint

    Public Overrides Function OnStart() As Boolean
        ' Get connection string and table name from the role's configuration settings.
        Dim connectionString As String = RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString")
        Dim tableName As String = RoleEnvironment.GetConfigurationSettingValue("TableName")

        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse(connectionString)
        Dim tableClient As CloudTableClient = storageAccount.CreateCloudTableClient

        ' Create the table if it does not exist.
        tableClient.CreateTableIfNotExist(tableName)

        Return MyBase.OnStart
    End Function
End Class
