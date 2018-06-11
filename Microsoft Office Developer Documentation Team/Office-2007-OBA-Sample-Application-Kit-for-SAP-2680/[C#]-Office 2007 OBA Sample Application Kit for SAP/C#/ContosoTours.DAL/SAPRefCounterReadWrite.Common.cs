using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCounterReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefCounterSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefCounterInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefCounterUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefCounterDelete";
            }
        }

        public SAPRefCounterReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefCounterReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@CarrID",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                3),
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
                                "@CarrID",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                3),
                            new SWPGenericDbParameter(
                                "@CounterNumber",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                8),
                            new SWPGenericDbParameter(
                                "@Airport",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                3),
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
                                "@CarrID",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                3),
                            new SWPGenericDbParameter(
                                "@CounterNumber",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                8),
                            new SWPGenericDbParameter(
                                "@Airport",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                3),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefCounter";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _carrIDColumnName = "CarrID";

        public const string _counterNumberColumnName = "CounterNumber";

        public const string _airportColumnName = "Airport";

        public int Insert(
            string carrID,
            string counterNumber,
            string airport)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                3,
                validationErrorReport);
            ValidateParameterLength(
                "counterNumber",
                counterNumber.Length, 
                8,
                validationErrorReport);
            ValidateParameterLength(
                "airport",
                airport.Length, 
                3,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    carrID);
            SWPGenericDbParameter counterNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CounterNumber",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    8,
                    counterNumber);
            SWPGenericDbParameter airportGenericDbParameter =
                new SWPGenericDbParameter(
                    "@Airport",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    airport);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCounterInsert",
                    carrIDGenericDbParameter,
                    counterNumberGenericDbParameter,
                    airportGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCounterInsert",
                    carrIDGenericDbParameter,
                    counterNumberGenericDbParameter,
                    airportGenericDbParameter);
            }

            return retVal;
        }

        public int Update(
            string carrID,
            string counterNumber,
            string airport)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                3,
                validationErrorReport);
            ValidateParameterLength(
                "counterNumber",
                counterNumber.Length, 
                8,
                validationErrorReport);
            ValidateParameterLength(
                "airport",
                airport.Length, 
                3,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    carrID);
            SWPGenericDbParameter counterNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CounterNumber",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    8,
                    counterNumber);
            SWPGenericDbParameter airportGenericDbParameter =
                new SWPGenericDbParameter(
                    "@Airport",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    airport);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCounterUpdate",
                    carrIDGenericDbParameter,
                    counterNumberGenericDbParameter,
                    airportGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCounterUpdate",
                    carrIDGenericDbParameter,
                    counterNumberGenericDbParameter,
                    airportGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            string carrID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                3,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    carrID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCounterDelete",
                    carrIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCounterDelete",
                    carrIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

