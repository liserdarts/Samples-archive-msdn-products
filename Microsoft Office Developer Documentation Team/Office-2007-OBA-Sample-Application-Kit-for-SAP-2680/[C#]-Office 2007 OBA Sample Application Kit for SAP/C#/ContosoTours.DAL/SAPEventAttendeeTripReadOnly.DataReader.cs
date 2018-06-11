using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeTripReadOnly : SWPDataReadOnlyBase
    {
        public SAPDataReaderEventAttendeeTrip ReaderSelectAll()
        {
            return new SAPDataReaderEventAttendeeTrip(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEventAttendeeTrip ReaderSelectByPackageID(
            int packageID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            return new SAPDataReaderEventAttendeeTrip(ReaderSelect(
                "uspEventAttendeeTripSelectByPackageID",
                packageIDGenericDbParameter));
        }
    }
}

