Public Class ThisAddIn

  Private Sub ThisAddIn_Startup() Handles Me.Startup
    Me.VstoSmartTags.Add(New TemperatureSmartTag().Base)
  End Sub

  Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown

  End Sub

End Class
