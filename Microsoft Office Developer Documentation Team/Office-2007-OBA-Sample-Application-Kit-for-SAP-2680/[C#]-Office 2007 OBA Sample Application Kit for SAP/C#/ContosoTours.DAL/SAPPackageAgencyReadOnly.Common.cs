using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageAgencyReadOnly : SWPDataReadOnlyBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspPackageAgencySelectAll";
            }
        }

        public SAPPackageAgencyReadOnly(string dbConnectionName)
            : base(dbConnectionName)
        {
        }

        private string _tableName = "PackageAgency";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }
        
        public const string _agencyNumberColumnName = "AgencyNumber";
    }
}

