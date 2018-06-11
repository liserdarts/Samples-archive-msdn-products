using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefFlightCityReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "RefFlightCityDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetRefFlightCity SelectAll()
        {            
            return (SAPDataSetRefFlightCity)Select(
                new SAPDataSetRefFlightCity(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetRefFlightCity ds)
        {
            Update(ds.RefFlightCity);
        }

        public SAPDataSetRefFlightCity UpdateAndRefresh(SAPDataSetRefFlightCity ds)
        {
            Update(ds.RefFlightCity);

            return SelectAll();
        }

        public SAPDataSetRefFlightCity SelectByCityID(
            int cityID)
        {
            SAPDataSetRefFlightCity retVal;
            SWPGenericDbParameter cityIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    cityID);

            retVal = (SAPDataSetRefFlightCity)Select(
                new SAPDataSetRefFlightCity(),
                "uspRefFlightCitySelectByCityID",
                cityIDGenericDbParameter);
            return retVal;
        }

    }
}

