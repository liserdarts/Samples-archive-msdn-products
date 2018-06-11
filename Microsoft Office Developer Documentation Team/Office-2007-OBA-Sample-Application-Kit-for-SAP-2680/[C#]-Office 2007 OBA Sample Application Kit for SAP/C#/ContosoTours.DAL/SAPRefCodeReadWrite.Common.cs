using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCodeReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefCodeSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefCodeInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefCodeUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefCodeDelete";
            }
        }

        public SAPRefCodeReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefCodeReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@RefTypeCode",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                2),
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
                                "@RefTypeCode",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                2),
                            new SWPGenericDbParameter(
                                "@TypeCode",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                10),
                            new SWPGenericDbParameter(
                                "@TypeName",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                100),
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
                                "@RefTypeCode",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                2),
                            new SWPGenericDbParameter(
                                "@TypeCode",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                10),
                            new SWPGenericDbParameter(
                                "@TypeName",
                                DbType.AnsiStringFixedLength,
                                ParameterDirection.Input,
                                100),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefCode";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _refTypeCodeColumnName = "RefTypeCode";

        public const string _typeCodeColumnName = "TypeCode";

        public const string _typeNameColumnName = "TypeName";

        public int Insert(
            string refTypeCode,
            string typeCode,
            string typeName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "refTypeCode",
                refTypeCode.Length, 
                2,
                validationErrorReport);
            ValidateParameterLength(
                "typeCode",
                typeCode.Length, 
                10,
                validationErrorReport);
            ValidateParameterLength(
                "typeName",
                typeName.Length, 
                100,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter refTypeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@RefTypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    2,
                    refTypeCode);
            SWPGenericDbParameter typeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@TypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    10,
                    typeCode);
            SWPGenericDbParameter typeNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@TypeName",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    100,
                    typeName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCodeInsert",
                    refTypeCodeGenericDbParameter,
                    typeCodeGenericDbParameter,
                    typeNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCodeInsert",
                    refTypeCodeGenericDbParameter,
                    typeCodeGenericDbParameter,
                    typeNameGenericDbParameter);
            }

            return retVal;
        }

        public int Update(
            string refTypeCode,
            string typeCode,
            string typeName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "refTypeCode",
                refTypeCode.Length, 
                2,
                validationErrorReport);
            ValidateParameterLength(
                "typeCode",
                typeCode.Length, 
                10,
                validationErrorReport);
            ValidateParameterLength(
                "typeName",
                typeName.Length, 
                100,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter refTypeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@RefTypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    2,
                    refTypeCode);
            SWPGenericDbParameter typeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@TypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    10,
                    typeCode);
            SWPGenericDbParameter typeNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@TypeName",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    100,
                    typeName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCodeUpdate",
                    refTypeCodeGenericDbParameter,
                    typeCodeGenericDbParameter,
                    typeNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCodeUpdate",
                    refTypeCodeGenericDbParameter,
                    typeCodeGenericDbParameter,
                    typeNameGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            string refTypeCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "refTypeCode",
                refTypeCode.Length, 
                2,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter refTypeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@RefTypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    2,
                    refTypeCode);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefCodeDelete",
                    refTypeCodeGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefCodeDelete",
                    refTypeCodeGenericDbParameter);
            }

            return retVal;
        }
    }
}

