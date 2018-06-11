using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageAgencyReadOnly : SWPDataReadOnlyBase
    {
        public SAPDataReaderPackageAgency ReaderSelectAll()
        {
            return new SAPDataReaderPackageAgency(
                ReaderSelect(
                SelectAllSPCommandText));
        }
    }
}

