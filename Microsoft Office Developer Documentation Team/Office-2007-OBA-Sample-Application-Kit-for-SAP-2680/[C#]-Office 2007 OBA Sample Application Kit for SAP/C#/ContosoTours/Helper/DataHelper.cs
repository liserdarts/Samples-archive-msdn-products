using System;
using System.Data;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Schema;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using System.Collections.Generic;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public static class DataHelper
    {
        #region Sales

        public static Sales GetSalesData()
        {
            return GetSalesData(0, 0);
        }

        public static Sales GetSalesData(int year)
        {
            return GetSalesData(0, year);
        }

        public static Sales GetSalesData(int month, int year)
        {
            Sales salesDataset = new Sales();

            Sales.PackageDataTable salesPackageDataTable = salesDataset.Package;
            Sales.PackageSaleDataTable salesPackageSaleDataTable = salesDataset.PackageSale;

            Sales.EventDataTable salesEventDataTable = salesDataset.Event;
            Sales.EventSaleDataTable salesEventSaleDataTable = salesDataset.EventSale;

            Sales.FlightDataTable salesFlightDataTable = salesDataset.Flight;

            SAPPackageReadWrite package =
                new SAPPackageReadWrite(Config._dbConnectionName);

            SAPEventAttendeeReadWrite attendee =
                new SAPEventAttendeeReadWrite(Config._dbConnectionName);

            SAPEventAttendeeAgencyMapReadWrite eventAttendeeAgencyMap =
                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);

            SAPPackageEventMapReadWrite packageEventMap =
                new SAPPackageEventMapReadWrite(Config._dbConnectionName);

            SAPEventReadWrite events =
                new SAPEventReadWrite(Config._dbConnectionName);

            SAPFlightTrip flightTripHelper =
                new SAPFlightTrip(Config.SAPUserName, Config.SAPPassword);

            SAPFlightConnection flightConnectionHelper =
                new SAPFlightConnection(Config.SAPUserName, Config.SAPPassword);

            //fetch all packages
            SAPDataSetPackage packageDataset = package.SelectAll();

            //iterate through the packages
            foreach (SAPDataSetPackage.PackageRow packageRow in packageDataset.Package.Rows)
            {
                Sales.PackageRow salesPackageRow = 
                    salesPackageDataTable.AddPackageRow(packageRow.PackageID, packageRow.PackageName);

                SAPDataSetEventAttendee attendeeDataset =
                    attendee.SelectByPackageID(packageRow.PackageID);

                SAPDataSetPackageEventMap packageEventMapDataset =
                    packageEventMap.SelectByPackageID(packageRow.PackageID);

                foreach (SAPDataSetEventAttendee.EventAttendeeRow eventAttendeeRow in attendeeDataset.EventAttendee.Rows)
                {
                    if ((eventAttendeeRow.Created.Month == month || month == 0) &&
                        (eventAttendeeRow.Created.Year == year || year == 0))
                    {
                        //build temp package table
                        Sales.PackageSaleRow salesPackageSaleRow =
                            salesPackageSaleDataTable.AddPackageSaleRow(
                                salesPackageRow,
                                eventAttendeeRow.EventAttendeeID,
                                eventAttendeeRow.Created,
                                eventAttendeeRow.DateOfBirth,
                                eventAttendeeRow.CustomerNumber,
                                0,
                                0);

                        SAPDataSetEventAttendeeAgencyMap eventAttendeeAgencyMapDataset =
                            eventAttendeeAgencyMap.SelectByEventAttendeeID(eventAttendeeRow.EventAttendeeID);

                        for (int i = 0; i < packageEventMapDataset.PackageEventMap.Rows.Count; i++)
                        {
                            SAPDataSetPackageEventMap.PackageEventMapRow packageEventMapRow =
                                packageEventMapDataset.PackageEventMap.Rows[i] as
                                SAPDataSetPackageEventMap.PackageEventMapRow;

                            SAPDataSetEvent eventDataset =
                                events.SelectByEventID(packageEventMapRow.EventID);

                            SAPDataSetEvent.EventRow eventRow = eventDataset.Event[0];

                            //assumes same record position as to event, need to revise!!!
                            //SAPDataSetEventAttendeeAgencyMap.EventAttendeeAgencyMapRow eventAttendeeAgencyMapRow =
                            //    eventAttendeeAgencyMapDataset.EventAttendeeAgencyMap.Rows[i] as
                            //    SAPDataSetEventAttendeeAgencyMap.EventAttendeeAgencyMapRow;

                            DataRow[] eventAttendeeAgencyMapDataRow =
                                eventAttendeeAgencyMapDataset.EventAttendeeAgencyMap.Select("EventID = " +
                                                                                            packageEventMapRow.EventID);

                            SAPDataSetEventAttendeeAgencyMap.EventAttendeeAgencyMapRow eventAttendeeAgencyMapRow =
                                eventAttendeeAgencyMapDataRow[0] as
                                SAPDataSetEventAttendeeAgencyMap.EventAttendeeAgencyMapRow;

                            Sales.EventRow salesEventRow = salesEventDataTable.FindByEventID(eventRow.EventID);
                            if (salesEventRow == null)
                            {
                                salesEventRow = salesEventDataTable.AddEventRow(
                                    eventRow.EventID,
                                    eventRow.EventName,
                                    eventRow.GoldPackagePrice,
                                    eventRow.SilverPackagePrice,
                                    eventRow.BronzePackagePrice,
                                    eventRow.GoldPackageTrueCost,
                                    eventRow.SilverPackageTrueCost,
                                    eventRow.BronzePackageTrueCost);
                            }

                            salesEventSaleDataTable.AddEventSaleRow(
                                salesEventRow,
                                packageRow.PackageID,
                                eventAttendeeRow.EventAttendeeID,
                                eventAttendeeAgencyMapRow.AgencyNumber,
                                eventAttendeeAgencyMapRow.TripNumber,
                                string.Empty,
                                0,
                                0);
                        }
                    }
                }
            }

            foreach (Sales.PackageSaleRow salesPackageSalesRow in salesPackageSaleDataTable.Rows)
            {
                foreach (DataRow row in salesPackageSalesRow.GetChildRows("PackageSale_EventSale"))
                {
                    Sales.EventSaleRow salesEventSaleRow = row as Sales.EventSaleRow;

                    flightTripHelper.GetList(
                        salesPackageSalesRow.CustomerNumber.Trim(),
                        salesEventSaleRow.AgencyNumber);

                    foreach (SAPServices.SAP_FLIGHTTRIPLIST.BAPISTRDAT trip in flightTripHelper._bapiFlightTripList)
                    {
                        if (salesEventSaleRow.TripNumber == trip.TRIPNUMBER)
                        {
                            flightConnectionHelper.GetDetail(
                                trip.FLCONN1,
                                trip.FLDATE1,
                                string.Empty,
                                salesEventSaleRow.AgencyNumber);

                            Sales.EventRow salesEventRow = 
                                salesEventDataTable.FindByEventID(salesEventSaleRow.EventID);

                            decimal ticketPrice = 0;
                            switch (trip.CLASS)
                            {
                                case "F":
                                    salesEventSaleRow.Cost = salesEventRow.GoldPackageCost;
                                    salesEventSaleRow.Price = salesEventRow.GoldPackagePrice;
                                    salesEventSaleRow.PackageType = "Gold";
                                    ticketPrice =
                                        trip.NUMADULT == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_FST1
                                            :
                                        trip.NUMCHILD == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_FST2
                                            :
                                        trip.NUMINFANT == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_FST3
                                            : 0;
                                    break;
                                case "C":
                                    salesEventSaleRow.Cost = salesEventRow.SilverPackageCost;
                                    salesEventSaleRow.Price = salesEventRow.SilverPackagePrice;
                                    salesEventSaleRow.PackageType = "Silver";
                                    ticketPrice =
                                        trip.NUMADULT == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_BUS1
                                            :
                                        trip.NUMCHILD == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_BUS2
                                            :
                                        trip.NUMINFANT == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_BUS3
                                            : 0;
                                    break;
                                case "Y":
                                    salesEventSaleRow.Cost = salesEventRow.BronzePackageCost;
                                    salesEventSaleRow.Price = salesEventRow.BronzePackagePrice;
                                    salesEventSaleRow.PackageType = "Bronze";
                                    ticketPrice =
                                        trip.NUMADULT == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_ECO1
                                            :
                                        trip.NUMCHILD == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_ECO2
                                            :
                                        trip.NUMINFANT == 1
                                            ? flightConnectionHelper._bapiPrice.PRICE_ECO3
                                            : 0;
                                    break;
                            }

                            salesPackageSalesRow.Cost += salesEventSaleRow.Cost;
                            salesPackageSalesRow.Price += salesEventSaleRow.Price;

                            salesFlightDataTable.AddFlightRow(
                                salesEventSaleRow,
                                trip.FLCONN1,
                                trip.FLDATE1,
                                trip.CLASS,
                                ticketPrice,
                                trip.NUMADULT,
                                trip.NUMCHILD,
                                trip.NUMINFANT);
                        }
                    }
                }
            }
            return salesDataset;
        }

        #endregion

        #region PackageInfo

        public static PackageInfo GetPackageInfo(string agencyNumber)
        {
            PackageInfo packageInfoDataset = new PackageInfo();

            PackageInfo.PackageDataTable packageInfoPackageDataTable =
                packageInfoDataset.Package;

            PackageInfo.EventDataTable packageInfoEventDataTable =
                packageInfoDataset.Event;

            PackageInfo.FlightDataTable packageInfoFlightDataTable =
                packageInfoDataset.Flight;

            SAPPackageReadWrite package =
                new SAPPackageReadWrite(Config._dbConnectionName);

            SAPEventReadWrite events =
                new SAPEventReadWrite(Config._dbConnectionName);

            SAPVenueReadWrite venue =
                new SAPVenueReadWrite(Config._dbConnectionName);

            SAPPackageEventMapReadWrite packageEventMap =
                new SAPPackageEventMapReadWrite(Config._dbConnectionName);

            SAPFlightConnection flightConnectionHelper = new SAPFlightConnection(
                Config.SAPUserName, Config.SAPPassword);

            SAPDataSetPackage packageDataset = package.SelectAll();

            foreach (SAPDataSetPackage.PackageRow packageRow in packageDataset.Package.Rows)
            {
                PackageInfo.PackageRow packageInfoPackageRow =
                    packageInfoPackageDataTable.AddPackageRow(
                        packageRow.PackageID,
                        packageRow.PackageName,
                        packageRow.PackageDescription);

                SAPDataSetPackageEventMap packageEventMapDataset =
                    packageEventMap.SelectByPackageID(packageRow.PackageID);

                foreach (SAPDataSetPackageEventMap.PackageEventMapRow packageEventMapRow in packageEventMapDataset.PackageEventMap.Rows)
                {
                    SAPDataSetEvent eventDataset = events.SelectByEventID(packageEventMapRow.EventID);

                    foreach (SAPDataSetEvent.EventRow eventRow in eventDataset.Event.Rows)
                    {
                        string venueCity = string.Empty;
                        using (SAPDataReaderVenue venueReader = venue.ReaderSelectByVenueID(eventRow.VenueID))
                        {
                            if (venueReader.DataReader.HasRows)
                            {
                                venueReader.DataReader.Read();
                                venueCity = venueReader.VenueCity.ToUpper();
                            }
                        }

                        PackageInfo.EventRow packageInfoEventRow =
                            packageInfoEventDataTable.AddEventRow(
                                eventRow.EventID,
                                packageInfoPackageRow,
                                eventRow.EventName,
                                eventRow.EventDescription,
                                eventRow.EventDate.ToString(),
                                string.Format("{0}, {1}", eventRow.VenueName, venueCity),
                                0,
                                0,
                                0);

                        if (venueCity != string.Empty)
                        {
                            flightConnectionHelper.GetList(string.Empty, agencyNumber, string.Empty, venueCity);
                            foreach (SAPServices.SAP_FLIGHTCONNLIST.BAPISCODAT flight in flightConnectionHelper._bapiConnectionList)
                            {
                                //flight dates should be between today and before the event date
                                if (Convert.ToDateTime(flight.FLIGHTDATE) > DateTime.Today &&
                                    Convert.ToDateTime(flight.FLIGHTDATE) < eventRow.EventDate)
                                {
                                    flightConnectionHelper.GetDetail(flight.FLIGHTCONN, flight.FLIGHTDATE, string.Empty,
                                                                     agencyNumber);
                                    
                                    PackageInfo.FlightRow packageInfoFlightRow =
                                        packageInfoFlightDataTable.AddFlightRow(
                                            packageInfoEventRow,
                                            flightConnectionHelper._bapiHopList[0].AIRLINE,
                                            flight.FLIGHTDATE,
                                            flight.DEPTIME,
                                            flight.AIRPORTFR,
                                            flight.CITYFROM,
                                            flight.AIRPORTTO,
                                            flight.CITYTO,
                                            flight.ARRDATE,
                                            flight.ARRTIME,
                                            flightConnectionHelper._bapiAvailability[0].FIRSTFREE,
                                            flightConnectionHelper._bapiAvailability[0].BUSINFREE,
                                            flightConnectionHelper._bapiAvailability[0].ECONOFREE);

                                    packageInfoEventRow.GoldAvailability += flightConnectionHelper._bapiAvailability[0].FIRSTFREE;
                                    packageInfoEventRow.SilverAvailability += flightConnectionHelper._bapiAvailability[0].BUSINFREE;
                                    packageInfoEventRow.BronzeAvailability += flightConnectionHelper._bapiAvailability[0].ECONOFREE;
                                }
                            }
                        }
                    }
                }
            }
            return packageInfoDataset;
        }

        public static PackageInfo GetPackageInfo()
        {
            PackageInfo packageInfoDataset = new PackageInfo();

            PackageInfo.PackageDataTable packageInfoPackageDataTable =
                packageInfoDataset.Package;

            PackageInfo.EventDataTable packageInfoEventDataTable =
                packageInfoDataset.Event;

            PackageInfo.FlightDataTable packageInfoFlightDataTable =
                packageInfoDataset.Flight;

            SAPPackageReadWrite package =
                new SAPPackageReadWrite(Config._dbConnectionName);

            SAPEventReadWrite events =
                new SAPEventReadWrite(Config._dbConnectionName);

            SAPVenueReadWrite venue =
                new SAPVenueReadWrite(Config._dbConnectionName);

            SAPPackageEventMapReadWrite packageEventMap =
                new SAPPackageEventMapReadWrite(Config._dbConnectionName);

            SAPFlight flightHelper = new SAPFlight(
                Config.SAPUserName, Config.SAPPassword);

            SAPDataSetPackage packageDataset = package.SelectAll();

            foreach (SAPDataSetPackage.PackageRow packageRow in packageDataset.Package.Rows)
            {
                PackageInfo.PackageRow packageInfoPackageRow =
                    packageInfoPackageDataTable.AddPackageRow(
                        packageRow.PackageID,
                        packageRow.PackageName,
                        packageRow.PackageDescription);

                SAPDataSetPackageEventMap packageEventMapDataset =
                    packageEventMap.SelectByPackageID(packageRow.PackageID);

                foreach (SAPDataSetPackageEventMap.PackageEventMapRow packageEventMapRow in packageEventMapDataset.PackageEventMap.Rows)
                {
                    SAPDataSetEvent eventDataset = events.SelectByEventID(packageEventMapRow.EventID);

                    foreach (SAPDataSetEvent.EventRow eventRow in eventDataset.Event.Rows)
                    {
                        PackageInfo.EventRow packageInfoEventRow =
                            packageInfoEventDataTable.AddEventRow(
                                eventRow.EventID,
                                packageInfoPackageRow,
                                eventRow.EventName,
                                eventRow.EventDescription,
                                eventRow.EventDate.ToString(),
                                eventRow.VenueName,
                                0,
                                0,
                                0);

                        string venueCity = string.Empty;
                        using (SAPDataReaderVenue venueReader = venue.ReaderSelectByVenueID(eventRow.VenueID))
                        {
                            if (venueReader.DataReader.HasRows)
                            {
                                venueReader.DataReader.Read();
                                venueCity = venueReader.VenueCity.ToUpper();
                            }
                        }

                        if (venueCity != string.Empty)
                        {
                            flightHelper.GetList(string.Empty, venueCity);
                            foreach (SAPServices.SAP_FLIGHTLIST.BAPISFLDAT flight in flightHelper._flightList)
                            {
                                //flight dates should be between today and before the event date
                                if (Convert.ToDateTime(flight.FLIGHTDATE) > DateTime.Today &&
                                    Convert.ToDateTime(flight.FLIGHTDATE) < eventRow.EventDate)
                                {
                                    flightHelper.CheckAvailability(flight.AIRLINEID, flight.CONNECTID, flight.FLIGHTDATE);
                                    
                                    PackageInfo.FlightRow packageInfoFlightRow =
                                        packageInfoFlightDataTable.AddFlightRow(
                                            packageInfoEventRow,
                                            flight.AIRLINE,
                                            flight.FLIGHTDATE,
                                            flight.DEPTIME,
                                            flight.AIRPORTFR,
                                            flight.CITYFROM,
                                            flight.AIRPORTTO,
                                            flight.CITYTO,
                                            flight.ARRDATE,
                                            flight.ARRTIME,
                                            flightHelper._availability.FIRSTFREE,
                                            flightHelper._availability.BUSINFREE,
                                            flightHelper._availability.ECONOFREE);

                                    packageInfoEventRow.GoldAvailability += flightHelper._availability.FIRSTFREE;
                                    packageInfoEventRow.SilverAvailability += flightHelper._availability.BUSINFREE;
                                    packageInfoEventRow.BronzeAvailability += flightHelper._availability.ECONOFREE;
                                }
                            }
                        }
                    }
                }
            }
            return packageInfoDataset;
        }

        #endregion

        #region

        public static StatisticList GetStatistics(int packageID)
        {
            StatisticList statList = new StatisticList();

            SortedDictionary<string, int> locationCount = 
                new SortedDictionary<string, int>();

            SortedDictionary<string, string> customerList =
                new SortedDictionary<string, string>();

            SAPEventAttendeeAgencyMapReadWrite eventTrip =
                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);
            
            SAPEventAttendeeReadWrite eventAttendee =
                new SAPEventAttendeeReadWrite(Config._dbConnectionName);

            #region get customer list first
            SAPCustomer sapCustomer = 
                new SAPCustomer(Config.SAPUserName, Config.SAPPassword);

            if (sapCustomer.GetList())
            {
                foreach (SAPServices.SAP_FLIGHTCUSTOMERLIST.BAPISCUDAT customerItem in sapCustomer._customerList)
                {
                    if (!customerList.ContainsKey(customerItem.CUSTOMERID))
                    {
                        customerList.Add(
                            customerItem.CUSTOMERID,
                            customerItem.CITY);
                    }
                }
            }
            #endregion

            #region get statistic
            using (SAPDataReaderEventAttendeeAgencyMap rdrTrip =
                eventTrip.ReaderSelectByPackageID(packageID))
            {
                if (rdrTrip.DataReader != null &&
                    rdrTrip.DataReader.HasRows)
                {
                    SAPFlightTrip flightTrip =
                        new SAPFlightTrip(Config.SAPUserName, Config.SAPPassword);
                    while (rdrTrip.DataReader.Read())
                    {
                        if (flightTrip.GetList(
                            rdrTrip.CustomerNumber.Trim(),
                            rdrTrip.AgencyNumber.Trim()))
                        {
                            SAPServices.SAP_FLIGHTTRIPLIST.BAPISTRDAT[] _bapiFlightTripList = 
                                flightTrip._bapiFlightTripList;

                            foreach (SAPServices.SAP_FLIGHTTRIPLIST.BAPISTRDAT _bapiFlightTrip in _bapiFlightTripList)
                            {
                                if (_bapiFlightTrip.TRIPNUMBER != rdrTrip.TripNumber.Trim())
                                {
                                    continue;
                                }
                                statList.AdultAgeStat += _bapiFlightTrip.NUMADULT;
                                statList.ChildAgeStat += _bapiFlightTrip.NUMCHILD;
                                statList.InfantAgeStat += _bapiFlightTrip.NUMINFANT;
                                switch (_bapiFlightTrip.CLASS)
                                {
                                    case "Y":
                                        statList.FirstClassStat++;
                                        break;
                                    case "C":
                                        statList.BusinessClassStat++;
                                        break;
                                    case "F":
                                        statList.EconomyClassStat++;
                                        break;
                                }
                            }
                        }

                    } //while (rdrTrip.DataReader.Read());
                }
            }
            #endregion

            using (SAPDataReaderEventAttendee rdrEventAttendee =
                eventAttendee.ReaderSelectByPackageID(packageID))
            {
                if (rdrEventAttendee.DataReader != null &&
                    rdrEventAttendee.DataReader.HasRows)
                {
                    while (rdrEventAttendee.DataReader.Read())
                    {
                        //location demographics
                        if (customerList.ContainsKey(rdrEventAttendee.CustomerNumber.Trim()))
                        {
                            string location = customerList[rdrEventAttendee.CustomerNumber.Trim()].ToUpper();

                            if (locationCount.ContainsKey(location))
                            {
                                locationCount[location]++;
                            }
                            else
                            {
                                locationCount.Add(location, 1);
                            }
                        }
                    } //while (rdrEventAttendee.DataReader.Read());
                }
            }

            Population populate = null;

            if (locationCount.Count > 0)
            {
                populate = new Population();
                
                Population.PopulationTableDataTable dt = 
                    populate.PopulationTable;
                
                foreach (KeyValuePair<string, int> kvp in locationCount)
                {
                    dt.AddPopulationTableRow(
                        kvp.Key,
                        kvp.Value);
                }
            }
            
            statList.LocationStat = populate;

            return statList;
        }

        public static bool AreEventsOutdated()
        {
            SAPEventReadWrite eventRW = new SAPEventReadWrite(Config._dbConnectionName);

            SAPDataSetEvent eventDataSet = eventRW.SelectAll();

            SAPDataSetEvent.EventRow[] eventRows = (SAPDataSetEvent.EventRow[])
                eventDataSet.Event.Select("EventDate >'" + DateTime.Now + "'");

            if (eventRows.Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

    }
}
