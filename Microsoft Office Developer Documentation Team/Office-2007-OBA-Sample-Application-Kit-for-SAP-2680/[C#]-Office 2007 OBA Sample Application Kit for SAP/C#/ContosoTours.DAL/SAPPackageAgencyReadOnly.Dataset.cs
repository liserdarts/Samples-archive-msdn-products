using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageAgencyReadOnly : SWPDataReadOnlyBase
    {
        private string _dataSetName = "PackageAgencyDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetPackageAgency SelectAll()
        {
            return (SAPDataSetPackageAgency)Select(
                new SAPDataSetPackageAgency(),
                SelectAllSPCommandText);
        }
    }
}

