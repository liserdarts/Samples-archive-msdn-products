using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace PPTReorderSlides
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine(PPTReorderSlides(@"C:\temp\Sample.pptx", 0, 3).ToString());
    }


    public static int PPTReorderSlides(string fileName, int originalPosition, int newPosition)
    {
      // Assume that no slide moves; return -1.
      int returnValue = -1;

      // Moving to and from same position? Get out now.
      if (newPosition == originalPosition)
      {
        return returnValue;
      }

      using (PresentationDocument doc = PresentationDocument.Open(fileName, true))
      {
        // Get the presentation part of the document.
        PresentationPart presentationPart = doc.PresentationPart;
        // No presentation part? Something's wrong with the document.
        if (presentationPart == null)
        {
          throw new ArgumentException("fileName");
        }

        // If you're here, you know that presentationPart exists.
        int slideCount = presentationPart.SlideParts.Count();

        // No slides? Just return -1 indicating that nothing  happened.
        if (slideCount == 0)
        {
          return returnValue;
        }

        // There are slides. Calculate real positions.
        int maxPosition = slideCount - 1;

        // Adjust the positions, if necessary.
        CalcPositions(ref originalPosition, ref newPosition, maxPosition);

        // The two positions could have ended up being the same 
        // thing. There's nothing to do, in that case. Otherwise,
        // do the work.
        if (newPosition != originalPosition)
        {
          Presentation presentation = presentationPart.Presentation;
          SlideIdList slideIdList = presentation.SlideIdList;

          // Get the slide ID of the source and target slides.
          SlideId sourceSlide = 
            (SlideId)(slideIdList.ChildElements[originalPosition]);
          SlideId targetSlide = 
            (SlideId)(slideIdList.ChildElements[newPosition]);

          // Remove the source slide from its parent tree. You can't
          // move a slide while it's part of an XML node tree.
          sourceSlide.Remove();

          if (newPosition > originalPosition)
          {
            slideIdList.InsertAfter(sourceSlide, targetSlide);
          }
          else
          {
            slideIdList.InsertBefore(sourceSlide, targetSlide);
          }

          // Set the return value.
          returnValue = newPosition;

          // Save the modified presentation.
          presentation.Save();
        }
      }
      return returnValue;
    }

    private static void CalcPositions(
      ref int originalPosition, ref int newPosition, int maxPosition)
    {
      // Adjust the original and new slide position as necessary.
      if (originalPosition < 0)
      {
        // Ask for the slide in the final position? Get that value now.
        originalPosition = maxPosition;
      }

      if (newPosition < 0)
      {
        // Ask for the final position? Get that value now.
        newPosition = maxPosition;
      }

      if (originalPosition > maxPosition)
      {
        originalPosition = maxPosition;
      }
      if (newPosition > maxPosition)
      {
        newPosition = maxPosition;
      }
    }
  }
}
