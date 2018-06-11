using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefLanguageReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefLanguage ReaderSelectAll()
        {
            return new SAPDataReaderRefLanguage(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefLanguage ReaderSelectByLanguageCode(
            string languageCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "languageCode",
                languageCode.Length, 
                4,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter languageCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@LanguageCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    languageCode);

            return new SAPDataReaderRefLanguage(ReaderSelect(
                "uspRefLanguageSelectByLanguageCode",
                languageCodeGenericDbParameter));
        }
    }
}

