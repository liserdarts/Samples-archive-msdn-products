using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventActorMapReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "EventActorMapDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEventActorMap SelectAll()
        {            
            return (SAPDataSetEventActorMap)Select(
                new SAPDataSetEventActorMap(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetEventActorMap ds)
        {
            Update(ds.EventActorMap);
        }

        public SAPDataSetEventActorMap UpdateAndRefresh(SAPDataSetEventActorMap ds)
        {
            Update(ds.EventActorMap);

            return SelectAll();
        }

        public SAPDataSetEventActorMap SelectByEventActorID(
            int eventActorID)
        {
            SAPDataSetEventActorMap retVal;
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);

            retVal = (SAPDataSetEventActorMap)Select(
                new SAPDataSetEventActorMap(),
                "uspEventActorMapSelectByEventActorID",
                eventActorIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventActorMap SelectByEventActorMapID(
            int eventActorMapID)
        {
            SAPDataSetEventActorMap retVal;
            SWPGenericDbParameter eventActorMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorMapID);

            retVal = (SAPDataSetEventActorMap)Select(
                new SAPDataSetEventActorMap(),
                "uspEventActorMapSelectByEventActorMapID",
                eventActorMapIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventActorMap SelectByEventID(
            int eventID)
        {
            SAPDataSetEventActorMap retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            retVal = (SAPDataSetEventActorMap)Select(
                new SAPDataSetEventActorMap(),
                "uspEventActorMapSelectByEventID",
                eventIDGenericDbParameter);
            return retVal;
        }

    }
}

