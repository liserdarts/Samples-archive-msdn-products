Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Workflow.ComponentModel.Compiler
Imports System.Workflow.ComponentModel.Serialization
Imports System.Workflow.ComponentModel
Imports System.Workflow.ComponentModel.Design
Imports System.Workflow.Runtime
Imports System.Workflow.Activities
Imports System.Workflow.Activities.Rules
Imports Microsoft.SharePoint.Workflow
Imports Microsoft.SharePoint.WorkflowActions

Public Class Workflow1
    Inherits StateMachineWorkflowActivity

    Public workflowProperties As New SPWorkflowActivationProperties

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Public createTask1_TaskId1 As System.Guid = Nothing
    Public createTask1_TaskProperties1 As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties

    Private Sub createTask1_MethodInvoking(ByVal sender As System.Object, ByVal e As System.EventArgs)
        createTask1_TaskId1 = Guid.NewGuid()
        createTask1_TaskProperties1.Title = "Finish Document"
        createTask1_TaskProperties1.AssignedTo = "CONTOSO\sanjays"
        createTask1_TaskProperties1.DueDate = Date.Now.AddDays(1.0)
    End Sub
    Public onTaskChanged1_AfterProperties1 As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties
    Public onTaskChanged1_BeforeProperties1 As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties

    Private Sub onTaskChanged1_Invoked(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ExternalDataEventArgs)
        onTaskChanged1_AfterProperties1 = onTaskChanged1.AfterProperties
        onTaskChanged1_BeforeProperties1 = onTaskChanged1.BeforeProperties
    End Sub
    Private Sub ReadyForReview(ByVal sender As Object, ByVal e As ConditionalEventArgs)
        If onTaskChanged1_AfterProperties1.PercentComplete = 1.0 Then
            e.Result = True
        Else
            e.Result = False
        End If
    End Sub
    Public createReviewTask_TaskId1 As System.Guid = Nothing
    Public createReviewTask_TaskProperties1 As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties

    Private Sub createReviewTask_MethodInvoking(ByVal sender As System.Object, ByVal e As System.EventArgs)
        createReviewTask_TaskId1 = Guid.NewGuid()
        createReviewTask_TaskProperties1.Title = "Review Document"
        createReviewTask_TaskProperties1.AssignedTo = "CONTOSO\andyj"
        createReviewTask_TaskProperties1.DueDate = Date.Now.AddDays(1.0)
    End Sub
    Public onTaskChanged2_AfterProperties1 As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties
    Public onTaskChanged2_BeforeProperties1 As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties

    Private Sub onTaskChanged2_Invoked(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ExternalDataEventArgs)
        onTaskChanged2_AfterProperties1 = onTaskChanged2.AfterProperties
        onTaskChanged2_BeforeProperties1 = onTaskChanged2.BeforeProperties
    End Sub
    Private Sub ReviewFinished(ByVal sender As Object, ByVal e As ConditionalEventArgs)
        If onTaskChanged2_AfterProperties1.PercentComplete = 1.0 Then
            e.Result = True
        Else
            e.Result = False
        End If
    End Sub
    Private Sub DocApproved(ByVal sender As Object, ByVal e As ConditionalEventArgs)
        If onTaskChanged2_AfterProperties1.Description = "<DIV>Approved</DIV>" Then
            e.Result = True
        Else
            e.Result = False
        End If
    End Sub

End Class