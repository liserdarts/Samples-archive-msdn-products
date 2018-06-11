using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Microsoft.SAPSK.ContosoTours.SAPServices
{
    public class SAPFlightTrip : SAPCredential
    {
        private SAP_FLIGHTTRIPLIST.BAPI_FLTRIP_GETLISTService _flightTrip;
        private SAP_COMMITWORK.BAPI_TRANSACTION_COMMITService _bapiCommitWork;
        private SAP_FLIGHTTRIPCREATE.BAPI_FLTRIP_CREATEService _bapiFlightTripCreate;

        public SAP_FLIGHTTRIPCREATE.BAPISTRPAS[] _bapiPassengers;
        public SAP_FLIGHTTRIPCREATE.BAPIRET2[] _bapiFlTripReturn;
        public SAP_FLIGHTTRIPCREATE.BAPIPAREX[] _bapiExtensionIn;
        public SAP_FLIGHTTRIPCREATE.BAPIPAREX[] _bapiExtensionOut;
        public SAP_FLIGHTTRIPCREATE.BAPISTRPRI _bapiTicketPrice;

        public SAP_FLIGHTTRIPLIST.BAPIPAREX[] _bapiExtIn;
        public SAP_FLIGHTTRIPLIST.BAPIPAREX[] _bapiExtOut;
        public SAP_FLIGHTTRIPLIST.BAPISTRDAT[] _bapiTripData;
        public SAP_FLIGHTTRIPLIST.BAPIRET2[] _bapiReturn;
        public SAP_FLIGHTTRIPLIST.BAPISTRDAT[] _bapiFlightTripList;
        public SAP_FLIGHTTRIPLIST.BAPISCODRA[] _bapiBookDateRange;
        public SAP_FLIGHTTRIPLIST.BAPISCODRA[] _bapiTripDateRange;

        public SAPFlightTrip(string userName, string password)
            : base(userName, password)
        {
        }

        public bool CreateTrip(
            string agencyNumber,
            string classType,
            string customerID,
            string flConn1,
            string flConn2,
            string flDate1,
            string flDate2,
            string passForm,
            string passDOB,
            string passName,
            out string travelAgencyNumber,
            out string tripNumber)
        {
            _bapiFlightTripCreate = new SAP_FLIGHTTRIPCREATE.BAPI_FLTRIP_CREATEService();
            _bapiCommitWork = new SAP_COMMITWORK.BAPI_TRANSACTION_COMMITService();

            _bapiFlightTripCreate.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTTRIPCREATE_BAPI_FLTRIP_CREATEService;
            _bapiCommitWork.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_COMMITWORK_BAPI_TRANSACTION_COMMITService;

            _bapiCommitWork.Credentials = SAPIdentity;
            _bapiFlightTripCreate.Credentials = SAPIdentity;

            _bapiPassengers = new SAP_FLIGHTTRIPCREATE.BAPISTRPAS[1];
            SAP_FLIGHTTRIPCREATE.BAPISTRPAS passenger = new SAP_FLIGHTTRIPCREATE.BAPISTRPAS();
            passenger.PASSFORM = passForm;
            passenger.PASSBIRTH = passDOB;
            passenger.PASSNAME = passName;
            _bapiPassengers[0] = passenger;

            return CreateTrip(
                agencyNumber,
                classType,
                customerID,
                flConn1,
                flConn2,
                flDate1,
                flDate2,
                out travelAgencyNumber,
                out tripNumber);
        }

        public bool CreateTrip(
            string agencyNumber,
            string classType,
            string customerID,
            string flConn1,
            string flConn2,
            string flDate1,
            string flDate2,
            out string travelAgencyNumber,
            out string tripNumber)
        {
            _bapiExtensionIn = new SAP_FLIGHTTRIPCREATE.BAPIPAREX[0];
            _bapiExtensionOut = new SAP_FLIGHTTRIPCREATE.BAPIPAREX[0];
            _bapiFlTripReturn = new SAP_FLIGHTTRIPCREATE.BAPIRET2[0];

            SAP_FLIGHTTRIPCREATE.BAPISTRNEW flData = 
                new SAP_FLIGHTTRIPCREATE.BAPISTRNEW();

            flData.AGENCYNUM = agencyNumber;
            flData.CLASS = classType;
            flData.CUSTOMERID = customerID;
            flData.FLCONN1 = flConn1;
            flData.FLCONN2 = flConn2;
            flData.FLDATE1 = flDate1;
            flData.FLDATE2 = flDate2;

            CookieContainer cookieContainer = new CookieContainer();

            _bapiFlightTripCreate.Url += "?session_mode=1";
            _bapiCommitWork.Url += "?session_mode=2";

            _bapiFlightTripCreate.CookieContainer = cookieContainer;
            _bapiCommitWork.CookieContainer = cookieContainer;

            _bapiTicketPrice = _bapiFlightTripCreate.BAPI_FLTRIP_CREATE(
                ref _bapiExtensionIn,
                flData,
                ref _bapiPassengers,
                ref _bapiFlTripReturn,
                out travelAgencyNumber,
                out tripNumber);
            if (_bapiFlTripReturn.Length > 1)
            {
                return false;
            }
            SAP_COMMITWORK.BAPIRET2 ret = _bapiCommitWork.BAPI_TRANSACTION_COMMIT("");
            return true;
        }

        public bool GetList(
            string customerNumber,
            string travelAgency)
        {
            _flightTrip = new SAP_FLIGHTTRIPLIST.BAPI_FLTRIP_GETLISTService();
            _flightTrip.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTTRIPLIST_BAPI_FLTRIP_GETLISTService;
            _flightTrip.Credentials = SAPIdentity;

            _bapiBookDateRange = new SAP_FLIGHTTRIPLIST.BAPISCODRA[0];
            _bapiTripDateRange = new SAP_FLIGHTTRIPLIST.BAPISCODRA[0];
            _bapiExtIn = new SAP_FLIGHTTRIPLIST.BAPIPAREX[0];
            _bapiExtOut = new SAP_FLIGHTTRIPLIST.BAPIPAREX[0];
            _bapiTripData = new SAP_FLIGHTTRIPLIST.BAPISTRDAT[0];
            _bapiReturn = new SAP_FLIGHTTRIPLIST.BAPIRET2[0];
            _bapiFlightTripList = new SAP_FLIGHTTRIPLIST.BAPISTRDAT[0];

            _flightTrip.BAPI_FLTRIP_GETLIST(
                ref _bapiBookDateRange,
                customerNumber,
                ref _bapiExtIn,
                ref _bapiExtOut,
                ref _bapiFlightTripList,
                0,
                false,
                ref _bapiReturn,
                travelAgency,
                ref _bapiTripDateRange);

            return (_bapiReturn.Length == 1);
        }
    }
}
