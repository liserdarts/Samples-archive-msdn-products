﻿using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Packaging;

namespace XLAddCustomUI
{


  class Program
  {
    const string SAMPLEXML = @"C:\Samples\CustomUI.xml";
    const string DEMOFILE = @"C:\Samples\XLCustomUI.xlsm";

    static void Main(string[] args)
    {
      string content = System.IO.File.OpenText(SAMPLEXML).ReadToEnd();
      XLAddCustomUI(DEMOFILE, content);

    }

    // Use these using statements:
    //using DocumentFormat.OpenXml.Packaging;
    //using DocumentFormat.OpenXml.Office.CustomUI;

    static public void XLAddCustomUI(string fileName, string customUIContent)
    {
      // Add a custom UI part to the document.
      // Use this sample XML to test:
      //<customUI xmlns="http://schemas.microsoft.com/office/2006/01/customui">
      //    <ribbon>
      //        <tabs>
      //            <tab idMso="TabAddIns">
      //                <group id="Group1" label="Group1">
      //                    <button id="Button1" label="Button1" showImage="false" onAction="SampleMacro"/>
      //                </group>
      //            </tab>
      //        </tabs>
      //    </ribbon>
      //</customUI>

    // In the sample XLSM file, create a module and create a procedure named SampleMacro, using
    // this signature:
    // Public Sub SampleMacro(control As IRibbonControl)
    // Add some code, then save and close the XLSM file. Run this
    // example to add a button to the Add-Ins tab that calls the macro, given the 
    // XML content above in the XLCustomUI.xml file.
      
      using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, true))
      {
        // You can only have a single ribbon extensibility part.
        // If the part doesn't exist, create it.
        var part = document.RibbonExtensibilityPart;
        if (part == null)
        {
          part = document.AddRibbonExtensibilityPart();
        }
        part.CustomUI = new CustomUI(customUIContent);
        part.CustomUI.Save();
      }
    }
  }
}
