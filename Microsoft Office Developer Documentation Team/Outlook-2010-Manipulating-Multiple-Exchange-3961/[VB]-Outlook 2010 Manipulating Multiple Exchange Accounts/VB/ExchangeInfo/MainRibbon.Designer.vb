Partial Class MainRibbon
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
    Me.Group1 = Me.Factory.CreateRibbonGroup
    Me.btnGetInfo = Me.Factory.CreateRibbonButton
    Me.btnCreateMail = Me.Factory.CreateRibbonButton
    Me.Tab1.SuspendLayout()
    Me.Group1.SuspendLayout()
    '
    'Tab1
    '
    Me.Tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office
    Me.Tab1.Groups.Add(Me.Group1)
    Me.Tab1.Label = "TabAddIns"
    Me.Tab1.Name = "Tab1"
    '
    'Group1
    '
    Me.Group1.Items.Add(Me.btnGetInfo)
    Me.Group1.Items.Add(Me.btnCreateMail)
    Me.Group1.Label = "Exchange Info"
    Me.Group1.Name = "Group1"
    '
    'btnGetInfo
    '
    Me.btnGetInfo.Label = "Get Information"
    Me.btnGetInfo.Name = "btnGetInfo"
    '
    'btnCreateMail
    '
    Me.btnCreateMail.Label = "Create Mail"
    Me.btnCreateMail.Name = "btnCreateMail"
    '
    'MainRibbon
    '
    Me.Name = "MainRibbon"
    Me.RibbonType = "Microsoft.Outlook.Explorer"
    Me.Tabs.Add(Me.Tab1)
    Me.Tab1.ResumeLayout(False)
    Me.Tab1.PerformLayout()
    Me.Group1.ResumeLayout(False)
    Me.Group1.PerformLayout()

  End Sub
  Friend WithEvents btnCreateMail As Microsoft.Office.Tools.Ribbon.RibbonButton
  Friend WithEvents Tab1 As Microsoft.Office.Tools.Ribbon.RibbonTab
  Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
  Friend WithEvents btnGetInfo As Microsoft.Office.Tools.Ribbon.RibbonButton

End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property Main() As MainRibbon
        Get
            Return Me.GetRibbon(Of MainRibbon)()
        End Get
    End Property
End Class
