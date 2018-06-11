using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeAgencyMapReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "EventAttendeeAgencyMapDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEventAttendeeAgencyMap SelectAll()
        {            
            return (SAPDataSetEventAttendeeAgencyMap)Select(
                new SAPDataSetEventAttendeeAgencyMap(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetEventAttendeeAgencyMap ds)
        {
            Update(ds.EventAttendeeAgencyMap);
        }

        public SAPDataSetEventAttendeeAgencyMap UpdateAndRefresh(SAPDataSetEventAttendeeAgencyMap ds)
        {
            Update(ds.EventAttendeeAgencyMap);

            return SelectAll();
        }

        public SAPDataSetEventAttendeeAgencyMap SelectByEventAttendeeAgencyMapID(
            int eventAttendeeAgencyMapID)
        {
            SAPDataSetEventAttendeeAgencyMap retVal;
            SWPGenericDbParameter eventAttendeeAgencyMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeAgencyMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeAgencyMapID);

            retVal = (SAPDataSetEventAttendeeAgencyMap)Select(
                new SAPDataSetEventAttendeeAgencyMap(),
                "uspEventAttendeeAgencyMapSelectByEventAttendeeAgencyMapID",
                eventAttendeeAgencyMapIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventAttendeeAgencyMap SelectByEventAttendeeID(
            int eventAttendeeID)
        {
            SAPDataSetEventAttendeeAgencyMap retVal;
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);

            retVal = (SAPDataSetEventAttendeeAgencyMap)Select(
                new SAPDataSetEventAttendeeAgencyMap(),
                "uspEventAttendeeAgencyMapSelectByEventAttendeeID",
                eventAttendeeIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventAttendeeAgencyMap SelectByAgencyNumber(
            string agencyNumber)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SAPDataSetEventAttendeeAgencyMap retVal;
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);

            retVal = (SAPDataSetEventAttendeeAgencyMap)Select(
                new SAPDataSetEventAttendeeAgencyMap(),
                "uspEventAttendeeAgencyMapSelectByAgencyNumber",
                agencyNumberGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventAttendeeAgencyMap SelectByPackageID(
            int packageID)
        {
            SAPDataSetEventAttendeeAgencyMap retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            retVal = (SAPDataSetEventAttendeeAgencyMap)Select(
                new SAPDataSetEventAttendeeAgencyMap(),
                "uspEventAttendeeAgencyMapSelectByPackageID",
                packageIDGenericDbParameter);
            return retVal;
        }

    }
}

