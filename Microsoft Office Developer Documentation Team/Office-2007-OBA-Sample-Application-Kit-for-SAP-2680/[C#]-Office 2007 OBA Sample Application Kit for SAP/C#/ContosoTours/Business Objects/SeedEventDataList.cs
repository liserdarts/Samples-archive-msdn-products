using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    public class SeedEventDataList
    {
        private int _eventID;
        private string _venueCity;
        private DateTime _eventDate;
        private string _flightConnNo;
        private string _agencyNum;
        private string _flightDate;
        private string _classType;
        private string _tripNumber;
        private string _cityFrom;

        internal SeedEventDataList()
        {
 
        }
        
        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        public string VenueCity
        {
            get { return _venueCity; }
            set { _venueCity = value; }
        }
        
        public DateTime EventDate
        {
            get { return _eventDate; }
            set { _eventDate = value; }
        }
        
        public string CityFrom
        {
            get { return _cityFrom; }
            set { _cityFrom = value; }
        }
        
        public string TripNumber
        {
            get { return _tripNumber; }
            set { _tripNumber = value; }
        }
        
        public string ClassType
        {
            get { return _classType; }
            set { _classType = value; }
        }
        
        public string FlightDate
        {
            get { return _flightDate; }
            set { _flightDate = value; }
        }
        
        public string FlightConnNo
        {
            get { return _flightConnNo; }
            set { _flightConnNo = value; }
        }
        
        public string AgencyNum
        {
            get { return _agencyNum; }
            set { _agencyNum = value; }
        }
    }
}
