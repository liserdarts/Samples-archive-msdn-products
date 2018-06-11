using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCounterReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefCounterDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefCounter SelectAll()
        {            
            return (SAPDataSetRefCounter)Select(
                new SAPDataSetRefCounter(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefCounter ds)
        {
            Update(ds.RefCounter);
        }

        public SAPDataSetRefCounter UpdateAndRefresh(SAPDataSetRefCounter ds)
        {
            Update(ds.RefCounter);

            return SelectAll();
        }

        public SAPDataSetRefCounter SelectByCarrID(
            string carrID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "carrID",
                carrID.Length, 
                3,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SAPDataSetRefCounter retVal;
            SWPGenericDbParameter carrIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CarrID",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    3,
                    carrID);

            retVal = (SAPDataSetRefCounter)Select(
                new SAPDataSetRefCounter(),
                "uspRefCounterSelectByCarrID",
                carrIDGenericDbParameter);
            return retVal;
        }

    }
}

