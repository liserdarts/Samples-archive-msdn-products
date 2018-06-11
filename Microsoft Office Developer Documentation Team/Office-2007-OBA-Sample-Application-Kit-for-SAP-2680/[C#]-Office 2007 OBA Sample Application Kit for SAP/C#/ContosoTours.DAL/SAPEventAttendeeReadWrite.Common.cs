using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventAttendeeSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspEventAttendeeInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPEventAttendeeUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspEventAttendeeDelete";
            }
        }

        public SAPEventAttendeeReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPEventAttendeeReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@EventAttendeeID",
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
                                "@PackageID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@CustomerNumber",
                                DbType.StringFixedLength,
                                ParameterDirection.Input,
                                25),
                            new SWPGenericDbParameter(
                                "@DateOfBirth",
                                DbType.DateTime,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@Created",
                                DbType.DateTime,
                                ParameterDirection.Output,
                                0),
                            new SWPGenericDbParameter(
                                "@EventAttendeeID",
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
                                "@EventAttendeeID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@PackageID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@CustomerNumber",
                                DbType.StringFixedLength,
                                ParameterDirection.Input,
                                25),
                            new SWPGenericDbParameter(
                                "@DateOfBirth",
                                DbType.DateTime,
                                ParameterDirection.Input,
                                0),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "EventAttendee";

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

        public const string _packageNameColumnName = "PackageName";

        public int Insert(
            int packageID,
            string customerNumber,
            DateTime dateOfBirth,
            out DateTime created,
            out int eventAttendeeID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "customerNumber",
                customerNumber.Length, 
                25,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
            SWPGenericDbParameter customerNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CustomerNumber",
                    DbType.StringFixedLength,
                    ParameterDirection.Input,
                    25,
                    customerNumber);
            SWPGenericDbParameter dateOfBirthGenericDbParameter =
                new SWPGenericDbParameter(
                    "@DateOfBirth",
                    DbType.DateTime,
                    ParameterDirection.Input,
                    0,
                    dateOfBirth);
            SWPGenericDbParameter createdGenericDbParameter =
                new SWPGenericDbParameter(
                    "@Created",
                    DbType.DateTime,
                    ParameterDirection.Output,
                    0);
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventAttendeeInsert",
                    packageIDGenericDbParameter,
                    customerNumberGenericDbParameter,
                    dateOfBirthGenericDbParameter,
                    createdGenericDbParameter,
                    eventAttendeeIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventAttendeeInsert",
                    packageIDGenericDbParameter,
                    customerNumberGenericDbParameter,
                    dateOfBirthGenericDbParameter,
                    createdGenericDbParameter,
                    eventAttendeeIDGenericDbParameter);
            }

            created = (DateTime)createdGenericDbParameter.Value;
            eventAttendeeID = (int)eventAttendeeIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int eventAttendeeID,
            int packageID,
            string customerNumber,
            DateTime dateOfBirth)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "customerNumber",
                customerNumber.Length, 
                25,
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
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
            SWPGenericDbParameter customerNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CustomerNumber",
                    DbType.StringFixedLength,
                    ParameterDirection.Input,
                    25,
                    customerNumber);
            SWPGenericDbParameter dateOfBirthGenericDbParameter =
                new SWPGenericDbParameter(
                    "@DateOfBirth",
                    DbType.DateTime,
                    ParameterDirection.Input,
                    0,
                    dateOfBirth);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventAttendeeUpdate",
                    eventAttendeeIDGenericDbParameter,
                    packageIDGenericDbParameter,
                    customerNumberGenericDbParameter,
                    dateOfBirthGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventAttendeeUpdate",
                    eventAttendeeIDGenericDbParameter,
                    packageIDGenericDbParameter,
                    customerNumberGenericDbParameter,
                    dateOfBirthGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int eventAttendeeID)
        {
            int retVal;
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventAttendeeDelete",
                    eventAttendeeIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventAttendeeDelete",
                    eventAttendeeIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

