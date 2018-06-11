using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPActorEventReadOnly : SWPDataReadOnlyBase
    {
        public SAPDataReaderActorEvent ReaderSelectAll()
        {
            return new SAPDataReaderActorEvent(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderActorEvent ReaderSelectByEventID(
            int eventID)
        {
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            return new SAPDataReaderActorEvent(ReaderSelect(
                "uspActorEventSelectByEventID",
                eventIDGenericDbParameter));
        }
    }
}

