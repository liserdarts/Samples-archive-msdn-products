using System;

namespace SAPBridgeMaster
{
    public class Flight
    {
        private string _carrierID;
        private string _carrierName;
        private string _cityFrom;
        private string _cityTo;
        private string _airportFrom;
        private string _airportTo;
        private string _flightDate;
        private string _departureTime;
        private string _arrivalDate;
        private string _arrivalTime;
        private int _firstClass;
        private int _businessClass;
        private int _economyClass;

        public Flight()
        {
            //empty
        }

        public Flight(
            string carrierID,
            string carrierName,
            string cityFrom,
            string cityTo,
            string airportFrom,
            string airportTo,
            string flightDate,
            string departureTime,
            string arrivalDate,
            string arrivalTime,
            int firstClass,
            int businessClass,
            int economyClass)
        {
            _carrierID = carrierID;
            _carrierName = carrierName;
            _cityFrom = cityFrom;
            _cityTo = cityTo;
            _airportFrom = airportFrom;
            _airportTo = airportTo;
            _flightDate = flightDate;
            _departureTime = departureTime;
            _arrivalDate = arrivalDate;
            _arrivalTime = arrivalTime;
            _firstClass = firstClass;
            _businessClass = businessClass;
            _economyClass = economyClass;
        }

        public string CarrierID
        {
            get
            {
                return _carrierID;
            }
            set
            {
                _carrierID = value;
            }
        }
        public string CarrierName
        {
            get
            {
                return _carrierName;
            }
            set
            {
                _carrierName = value;
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
        public int FirstClass
        {
            get
            {
                return _firstClass;
            }
            set
            {
                _firstClass = value;
            }
        }

        public int BusinessClass
        {
            get
            {
                return _businessClass;
            }
            set
            {
                _businessClass = value;
            }
        }

        public int EconomyClass
        {
            get
            {
                return _economyClass;
            }
            set
            {
                _economyClass = value;
            }
        }
    }
}
