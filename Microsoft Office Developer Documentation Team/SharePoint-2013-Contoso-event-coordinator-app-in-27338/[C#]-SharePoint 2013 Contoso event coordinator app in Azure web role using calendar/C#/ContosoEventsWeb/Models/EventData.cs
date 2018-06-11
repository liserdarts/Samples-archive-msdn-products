/*
 * Developed by:    Martin Harwar, www.Point8020.com
 * Developed for:   MSDN and SharePoint Product group
 * First released:  14th February, 2014
 * 
 * This file contains eight classes: EventData, Event, EventRole, EventAgenda, EventCatering, EventAttendee, EventPresenter and EventAttachement
 * 

 * 
 * EventData:
 * This is the most complex class. It contains:
 * 
 * 1. A private function named eventConnection that returns a SQLConnection object to the SQL Azure database
 * 
 * 2. Many other functions for retrieving, inserting, updating, and deleting event planning data. These functions all
 * follow very similar patterns in that they call various SQL stored procedures for performing the appropriate data operations.
 * Some of these methods accept parameters, the values of which are then used appropriateley in SQL stored procedure parameters.
 * Note that many of the stored procedures return 'Status' fields. A value of "Point8020.Success" is consistently uses to signal
 * that the data operation was processed as expected. Any other value returned signals that although the procedure completed without
 * error, the data in the database did not represent an expected state (such as listing agenda items for an event that does not exist)
 * 
 * 
 * 
 * All other classes simply define properites that map to their respective tables in the SQL Azure database
 * 
 */

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ContosoEventsWeb.Models
{
    public class EventData
    {
        private SqlConnection eventConnection()
        {
            SqlConnection sqlCon = new SqlConnection();
            string connStr = string.Empty;
            if (ConfigurationManager.ConnectionStrings["EventDB"] != null)
            {
                sqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["EventDB"].ConnectionString;
                return (sqlCon);
            }
            else
            {
                return (null);
            }
        }

        public List<Event> EventList(string UserName, bool IsSiteOwner)
        {
            List<Event> eventList = new List<Event>();
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "ListEvents";
                    SqlParameter userName = sqlCmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
                    userName.Value = UserName;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Event eventItem = new Event();
                        if (IsSiteOwner)
                        {
                            eventItem.CurrentUserCanEdit = true;
                        }
                        else
                        {
                            if (sqlReader["CurrentUserCanEdit"].ToString() == "0")
                            {
                                eventItem.CurrentUserCanEdit = false;
                            }
                            else
                            {
                                eventItem.CurrentUserCanEdit = true;
                            }
                        }
                        eventItem.EventID = sqlReader["EventID"].ToString();
                        eventItem.Title = sqlReader["Title"].ToString();
                        eventItem.Venue = sqlReader["Venue"].ToString();
                        eventItem.Address1 = sqlReader["Address1"].ToString();
                        eventItem.Address2 = sqlReader["Address2"].ToString();
                        eventItem.City = sqlReader["City"].ToString();
                        eventItem.State = sqlReader["State"].ToString();
                        eventItem.PostalCode = sqlReader["PostalCode"].ToString();
                        eventItem.StartDateTime = sqlReader["StartDateTime"].ToString();
                        eventItem.EndDateTime = sqlReader["EndDateTime"].ToString();
                        eventItem.LogoURL = sqlReader["LogoURL"].ToString();
                        eventItem.Description = sqlReader["Description"].ToString();
                        eventItem.BudgetURL = sqlReader["BudgetURL"].ToString();
                        eventList.Add(eventItem);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (eventList);

                }
                catch
                {
                    return (eventList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (eventList);
        }

        public string AddEvent(string EventTitle, string EventVenue, string EventAddress1, string EventAddress2, string EventCity, string EventState, string EventPostalCode, DateTime EventStartDateTime, DateTime EventEndDateTime, string EventDescription)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddEvent";

                    SqlParameter eventTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    eventTitle.Value = EventTitle;

                    SqlParameter eventVenue = sqlCmd.Parameters.Add("@Venue", SqlDbType.NVarChar);
                    eventVenue.Value = EventVenue;

                    SqlParameter eventAddress1 = sqlCmd.Parameters.Add("@Address1", SqlDbType.NVarChar);
                    eventAddress1.Value = EventAddress1;

                    SqlParameter eventAddress2 = sqlCmd.Parameters.Add("@Address2", SqlDbType.NVarChar);
                    eventAddress2.Value = EventAddress2;

                    SqlParameter eventCity = sqlCmd.Parameters.Add("@City", SqlDbType.NVarChar);
                    eventCity.Value = EventCity;

                    SqlParameter eventState = sqlCmd.Parameters.Add("@State", SqlDbType.NVarChar);
                    eventState.Value = EventState;

                    SqlParameter eventPostalCode = sqlCmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar);
                    eventPostalCode.Value = EventPostalCode;

                    SqlParameter eventStartDateTime = sqlCmd.Parameters.Add("@StartDateTime", SqlDbType.SmallDateTime);
                    eventStartDateTime.Value = EventStartDateTime.ToUniversalTime();

                    SqlParameter eventEndDateTime = sqlCmd.Parameters.Add("@EndDateTime", SqlDbType.SmallDateTime);
                    eventEndDateTime.Value = EventEndDateTime.ToUniversalTime();

                    SqlParameter eventDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    eventDescription.Value = EventDescription;


                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["EventID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string UpdateEvent(string EventID, string EventTitle, string EventVenue, string EventAddress1, string EventAddress2, string EventCity, string EventState, string EventPostalCode, DateTime EventStartDateTime, DateTime EventEndDateTime, string EventDescription)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateEvent";

                    SqlParameter eventID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    eventID.Value = Guid.Parse(EventID);

                    SqlParameter eventTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    eventTitle.Value = EventTitle;

                    SqlParameter eventVenue = sqlCmd.Parameters.Add("@Venue", SqlDbType.NVarChar);
                    eventVenue.Value = EventVenue;

                    SqlParameter eventAddress1 = sqlCmd.Parameters.Add("@Address1", SqlDbType.NVarChar);
                    eventAddress1.Value = EventAddress1;

                    SqlParameter eventAddress2 = sqlCmd.Parameters.Add("@Address2", SqlDbType.NVarChar);
                    eventAddress2.Value = EventAddress2;

                    SqlParameter eventCity = sqlCmd.Parameters.Add("@City", SqlDbType.NVarChar);
                    eventCity.Value = EventCity;

                    SqlParameter eventState = sqlCmd.Parameters.Add("@State", SqlDbType.NVarChar);
                    eventState.Value = EventState;

                    SqlParameter eventPostalCode = sqlCmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar);
                    eventPostalCode.Value = EventPostalCode;

                    SqlParameter eventStartDateTime = sqlCmd.Parameters.Add("@StartDateTime", SqlDbType.SmallDateTime);
                    eventStartDateTime.Value = EventStartDateTime;

                    SqlParameter eventEndDateTime = sqlCmd.Parameters.Add("@EndDateTime", SqlDbType.SmallDateTime);
                    eventEndDateTime.Value = EventEndDateTime;

                    SqlParameter eventDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    eventDescription.Value = EventDescription;


                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["EventID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public bool DeleteEvent(string eventID)
        {
            bool success = false;
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteEvent";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(eventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            success = true;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (success);
                }
                catch
                {
                    return (success);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (success);
        }

        public List<EventAgenda> Agenda(string EventID)
        {
            List<EventAgenda> agendaList = new List<EventAgenda>();
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetAgendaItems";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        EventAgenda agendaItem = new EventAgenda();
                        agendaItem.EventID = sqlReader["EventID"].ToString();
                        agendaItem.AgendaItemID = sqlReader["AgendaItemID"].ToString();
                        agendaItem.Title = sqlReader["Title"].ToString();
                        agendaItem.Description = sqlReader["Description"].ToString();
                        agendaItem.ItemOrder = int.Parse(sqlReader["ItemOrder"].ToString());

                        agendaList.Add(agendaItem);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (agendaList);

                }
                catch
                {
                    return (agendaList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (agendaList);
        }

        public string UpdateAgendaItem(string EventID, string ItemID, string Title, string Description)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateAgendaItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter itmID = sqlCmd.Parameters.Add("@AgendaItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);

                    SqlParameter agendaTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    agendaTitle.Value = Title;

                    SqlParameter agendaDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    agendaDescription.Value = Description;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["AgendaItemID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string UpdateAgenda(string IDList)
        {
            string status = "Success";
            string[] ids = IDList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int idx = 0;
            foreach (string id in ids)
            {
                idx++;
                SqlConnection sqlCon = this.eventConnection();
                if (sqlCon != null)
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    try
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "UpdateAgenda";
                        SqlParameter itmID = sqlCmd.Parameters.Add("@AgendaItemID", SqlDbType.UniqueIdentifier);
                        itmID.Value = Guid.Parse(id);
                        SqlParameter itmOrder = sqlCmd.Parameters.Add("@ItemOrder", SqlDbType.TinyInt);
                        itmOrder.Value = idx;
                        sqlCon.Open();
                        sqlCmd.Connection = sqlCon;
                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            if (sqlReader["Status"].ToString() != "Point8020.Success")
                            {
                                throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                            }
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: Error validating save operation"));
                        }
                    }
                    catch (Exception ex)
                    {
                        status = ex.Message;
                    }
                    finally
                    {
                        sqlCmd.Dispose();
                        sqlCon.Dispose();
                    }
                }
                else
                {
                    status = "Point8020.Error Database cannot be contacted";
                }
            }
            return (status);
        }

        public string DeleteAgendaItem(string ItemID)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteAgendaItem";

                    SqlParameter itmID = sqlCmd.Parameters.Add("@AgendaItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return ("Success");
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string AddAgendaItem(string EventID, string Title, string Description)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddAgendaItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter agendaTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    agendaTitle.Value = Title;

                    SqlParameter agendaDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    agendaDescription.Value = Description;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["AgendaItemID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public List<EventPresenter> Presenters(string EventID)
        {
            List<EventPresenter> presenterList = new List<EventPresenter>();
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetPresenterItems";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        EventPresenter presenterItem = new EventPresenter();
                        presenterItem.EventID = sqlReader["EventID"].ToString();
                        presenterItem.PresenterID = sqlReader["PresenterID"].ToString();
                        presenterItem.Title = sqlReader["PresenterName"].ToString();
                        presenterItem.PresenterBio = sqlReader["PresenterBio"].ToString();
                        presenterItem.ItemOrder = int.Parse(sqlReader["ItemOrder"].ToString());

                        presenterList.Add(presenterItem);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (presenterList);

                }
                catch
                {
                    return (presenterList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (presenterList);
        }

        public string UpdatePresenterItem(string EventID, string ItemID, string Title, string Description)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdatePresenterItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter itmID = sqlCmd.Parameters.Add("@PresenterItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);

                    SqlParameter presenterTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    presenterTitle.Value = Title;

                    SqlParameter presenterDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    presenterDescription.Value = Description;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["PresenterItemID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string UpdatePresenter(string IDList)
        {
            string status = "Success";
            string[] ids = IDList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int idx = 0;
            foreach (string id in ids)
            {
                idx++;
                SqlConnection sqlCon = this.eventConnection();
                if (sqlCon != null)
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    try
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "UpdatePresenter";
                        SqlParameter itmID = sqlCmd.Parameters.Add("@PresenterItemID", SqlDbType.UniqueIdentifier);
                        itmID.Value = Guid.Parse(id);
                        SqlParameter itmOrder = sqlCmd.Parameters.Add("@ItemOrder", SqlDbType.TinyInt);
                        itmOrder.Value = idx;
                        sqlCon.Open();
                        sqlCmd.Connection = sqlCon;
                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            if (sqlReader["Status"].ToString() != "Point8020.Success")
                            {
                                throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                            }
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: Error validating save operation"));
                        }
                    }
                    catch (Exception ex)
                    {
                        status = ex.Message;
                    }
                    finally
                    {
                        sqlCmd.Dispose();
                        sqlCon.Dispose();
                    }
                }
                else
                {
                    status = "Point8020.Error Database cannot be contacted";
                }
            }
            return (status);
        }

        public string DeletePresenterItem(string ItemID)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeletePresenterItem";

                    SqlParameter itmID = sqlCmd.Parameters.Add("@PresenterItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return ("Success");
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string AddPresenterItem(string EventID, string Title, string Description)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddPresenterItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter presenterTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    presenterTitle.Value = Title;

                    SqlParameter presenterDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    presenterDescription.Value = Description;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["PresenterID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public List<EventAttendee> Attendees(string EventID)
        {
            List<EventAttendee> attendeeList = new List<EventAttendee>();
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetAttendeeItems";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        EventAttendee attendeeItem = new EventAttendee();
                        attendeeItem.EventID = sqlReader["EventID"].ToString();
                        attendeeItem.AttendeeID = sqlReader["AttendeeID"].ToString();
                        attendeeItem.AttendeeName = sqlReader["AttendeeName"].ToString();
                        attendeeItem.AttendeeEmail = sqlReader["AttendeeEmail"].ToString();

                        attendeeList.Add(attendeeItem);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (attendeeList);

                }
                catch
                {
                    return (attendeeList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (attendeeList);
        }

        public string UpdateAttendeeItem(string EventID, string ItemID, string Name, string Email)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateAttendeeItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter itmID = sqlCmd.Parameters.Add("@AttendeeItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);

                    SqlParameter attendeeTitle = sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                    attendeeTitle.Value = Name;

                    SqlParameter attendeeDescription = sqlCmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    attendeeDescription.Value = Email;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["AttendeeItemID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string DeleteAttendeeItem(string ItemID)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteAttendeeItem";

                    SqlParameter itmID = sqlCmd.Parameters.Add("@AttendeeItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return ("Success");
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string AddAttendeeItem(string EventID, string Name, string Email)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddAttendeeItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter attendeeTitle = sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                    attendeeTitle.Value = Name;

                    SqlParameter attendeeDescription = sqlCmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    attendeeDescription.Value = Email;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["AttendeeID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public List<EventCatering> Catering(string EventID)
        {
            List<EventCatering> cateringList = new List<EventCatering>();
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetCateringItems";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        EventCatering cateringItem = new EventCatering();
                        cateringItem.EventID = sqlReader["EventID"].ToString();
                        cateringItem.CateringItemID = sqlReader["CateringID"].ToString();
                        cateringItem.Title = sqlReader["Title"].ToString();
                        cateringItem.Description = sqlReader["Description"].ToString();
                        cateringItem.ItemOrder = int.Parse(sqlReader["MealOrder"].ToString());

                        cateringList.Add(cateringItem);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (cateringList);

                }
                catch
                {
                    return (cateringList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (cateringList);
        }

        public string UpdateCateringItem(string EventID, string ItemID, string Title, string Description)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateCateringItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter itmID = sqlCmd.Parameters.Add("@CateringItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);

                    SqlParameter cateringTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    cateringTitle.Value = Title;

                    SqlParameter cateringDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    cateringDescription.Value = Description;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["CateringItemID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string UpdateCatering(string IDList)
        {
            string status = "Success";
            string[] ids = IDList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int idx = 0;
            foreach (string id in ids)
            {
                idx++;
                SqlConnection sqlCon = this.eventConnection();
                if (sqlCon != null)
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    try
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "UpdateCatering";
                        SqlParameter itmID = sqlCmd.Parameters.Add("@CateringItemID", SqlDbType.UniqueIdentifier);
                        itmID.Value = Guid.Parse(id);
                        SqlParameter itmOrder = sqlCmd.Parameters.Add("@ItemOrder", SqlDbType.TinyInt);
                        itmOrder.Value = idx;
                        sqlCon.Open();
                        sqlCmd.Connection = sqlCon;
                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            if (sqlReader["Status"].ToString() != "Point8020.Success")
                            {
                                throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                            }
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: Error validating save operation"));
                        }
                    }
                    catch (Exception ex)
                    {
                        status = ex.Message;
                    }
                    finally
                    {
                        sqlCmd.Dispose();
                        sqlCon.Dispose();
                    }
                }
                else
                {
                    status = "Point8020.Error Database cannot be contacted";
                }
            }
            return (status);
        }

        public string DeleteCateringItem(string ItemID)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteCateringItem";

                    SqlParameter itmID = sqlCmd.Parameters.Add("@CateringItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return ("Success");
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string AddCateringItem(string EventID, string Title, string Description)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddCateringItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter cateringTitle = sqlCmd.Parameters.Add("@Title", SqlDbType.NVarChar);
                    cateringTitle.Value = Title;

                    SqlParameter cateringDescription = sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                    cateringDescription.Value = Description;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return (sqlReader["CateringID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public List<EventRole> Coordinators(string EventID)
        {
            List<EventRole> coordinatorsList = new List<EventRole>();
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetCoordinators";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        EventRole coordinator = new EventRole();
                        coordinator.EventID = sqlReader["EventID"].ToString();
                        coordinator.RoleID = sqlReader["RoleID"].ToString();
                        coordinator.MemberName = sqlReader["MemberName"].ToString();
                        coordinatorsList.Add(coordinator);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (coordinatorsList);

                }
                catch
                {
                    return (coordinatorsList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (coordinatorsList);
        }

        public string SaveRoles(string eventID, string memberList)
        {
            string status = "Success";
            status = clearRoles(eventID);
            string[] members = memberList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string member in members)
            {
                SqlConnection sqlCon = this.eventConnection();
                if (sqlCon != null)
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    try
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "AddCoordinator";
                        SqlParameter itmID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                        itmID.Value = Guid.Parse(eventID);
                        SqlParameter memberName = sqlCmd.Parameters.Add("@MemberName", SqlDbType.NVarChar);
                        memberName.Value = member;
                        sqlCon.Open();
                        sqlCmd.Connection = sqlCon;
                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            if (sqlReader["Status"].ToString() != "Point8020.Success")
                            {
                                throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                            }
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: Error validating save operation"));
                        }
                    }
                    catch (Exception ex)
                    {
                        status = ex.Message;
                    }
                    finally
                    {
                        sqlCmd.Dispose();
                        sqlCon.Dispose();
                    }
                }
                else
                {
                    status = "Point8020.Error Database cannot be contacted";
                }
            }
            return (status);
        }

        private string clearRoles(string eventID)
        {
            string clearStatus = "Success";
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteAllCoordinators";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(eventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() != "Point8020.Success")
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    clearStatus = ex.Message;
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            else
            {
                clearStatus = "Point8020.Error Database cannot be contacted";
            }
            return (clearStatus);
        }

        public List<EventAttachment> Attachments(string EventID)
        {
            List<EventAttachment> attachmentList = new List<EventAttachment>();
            string urlBase = string.Empty;
            if (ConfigurationManager.ConnectionStrings["EventAzureURLBase"] != null)
            {
                urlBase = ConfigurationManager.ConnectionStrings["EventAzureURLBase"].ConnectionString;
            }
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetAttachmentItems";
                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        EventAttachment attachmentItem = new EventAttachment();
                        attachmentItem.EventID = sqlReader["EventID"].ToString();
                        attachmentItem.AttachmentID = sqlReader["AttachmentID"].ToString();
                        attachmentItem.AttachmentURL = urlBase + sqlReader["AttachmentURL"].ToString().ToLower();
                        int iPos = sqlReader["AttachmentURL"].ToString().LastIndexOf("/");
                        attachmentItem.DisplayText = sqlReader["AttachmentURL"].ToString().Substring(iPos + 1);
                        attachmentList.Add(attachmentItem);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (attachmentList);

                }
                catch
                {
                    return (attachmentList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (attachmentList);
        }

        public string DeleteAttachmentItem(string ItemID)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteAttachmentItem";

                    SqlParameter itmID = sqlCmd.Parameters.Add("@AttachmentItemID", SqlDbType.UniqueIdentifier);
                    itmID.Value = Guid.Parse(ItemID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return ("Success");
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public string AddAttachmentItem(string EventID, string Url)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddAttachmentItem";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);

                    SqlParameter attachmentUrl = sqlCmd.Parameters.Add("@URL", SqlDbType.NVarChar);
                    attachmentUrl.Value = Url;

                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            return ("Point8020.Success:" + sqlReader["AttachmentID"].ToString());
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return ("Point8020.Error: Database cannont be contacted.");
        }

        public bool IsCoordinator(string EventID, string UserName)
        {
            SqlConnection sqlCon = this.eventConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "IsUserCoordinator";

                    SqlParameter evtID = sqlCmd.Parameters.Add("@EventID", SqlDbType.UniqueIdentifier);
                    evtID.Value = Guid.Parse(EventID);
                    SqlParameter uName = sqlCmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
                    uName.Value = UserName;
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            if (sqlReader["IsInRole"].ToString() == "TRUE")
                            {
                                return (true);
                            }
                            else
                            {
                                return (false);
                            }
                        }
                        else
                        {
                            throw (new Exception("Point8020.Error: " + sqlReader["Status"].ToString()));
                        }
                    }
                    else
                    {
                        throw (new Exception("Point8020.Error: Error validating save operation"));
                    }
                }
                catch
                {
                    return (false);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }

            }
            return (false);

        }
    }
    public class Event
    {
        public bool CurrentUserCanEdit { get; set; }
        public string EventID { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string LogoURL { get; set; }
        public string Description { get; set; }
        public string BudgetURL { get; set; }

    }
    public class EventRole
    {
        public string EventID { get; set; }
        public string RoleID { get; set; }
        public string MemberName { get; set; }

    }
    public class EventAgenda
    {
        public string EventID { get; set; }
        public string AgendaItemID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ItemOrder { get; set; }

    }
    public class EventCatering
    {
        public string EventID { get; set; }
        public string CateringItemID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ItemOrder { get; set; }

    }
    public class EventPresenter
    {
        public string EventID { get; set; }
        public string PresenterID { get; set; }
        public string Title { get; set; }
        public string PresenterBio { get; set; }
        public int ItemOrder { get; set; }

    }
    public class EventAttendee
    {
        public string EventID { get; set; }
        public string AttendeeID { get; set; }
        public string AttendeeName { get; set; }
        public string AttendeeEmail { get; set; }

    }
    public class EventAttachment
    {
        public string EventID { get; set; }
        public string AttachmentID { get; set; }
        public string AttachmentURL { get; set; }
        public string DisplayText { get; set; }

    }
}