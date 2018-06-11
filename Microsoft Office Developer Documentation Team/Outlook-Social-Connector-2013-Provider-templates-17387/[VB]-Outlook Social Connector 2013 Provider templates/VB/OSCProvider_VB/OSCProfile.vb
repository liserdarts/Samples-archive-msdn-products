Imports System
Imports System.Data
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports OutlookSocialProvider

Namespace OSCProvider_VB
    Public Class OSCProfile
        Implements OutlookSocialProvider.ISocialProfile

#Region "ISocialProfile Members"
        Public Function GetActivities(ByVal startTime As Date) As String _
            Implements OutlookSocialProvider.ISocialPerson.GetActivities, _
            OutlookSocialProvider.ISocialProfile.GetActivities
            'Not supported since OSC 2013
            Return String.Empty
        End Function

        Public Function GetDetails() As String _
            Implements OutlookSocialProvider.ISocialPerson.GetDetails, _
            OutlookSocialProvider.ISocialProfile.GetDetails
            'To-Do: Implement GetDetails
            Return String.Empty
        End Function

        Public Function GetFriendsAndColleagues() As String _
            Implements OutlookSocialProvider.ISocialPerson.GetFriendsAndColleagues, _
            ISocialProfile.GetFriendsAndColleagues
            'To-Do: Implement GetFriendsAndColleagues
            Return String.Empty
        End Function

        Public Function GetFriendsAndColleaguesIDs() As String() _
            Implements OutlookSocialProvider.ISocialPerson.GetFriendsAndColleaguesIDs, _
            OutlookSocialProvider.ISocialProfile.GetFriendsAndColleaguesIDs
            'To-Do: Implement GetFriendsAndColleaguesIDs
            Return Nothing
        End Function

        Public Function GetPicture() As Byte() _
            Implements OutlookSocialProvider.ISocialPerson.GetPicture, _
            OutlookSocialProvider.ISocialProfile.GetPicture
            'To-Do: Implement GetPicture
            Return Nothing
        End Function

        Public Function GetStatus() As String _
            Implements OutlookSocialProvider.ISocialPerson.GetStatus, _
            OutlookSocialProvider.ISocialProfile.GetStatus
            'Not supported
            Return String.Empty
        End Function

        Public Function AreFriendsOrColleagues(ByVal userIDs As String()) As Boolean() _
            Implements OutlookSocialProvider.ISocialProfile.AreFriendsOrColleagues
            'Not supported in OSC version 1.0 and version 1.1
            Dim result As Boolean() = {True}
            Return result
        End Function

        Public Function GetActivitiesOfFriendsAndColleagues(ByVal startTime As Date) As String _
            Implements OutlookSocialProvider.ISocialProfile.GetActivitiesOfFriendsAndColleagues
            'Not supported since OSC 2013
            Return String.Empty
        End Function

        Public Sub SetStatus(ByVal status As String) _
        Implements OutlookSocialProvider.ISocialProfile.SetStatus
            'Not supported
        End Sub

#End Region
    End Class
End Namespace

