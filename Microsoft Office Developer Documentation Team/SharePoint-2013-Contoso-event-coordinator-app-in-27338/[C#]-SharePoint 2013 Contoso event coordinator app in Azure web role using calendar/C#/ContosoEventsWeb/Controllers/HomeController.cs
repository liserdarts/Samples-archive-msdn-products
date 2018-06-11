/*
 * Developed by:    Martin Harwar, www.Point8020.com
 * Developed for:   MSDN and SharePoint Product group
 * First released:  14th February, 2014
 */ 

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace ContosoEventsWeb.Controllers
{
    public class HomeController : Controller
    {

        /*
        * The following declarations are class-level variables for 
        * holding various bits of information from talking to SharePoint.
        */
        bool eVentCalendarExists = false;
        bool isSiteOwner = false;
        List eventCalendar = null;
        Web hostWeb = null;
        List<string> members = new List<string>();

        /*
      * The getSharePointData method uses the SharePoint client-side object model to:
       * 1. Retrive the currently logged-on user's Email address for display in the app
       * 2. Determine whether the required calendar exists in the host SharePoint site
       * 3. Determine group membership details from the host SharePoint site
      */
        private void getSharePointData()
        {
            User spUser = null;
            ListCollection hostLists = null;
            GroupCollection userGroups;
            UserCollection siteMembers;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    hostWeb = clientContext.Web;
                    spUser = hostWeb.CurrentUser;
                    hostLists = hostWeb.Lists;
                    userGroups = spUser.Groups;
                    siteMembers = hostWeb.SiteUsers;
                    clientContext.Load(hostWeb);
                    clientContext.Load(hostLists);
                    clientContext.Load(spUser);
                    clientContext.Load(userGroups);
                    clientContext.Load(siteMembers);
                    clientContext.ExecuteQuery();
                    ViewBag.UserName = spUser.Email;
                    bool calendarFound = false;
                    foreach (List lst in hostLists)
                    {
                        if (lst.BaseTemplate == (int)ListTemplateType.Events)
                        {
                            if (lst.Title == "Contoso Events Calendar")
                            {
                                calendarFound = true;
                                break;
                            }
                        }
                    }
                    eVentCalendarExists = calendarFound;
                    foreach (Group group in userGroups)
                    {
                        if (group.Title.Contains("Owners"))
                        {
                            isSiteOwner = true;
                            break;
                        }
                    }
                    foreach (User member in siteMembers)
                    {
                        bool isOwner = false;
                        bool isMember = false;
                        clientContext.Load(member.Groups);
                        clientContext.ExecuteQuery();
                        foreach (Group memberGroup in member.Groups)
                        {
                            if (memberGroup.Title.Contains("Owners"))
                            {
                                isOwner = true;
                                break;
                            }
                        }
                        if (!isOwner)
                        {
                            foreach (Group memberGroup in member.Groups)
                            {
                                if (memberGroup.Title.Contains("Members"))
                                {
                                    isMember = true;
                                    break;
                                }
                            }
                        }
                        if (isMember)
                        {
                            members.Add(member.Email);
                        }
                    }
                }
            }
        }

        /*
        * The checkDB method verifies that the database can be contacted.
        */
        private string checkDB()
        {
            string message = string.Empty;
            if (ConfigurationManager.ConnectionStrings["EventDB"] != null)
            {
                SqlConnection sqlCon = new SqlConnection();
                try
                {
                    sqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["EventDB"].ConnectionString;
                    sqlCon.Open();
                    // Database Connection
                    message = "Success!";
                    sqlCon.Close();
                }
                catch
                {
                    message = "The data source referred to by the EventDB connectionString cannot be contacted or is incorrectly configured. Please refer to the readme file that accompanies this app for instructions on how to set up the app.";
                }
                finally
                {
                    sqlCon.Dispose();
                }
                return (message);
            }

            // Database Connection
            message = "The EventDB connectionString element has not been set in Web.config. Please refer to the readme file that accompanies this app for instructions on how to set up the app.";
            return (message);
        }

        /*
        * The isCoordinator method verifies whether the current user is a coordinator for a specific event.
        */
        private bool isCoordinator(string EventID)
        {
            Models.EventData eventData = new Models.EventData();
            return (eventData.IsCoordinator(EventID, ViewBag.UserName));
        }

        /*
        * The UploadFiles method coordinates saving a file to Azure BLOB storage with updating the database through a Model class.
        */
        public ActionResult UploadFiles(string EventID)
        {
            bool isSaved = true;
            string savedStatus = string.Empty;
            for (int idx = 0; idx < Request.Files.Count; idx++)
            {
                HttpPostedFileBase file = (HttpPostedFileBase)Request.Files[idx];
                Stream fileStream = file.InputStream;
                string fileName = getFileName(file.FileName);
                Models.EventData eventData = new Models.EventData();
                savedStatus = eventData.AddAttachmentItem(EventID, EventID + "/" + fileName);

                if (savedStatus.StartsWith("Point8020.Success"))
                {
                    savedStatus = SaveFileToAzure(fileStream, file.ContentType, EventID + "/" + fileName);
                    if (savedStatus != "Point8020.Success")
                    {
                        isSaved = false;
                    }
                }
                else
                {
                    isSaved = false;
                }
            }
            if (isSaved == true)
            {
                return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "File could not be saved: Error Code is: " + savedStatus, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The getFileName method is a helper method for manipulating file display names.
        */
        private string getFileName(string fullPath)
        {
            int iPos = fullPath.LastIndexOf(@"\");
            return (fullPath.Substring(iPos + 1));
        }

        /*
        * The SaveFileToAzure method uses Azure classes to upload and save a file to Windows Azure BLOB stroage.
        */
        private string SaveFileToAzure(Stream contents, string contentType, string fileName)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount;
                CloudBlobClient blobClient;
                CloudBlobContainer blobContainer;
                BlobContainerPermissions containerPermissions;
                CloudBlob blob;
                string azureConnection = string.Empty;
                if (ConfigurationManager.ConnectionStrings["EventAzureStroage"] != null)
                {
                    azureConnection = ConfigurationManager.ConnectionStrings["EventAzureStroage"].ConnectionString;
                }
                else
                {
                    return ("Point8020.Error.AzureConnectionNotSet");
                }
                cloudStorageAccount = CloudStorageAccount.Parse(azureConnection);
                blobClient = cloudStorageAccount.CreateCloudBlobClient();
                blobContainer = blobClient.GetContainerReference("eventassets");
                blobContainer.CreateIfNotExist();
                containerPermissions = new BlobContainerPermissions();
                containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                blobContainer.SetPermissions(containerPermissions);
                blob = blobContainer.GetBlobReference(fileName.ToLower());
                blob.Properties.ContentType = contentType;
                blob.UploadFromStream(contents);
                return ("Point8020.Success");
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }


        /*
         * The Index method checks for database connectivity. 
         * It also checks for the existence of a calendar in the host SharePoint site
         * The method also retrieves information about the currently-logged on user
         */
        [SharePointContextFilter]
        public ActionResult Index()
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events App: Overview";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            return View();
        }

        /*
         * The Events method uses the Model classes to retrieve data from the database.
         * The data retrieved from the Model classes represents planned events.
         * The method then builds various members of the ViewBag object, so that the eVents view can render the data.
         */
        [SharePointContextFilter]
        public ActionResult Events()
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events List";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            Models.EventData eventData = new Models.EventData();
            List<Models.Event> events = eventData.EventList(ViewBag.UserName, isSiteOwner);
            ViewBag.Events = events;
            return View();
        }


        /*
         * The About method is a simple MVC ActionResult for showing an 'About' view (if required).
         */
        [SharePointContextFilter]
        public ActionResult About()
        {
            getSharePointData();
            ViewBag.Title = "About Contoso Events";
            ViewBag.Message = "This App was developed by Point8020.com as part of the Showcase Series";
            return View();
        }

        /*
        * The About method is a simple MVC ActionResult for showing error messages.
        */
        [SharePointContextFilter]
        public ActionResult TrappedError(string ErrorMessage)
        {
            getSharePointData();
            ViewBag.Title = "Error";
            ViewBag.Message = ErrorMessage;
            return View();
        }

        /*
         * The CreateCalendar method uses the SharePoint client-side object model to create a calendar in the host site.
         */
        [SharePointContextFilter]
        public ActionResult CreateCalendar()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    ListCollection hostLists = null;
                    ListCreationInformation listCreateInfo = new ListCreationInformation();
                    listCreateInfo.Title = "Contoso Events Calendar";
                    listCreateInfo.TemplateType = (int)ListTemplateType.Events; //Calendar

                    hostWeb = clientContext.Web;
                    hostLists = hostWeb.Lists;
                    eventCalendar = hostLists.Add(listCreateInfo);

                    clientContext.Load(hostWeb);
                    clientContext.Load(hostLists);
                    clientContext.Load(eventCalendar);
                    clientContext.ExecuteQuery();
                }
            }

            return (RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }


        /*
        * The CreateEvent method uses the Model classes to interact with data in the database.
        * It also uses the SharePoint client-side object model to create a corresponding calendar item
        */
        [SharePointContextFilter]
        public ActionResult CreateEvent(string EventTitle, string EventVenue, string EventAddress1, string EventAddress2, string EventCity, string EventState, string EventPostalCode, DateTime EventStartDateTime, DateTime EventEndDateTime, string EventDescription)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.AddEvent(EventTitle, EventVenue, EventAddress1, EventAddress2, EventCity, EventState, EventPostalCode, EventStartDateTime.ToUniversalTime(), EventEndDateTime.ToUniversalTime(), EventDescription);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Event could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                using (var clientContext = spContext.CreateUserClientContextForSPHost())
                {
                    if (clientContext != null)
                    {
                        hostWeb = clientContext.Web;
                        clientContext.Load(hostWeb);
                        ListCollection hostLists = hostWeb.Lists;
                        clientContext.Load(hostLists);
                        clientContext.ExecuteQuery();
                        bool calendarFound = false;
                        foreach (List lst in hostLists)
                        {
                            if (lst.BaseTemplate == (int)ListTemplateType.Events)
                            {
                                if (lst.Title == "Contoso Events Calendar")
                                {
                                    calendarFound = true;
                                    break;
                                }
                            }
                        }
                        eVentCalendarExists = calendarFound;
                        if (calendarFound)
                        {
                            List calendar = hostWeb.Lists.GetByTitle("Contoso Events Calendar");
                            clientContext.Load(calendar);
                            clientContext.ExecuteQuery();
                            ListItemCreationInformation itemInfo = new ListItemCreationInformation();
                            ListItem newEvent = calendar.AddItem(itemInfo);
                            newEvent["Title"] = EventTitle;
                            newEvent["Location"] = EventCity;
                            newEvent["EventDate"] = EventStartDateTime.ToUniversalTime();
                            newEvent["EndDate"] = EventEndDateTime.ToUniversalTime();
                            newEvent["Category"] = saved;

                            newEvent.Update();
                            clientContext.ExecuteQuery();
                        }
                    }
                }

                return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SaveEvent method uses the Model classes to interact with data in the database.
        * It also uses the SharePoint client-side object model to update corresponding calendar items
        */
        [SharePointContextFilter]
        public ActionResult SaveEvent(string EventID, string EventTitle, string EventVenue, string EventAddress1, string EventAddress2, string EventCity, string EventState, string EventPostalCode, DateTime EventStartDateTime, DateTime EventEndDateTime, string EventDescription)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdateEvent(EventID, EventTitle, EventVenue, EventAddress1, EventAddress2, EventCity, EventState, EventPostalCode, EventStartDateTime.ToUniversalTime(), EventEndDateTime.ToUniversalTime(), EventDescription);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Event could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                using (var clientContext = spContext.CreateUserClientContextForSPHost())
                {
                    if (clientContext != null)
                    {
                        hostWeb = clientContext.Web;
                        clientContext.Load(hostWeb);
                        ListCollection hostLists = hostWeb.Lists;
                        clientContext.Load(hostLists);
                        clientContext.ExecuteQuery();
                        bool calendarFound = false;
                        foreach (List lst in hostLists)
                        {
                            if (lst.BaseTemplate == (int)ListTemplateType.Events)
                            {
                                if (lst.Title == "Contoso Events Calendar")
                                {
                                    calendarFound = true;
                                    break;
                                }
                            }
                        }
                        eVentCalendarExists = calendarFound;
                        if (calendarFound)
                        {
                            List calendar = hostWeb.Lists.GetByTitle("Contoso Events Calendar");

                            clientContext.Load(calendar);
                            clientContext.ExecuteQuery();
                            CamlQuery camlQuery = new CamlQuery();
                            camlQuery.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Category' /><Value Type='Text'>" + EventID + "</Value></Eq></Where></Query></View>";
                            ListItemCollection eventItems = calendar.GetItems(camlQuery);
                            clientContext.Load(eventItems);
                            clientContext.ExecuteQuery();
                            ListItem editEvent = eventItems[0];


                            editEvent["Title"] = EventTitle;
                            editEvent["Location"] = EventCity;
                            editEvent["EventDate"] = EventStartDateTime.ToUniversalTime();
                            editEvent["EndDate"] = EventEndDateTime.ToUniversalTime();
                            editEvent["Category"] = saved;

                            editEvent.Update();
                            clientContext.ExecuteQuery();
                        }
                    }
                    return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
                }
            }
        }

        /*
        * The DeleteEvent method uses the Model classes to interact with data in the database.
        * It also uses the SharePoint client-side object model to delete the corresponding calendar item
        */
        public ActionResult DeleteEvent(string EventID)
        {
            Models.EventData eData = new Models.EventData();
            bool result = eData.DeleteEvent(EventID);
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    hostWeb = clientContext.Web;
                    clientContext.Load(hostWeb);
                    ListCollection hostLists = hostWeb.Lists;
                    clientContext.Load(hostLists);
                    clientContext.ExecuteQuery();
                    bool calendarFound = false;
                    foreach (List lst in hostLists)
                    {
                        if (lst.BaseTemplate == (int)ListTemplateType.Events)
                        {
                            if (lst.Title == "Contoso Events Calendar")
                            {
                                calendarFound = true;
                                break;
                            }
                        }
                    }
                    eVentCalendarExists = calendarFound;
                    if (calendarFound)
                    {
                        List calendar = hostWeb.Lists.GetByTitle("Contoso Events Calendar");

                        clientContext.Load(calendar);
                        clientContext.ExecuteQuery();
                        CamlQuery camlQuery = new CamlQuery();
                        camlQuery.ViewXml = "<View><Query><Where><Eq><FieldRef Name='Category' /><Value Type='Text'>" + EventID + "</Value></Eq></Where></Query></View>";
                        ListItemCollection eventItems = calendar.GetItems(camlQuery);
                        clientContext.Load(eventItems);
                        clientContext.ExecuteQuery();
                        foreach (ListItem eventToDelete in eventItems)
                        {
                            eventToDelete.DeleteObject();
                        }
                        clientContext.ExecuteQuery();
                    }
                }
                return (RedirectToAction("events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The Agenda method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult Agenda(string EventID)
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events Agenda";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            ViewBag.IsCoordinator = isCoordinator(EventID);
            Models.EventData eventData = new Models.EventData();
            List<Models.EventAgenda> agendaItems = eventData.Agenda(EventID);
            ViewBag.EventID = EventID;
            ViewBag.AgendaItems = agendaItems;
            return View();
        }

        /*
        * The CreateAgendaItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult CreateAgendaItem(string eventID, string title, string description)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.AddAgendaItem(eventID, title, description);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Agenda", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SaveAgendaItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult SaveAgendaItem(string eventID, string itemID, string title, string description)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdateAgendaItem(eventID, itemID, title, description);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Agenda", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The DeleteAgendaItem method uses the Model classes to interact with data in the database.
        */
        public ActionResult DeleteAgendaItem(string eventID, string itemID)
        {
            Models.EventData eventData = new Models.EventData();
            string saved = eventData.DeleteAgendaItem(itemID);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be deleted: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Agenda", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SaveAgenda method uses the Model classes to interact with data in the database.
        */
        public ActionResult SaveAgenda(string eventID, string items)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdateAgenda(items);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Agenda could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The Presenters method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult Presenters(string EventID)
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events Presenters";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            ViewBag.IsCoordinator = isCoordinator(EventID);
            Models.EventData eventData = new Models.EventData();
            List<Models.EventPresenter> presenterItems = eventData.Presenters(EventID);
            ViewBag.EventID = EventID;
            ViewBag.PresenterItems = presenterItems;
            return View();
        }

        /*
        * The CreatePresenterItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult CreatePresenterItem(string eventID, string title, string description)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.AddPresenterItem(eventID, title, description);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Presenters", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SavePresenterItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult SavePresenterItem(string eventID, string itemID, string title, string description)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdatePresenterItem(eventID, itemID, title, description);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Presenters", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The DeletePresenterItem method uses the Model classes to interact with data in the database.
        */
        public ActionResult DeletePresenterItem(string eventID, string itemID)
        {
            Models.EventData eventData = new Models.EventData();
            string saved = eventData.DeletePresenterItem(itemID);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be deleted: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Presenters", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SavePresenter method uses the Model classes to interact with data in the database.
        */
        public ActionResult SavePresenter(string eventID, string items)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdatePresenter(items);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Presenter could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The Attendees method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult Attendees(string EventID)
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events Attendees";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            ViewBag.IsCoordinator = isCoordinator(EventID);
            Models.EventData eventData = new Models.EventData();
            List<Models.EventAttendee> attendeeItems = eventData.Attendees(EventID);
            ViewBag.EventID = EventID;
            ViewBag.AttendeeItems = attendeeItems;
            return View();
        }

        /*
        * The CreateAttendeeItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult CreateAttendeeItem(string eventID, string name, string email)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.AddAttendeeItem(eventID, name, email);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Attendees", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SaveAttendeeItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult SaveAttendeeItem(string eventID, string itemID, string name, string email)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdateAttendeeItem(eventID, itemID, name, email);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Attendees", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The DeleteAttendeeItem method uses the Model classes to interact with data in the database.
        */
        public ActionResult DeleteAttendeeItem(string eventID, string itemID)
        {
            Models.EventData eventData = new Models.EventData();
            string saved = eventData.DeleteAttendeeItem(itemID);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be deleted: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Attendees", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The Catering method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult Catering(string EventID)
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events Catering";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            ViewBag.IsCoordinator = isCoordinator(EventID);
            Models.EventData eventData = new Models.EventData();
            List<Models.EventCatering> cateringItems = eventData.Catering(EventID);
            ViewBag.EventID = EventID;
            ViewBag.CateringItems = cateringItems;
            return View();
        }

        /*
        * The CreateCateringItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult CreateCateringItem(string eventID, string title, string description)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.AddCateringItem(eventID, title, description);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Catering", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SaveCateringItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult SaveCateringItem(string eventID, string itemID, string title, string description)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdateCateringItem(eventID, itemID, title, description);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Catering", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The DeleteCateringItem method uses the Model classes to interact with data in the database.
        */
        public ActionResult DeleteCateringItem(string eventID, string itemID)
        {
            Models.EventData eventData = new Models.EventData();
            string saved = eventData.DeleteCateringItem(itemID);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be deleted: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Catering", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The SaveCatering method uses the Model classes to interact with data in the database.
        */
        public ActionResult SaveCatering(string eventID, string items)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.UpdateCatering(items);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Catering could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The Attachments method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult Attachments(string EventID)
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events Attachments";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            ViewBag.IsCoordinator = isCoordinator(EventID);
            Models.EventData eventData = new Models.EventData();
            List<Models.EventAttachment> AttachmentItems = eventData.Attachments(EventID);
            ViewBag.EventID = EventID;
            ViewBag.AttachmentItems = AttachmentItems;
            ViewBag.SPContextUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri;
            return View();
        }

        /*
        * The CreateAttachmentItem method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult CreateAttachmentItem(string eventID, string url)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.AddAttachmentItem(eventID, url);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Attachments", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The DeleteAttachmentItem method uses the Model classes to interact with data in the database.
        */
        public ActionResult DeleteAttachmentItem(string eventID, string itemID)
        {
            Models.EventData eventData = new Models.EventData();
            string saved = eventData.DeleteAttachmentItem(itemID);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Item could not be deleted: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Attachments", "Home", new { EventID = eventID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The Roles method uses the Model classes to interact with data in the database.
        */
        [SharePointContextFilter]
        public ActionResult Roles(string EventID)
        {
            getSharePointData();
            ViewBag.Title = "Contoso Events Security";
            ViewBag.DatabaseCheck = checkDB();
            ViewBag.CalendarCheck = eVentCalendarExists;
            ViewBag.IsSiteOwner = isSiteOwner;
            ViewBag.IsCoordinator = isCoordinator(EventID);
            Models.EventData eventData = new Models.EventData();
            List<Models.EventRole> coordinators = eventData.Coordinators(EventID);
            foreach (Models.EventRole coordinator in coordinators)
            {
                members.Remove(coordinator.MemberName);
            }
            ViewBag.SiteMembers = members;
            ViewBag.EventID = EventID;
            ViewBag.Coordinators = coordinators;
            return View();
        }

        /*
        * The SaveRoles method uses the Model classes to interact with data in the database.
        */
        public ActionResult SaveRoles(string eventID, string items)
        {

            Models.EventData eventData = new Models.EventData();
            string saved = eventData.SaveRoles(eventID, items);
            if (saved.StartsWith("Point8020.Error"))
            {
                return (RedirectToAction("TrappedError", "Home", new { ErrorMessage = "Security settings could not be saved: Error Code is: " + saved, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            else
            {
                return (RedirectToAction("Events", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

      
    }

}
