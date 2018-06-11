using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefLanguageReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefLanguageSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefLanguageInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefLanguageUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefLanguageDelete";
            }
        }

        public SAPRefLanguageReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefLanguageReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@LanguageCode",
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
                                "@LanguageCode",
                                DbType.String,
                                ParameterDirection.Input,
                                4),
                            new SWPGenericDbParameter(
                                "@Language",
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
                                "@LanguageCode",
                                DbType.String,
                                ParameterDirection.Input,
                                4),
                            new SWPGenericDbParameter(
                                "@Language",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefLanguage";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _languageCodeColumnName = "LanguageCode";

        public const string _languageColumnName = "Language";

        public int Insert(
            string languageCode,
            string language)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "languageCode",
                languageCode.Length, 
                4,
                validationErrorReport);
            ValidateParameterLength(
                "language",
                language.Length, 
                50,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter languageCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@LanguageCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    languageCode);
            SWPGenericDbParameter languageGenericDbParameter =
                new SWPGenericDbParameter(
                    "@Language",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    language);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefLanguageInsert",
                    languageCodeGenericDbParameter,
                    languageGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefLanguageInsert",
                    languageCodeGenericDbParameter,
                    languageGenericDbParameter);
            }

            return retVal;
        }

        public int Update(
            string languageCode,
            string language)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "languageCode",
                languageCode.Length, 
                4,
                validationErrorReport);
            ValidateParameterLength(
                "language",
                language.Length, 
                50,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter languageCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@LanguageCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    languageCode);
            SWPGenericDbParameter languageGenericDbParameter =
                new SWPGenericDbParameter(
                    "@Language",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    language);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefLanguageUpdate",
                    languageCodeGenericDbParameter,
                    languageGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefLanguageUpdate",
                    languageCodeGenericDbParameter,
                    languageGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            string languageCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "languageCode",
                languageCode.Length, 
                4,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter languageCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@LanguageCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    languageCode);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefLanguageDelete",
                    languageCodeGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefLanguageDelete",
                    languageCodeGenericDbParameter);
            }

            return retVal;
        }
    }
}

