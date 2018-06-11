using System;
using System.Data.Common;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public class SWPTransactionDBConnection : ISWPTransactionDBConnection, IDisposable
    {        
        private Database _database = null;

        private DbTransaction _transaction = null;

        private DbConnection _connection = null;

        public SWPTransactionDBConnection(string connectionName)
        {
            _database = SWPDBHelper.DBFromConnectionName(connectionName);
            _connection = _database.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public SWPTransactionDBConnection(
            SWPConnectionTextFormatType connectionTextFormatType,
            string connectionNameOrText)

        {
            _database = SWPDBHelper.DBConnection(
                            connectionTextFormatType, 
                            connectionNameOrText);
            _connection = _database.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        ~SWPTransactionDBConnection()
        {
            Dispose(false);
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException(
                    "Transaction was disposed of (previously set to null)");
            }

            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException(
                    "Transaction was disposed of (previously set to null)");
            }

            _transaction.Rollback();
            Dispose();
        }

        #region SWPTransactionDBConnection Members

        public DbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public Database TransactionDatabase
        {
            get
            {
                return _database;
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return ; // let GC clean up objects if Finalizer
            }

            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            if (_database != null)
            {
                _database = null;
            }
        }
    }
}
