using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPVenueReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "VenueDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetVenue SelectAll()
        {            
            return (SAPDataSetVenue)Select(
                new SAPDataSetVenue(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetVenue ds)
        {
            Update(ds.Venue);
        }

        public SAPDataSetVenue UpdateAndRefresh(SAPDataSetVenue ds)
        {
            Update(ds.Venue);

            return SelectAll();
        }

        public SAPDataSetVenue SelectByVenueID(
            int venueID)
        {
            SAPDataSetVenue retVal;
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);

            retVal = (SAPDataSetVenue)Select(
                new SAPDataSetVenue(),
                "uspVenueSelectByVenueID",
                venueIDGenericDbParameter);
            return retVal;
        }

    }
}

