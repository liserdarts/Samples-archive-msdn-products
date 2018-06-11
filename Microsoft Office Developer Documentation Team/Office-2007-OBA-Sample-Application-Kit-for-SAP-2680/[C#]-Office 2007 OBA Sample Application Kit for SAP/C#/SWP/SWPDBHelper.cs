using System;
using System.IO;
using System.Data;
using System.Text;
using System.Data.Common;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public static class SWPDBHelper
    {
        public static Database DBFromConnectionName(string connectionName)
        {
            return Database.DBFromConnectionName(connectionName);
        }

        public static Database DBFromConnectionText(string connectionText)
        {
            return Database.DBFromConnectionText(connectionText);
        }

        public static Database DBConnection(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText)
        {
            if (connectionTextFormatType == SWPConnectionTextFormatType.ConnectionName)
            {
                return DBFromConnectionName(connectionNameOrText);
            }

            else // if (connectionTextFormatType == SWPConnectionTextFormatType.ConnectionString)
            {
                return DBFromConnectionText(connectionNameOrText);
            }
        }

        // We need to return both the connection and data reader so that
        // we can clean up the connection (as opposed to just leaving it
        // hanging for garabage collection)
        private static SWPConnectionDataReader SqlStringDataReader(
            Database db,
            string commandText)
        {
            DbCommand command = db.GetSqlStringCommand(commandText);
            DbConnection connection = db.CreateConnection();
            SWPConnectionDataReader retVal;

            connection.Open();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                retVal = new SWPConnectionDataReader(
                    connection, 
                    command.ExecuteReader());
            }

            catch
            {
                // if there is an error insre we close the connection
                connection.Close();
                throw; 
            }

            return retVal;
        }

        public static SWPConnectionDataReader SqlStringDataReader(
            string connectionName,
            string commandText)
        {
            return SqlStringDataReader(DBFromConnectionName(connectionName), commandText);
        }

        public static SWPConnectionDataReader SqlStringDataReader(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText)
        {
            return SqlStringDataReader(
                DBConnection(connectionTextFormatType, connectionNameOrText), 
                commandText);
        }

        private static DataSet SqlStringDataSet(
            Database db,
            string commandText)
        {
            DataSet retVal;

            using (DbConnection connection = db.CreateConnection())
            {
                DbCommand command; 

                connection.Open();
                command = db.GetSqlStringCommand(commandText);
                command.Connection = connection;
                retVal = db.ExecuteDataSet(command);
                connection.Close();
            }

            return retVal;
        }

        public static DataSet SqlStringDataSet(
            string connectionName,
            string commandText)
        {
            return SqlStringDataSet(DBFromConnectionName(connectionName), commandText);
        }

        public static DataSet SqlStringDataSet(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText)
        {
            return SqlStringDataSet(
                DBConnection(connectionTextFormatType, connectionNameOrText), 
                commandText);
        }

        private static void PopulateCommandWithParameters(
            DbCommand command, 
            SWPGenericDbParameter[] parameters)
        {
            DbParameter parameter;

            foreach (SWPGenericDbParameter genericParameter in parameters)
            {
                parameter = command.CreateParameter();
                genericParameter.Assign_ParameterName_DBType_Direction_Size_Value(
                    parameter);
                command.Parameters.Add(parameter);
            }
        }

        private static void GetReturnValueForCommandParameters(
            DbCommand command, 
            SWPGenericDbParameter[] parameters)
        {
            SWPGenericDbParameter genericParameter;

            foreach (DbParameter parameter in command.Parameters)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput) ||
                    (parameter.Direction == ParameterDirection.Output))
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        genericParameter = parameters[i];
                        // we need to look up by name b/c when a stored proc 
                        // returns a value it adds a paramter to the list.
                        if (genericParameter.ParameterName == parameter.ParameterName)
                        {
                            genericParameter.Value = parameter.Value;
                            break;
                        }
                    }
                }
            }
        }

        public static int StoredProcExecuteNonQuery(
            ISWPTransactionDBConnection transactionDBConnection,
            string storedProcedureName,
            params SWPGenericDbParameter[] parameters)
        {
            DbCommand command = 
                transactionDBConnection.TransactionDatabase.GetStoredProcCommand(
                    storedProcedureName);
            int retVal;

            command.Connection = transactionDBConnection.Transaction.Connection;
            command.Transaction = transactionDBConnection.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            PopulateCommandWithParameters(command, parameters);
            retVal = command.ExecuteNonQuery();
            GetReturnValueForCommandParameters(command, parameters);

            return retVal;
        }

        private static int StoredProcExecuteNonQuery(
            Database db,
            string storedProcedureName,
            params SWPGenericDbParameter[] parameters)
        {
            int retVal;
            using (DbConnection connection = db.CreateConnection())
            {
                DbCommand command = db.GetStoredProcCommand(storedProcedureName);

                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                PopulateCommandWithParameters(command, parameters);
                retVal = command.ExecuteNonQuery();
                GetReturnValueForCommandParameters(command, parameters);
                connection.Close();
            }

            return retVal;
        }

        public static int StoredProcExecuteNonQuery(
            string connectionName,
            string storedProcedureName,
            params SWPGenericDbParameter[] parameters)
        {
            return StoredProcExecuteNonQuery(
                DBFromConnectionName(connectionName), 
                storedProcedureName, 
                parameters);
        }

        public static int StoredProcExecuteNonQuery(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string storedProcedureName,
            params SWPGenericDbParameter[] parameters)
        {
            return StoredProcExecuteNonQuery(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                storedProcedureName,
                parameters);
        }

        private static DataSet StoredProcDataSet(
            Database db,
            DataSet ds,
            string storedProcedureName,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            using (DbConnection connection = db.CreateConnection())
            {
                DbCommand command = db.GetStoredProcCommand(storedProcedureName);
                DbDataAdapter dataAdapter = db.GetDataAdapter();

                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                PopulateCommandWithParameters(command, parameters);
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(ds, tableName);
                GetReturnValueForCommandParameters(command, parameters);
                connection.Close();
            }

            return ds;
        }

        public static DataSet StoredProcDataSet(
            string connectionName,
            DataSet ds,
            string storedProcedureName,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            return StoredProcDataSet(
                DBFromConnectionName(connectionName),
                ds,
                storedProcedureName,
                tableName,
                parameters);
        }

        public static DataSet StoredProcDataSet(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            DataSet ds,
            string storedProcedureName,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            return StoredProcDataSet(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                ds,
                storedProcedureName,
                tableName,
                parameters);
        }

        public static DataSet StoredProcDataSet(
            string connectionName,
            string storedProcedureName,
            string dataSetName,
            string tableName,
            params SWPGenericDbParameter [] parameters)
        {
            DataSet ds = new DataSet(dataSetName);

            return StoredProcDataSet(
                connectionName,
                ds, 
                storedProcedureName, 
                tableName,
                parameters);
        }

        public static DataSet StoredProcDataSet(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string storedProcedureName,
            string dataSetName,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            DataSet ds = new DataSet(dataSetName);

            return StoredProcDataSet(
                connectionTextFormatType,
                connectionNameOrText,
                ds,
                storedProcedureName,
                tableName,
                parameters);
        }

        private static DataSet DataSetSchema(
            Database db,
            string dataSetName,
            string commandText,
            string sourceTableName,
            SchemaType schemaType,
            CommandType commandType)

        {
            DataSet ds = null;
            DbCommand command = null;
            DbDataAdapter dataAdapter;

            using (DbConnection connection = db.CreateConnection())
            {
                if (commandType == CommandType.Text)
                {
                    command = db.GetSqlStringCommand(commandText);
                }
                else
                {
                    command = db.GetStoredProcCommand(commandText);
                }

                dataAdapter = db.GetDataAdapter();                
                ds = new DataSet(dataSetName);
                command.Connection = connection;
                command.CommandType = commandType;
                dataAdapter.SelectCommand = command;
                dataAdapter.FillSchema(ds, schemaType, sourceTableName);
                if (ds.Tables.Count == 0)
                {
                    ds = new DataSet(dataSetName);
                    dataAdapter.Fill(ds, 0, 1, sourceTableName);
                    ds.Clear();
                }

                connection.Close();
            }

            return ds;
        }

        private static DataSet DataSetSchema(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string dataSetName,
            string commandText,
            string sourceTableName,
            SchemaType schemaType,
            CommandType commandType)
        {
            return DataSetSchema(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                dataSetName,
                commandText,
                sourceTableName,
                schemaType,
                commandType);
        }

        public static DataSet SqlStringDataSetSchema(
            string connectionName,
            string dataSetName,
            string commandText,
            string sourceTableName,
            SchemaType schemaType)
        {
            return DataSetSchema(
                DBFromConnectionName(connectionName), 
                dataSetName, 
                commandText, 
                sourceTableName, 
                schemaType,
                CommandType.Text);
        }

        public static DataSet SqlStringDataSetSchema(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string dataSetName,
            string commandText,
            string sourceTableName,
            SchemaType schemaType)
        {
            return DataSetSchema(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                dataSetName,
                commandText,
                sourceTableName,
                schemaType,
                CommandType.Text);
        }

        public static DataSet StoredProcDataSetSchema(
            string connectionName,
            string dataSetName,
            string commandText,
            string sourceTableName,
            SchemaType schemaType)
        {
            return DataSetSchema(
                DBFromConnectionName(connectionName),
                dataSetName,
                commandText,
                sourceTableName,
                schemaType,
                CommandType.StoredProcedure);
        }

        public static DataSet StoredProcDataSetSchema(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string dataSetName,
            string commandText,
            string sourceTableName,
            SchemaType schemaType)
        {
            return DataSetSchema(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                dataSetName,
                commandText,
                sourceTableName,
                schemaType,
                CommandType.StoredProcedure);
        }

        private static DbDataReader StoredProcDataReader(
            Database db,
            string commandText)
        {
            DbDataReader reader = null;
            DbConnection connection = db.CreateConnection();
            DbCommand command;

            connection.Open();
            command = db.GetStoredProcCommand(commandText);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static DbDataReader StoredProcDataReader(
            string connectionName,
            string commandText)
        {
            return StoredProcDataReader(
                DBFromConnectionName(connectionName),
                commandText);
        }

        public static DbDataReader StoredProcDataReader(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText)
        {
            return StoredProcDataReader(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                commandText);
        }

        public static DbDataReader StoredProcDataReader(
            Database db,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            DbDataReader reader = null;
            DbConnection connection = db.CreateConnection();
            DbCommand command;

            connection.Open();
            command = db.GetStoredProcCommand(commandText);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            PopulateCommandWithParameters(command, parameters);
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static DbDataReader StoredProcDataReader(
            string connectionName,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            return StoredProcDataReader(
                DBFromConnectionName(connectionName),
                commandText,
                parameters);
        }

        public static DbDataReader StoredProcDataReader(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            return StoredProcDataReader(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                commandText,
                parameters);
        }

        public static int SqlStringExecuteNonQuery(
            ISWPTransactionDBConnection transactionDBComnnection,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            DbCommand command =
                transactionDBComnnection.TransactionDatabase.GetSqlStringCommand(
                    commandText);
            int retVal;

            command.Connection = transactionDBComnnection.Transaction.Connection;
            command.Transaction = transactionDBComnnection.Transaction;
            command.CommandType = CommandType.Text;
            PopulateCommandWithParameters(command, parameters);
            retVal = command.ExecuteNonQuery();
            GetReturnValueForCommandParameters(command, parameters);

            return retVal;
        }

        public static int SqlStringExecuteNonQuery(
            Database db,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            int retVal;
            DbCommand command;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                command = db.GetSqlStringCommand(commandText);
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                PopulateCommandWithParameters(command, parameters);
                retVal = command.ExecuteNonQuery();
                GetReturnValueForCommandParameters(command, parameters);
                connection.Close();
            }

            return retVal;
        }

        public static int SqlStringExecuteNonQuery(
            string connectionName,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            return SqlStringExecuteNonQuery(
                DBFromConnectionName(connectionName),
                commandText,
                parameters);
        }

        public static int SqlStringExecuteNonQuery(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            return SqlStringExecuteNonQuery(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                commandText,
                parameters);
        }

        public static DataSet SqlStringDataSet(
            Database db,
            DataSet ds,
            string commandText,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            DbCommand command;
            DbDataAdapter dataAdapter;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                command = db.GetSqlStringCommand(commandText);
                dataAdapter = db.GetDataAdapter();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                PopulateCommandWithParameters(command, parameters);
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(ds, tableName);
                GetReturnValueForCommandParameters(command, parameters);
                connection.Close();
            }

            return ds;
        }

        public static DataSet SqlStringDataSet(
            string connectionName,
            DataSet ds,
            string commandText,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            return SqlStringDataSet(
                DBFromConnectionName(connectionName),
                ds,
                commandText,
                tableName,
                parameters);
        }

        public static DataSet SqlStringDataSet(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            DataSet ds,
            string commandText,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            return SqlStringDataSet(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                ds,
                commandText,
                tableName,
                parameters);
        }

        public static DataSet SqlStringDataSet(
            string connectionName,
            string commandText,
            string dataSetName,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            DataSet ds = new DataSet(dataSetName);

            return SqlStringDataSet(
                connectionName,
                ds,
                commandText,
                tableName,
                parameters);
        }

        public static DataSet SqlStringDataSet(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText,
            string dataSetName,
            string tableName,
            params SWPGenericDbParameter[] parameters)
        {
            DataSet ds = new DataSet(dataSetName);

            return SqlStringDataSet(
                connectionTextFormatType,
                connectionNameOrText,
                ds,
                commandText,
                tableName,
                parameters);
        }

        private static DbDataReader SqlStringDataReader(
            Database db,
            string commandText,
            CommandType commandType)
        {
            DbDataReader reader = null;
            DbConnection connection = db.CreateConnection();
            DbCommand command;

            connection.Open();
            command = db.GetSqlStringCommand(commandText);
            command.Connection = connection;
            command.CommandType = commandType;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static DbDataReader SqlStringDataReader(
            string connectionName,
            string commandText,
            CommandType commandType)
        {
            return SqlStringDataReader(
                DBFromConnectionName(connectionName),
                commandText,
                commandType);
        }

        public static DbDataReader SqlStringDataReader(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText,
            CommandType commandType)
        {
            return SqlStringDataReader(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                commandText,
                commandType);
        }

        private static DbDataReader SqlStringDataReader(
            Database db,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            DbDataReader reader = null;
            DbConnection connection = db.CreateConnection();
            DbCommand command;

            connection.Open();
            command = db.GetSqlStringCommand(commandText);
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            PopulateCommandWithParameters(command, parameters);
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static DbDataReader SqlStringDataReader(
            string connectionName,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            return SqlStringDataReader(
                DBFromConnectionName(connectionName),
                commandText,
                parameters);
        }

        public static DbDataReader SqlStringDataReader(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText,
            string commandText,
            params SWPGenericDbParameter[] parameters)
        {
            return SqlStringDataReader(
                DBConnection(connectionTextFormatType, connectionNameOrText),
                commandText,
                parameters);
        }
    }
}
