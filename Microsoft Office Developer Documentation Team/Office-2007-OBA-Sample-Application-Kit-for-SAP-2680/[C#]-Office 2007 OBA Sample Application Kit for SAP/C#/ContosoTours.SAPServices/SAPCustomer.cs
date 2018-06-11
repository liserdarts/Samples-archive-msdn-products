using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Microsoft.SAPSK.ContosoTours.SAPServices
{
    public class SAPCustomer: SAPCredential
    {
        private SAP_COMMITWORK.BAPI_TRANSACTION_COMMITService _bapiCommitWork;
        private SAP_FLIGHTCUSTOMERCREATE.BAPI_FLCUST_CREATEFROMDATAService _bapiCustomerCreate;
        private SAP_FLIGHTCUSTOMERLIST.BAPI_FLCUST_GETLISTService _bapiCustomerList;

        public SAP_FLIGHTCUSTOMERCREATE.BAPIPAREX[] _bapiExtIn;
        public SAP_FLIGHTCUSTOMERCREATE.BAPIPAREX[] _bapiExtOut;
        public SAP_FLIGHTCUSTOMERCREATE.BAPISCUNEW _bapiCustData;
        public SAP_FLIGHTCUSTOMERCREATE.BAPIRET2[] _bapiReturn;

        public SAP_FLIGHTCUSTOMERLIST.BAPIRET2[] _bapiReturnGetList;
        public SAP_FLIGHTCUSTOMERLIST.BAPISCUDAT[] _customerList;

        public SAPCustomer(string userName, string password):base(userName,password)
        {
        }

        public bool CreateFromData(
            string city,
            string country,
            string customerName,
            string customerType,
            string formType,
            string language,
            string address,
            string phone,
            string postCode)
        {
            _bapiCustData = new SAP_FLIGHTCUSTOMERCREATE.BAPISCUNEW();
            _bapiCustData.CITY = city;
            _bapiCustData.COUNTR = country;
            _bapiCustData.CUSTNAME = customerName;
            _bapiCustData.CUSTTYPE = customerType;
            _bapiCustData.FORM = formType;
            _bapiCustData.LANGU = language;
            _bapiCustData.STREET = address;
            _bapiCustData.PHONE = phone;
            _bapiCustData.POSTCODE = postCode;
            return CreateFromData();
        }

        public bool CreateFromData()
        {
            _bapiCustomerCreate =
                new SAP_FLIGHTCUSTOMERCREATE.BAPI_FLCUST_CREATEFROMDATAService();
            _bapiCustomerCreate.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTCUSTOMERCREATE_BAPI_FLCUST_CREATEFROMDATAService;

            _bapiCommitWork = new SAP_COMMITWORK.BAPI_TRANSACTION_COMMITService();
            _bapiCommitWork.Url =
                Properties.Settings.Default.ContosoTours_SAPServices_SAP_COMMITWORK_BAPI_TRANSACTION_COMMITService;

            _bapiCommitWork.Credentials = SAPIdentity;
            _bapiCustomerCreate.Credentials = SAPIdentity;

            _bapiExtIn = new SAP_FLIGHTCUSTOMERCREATE.BAPIPAREX[0];
            _bapiExtOut = new SAP_FLIGHTCUSTOMERCREATE.BAPIPAREX[0];
            _bapiReturn = new SAP_FLIGHTCUSTOMERCREATE.BAPIRET2[0];

            CookieContainer cookieContainer = new CookieContainer();

            _bapiCustomerCreate.Url += "?session_mode=1";
            _bapiCommitWork.Url += "?session_mode=2";

            _bapiCustomerCreate.CookieContainer = cookieContainer;
            _bapiCommitWork.CookieContainer = cookieContainer;

            _bapiCustomerCreate.BAPI_FLCUST_CREATEFROMDATA(
                _bapiCustData,
                ref _bapiExtIn,
                ref _bapiReturn,
                "");
            if (_bapiReturn.Length > 1)
            {
                return false;
            }
            _bapiCommitWork.BAPI_TRANSACTION_COMMIT("");
            return true;
        }

        public bool GetList()
        {
            _bapiCustomerList =
                new SAP_FLIGHTCUSTOMERLIST.BAPI_FLCUST_GETLISTService();
            _bapiCustomerList.Credentials = SAPIdentity;

            SAP_FLIGHTCUSTOMERLIST.BAPISCUCRA[] customerRange =
                new SAP_FLIGHTCUSTOMERLIST.BAPISCUCRA[0];
            SAP_FLIGHTCUSTOMERLIST.BAPIPAREX[] extensionIn =
                new SAP_FLIGHTCUSTOMERLIST.BAPIPAREX[0];
            SAP_FLIGHTCUSTOMERLIST.BAPIPAREX[] extensionOut =
                new SAP_FLIGHTCUSTOMERLIST.BAPIPAREX[0];

            _bapiReturnGetList = new SAP_FLIGHTCUSTOMERLIST.BAPIRET2[0];
            _customerList = new SAP_FLIGHTCUSTOMERLIST.BAPISCUDAT[0];
            _bapiCustomerList.BAPI_FLCUST_GETLIST(
                ref _customerList,
                string.Empty,
                ref customerRange,
                ref extensionIn,
                ref extensionOut,
                0,
                false,
                ref _bapiReturnGetList,
                string.Empty);
            return (_bapiReturnGetList.Length == 1);
        }
    }
}
