using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace RetrieveSlideCount
{
  class Program
  {

    // Alter this constant as necessary for your own environment:
    private const string DEMOPATH = @"I:\Samples\HiddenSlides.pptx";
    static void Main(string[] args)
    {
      Console.WriteLine(PPTGetSlideCount(DEMOPATH, false));
      Console.WriteLine(PPTGetSlideCount(DEMOPATH));
    }

    public static int PPTGetSlideCount(string fileName)
    {
      return PPTGetSlideCount(fileName, true);
    }

    public static int PPTGetSlideCount(string fileName, bool includeHidden = true)
    {
      int slidesCount = 0;

      using (PresentationDocument doc = PresentationDocument.Open(fileName, false))
      {
        // Get the presentation part of the document.
        PresentationPart presentationPart = doc.PresentationPart;
        if (presentationPart != null)
        {
          if (includeHidden)
          {
            slidesCount = presentationPart.SlideParts.Count();
          }
          else
          {
            // Each slide can include a Show property, which if hidden will contain the value "0".
            // The Show property may not exist, and most likely will not, for non-hidden slides.
            var slides = presentationPart.SlideParts.Where(
                (s) => (s.Slide != null) && 
                  ((s.Slide.Show == null) || (s.Slide.Show.HasValue && s.Slide.Show.Value)));
            slidesCount = slides.Count();
          }
        }
      }
      return slidesCount;
    }
  }
}
