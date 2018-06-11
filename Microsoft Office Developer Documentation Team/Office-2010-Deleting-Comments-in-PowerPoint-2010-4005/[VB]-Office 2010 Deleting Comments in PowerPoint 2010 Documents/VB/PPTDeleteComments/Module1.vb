Option Strict On

Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Presentation

Module Module1

  Sub Main()
    PPTDeleteComments("C:\temp\comments.pptx", "Ken Getz")
  End Sub



  Public Sub PPTDeleteComments(ByVal fileName As String,
                               Optional ByVal author As String = "")

    Using doc As PresentationDocument = PresentationDocument.Open(fileName, True)
      ' Get the authors part.
      Dim authorsPart As CommentAuthorsPart = doc.PresentationPart.CommentAuthorsPart

      If authorsPart Is Nothing Then
        ' There's no authors part, so just
        ' fail. If no authors, there can't be any comments.
        Return
      End If

      ' Get the comment authors, or the specified author if supplied:
      Dim commentAuthors = authorsPart.
        CommentAuthorList.Elements(Of CommentAuthor)()
      If (Not String.IsNullOrEmpty(author)) Then
        commentAuthors = commentAuthors.
          Where(Function(e) e.Name.Value.Equals(author))
      End If

      Dim changed As Boolean = False
      ' The commentAuthors list contains either all the authors, or one author.
      For Each commentAuthor In commentAuthors
        Dim authorId = commentAuthor.Id

        ' Iterate through all the slides and get the slide parts.
        For Each slide In doc.PresentationPart.SlideParts
          ' Iterate through the slide parts and find the slide comment part.
          Dim slideCommentsPart = slide.SlideCommentsPart
          If slideCommentsPart IsNot Nothing Then

            ' Get the list of comments.
            Dim commentList = slideCommentsPart.CommentList.Elements(Of Comment)().
              Where(Function(e) e.AuthorId.Value = authorId.Value)
            For Each comment In commentList.ToArray()
              ' Delete all the comments by the specified author.
              comment.Remove()
            Next comment

            ' No comments left? Delete the comments part for this slide.
            If slideCommentsPart.CommentList.Count() = 0 Then
              slide.DeletePart(slideCommentsPart)
            Else
              ' Save the slide comments part.
              slideCommentsPart.CommentList.Save()
            End If
          End If

        Next slide

        ' Delete the comment author from the comment authors part.
        commentAuthor.Remove()
      Next commentAuthor

      If authorsPart.CommentAuthorList.Count = 0 Then
        ' No authors left, so delete the part.
        doc.PresentationPart.DeletePart(authorsPart)
      Else
        ' Save the comment authors part.
        authorsPart.CommentAuthorList.Save()
      End If
    End Using
  End Sub
End Module
