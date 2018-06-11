using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCounterReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefCounter ReaderSelectAll()
        {
            return new SAPDataReaderRefCounter(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefCounter ReaderSelectByCarrID(
            string carrID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                3,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    carrID);

            return new SAPDataReaderRefCounter(ReaderSelect(
                "uspRefCounterSelectByCarrID",
                carrIDGenericDbParameter));
        }
    }
}

