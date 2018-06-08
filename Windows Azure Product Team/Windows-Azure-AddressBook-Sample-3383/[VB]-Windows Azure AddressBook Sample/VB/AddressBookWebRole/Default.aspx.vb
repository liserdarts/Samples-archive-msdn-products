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

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Public Shared stringAll As String = "All"

    ' Create button links for filtering alphabetically
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        ButtonRepeater.DataSource = New String() {stringAll}.Concat(Enumerable.Range(0, 26).Select(Function(i) (ChrW(AscW("A"c) + i)).ToString))
        ButtonRepeater.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ' Indicate that all contacts are displayed in list box. 
            Me.ViewState("filter") = stringAll

            ' Fill the list box for the first time.
            Me.FillListBox(Nothing)
        End If
    End Sub

    ' Event handler for click to any of the filter buttons.
    Public Sub ClickFilter(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim text = e.CommandArgument.ToString
        Me.FillListBox(text)
        ViewState("filter") = text
    End Sub

    ' Update the list box after an item is inserted.
    Protected Sub frmDetail_ItemInserted(ByVal sender As Object, ByVal e As FormViewInsertedEventArgs)
        Me.FillListBoxPreserveFilter()
    End Sub

    ' Update the list box after an item is updated, selecting the updated record.
    Protected Sub frmDetail_ItemUpdated(ByVal sender As Object, ByVal e As FormViewUpdatedEventArgs)
        Me.FillListBoxPreserveFilter()

        ' Set the selected item to be the updated record, if possible.
        If Nothing IsNot lstContactNames.Items.FindByValue(frmDetail.DataKey.Value.ToString) Then
            lstContactNames.SelectedValue = frmDetail.DataKey.Value.ToString
        End If
    End Sub

    ' Update the list box after an item is deleted.
    Protected Sub frmDetail_ItemDeleted(ByVal sender As Object, ByVal e As FormViewDeletedEventArgs)
        Me.FillListBoxPreserveFilter()
    End Sub

    ' Display the page to import a csv file.
    Protected Sub btnImportPage_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("Import.aspx")
    End Sub

    ' Fill the list box using data from the table.
    Private Sub FillListBox(ByVal prefix As String)
        ' Clear the existing data.
        lstContactNames.Items.Clear()

        Try
            ' Retrieve list of contacts.
            Dim list As List(Of ContactEntity) = DataLayer.GetContactsByPrefix(If(prefix = stringAll, Nothing, prefix))

            ' Add contacts to list box, using FirstName and LastName.
            For Each entity In list
                Dim nameItem As New ListItem
                nameItem.Text = entity.FirstName & " " & entity.LastName
                nameItem.Value = entity.RowKey
                Me.lstContactNames.Items.Add(nameItem)
            Next entity

            If list.Count > 0 Then
                ' Set first selection.
                Me.lstContactNames.SelectedIndex = 0
                Me.lstContactNames.Enabled = True
                Me.frmDetail.Visible = True
            Else
                ' Indicate that there's no data. 
                Dim noDataItem As New ListItem
                noDataItem.Text = "<No entries>"
                noDataItem.Value = "0"
                Me.lstContactNames.Items.Add(noDataItem)
                Me.lstContactNames.Enabled = False
                Me.frmDetail.Visible = False
            End If
        Catch e As Exception
            Dim errorItem As New ListItem
            errorItem.Text = "<Error: >" & e.Message
            errorItem.Value = "0"
            Me.lstContactNames.Items.Add(errorItem)
            Me.lstContactNames.Enabled = False
            Me.frmDetail.Visible = False
        End Try
    End Sub

    ' Fill the list box, maintaining the same filter if a filter has been specified.
    Private Sub FillListBoxPreserveFilter()
        Me.FillListBox(TryCast(Me.ViewState("filter"), String))
    End Sub
End Class
