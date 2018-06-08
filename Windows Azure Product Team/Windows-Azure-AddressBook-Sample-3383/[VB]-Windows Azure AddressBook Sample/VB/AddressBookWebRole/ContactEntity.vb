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

Imports Microsoft.WindowsAzure.StorageClient

' Class to represent the table schema
Public Class ContactEntity
    Inherits TableServiceEntity

    Public Sub New()
    End Sub

    Public Property FirstName As String

    Public Property LastName As String

    Public Property Email As String

    Public Property HomePhone As String

    Public Property CellPhone As String

    Public Property StreetAddress As String

    Public Property City As String

    Public Property State As String

    Public Property ZipCode As String
End Class
