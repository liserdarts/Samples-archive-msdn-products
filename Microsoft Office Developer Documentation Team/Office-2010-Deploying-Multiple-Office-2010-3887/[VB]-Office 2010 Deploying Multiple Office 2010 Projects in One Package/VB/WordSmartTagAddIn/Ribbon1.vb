Imports Microsoft.Office.Tools.Ribbon

Public Class Ribbon1

  Private Sub Ribbon1_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

  End Sub

  Private Sub TemperatureEditBox_TextChanged(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles TemperatureEditBox.TextChanged
    InsertButton.Enabled = (TemperatureEditBox.Text.Length > 0)
  End Sub

  Private Sub InsertButton_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles InsertButton.Click
    Dim temp As String = TemperatureEditBox.Text
    Select Case ScaleDropDown.SelectedItem.Label
      Case "Fahrenheit"
        temp &= "°F"
      Case "Centigrade"
        temp &= "°C"
    End Select
    Dim sel = Globals.ThisAddIn.Application.Selection
    sel.InsertAfter(temp)
    sel.MoveRight()
  End Sub

End Class
