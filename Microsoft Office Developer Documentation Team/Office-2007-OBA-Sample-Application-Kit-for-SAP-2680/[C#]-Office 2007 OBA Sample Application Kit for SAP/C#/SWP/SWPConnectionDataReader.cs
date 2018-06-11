using System;
using System.Data.Common;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public class SWPConnectionDataReader : IDisposable
    {
        private DbConnection _connection;

        private DbDataReader _reader;

        public SWPConnectionDataReader(
            DbConnection connection,
            DbDataReader reader)
        {
            _connection = connection;
            _reader = reader;
        }

        public DbDataReader DataReader
        {
            get
            {
                return _reader;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
                _reader = null;
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        public void Close()
        {
            Dispose();
        }

        ~SWPConnectionDataReader()
        {
            Dispose();
        }
    }
}
