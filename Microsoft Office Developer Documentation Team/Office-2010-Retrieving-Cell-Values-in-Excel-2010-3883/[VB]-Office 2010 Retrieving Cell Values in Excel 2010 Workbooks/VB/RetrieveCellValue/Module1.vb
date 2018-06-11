Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Linq

Module Module1

  Sub Main()
    Const fileName As String = "C:\temp\GetCellValue.xlsx"
    Dim value As String = XLGetCellValue(fileName, "Sheet1", "A1")
    Console.WriteLine(value)
    value = XLGetCellValue(fileName, "Sheet1", "A2")
    Console.WriteLine(value)
    value = XLGetCellValue(fileName, "Sheet1", "A3")
    Console.WriteLine(value)
    value = XLGetCellValue(fileName, "Sheet1", "A4")
    Console.WriteLine(value)

  End Sub

  Public Function XLGetCellValue(ByVal fileName As String,
    ByVal sheetName As String, ByVal addressName As String) As String
    Dim value As String = Nothing

    Using document As SpreadsheetDocument =
      SpreadsheetDocument.Open(fileName, False)

      Dim wbPart As WorkbookPart = document.WorkbookPart

      ' Find the sheet with the supplied name, and then use that Sheet object
      ' to retrieve a reference to the appropriate worksheet.
      Dim theSheet As Sheet = wbPart.Workbook.Descendants(Of Sheet)().
        Where(Function(s) s.Name = sheetName).FirstOrDefault()

      If theSheet Is Nothing Then
        Throw New ArgumentException("sheetName")
      End If

      ' Retrieve a reference to the worksheet part, and then use its Worksheet property to get 
      ' a reference to the cell whose address matches the address you've supplied:
      Dim wsPart As WorksheetPart =
        CType(wbPart.GetPartById(theSheet.Id), WorksheetPart)
      Dim theCell As Cell = wsPart.Worksheet.Descendants(Of Cell).
        Where(Function(c) c.CellReference = addressName).FirstOrDefault

      ' If the cell doesn't exist, return an empty string.
      If theCell IsNot Nothing Then
        value = theCell.InnerText

        ' If the cell represents an numeric value, you're done. 
        ' For dates, this code returns the serialized value that 
        ' represents the date. The code handles strings and booleans
        ' individually. For   shared strings, the code looks up the corresponding
        ' value in the shared string table. For booleans, the code converts 
        ' the value into the words TRUE or FALSE.
        If theCell.DataType IsNot Nothing Then
          Select Case theCell.DataType.Value
            Case CellValues.SharedString
              ' For shared strings, look up the value in the shared strings table.
              Dim stringTable = wbPart.
                GetPartsOfType(Of SharedStringTablePart).FirstOrDefault()
              ' If the shared string table is missing, something's wrong.
              ' Just return the index that you found in the cell.
              ' Otherwise, look up the correct text in the table.
              If stringTable IsNot Nothing Then
                value = stringTable.SharedStringTable.
                  ElementAt(Integer.Parse(value)).InnerText
              End If
            Case CellValues.Boolean
              Select Case value
                Case "0"
                  value = "FALSE"
                Case Else
                  value = "TRUE"
              End Select
          End Select
        End If
      End If
    End Using
    Return value
  End Function
End Module
