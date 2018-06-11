Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports DocumentFormat.OpenXml

Module Module1

  Sub Main()
    Dim result = WDRetrieveTOC("C:\Temp\TOC.docx")
    Console.WriteLine(result.Value)
  End Sub

  ' Given a document, return its table of contents, if it exists, as an XElement.
  Public Function WDRetrieveTOC(ByVal fileName As String) As XElement
    Dim TOC As XElement = Nothing

    Using document = WordprocessingDocument.Open(fileName, False)
      Dim doc = document.MainDocumentPart.Document

      Dim block As OpenXmlElement = _
        doc.Descendants(Of DocPartGallery)().
        Where(Function(b) b.Val.HasValue AndAlso
                (b.Val.Value = "Table of Contents")).FirstOrDefault()
      If block IsNot Nothing Then
        ' Back up to the enclosing SdtBlock and return that XML.
        Do While (block IsNot Nothing) AndAlso (Not TypeOf block Is SdtBlock)
          block = block.Parent
        Loop
        TOC = New XElement("TOC", block.OuterXml)
      End If
    End Using
    Return TOC
  End Function

End Module
