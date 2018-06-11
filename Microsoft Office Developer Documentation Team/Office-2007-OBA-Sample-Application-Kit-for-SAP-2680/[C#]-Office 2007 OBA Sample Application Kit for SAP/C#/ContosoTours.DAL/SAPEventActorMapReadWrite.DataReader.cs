using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventActorMapReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderEventActorMap ReaderSelectAll()
        {
            return new SAPDataReaderEventActorMap(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderEventActorMap ReaderSelectByEventActorID(
            int eventActorID)
        {
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);

            return new SAPDataReaderEventActorMap(ReaderSelect(
                "uspEventActorMapSelectByEventActorID",
                eventActorIDGenericDbParameter));
        }

        public SAPDataReaderEventActorMap ReaderSelectByEventActorMapID(
            int eventActorMapID)
        {
            SWPGenericDbParameter eventActorMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorMapID);

            return new SAPDataReaderEventActorMap(ReaderSelect(
                "uspEventActorMapSelectByEventActorMapID",
                eventActorMapIDGenericDbParameter));
        }

        public SAPDataReaderEventActorMap ReaderSelectByEventID(
            int eventID)
        {
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            return new SAPDataReaderEventActorMap(ReaderSelect(
                "uspEventActorMapSelectByEventID",
                eventIDGenericDbParameter));
        }
    }
}

