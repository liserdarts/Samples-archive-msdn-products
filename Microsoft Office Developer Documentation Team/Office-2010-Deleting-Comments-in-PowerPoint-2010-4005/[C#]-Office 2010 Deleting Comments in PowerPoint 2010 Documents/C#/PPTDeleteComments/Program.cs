using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace PPTDeleteComments
{
  class Program
  {
    static void Main(string[] args)
    {
      PPTDeleteComments(@"C:\temp\comments.pptx", "George Li");
    }

    // Delete comments by a specific author. Pass an empty string for the author to delete all comments, by all authors.
    public static void PPTDeleteComments(string fileName, string author = "")
    {

      using (PresentationDocument doc = PresentationDocument.Open(fileName, true))
      {
        // Get the authors part.
        CommentAuthorsPart authorsPart = doc.PresentationPart.CommentAuthorsPart;

        if (authorsPart == null)
        {
          // There's no authors part, so just
          // fail. If no authors, there can't be any comments.
          return;
        }

        // Get the comment authors, or the specified author if supplied:
        var commentAuthors = authorsPart.
          CommentAuthorList.Elements<CommentAuthor>();
        if (!string.IsNullOrEmpty(author))
        {
          commentAuthors = commentAuthors.
            Where(e => e.Name.Value.Equals(author));
        }

        foreach (var commentAuthor in commentAuthors)
        {
          var authorId = commentAuthor.Id;

          // Iterate through all the slides and get the slide parts.
          foreach (var slide in doc.PresentationPart.SlideParts)
          {
            // Iterate through the slide parts and find the slide comment part.
            var slideCommentsPart = slide.SlideCommentsPart;
            if (slideCommentsPart != null)
            {
              // Get the list of comments.
              var commentList = slideCommentsPart.CommentList.
                Elements<Comment>().Where(e => e.AuthorId.Value == authorId.Value);

              foreach (var comment in commentList.ToArray())
              {
                // Delete all the comments by the specified author.
                comment.Remove();
              }

              // No comments left? Delete the comments part for this slide.
              if (slideCommentsPart.CommentList.Count() == 0)
              {
                slide.DeletePart(slideCommentsPart);
              }
              else
              {
                // Save the slide comments part.
                slideCommentsPart.CommentList.Save();
              }
            }
          }

          // Delete the comment author from the comment authors part.
          commentAuthor.Remove();
        }

        if (authorsPart.CommentAuthorList.Count() == 0)
        {
          // No authors left, so delete the part.
          doc.PresentationPart.DeletePart(authorsPart);
        }
        else
        {
          // Save the comment authors part.
          authorsPart.CommentAuthorList.Save();
        }
      }
    }
  }
}
