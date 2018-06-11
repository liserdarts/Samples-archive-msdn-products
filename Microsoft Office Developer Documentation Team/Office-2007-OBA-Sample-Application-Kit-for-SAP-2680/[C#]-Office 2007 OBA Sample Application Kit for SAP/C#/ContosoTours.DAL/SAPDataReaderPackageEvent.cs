using System;
using System.Data;
using System.Text;
using System.Data.Common;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public class SAPDataReaderPackageEvent : IDisposable
    {
        private DbDataReader _reader;
        private bool disposed = false;

        internal SAPDataReaderPackageEvent(DbDataReader reader)
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

        public Int32 EventID
        {
            get
            {
                return (Int32)_reader["EventID"];
            }
        }

        public Int32 VenueID
        {
            get
            {
                return (Int32)_reader["VenueID"];
            }
        }

        public string EventName
        {
            get
            {
                return (string)_reader["EventName"];
            }
        }

        public string EventDescription
        {
            get
            {
                return (string)_reader["EventDescription"];
            }
        }

        public byte [] EventPhoto
        {
            get
            {
                return (byte [])_reader["EventPhoto"];
            }
        }

        public DateTime EventDate
        {
            get
            {
                return (DateTime)_reader["EventDate"];
            }
        }

        public decimal GoldPackagePrice
        {
            get
            {
                return (decimal)_reader["GoldPackagePrice"];
            }
        }

        public decimal SilverPackagePrice
        {
            get
            {
                return (decimal)_reader["SilverPackagePrice"];
            }
        }

        public decimal BronzePackagePrice
        {
            get
            {
                return (decimal)_reader["BronzePackagePrice"];
            }
        }

        public decimal GoldPackageTrueCost
        {
            get
            {
                return (decimal)_reader["GoldPackageTrueCost"];
            }
        }

        public decimal SilverPackageTrueCost
        {
            get
            {
                return (decimal)_reader["SilverPackageTrueCost"];
            }
        }

        public decimal BronzePackageTrueCost
        {
            get
            {
                return (decimal)_reader["BronzePackageTrueCost"];
            }
        }

        public decimal EventTotalCost
        {
            get
            {
                return (decimal)_reader["EventTotalCost"];
            }
        }

        public string EventTypeName
        {
            get
            {
                return (string)_reader["EventTypeName"];
            }
        }

        public string VenueName
        {
            get
            {
                return (string)_reader["VenueName"];
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

        public Int32 PackageID
        {
            get
            {
                return (Int32)_reader["PackageID"];
            }
        }
    }
}

