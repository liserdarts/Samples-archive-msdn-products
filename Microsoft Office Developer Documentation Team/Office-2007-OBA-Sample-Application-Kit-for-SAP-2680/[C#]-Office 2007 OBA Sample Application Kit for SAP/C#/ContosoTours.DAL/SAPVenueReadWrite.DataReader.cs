using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPVenueReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderVenue ReaderSelectAll()
        {
            return new SAPDataReaderVenue(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderVenue ReaderSelectByVenueID(
            int venueID)
        {
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);

            return new SAPDataReaderVenue(ReaderSelect(
                "uspVenueSelectByVenueID",
                venueIDGenericDbParameter));
        }
    }
}

