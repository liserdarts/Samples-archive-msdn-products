Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

''' <summary>
''' This class contains the properties for Entity1. The properties keep the data for Entity1.
''' If you want to rename the class, don't forget to rename the entity in the model xml as well.
''' </summary>
Partial Public Class TrainingEvent
    Private _TrainingEventID As Int32
    Private _EventDate As DateTime
    Private _Status As String
    Private _LoginName As String
    Private _Title As String
    Private _EventType As String
    Private _Description As String
    Public Property TrainingEventID() As Int32
        Get
            Return _TrainingEventID
        End Get
        Set(ByVal value As Int32)
            _TrainingEventID = value
        End Set
    End Property

    Public Property EventDate() As DateTime
        Get
            Return _EventDate
        End Get
        Set(ByVal value As DateTime)
            _EventDate = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property
    Public Property LoginName() As String
        Get
            Return _LoginName
        End Get
        Set(ByVal value As String)
            _LoginName = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
    Public Property EventType() As String
        Get
            Return _EventType
        End Get
        Set(ByVal value As String)
            _EventType = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

End Class
