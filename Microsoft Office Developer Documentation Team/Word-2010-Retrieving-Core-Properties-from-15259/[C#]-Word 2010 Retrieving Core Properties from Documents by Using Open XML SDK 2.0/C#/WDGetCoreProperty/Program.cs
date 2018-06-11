using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace WDGetCoreProperty
{
  class Program
  {

    const string FILENAME = @"C:\Samples\DocumentProperties.docx";
    static void Main(string[] args)
    {
      using (WordprocessingDocument document = WordprocessingDocument.Open(FILENAME, false))
      {
        var props = document.PackageProperties;
        Console.WriteLine("Creator = " + props.Creator);
        Console.WriteLine("Created = " + props.Created);
        Console.WriteLine("Title = " + props.Title);
      }
    }
  }
}

