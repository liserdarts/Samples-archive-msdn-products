using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefTravelAgencyReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefTravelAgencyDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefTravelAgency SelectAll()
        {            
            return (SAPDataSetRefTravelAgency)Select(
                new SAPDataSetRefTravelAgency(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefTravelAgency ds)
        {
            Update(ds.RefTravelAgency);
        }

        public SAPDataSetRefTravelAgency UpdateAndRefresh(SAPDataSetRefTravelAgency ds)
        {
            Update(ds.RefTravelAgency);

            return SelectAll();
        }

        public SAPDataSetRefTravelAgency SelectByAgencyNumber(
            string agencyNumber)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "agencyNumber",
                agencyNumber.Length, 
                16,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            SAPDataSetRefTravelAgency retVal;
            SWPGenericDbParameter agencyNumberGenericDbParameter =
                new SWPGenericDbParameter(
                    "@AgencyNumber",
                    DbType.String,
                    ParameterDirection.Input,
                    16,
                    agencyNumber);

            retVal = (SAPDataSetRefTravelAgency)Select(
                new SAPDataSetRefTravelAgency(),
                "uspRefTravelAgencySelectByAgencyNumber",
                agencyNumberGenericDbParameter);
            return retVal;
        }

    }
}

