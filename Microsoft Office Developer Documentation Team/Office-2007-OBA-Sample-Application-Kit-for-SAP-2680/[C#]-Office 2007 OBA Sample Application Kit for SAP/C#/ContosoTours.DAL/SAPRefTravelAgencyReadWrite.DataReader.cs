using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefTravelAgencyReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefTravelAgency ReaderSelectAll()
        {
            return new SAPDataReaderRefTravelAgency(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefTravelAgency ReaderSelectByAgencyNumber(
            string agencyNumber)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);

            return new SAPDataReaderRefTravelAgency(ReaderSelect(
                "uspRefTravelAgencySelectByAgencyNumber",
                agencyNumberGenericDbParameter));
        }
    }
}

