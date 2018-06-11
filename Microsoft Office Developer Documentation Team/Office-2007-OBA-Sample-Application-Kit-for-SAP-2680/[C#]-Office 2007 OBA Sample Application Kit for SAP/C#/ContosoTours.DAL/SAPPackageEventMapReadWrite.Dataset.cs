using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageEventMapReadWrite : SWPDataReadWriteBase
    {
        private string _dataSetName = "PackageEventMapDataSet";

        public override string DataSetName
        {
            get
            {
                return _dataSetName;
            }
        }

        public SAPDataSetPackageEventMap SelectAll()
        {            
            return (SAPDataSetPackageEventMap)Select(
                new SAPDataSetPackageEventMap(),
                SelectAllSPCommandText);
        }

        public void Update(SAPDataSetPackageEventMap ds)
        {
            Update(ds.PackageEventMap);
        }

        public SAPDataSetPackageEventMap UpdateAndRefresh(SAPDataSetPackageEventMap ds)
        {
            Update(ds.PackageEventMap);

            return SelectAll();
        }

        public SAPDataSetPackageEventMap SelectByEventID(
            int eventID)
        {
            SAPDataSetPackageEventMap retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            retVal = (SAPDataSetPackageEventMap)Select(
                new SAPDataSetPackageEventMap(),
                "uspPackageEventMapSelectByEventID",
                eventIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetPackageEventMap SelectByPackageEventMapID(
            int packageEventMapID)
        {
            SAPDataSetPackageEventMap retVal;
            SWPGenericDbParameter packageEventMapIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageEventMapID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageEventMapID);

            retVal = (SAPDataSetPackageEventMap)Select(
                new SAPDataSetPackageEventMap(),
                "uspPackageEventMapSelectByPackageEventMapID",
                packageEventMapIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetPackageEventMap SelectByPackageID(
            int packageID)
        {
            SAPDataSetPackageEventMap retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);

            retVal = (SAPDataSetPackageEventMap)Select(
                new SAPDataSetPackageEventMap(),
                "uspPackageEventMapSelectByPackageID",
                packageIDGenericDbParameter);
            return retVal;
        }

        public SAPDataSetPackageEventMap SelectByPackageIDEventID(
            int packageID,
            int eventID)
        {
            SAPDataSetPackageEventMap retVal;
            SWPGenericDbParameter packageIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@PackageID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    packageID);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);

            retVal = (SAPDataSetPackageEventMap)Select(
                new SAPDataSetPackageEventMap(),
                "uspPackageEventMapSelectByPackageIDEventID",
                packageIDGenericDbParameter,
                eventIDGenericDbParameter);
            return retVal;
        }

    }
}

