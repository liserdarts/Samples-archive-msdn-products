using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventTypeReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderEventType ReaderSelectAll()
        {
            return new SAPDataReaderEventType(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEventType ReaderSelectByEventTypeID(
            int eventTypeID)
        {
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);

            return new SAPDataReaderEventType(ReaderSelect(
                "uspEventTypeSelectByEventTypeID",
                eventTypeIDGenericDbParameter));
        }

        public SAPDataReaderEventType ReaderSelectByEventTypeName(
            string eventTypeName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventTypeName",
                eventTypeName.Length, 
                255,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SWPGenericDbParameter eventTypeNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventTypeName);

            return new SAPDataReaderEventType(ReaderSelect(
                "uspEventTypeSelectByEventTypeName",
                eventTypeNameGenericDbParameter));
        }
    }
}

