using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCodeReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefCodeDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefCode SelectAll()
        {            
            return (SAPDataSetRefCode)Select(
                new SAPDataSetRefCode(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefCode ds)
        {
            Update(ds.RefCode);
        }

        public SAPDataSetRefCode UpdateAndRefresh(SAPDataSetRefCode ds)
        {
            Update(ds.RefCode);

            return SelectAll();
        }

        public SAPDataSetRefCode SelectByRefTypeCode(
            string refTypeCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "refTypeCode",
                refTypeCode.Length, 
                2,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SAPDataSetRefCode retVal;
            SWPGenericDbParameter refTypeCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@RefTypeCode",
                    DbType.AnsiStringFixedLength,
                    ParameterDirection.Input,
                    2,
                    refTypeCode);

            retVal = (SAPDataSetRefCode)Select(
                new SAPDataSetRefCode(),
                "uspRefCodeSelectByRefTypeCode",
                refTypeCodeGenericDbParameter);
            return retVal;
        }

    }
}

