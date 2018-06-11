Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml
Imports System.IO

Module Module1

  Sub Main()
    WDConvertDOCMtoDOCX("C:\temp\WithMacros.docm")
  End Sub

  ' Given a DOCM file (with macro storage), remove the VBA 
  ' project, reset the document type, and save the document with a new name.
  Public Sub WDConvertDOCMtoDOCX(ByVal fileName As String)
    Dim fileChanged As Boolean = False

    Using document As WordprocessingDocument =
      WordprocessingDocument.Open(fileName, True)

      Dim docPart = document.MainDocumentPart

      ' Look for the VBA part. If it's there, delete it.
      Dim vbaPart = docPart.VbaProjectPart
      If vbaPart IsNot Nothing Then
        docPart.DeletePart(vbaPart)

        docPart.Document.Save()

        ' Change the document type so that it no 
        ' longer thinks it is macro-enabled.
        document.ChangeDocumentType(WordprocessingDocumentType.Document)

        ' Track that the document has been changed.
        fileChanged = True
      End If
    End Using

    ' If anything goes wrong in this file handling,
    ' the code will raise an exception back to the caller.
    If fileChanged Then
      Dim newFileName = Path.ChangeExtension(fileName, ".docx")
      If File.Exists(newFileName) Then
        File.Delete(newFileName)
      End If
      File.Move(fileName, newFileName)
    End If
  End Sub

End Module
