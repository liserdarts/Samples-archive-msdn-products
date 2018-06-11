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

'NOTE: When changing the namespace; please update XmlnsDefinitionAttribute in AssemblyInfo.vb
Public Class Workflow1
    Inherits SequentialWorkflowActivity
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Dim bIsWorkflowPending As Boolean = True

    Public workflowProperties As SPWorkflowActivationProperties = New Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties

    Private Sub onWorkflowActivated(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ExternalDataEventArgs)
        CheckStatus()
    End Sub

    Private Sub isWorkflowPending(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ConditionalEventArgs)
        e.Result = bIsWorkflowPending
    End Sub

    Private Sub onWorkflowItemChanged(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ExternalDataEventArgs)
        CheckStatus()
    End Sub

    Private Sub CheckStatus()
        If CStr(workflowProperties.Item("Document Status")) = "Review Complete" Then
            bIsWorkflowPending = False
        End If
    End Sub
End Class




