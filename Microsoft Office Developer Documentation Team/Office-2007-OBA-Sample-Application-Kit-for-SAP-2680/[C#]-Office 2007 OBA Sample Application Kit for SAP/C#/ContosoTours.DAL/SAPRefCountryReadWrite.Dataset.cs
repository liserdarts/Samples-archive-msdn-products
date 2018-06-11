using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefCountryReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefCountryDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefCountry SelectAll()
        {            
            return (SAPDataSetRefCountry)Select(
                new SAPDataSetRefCountry(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefCountry ds)
        {
            Update(ds.RefCountry);
        }

        public SAPDataSetRefCountry UpdateAndRefresh(SAPDataSetRefCountry ds)
        {
            Update(ds.RefCountry);

            return SelectAll();
        }

        public SAPDataSetRefCountry SelectByCountryCode(
            string countryCode)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "countryCode",
                countryCode.Length, 
                4,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SAPDataSetRefCountry retVal;
            SWPGenericDbParameter countryCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CountryCode",
                    DbType.String,
                    ParameterDirection.Input,
                    4,
                    countryCode);

            retVal = (SAPDataSetRefCountry)Select(
                new SAPDataSetRefCountry(),
                "uspRefCountrySelectByCountryCode",
                countryCodeGenericDbParameter);
            return retVal;
        }

    }
}

