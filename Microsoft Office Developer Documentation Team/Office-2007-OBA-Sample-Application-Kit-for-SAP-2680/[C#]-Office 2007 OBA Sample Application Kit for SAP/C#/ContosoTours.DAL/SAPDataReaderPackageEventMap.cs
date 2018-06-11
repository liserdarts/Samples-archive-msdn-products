using System;
using System.Data;
using System.Text;
using System.Data.Common;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public class SAPDataReaderPackageEventMap : IDisposable
    {
        private DbDataReader _reader;
        private bool disposed = false;

        internal SAPDataReaderPackageEventMap(DbDataReader reader)
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

        public Int32 PackageEventMapID
        {
            get
            {
                return (Int32)_reader["PackageEventMapID"];
            }
        }

        public Int32 PackageID
        {
            get
            {
                return (Int32)_reader["PackageID"];
            }
        }

        public Int32 EventID
        {
            get
            {
                return (Int32)_reader["EventID"];
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

