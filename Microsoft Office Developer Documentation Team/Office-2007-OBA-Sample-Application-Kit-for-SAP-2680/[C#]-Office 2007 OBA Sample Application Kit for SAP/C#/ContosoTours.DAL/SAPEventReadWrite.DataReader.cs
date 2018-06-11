using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderEvent ReaderSelectAll()
        {
            return new SAPDataReaderEvent(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEvent ReaderSelectByEventID(
            int eventID)
        {
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            return new SAPDataReaderEvent(ReaderSelect(
                "uspEventSelectByEventID",
                eventIDGenericDbParameter));
        }

        public SAPDataReaderEvent ReaderSelectByEventTypeID(
            int eventTypeID)
        {
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);

            return new SAPDataReaderEvent(ReaderSelect(
                "uspEventSelectByEventTypeID",
                eventTypeIDGenericDbParameter));
        }

        public SAPDataReaderEvent ReaderSelectByVenueID(
            int venueID)
        {
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);

            return new SAPDataReaderEvent(ReaderSelect(
                "uspEventSelectByVenueID",
                venueIDGenericDbParameter));
        }
    }
}

