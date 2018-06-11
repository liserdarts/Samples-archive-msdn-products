using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderPackage ReaderSelectAll()
        {
            return new SAPDataReaderPackage(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderPackage ReaderSelectByPackageID(
            int packageID)
        {
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            return new SAPDataReaderPackage(ReaderSelect(
                "uspPackageSelectByPackageID",
                packageIDGenericDbParameter));
        }

        public SAPDataReaderPackage ReaderSelectByPackageName(
            string packageName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "packageName",
                packageName.Length, 
                255,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter packageNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    packageName);

            return new SAPDataReaderPackage(ReaderSelect(
                "uspPackageSelectByPackageName",
                packageNameGenericDbParameter));
        }
    }
}

