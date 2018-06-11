Imports DocumentFormat.OpenXml.Wordprocessing
Imports DocumentFormat.OpenXml.Packaging

Module Module1

  Sub Main()
    WDDeleteHeadersAndFooters("C:\Temp\Headers.docx")
  End Sub

  ' To delete headers and footers in a document.
  Public Sub WDDeleteHeadersAndFooters(ByVal docName As String)
    ' Given a document name, remove all headers and footers.
    Using wdDoc = WordprocessingDocument.Open(docName, True)
      Dim docPart = wdDoc.MainDocumentPart
      If (docPart.HeaderParts.Count > 0) Or
        (docPart.FooterParts.Count > 0) Then

        docPart.DeleteParts(docPart.HeaderParts)
        docPart.DeleteParts(docPart.FooterParts)

        ' Remove references to the headers and footers.
        Dim doc As Document = docPart.Document

        ' Remove references to the headers and footers.
        ' This requires digging into the XML content
        ' of the document:

        Dim headers = _
          doc.Descendants(Of HeaderReference).ToList()
        For Each header In headers
          header.Remove()
        Next

        Dim footers = _
          doc.Descendants(Of FooterReference).ToList()
        For Each footer In footers
          footer.Remove()
        Next

        doc.Save()
      End If
    End Using
  End Sub

End Module
