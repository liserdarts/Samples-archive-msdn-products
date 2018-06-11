using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.SAPSK.ContosoTours;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTCONNLIST;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public static class SeedDataHelper
    {
        private static string[] packages = new string[] { 
            "WORLD LEADERS", 
            "ADMINISTRATION AROUND THE WORLD", 
            "DEVELOPMENT AROUND THE WORLD",
            "VISUAL STUDIO TOOLS FOR OFFICE AROUND THE WORLD"};

        private static string[] _classType = new string[] { "Y", "C", "F" };

        private static SortedDictionary<string, string> cityCountry = null;

        private static SAPFlightConnection _flightConnection = null;

        private static DateTime FlightDate(string flightDate)
        {
            return new DateTime(Convert.ToInt32(flightDate.Substring(0, 4)),
                Convert.ToInt32(flightDate.Substring(5, 2)),
                Convert.ToInt32(flightDate.Substring(8, 2)));
        }

        private static bool IsValidFlightDate(string dateOfFlight, DateTime eventDate, DateTime previousDate)
        {
            DateTime flightDate = FlightDate(dateOfFlight);

            if (flightDate > eventDate)
            {
                return false;
            }
            else if (flightDate < DateTime.Now)
            {
                return false;
            }
            else if (flightDate < previousDate)
            {
                return false;
            }
            return true;
        }

        private static bool IsFlightAvailable(BAPISCODAT item, string classType)
        {
            bool isAvailable = true;

            #region check availability
            if (!_flightConnection.GetDetail(
                item.FLIGHTCONN,
                item.FLIGHTDATE,
                "",
                item.AGENCYNUM))
            {
                return false;
            }
           
            if (_flightConnection._bapiAvailability.Length > 0)
            {
                for (int counter = 0; counter < _flightConnection._bapiAvailability.Length; counter++)
                {
                    switch (classType)
                    {
                        case "Y":
                            if (_flightConnection._bapiAvailability[counter].ECONOFREE == 0)
                            {
                                isAvailable = false;
                            }
                            break;
                        case "C":
                            if (_flightConnection._bapiAvailability[counter].BUSINFREE == 0)
                            {
                                isAvailable = false;
                            }
                            break;
                        case "F":
                            if (_flightConnection._bapiAvailability[counter].FIRSTFREE == 0)
                            {
                                isAvailable = false;
                            }
                            break;
                    }
                }
            }
            #endregion

            return isAvailable;
        }

        private static SeedEventDataList[] GetEvent(int packageID)
        {
            ArrayList arrEvent = new ArrayList();
            
            SAPPackageEventReadOnly packageEvent =
                new SAPPackageEventReadOnly(Config._dbConnectionName);

            using (SAPDataReaderPackageEvent rdrEvent =
                packageEvent.ReaderSelectByPackageID(packageID))
            {
                if (rdrEvent.DataReader != null &&
                    rdrEvent.DataReader.HasRows)
                {
                    while (rdrEvent.DataReader.Read()) 
                    {
                        SeedEventDataList data = new SeedEventDataList();
                        data.VenueCity = rdrEvent.VenueCity;
                        data.EventID = rdrEvent.EventID;
                        data.EventDate = rdrEvent.EventDate;
                        arrEvent.Add(data);
                    } //while (rdrEvent.DataReader.Read());
                }
            }
            return (SeedEventDataList[])arrEvent.ToArray(typeof(SeedEventDataList));
        }

        private static void SetCityCountry()
        {
            cityCountry.Add("TOKYO", "JPJA");
            cityCountry.Add("OSAKA", "JPJA" );
            cityCountry.Add("FRANKFURT", "DEDE");
            cityCountry.Add("BERLIN", "DEDE");
            cityCountry.Add("ROME", "ITIT");
            cityCountry.Add("SINGAPORE", "SGZH");
            cityCountry.Add("SAN FRANCISCO", "USEN");
        }

        private static string GetCountry(string city, out string language)
        {
            if (cityCountry.Count > 0 &&
                cityCountry.ContainsKey(city.ToUpper()))
            {
                string value = cityCountry[city.ToUpper()];
                language = value.Substring(2);
                return value.Substring(0,2);
            }
            else
            {
                language = "EN";
                return "US";
            }
        }

        public static void CreateSeedData()
        {
            #region create random name/date of birth list
            Random rand=new Random((int)DateTime.Now.Ticks);
            int index = 0;
            List<string> names = new List<string>();
            List<string> nameList=
                Properties.Resources.ValidNames.Split(
                    Environment.NewLine.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            DateTime[] dobs = new DateTime[Config.SeedDataLimit*4+1];

            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;

            int nameCounter = 0;
            do
            {
                if(!names.Contains(nameList[nameCounter]))
                {
                    names.Add(nameList[nameCounter]);
                    index = rand.Next(year - 40, year);
                    dobs[names.Count - 1] = new DateTime(index, month, day);
                    nameCounter++;
                }
            } while (names.Count < Config.SeedDataLimit*4+1);
            
            #endregion

            cityCountry =
                new SortedDictionary<string, string>();

            SetCityCountry();
                    
            #region Clear Tables
            SAPEventActorMapReadWrite eventActorMapRW =
                new SAPEventActorMapReadWrite(Config._dbConnectionName);

            SAPDataSetEventActorMap eventActorMapDataSet = eventActorMapRW.SelectAll();

            foreach (SAPDataSetEventActorMap.EventActorMapRow row in
                eventActorMapDataSet.EventActorMap.Rows)
            {
                eventActorMapRW.Delete(row.EventActorMapID);
            }

            SAPEventAttendeeAgencyMapReadWrite eventAttendeeAgencyMapRW =
                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);

            SAPDataSetEventAttendeeAgencyMap eventAttendeeAgencyMapDataSet =
                eventAttendeeAgencyMapRW.SelectAll();

            foreach (SAPDataSetEventAttendeeAgencyMap.EventAttendeeAgencyMapRow row
                in eventAttendeeAgencyMapDataSet.EventAttendeeAgencyMap.Rows)
            {
                eventAttendeeAgencyMapRW.Delete(row.EventAttendeeAgencyMapID);
            }


            SAPEventAttendeeReadWrite eventAttendeeRW =
                new SAPEventAttendeeReadWrite(Config._dbConnectionName);

            SAPDataSetEventAttendee eventAttendeeDataSet = eventAttendeeRW.SelectAll();

            foreach (SAPDataSetEventAttendee.EventAttendeeRow row in
                eventAttendeeDataSet.EventAttendee.Rows)
            {
                eventAttendeeRW.Delete(row.EventAttendeeID);
            }

            SAPEventActorReadWrite eventActorRW =
                new SAPEventActorReadWrite(Config._dbConnectionName);

            SAPDataSetEventActor eventActorDataSet = eventActorRW.SelectAll();

            foreach (SAPDataSetEventActor.EventActorRow row in
                eventActorDataSet.EventActor.Rows)
            {
                eventActorRW.Delete(row.EventActorID);
            }

            SAPPackageEventMapReadWrite packageEventMapRW =
                new SAPPackageEventMapReadWrite(Config._dbConnectionName);

            SAPDataSetPackageEventMap packageEventMapDataset = packageEventMapRW.SelectAll();

            foreach (SAPDataSetPackageEventMap.PackageEventMapRow row in
                packageEventMapDataset.PackageEventMap.Rows)
            {
                packageEventMapRW.Delete(row.PackageEventMapID);
            }

            SAPPackageReadWrite packageRW =
                new SAPPackageReadWrite(Config._dbConnectionName);

            SAPDataSetPackage packageDataSet = packageRW.SelectAll();

            foreach (SAPDataSetPackage.PackageRow row in
                packageDataSet.Package.Rows)
            {
                packageRW.Delete(row.PackageID);
            }

            SAPEventReadWrite eventRW =
                new SAPEventReadWrite(Config._dbConnectionName);

            SAPDataSetEvent eventDataSet = eventRW.SelectAll();

            foreach (SAPDataSetEvent.EventRow row in eventDataSet.Event.Rows)
            {
                eventRW.Delete(row.EventID);
            }

           
            SAPEventTypeReadWrite eventTypeRW =
                new SAPEventTypeReadWrite(Config._dbConnectionName);

            SAPDataSetEventType eventTypeDataSet = eventTypeRW.SelectAll();

            foreach (SAPDataSetEventType.EventTypeRow row in
                eventTypeDataSet.EventType.Rows)
            {
                eventTypeRW.Delete(row.EventTypeID);
            }

            #endregion

            #region Repopulate Tables

            SAPEventTypeReadWrite seedEventTypeRW =
                new SAPEventTypeReadWrite(Config._dbSeedConnectionName);

            SAPDataSetEventType seedEventTypeDataSet =
                seedEventTypeRW.SelectAll();

            int retEventTypeID = 0;

            foreach (SAPDataSetEventType.EventTypeRow row in
                seedEventTypeDataSet.EventType.Rows)
            {
                eventTypeRW.Insert(row.EventTypeName, row.EventTypeDescription, out retEventTypeID);
            }
            

            SAPEventReadWrite seedEventRW =
                new SAPEventReadWrite(Config._dbSeedConnectionName);

            SAPDataSetEvent seedEventDataSet = seedEventRW.SelectAll();

            int retEventID = 0;

            //check the difference in seed date to adjust the event dates
            long monthDiff = UtilityHelper.GetMonthDifference(
                Config.DateLastSeed,
                DateTime.Now);

            foreach (SAPDataSetEvent.EventRow row in
                seedEventDataSet.Event.Rows)
            {
             
                using (SAPDataReaderEventType rdrSeedEventType =
                    eventTypeRW.ReaderSelectByEventTypeName(row.EventTypeName))
                {
                    if (rdrSeedEventType.DataReader != null &&
                        rdrSeedEventType.DataReader.HasRows)
                    {
                        DateTime newDate = new DateTime();
                        //long monthDifference =
                        //    UtilityHelper.GetMonthDifference(DateTime.Now, row.EventDate);
                        
                        newDate = row.EventDate.AddMonths((int)monthDiff);
                        
                        rdrSeedEventType.DataReader.Read();
                        eventRW.Insert(row.VenueID, rdrSeedEventType.EventTypeID, row.EventName,
                            row.EventDescription, row.EventPhoto, newDate, row.GoldPackagePrice,
                            row.SilverPackagePrice, row.BronzePackagePrice, row.GoldPackageTrueCost,
                            row.SilverPackageTrueCost, row.BronzePackageTrueCost, row.EventTotalCost,
                            out retEventID);
                    }
                }
            }

            SAPPackageReadWrite seedPackageRW =
                new SAPPackageReadWrite(Config._dbSeedConnectionName);

            SAPDataSetPackage seedPackageDataset =
                seedPackageRW.SelectAll();

            int retPackageID = 0;

           
            foreach (SAPDataSetPackage.PackageRow row in
                seedPackageDataset.Package.Rows)
            {
                packageRW.Insert(row.PackageName, row.PackageDescription, 
                    row.PackageImage, out retPackageID);
            }

            int retPackageEventMapID = 0;
            int[] seedNumberOfEventPerPackage = { 4, 3,4, 4 };
            int packageIndex = 0;

            using (SAPDataReaderPackage rdrPackage = packageRW.ReaderSelectAll())
            {
                if (rdrPackage.DataReader != null &&
                    rdrPackage.DataReader.HasRows)
                {
                    int eventIdx = 0;

                    while (rdrPackage.DataReader.Read())
                    {
                        int eventID = 0;
                        for (int idx = 0; idx < seedNumberOfEventPerPackage[packageIndex]; idx++)
                        {
                            using (SAPDataReaderEvent rdrEvent = eventRW.ReaderSelectAll())
                            {
                                if (rdrEvent.DataReader != null &&
                                     rdrEvent.DataReader.HasRows)
                                {

                                    if (eventIdx == 0)
                                    {
                                        rdrEvent.DataReader.Read();
                                        eventID = rdrEvent.EventID;
                                    }
                                    else
                                    {
                                        for (int eidx = 0; eidx <= eventIdx; eidx++)
                                        {
                                            rdrEvent.DataReader.Read();
                                            eventID = rdrEvent.EventID;
                                        }
                                    }
                                }
                            }
                            packageEventMapRW.Insert(rdrPackage.PackageID, eventID, out retPackageEventMapID);
                            eventIdx++;
                        }
                        packageIndex++;
                    }

                }

            }

            #endregion

            int currentNameIndex = 0;

            //get the 3 package here
       
            SAPPackageEventReadOnly packageEvent =
                new SAPPackageEventReadOnly(Config._dbConnectionName);

            _flightConnection =
                new SAPFlightConnection(Config.SAPUserName, Config.SAPPassword);

            #region generate data
            using (SAPDataReaderPackage rdrPackage =
                packageRW.ReaderSelectAll())
            {
                if (rdrPackage.DataReader != null
                    && rdrPackage.DataReader.HasRows)
                {
                    while (rdrPackage.DataReader.Read()) 
                    { 
                        if (Array.IndexOf(packages, rdrPackage.PackageName.ToUpper().Trim()) > -1)
                        {
                            List<string> location = new List<string>();

                            SeedEventDataList[] flights = GetEvent(rdrPackage.PackageID);

                            for (int i = 1; i <= Config.SeedDataLimit; i++)
                            {
                                DateTime previousDate = DateTime.MinValue;
                                string cityFrom = string.Empty;

                                #region iterate for each event
                                for (int idx = 0; idx < flights.Length; idx++)
                                {
                                    BAPISCODAT selectedItem = null;
                                    string selectedClass = string.Empty;
                                    if (_flightConnection.GetList("", Config.SeedTravelAgency, cityFrom, flights[idx].VenueCity))
                                    {
                                        List<int> classTypeIndex = new List<int>();
                                        BAPISCODAT[] flightList = _flightConnection._bapiConnectionList;
                                        do
                                        {
                                            #region random classtype
                                            if (classTypeIndex.Count < 3)
                                            {
                                                do
                                                {
                                                    int ridx = rand.Next(0, 3);
                                                    if (!classTypeIndex.Contains(ridx))
                                                    {
                                                        classTypeIndex.Add(ridx);
                                                        break;
                                                    }
                                                } while (true);
                                            }
                                            string classType = _classType[classTypeIndex[classTypeIndex.Count - 1]];
                                            #endregion

                                            foreach (BAPISCODAT item in flightList)
                                            {
                                                bool isAvailable = true;

                                                if (!IsValidFlightDate(item.FLIGHTDATE, flights[idx].EventDate, previousDate))
                                                {
                                                    continue;
                                                }

                                                isAvailable = IsFlightAvailable(item, classType);

                                                #region check if city to is not found on previous list
                                                //make sure that this is the startup point of the event
                                                if (isAvailable)
                                                {
                                                    if (previousDate == DateTime.MinValue)
                                                    {
                                                        if (location.Contains(item.CITYFROM))
                                                        {
                                                            //mark this in case there would be no more cityfrom on the list
                                                            if (selectedItem == null)
                                                            {
                                                                selectedItem = item;
                                                                selectedClass = classType;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            location.Add(item.CITYFROM);
                                                            selectedItem = item;
                                                            selectedClass = classType;
                                                            classTypeIndex.Add(1);
                                                            classTypeIndex.Add(2);
                                                            classTypeIndex.Add(3);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        selectedItem = item;
                                                        selectedClass = classType;
                                                        classTypeIndex.Add(1);
                                                        classTypeIndex.Add(2);
                                                        classTypeIndex.Add(3);
                                                        break;
                                                    }
                                                }
                                                #endregion
                                            }
                                        } while (classTypeIndex.Count < 3);
                                    }
                                    previousDate = flights[idx].EventDate;
                                    if (selectedItem != null)
                                    {
                                        SeedEventDataList eventData = flights[idx];
                                        eventData.AgencyNum = selectedItem.AGENCYNUM;
                                        eventData.FlightConnNo = selectedItem.FLIGHTCONN;
                                        eventData.CityFrom = selectedItem.CITYFROM;
                                        eventData.FlightDate = selectedItem.FLIGHTDATE;
                                        eventData.ClassType = selectedClass;
                                        flights[idx] = eventData;
                                        cityFrom = eventData.VenueCity;
                                    }
                                }
                                #endregion

                                #region verify if there's flight
                                bool isReady = true;
                                for (int idx = 0; idx < flights.Length; idx++)
                                {
                                    if (String.IsNullOrEmpty(flights[idx].FlightConnNo))
                                    {
                                        isReady = false;
                                    }
                                }
                                #endregion

                                if (isReady)
                                {
                                    string customerCity = flights[0].CityFrom;
                                    string customerName = names[currentNameIndex];
                                    string language = string.Empty;
                                    string country = GetCountry(customerCity, out language);
                                    string dob =
                                        string.Format("{0:yyyy-MM-dd}", dobs[currentNameIndex]);
                                    string customerNumber = string.Empty;
                                    bool isError = false;

                                    #region save customer
                                    SAPCustomer customer =
                                        new SAPCustomer(Config.SAPUserName, Config.SAPPassword);

                                    if (customer.CreateFromData(
                                       customerCity,
                                       country,
                                       customerName,
                                       "P",
                                       "none",
                                       language,
                                       "N/A",
                                       "",
                                       ""))
                                    {
                                        //get customer number of inserted customer
                                        bool found = false;
                                        while (!found)
                                        {
                                            customer.GetList();
                                            if (customer._customerList.Length > 0)
                                            {
                                                //search last 3
                                                for (int j = customer._customerList.Length - 1;
                                                     j > (customer._customerList.Length - 3);
                                                     j--)
                                                {
                                                    if (customer._customerList[j].CUSTNAME.Trim() == customerName.Trim())
                                                    {
                                                        customerNumber = customer._customerList[j].CUSTOMERID;
                                                        found = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        isError = true;
                                    }
                                    #endregion

                                    #region create flight trip
                                    if (customerNumber.Length > 0)
                                    {
                                        SAPFlightTrip flightTrip =
                                            new SAPFlightTrip(Config.SAPUserName, Config.SAPPassword);
                                        for (int idx = 0; idx < flights.Length; idx++)
                                        {
                                            SeedEventDataList item = flights[idx];
                                            string tripNumber;
                                            string travelAgencyNumber;
                                            if (flightTrip.CreateTrip(
                                                item.AgencyNum,
                                                item.ClassType,
                                                customerNumber,
                                                item.FlightConnNo,
                                                "",
                                                item.FlightDate,
                                                "",
                                                "none",
                                                dob,
                                                customerName,
                                                out travelAgencyNumber,
                                                out tripNumber))
                                            {
                                                flights[idx].TripNumber = tripNumber;
                                            }
                                            else
                                            {
                                                isError = true;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region save to local db
                                    if (!isError)
                                    {
                                        int eventAttendeeID = 0;
                                        SAPEventAttendeeReadWrite eventAttendee =
                                            new SAPEventAttendeeReadWrite(Config._dbConnectionName);
                                        DateTime dateCreate;
                                        eventAttendee.Insert(
                                            rdrPackage.PackageID,
                                            customerNumber,
                                            DateTime.Now,
                                            out dateCreate,
                                            out eventAttendeeID);

                                        for (int idx = 0; idx < flights.Length; idx++)
                                        {
                                            int eventAttendMapID = 0;
                                            SAPEventAttendeeAgencyMapReadWrite eventMap =
                                                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);
                                            eventMap.Insert(
                                                eventAttendeeID,
                                                flights[idx].EventID,
                                                flights[idx].AgencyNum,
                                                flights[idx].TripNumber,
                                                out eventAttendMapID);
                                        }
                                    }
                                    #endregion
                                    
                                    currentNameIndex++;
                                    
                                }
                                //reset flights
                                for (int idx = 0; idx < flights.Length; idx++)
                                {
                                    flights[idx].AgencyNum = string.Empty;
                                    flights[idx].ClassType = string.Empty;
                                    flights[idx].FlightConnNo = string.Empty;
                                    flights[idx].FlightDate = string.Empty;
                                    flights[idx].TripNumber = string.Empty;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            Config.UpdateKey(Config._keySeedData, "true");
            Config.UpdateKey(Config._keyDateLastSeed, DateTime.Now.ToString());

        }
    }
}
