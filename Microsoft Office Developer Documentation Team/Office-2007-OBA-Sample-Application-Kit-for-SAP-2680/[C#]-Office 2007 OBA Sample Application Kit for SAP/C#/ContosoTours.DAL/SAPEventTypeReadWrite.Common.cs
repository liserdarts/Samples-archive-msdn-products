using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventTypeReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventTypeSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspEventTypeInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPEventTypeUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspEventTypeDelete";
            }
        }

        public SAPEventTypeReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPEventTypeReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@EventTypeID",
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
                                "@EventTypeName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@EventTypeDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@EventTypeID",
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
                                "@EventTypeID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventTypeName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@EventTypeDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "EventType";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _eventTypeIDColumnName = "EventTypeID";

        public const string _eventTypeNameColumnName = "EventTypeName";

        public const string _eventTypeDescriptionColumnName = "EventTypeDescription";

        public int Insert(
            string eventTypeName,
            string eventTypeDescription,
            out int eventTypeID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventTypeName",
                eventTypeName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "eventTypeDescription",
                eventTypeDescription.Length, 
                4000,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventTypeNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventTypeName);
            SWPGenericDbParameter eventTypeDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    eventTypeDescription);
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventTypeInsert",
                    eventTypeNameGenericDbParameter,
                    eventTypeDescriptionGenericDbParameter,
                    eventTypeIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventTypeInsert",
                    eventTypeNameGenericDbParameter,
                    eventTypeDescriptionGenericDbParameter,
                    eventTypeIDGenericDbParameter);
            }

            eventTypeID = (int)eventTypeIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int eventTypeID,
            string eventTypeName,
            string eventTypeDescription)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventTypeName",
                eventTypeName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "eventTypeDescription",
                eventTypeDescription.Length, 
                4000,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);
            SWPGenericDbParameter eventTypeNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventTypeName);
            SWPGenericDbParameter eventTypeDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    eventTypeDescription);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventTypeUpdate",
                    eventTypeIDGenericDbParameter,
                    eventTypeNameGenericDbParameter,
                    eventTypeDescriptionGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventTypeUpdate",
                    eventTypeIDGenericDbParameter,
                    eventTypeNameGenericDbParameter,
                    eventTypeDescriptionGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int eventTypeID)
        {
            int retVal;
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventTypeDelete",
                    eventTypeIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventTypeDelete",
                    eventTypeIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

