using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "PackageDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetPackage SelectAll()
        {            
            return (SAPDataSetPackage)Select(
                new SAPDataSetPackage(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetPackage ds)
        {
            Update(ds.Package);
        }

        public SAPDataSetPackage UpdateAndRefresh(SAPDataSetPackage ds)
        {
            Update(ds.Package);

            return SelectAll();
        }

        public SAPDataSetPackage SelectByPackageID(
            int packageID)
        {
            SAPDataSetPackage retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            retVal = (SAPDataSetPackage)Select(
                new SAPDataSetPackage(),
                "uspPackageSelectByPackageID",
                packageIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetPackage SelectByPackageName(
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

            SAPDataSetPackage retVal;
            SWPGenericDbParameter packageNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    packageName);

            retVal = (SAPDataSetPackage)Select(
                new SAPDataSetPackage(),
                "uspPackageSelectByPackageName",
                packageNameGenericDbParameter);
            return retVal;
        }

    }
}

