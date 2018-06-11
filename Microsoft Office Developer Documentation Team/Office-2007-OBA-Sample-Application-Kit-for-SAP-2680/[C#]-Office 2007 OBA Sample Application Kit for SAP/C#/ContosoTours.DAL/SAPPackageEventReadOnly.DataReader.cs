using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageEventReadOnly : SWPDataReadOnlyBase
    {
        public SAPDataReaderPackageEvent ReaderSelectAll()
        {
            return new SAPDataReaderPackageEvent(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderPackageEvent ReaderSelectByPackageID(
            int packageID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            return new SAPDataReaderPackageEvent(ReaderSelect(
                "uspPackageEventSelectByPackageID",
                packageIDGenericDbParameter));
        }
    }
}

