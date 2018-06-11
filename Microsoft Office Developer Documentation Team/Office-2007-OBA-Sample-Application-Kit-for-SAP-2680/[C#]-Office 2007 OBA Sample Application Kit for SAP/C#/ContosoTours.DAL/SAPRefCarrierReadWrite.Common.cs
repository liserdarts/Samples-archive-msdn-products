using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCarrierReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefCarrierSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefCarrierInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefCarrierUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefCarrierDelete";
            }
        }

        public SAPRefCarrierReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefCarrierReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                DbType.String,
                                ParameterDirection.Input,
                                6),
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
                                DbType.String,
                                ParameterDirection.Input,
                                6),
                            new SWPGenericDbParameter(
                                "@CarrName",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
                            new SWPGenericDbParameter(
                                "@CurrCode",
                                DbType.String,
                                ParameterDirection.Input,
                                8),
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
                                DbType.String,
                                ParameterDirection.Input,
                                6),
                            new SWPGenericDbParameter(
                                "@CarrName",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
                            new SWPGenericDbParameter(
                                "@CurrCode",
                                DbType.String,
                                ParameterDirection.Input,
                                8),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefCarrier";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _carrIDColumnName = "CarrID";

        public const string _carrNameColumnName = "CarrName";

        public const string _currCodeColumnName = "CurrCode";

        public int Insert(
            string carrID,
            string carrName,
            string currCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                6,
                validationErrorReport);
            ValidateParameterLength(
                "carrName",
                carrName.Length, 
                50,
                validationErrorReport);
            ValidateParameterLength(
                "currCode",
                currCode.Length, 
                8,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.String,
                    ParameterDirection.Input,
                    6,
                    carrID);
            SWPGenericDbParameter carrNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrName",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    carrName);
            SWPGenericDbParameter currCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CurrCode",
                    DbType.String,
                    ParameterDirection.Input,
                    8,
                    currCode);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCarrierInsert",
                    carrIDGenericDbParameter,
                    carrNameGenericDbParameter,
                    currCodeGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCarrierInsert",
                    carrIDGenericDbParameter,
                    carrNameGenericDbParameter,
                    currCodeGenericDbParameter);
            }

            return retVal;
        }

        public int Update(
            string carrID,
            string carrName,
            string currCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                6,
                validationErrorReport);
            ValidateParameterLength(
                "carrName",
                carrName.Length, 
                50,
                validationErrorReport);
            ValidateParameterLength(
                "currCode",
                currCode.Length, 
                8,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.String,
                    ParameterDirection.Input,
                    6,
                    carrID);
            SWPGenericDbParameter carrNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrName",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    carrName);
            SWPGenericDbParameter currCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CurrCode",
                    DbType.String,
                    ParameterDirection.Input,
                    8,
                    currCode);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCarrierUpdate",
                    carrIDGenericDbParameter,
                    carrNameGenericDbParameter,
                    currCodeGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCarrierUpdate",
                    carrIDGenericDbParameter,
                    carrNameGenericDbParameter,
                    currCodeGenericDbParameter);
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
                6,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.String,
                    ParameterDirection.Input,
                    6,
                    carrID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCarrierDelete",
                    carrIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCarrierDelete",
                    carrIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

