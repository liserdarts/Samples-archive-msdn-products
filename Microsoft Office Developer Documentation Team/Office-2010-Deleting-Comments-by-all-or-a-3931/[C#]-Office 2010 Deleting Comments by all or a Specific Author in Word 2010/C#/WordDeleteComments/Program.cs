using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace WordDeleteComments
{
  class Program
  {
    static void Main(string[] args)
    {
      WDDeleteComments("C:\\Temp\\Comments.docx", "Ken Getz");
    }


    // Delete comments by a specific author. Pass an empty string for the author to delete all comments, by all authors.
    public static void WDDeleteComments(string fileName, string author = "")
    {
      // Get an existing Wordprocessing document.
      using (WordprocessingDocument document = 
        WordprocessingDocument.Open(fileName, true))
      {
        // Set commentPart to the document WordprocessingCommentsPart, 
        // if it exists.
        WordprocessingCommentsPart commentPart =
          document.MainDocumentPart.WordprocessingCommentsPart;

        // If no WordprocessingCommentsPart exists, there can be no comments. 
        // Stop execution and return from the method.
        if (commentPart == null)
        {
          return;
        }

        List<Comment> commentsToDelete =
          commentPart.Comments.Elements<Comment>().ToList();

        // Create a list of comments by the specified author.
        if (!String.IsNullOrEmpty(author))
        {
          commentsToDelete = commentsToDelete.
            Where(c => c.Author == author).ToList();
        }
        IEnumerable<string> commentIds = 
          commentsToDelete.Select(r => r.Id.Value);

        // Delete each comment in commentToDelete from the 
        // Comments collection.
        foreach (Comment c in commentsToDelete)
        {
          c.Remove();
        }

        // Save comment part change.
        commentPart.Comments.Save();

        Document doc = document.MainDocumentPart.Document;

        // Delete CommentRangeStart within main document.
        List<CommentRangeStart> commentRangeStartToDelete =
          doc.Descendants<CommentRangeStart>().
          Where(c => commentIds.Contains(c.Id.Value)).ToList();
        foreach (CommentRangeStart c in commentRangeStartToDelete)
        {
          c.Remove();
        }

        // Delete CommentRangeEnd within the main document.
        List<CommentRangeEnd> commentRangeEndToDelete =
          doc.Descendants<CommentRangeEnd>().
          Where(c => commentIds.Contains(c.Id.Value)).ToList();
        foreach (CommentRangeEnd c in commentRangeEndToDelete)
        {
          c.Remove();
        }

        // Delete CommentReference within main document.
        List<CommentReference> commentRangeReferenceToDelete =
          doc.Descendants<CommentReference>().
          Where(c => commentIds.Contains(c.Id.Value)).ToList();
        foreach (CommentReference c in commentRangeReferenceToDelete)
        {
          c.Remove();
        }

        // Save changes back to the MainDocumentPart part.
        doc.Save();
      }
    }     

  }
}
