Partial Class SolutionModuleRibbon
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()> _
   Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'Component overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.Tab1 = Me.Factory.CreateRibbonTab
    Me.grpSM = Me.Factory.CreateRibbonGroup
    Me.btnCreateSM = Me.Factory.CreateRibbonButton
    Me.Tab1.SuspendLayout()
    Me.grpSM.SuspendLayout()
    '
    'Tab1
    '
    Me.Tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office
    Me.Tab1.Groups.Add(Me.grpSM)
    Me.Tab1.Label = "TabAddIns"
    Me.Tab1.Name = "Tab1"
    '
    'grpSM
    '
    Me.grpSM.Items.Add(Me.btnCreateSM)
    Me.grpSM.Label = "Custom App"
    Me.grpSM.Name = "grpSM"
    '
    'btnCreateSM
    '
    Me.btnCreateSM.Label = "Show It"
    Me.btnCreateSM.Name = "btnCreateSM"
    '
    'SolutionModuleRibbon
    '
    Me.Name = "SolutionModuleRibbon"
    Me.RibbonType = "Microsoft.Outlook.Explorer"
    Me.Tabs.Add(Me.Tab1)
    Me.Tab1.ResumeLayout(False)
    Me.Tab1.PerformLayout()
    Me.grpSM.ResumeLayout(False)
    Me.grpSM.PerformLayout()

  End Sub

  Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
  Friend WithEvents Button1 As Microsoft.Office.Tools.Ribbon.RibbonButton
  Friend WithEvents btnCreateSM As Microsoft.Office.Tools.Ribbon.RibbonButton
  Friend WithEvents grpSM As Microsoft.Office.Tools.Ribbon.RibbonGroup
  Friend WithEvents Tab1 As Microsoft.Office.Tools.Ribbon.RibbonTab
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property SolutionModuleRibbon() As SolutionModuleRibbon
        Get
            Return Me.GetRibbon(Of SolutionModuleRibbon)()
        End Get
    End Property
End Class
