using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCountryReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefCountry ReaderSelectAll()
        {
            return new SAPDataReaderRefCountry(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefCountry ReaderSelectByCountryCode(
            string countryCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "countryCode",
                countryCode.Length, 
                4,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter countryCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    countryCode);

            return new SAPDataReaderRefCountry(ReaderSelect(
                "uspRefCountrySelectByCountryCode",
                countryCodeGenericDbParameter));
        }
    }
}

