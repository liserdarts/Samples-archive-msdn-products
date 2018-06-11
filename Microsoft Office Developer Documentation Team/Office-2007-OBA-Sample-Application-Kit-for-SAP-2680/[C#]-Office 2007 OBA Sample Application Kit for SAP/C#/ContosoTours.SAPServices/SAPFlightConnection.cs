using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Microsoft.SAPSK.ContosoTours.SAPServices
{
    public class SAPFlightConnection : SAPCredential
    {
        private SAP_FLIGHTCONNDETAIL.BAPI_FLCONN_GETDETAILService _flightConnDetail;
        private SAP_FLIGHTCONNLIST.BAPI_FLCONN_GETLISTService _flightConnList;

        public SAP_FLIGHTCONNLIST.BAPISCODRA[] _bapiDateRange;
        public SAP_FLIGHTCONNLIST.BAPISCODAT[] _bapiConnectionList;
        public SAP_FLIGHTCONNLIST.BAPISCODST _bapiDestinationFrom = null;
        public SAP_FLIGHTCONNLIST.BAPISCODST _bapiDestinationTo = null;

        public SAP_FLIGHTCONNDETAIL.BAPISCOHOP[] _bapiHopList;
        public SAP_FLIGHTCONNDETAIL.BAPISCOPRI _bapiPrice;
        public SAP_FLIGHTCONNDETAIL.BAPISCOAVA[] _bapiAvailability;

        public SAP_FLIGHTCONNLIST.BAPIRET2[] _bapiReturnList;
        public SAP_FLIGHTCONNDETAIL.BAPIRET2[] _bapiReturnDetail;

        public SAPFlightConnection(string userName, string password):base(userName,password)
        {
        }

        public bool GetList(
            string airlineID,
            string travelAgency,
            string cityFrom,
            string cityTo)
        {
            _bapiDestinationTo = new SAP_FLIGHTCONNLIST.BAPISCODST();
            _bapiDestinationFrom = new SAP_FLIGHTCONNLIST.BAPISCODST();
            
            _bapiDestinationTo.AIRPORTID = "";
            _bapiDestinationTo.CITY = cityTo;
            _bapiDestinationTo.COUNTR = "";
            _bapiDestinationTo.COUNTR_ISO = "";

            _bapiDestinationFrom.AIRPORTID = "";
            _bapiDestinationFrom.CITY = cityFrom;
            _bapiDestinationFrom.COUNTR = "";
            _bapiDestinationFrom.COUNTR_ISO = "";

            return GetList(
                airlineID,
                travelAgency);
        }

        public bool GetList(
            string airlineID,
            string travelAgency)
        {
            _flightConnList = new SAP_FLIGHTCONNLIST.BAPI_FLCONN_GETLISTService();
            _flightConnList.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTCONNLIST_BAPI_FLCONN_GETLISTService;
            _flightConnList.Credentials = SAPIdentity;

            _bapiDateRange = new SAP_FLIGHTCONNLIST.BAPISCODRA[0];
            _bapiConnectionList = new SAP_FLIGHTCONNLIST.BAPISCODAT[0];
            _bapiReturnList = new SAP_FLIGHTCONNLIST.BAPIRET2[0];

            SAP_FLIGHTCONNLIST.BAPIPAREX[] bapiExtensionIn = new SAP_FLIGHTCONNLIST.BAPIPAREX[0];
            SAP_FLIGHTCONNLIST.BAPIPAREX[] bapiExtensionOut = new SAP_FLIGHTCONNLIST.BAPIPAREX[0];
            
            //_bapiDestinationFrom and _bapiDestinationTo should have values

            _flightConnList.BAPI_FLCONN_GETLIST(
                airlineID,
                ref _bapiDateRange,
                _bapiDestinationFrom,
                _bapiDestinationTo,
                ref bapiExtensionIn,
                ref bapiExtensionOut,
                ref _bapiConnectionList,
                0,
                false,
                ref _bapiReturnList,
                travelAgency);

            return (_bapiReturnList.Length == 1);
        }

        public bool GetDetail(
            string connectionNumber,
            string flightDate,
            string noAvailability,
            string travelAgency)
        {
            _flightConnDetail = new SAP_FLIGHTCONNDETAIL.BAPI_FLCONN_GETDETAILService();
            _flightConnDetail.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTCONNDETAIL_BAPI_FLCONN_GETDETAILService;
            _flightConnDetail.Credentials = SAPIdentity;

            _bapiAvailability = new SAP_FLIGHTCONNDETAIL.BAPISCOAVA[0];
            _bapiHopList = new SAP_FLIGHTCONNDETAIL.BAPISCOHOP[0];
            _bapiPrice = new SAP_FLIGHTCONNDETAIL.BAPISCOPRI();

            SAP_FLIGHTCONNDETAIL.BAPIPAREX[] bapiExtensionIn = new SAP_FLIGHTCONNDETAIL.BAPIPAREX[0];
            SAP_FLIGHTCONNDETAIL.BAPIPAREX[] bapiExtensionOut = new SAP_FLIGHTCONNDETAIL.BAPIPAREX[0];
            _bapiReturnDetail = new SAP_FLIGHTCONNDETAIL.BAPIRET2[0];

            _flightConnDetail.BAPI_FLCONN_GETDETAIL(
                ref _bapiAvailability,
                connectionNumber,
                ref bapiExtensionIn,
                ref bapiExtensionOut,
                flightDate,
                ref _bapiHopList,
                noAvailability,
                ref _bapiReturnDetail,
                travelAgency,
                out _bapiPrice);

            return (_bapiReturnDetail.Length == 1);
        }       
    }
}
