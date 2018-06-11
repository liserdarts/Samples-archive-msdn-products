using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeTripReadOnly : SWPDataReadOnlyBase
    {
        private string _dataSetName = "EventAttendeeTripDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEventAttendeeTrip SelectAll()
        {
            return (SAPDataSetEventAttendeeTrip)Select(
                new SAPDataSetEventAttendeeTrip(),
                SelectAllSPCommandText);
        }

        public SAPDataSetEventAttendeeTrip SelectByPackageID(
            int packageID)
        {
            SAPDataSetEventAttendeeTrip retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            retVal = (SAPDataSetEventAttendeeTrip)Select(
                new SAPDataSetEventAttendeeTrip(),
                "uspEventAttendeeTripSelectByPackageID",
                packageIDGenericDbParameter);
            return retVal;
        }
    }
}

