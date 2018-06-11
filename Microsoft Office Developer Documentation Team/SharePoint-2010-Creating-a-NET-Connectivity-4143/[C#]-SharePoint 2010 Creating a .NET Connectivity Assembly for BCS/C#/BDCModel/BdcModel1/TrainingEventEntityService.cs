using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace BDCModel.BdcModel1
{
    /// <summary>
    /// All the methods for retrieving, updating and deleting data are implemented in this class file.
    /// The samples below show the finder and specific finder method for Entity1.
    /// </summary>
    public class TrainingEventEntityService
    {
        static SqlConnection getSqlConnection()
        {
            SqlConnection sqlConn = new SqlConnection(
              "Integrated Security=SSPI;Persist Security Info=False;"
              + "Initial Catalog=HRTrainingManagement;"
              + @"Data Source= Demo2010a");
            return (sqlConn);
        }


        /// <summary>
        /// This is a sample specific finder method for Entity1.
        /// If you want to delete or rename the method think about changing the xml in the BDC model file as well.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity1</returns>
        public static TrainingEvent ReadItem(int id)
        {
            SqlConnection thisConn = null;
            TrainingEvent evt = null;
            try
            {
                evt = new TrainingEvent();
                thisConn = getSqlConnection();
                thisConn.Open();
                SqlCommand thisCmd = new SqlCommand();
                thisCmd.CommandText = "SELECT e.TrainingEventID, s.LoginName,"
                + " t.Title, t.EventType, t.Description, e.EventDate, e.Status"
                + " FROM Student s"
                + " INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID"
                + " INNER JOIN TrainingObjects t ON e.TrainingID = t.TrainingID"
                + " WHERE e.TrainingEventID = " + id.ToString();
                thisCmd.Connection = thisConn;
                SqlDataReader thisReader =
                  thisCmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (thisReader.Read())
                {
                    evt.TrainingEventID = id;
                    evt.LoginName = thisReader[1].ToString();
                    evt.Title = thisReader[2].ToString();
                    evt.EventType = thisReader[3].ToString();
                    evt.Description = thisReader[4].ToString();
                    evt.EventDate = DateTime.Parse(thisReader[5].ToString());
                    evt.Status = thisReader[6].ToString();
                }
                else
                {
                    evt.TrainingEventID = -1;
                    evt.LoginName = "Data Not Found";
                    evt.Title = "Data Not Found";
                    evt.EventType = "Data Not Found";
                    evt.Description = "Data Not Found";
                    evt.EventDate = DateTime.MinValue;
                    evt.Status = "Data Not Found";
                }
                thisReader.Close();
                return (evt);
            }
            catch
            {
                evt.TrainingEventID = -1;
                evt.LoginName = "Data Not Found";
                evt.Title = "Data Not Found";
                evt.EventType = "Data Not Found";
                evt.Description = "Data Not Found";
                evt.EventDate = DateTime.MinValue;
                evt.Status = "Data Not Found";
                return (evt);
            }
            finally
            {
                thisConn.Dispose();
            }

        }
        /// <summary>
        /// This is a sample finder method for Entity1.
        /// If you want to delete or rename the method think about changing the xml in the BDC model file as well.
        /// </summary>
        /// <returns>IEnumerable of Entities</returns>
        public static IEnumerable<TrainingEvent> ReadList()
        {
            SqlConnection thisConn = null;
            List<TrainingEvent> allEvents;
            try
            {
                thisConn = getSqlConnection();
                allEvents = new List<TrainingEvent>();
                thisConn.Open();
                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConn;
                thisCommand.CommandText = "SELECT e.TrainingEventID, LoginName,"
                + " t.Title, t.EventType, t.Description, e.EventDate, e.Status"
                + " FROM Student s"
                + " INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID"
                + " INNER JOIN TrainingObjects t ON e.TrainingID = t.TrainingID";
                SqlDataReader thisReader =
                  thisCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (thisReader.Read())
                {
                    TrainingEvent evt = new TrainingEvent();
                    evt.TrainingEventID = int.Parse(thisReader[0].ToString());
                    evt.LoginName = thisReader[1].ToString();
                    evt.Title = thisReader[2].ToString();
                    evt.EventType = thisReader[3].ToString();
                    evt.Description = thisReader[4].ToString();
                    evt.EventDate = DateTime.Parse(thisReader[5].ToString());
                    evt.Status = thisReader[6].ToString();
                    allEvents.Add(evt);
                }
                thisReader.Close();
                TrainingEvent[] eventList = new TrainingEvent[allEvents.Count];
                for (int evtCounter = 0;
                  evtCounter <= allEvents.Count - 1;
                  evtCounter++)
                {
                    eventList[evtCounter] = allEvents[evtCounter];
                }
                return (eventList);
            }
            catch (Exception ex)
            {
                TrainingEvent[] errEventList = new TrainingEvent[1];
                TrainingEvent errEvt = new TrainingEvent();
                errEvt.TrainingEventID = -1;
                errEvt.LoginName = ex.Message;
                errEvt.Title = ex.Message;
                errEvt.EventType = ex.Message;
                errEvt.Description = ex.Message;
                errEvt.EventDate = DateTime.MinValue;
                errEvt.Status = ex.Message;
                errEventList[0] = errEvt;
                return (errEventList);
            }
            finally
            {
                thisConn.Dispose();
            }

        }

        public static TrainingEvent Create(TrainingEvent newTrainingEventEntity)
        {
            SqlConnection thisConn = null;
            try
            {
                thisConn = getSqlConnection();
                thisConn.Open();
                string studentName = newTrainingEventEntity.LoginName;
                string trainingTitle = newTrainingEventEntity.Title;
                string trainingType = newTrainingEventEntity.EventType;
                string trainingDescription = newTrainingEventEntity.Description;
                DateTime trainingDate = newTrainingEventEntity.EventDate;
                string trainingStatus = newTrainingEventEntity.Status;
                int studentID = 0;
                SqlCommand studentCmd = new SqlCommand();
                studentCmd.Connection = thisConn;
                studentCmd.CommandText = "SELECT StudentID"
                  + " FROM Student"
                  + " WHERE LoginName='" + studentName + "'";
                SqlDataReader studentReader =
                  studentCmd.ExecuteReader(CommandBehavior.Default);
                if (studentReader.Read())
                {
                    studentID = int.Parse(studentReader[0].ToString());
                    studentReader.Close();
                }
                else
                {
                    studentReader.Close();
                    SqlCommand addStudentCommand = new SqlCommand();
                    addStudentCommand.Connection = thisConn;
                    addStudentCommand.CommandText =
                      "INSERT Student(LoginName) VALUES('" + studentName + "')";
                    addStudentCommand.ExecuteNonQuery();
                    SqlCommand getNewStudentCmd = new SqlCommand();
                    getNewStudentCmd.Connection = thisConn;
                    getNewStudentCmd.CommandText = "SELECT StudentID"
                      + " FROM Student"
                      + " WHERE LoginName = '" + studentName + "'";
                    SqlDataReader getNewStudentReader =
                      getNewStudentCmd.ExecuteReader(CommandBehavior.Default);
                    getNewStudentReader.Read();
                    studentID = int.Parse(getNewStudentReader[0].ToString());
                    getNewStudentReader.Close();
                }
                int trainingID = 0;
                SqlCommand trainingCmd = new SqlCommand();
                trainingCmd.Connection = thisConn;
                trainingCmd.CommandText = "SELECT TrainingID"
                  + " FROM TrainingObjects"
                  + " WHERE Title = '" + trainingTitle + "'"
                  + " AND EventType = '" + trainingType + "'"
                  + " AND Description = '" + trainingDescription + "'";
                SqlDataReader trainingReader =
                  trainingCmd.ExecuteReader(CommandBehavior.Default);
                if (trainingReader.Read())
                {
                    trainingID = int.Parse(trainingReader[0].ToString());
                    trainingReader.Close();
                }
                else
                {
                    trainingReader.Close();
                    SqlCommand addTrainingCommand = new SqlCommand();
                    addTrainingCommand.Connection = thisConn;
                    addTrainingCommand.CommandText =
                      "INSERT TrainingObjects(Title, EventType, Description)"
                      + " VALUES('" + trainingTitle + "','"
                      + trainingType + "','"
                      + trainingDescription + "')";
                    addTrainingCommand.ExecuteNonQuery();
                    SqlCommand getNewTrainingCmd = new SqlCommand();
                    getNewTrainingCmd.Connection = thisConn;
                    getNewTrainingCmd.CommandText = "SELECT TrainingID"
                      + " FROM TrainingObjects"
                      + " WHERE Title = '" + trainingTitle + "'"
                      + " AND EventType = '" + trainingType + "'"
                      + " AND Description = '" + trainingDescription + "'";
                    SqlDataReader getNewTrainingReader =
                      getNewTrainingCmd.ExecuteReader(CommandBehavior.Default);
                    getNewTrainingReader.Read();
                    trainingID = int.Parse(getNewTrainingReader[0].ToString());
                    getNewTrainingReader.Close();
                }
                SqlCommand insertEventCommand = new SqlCommand();
                insertEventCommand.Connection = thisConn;
                insertEventCommand.CommandText = "INSERT TrainingEvent"
                  + "(StudentID, TrainingID, EventDate, Status) VALUES("
                  + studentID
                  + ", " + trainingID
                  + ", '" + trainingDate.ToShortDateString() + "'"
                  + ", '" + trainingStatus + "')";
                insertEventCommand.ExecuteNonQuery();
                return (newTrainingEventEntity);
            }
            finally
            {
                thisConn.Dispose();
            }

        }

        public static void Delete(int trainingEventID)
        {
            SqlConnection thisConn = null;
            try
            {
                thisConn = getSqlConnection();
                thisConn.Open();
                SqlCommand thisCommand = new SqlCommand();
                thisCommand.Connection = thisConn;
                thisCommand.CommandText =
                  "DELETE TrainingEvent WHERE TrainingEventID = "
                  + trainingEventID.ToString();
                thisCommand.ExecuteNonQuery();
            }
            finally
            {
                thisConn.Dispose();
            }

        }

        public static void Update(TrainingEvent trainingEventEntity)
        {
            SqlConnection thisConn = null;
            try
            {
                thisConn = getSqlConnection();
                thisConn.Open();
                int trainingEventID = trainingEventEntity.TrainingEventID;
                string studentName = trainingEventEntity.LoginName;
                string trainingTitle = trainingEventEntity.Title;
                string trainingType = trainingEventEntity.EventType;
                string trainingDescription = trainingEventEntity.Description;
                DateTime trainingDate = trainingEventEntity.EventDate;
                string trainingStatus = trainingEventEntity.Status;
                int studentID = 0;
                SqlCommand studentCmd = new SqlCommand();
                studentCmd.Connection = thisConn;
                studentCmd.CommandText = "SELECT s.StudentID, s.LoginName"
                 + " FROM Student s"
                 + " INNER JOIN TrainingEvent e ON s.StudentID = e.StudentID"
                 + " WHERE e.TrainingEventID = " + trainingEventID.ToString();
                SqlDataReader studentReader =
                  studentCmd.ExecuteReader(CommandBehavior.Default);
                studentReader.Read();
                if (studentReader[1].ToString() == studentName)
                {
                    studentID = int.Parse(studentReader[0].ToString());
                    studentReader.Close();
                }
                else
                {
                    studentReader.Close();
                    SqlCommand changeStudentCmd = new SqlCommand();
                    changeStudentCmd.Connection = thisConn;
                    changeStudentCmd.CommandText = "SELECT StudentID"
                      + " FROM Student"
                      + " WHERE LoginName = '" + studentName + "'";
                    SqlDataReader changeStudentReader =
                    changeStudentCmd.ExecuteReader(CommandBehavior.Default);
                    if (changeStudentReader.Read())
                    {
                        studentID = int.Parse(changeStudentReader[0].ToString());
                        changeStudentReader.Close();
                    }
                    else
                    {
                        changeStudentReader.Close();
                        SqlCommand addStudentCommand = new SqlCommand();
                        addStudentCommand.Connection = thisConn;
                        addStudentCommand.CommandText =
                         "INSERT Student(LoginName) VALUES('" + studentName + "')";
                        addStudentCommand.ExecuteNonQuery();
                        SqlCommand getNewStudentCmd = new SqlCommand();
                        getNewStudentCmd.Connection = thisConn;
                        getNewStudentCmd.CommandText = "SELECT StudentID"
                          + " FROM Student"
                          + " WHERE LoginName = '" + studentName + "'";
                        SqlDataReader getNewStudentReader =
                          getNewStudentCmd.ExecuteReader(CommandBehavior.Default);
                        getNewStudentReader.Read();
                        studentID = int.Parse(getNewStudentReader[0].ToString());
                        getNewStudentReader.Close();
                    }
                }
                int trainingID = 0;
                SqlCommand trainingCmd = new SqlCommand();
                studentCmd.Connection = thisConn;
                studentCmd.CommandText =
                  "SELECT t.TrainingID, t.Title, t.EventType, t.Description"
                  + " FROM TrainingObjects t"
                  + " INNER JOIN TrainingEvent e ON t.TrainingID = e.TrainingID"
                  + " WHERE e.TrainingEventID = " + trainingEventID.ToString();
                SqlDataReader trainingReader =
                studentCmd.ExecuteReader(CommandBehavior.Default);
                trainingReader.Read();
                if ((trainingReader[1].ToString() == trainingTitle)
                  && (trainingReader[2].ToString() == trainingType)
                  && (trainingReader[3].ToString() == trainingDescription))
                {
                    trainingID = int.Parse(trainingReader[0].ToString());
                    trainingReader.Close();
                }
                else
                {
                    trainingReader.Close();
                    SqlCommand changeTrainingCmd = new SqlCommand();
                    changeTrainingCmd.Connection = thisConn;
                    changeTrainingCmd.CommandText = "SELECT TrainingID"
                    + " FROM TrainingObjects"
                    + " WHERE Title = '" + trainingTitle + "'"
                    + " AND EventType = '" + trainingType + "'"
                    + " AND Description = '" + trainingDescription + "'";
                    SqlDataReader changeTrainingReader =
                      changeTrainingCmd.ExecuteReader(CommandBehavior.Default);
                    if (changeTrainingReader.Read())
                    {
                        trainingID = int.Parse(changeTrainingReader[0].ToString());
                        changeTrainingReader.Close();
                    }
                    else
                    {
                        changeTrainingReader.Close();
                        SqlCommand addTrainingCommand = new SqlCommand();
                        addTrainingCommand.Connection = thisConn;
                        addTrainingCommand.CommandText = "INSERT TrainingObjects("
                          + "Title, EventType, Description) VALUES('"
                          + trainingTitle + "','"
                          + trainingType + "','"
                          + trainingDescription + "')";
                        addTrainingCommand.ExecuteNonQuery();
                        SqlCommand getNewTrainingCmd = new SqlCommand();
                        getNewTrainingCmd.Connection = thisConn;
                        getNewTrainingCmd.CommandText = "SELECT TrainingID"
                          + " FROM TrainingObjects"
                          + " WHERE Title = '" + trainingTitle + "'"
                          + " AND EventType = '" + trainingType + "'"
                          + " AND Description = '" + trainingDescription + "'";
                        SqlDataReader getNewTrainingReader =
                          getNewTrainingCmd.ExecuteReader(CommandBehavior.Default);
                        getNewTrainingReader.Read();
                        trainingID = int.Parse(getNewTrainingReader[0].ToString());
                        getNewTrainingReader.Close();
                    }
                }
                SqlCommand updateEventCommand = new SqlCommand();
                updateEventCommand.Connection = thisConn;
                updateEventCommand.CommandText = "UPDATE TrainingEvent"
                  + " SET StudentID=" + studentID
                  + ", TrainingID=" + trainingID
                  + ", EventDate='" + trainingDate.ToShortDateString() + "'"
                  + ", Status='" + trainingStatus + "'"
                  + " WHERE TrainingEventID=" + trainingEventID;
                updateEventCommand.ExecuteNonQuery();
            }
            finally
            {
                thisConn.Dispose();
            }

        }
    }
}
