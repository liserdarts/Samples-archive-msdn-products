using System;
using System.Net;
using Microsoft.SAPSK.ContosoTours.SAPServices.SAP_COMMITWORK;

namespace Microsoft.SAPSK.ContosoTours.SAPServices
{
    public class SAPCredential
    {
        private string _userName;
        private string _password;
        private NetworkCredential _sapCredential; 

        public SAPCredential(string userName, string password)
        {
            _userName = userName;
            _password = password;
            _sapCredential = new NetworkCredential(_userName, password);
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

        public NetworkCredential SAPIdentity
        {
            get
            {
                return _sapCredential;
            }
        }

        public static bool ValidateUser(string username, string password)
        {
            try
            {
                BAPI_TRANSACTION_COMMITService _bapiCommitWork
                    = new BAPI_TRANSACTION_COMMITService();
                _bapiCommitWork.Credentials = new NetworkCredential(username, password);
                _bapiCommitWork.BAPI_TRANSACTION_COMMIT("");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
