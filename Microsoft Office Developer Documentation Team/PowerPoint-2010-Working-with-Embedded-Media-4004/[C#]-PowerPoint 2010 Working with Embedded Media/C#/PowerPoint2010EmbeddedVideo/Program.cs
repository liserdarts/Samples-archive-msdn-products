using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;

namespace PowerPoint2010EmbeddedVideo
{
  class Program
  {
    static void Main(string[] args)
    {
      //Declare variables to hold references to the PowerPoint objects.
      Application application = new Application();
      Presentation presentation;
      Master master;
      CustomLayout cl;
      Slide slide;
      Microsoft.Office.Interop.PowerPoint.Shape mediaFile;

      //Declare variable for the new presentation file.
      String presentationName = @"c:\temp\PowerPoint2010EmbeddedVideo";

      //This variable references the video file that will be embedded
      //  into the presentation.
      //It may need to be updated to reference a path and filename to 
      //  a video file located on your computer.
      String mediaFileName = @"C:\temp\Butterfly.wmv";

      try
      {
        //Create a new PowerPoint 2010 presentation.
        presentation =
          application.Presentations.Add(MsoTriState.msoFalse);

        //Add a new slide to the presentation.
        master = presentation.SlideMaster;
        cl = master.CustomLayouts[PpSlideLayout.ppLayoutChartAndText];
        slide = presentation.Slides.AddSlide(1, cl);

        //Add a title above the Video.
        slide.Shapes[1].TextFrame.TextRange.Text = "Butterfly";

        //Embed the video file into the slide.
        mediaFile = slide.Shapes.AddMediaObject2(
          FileName: mediaFileName,
          LinkToFile: MsoTriState.msoFalse,
          SaveWithDocument: MsoTriState.msoCTrue,
          Left: 90f,
          Top: 114f);

        //Resize the video to its original size.
        mediaFile.ScaleHeight(1f, MsoTriState.msoCTrue);
        mediaFile.ScaleWidth(1f, MsoTriState.msoCTrue);

        //Format to have a Beveled edge.
        mediaFile.ThreeD.BevelTopDepth = 8;
        mediaFile.ThreeD.BevelTopInset = 6;
        mediaFile.ThreeD.BevelBottomType =
          MsoBevelType.msoBevelSoftRound;

        //Format to display in a rounded rectangle shape.
        mediaFile.AutoShapeType =
          MsoAutoShapeType.msoShapeRoundedRectangle;

        //Save the presentation.
        presentation.SaveAs(
          presentationName,
          PpSaveAsFileType.ppSaveAsOpenXMLPresentation,
          MsoTriState.msoFalse);
      }
      finally
      {
        //Release the references to the PowerPoint objects.
        mediaFile = null;
        slide = null;
        presentation = null;

        //Release the ApplicationClass object.
        if (application != null)
        {
          application.Quit();
          application = null;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        GC.WaitForPendingFinalizers();
      }
    }
  }
}
