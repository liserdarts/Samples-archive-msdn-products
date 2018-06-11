﻿Option Strict On

Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Office.CustomUI

Module Module1

    Const SAMPLEXML As String = "C:\Samples\CustomUI.xml"
    Const DEMOFILE As String = "C:\Samples\XLCustomUI.xlsm"

  Sub Main()
    Dim content As String = System.IO.File.OpenText(SAMPLEXML).ReadToEnd()
    XLAddCustomUI(DEMOFILE, content)
  End Sub


  Public Sub XLAddCustomUI(ByVal fileName As String, ByVal customUIContent As String)
    ' Add a custom UI part to the document.
    ' Use this sample XML to test:

    '<customUI xmlns="http://schemas.microsoft.com/office/2006/01/customui">
    '    <ribbon>
    '        <tabs>
    '            <tab idMso="TabAddIns">
    '                <group id="Group1" label="Group1">
    '                    <button id="Button1" label="Button1" showImage="false" onAction="SampleMacro"/>
    '                </group>
    '            </tab>
    '        </tabs>
    '    </ribbon>
    '</customUI>

    ' In the sample XLSM file, create a module and create a procedure named SampleMacro, using
    ' this signature:
    ' Public Sub SampleMacro(control As IRibbonControl)
    ' Add some code, then save and close the XLSM file. Run this
    ' example to add a button to the Add-Ins tab that calls the macro, given the 
    ' XML content above in the XLCustomUI.xml file.

    Using document As SpreadsheetDocument = SpreadsheetDocument.Open(fileName, True)
      ' You can only have a single ribbon extensibility part.
      ' If the part doesn't exist, add it.
      Dim part = document.RibbonExtensibilityPart
      If part Is Nothing Then
        part = document.AddRibbonExtensibilityPart
      End If
      part.CustomUI = New CustomUI(customUIContent)
      part.CustomUI.Save()
    End Using
  End Sub

End Module