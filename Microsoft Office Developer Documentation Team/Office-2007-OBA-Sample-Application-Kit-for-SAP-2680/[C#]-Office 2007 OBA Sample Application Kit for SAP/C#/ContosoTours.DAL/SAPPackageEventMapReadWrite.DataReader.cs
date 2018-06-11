using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageEventMapReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderPackageEventMap ReaderSelectAll()
        {
            return new SAPDataReaderPackageEventMap(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderPackageEventMap ReaderSelectByEventID(
            int eventID)
        {
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            return new SAPDataReaderPackageEventMap(ReaderSelect(
                "uspPackageEventMapSelectByEventID",
                eventIDGenericDbParameter));
        }

        public SAPDataReaderPackageEventMap ReaderSelectByPackageEventMapID(
            int packageEventMapID)
        {
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageEventMapID);

            return new SAPDataReaderPackageEventMap(ReaderSelect(
                "uspPackageEventMapSelectByPackageEventMapID",
                packageEventMapIDGenericDbParameter));
        }

        public SAPDataReaderPackageEventMap ReaderSelectByPackageID(
            int packageID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            return new SAPDataReaderPackageEventMap(ReaderSelect(
                "uspPackageEventMapSelectByPackageID",
                packageIDGenericDbParameter));
        }

        public SAPDataReaderPackageEventMap ReaderSelectByPackageIDEventID(
            int packageID,
            int eventID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            return new SAPDataReaderPackageEventMap(ReaderSelect(
                "uspPackageEventMapSelectByPackageIDEventID",
                packageIDGenericDbParameter,
                eventIDGenericDbParameter));
        }
    }
}

