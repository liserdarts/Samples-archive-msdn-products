Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.WebControls
Imports Microsoft.SharePoint.Utilities

Partial Public Class VisualWebPart1UserControl
    Inherits UserControl
    Dim query As SPQuery
    Dim thisWeb As SPWeb


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        thisWeb = SPContext.Current.Web
        taskView.List = thisWeb.Lists("Project Tasks")
        query = New SPQuery(taskView.List.DefaultView)
        query.ViewFields = "<FieldRef Name='Title' /><FieldRef Name='AssignedTo' /><FieldRef Name='DueDate' />"
        taskView.Query = query
    End Sub
    Sub filterDate_DateChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim camlQuery As String
        camlQuery = "<Where><Leq><FieldRef Name='DueDate' />" _
               + "<Value Type='DateTime'>" _
               + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTimeControl1.SelectedDate) _
               + "</Value></Leq></Where>"
        query.Query = camlQuery

    End Sub
End Class
