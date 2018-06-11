using System;
using System.Data;
using System.Text;
using System.Data.Common;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public class SAPDataReaderActorEvent : IDisposable
    {
        private DbDataReader _reader;
        private bool disposed = false;

        internal SAPDataReaderActorEvent(DbDataReader reader)
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

        public Int32 EventActorID
        {
            get
            {
                return (Int32)_reader["EventActorID"];
            }
        }

        public string EventActorName
        {
            get
            {
                return (string)_reader["EventActorName"];
            }
        }

        public Int32 EventID
        {
            get
            {
                return (Int32)_reader["EventID"];
            }
        }

        public Int32 EventActorMapID
        {
            get
            {
                return (Int32)_reader["EventActorMapID"];
            }
        }
    }
}

