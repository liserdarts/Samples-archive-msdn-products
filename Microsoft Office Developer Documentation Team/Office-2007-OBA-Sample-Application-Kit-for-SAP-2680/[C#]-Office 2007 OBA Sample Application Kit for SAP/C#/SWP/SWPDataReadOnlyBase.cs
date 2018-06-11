/* 
 ===============================================================================
 SWPCodeGeneration for .NET
 Version 3.0
 ===============================================================================
 Copyright © Software Pronto, Inc.  All rights reserved.
 THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
 OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 FITNESS FOR A PARTICULAR PURPOSE.
 ===============================================================================
 */

using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public abstract class SWPDataReadOnlyBase
    {
        protected SWPConnectionTextFormatType _connectionTextFormatType =
            SWPConnectionTextFormatType.ConnectionName;

        protected string _dbConnectionName;

        protected SWPDataReadOnlyBase()
        {
            _dbConnectionName = String.Empty;
        }

        public SWPDataReadOnlyBase(string dbConnectionNameOrText)
        {
            _dbConnectionName = dbConnectionNameOrText;
            _connectionTextFormatType =
                        SWPConnectionTextFormatType.ConnectionName;
        }

        public SWPDataReadOnlyBase(
            SWPConnectionTextFormatType connectionTextFormatType,
            string dbConnectionName)
        {
            _dbConnectionName = dbConnectionName;
            _connectionTextFormatType = connectionTextFormatType;
        }

        public abstract string SelectAllSPCommandText
        {
            get;
        }

        public abstract string TableName
        {
            get;
        }

        public abstract string DataSetName
        {
            get;
        }

        protected virtual DataSet Select(
            DataSet ds,
            string selectText,
            params SWPGenericDbParameter [] parameters)
        {
            return SWPDBHelper.StoredProcDataSet(
                _dbConnectionName,
                ds,
                selectText, 
                TableName,
                parameters);
        }

        protected virtual DataSet SqlStringSelect(
            DataSet ds,
            string selectText,
            params SWPGenericDbParameter[] parameters)
        {
            return SWPDBHelper.SqlStringDataSet(
                _dbConnectionName,
                ds,
                selectText,
                TableName,
                parameters);
        }

        protected virtual DbDataReader SqlStringReaderSelect(string selectText)
        {
            return SWPDBHelper.SqlStringDataReader(
                _dbConnectionName,
                selectText,
                CommandType.Text);
        }

        protected virtual DbDataReader ReaderSelect(string selectText)
        {
            return SWPDBHelper.StoredProcDataReader(
                _dbConnectionName,
                selectText);
        }

        protected virtual DbDataReader ReaderSelect(
            string selectText,
            params SWPGenericDbParameter[] parameters)
        {
            return SWPDBHelper.StoredProcDataReader(
                _dbConnectionName,
                selectText,
                parameters);
        }
        
        protected virtual DbDataReader SqlStringReaderSelect(
            string selectText,
            params SWPGenericDbParameter[] parameters)
        {
            return SWPDBHelper.SqlStringDataReader(
                _dbConnectionName,
                selectText,
                parameters);
        }

        protected static void ValidateParameterLength(
            string parameterName,
            int parameterLength,
            int maxLength,
            StringBuilder validationErrorReport)
        {
            if (maxLength > 0)
            {
                int excess = parameterLength - maxLength;

                if (excess > 0)
                {
                    if (validationErrorReport.Length > 0)
                    {
                        validationErrorReport.Append(Environment.NewLine);
                    }

                    validationErrorReport.Append(String.Format(
                        "Parameter [{0}] is limited to a maximum length of {1}. You have passed a value with an excess length of {2}.",
                        parameterName,
                        maxLength.ToString(),
                        excess.ToString()));
                }
            }
        }
    }
}