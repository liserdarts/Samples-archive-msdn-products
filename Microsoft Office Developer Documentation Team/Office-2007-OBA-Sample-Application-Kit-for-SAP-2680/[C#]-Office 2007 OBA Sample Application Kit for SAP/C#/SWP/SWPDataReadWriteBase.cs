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
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public abstract class SWPDataReadWriteBase : SWPDataReadOnlyBase
    {
        protected ISWPTransactionDBConnection _dbTransactionConnection = null;

        public SWPDataReadWriteBase(string dbConnectionName) : base(dbConnectionName)
        {
        }

        public SWPDataReadWriteBase(
            SWPConnectionTextFormatType connectionTextFormatType,
            string dbConnectionNameOrText)
            : base(connectionTextFormatType, dbConnectionNameOrText)
        {
        }

        public SWPDataReadWriteBase(ISWPTransactionDBConnection dbTransactionConnection) : 
            base() 
        {
            _dbTransactionConnection = dbTransactionConnection;
        }

        public abstract string DeleteSPCommandText
        {
            get;
        }

        public abstract string InsertSPCommandText
        {
            get;
        }

        public abstract string UpdateSPCommandText
        {
            get;
        }

        public abstract DbParameter [] DeleteSPParameters
        {
            get;
        }

        public abstract DbParameter [] InsertSPParameters
        {
            get;
        }

        public abstract DbParameter [] UpdateSPParameters
        {
            get;
        }

        private void Assign_ParameterName_DBType_Direction_Size_Value(
          DbParameter parameter,
          DbParameter parameterAdded)
        {
            parameter.ParameterName = parameterAdded.ParameterName;
            parameter.DbType = parameterAdded.DbType;
            parameter.Direction = parameterAdded.Direction;
            parameter.Size = parameterAdded.Size;
            parameter.Value = parameterAdded.Value;
        }

        private void PopulateCommandWithParameters(DbCommand command, DbParameter[] parameters)
        {
            DbParameter parameter;

            foreach (DbParameter parameterAdded in parameters)
            {
                parameter = command.CreateParameter();
                Assign_ParameterName_DBType_Direction_Size_Value(
                    parameter, 
                    parameterAdded);
                command.Parameters.Add(parameter);
            }
        }

        private string ColumnName(string connectionName, string parameterName)
        {
            string retVal = String.Empty;

            switch (System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    retVal = parameterName.Substring(0, parameterName.Length - "Parameter".Length);
                    break;
                default:
                    retVal = parameterName.Substring(1);
                    break;
            }

            return retVal;
        }
        
        private void ProcessCommand(
            string storedProcedureName,
            DataTable table,
            DataRowVersion dataRowVersion, 
            DbParameter [] parameters)
        {
            string candidateColumnName;
            bool parmSet;
            Database db = null;
            DbCommand command;
            DbConnection conn = null;

            if (table == null)
            {
                return ;
            }

            try
            {
                if (_dbTransactionConnection == null)
                {
                    db = SWPDBHelper.DBConnection(
                        _connectionTextFormatType,
                        _dbConnectionName);
                    command = db.GetStoredProcCommand(storedProcedureName);
                    conn = db.CreateConnection();
                    conn.Open();
                    command.Connection = conn;
                }

                else
                {
                    command = _dbTransactionConnection.TransactionDatabase.GetStoredProcCommand(
                        storedProcedureName);
                }

                command.CommandType = CommandType.StoredProcedure;
                foreach (DataRow row in table.Rows)
                {
                    command.Parameters.Clear();
                    PopulateCommandWithParameters(command, parameters);
                    foreach (DbParameter commandParameter in command.Parameters)
                    {
                        parmSet = false;
                        // Skip the @ character
                        candidateColumnName = ColumnName(_dbConnectionName, commandParameter.ParameterName);

                        // we want to be case insensitive so lets 
                        // do this by hand
                        foreach (DataColumn column in table.Columns)
                        {
                            if (String.Compare(
                                column.ColumnName,
                                candidateColumnName,
                                true) == 0)
                            {
                                commandParameter.Value = row[column, dataRowVersion];
                                parmSet = true;

                                break;
                            }
                        }

                        if (!parmSet)
                        {
                            throw new Exception(
                                "No matching column found for parameter: " +
                                commandParameter.ParameterName);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }

            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    conn = null;
                }
            }
        }

        protected override DataSet SqlStringSelect(
            DataSet ds,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            if (null == _dbTransactionConnection)
            {
                SWPDBHelper.SqlStringDataSet(
                    _dbConnectionName,
                    ds,
                    commandText,
                    TableName,
                    parameters);
            }

            else
            {
                SelectTransaction(ds, commandText, CommandType.Text, parameters);
            }

            return ds;
        }

        protected DataSet SelectTransaction(
            DataSet ds,
            string commandText,
            CommandType commandType,
            params SWPGenericDbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataAdapter dataAdapter;

            if (commandType == CommandType.StoredProcedure)
            {
                command =
                   _dbTransactionConnection.TransactionDatabase.GetStoredProcCommand(
                       commandText);
            }

            else
            {
                command =
                  _dbTransactionConnection.TransactionDatabase.GetSqlStringCommand(
                      commandText);
            }

            command.Transaction = _dbTransactionConnection.Transaction;
            command.Connection = _dbTransactionConnection.Transaction.Connection;
            command.CommandType = commandType;
            dataAdapter =
                _dbTransactionConnection.TransactionDatabase.GetDataAdapter();
            PopulateCommandWithParameters(command, parameters);
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(ds, TableName);

            return ds;
        }

        protected override DataSet Select(
            DataSet ds,
            string selectStoredProcedure,
            params SWPGenericDbParameter[] parameters)
        {
            if (null == _dbTransactionConnection)
            {
                SWPDBHelper.StoredProcDataSet(
                    _dbConnectionName,
                    ds,
                    selectStoredProcedure, 
                    TableName,
                    parameters);
            }

            else
            {
                ds = SelectTransaction(
                    ds, 
                    selectStoredProcedure, 
                    CommandType.StoredProcedure, 
                    parameters);
            }

            return ds;
        }

        // This will only pick up rows deleted with DataRow.Delete
        // DO NOT USE DataTable.Rows.Remove!!!!
        protected void Update(DataTable dt)
        {
            DataTable dataTableDeleted = dt.GetChanges(DataRowState.Deleted);
            DataTable dataTableAdded = dt.GetChanges(DataRowState.Added);
            DataTable dataTableModified = dt.GetChanges(DataRowState.Modified);

            IsValidCommand("Delete", 
                DeleteSPCommandText,
                dataTableDeleted);
            IsValidCommand("Insert", 
                InsertSPCommandText,
                dataTableAdded);
            IsValidCommand("Update", 
                UpdateSPCommandText, 
                dataTableModified);
            ProcessCommand(
                DeleteSPCommandText,
                dataTableDeleted,
                DataRowVersion.Original,
                DeleteSPParameters);
            ProcessCommand(
                InsertSPCommandText, 
                dataTableAdded,
                DataRowVersion.Current,
                InsertSPParameters);
            ProcessCommand(
                UpdateSPCommandText, 
                dataTableModified,
                DataRowVersion.Current,
                UpdateSPParameters);
            dt.AcceptChanges();
        }

        protected void IsValidCommand(string procType, string procName, DataTable dt)
        {
            if (procName == null)
            {
                procName = String.Empty;
            }

            if ((procName.Trim() == String.Empty) &&
                ((null != dt) && (dt.Rows.Count > 0)))
            {
                throw new Exception(
                    String.Format("DataSet update failed. No {0} procedure found.", procType));
            }
        }

        protected override DbDataReader SqlStringReaderSelect(string selectText)
        {
            if (_dbTransactionConnection == null)
            {
                return base.SqlStringReaderSelect(selectText);
            }

            else
            {
                DbCommand command =
                    _dbTransactionConnection.TransactionDatabase.GetSqlStringCommand(selectText);

                command.CommandType = CommandType.Text;

                return command.ExecuteReader();
            }
        }

        protected override DbDataReader ReaderSelect(string selectText)
        {
            if (_dbTransactionConnection == null)
            {
                return base.ReaderSelect(selectText);
            }

            else
            {
                DbCommand command =
                    _dbTransactionConnection.TransactionDatabase.GetSqlStringCommand(selectText);

                command.CommandType = CommandType.StoredProcedure;

                return command.ExecuteReader();
            }
        }

        protected override DbDataReader ReaderSelect(
            string selectText,
            params SWPGenericDbParameter[] parameters)
        {
            if (_dbTransactionConnection == null)
            {
                return base.ReaderSelect(selectText, parameters);
            }

            else
            {
                DbCommand command =
                    _dbTransactionConnection.TransactionDatabase.GetSqlStringCommand(selectText);

                command.CommandType = CommandType.StoredProcedure;
                PopulateCommandWithParameters(command, parameters);

                return command.ExecuteReader();
            }
        }

        protected override DbDataReader SqlStringReaderSelect(
            string selectText,
            params SWPGenericDbParameter[] parameters)
        {
            if (_dbTransactionConnection == null)
            {
                return base.SqlStringReaderSelect(selectText, parameters);
            }

            else
            {
                DbCommand command =
                    _dbTransactionConnection.TransactionDatabase.GetSqlStringCommand(selectText);

                command.CommandType = CommandType.Text;
                PopulateCommandWithParameters(command, parameters);

                return command.ExecuteReader();
            }
        }
    }
}