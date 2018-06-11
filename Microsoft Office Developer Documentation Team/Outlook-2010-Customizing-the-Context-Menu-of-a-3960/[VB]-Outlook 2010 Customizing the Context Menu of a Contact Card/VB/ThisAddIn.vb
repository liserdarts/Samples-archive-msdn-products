Public Class ThisAddIn

    Private Sub ThisAddIn_Startup() Handles Me.Startup

    End Sub

    Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown

    End Sub

  Protected Overrides Function CreateRibbonExtensibilityObject() As  _
    Microsoft.Office.Core.IRibbonExtensibility
    Return New ContactCardContextMenu()
  End Function
End Class
