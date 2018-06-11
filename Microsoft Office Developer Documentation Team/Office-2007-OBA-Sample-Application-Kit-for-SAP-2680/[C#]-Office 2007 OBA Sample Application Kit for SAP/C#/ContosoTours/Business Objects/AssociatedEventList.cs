using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    public class AssociatedEventList
    {
        #region > declarations
        private int _eventID;
        private string _eventName;
        private DateTime _eventDate;
        private string _eventVenue;
        private decimal _eventGoldPrice;
        private decimal _eventSilverPrice;
        private decimal _eventBronzePrice;
        private string _packageType;
        private int _packageMapID;
        private bool _isNewID;
        private bool _isUpdated;
        private int _eventTypeID;
        #endregion

        internal AssociatedEventList()
        { }

        #region > properties
        public int EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public string EventName
        {
            get
            {
                return _eventName;
            }
            set
            {
                _eventName = value;
            }
        }

        public DateTime EventDate
        {
            get
            {
                return _eventDate;
            }
            set
            {
                _eventDate = value;
            }
        }

        public string EventVenue
        {
            get
            {
                return _eventVenue;
            }
            set
            {
                _eventVenue = value;
            }
        }

        public decimal EventGoldPrice
        {
            get
            {
                return _eventGoldPrice;
            }

            set
            {
                _eventGoldPrice = value;
            }
        }

        public decimal EventSilverPrice
        {
            get
            {
                return _eventSilverPrice;
            }
            set
            {
                _eventSilverPrice = value;
            }
        }

        public decimal EventBronzePrice
        {
            get
            {
                return _eventBronzePrice;
            }
            set
            {
                _eventBronzePrice = value;
            }
        }

        public string PackageType
        {
            get
            {
                return _packageType;
            }
            set
            {
                _packageType = value;
            }
        }

        public bool IsNewID
        {
            get
            {
                return _isNewID;
            }
            set
            {
                _isNewID = value;
            }
        }

        public bool IsUpdated
        {
            get
            {
                return _isUpdated;
            }
            set
            {
                _isUpdated = value;
            }
        }

        public int PackageMapID
        {
            get
            {
                return _packageMapID;
            }
            set
            {
                _packageMapID = value;
            }
        }

        public int EventTypeID
        {
            get
            {
                return _eventTypeID;
            }
            set
            {
                _eventTypeID = value;
            }
        }
        #endregion
    }
}
