using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPActorEventReadOnly : SWPDataReadOnlyBase
    {
        private string _dataSetName = "ActorEventDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetActorEvent SelectAll()
        {
            return (SAPDataSetActorEvent)Select(
                new SAPDataSetActorEvent(),
                SelectAllSPCommandText);
        }

        public SAPDataSetActorEvent SelectByEventID(
            int eventID)
        {
            SAPDataSetActorEvent retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            retVal = (SAPDataSetActorEvent)Select(
                new SAPDataSetActorEvent(),
                "uspActorEventSelectByEventID",
                eventIDGenericDbParameter);
            return retVal;
        }
    }
}

