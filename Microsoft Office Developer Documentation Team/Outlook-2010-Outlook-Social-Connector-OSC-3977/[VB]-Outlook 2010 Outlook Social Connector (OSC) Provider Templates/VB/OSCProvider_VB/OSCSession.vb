Imports System
Imports System.Data
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports OutlookSocialProvider

Namespace OSCProvider_VB
    Public Class OSCSession
        Implements OutlookSocialProvider.ISocialSession, OutlookSocialProvider.ISocialSession2

#Region "ISocialSession Members"
        Public Function GetActivities(ByVal emailAddresses As String(), ByVal startTime As Date) As String Implements OutlookSocialProvider.ISocialSession.GetActivities
            'To-Do: Implement GetActivities
            Return String.Empty
        End Function
        Public Function FindPerson(ByVal userID As String) As String Implements OutlookSocialProvider.ISocialSession.FindPerson
            'To-Do: Implement FindPerson
            Return String.Empty
        End Function

        Public Function GetLoggedOnUser() As OutlookSocialProvider.ISocialProfile Implements OutlookSocialProvider.ISocialSession.GetLoggedOnUser
            Return New OSCProfile()
        End Function

        Public Function GetLogonUrl() As String Implements OutlookSocialProvider.ISocialSession.GetLogonUrl
            'To-Do: Implement GetLogonUrl
            Return String.Empty
        End Function

        Public Function GetNetworkIdentifier() As String Implements OutlookSocialProvider.ISocialSession.GetNetworkIdentifier
            'To-Do: Implement GetNetworkIdentifier
            Return String.Empty
        End Function

        Public Function GetPerson(ByVal userID As String) As OutlookSocialProvider.ISocialPerson Implements OutlookSocialProvider.ISocialSession.GetPerson
            'To-Do: Implement GetPerson
            Return New OSCPerson
        End Function

        Public Sub UnFollowPerson(ByVal userID As String) Implements OutlookSocialProvider.ISocialSession.UnFollowPerson
            'To-Do: Implement UnFollowPerson
        End Sub

        Public Sub FollowPerson(ByVal emailAddress As String) Implements OutlookSocialProvider.ISocialSession.FollowPerson
            'To-Do: Implement FollowPerson
        End Sub

        Public ReadOnly Property LoggedOnUserID() As String Implements OutlookSocialProvider.ISocialSession.LoggedOnUserID
            Get
                'To-Do: Implement LoggedOnUserID
                Return String.Empty
            End Get
        End Property

        Public ReadOnly Property LoggedOnUserName() As String Implements OutlookSocialProvider.ISocialSession.LoggedOnUserName
            Get
                'To-Do: Implement LoggedOnUserName
                Return String.Empty
            End Get
        End Property

        Public Sub Logon(ByVal userName As String, ByVal password As String) Implements OutlookSocialProvider.ISocialSession.Logon
            'To-do: Implement LogonWeb or Logon depending on supported auth model
        End Sub

        Public Sub LogonWeb(ByVal connectIn As String, ByRef connectOut As String) Implements OutlookSocialProvider.ISocialSession.LogonWeb
            'To-do: Implement LogonWeb or Logon depending on supported auth model
        End Sub

        Public WriteOnly Property SiteUrl() As String Implements OutlookSocialProvider.ISocialSession.SiteUrl
            Set(ByVal value As String)
                'To-Do: Implement SiteUrl
            End Set
        End Property
#End Region

#Region "ISocialSession2 Members"
        Public Function GetActivitiesEx(ByVal hashedAddresses As String(), ByVal startTime As Date) As String Implements ISocialSession2.GetActivitiesEx
            'To-Do: Implement GetActivitiesEx
            Return String.Empty
        End Function

        Public Function GetPeopleDetails(ByVal personsAddresses As String) As String Implements ISocialSession2.GetPeopleDetails
            'To-Do: Implement GetPeopleDetails
            Return String.Empty
        End Function

        Public Sub FollowPersonEx(ByVal emailAddresses As String(), ByVal displayName As String) Implements ISocialSession2.FollowPersonEx
            'To-Do: Implement FollowPersonEx
        End Sub

        Public Sub LogonCached(ByVal connectIn As String, ByVal userName As String, ByVal password As String, ByRef connectOut As String) Implements ISocialSession2.LogonCached
            'To-Do: Implement LogonCached
        End Sub
#End Region
    End Class
End Namespace

