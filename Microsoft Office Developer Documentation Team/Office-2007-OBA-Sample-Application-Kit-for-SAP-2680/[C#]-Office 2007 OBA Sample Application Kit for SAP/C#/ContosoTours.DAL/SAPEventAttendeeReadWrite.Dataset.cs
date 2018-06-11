using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "EventAttendeeDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEventAttendee SelectAll()
        {            
            return (SAPDataSetEventAttendee)Select(
                new SAPDataSetEventAttendee(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetEventAttendee ds)
        {
            Update(ds.EventAttendee);
        }

        public SAPDataSetEventAttendee UpdateAndRefresh(SAPDataSetEventAttendee ds)
        {
            Update(ds.EventAttendee);

            return SelectAll();
        }

        public SAPDataSetEventAttendee SelectByEventAttendeeID(
            int eventAttendeeID)
        {
            SAPDataSetEventAttendee retVal;
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);

            retVal = (SAPDataSetEventAttendee)Select(
                new SAPDataSetEventAttendee(),
                "uspEventAttendeeSelectByEventAttendeeID",
                eventAttendeeIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventAttendee SelectByPackageID(
            int packageID)
        {
            SAPDataSetEventAttendee retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            retVal = (SAPDataSetEventAttendee)Select(
                new SAPDataSetEventAttendee(),
                "uspEventAttendeeSelectByPackageID",
                packageIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventAttendee SelectByPackageEventMapID(
            int packageEventMapID)
        {
            SAPDataSetEventAttendee retVal;
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageEventMapID);

            retVal = (SAPDataSetEventAttendee)Select(
                new SAPDataSetEventAttendee(),
                "uspEventAttendeeSelectByPackageEventMapID",
                packageEventMapIDGenericDbParameter);
            return retVal;
        }

    }
}

