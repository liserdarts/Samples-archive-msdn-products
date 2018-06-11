Option Strict On
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Module Module1

  Sub Main()
    Dim result = XLGetDefinedNames("C:\temp\definednames.xlsx")
    For Each dn In result
      Console.WriteLine("{0}: {1}", dn.Key, dn.Value)
    Next
  End Sub


  Public Function XLGetDefinedNames(
    ByVal fileName As String) As Dictionary(Of String, String)

    ' Given a workbook name, return a dictionary of defined names.
    ' The pairs include the range name and a string representing the range.

    Dim returnValue As New Dictionary(Of String, String)
    Using document As SpreadsheetDocument =
      SpreadsheetDocument.Open(fileName, False)
      Dim wbPart As WorkbookPart = document.WorkbookPart

      Dim definedNames As DefinedNames = wbPart.Workbook.DefinedNames
      If definedNames IsNot Nothing Then
        For Each dn As DefinedName In definedNames
          returnValue.Add(dn.Name.Value, dn.Text)
        Next
      End If
    End Using
    Return returnValue
  End Function

End Module
