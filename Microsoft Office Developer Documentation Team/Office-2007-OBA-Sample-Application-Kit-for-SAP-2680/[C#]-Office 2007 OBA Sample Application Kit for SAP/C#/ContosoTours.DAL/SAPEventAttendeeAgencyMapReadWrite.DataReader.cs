using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeAgencyMapReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderEventAttendeeAgencyMap ReaderSelectAll()
        {
            return new SAPDataReaderEventAttendeeAgencyMap(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEventAttendeeAgencyMap ReaderSelectByEventAttendeeAgencyMapID(
            int eventAttendeeAgencyMapID)
        {
            SWPGenericDbParameter eventAttendeeAgencyMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeAgencyMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeAgencyMapID);

            return new SAPDataReaderEventAttendeeAgencyMap(ReaderSelect(
                "uspEventAttendeeAgencyMapSelectByEventAttendeeAgencyMapID",
                eventAttendeeAgencyMapIDGenericDbParameter));
        }

        public SAPDataReaderEventAttendeeAgencyMap ReaderSelectByEventAttendeeID(
            int eventAttendeeID)
        {
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);

            return new SAPDataReaderEventAttendeeAgencyMap(ReaderSelect(
                "uspEventAttendeeAgencyMapSelectByEventAttendeeID",
                eventAttendeeIDGenericDbParameter));
        }

        public SAPDataReaderEventAttendeeAgencyMap ReaderSelectByAgencyNumber(
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

            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);

            return new SAPDataReaderEventAttendeeAgencyMap(ReaderSelect(
                "uspEventAttendeeAgencyMapSelectByAgencyNumber",
                agencyNumberGenericDbParameter));
        }

        public SAPDataReaderEventAttendeeAgencyMap ReaderSelectByPackageID(
            int packageID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            return new SAPDataReaderEventAttendeeAgencyMap(ReaderSelect(
                "uspEventAttendeeAgencyMapSelectByPackageID",
                packageIDGenericDbParameter));
        }
    }
}

