Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

''' <summary>
''' All the methods for retrieving, updating and deleting data are implemented in this class file.
''' The samples below show the finder and specific finder method for Entity1.
''' </summary>
Public Class TrainingEventEntityService
    Private Shared Function getSqlConnection() As SqlConnection
        Dim sqlConn As New SqlConnection("Integrated Security=SSPI;Persist Security Info=False; Initial Catalog=HRTrainingManagement; Data Source=Demo2010a")
        Return (sqlConn)
    End Function
    ''' <summary>
    ''' This is a sample specific finder method for Entity1.
    ''' If you want to delete or rename the method think about changing the xml in the BDC model file as well.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>Entity1</returns>
    Public Shared Function ReadItem(ByVal id As Integer) As TrainingEvent
        Dim thisConn As SqlConnection = Nothing
        Dim evt As TrainingEvent = Nothing
        Try
            evt = New TrainingEvent()
            thisConn = getSqlConnection()
            thisConn.Open()
            Dim thisCmd As New SqlCommand()
            thisCmd.CommandText = "SELECT e.TrainingEventID, s.LoginName, t.Title, t.EventType, t.Description, e.EventDate, e.Status FROM Student s INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID INNER JOIN TrainingObjects t ON e.TrainingID = t.TrainingID WHERE e.TrainingEventID = " & id.ToString()
            thisCmd.Connection = thisConn
            Dim thisReader As SqlDataReader = thisCmd.ExecuteReader(CommandBehavior.CloseConnection)
            If thisReader.Read() Then
                evt.TrainingEventID = id
                evt.LoginName = thisReader(1).ToString()
                evt.Title = thisReader(2).ToString()
                evt.EventType = thisReader(3).ToString()
                evt.Description = thisReader(4).ToString()
                evt.EventDate = DateTime.Parse(thisReader(5).ToString())
                evt.Status = thisReader(6).ToString()
            Else
                evt.TrainingEventID = -1
                evt.LoginName = "Data Not Found"
                evt.Title = "Data Not Found"
                evt.EventType = "Data Not Found"
                evt.Description = "Data Not Found"
                evt.EventDate = DateTime.MinValue
                evt.Status = "Data Not Found"
            End If
            thisReader.Close()
            Return (evt)
        Catch ex As Exception
            evt.TrainingEventID = -1
            evt.LoginName = "Data Not Found"
            evt.Title = "Data Not Found"
            evt.EventType = "Data Not Found"
            evt.Description = "Data Not Found"
            evt.EventDate = DateTime.MinValue
            evt.Status = "Data Not Found"
            Return (evt)
        Finally
            thisConn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' This is a sample finder method for Entity1.
    ''' If you want to delete or rename the method think about changing the xml in the BDC model file as well.
    ''' </summary>
    ''' <returns>IEnumerable of Entities</returns>
    Public Shared Function ReadList() As IEnumerable(Of TrainingEvent)
        Dim thisConn As SqlConnection = Nothing
        Dim allEvents As List(Of TrainingEvent)
        Try
            thisConn = getSqlConnection()
            allEvents = New List(Of TrainingEvent)()
            thisConn.Open()
            Dim thisCommand As New SqlCommand()
            thisCommand.Connection = thisConn
            thisCommand.CommandText = "SELECT e.TrainingEventID, LoginName, t.Title, t.EventType, t.Description, e.EventDate, e.Status FROM Student s INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID INNER JOIN TrainingObjects t ON e.TrainingID = t.TrainingID"
            Dim thisReader As SqlDataReader = thisCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While thisReader.Read()
                Dim evt As New TrainingEvent()
                evt.TrainingEventID = Integer.Parse(thisReader(0).ToString())
                evt.LoginName = thisReader(1).ToString()
                evt.Title = thisReader(2).ToString()
                evt.EventType = thisReader(3).ToString()
                evt.Description = thisReader(4).ToString()
                evt.EventDate = DateTime.Parse(thisReader(5).ToString())
                evt.Status = thisReader(6).ToString()
                allEvents.Add(evt)
            End While
            thisReader.Close()
            Dim eventList As TrainingEvent() = New TrainingEvent(allEvents.Count - 1) {}
            For evtCounter As Integer = 0 To allEvents.Count - 1
                eventList(evtCounter) = allEvents(evtCounter)
            Next
            Return (eventList)
        Catch ex As Exception
            Dim errEventList As TrainingEvent() = New TrainingEvent(0) {}
            Dim errEvt As New TrainingEvent()
            errEvt.TrainingEventID = -1
            errEvt.LoginName = ex.Message
            errEvt.Title = ex.Message
            errEvt.EventType = ex.Message
            errEvt.Description = ex.Message
            errEvt.EventDate = DateTime.MinValue
            errEvt.Status = ex.Message
            errEventList(0) = errEvt
            Return (errEventList)
        Finally
            thisConn.Dispose()
        End Try

    End Function

    Public Shared Function Create(ByVal newTrainingEventEntity As TrainingEvent) As TrainingEvent
        Dim thisConn As SqlConnection = Nothing
        Try
            thisConn = getSqlConnection()
            thisConn.Open()
            Dim studentName As String = newTrainingEventEntity.LoginName
            Dim trainingTitle As String = newTrainingEventEntity.Title
            Dim trainingType As String = newTrainingEventEntity.EventType
            Dim trainingDescription As String = newTrainingEventEntity.Description
            Dim trainingDate As DateTime = newTrainingEventEntity.EventDate
            Dim trainingStatus As String = newTrainingEventEntity.Status
            Dim studentID As Integer = 0
            Dim studentCmd As New SqlCommand()
            studentCmd.Connection = thisConn
            studentCmd.CommandText = "SELECT StudentID FROM Student WHERE LoginName='" & studentName & "'"
            Dim studentReader As SqlDataReader = studentCmd.ExecuteReader(CommandBehavior.[Default])
            If studentReader.Read() Then
                studentID = Integer.Parse(studentReader(0).ToString())
                studentReader.Close()
            Else
                studentReader.Close()
                Dim addStudentCommand As New SqlCommand()
                addStudentCommand.Connection = thisConn
                addStudentCommand.CommandText = "INSERT Student(LoginName) VALUES('" & studentName & "')"
                addStudentCommand.ExecuteNonQuery()
                Dim getNewStudentCmd As New SqlCommand()
                getNewStudentCmd.Connection = thisConn
                getNewStudentCmd.CommandText = "SELECT StudentID FROM Student WHERE LoginName = '" & studentName & "'"
                Dim getNewStudentReader As SqlDataReader = getNewStudentCmd.ExecuteReader(CommandBehavior.[Default])
                getNewStudentReader.Read()
                studentID = Integer.Parse(getNewStudentReader(0).ToString())
                getNewStudentReader.Close()
            End If
            Dim trainingID As Integer = 0
            Dim trainingCmd As New SqlCommand()
            trainingCmd.Connection = thisConn
            trainingCmd.CommandText = "SELECT TrainingID" & " FROM TrainingObjects" & " WHERE Title = '" & trainingTitle & "'" & " AND EventType = '" & trainingType & "'" & " AND Description = '" & trainingDescription & "'"
            Dim trainingReader As SqlDataReader = trainingCmd.ExecuteReader(CommandBehavior.[Default])
            If trainingReader.Read() Then
                trainingID = Integer.Parse(trainingReader(0).ToString())
                trainingReader.Close()
            Else
                trainingReader.Close()
                Dim addTrainingCommand As New SqlCommand()
                addTrainingCommand.Connection = thisConn
                addTrainingCommand.CommandText = "INSERT TrainingObjects(Title, EventType, Description) VALUES('" & trainingTitle & "','" & trainingType & "','" & trainingDescription & "')"
                addTrainingCommand.ExecuteNonQuery()
                Dim getNewTrainingCmd As New SqlCommand()
                getNewTrainingCmd.Connection = thisConn
                getNewTrainingCmd.CommandText = "SELECT TrainingID FROM TrainingObjects WHERE Title = '" + trainingTitle & "' AND EventType = '" & trainingType & "' AND Description = '" & trainingDescription & "'"
                Dim getNewTrainingReader As SqlDataReader = getNewTrainingCmd.ExecuteReader(CommandBehavior.[Default])
                getNewTrainingReader.Read()
                trainingID = Integer.Parse(getNewTrainingReader(0).ToString())
                getNewTrainingReader.Close()
            End If
            Dim insertEventCommand As New SqlCommand()
            insertEventCommand.Connection = thisConn
            insertEventCommand.CommandText = "INSERT TrainingEvent (StudentID, TrainingID, EventDate, Status) VALUES(" & studentID & ", " & trainingID & ", '" + trainingDate.ToShortDateString() & "', '" & trainingStatus & "')"
            insertEventCommand.ExecuteNonQuery()
            Return (newTrainingEventEntity)
        Finally
            thisConn.Dispose()
        End Try

    End Function

    Public Shared Sub Delete(ByVal trainingEventID As Integer)
        Dim thisConn As SqlConnection = Nothing
        Try
            thisConn = getSqlConnection()
            thisConn.Open()

            Dim thisCommand As New SqlCommand()
            thisCommand.Connection = thisConn
            thisCommand.CommandText = "DELETE TrainingEvent WHERE TrainingEventID = " & trainingEventID.ToString()
            thisCommand.ExecuteNonQuery()
        Finally
            thisConn.Dispose()
        End Try

    End Sub

    Public Shared Sub Update(ByVal trainingEventEntity As TrainingEvent)
        Dim thisConn As SqlConnection = Nothing
        Try
            thisConn = getSqlConnection()
            thisConn.Open()
            Dim trainingEventID As Integer = trainingEventEntity.TrainingEventID
            Dim studentName As String = trainingEventEntity.LoginName
            Dim trainingTitle As String = trainingEventEntity.Title
            Dim trainingType As String = trainingEventEntity.EventType
            Dim trainingDescription As String = trainingEventEntity.Description
            Dim trainingDate As DateTime = trainingEventEntity.EventDate
            Dim trainingStatus As String = trainingEventEntity.Status
            Dim studentID As Integer = 0
            Dim studentCmd As New SqlCommand()
            studentCmd.Connection = thisConn
            studentCmd.CommandText = "SELECT s.StudentID, s.LoginName FROM Student s INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID WHERE e.TrainingEventID = " & trainingEventID.ToString()
            Dim studentReader As SqlDataReader = studentCmd.ExecuteReader(CommandBehavior.[Default])
            studentReader.Read()
            If studentReader(1).ToString() = studentName Then
                studentID = Integer.Parse(studentReader(0).ToString())
                studentReader.Close()
            Else
                studentReader.Close()
                Dim changeStudentCmd As New SqlCommand()
                changeStudentCmd.Connection = thisConn
                changeStudentCmd.CommandText = "SELECT StudentID FROM Student WHERE LoginName = '" + studentName & "'"
                Dim changeStudentReader As SqlDataReader = changeStudentCmd.ExecuteReader(CommandBehavior.[Default])
                If changeStudentReader.Read() Then
                    studentID = Integer.Parse(changeStudentReader(0).ToString())
                    changeStudentReader.Close()
                Else
                    changeStudentReader.Close()
                    Dim addStudentCommand As New SqlCommand()
                    addStudentCommand.Connection = thisConn
                    addStudentCommand.CommandText = "INSERT Student(LoginName) VALUES('" & studentName & "')"
                    addStudentCommand.ExecuteNonQuery()
                    Dim getNewStudentCmd As New SqlCommand()
                    getNewStudentCmd.Connection = thisConn
                    getNewStudentCmd.CommandText = "SELECT StudentID FROM Student WHERE LoginName = '" + studentName & "'"
                    Dim getNewStudentReader As SqlDataReader = getNewStudentCmd.ExecuteReader(CommandBehavior.[Default])
                    getNewStudentReader.Read()
                    studentID = Integer.Parse(getNewStudentReader(0).ToString())
                    getNewStudentReader.Close()
                End If
            End If
            Dim trainingID As Integer = 0
            Dim trainingCmd As New SqlCommand()
            studentCmd.Connection = thisConn
            studentCmd.CommandText = "SELECT t.TrainingID, t.Title, t.EventType, t.Description FROM TrainingObjects t INNER JOIN TrainingEvent e ON t.TrainingID = e.TrainingID WHERE e.TrainingEventID = " & trainingEventID.ToString()
            Dim trainingReader As SqlDataReader = studentCmd.ExecuteReader(CommandBehavior.[Default])
            trainingReader.Read()
            If (trainingReader(1).ToString() = trainingTitle) AndAlso (trainingReader(2).ToString() = trainingType) AndAlso (trainingReader(3).ToString() = trainingDescription) Then
                trainingID = Integer.Parse(trainingReader(0).ToString())
                trainingReader.Close()
            Else
                trainingReader.Close()
                Dim changeTrainingCmd As New SqlCommand()
                changeTrainingCmd.Connection = thisConn
                changeTrainingCmd.CommandText = "SELECT TrainingID FROM TrainingObjects WHERE Title = '" & trainingTitle & "' AND EventType = '" + trainingType & "' AND Description = '" & trainingDescription & "'"
                Dim changeTrainingReader As SqlDataReader = changeTrainingCmd.ExecuteReader(CommandBehavior.[Default])
                If changeTrainingReader.Read() Then
                    trainingID = Integer.Parse(changeTrainingReader(0).ToString())
                    changeTrainingReader.Close()
                Else
                    changeTrainingReader.Close()
                    Dim addTrainingCommand As New SqlCommand()
                    addTrainingCommand.Connection = thisConn
                    addTrainingCommand.CommandText = "INSERT TrainingObjects(Title, EventType, Description) VALUES('" & trainingTitle & "','" & trainingType & "','" & trainingDescription & "')"
                    addTrainingCommand.ExecuteNonQuery()
                    Dim getNewTrainingCmd As New SqlCommand()
                    getNewTrainingCmd.Connection = thisConn
                    getNewTrainingCmd.CommandText = "SELECT TrainingID FROM TrainingObjects WHERE Title = '" & trainingTitle & "' AND EventType = '" & trainingType & "' AND Description = '" & trainingDescription & "'"
                    Dim getNewTrainingReader As SqlDataReader = getNewTrainingCmd.ExecuteReader(CommandBehavior.[Default])
                    getNewTrainingReader.Read()
                    trainingID = Integer.Parse(getNewTrainingReader(0).ToString())
                    getNewTrainingReader.Close()
                End If
            End If
            Dim updateEventCommand As New SqlCommand()
            updateEventCommand.Connection = thisConn
            updateEventCommand.CommandText = "UPDATE TrainingEvent SET StudentID=" & studentID & ", TrainingID=" & trainingID & ", EventDate='" & trainingDate.ToShortDateString() & "'" & ", Status='" & trainingStatus & "' WHERE TrainingEventID=" & trainingEventID
            updateEventCommand.ExecuteNonQuery()
        Finally
            thisConn.Dispose()
        End Try

    End Sub
End Class
