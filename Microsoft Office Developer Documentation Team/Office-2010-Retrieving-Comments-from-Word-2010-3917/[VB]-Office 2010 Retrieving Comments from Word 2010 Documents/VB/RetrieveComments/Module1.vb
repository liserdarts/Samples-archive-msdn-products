Imports DocumentFormat.OpenXml.Packaging
Imports System.IO
Imports System.Xml

Module Module1

  ' Alter this path as necessary for your own demonstration:
  Private Const DEMOPATH As String = "I:\Samples\comments.docx"

  Sub Main()
    ' Retrieve an XDocument containing comments:
    Dim comments = WDRetrieveComments(DEMOPATH)
    If comments IsNot Nothing Then
      Console.WriteLine(comments.ToString())
    End If
  End Sub

  ' Retrieve the comments content from the document, as an XDocument.
  Public Function WDRetrieveComments(ByVal fileName As String) As XDocument
    Dim comments As XDocument = Nothing

    Using document = WordprocessingDocument.Open(fileName, False)
      ' Retrieve the document part:
      Dim docPart = document.MainDocumentPart
      If docPart IsNot Nothing Then
        ' Retrieve the comments part:
        Dim commentsPart = docPart.WordprocessingCommentsPart
        If commentsPart IsNot Nothing Then
          ' Load an XElement with the comments content:
          Using stm As Stream = commentsPart.GetStream(FileMode.Open, FileAccess.Read)
            comments = XDocument.Load(XmlReader.Create(stm))
          End Using
        End If
      End If
    End Using
    Return comments
  End Function
End Module
