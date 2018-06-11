using System;
using System.Data;
using System.Text;
using System.Data.Common;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public class SAPDataReaderVenue : IDisposable
    {
        private DbDataReader _reader;
        private bool disposed = false;

        internal SAPDataReaderVenue(DbDataReader reader)
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

        public Int32 VenueID
        {
            get
            {
                return (Int32)_reader["VenueID"];
            }
        }

        public string VenueName
        {
            get
            {
                return (string)_reader["VenueName"];
            }
        }

        public string VenueDescription
        {
            get
            {
                return (string)_reader["VenueDescription"];
            }
        }

        public string VenueStreet
        {
            get
            {
                return (string)_reader["VenueStreet"];
            }
        }

        public string VenueCity
        {
            get
            {
                return (string)_reader["VenueCity"];
            }
        }

        public string VenueState
        {
            get
            {
                return (string)_reader["VenueState"];
            }
        }

        public string VenuePostalCode
        {
            get
            {
                return (string)_reader["VenuePostalCode"];
            }
        }

        public byte [] VenueGeographicMap
        {
            get
            {
                return (byte [])_reader["VenueGeographicMap"];
            }
        }

        public byte [] VenueFacilityMap
        {
            get
            {
                return (byte [])_reader["VenueFacilityMap"];
            }
        }

        public byte [] VenueImage
        {
            get
            {
                return (byte [])_reader["VenueImage"];
            }
        }
    }
}

