using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    [Serializable()]
    public class FlightConnectionList
    {
        #region > declarations
        private string _agencyNumber;
        private string _flightConnectionNumber;
        private string _numberHops;
        private string _flightDate;
        private int _flightTime;
        private string _departureTime;
        private string _airportFrom;
        private string _cityFrom;
        private string _arrivalDate;
        private string _arrivalTime;
        private string _airportTo;
        private string _cityTo;
        private int _eventID;
        private string _tripNumber;

        #endregion

        public FlightConnectionList()
        { }

        #region > properties

        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        public string AgencyNumber
        {
            get
            {
                return _agencyNumber;
            }
            set
            {
                _agencyNumber = value;
            }
        }

        public string FlightConnectionNumber
        {
            get
            {
                return _flightConnectionNumber;
            }
            set
            {
                _flightConnectionNumber = value;
            }
        }

        public string NumberHops
        {
            get 
            { 
                return _numberHops; 
            }
            set 
            { 
                _numberHops = value; 
            }
        }

        public string FlightDate
        {
            get 
            { 
                return _flightDate; 
            }
            set 
            { 
                _flightDate = value; 
            }
        }

        public int FlightTime
        {
            get 
            { 
                return _flightTime; 
            }
            set 
            { 
                _flightTime = value; 
            }
        }

        public string DepartureTime
        {
            get 
            {
                return _departureTime;
            }
            set
            {
                _departureTime = value;
            }
        }

        public string AirportFrom
        {
            get 
            { 
                return _airportFrom; 
            }
            set 
            { 
                _airportFrom = value; 
            }
        }

        public string CityFrom
        {
            get 
            { 
                return _cityFrom; 
            }
            set 
            { 
                _cityFrom = value; 
            }
        }

        public string ArrivalDate
        {
            get 
            { 
                return _arrivalDate; 
            }
            set 
            { 
                _arrivalDate = value; 
            }
        }

        public string ArrivalTime
        {
            get 
            { 
                return _arrivalTime; 
            }
            set 
            { 
                _arrivalTime = value; 
            }
        }

        public string AirportTo
        {
            get 
            { 
                return _airportTo; 
            }
            set 
            { 
                _airportTo = value; 
            }
        }

        public string CityTo
        {
            get 
            { 
                return _cityTo; 
            }
            set 
            { 
                _cityTo = value; 
            }
        }

        public string TripNumber
        {
            get
            {
                return _tripNumber;
            }
            set
            {
                _tripNumber = value;
            }
        }

        #endregion
    }
}
