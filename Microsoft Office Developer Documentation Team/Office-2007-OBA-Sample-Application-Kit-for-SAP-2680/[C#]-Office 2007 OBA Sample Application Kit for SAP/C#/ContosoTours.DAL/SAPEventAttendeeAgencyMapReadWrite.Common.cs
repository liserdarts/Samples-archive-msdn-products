using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeAgencyMapReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventAttendeeAgencyMapSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspEventAttendeeAgencyMapInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPEventAttendeeAgencyMapUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspEventAttendeeAgencyMapDelete";
            }
        }

        public SAPEventAttendeeAgencyMapReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPEventAttendeeAgencyMapReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
            :
            base(dbTransactionConnection)
        {
        }

        private DbParameter[] _deleteSPParameters = null;

        private DbParameter[] _insertSPParameters = null;

        private DbParameter[] _updateSPParameters = null;

        public override DbParameter[] DeleteSPParameters
        {
            get
            {
                if (_deleteSPParameters == null)
                {
                    // This is marker used by the code generator so do not
                    // modify the following code
                    _deleteSPParameters =  new SWPGenericDbParameter []
                        {
                            new SWPGenericDbParameter(
                                "@EventAttendeeAgencyMapID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                        };
                }

                return _deleteSPParameters;
            }
        }

        public override DbParameter[] InsertSPParameters
        {
            get
            {
                if (_insertSPParameters == null)
                {
                    // This is marker used by the code generator so do not
                    // modify the following code
                    _insertSPParameters =  new SWPGenericDbParameter []
                        {
                            new SWPGenericDbParameter(
                                "@EventAttendeeID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@AgencyNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
                            new SWPGenericDbParameter(
                                "@TripNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
                            new SWPGenericDbParameter(
                                "@EventAttendeeAgencyMapID",
                                DbType.Int32,
                                ParameterDirection.Output,
                                0),
                        };
                }

                return _insertSPParameters;
            }
        }

        public override DbParameter[] UpdateSPParameters
        {
            get
            {
                if (_updateSPParameters == null)
                {
                    // This is marker used by the code generator so do not
                    // modify the following code
                    _updateSPParameters =  new SWPGenericDbParameter []
                        {
                            new SWPGenericDbParameter(
                                "@EventAttendeeAgencyMapID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventAttendeeID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@AgencyNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
                            new SWPGenericDbParameter(
                                "@TripNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "EventAttendeeAgencyMap";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _eventAttendeeIDColumnName = "EventAttendeeID";

        public const string _packageIDColumnName = "PackageID";

        public const string _customerNumberColumnName = "CustomerNumber";

        public const string _dateOfBirthColumnName = "DateOfBirth";

        public const string _createdColumnName = "Created";

        public const string _eventAttendeeAgencyMapIDColumnName = "EventAttendeeAgencyMapID";

        public const string _eventIDColumnName = "EventID";

        public const string _agencyNumberColumnName = "AgencyNumber";

        public const string _tripNumberColumnName = "TripNumber";

        public int Insert(
            int eventAttendeeID,
            int eventID,
            string agencyNumber,
            string tripNumber,
            out int eventAttendeeAgencyMapID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);
            ValidateParameterLength(
                "tripNumber",
                tripNumber.Length, 
                16,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);
            SWPGenericDbParameter tripNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@TripNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    tripNumber);
            SWPGenericDbParameter eventAttendeeAgencyMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeAgencyMapID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventAttendeeAgencyMapInsert",
                    eventAttendeeIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    agencyNumberGenericDbParameter,
                    tripNumberGenericDbParameter,
                    eventAttendeeAgencyMapIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventAttendeeAgencyMapInsert",
                    eventAttendeeIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    agencyNumberGenericDbParameter,
                    tripNumberGenericDbParameter,
                    eventAttendeeAgencyMapIDGenericDbParameter);
            }

            eventAttendeeAgencyMapID = (int)eventAttendeeAgencyMapIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int eventAttendeeAgencyMapID,
            int eventAttendeeID,
            int eventID,
            string agencyNumber,
            string tripNumber)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);
            ValidateParameterLength(
                "tripNumber",
                tripNumber.Length, 
                16,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventAttendeeAgencyMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeAgencyMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeAgencyMapID);
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);
            SWPGenericDbParameter tripNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@TripNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    tripNumber);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventAttendeeAgencyMapUpdate",
                    eventAttendeeAgencyMapIDGenericDbParameter,
                    eventAttendeeIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    agencyNumberGenericDbParameter,
                    tripNumberGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventAttendeeAgencyMapUpdate",
                    eventAttendeeAgencyMapIDGenericDbParameter,
                    eventAttendeeIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    agencyNumberGenericDbParameter,
                    tripNumberGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int eventAttendeeAgencyMapID)
        {
            int retVal;
            SWPGenericDbParameter eventAttendeeAgencyMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeAgencyMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeAgencyMapID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventAttendeeAgencyMapDelete",
                    eventAttendeeAgencyMapIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventAttendeeAgencyMapDelete",
                    eventAttendeeAgencyMapIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

