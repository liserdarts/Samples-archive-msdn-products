using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageEventReadOnly : SWPDataReadOnlyBase
    {
        private string _dataSetName = "PackageEventDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetPackageEvent SelectAll()
        {
            return (SAPDataSetPackageEvent)Select(
                new SAPDataSetPackageEvent(),
                SelectAllSPCommandText);
        }

        public SAPDataSetPackageEvent SelectByPackageID(
            int packageID)
        {
            SAPDataSetPackageEvent retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            retVal = (SAPDataSetPackageEvent)Select(
                new SAPDataSetPackageEvent(),
                "uspPackageEventSelectByPackageID",
                packageIDGenericDbParameter);
            return retVal;
        }
    }
}

