using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "EventDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEvent SelectAll()
        {            
            return (SAPDataSetEvent)Select(
                new SAPDataSetEvent(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetEvent ds)
        {
            Update(ds.Event);
        }

        public SAPDataSetEvent UpdateAndRefresh(SAPDataSetEvent ds)
        {
            Update(ds.Event);

            return SelectAll();
        }

        public SAPDataSetEvent SelectByEventID(
            int eventID)
        {
            SAPDataSetEvent retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            retVal = (SAPDataSetEvent)Select(
                new SAPDataSetEvent(),
                "uspEventSelectByEventID",
                eventIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEvent SelectByEventTypeID(
            int eventTypeID)
        {
            SAPDataSetEvent retVal;
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);

            retVal = (SAPDataSetEvent)Select(
                new SAPDataSetEvent(),
                "uspEventSelectByEventTypeID",
                eventTypeIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEvent SelectByVenueID(
            int venueID)
        {
            SAPDataSetEvent retVal;
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);

            retVal = (SAPDataSetEvent)Select(
                new SAPDataSetEvent(),
                "uspEventSelectByVenueID",
                venueIDGenericDbParameter);
            return retVal;
        }

    }
}

