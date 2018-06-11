using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderEventAttendee ReaderSelectAll()
        {
            return new SAPDataReaderEventAttendee(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEventAttendee ReaderSelectByEventAttendeeID(
            int eventAttendeeID)
        {
            SWPGenericDbParameter eventAttendeeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventAttendeeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventAttendeeID);

            return new SAPDataReaderEventAttendee(ReaderSelect(
                "uspEventAttendeeSelectByEventAttendeeID",
                eventAttendeeIDGenericDbParameter));
        }

        public SAPDataReaderEventAttendee ReaderSelectByPackageID(
            int packageID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            return new SAPDataReaderEventAttendee(ReaderSelect(
                "uspEventAttendeeSelectByPackageID",
                packageIDGenericDbParameter));
        }

        public SAPDataReaderEventAttendee ReaderSelectByPackageEventMapID(
            int packageEventMapID)
        {
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageEventMapID);

            return new SAPDataReaderEventAttendee(ReaderSelect(
                "uspEventAttendeeSelectByPackageEventMapID",
                packageEventMapIDGenericDbParameter));
        }
    }
}

