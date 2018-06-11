Imports System.Globalization
Imports Excel = Microsoft.Office.Tools.Excel
Imports System.Text.RegularExpressions
Imports MSExcel = Microsoft.Office.Interop.Excel

Public Class TemperatureSmartTag
  Implements Excel.ISmartTagExtension

  Const TEMPERATURE_CELSIUS As String = "C"
  Dim WithEvents temperatureAction As Excel.Action
  Private smartTagDemo As Excel.SmartTag

  Public Sub New()
    Me.smartTagDemo = Globals.Factory.CreateSmartTag(
      "http://www.microsoft.com#TemperatureSmartTag", "Temperature", Me)
    ' Add a regular expression for the Temperature SmartTag recognition.
    smartTagDemo.Expressions.Add(New Regex(
      "\s*(?'number'[-+]?[0-9]+)°?\s?(?'temperature'(F|C|f|c))\s*"))
    ' Create an instance of the temperature action.
    temperatureAction = Globals.Factory.CreateAction("Convert")
    ' Add the action to the Actions array for the smart tag.
    smartTagDemo.Actions = New Excel.Action() {temperatureAction}
  End Sub

  Public Sub TemperatureAction_BeforeCaptionShow(ByVal sender As Object, ByVal e As ActionEventArgs) Handles temperatureAction.BeforeCaptionShow
    Dim temperature As String = e.Properties.Read(
      "temperature").ToUpper(CultureInfo.CurrentCulture)
    If temperature = TEMPERATURE_CELSIUS Then
      CType(sender, Excel.Action).Caption = "Convert to Fahrenheit"
    Else
      CType(sender, Excel.Action).Caption = "Convert to Celsius"
    End If
  End Sub

  Public Sub TemperatureAction_Click(ByVal sender As Object, ByVal e As ActionEventArgs) Handles temperatureAction.Click
    Dim fahrenheit As Double
    Dim celsius As Double
    'Read the temperature type and value from the smart tag properties.
    Dim value As String = e.Properties.Read("number")
    Dim temperature As String = e.Properties.Read(
      "temperature").ToUpper(CultureInfo.CurrentCulture)
    Dim resultText As String

    If temperature = TEMPERATURE_CELSIUS Then
      celsius = System.Convert.ToDouble(
        value, NumberFormatInfo.CurrentInfo)
      fahrenheit = Math.Round((celsius * 9 / 5) + 32)
      resultText = String.Format(" {0}° F ",
        fahrenheit.ToString("0", NumberFormatInfo.CurrentInfo))
    Else
      fahrenheit = System.Convert.ToDouble(
        value, NumberFormatInfo.CurrentInfo)
      celsius = Math.Round((fahrenheit - 32) * 5 / 9)
      resultText = String.Format(" {0}° C ",
        celsius.ToString("0", NumberFormatInfo.CurrentInfo))
    End If
    e.Range.Value = resultText
  End Sub

  Public ReadOnly Property Base As Excel.SmartTag
    Get
      Return smartTagDemo
    End Get
  End Property

  Public ReadOnly Property ExtensionBase As Object Implements Microsoft.Office.Tools.IExtension.ExtensionBase
    Get
      Return smartTagDemo
    End Get
  End Property

  Public Sub Recognize(ByVal text As String, ByVal site As Microsoft.Office.Interop.SmartTag.ISmartTagRecognizerSite, ByVal tokenList As Microsoft.Office.Interop.SmartTag.ISmartTagTokenList, ByVal context As Microsoft.Office.Tools.Excel.SmartTagRecognizeContext) Implements Microsoft.Office.Tools.Excel.ISmartTagExtension.Recognize

  End Sub
End Class
