using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventActorReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "EventActorDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEventActor SelectAll()
        {            
            return (SAPDataSetEventActor)Select(
                new SAPDataSetEventActor(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetEventActor ds)
        {
            Update(ds.EventActor);
        }

        public SAPDataSetEventActor UpdateAndRefresh(SAPDataSetEventActor ds)
        {
            Update(ds.EventActor);

            return SelectAll();
        }

        public SAPDataSetEventActor SelectByEventActorID(
            int eventActorID)
        {
            SAPDataSetEventActor retVal;
            SWPGenericDbParameter eventActorIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventActorID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventActorID);

            retVal = (SAPDataSetEventActor)Select(
                new SAPDataSetEventActor(),
                "uspEventActorSelectByEventActorID",
                eventActorIDGenericDbParameter);
            return retVal;
        }

    }
}

