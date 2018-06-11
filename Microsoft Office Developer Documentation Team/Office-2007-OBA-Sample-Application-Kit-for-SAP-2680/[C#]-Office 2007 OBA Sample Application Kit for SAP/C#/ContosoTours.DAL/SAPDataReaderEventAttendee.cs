using System;
using System.Data;
using System.Text;
using System.Data.Common;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public class SAPDataReaderEventAttendee : IDisposable
    {
        private DbDataReader _reader;
        private bool disposed = false;

        internal SAPDataReaderEventAttendee(DbDataReader reader)
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

        public Int32 EventAttendeeID
        {
            get
            {
                return (Int32)_reader["EventAttendeeID"];
            }
        }

        public Int32 PackageID
        {
            get
            {
                return (Int32)_reader["PackageID"];
            }
        }

        public string CustomerNumber
        {
            get
            {
                return (string)_reader["CustomerNumber"];
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return (DateTime)_reader["DateOfBirth"];
            }
        }

        public DateTime Created
        {
            get
            {
                return (DateTime)_reader["Created"];
            }
        }

        public string PackageName
        {
            get
            {
                return (string)_reader["PackageName"];
            }
        }
    }
}

