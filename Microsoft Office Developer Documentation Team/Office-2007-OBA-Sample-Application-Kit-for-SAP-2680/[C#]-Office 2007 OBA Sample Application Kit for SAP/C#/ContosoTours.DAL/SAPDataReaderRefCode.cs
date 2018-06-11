using System;
using System.Data;
using System.Text;
using System.Data.Common;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public class SAPDataReaderRefCode : IDisposable
    {
        private DbDataReader _reader;
        private bool disposed = false;

        internal SAPDataReaderRefCode(DbDataReader reader)
        {
            _reader = reader;
        }

        public DbDataReader DataReader
        {
            get
            {
                return _reader;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_reader != null)
                    {
                        _reader.Close();
                        _reader.Dispose();
                        _reader = null;
                    }
                }
                disposed = true;
            }
        }

        public void Close()
        {
            Dispose();
        }

        public string RefTypeCode
        {
            get
            {
                return (string)_reader["RefTypeCode"];
            }
        }

        public string TypeCode
        {
            get
            {
                return (string)_reader["TypeCode"];
            }
        }

        public string TypeName
        {
            get
            {
                return (string)_reader["TypeName"];
            }
        }
    }
}

