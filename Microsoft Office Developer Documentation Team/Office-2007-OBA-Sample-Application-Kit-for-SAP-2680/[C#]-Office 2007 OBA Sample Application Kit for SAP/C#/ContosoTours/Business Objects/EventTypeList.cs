using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    [Serializable()]
    public class EventTypeList
    {
        #region > declarations
        private int _eventTypeID;
        private string _eventTypeName;
        #endregion

        public EventTypeList()
        { }

        #region > properties
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

        public string EventTypeName
        {
            get
            {
                return _eventTypeName;
            }
            set
            {
                _eventTypeName = value;
            }
        }
        #endregion
    }
}
