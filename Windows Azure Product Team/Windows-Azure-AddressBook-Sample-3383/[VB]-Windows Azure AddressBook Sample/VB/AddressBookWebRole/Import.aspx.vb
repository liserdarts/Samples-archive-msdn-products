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

Imports System.IO
Imports System.Reflection
Imports Microsoft.VisualBasic.FileIO

Partial Public Class Import
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Check for file.
        If Me.Uploader.PostedFile.FileName = String.Empty Then
            Me.lblInfo.Text = "No file specified."
            Me.lblInfo.ForeColor = System.Drawing.Color.Red
        Else
            Try
                ' Import csv data into a table.
                Me.ImportData(Me.Uploader.PostedFile.InputStream)
            Catch err As Exception
                Me.lblInfo.Text = err.Message
                Me.lblInfo.ForeColor = System.Drawing.Color.Red
            End Try
        End If
    End Sub

    ' Process csv file and import data into the application.
    Private Sub ImportData(ByVal csvStream As Stream)
        Me.lblInfo.Text = ""

        ' Create data table to hold the data in memory.
        Dim dt As New DataTable

        ' Parse the csv file and add the data to the data table.
        Using csvFile = New TextFieldParser(csvStream)
            csvFile.TextFieldType = FieldType.Delimited
            csvFile.SetDelimiters(",")
            csvFile.HasFieldsEnclosedInQuotes = True

            ' Read the first row of data (which should contain column names).
            Dim fields() As String = csvFile.ReadFields

            ' Add columns to data table, using first (header) row.
            Dim col As DataColumn = Nothing
            Dim fieldIndices As New List(Of Integer)

            If Not csvFile.EndOfData Then
                ' The FirstName field is required, since it's used for the partition key and row key.
                If Not fields.Contains("FirstName") Then
                    Me.lblInfo.Text = "The .csv file must contain a FirstName field, named in the first row of data."
                    Me.lblInfo.ForeColor = System.Drawing.Color.Red
                End If

                ' Create array of property names from ContactEntity.
                Dim propertyNames As New List(Of String)
                For Each info In GetType(ContactEntity).GetProperties
                    propertyNames.Add(info.Name)
                Next info

                ' Add a field to the data table if it matches one defined by ContactEntity.
                For i = 0 To fields.Length - 1
                    If propertyNames.Contains(fields(i)) Then
                        col = New DataColumn(fields(i))
                        dt.Columns.Add(col)

                        ' Track the field's index, so we know which ones to add data for below.
                        ' This way any fields other than those named by ContactEntity will be ignored.  
                        fieldIndices.Add(i)
                    End If
                Next i
            End If

            ' Add data from each row to data table where it matches column name.
            Dim row As DataRow = Nothing
            Do While Not csvFile.EndOfData
                ' Get the current row from the csv file.
                Dim currentRow() As String = csvFile.ReadFields

                ' Create a new row in the data table.
                row = dt.NewRow

                ' Copy the data from the csv to the data table.
                For Each index In fieldIndices
                    row(index) = currentRow(index)
                Next index

                ' Add the row.
                dt.Rows.Add(row)
            Loop
        End Using

        ' Insert values from the data table into a Windows Azure table.
        Try
            DataLayer.BulkInsertContacts(dt)

            ' Redirect to main page.
            Response.Redirect("Default.aspx")
        Catch e As ApplicationException
            Me.lblInfo.Text = "Error importing csv file: " & e.Message
            Me.lblInfo.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub
End Class
