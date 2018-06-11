using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefTravelAgencyReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefTravelAgencySelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefTravelAgencyInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefTravelAgencyUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefTravelAgencyDelete";
            }
        }

        public SAPRefTravelAgencyReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefTravelAgencyReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@AgencyNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
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
                                "@AgencyNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
                            new SWPGenericDbParameter(
                                "@AgencyName",
                                DbType.String,
                                ParameterDirection.Input,
                                100),
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
                                "@AgencyNumber",
                                DbType.String,
                                ParameterDirection.Input,
                                16),
                            new SWPGenericDbParameter(
                                "@AgencyName",
                                DbType.String,
                                ParameterDirection.Input,
                                100),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefTravelAgency";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _agencyNumberColumnName = "AgencyNumber";

        public const string _agencyNameColumnName = "AgencyName";

        public int Insert(
            string agencyNumber,
            string agencyName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);
            ValidateParameterLength(
                "agencyName",
                agencyName.Length, 
                100,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);
            SWPGenericDbParameter agencyNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyName",
                    DbType.String,
                    ParameterDirection.Input,
                    100,
                    agencyName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefTravelAgencyInsert",
                    agencyNumberGenericDbParameter,
                    agencyNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefTravelAgencyInsert",
                    agencyNumberGenericDbParameter,
                    agencyNameGenericDbParameter);
            }

            return retVal;
        }

        public int Update(
            string agencyNumber,
            string agencyName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);
            ValidateParameterLength(
                "agencyName",
                agencyName.Length, 
                100,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);
            SWPGenericDbParameter agencyNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyName",
                    DbType.String,
                    ParameterDirection.Input,
                    100,
                    agencyName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefTravelAgencyUpdate",
                    agencyNumberGenericDbParameter,
                    agencyNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefTravelAgencyUpdate",
                    agencyNumberGenericDbParameter,
                    agencyNameGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            string agencyNumber)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefTravelAgencyDelete",
                    agencyNumberGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefTravelAgencyDelete",
                    agencyNumberGenericDbParameter);
            }

            return retVal;
        }
    }
}

