Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.Linq
Imports System.Linq

Partial Public Class VisualWebPart1UserControl
    Inherits UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim thisWeb As SPWeb = SPContext.Current.Web
        Using ctx As EntitiesDataContext = New EntitiesDataContext("http://intranet.contoso.com")
            Try
                Dim query = From interviews In ctx.Interviews _
                            Select interviews.Title, interviews.Candidate.Name, interviews.Candidate.HomeCity, interviews.InterviewerImnName
                For Each Item In query
                    Dim interviewTreeNode As TreeNode = New TreeNode("Interview Details:" + Item.Title, Nothing, Nothing, thisWeb.Lists("Interviews").DefaultViewUrl, "_self")
                    Dim applicantTreeNode As TreeNode = New TreeNode("Applicant:" + Item.Name, Nothing, Nothing, thisWeb.Lists("Candidates").DefaultViewUrl, "_self")
                    Dim homecityTreeNode As TreeNode = New TreeNode("Home City:" + Item.HomeCity, Nothing, Nothing, thisWeb.Lists("Candidates").DefaultViewUrl, "_self")
                    Dim interviewerTreeNode As TreeNode = New TreeNode("Interviewer:" + Item.InterviewerImnName, Nothing, Nothing, thisWeb.Lists("Interviews").DefaultViewUrl, "_self")

                    interviewTreeNode.ChildNodes.Add(applicantTreeNode)
                    interviewTreeNode.ChildNodes.Add(homecityTreeNode)
                    interviewTreeNode.ChildNodes.Add(interviewerTreeNode)

                    TreeView1.Nodes.Add(interviewTreeNode)
                Next
            Catch ex As Exception
                TreeView1.Nodes.Add(New TreeNode("err" + ex.Message))
            End Try
        End Using
    End Sub

End Class
