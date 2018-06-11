using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCarrierReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefCarrier ReaderSelectAll()
        {
            return new SAPDataReaderRefCarrier(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefCarrier ReaderSelectByCarrID(
            string carrID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                6,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.String,
                    ParameterDirection.Input,
                    6,
                    carrID);

            return new SAPDataReaderRefCarrier(ReaderSelect(
                "uspRefCarrierSelectByCarrID",
                carrIDGenericDbParameter));
        }
    }
}

