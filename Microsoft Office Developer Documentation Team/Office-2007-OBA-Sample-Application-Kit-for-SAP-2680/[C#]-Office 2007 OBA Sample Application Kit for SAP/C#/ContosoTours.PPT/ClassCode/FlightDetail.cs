using System.Collections.Generic;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTTRIPLIST;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public static class FlightDetail
    {
        public static StatisticList GetStatistics(int packageID)
        {
            StatisticList statList = new StatisticList();

            SortedDictionary<string, string> customerList =
                new SortedDictionary<string, string>();

            SAPEventAttendeeAgencyMapReadWrite eventTrip =
                new SAPEventAttendeeAgencyMapReadWrite(Config._dbConnectionName);

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
                            BAPISTRDAT[] _bapiFlightTripList =
                                flightTrip._bapiFlightTripList;

                            foreach (BAPISTRDAT _bapiFlightTrip in _bapiFlightTripList)
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

                    }// while (rdrTrip.DataReader.Read());
                }
            }
            #endregion

            return statList;
        }
    }
}
