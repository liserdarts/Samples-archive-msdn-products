Imports System.IO
Imports System.Xml
Imports DocumentFormat.OpenXml.Packaging

Module Module1
  Private Const fromDoc As String = "C:\Temp\StylesFrom.docx"
  Private Const toDoc As String = "C:\Temp\StylesTo.docx"

  Sub Main()
    ' Handle the styles part.
    Dim node = WDExtractStyles(fromDoc, False)
    If node IsNot Nothing Then
      WDReplaceStyles(toDoc, node, False)
    End If

    ' Handle the stylesWithEffects part. To fully support 
    ' round-tripping from Word 2010 to Word 2007, you should 
    ' replace this part, as well.
    node = WDExtractStyles(fromDoc, True)
    If node IsNot Nothing Then
      WDReplaceStyles(toDoc, node, True)
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

          styles = XDocument.Load(reader)
        End Using
      End If
    End Using
    Return styles
  End Function

  ' Given a file and an XDocument containing a full styles or stylesWithEffects part,  
  ' replace the styles in the document with the new styles.
  Public Sub WDReplaceStyles(
    ByVal fileName As String, ByVal newStyles As XDocument,
    Optional ByVal setStylesWithEffectsPart As Boolean = True)

    Using document = WordprocessingDocument.Open(fileName, True)
      Dim docPart = document.MainDocumentPart

      Dim stylesPart As StylesPart = Nothing

      If setStylesWithEffectsPart Then
        stylesPart = docPart.StylesWithEffectsPart
      Else
        stylesPart = docPart.StyleDefinitionsPart
      End If
      If stylesPart IsNot Nothing Then
        newStyles.Save(New StreamWriter(
          stylesPart.GetStream(FileMode.Create, FileAccess.Write)))
      End If
    End Using
  End Sub

End Module
