using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventActorReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventActorSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspEventActorInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPEventActorUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspEventActorDelete";
            }
        }

        public SAPEventActorReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPEventActorReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@EventActorID",
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
                                "@EventActorName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@EventActorID",
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
                                "@EventActorID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventActorName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "EventActor";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _eventActorIDColumnName = "EventActorID";

        public const string _eventActorNameColumnName = "EventActorName";

        public int Insert(
            string eventActorName,
            out int eventActorID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventActorName",
                eventActorName.Length, 
                255,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventActorNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventActorName);
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventActorInsert",
                    eventActorNameGenericDbParameter,
                    eventActorIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorInsert",
                    eventActorNameGenericDbParameter,
                    eventActorIDGenericDbParameter);
            }

            eventActorID = (int)eventActorIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int eventActorID,
            string eventActorName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventActorName",
                eventActorName.Length, 
                255,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);
            SWPGenericDbParameter eventActorNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventActorName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventActorUpdate",
                    eventActorIDGenericDbParameter,
                    eventActorNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorUpdate",
                    eventActorIDGenericDbParameter,
                    eventActorNameGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int eventActorID)
        {
            int retVal;
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventActorDelete",
                    eventActorIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorDelete",
                    eventActorIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

