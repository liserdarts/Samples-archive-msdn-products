Imports System
Imports System.Data
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports OutlookSocialProvider

Namespace OSCProvider_VB
    Public Class OSCPerson
        Implements OutlookSocialProvider.ISocialPerson

#Region "ISocialPerson Members"
        Public Function GetActivities(ByVal startTime As Date) As String Implements OutlookSocialProvider.ISocialPerson.GetActivities
            'To-Do: Implement GetActivities
            Return String.Empty
        End Function

        Public Function GetDetails() As String Implements OutlookSocialProvider.ISocialPerson.GetDetails
            'To-Do: Implement GetDetails
            Return String.Empty
        End Function

        Public Function GetFriendsAndColleagues() As String Implements OutlookSocialProvider.ISocialPerson.GetFriendsAndColleagues
            'To-Do: Implement GetFriendsAndColleagues
            Return String.Empty
        End Function

        Public Function GetFriendsAndColleaguesIDs() As String() Implements OutlookSocialProvider.ISocialPerson.GetFriendsAndColleaguesIDs
            'To-Do: Implement GetFriendAndColleaguesIDs
            Dim result As String() = {""}
            Return result
        End Function

        Public Function GetPicture() As Byte() Implements OutlookSocialProvider.ISocialPerson.GetPicture
            'To-Do: Implement GetPicture
            Return (HelperMethods.GetProviderJpeg())
        End Function

        Public Function GetStatus() As String Implements OutlookSocialProvider.ISocialPerson.GetStatus
            'Not supported in OSC version 1.0 and version 1.1
            Return String.Empty
        End Function
#End Region
    End Class
End Namespace

