using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageEventMapReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspPackageEventMapSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspPackageEventMapInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPPackageEventMapUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspPackageEventMapDelete";
            }
        }

        public SAPPackageEventMapReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPPackageEventMapReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@PackageEventMapID",
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
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@PackageEventMapID",
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
                                "@PackageEventMapID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@PackageID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "PackageEventMap";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _packageEventMapIDColumnName = "PackageEventMapID";

        public const string _packageIDColumnName = "PackageID";

        public const string _eventIDColumnName = "EventID";

        public const string _packageNameColumnName = "PackageName";

        public int Insert(
            int packageID,
            int eventID,
            out int packageEventMapID)
        {
            int retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspPackageEventMapInsert",
                    packageIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    packageEventMapIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspPackageEventMapInsert",
                    packageIDGenericDbParameter,
                    eventIDGenericDbParameter,
                    packageEventMapIDGenericDbParameter);
            }

            packageEventMapID = (int)packageEventMapIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int packageEventMapID,
            int packageID,
            int eventID)
        {
            int retVal;
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageEventMapID);
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
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
                    "uspPackageEventMapUpdate",
                    packageEventMapIDGenericDbParameter,
                    packageIDGenericDbParameter,
                    eventIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspPackageEventMapUpdate",
                    packageEventMapIDGenericDbParameter,
                    packageIDGenericDbParameter,
                    eventIDGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int packageEventMapID)
        {
            int retVal;
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageEventMapID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspPackageEventMapDelete",
                    packageEventMapIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspPackageEventMapDelete",
                    packageEventMapIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

