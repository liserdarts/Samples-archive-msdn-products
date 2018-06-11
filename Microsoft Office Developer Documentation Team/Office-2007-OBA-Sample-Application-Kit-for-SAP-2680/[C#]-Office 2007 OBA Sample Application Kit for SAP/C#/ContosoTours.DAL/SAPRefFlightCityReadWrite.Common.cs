using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPRefFlightCityReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspRefFlightCitySelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspRefFlightCityInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPRefFlightCityUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspRefFlightCityDelete";
            }
        }

        public SAPRefFlightCityReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPRefFlightCityReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
            :
            base(dbTransactionConnection)
        {
        }

        private DbParameter[] _deleteSPParameters = null;

        private DbParameter[] _insertSPParameters = null;

        private DbParameter[] _updateSPParameters = null;

        public override DbParameter[] DeleteSPParameters
        {
            get
            {
                if (_deleteSPParameters == null)
                {
                    // This is marker used by the code generator so do not
                    // modify the following code
                    _deleteSPParameters =  new SWPGenericDbParameter []
                        {
                            new SWPGenericDbParameter(
                                "@CityID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                        };
                }

                return _deleteSPParameters;
            }
        }

        public override DbParameter[] InsertSPParameters
        {
            get
            {
                if (_insertSPParameters == null)
                {
                    // This is marker used by the code generator so do not
                    // modify the following code
                    _insertSPParameters =  new SWPGenericDbParameter []
                        {
                            new SWPGenericDbParameter(
                                "@CityName",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
                            new SWPGenericDbParameter(
                                "@CityID",
                                DbType.Int32,
                                ParameterDirection.Output,
                                0),
                        };
                }

                return _insertSPParameters;
            }
        }

        public override DbParameter[] UpdateSPParameters
        {
            get
            {
                if (_updateSPParameters == null)
                {
                    // This is marker used by the code generator so do not
                    // modify the following code
                    _updateSPParameters =  new SWPGenericDbParameter []
                        {
                            new SWPGenericDbParameter(
                                "@CityID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@CityName",
                                DbType.String,
                                ParameterDirection.Input,
                                50),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "RefFlightCity";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _cityIDColumnName = "CityID";

        public const string _cityNameColumnName = "CityName";

        public int Insert(
            string cityName,
            out int cityID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "cityName",
                cityName.Length, 
                50,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter cityNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityName",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    cityName);
            SWPGenericDbParameter cityIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefFlightCityInsert",
                    cityNameGenericDbParameter,
                    cityIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefFlightCityInsert",
                    cityNameGenericDbParameter,
                    cityIDGenericDbParameter);
            }

            cityID = (int)cityIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int cityID,
            string cityName)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "cityName",
                cityName.Length, 
                50,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter cityIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    cityID);
            SWPGenericDbParameter cityNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityName",
                    DbType.String,
                    ParameterDirection.Input,
                    50,
                    cityName);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefFlightCityUpdate",
                    cityIDGenericDbParameter,
                    cityNameGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefFlightCityUpdate",
                    cityIDGenericDbParameter,
                    cityNameGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int cityID)
        {
            int retVal;
            SWPGenericDbParameter cityIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@CityID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    cityID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspRefFlightCityDelete",
                    cityIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspRefFlightCityDelete",
                    cityIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

