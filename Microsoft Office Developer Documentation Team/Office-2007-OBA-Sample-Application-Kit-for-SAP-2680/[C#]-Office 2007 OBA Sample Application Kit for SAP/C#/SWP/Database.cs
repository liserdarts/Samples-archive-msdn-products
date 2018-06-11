using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public class Database
    {
        private string _connectionName;

        private string _connectionText;

        private Database(
            string connectionName,
            string connectionText)
        {
            _connectionName = connectionName;
            _connectionText = connectionText;
        }

        public static Database DBFromConnectionName(string connectionName)
        {
            return new Database(connectionName, String.Empty);
        }

        public static Database DBFromConnectionText(string connectionText)
        {
            return new Database(String.Empty, connectionText);
        }

        private bool IsConnectionName
        {
            get
            {
                return (_connectionText == String.Empty);
            }
        }

        public string ConnectionText
        {
            get
            {
                if (IsConnectionName)
                {
                    SWPConnectionString connectionString = SWPConnectionString.Get(
                        SWPConnectionString.ConfigFilePath,
                        _connectionName);

                    return connectionString.ConnectionString;
                }

                else
                {
                    return _connectionText;
                }
            }
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionText);
        }

        public DbCommand GetSqlStringCommand(string commandText)
        {
            SqlCommand retVal = new SqlCommand(
                commandText, 
                new SqlConnection(ConnectionText));

            retVal.CommandType = CommandType.Text;

            return retVal;
        }

        public DbCommand GetStoredProcCommand(
                    string storedProcedureName)
        {
            SqlCommand retVal = new SqlCommand(
                storedProcedureName,
                new SqlConnection(ConnectionText));

            retVal.CommandType = CommandType.StoredProcedure;

            return retVal;
        }

        public DataSet ExecuteDataSet(DbCommand command)
        {
            // not the prettiest cast but we are getting by without enterprise
            // library
            SqlDataAdapter dataAdapter = new SqlDataAdapter((SqlCommand)command);
            DataSet retVal = new DataSet();

            dataAdapter.Fill(retVal);
            
            return retVal;
        }

        public DbDataAdapter GetDataAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
