using System;
using System.Net;
using System.Web;
using System.Linq;
using System.Data;
using System.Xml.Linq;
using System.Collections;
using System.Web.Services;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Services.Protocols;


namespace SAPBridgeMaster
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.microsoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SAPService : System.Web.Services.WebService
    {
        private List<Customer> _customerList = new List<Customer>();
        private List<Flight> _flightList = new List<Flight>();

        
        #region Customers

        [WebMethod]
        public Customer[] GetCustomerList(
            string paramCustomerName,
            string paramCountry)
        {
            try
            {
                if (_customerList == null ||
                    _customerList.Count == 0)
                {
                    GetCustomers();
                }

                var customers =
                    from list in _customerList
                    select list;

                if (paramCountry != null)
                {
                    customers =
                        from list in _customerList
                        where CompareString(list.CustomerName, paramCustomerName) == true
                            && CompareString(list.Country, paramCountry) == true
                        select list;
                }
                else
                {
                    customers =
                        from list in _customerList
                        where CompareString(list.CustomerName, paramCustomerName) == true
                        select list;
                }

                return customers.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Flights

        [WebMethod]
        public Flight[] GetFlightList(
            string paramAirlineName,
            string paramCityFrom,
            string paramCityTo,
            string paramFDate)
        {
            try
            {
                if (_flightList == null ||
                    _flightList.Count < 1)
                {
                    GetFlights();
                }

                var flights =
                    from list in _flightList
                    select list;

                if (paramCityFrom != null &&
                    paramCityTo == null)
                {
                    flights =
                        from list in _flightList
                        where CompareString(list.CarrierName, paramAirlineName) == true
                            && CompareString(list.CityFrom, paramCityFrom) == true
                        select list;
                }
                else if (paramCityFrom == null &&
                    paramCityTo != null)
                {
                    flights =
                        from list in _flightList
                        where CompareString(list.CarrierName, paramAirlineName) == true
                            && CompareString(list.CityTo, paramCityTo) == true
                        select list;
                }
                else if (paramCityFrom != null &&
                    paramCityTo != null)
                {
                    flights =
                        from list in _flightList
                        where CompareString(list.CarrierName, paramAirlineName) == true
                            && CompareString(list.CityTo, paramCityTo) == true
                            && CompareString(list.CityFrom, paramCityFrom) == true
                        select list;
                }
                else
                {
                    flights =
                        from list in _flightList
                        where CompareString(list.CarrierName, paramAirlineName) == true
                        select list;
                }

                if (paramFDate != null)
                {
                    flights =
                        from list in flights
                        where Convert.ToDateTime(list.FlightDate) >=
                            Convert.ToDateTime(paramFDate)
                        select list;
                }

                return flights.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
        #region private Methods

        private void GetCustomers()
        {
            BAPI_FLCUST_GETLIST.BAPI_FLCUST_GETLISTService custListService =
               new BAPI_FLCUST_GETLIST.BAPI_FLCUST_GETLISTService();

            custListService.Credentials = new NetworkCredential("bcuser", "minisap");

            BAPI_FLCUST_GETLIST.BAPIRET2[] returnCustList;
            BAPI_FLCUST_GETLIST.BAPISCUDAT[] customers;

            BAPI_FLCUST_GETLIST.BAPISCUCRA[] customerRange =
                new BAPI_FLCUST_GETLIST.BAPISCUCRA[0];
            BAPI_FLCUST_GETLIST.BAPIPAREX[] extensionIn =
                new BAPI_FLCUST_GETLIST.BAPIPAREX[0];
            BAPI_FLCUST_GETLIST.BAPIPAREX[] extensionOut =
                new BAPI_FLCUST_GETLIST.BAPIPAREX[0];


            returnCustList = new BAPI_FLCUST_GETLIST.BAPIRET2[0];

            customers = new BAPI_FLCUST_GETLIST.BAPISCUDAT[0];

            custListService.BAPI_FLCUST_GETLIST(
                ref customers,
                String.Empty,
                ref customerRange,
                ref extensionIn,
                ref extensionOut,
                0,
                false,
                ref returnCustList,
                string.Empty);

            _customerList.Clear();

            for (int idx = 0; idx < customers.Length; idx++)
            {
                Customer newCustomer = new Customer(
                    customers[idx].CUSTOMERID,
                    customers[idx].CUSTNAME,
                    customers[idx].STREET,
                    customers[idx].POBOX,
                    customers[idx].POSTCODE,
                    customers[idx].CITY,
                    customers[idx].COUNTR,
                    customers[idx].PHONE);

                _customerList.Add(newCustomer);
            }
        }

        private void GetFlights()
        {
            BAPI_FLIGHT_GETLIST.BAPI_FLIGHT_GETLISTService flightListService =
               new BAPI_FLIGHT_GETLIST.BAPI_FLIGHT_GETLISTService();

            flightListService.Credentials = new NetworkCredential("bcuser", "minisap");

            BAPI_FLIGHT_GETLIST.BAPIRET2[] returnFlightList;
            BAPI_FLIGHT_GETLIST.BAPISFLDRA[] dateRange;

            BAPI_FLIGHT_GETLIST.BAPISFLDST destFrom;
            BAPI_FLIGHT_GETLIST.BAPISFLDST destTo;

            BAPI_FLIGHT_GETLIST.BAPIPAREX[] extensionIn;
            BAPI_FLIGHT_GETLIST.BAPIPAREX[] extensionOut;
            BAPI_FLIGHT_GETLIST.BAPISFLDAT[] flights;

            dateRange = new BAPI_FLIGHT_GETLIST.BAPISFLDRA[0];

            extensionIn = new BAPI_FLIGHT_GETLIST.BAPIPAREX[0];
            extensionOut = new BAPI_FLIGHT_GETLIST.BAPIPAREX[0];
            flights = new BAPI_FLIGHT_GETLIST.BAPISFLDAT[0];
            returnFlightList = new BAPI_FLIGHT_GETLIST.BAPIRET2[0];
            destFrom = new BAPI_FLIGHT_GETLIST.BAPISFLDST();
            destTo = new BAPI_FLIGHT_GETLIST.BAPISFLDST();

            flightListService.BAPI_FLIGHT_GETLIST(
                string.Empty,
                ref dateRange,
                destFrom,
                destTo,
                ref extensionIn,
                ref extensionOut,
                ref flights,
                0,
                false,
                ref  returnFlightList);

            _flightList.Clear();

            for (int idx = 0; idx < flights.Length; idx++)
            {
                int firstClassFree=0;
                int businessClassFree=0;
                int economyClassFree=0;

                bool isAvailable=CheckAvailability(
                    flights[idx].AIRLINEID,
                    flights[idx].CONNECTID,
                    flights[idx].FLIGHTDATE,
                    ref firstClassFree,
                    ref businessClassFree,
                    ref economyClassFree);

                if(isAvailable && 
                    Convert.ToDateTime(flights[idx].FLIGHTDATE)>=DateTime.Now)
                {
                    Flight flight = new Flight(
                        flights[idx].AIRLINEID,
                        flights[idx].AIRLINE,
                        flights[idx].CITYFROM,
                        flights[idx].CITYTO,
                        flights[idx].AIRPORTFR,
                        flights[idx].AIRPORTTO,
                        flights[idx].FLIGHTDATE,
                        flights[idx].DEPTIME,
                        flights[idx].ARRDATE,
                        flights[idx].ARRTIME,
                        firstClassFree,
                        businessClassFree,
                        economyClassFree);                 

                    _flightList.Add(flight);
                }
                
            }
        }

        private bool CompareString(string paramFromList, string paramFromParameter)
        {
            int lenghtOfSearchString = 0;
            
            //the * is used as the wildcard character
            if (paramFromParameter.StartsWith("*") &&
                paramFromParameter.EndsWith("*"))
            {
                paramFromParameter = paramFromParameter.Replace("*", " ");
                paramFromParameter = paramFromParameter.Trim();

                if (paramFromList.ToLower().IndexOf(paramFromParameter.ToLower()) >= 0)
                {
                    return true;
                }
            }
            //starts with filter
            else if (paramFromParameter.EndsWith("*"))
            {
                //remove the wildcard
                lenghtOfSearchString = paramFromParameter.Length - 1;

                if (paramFromList.Length >= lenghtOfSearchString)
                {
                    if (paramFromList.Substring(0, lenghtOfSearchString).ToLower() ==
                             paramFromParameter.Substring(0, lenghtOfSearchString).ToLower())
                    {
                        return true;
                    }
                }
            }
            //ends with filter
            else if (paramFromParameter.StartsWith("*"))
            {
                //remove the wildcard
                lenghtOfSearchString = paramFromParameter.Length - 1;

                int startIndex = 0;

                if (paramFromList.Length > lenghtOfSearchString)
                {
                    startIndex = paramFromList.Length - lenghtOfSearchString;

                    if (paramFromList.Substring(startIndex).ToLower() ==
                             paramFromParameter.Substring(1).ToLower())
                    {
                        return true;
                    }
                }
            }
            //equals
            else
            {
                if (paramFromList.ToLower() ==
                        paramFromParameter.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        
        private bool CheckAvailability(
            string paramAirlineID,
            string paramConnectionID,
            string paramFlightDate,
            ref int paramFCFree,
            ref int paramBCFree,
            ref int paramECFree)
        {
            BAPI_FLIGHT_CHECKAVAILIBILITY.BAPISFLAVA availability;

            BAPI_FLIGHT_CHECKAVAILIBILITY.BAPI_FLIGHT_CHECKAVAILIBILITYService flightAvail = 
                new BAPI_FLIGHT_CHECKAVAILIBILITY.BAPI_FLIGHT_CHECKAVAILIBILITYService();
            
            flightAvail.Credentials = new NetworkCredential("bcuser","minisap");

            BAPI_FLIGHT_CHECKAVAILIBILITY.BAPIRET2[] bapiReturnAvail =
                new BAPI_FLIGHT_CHECKAVAILIBILITY.BAPIRET2[0];

            availability =
                flightAvail.BAPI_FLIGHT_CHECKAVAILIBILITY(
                    paramAirlineID,
                    paramConnectionID,
                    paramFlightDate,
                    ref bapiReturnAvail);

            paramFCFree = availability.FIRSTFREE;
            paramBCFree = availability.BUSINFREE;
            paramECFree = availability.ECONOFREE;

            return (bapiReturnAvail.Length == 1);
        }

        #endregion
    }
}
