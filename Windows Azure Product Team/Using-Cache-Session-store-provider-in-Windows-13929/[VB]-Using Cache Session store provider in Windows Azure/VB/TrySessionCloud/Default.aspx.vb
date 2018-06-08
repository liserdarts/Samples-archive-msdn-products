Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace WebRole1
	Partial Public Class _Default
		Inherits System.Web.UI.Page
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		End Sub

		Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)
			If (Not String.IsNullOrEmpty(Me.TextBox1.Text)) Then
				Session.Add(Guid.NewGuid().ToString(), Me.TextBox1.Text)
			End If

			For Each key As String In Session.Keys
				Dim cell As New TableCell()
				cell.Text = CStr(Session(key))

				Dim row As New TableRow()
				row.Cells.Add(cell)

				Me.Table1.Rows.Add(row)
			Next key

		End Sub
	End Class
End Namespace
