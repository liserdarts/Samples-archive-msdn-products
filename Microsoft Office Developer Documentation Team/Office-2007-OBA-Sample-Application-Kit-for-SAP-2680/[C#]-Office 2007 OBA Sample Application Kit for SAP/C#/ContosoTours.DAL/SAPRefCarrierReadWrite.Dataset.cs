using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCarrierReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefCarrierDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefCarrier SelectAll()
        {            
            return (SAPDataSetRefCarrier)Select(
                new SAPDataSetRefCarrier(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefCarrier ds)
        {
            Update(ds.RefCarrier);
        }

        public SAPDataSetRefCarrier UpdateAndRefresh(SAPDataSetRefCarrier ds)
        {
            Update(ds.RefCarrier);

            return SelectAll();
        }

        public SAPDataSetRefCarrier SelectByCarrID(
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

            SAPDataSetRefCarrier retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.String,
                    ParameterDirection.Input,
                    6,
                    carrID);

            retVal = (SAPDataSetRefCarrier)Select(
                new SAPDataSetRefCarrier(),
                "uspRefCarrierSelectByCarrID",
                carrIDGenericDbParameter);
            return retVal;
        }

    }
}

