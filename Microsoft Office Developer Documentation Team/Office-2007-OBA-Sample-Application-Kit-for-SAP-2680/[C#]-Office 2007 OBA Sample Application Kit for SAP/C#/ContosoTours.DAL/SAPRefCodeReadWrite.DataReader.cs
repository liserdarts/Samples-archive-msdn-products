using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCodeReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefCode ReaderSelectAll()
        {
            return new SAPDataReaderRefCode(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefCode ReaderSelectByRefTypeCode(
            string refTypeCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "refTypeCode",
                refTypeCode.Length, 
                2,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter refTypeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@RefTypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    2,
                    refTypeCode);

            return new SAPDataReaderRefCode(ReaderSelect(
                "uspRefCodeSelectByRefTypeCode",
                refTypeCodeGenericDbParameter));
        }
    }
}

