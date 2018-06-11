using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefFlightCityReadWrite : SWPDataReadWriteBase
    {
        public SAPDataReaderRefFlightCity ReaderSelectAll()
        {
            return new SAPDataReaderRefFlightCity(
                ReaderSelect(
                SelectAllSPCommandText));
        }

        public SAPDataReaderRefFlightCity ReaderSelectByCityID(
            int cityID)
        {
            SWPGenericDbParameter cityIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    cityID);

            return new SAPDataReaderRefFlightCity(ReaderSelect(
                "uspRefFlightCitySelectByCityID",
                cityIDGenericDbParameter));
        }
    }
}

