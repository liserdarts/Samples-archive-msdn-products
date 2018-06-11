using System;
using System.Data;
using System.Text;
using System.Data.Common;


using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefLanguageReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefLanguageDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefLanguage SelectAll()
        {            
            return (SAPDataSetRefLanguage)Select(
                new SAPDataSetRefLanguage(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefLanguage ds)
        {
            Update(ds.RefLanguage);
        }

        public SAPDataSetRefLanguage UpdateAndRefresh(SAPDataSetRefLanguage ds)
        {
            Update(ds.RefLanguage);

            return SelectAll();
        }

        public SAPDataSetRefLanguage SelectByLanguageCode(
            string languageCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "languageCode",
                languageCode.Length, 
                4,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SAPDataSetRefLanguage retVal;
            SWPGenericDbParameter languageCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@LanguageCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    languageCode);

            retVal = (SAPDataSetRefLanguage)Select(
                new SAPDataSetRefLanguage(),
                "uspRefLanguageSelectByLanguageCode",
                languageCodeGenericDbParameter);
            return retVal;
        }

    }
}

