using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventActorReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderEventActor ReaderSelectAll()
        {
            return new SAPDataReaderEventActor(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEventActor ReaderSelectByEventActorID(
            int eventActorID)
        {
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);

            return new SAPDataReaderEventActor(ReaderSelect(
                "uspEventActorSelectByEventActorID",
                eventActorIDGenericDbParameter));
        }
    }
}

