Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.CustomProperties

Module Module1

  Const FILENAME As String = "C:\Samples\DocumentProperties.docx"
  Sub Main()
    DisplayProperty("Disposition")
    DisplayProperty("IsItSafe")
    DisplayProperty("Typist")
    DisplayProperty("FakeProperty")
  End Sub

  Private Sub DisplayProperty(propName As String)
    Console.WriteLine("{0} = {1}",
                      propName, WDGetCustomProperty(FILENAME, propName))
  End Sub

  Public Function WDGetCustomProperty(ByVal fileName As String, ByVal propertyName As String) As String
    ' Given a document name and a custom property, retrieve the value of the property.

    Dim returnValue As String = Nothing

    Using document = WordprocessingDocument.Open(fileName, False)
      Dim customProps = document.CustomFilePropertiesPart
      If customProps IsNot Nothing Then
        ' No custom properties? Nothing to return, in that case.
        Dim props = customProps.Properties
        If props IsNot Nothing Then
          ' This will trigger an exception is the property's Name property is null, but
          ' if that happens, the property is damaged, and probably should raise an exception.
          Dim prop = props. _
            Where(Function(p) CType(p, CustomDocumentProperty).Name.Value = propertyName).FirstOrDefault()
          ' Does the property exist? If so, get the return value.
          If prop IsNot Nothing Then
            returnValue = prop.InnerText
          End If
        End If
      End If
    End Using
    Return returnValue
  End Function


End Module
