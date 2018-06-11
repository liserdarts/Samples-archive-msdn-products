Partial Class Ribbon1
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
    Dim RibbonDropDownItemImpl1 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
    Dim RibbonDropDownItemImpl2 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
    Me.TabCustom = Me.Factory.CreateRibbonTab
    Me.temperatureGroup = Me.Factory.CreateRibbonGroup
    Me.TemperatureEditBox = Me.Factory.CreateRibbonEditBox
    Me.ScaleDropDown = Me.Factory.CreateRibbonDropDown
    Me.InsertButton = Me.Factory.CreateRibbonButton
    Me.TabCustom.SuspendLayout()
    Me.temperatureGroup.SuspendLayout()
    '
    'TabCustom
    '
    Me.TabCustom.Groups.Add(Me.temperatureGroup)
    Me.TabCustom.Label = "Custom"
    Me.TabCustom.Name = "TabCustom"
    '
    'temperatureGroup
    '
    Me.temperatureGroup.Items.Add(Me.TemperatureEditBox)
    Me.temperatureGroup.Items.Add(Me.ScaleDropDown)
    Me.temperatureGroup.Items.Add(Me.InsertButton)
    Me.temperatureGroup.Label = "Temperature"
    Me.temperatureGroup.Name = "temperatureGroup"
    '
    'TemperatureEditBox
    '
    Me.TemperatureEditBox.Label = "Temperature"
    Me.TemperatureEditBox.Name = "TemperatureEditBox"
    Me.TemperatureEditBox.Text = Nothing
    '
    'ScaleDropDown
    '
    RibbonDropDownItemImpl1.Label = "Centigrade"
    RibbonDropDownItemImpl2.Label = "Fahrenheit"
    Me.ScaleDropDown.Items.Add(RibbonDropDownItemImpl1)
    Me.ScaleDropDown.Items.Add(RibbonDropDownItemImpl2)
    Me.ScaleDropDown.Label = "Scale:"
    Me.ScaleDropDown.Name = "ScaleDropDown"
    '
    'InsertButton
    '
    Me.InsertButton.Enabled = False
    Me.InsertButton.Label = "Insert Temperature"
    Me.InsertButton.Name = "InsertButton"
    '
    'Ribbon1
    '
    Me.Name = "Ribbon1"
    Me.RibbonType = "Microsoft.Word.Document"
    Me.Tabs.Add(Me.TabCustom)
    Me.TabCustom.ResumeLayout(False)
    Me.TabCustom.PerformLayout()
    Me.temperatureGroup.ResumeLayout(False)
    Me.temperatureGroup.PerformLayout()

  End Sub
  Friend WithEvents TabCustom As Microsoft.Office.Tools.Ribbon.RibbonTab
  Friend WithEvents temperatureGroup As Microsoft.Office.Tools.Ribbon.RibbonGroup
  Friend WithEvents TemperatureEditBox As Microsoft.Office.Tools.Ribbon.RibbonEditBox
  Friend WithEvents ScaleDropDown As Microsoft.Office.Tools.Ribbon.RibbonDropDown
  Friend WithEvents InsertButton As Microsoft.Office.Tools.Ribbon.RibbonButton

End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property Ribbon1() As Ribbon1
        Get
            Return Me.GetRibbon(Of Ribbon1)()
        End Get
    End Property
End Class
