using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCountryReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefCountrySelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefCountryInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefCountryUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefCountryDelete";
            }
        }

        public SAPRefCountryReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefCountryReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@CountryCode",
                                DbType.String,
                                ParameterDirection.Input,
                                4),
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
                                "@CountryCode",
                                DbType.String,
                                ParameterDirection.Input,
                                4),
                            new SWPGenericDbParameter(
                                "@CountryName",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
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
                                "@CountryCode",
                                DbType.String,
                                ParameterDirection.Input,
                                4),
                            new SWPGenericDbParameter(
                                "@CountryName",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefCountry";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _countryCodeColumnName = "CountryCode";

        public const string _countryNameColumnName = "CountryName";

        public int Insert(
            string countryCode,
            string countryName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "countryCode",
                countryCode.Length, 
                4,
                validationErrorReport);
            ValidateParameterLength(
                "countryName",
                countryName.Length, 
                50,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter countryCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    countryCode);
            SWPGenericDbParameter countryNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryName",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    countryName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCountryInsert",
                    countryCodeGenericDbParameter,
                    countryNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCountryInsert",
                    countryCodeGenericDbParameter,
                    countryNameGenericDbParameter);
            }

            return retVal;
        }

        public int Update(
            string countryCode,
            string countryName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "countryCode",
                countryCode.Length, 
                4,
                validationErrorReport);
            ValidateParameterLength(
                "countryName",
                countryName.Length, 
                50,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter countryCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    countryCode);
            SWPGenericDbParameter countryNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryName",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    countryName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCountryUpdate",
                    countryCodeGenericDbParameter,
                    countryNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCountryUpdate",
                    countryCodeGenericDbParameter,
                    countryNameGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            string countryCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "countryCode",
                countryCode.Length, 
                4,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter countryCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    countryCode);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCountryDelete",
                    countryCodeGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCountryDelete",
                    countryCodeGenericDbParameter);
            }

            return retVal;
        }
    }
}

