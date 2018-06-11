using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventTypeReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "EventTypeDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetEventType SelectAll()
        {            
            return (SAPDataSetEventType)Select(
                new SAPDataSetEventType(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetEventType ds)
        {
            Update(ds.EventType);
        }

        public SAPDataSetEventType UpdateAndRefresh(SAPDataSetEventType ds)
        {
            Update(ds.EventType);

            return SelectAll();
        }

        public SAPDataSetEventType SelectByEventTypeID(
            int eventTypeID)
        {
            SAPDataSetEventType retVal;
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);

            retVal = (SAPDataSetEventType)Select(
                new SAPDataSetEventType(),
                "uspEventTypeSelectByEventTypeID",
                eventTypeIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetEventType SelectByEventTypeName(
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

            SAPDataSetEventType retVal;
            SWPGenericDbParameter eventTypeNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventTypeName);

            retVal = (SAPDataSetEventType)Select(
                new SAPDataSetEventType(),
                "uspEventTypeSelectByEventTypeName",
                eventTypeNameGenericDbParameter);
            return retVal;
        }

    }
}

