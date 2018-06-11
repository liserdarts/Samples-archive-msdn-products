using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Microsoft.SAPSK.ContosoTours.SAPServices
{
    public class SAPFlight : SAPCredential
    {
        private SAP_FLIGHTAVAILIBILITY.BAPI_FLIGHT_CHECKAVAILIBILITYService _flightAvail;
        private SAP_FLIGHTLIST.BAPI_FLIGHT_GETLISTService _flight;

        public SAP_FLIGHTAVAILIBILITY.BAPIRET2[] _bapiReturnAvail;
        public SAP_FLIGHTAVAILIBILITY.BAPISFLAVA _availability;

        public SAP_FLIGHTLIST.BAPISFLDRA[] dateRange;

        public SAP_FLIGHTLIST.BAPISFLDST _destFrom;
        public SAP_FLIGHTLIST.BAPISFLDST _destTo;

        public SAP_FLIGHTLIST.BAPIPAREX[] _extensionIn;
        public SAP_FLIGHTLIST.BAPIPAREX[] _extensionOut;
        public SAP_FLIGHTLIST.BAPISFLDAT[] _flightList;
        public SAP_FLIGHTLIST.BAPIRET2[] _bapiReturnList;
        
        public SAPFlight(string userName, string password)
            : base(userName, password)
        {
            
        }

        public bool CheckAvailability(
            string airlineID,
            string connectionID,
            string flightDate)
        {
            _flightAvail = new SAP_FLIGHTAVAILIBILITY.BAPI_FLIGHT_CHECKAVAILIBILITYService();
            _flightAvail.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTAVAILIBILITY_BAPI_FLIGHT_CHECKAVAILIBILITYService;
            _flightAvail.Credentials = SAPIdentity;

            _bapiReturnAvail = new SAP_FLIGHTAVAILIBILITY.BAPIRET2[0];

            _availability =
                _flightAvail.BAPI_FLIGHT_CHECKAVAILIBILITY(
                    airlineID,
                    connectionID,
                    flightDate,
                    ref _bapiReturnAvail);

            return (_bapiReturnAvail.Length == 1);
        }

        public bool GetList()
        {
            _destFrom = new SAP_FLIGHTLIST.BAPISFLDST();
            _destTo = new SAP_FLIGHTLIST.BAPISFLDST();

            return GetFlightList();
        }

        public bool GetList(string destFrom, string destTo)
        {

            _destFrom = new SAP_FLIGHTLIST.BAPISFLDST();
            _destFrom.CITY = destFrom.ToUpper();

            _destTo = new SAP_FLIGHTLIST.BAPISFLDST();
            _destTo.CITY = destTo.ToUpper();

            return GetFlightList();
        }
        private bool GetFlightList()
        {
            _flight = new SAP_FLIGHTLIST.BAPI_FLIGHT_GETLISTService();
            _flight.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTLIST_BAPI_FLIGHT_GETLISTService;
            _flight.Credentials = SAPIdentity;

            dateRange = new SAP_FLIGHTLIST.BAPISFLDRA[0];

            _extensionIn = new SAP_FLIGHTLIST.BAPIPAREX[0];
            _extensionOut = new SAP_FLIGHTLIST.BAPIPAREX[0];
            _flightList = new SAP_FLIGHTLIST.BAPISFLDAT[0];
            _bapiReturnList = new SAP_FLIGHTLIST.BAPIRET2[0];

            _flight.BAPI_FLIGHT_GETLIST(
                string.Empty,
                ref dateRange,
                _destFrom,
                _destTo,
                ref _extensionIn,
                ref _extensionOut,
                ref _flightList,
                0,
                false,
                ref  _bapiReturnList);

            return (_bapiReturnList.Length == 1); 
        }
    }
}
