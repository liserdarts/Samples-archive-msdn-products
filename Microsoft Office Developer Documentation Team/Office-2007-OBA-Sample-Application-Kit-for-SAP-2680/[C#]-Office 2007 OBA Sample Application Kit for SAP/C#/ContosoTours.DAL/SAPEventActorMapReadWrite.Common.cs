using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventActorMapReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventActorMapSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspEventActorMapInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPEventActorMapUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspEventActorMapDelete";
            }
        }

        public SAPEventActorMapReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPEventActorMapReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@EventActorMapID",
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
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventActorID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventActorMapID",
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
                                "@EventActorMapID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventActorID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "EventActorMap";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _eventActorMapIDColumnName = "EventActorMapID";

        public const string _eventIDColumnName = "EventID";

        public const string _eventActorIDColumnName = "EventActorID";

        public int Insert(
            int eventID,
            int eventActorID,
            out int eventActorMapID)
        {
            int retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);
            SWPGenericDbParameter eventActorMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorMapID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventActorMapInsert",
                    eventIDGenericDbParameter,
                    eventActorIDGenericDbParameter,
                    eventActorMapIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorMapInsert",
                    eventIDGenericDbParameter,
                    eventActorIDGenericDbParameter,
                    eventActorMapIDGenericDbParameter);
            }

            eventActorMapID = (int)eventActorMapIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int eventActorMapID,
            int eventID,
            int eventActorID)
        {
            int retVal;
            SWPGenericDbParameter eventActorMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorMapID);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
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
                    "uspEventActorMapUpdate",
                    eventActorMapIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    eventActorIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorMapUpdate",
                    eventActorMapIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    eventActorIDGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int eventActorMapID)
        {
            int retVal;
            SWPGenericDbParameter eventActorMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorMapID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventActorMapDelete",
                    eventActorMapIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorMapDelete",
                    eventActorMapIDGenericDbParameter);
            }

            return retVal;
        }

        public int DeleteByEventActorID(
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
                    "uspEventActorMapDeleteByEventActorID",
                    eventActorIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorMapDeleteByEventActorID",
                    eventActorIDGenericDbParameter);
            }

            return retVal;
        }

        public int DeleteByEventID(
            int eventID)
        {
            int retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventActorMapDeleteByEventID",
                    eventIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventActorMapDeleteByEventID",
                    eventIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

