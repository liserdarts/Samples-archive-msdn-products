Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Presentation

Module Module1

  Sub Main()
    Console.WriteLine(PPTReorderSlides("C:\temp\Sample.pptx", 0, 3).ToString())
  End Sub


  Public Function PPTReorderSlides(ByVal fileName As String, _
    ByVal originalPosition As Integer, ByVal newPosition As Integer) As Integer

    ' Given a PPT deck, an original position, and a new position, attempt to place 
    ' the slide in the original position into the new position within the deck.
    ' If the original position is outside the range of the number of slides in the 
    ' deck, use the last slide. If the new position is outside the range
    ' of slides in the deck, put the selected slide at the end of the deck. 
    ' Use -1 in either property to indicate that you want to use the
    ' highest-numbered position. If you put -1 in both original and new positions,
    ' however, the procedure will do nothing.

    ' Return the location in which the selected slide was placed, or -1 if 
    ' no slide was moved.

    ' Example:
    ' MessageBox.Show(PPTReorderSlides("C:\Samples\ReorderSlides.pptx", 1, 3).ToString())

    ' Assume that no slide moves; return -1.
    Dim returnValue As Integer = -1

    ' Moving to and from same position? Get out now.
    If newPosition = originalPosition Then
      Return returnValue
    End If

    Using doc As PresentationDocument = PresentationDocument.Open(fileName, True)
      ' Get the presentation part of the document.
      Dim presentationPart As PresentationPart = doc.PresentationPart
      ' No presentation part? Something's wrong with the document.
      If presentationPart Is Nothing Then
        Throw New ArgumentException("fileName")
      End If

      ' If you're here, you know that presentationPart exists.
      Dim slideCount As Integer = presentationPart.SlideParts.Count()

      ' No slides? Just return -1 indicating that nothing  happened.
      If slideCount = 0 Then
        Return returnValue
      End If

      ' There are slides. Calculate real positions.
      Dim maxPosition As Integer = slideCount - 1

      ' Adjust the positions, if necessary.
      CalcPositions(originalPosition, newPosition, maxPosition)

      ' The two positions could have ended up being the same 
      ' thing. There's nothing to do, in that case. Otherwise,
      ' do the work.
      If newPosition <> originalPosition Then
        Dim presentation As Presentation = presentationPart.Presentation
        Dim slideIdList As SlideIdList = presentation.SlideIdList

        ' Get the slide ID of the source and target slides.
        Dim sourceSlide As SlideId =
          CType(slideIdList.ChildElements(originalPosition), SlideId)
        Dim targetSlide As SlideId =
          CType(slideIdList.ChildElements(newPosition), SlideId)

        ' Remove the source slide from its parent tree. You can't
        ' move a slide while it's part of an XML node tree.
        sourceSlide.Remove()

        If newPosition > originalPosition Then
          slideIdList.InsertAfter(sourceSlide, targetSlide)
        Else
          slideIdList.InsertBefore(sourceSlide, targetSlide)
        End If

        ' Set the return value.
        returnValue = newPosition

        ' Save the modified presentation.
        presentation.Save()
      End If
    End Using
    Return returnValue
  End Function

  Private Sub CalcPositions(
    ByRef originalPosition As Integer,
    ByRef newPosition As Integer, ByVal maxPosition As Integer)

    ' Adjust the original and new slide position as necessary.

    If originalPosition < 0 Then
      ' Ask for the slide in the final position? Get that value now.
      originalPosition = maxPosition
    End If

    If newPosition < 0 Then
      ' Ask for the final position? Get that value now.
      newPosition = maxPosition
    End If

    If originalPosition > maxPosition Then
      originalPosition = maxPosition
    End If
    If newPosition > maxPosition Then
      newPosition = maxPosition
    End If
  End Sub
End Module
