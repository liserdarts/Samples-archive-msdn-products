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
Imports Microsoft.WindowsAzure.ServiceRuntime
Imports Microsoft.WindowsAzure.StorageClient

' Provides methods to access data in the Windows Azure table. 
Public NotInheritable Class DataLayer
    Private Shared connectionString As String
    Private Shared tableName As String
    Private Shared storageAccount As CloudStorageAccount
    Private Shared tableClient As CloudTableClient

    Private Sub New()
    End Sub

    Shared Sub New()
        ' Get connection string and table name from settings.
        connectionString = RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString")
        tableName = RoleEnvironment.GetConfigurationSettingValue("TableName")

        ' Reference storage account from connection string. 
        storageAccount = CloudStorageAccount.Parse(connectionString)

        ' Create Table service client.
        tableClient = storageAccount.CreateCloudTableClient
    End Sub

    ' Get contacts filtered by prefix.
    Public Shared Function GetContactsByPrefix(ByVal prefix As String) As List(Of ContactEntity)
        Dim query As IQueryable(Of ContactEntity) = tableClient.GetDataServiceContext.CreateQuery(Of ContactEntity)(tableName)
        If prefix IsNot Nothing Then
            query = query.Where(Function(c) c.PartitionKey = prefix.ToUpper)
        End If
        Return query.AsTableServiceQuery().ToList
    End Function

    ' Create new context and get contact.
    Public Shared Function GetContact(ByVal rowKey As String) As ContactEntity
        ' Get data context.
        Dim context As TableServiceContext = tableClient.GetDataServiceContext

        Return GetContact(rowKey, context)
    End Function

    ' Get contact using existing context.
    Public Shared Function GetContact(ByVal rowKey As String, ByVal context As TableServiceContext) As ContactEntity
        Return context.CreateQuery(Of ContactEntity)(tableName).Where(Function(e) e.PartitionKey = rowKey.Substring(0, 1).ToUpper AndAlso e.RowKey = rowKey).Single
    End Function

    ' Update contact data.
    Public Shared Sub UpdateContact(ByVal rowKey As String, ByVal firstName As String, ByVal lastName As String, ByVal email As String, ByVal cellPhone As String, ByVal homePhone As String, ByVal streetAddress As String, ByVal city As String, ByVal state As String, ByVal zipCode As String)
        ' Update the contact if the sort position did not change.
        If rowKey.StartsWith(firstName & "_") Then
            ' Get data context.
            Dim context As TableServiceContext = tableClient.GetDataServiceContext

            ' Set updated values
            Dim entity As ContactEntity = GetContact(rowKey, context)

            entity.FirstName = firstName
            entity.LastName = lastName
            entity.Email = email
            entity.CellPhone = cellPhone
            entity.HomePhone = homePhone
            entity.StreetAddress = streetAddress
            entity.City = city
            entity.State = state
            entity.ZipCode = zipCode

            ' Update the object.
            context.UpdateObject(entity)

            ' Write changes to the Table service.
            context.SaveChanges()
        Else
            ' Delete the contact and insert a new one with new keys
            DeleteContact(rowKey)

            InsertContact(firstName, lastName, email, cellPhone, homePhone, streetAddress, city, state, zipCode)
        End If
    End Sub

    ' Insert a new contact.
    Public Shared Sub InsertContact(ByVal firstName As String, ByVal lastName As String, ByVal email As String, ByVal cellPhone As String, ByVal homePhone As String, ByVal streetAddress As String, ByVal city As String, ByVal state As String, ByVal zipCode As String)
        ' Get data context.
        Dim context As TableServiceContext = tableClient.GetDataServiceContext

        ' Insert the new entity.
        InsertContactInternal(context, firstName, lastName, email, cellPhone, homePhone, streetAddress, city, state, zipCode)

        ' Save changes to the service.
        context.SaveChanges()
    End Sub

    ' Insert a new contact.
    Private Shared Sub InsertContactInternal(ByVal context As TableServiceContext, ByVal firstName As String, ByVal lastName As String, ByVal email As String, ByVal cellPhone As String, ByVal homePhone As String, ByVal streetAddress As String, ByVal city As String, ByVal state As String, ByVal zipCode As String)
        ' Create the new entity.
        Dim entity As New ContactEntity

        ' Partition key is first letter of contact's first name.
        entity.PartitionKey = firstName.Substring(0, 1).ToUpper

        ' Row key is value of first name, with GUID appended to avoid conflicts in case where two first names are the same.
        entity.RowKey = firstName & "_" & Guid.NewGuid().ToString

        ' Populate the other properties.
        entity.FirstName = firstName
        entity.LastName = lastName
        entity.Email = email
        entity.CellPhone = cellPhone
        entity.HomePhone = homePhone
        entity.StreetAddress = streetAddress
        entity.City = city
        entity.State = state
        entity.ZipCode = zipCode

        ' Add the entity.
        context.AddObject(tableName, entity)
    End Sub

    ' Delete a contact.
    Public Shared Sub DeleteContact(ByVal rowKey As String)
        ' Get data context.
        Dim context As TableServiceContext = tableClient.GetDataServiceContext

        ' Retrieve contact.
        Dim entity As ContactEntity = GetContact(rowKey, context)

        ' Delete the entity.
        context.DeleteObject(entity)

        ' Save changes to the service.
        context.SaveChanges()
    End Sub

    ' Bulk insert contacts from a DataTable object.
    Public Shared Sub BulkInsertContacts(ByVal dt As DataTable)
        ' Ensure that the data table will be filtered case-insensitively.
        dt.CaseSensitive = False

        ' Add the data to the Contacts table using batch operations. Each batch consists of the contacts
        ' whose first name starts with the same letter of the alphabet (corresponding to the partition key).
        For a = 65 To 90
            Dim c As Char = System.Convert.ToChar(a)
            ' Select all rows where FirstName begins with same letter.
            Dim rows() = dt.Select("FirstName LIKE '" & c.ToString() & "*'", "FirstName ASC")

            ' Get data context.
            Dim context As TableServiceContext = tableClient.GetDataServiceContext

            Dim i = 0

            ' Create and add each entity.
            For Each row In rows
                ' Insert the new entity for this row.
                InsertContactInternal(context, row.Field(Of String)("FirstName"),
                                      If(dt.Columns.Contains("LastName"), row.Field(Of String)("LastName"), String.Empty),
                                      If(dt.Columns.Contains("Email"), row.Field(Of String)("Email"), String.Empty),
                                      If(dt.Columns.Contains("CellPhone"), row.Field(Of String)("CellPhone"), String.Empty),
                                      If(dt.Columns.Contains("HomePhone"), row.Field(Of String)("HomePhone"), String.Empty),
                                      If(dt.Columns.Contains("StreetAddress"), row.Field(Of String)("StreetAddress"), String.Empty),
                                      If(dt.Columns.Contains("City"), row.Field(Of String)("City"), String.Empty),
                                      If(dt.Columns.Contains("State"), row.Field(Of String)("State"), String.Empty),
                                      If(dt.Columns.Contains("ZipCode"), row.Field(Of String)("ZipCode"), String.Empty))

                ' Increment the counter.
                i += 1

                ' Batch supports only 100 transactions at a time, so if we hit 100 records for this partition,
                ' submit the transaction and keep going.
                If i = 100 Then
                    ' Save changes, using the Batch option.
                    context.SaveChanges(System.Data.Services.Client.SaveChangesOptions.Batch)

                    ' Reset the counter.
                    i = 0
                End If
            Next row

            ' Save changes, using the Batch option.
            context.SaveChanges(System.Data.Services.Client.SaveChangesOptions.Batch)
        Next a
    End Sub
End Class
