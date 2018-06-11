Imports DocumentFormat.OpenXml.Packaging

Module Module1
  Private Const FILENAME As String = "C:\Samples\DocumentProperties.docx"

  Sub Main()
    Using document As WordprocessingDocument =
      WordprocessingDocument.Open(FILENAME, True)
      Dim props = document.PackageProperties

      Console.WriteLine("Creator = " & props.Creator)
      Console.WriteLine("Created = " & props.Created)
      Console.WriteLine("Title = " & props.Title)
    End Using
  End Sub
End Module
