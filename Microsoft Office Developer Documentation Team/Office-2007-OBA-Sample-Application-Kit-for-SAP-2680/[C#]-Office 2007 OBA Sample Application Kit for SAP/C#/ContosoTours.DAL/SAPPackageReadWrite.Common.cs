using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspPackageSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspPackageInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPPackageUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspPackageDelete";
            }
        }

        public SAPPackageReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPPackageReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@PackageID",
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
                                "@PackageName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@PackageDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@PackageImage",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@PackageID",
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
                                "@PackageID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@PackageName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@PackageDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@PackageImage",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "Package";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _packageIDColumnName = "PackageID";

        public const string _packageNameColumnName = "PackageName";

        public const string _packageDescriptionColumnName = "PackageDescription";

        public const string _packageImageColumnName = "PackageImage";

        public int Insert(
            string packageName,
            string packageDescription,
            byte [] packageImage,
            out int packageID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "packageName",
                packageName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "packageDescription",
                packageDescription.Length, 
                4000,
                validationErrorReport);
            ValidateParameterLength(
                "packageImage",
                packageImage.Length, 
                2147483647,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter packageNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    packageName);
            SWPGenericDbParameter packageDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    packageDescription);
            SWPGenericDbParameter packageImageGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageImage",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    packageImage);
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspPackageInsert",
                    packageNameGenericDbParameter,
                    packageDescriptionGenericDbParameter,
                    packageImageGenericDbParameter,
                    packageIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspPackageInsert",
                    packageNameGenericDbParameter,
                    packageDescriptionGenericDbParameter,
                    packageImageGenericDbParameter,
                    packageIDGenericDbParameter);
            }

            packageID = (int)packageIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int packageID,
            string packageName,
            string packageDescription,
            byte [] packageImage)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "packageName",
                packageName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "packageDescription",
                packageDescription.Length, 
                4000,
                validationErrorReport);
            ValidateParameterLength(
                "packageImage",
                packageImage.Length, 
                2147483647,
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
            SWPGenericDbParameter packageNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    packageName);
            SWPGenericDbParameter packageDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    packageDescription);
            SWPGenericDbParameter packageImageGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageImage",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    packageImage);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspPackageUpdate",
                    packageIDGenericDbParameter,
                    packageNameGenericDbParameter,
                    packageDescriptionGenericDbParameter,
                    packageImageGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspPackageUpdate",
                    packageIDGenericDbParameter,
                    packageNameGenericDbParameter,
                    packageDescriptionGenericDbParameter,
                    packageImageGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int packageID)
        {
            int retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspPackageDelete",
                    packageIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspPackageDelete",
                    packageIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

