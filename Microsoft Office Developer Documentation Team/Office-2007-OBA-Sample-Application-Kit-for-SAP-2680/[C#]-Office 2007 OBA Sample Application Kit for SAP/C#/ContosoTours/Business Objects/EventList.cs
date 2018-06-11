using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    [Serializable()]
    public class EventList
    {
        #region > declarations
        private int _eventID;
        private string _eventName;
        private string _eventDescription;
        private byte[] _eventPhoto;
        private bool _eventEditTag;
        private bool _eventDeleteTag;
        #endregion

        public EventList()
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

        public string EventDescription
        {
            get
            {
                return _eventDescription;
            }
            set
            {
                _eventDescription = value;
            }
        }

        public byte[] EventPhoto
        {
            get
            {
                return _eventPhoto;
            }
            set
            {
                _eventPhoto = value;
            }
        }

        public bool EventEditTag
        {
            
            get 
            { 
                return _eventEditTag; 
            }
            set 
            {
                _eventEditTag = value; 
            }
        }

        public bool EventDeleteTag
        {

            get
            {
                return _eventDeleteTag;
            }
            set
            {
                _eventDeleteTag = value;
            }
        }
        #endregion
    }
}
