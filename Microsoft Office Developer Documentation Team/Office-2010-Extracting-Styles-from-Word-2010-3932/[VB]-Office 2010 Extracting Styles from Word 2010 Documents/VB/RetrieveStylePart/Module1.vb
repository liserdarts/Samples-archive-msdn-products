Imports DocumentFormat.OpenXml.Packaging
Imports System.IO
Imports System.Xml

Module Module1

  Sub Main()
    ' Retrieve the StylesWithEffects part. You could repeat
    ' this call, passing False in the second parameter, to retrieve
    ' the old Styles part.
        Dim styles = WDExtractStyles("C:\Temp\StylesFrom.docx", True)
        If styles IsNot Nothing Then
            Console.WriteLine(styles.ToString())
        End If
  End Sub

  ' Extract the styles or stylesWithEffects part from a document as an XDocument instance.
  Public Function WDExtractStyles(
    ByVal fileName As String,
    Optional ByVal getStylesWithEffectsPart As Boolean = True) As XDocument

    Dim styles As XDocument = Nothing

    Using document = WordprocessingDocument.Open(fileName, False)
      Dim docPart = document.MainDocumentPart
      Dim stylesPart As StylesPart = Nothing

      If getStylesWithEffectsPart Then
        stylesPart = docPart.StylesWithEffectsPart
      Else
        stylesPart = docPart.StyleDefinitionsPart
      End If
      If stylesPart IsNot Nothing Then
        Using reader = XmlNodeReader.Create(
          stylesPart.GetStream(FileMode.Open, FileAccess.Read))
          ' Create the XDocument:  
          styles = XDocument.Load(reader)
        End Using
      End If
    End Using
    Return styles
  End Function

End Module
