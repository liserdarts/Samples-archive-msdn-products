using System;
using System.Data;
using System.Text;
using System.Data.Common;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPVenueReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspVenueSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspVenueInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPVenueUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspVenueDelete";
            }
        }

        public SAPVenueReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPVenueReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@VenueID",
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
                                "@VenueName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenueDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@VenueStreet",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenueCity",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenueState",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenuePostalCode",
                                DbType.StringFixedLength,
                                ParameterDirection.Input,
                                25),
                            new SWPGenericDbParameter(
                                "@VenueGeographicMap",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@VenueFacilityMap",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@VenueImage",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@VenueID",
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
                                "@VenueID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@VenueName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenueDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@VenueStreet",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenueCity",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenueState",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@VenuePostalCode",
                                DbType.StringFixedLength,
                                ParameterDirection.Input,
                                25),
                            new SWPGenericDbParameter(
                                "@VenueGeographicMap",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@VenueFacilityMap",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@VenueImage",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "Venue";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _venueIDColumnName = "VenueID";

        public const string _venueNameColumnName = "VenueName";

        public const string _venueDescriptionColumnName = "VenueDescription";

        public const string _venueStreetColumnName = "VenueStreet";

        public const string _venueCityColumnName = "VenueCity";

        public const string _venueStateColumnName = "VenueState";

        public const string _venuePostalCodeColumnName = "VenuePostalCode";

        public const string _venueGeographicMapColumnName = "VenueGeographicMap";

        public const string _venueFacilityMapColumnName = "VenueFacilityMap";

        public const string _venueImageColumnName = "VenueImage";

        public int Insert(
            string venueName,
            string venueDescription,
            string venueStreet,
            string venueCity,
            string venueState,
            string venuePostalCode,
            byte [] venueGeographicMap,
            byte [] venueFacilityMap,
            byte [] venueImage,
            out int venueID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "venueName",
                venueName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venueDescription",
                venueDescription.Length, 
                4000,
                validationErrorReport);
            ValidateParameterLength(
                "venueStreet",
                venueStreet.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venueCity",
                venueCity.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venueState",
                venueState.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venuePostalCode",
                venuePostalCode.Length, 
                25,
                validationErrorReport);
            ValidateParameterLength(
                "venueGeographicMap",
                venueGeographicMap.Length, 
                2147483647,
                validationErrorReport);
            ValidateParameterLength(
                "venueFacilityMap",
                venueFacilityMap.Length, 
                2147483647,
                validationErrorReport);
            ValidateParameterLength(
                "venueImage",
                venueImage.Length, 
                2147483647,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter venueNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueName);
            SWPGenericDbParameter venueDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    venueDescription);
            SWPGenericDbParameter venueStreetGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueStreet",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueStreet);
            SWPGenericDbParameter venueCityGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueCity",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueCity);
            SWPGenericDbParameter venueStateGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueState",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueState);
            SWPGenericDbParameter venuePostalCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenuePostalCode",
                    DbType.StringFixedLength,
                    ParameterDirection.Input,
                    25,
                    venuePostalCode);
            SWPGenericDbParameter venueGeographicMapGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueGeographicMap",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    venueGeographicMap);
            SWPGenericDbParameter venueFacilityMapGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueFacilityMap",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    venueFacilityMap);
            SWPGenericDbParameter venueImageGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueImage",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    venueImage);
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspVenueInsert",
                    venueNameGenericDbParameter,
                    venueDescriptionGenericDbParameter,
                    venueStreetGenericDbParameter,
                    venueCityGenericDbParameter,
                    venueStateGenericDbParameter,
                    venuePostalCodeGenericDbParameter,
                    venueGeographicMapGenericDbParameter,
                    venueFacilityMapGenericDbParameter,
                    venueImageGenericDbParameter,
                    venueIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspVenueInsert",
                    venueNameGenericDbParameter,
                    venueDescriptionGenericDbParameter,
                    venueStreetGenericDbParameter,
                    venueCityGenericDbParameter,
                    venueStateGenericDbParameter,
                    venuePostalCodeGenericDbParameter,
                    venueGeographicMapGenericDbParameter,
                    venueFacilityMapGenericDbParameter,
                    venueImageGenericDbParameter,
                    venueIDGenericDbParameter);
            }

            venueID = (int)venueIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int venueID,
            string venueName,
            string venueDescription,
            string venueStreet,
            string venueCity,
            string venueState,
            string venuePostalCode,
            byte [] venueGeographicMap,
            byte [] venueFacilityMap,
            byte [] venueImage)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "venueName",
                venueName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venueDescription",
                venueDescription.Length, 
                4000,
                validationErrorReport);
            ValidateParameterLength(
                "venueStreet",
                venueStreet.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venueCity",
                venueCity.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venueState",
                venueState.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "venuePostalCode",
                venuePostalCode.Length, 
                25,
                validationErrorReport);
            ValidateParameterLength(
                "venueGeographicMap",
                venueGeographicMap.Length, 
                2147483647,
                validationErrorReport);
            ValidateParameterLength(
                "venueFacilityMap",
                venueFacilityMap.Length, 
                2147483647,
                validationErrorReport);
            ValidateParameterLength(
                "venueImage",
                venueImage.Length, 
                2147483647,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);
            SWPGenericDbParameter venueNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueName);
            SWPGenericDbParameter venueDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    venueDescription);
            SWPGenericDbParameter venueStreetGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueStreet",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueStreet);
            SWPGenericDbParameter venueCityGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueCity",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueCity);
            SWPGenericDbParameter venueStateGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueState",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    venueState);
            SWPGenericDbParameter venuePostalCodeGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenuePostalCode",
                    DbType.StringFixedLength,
                    ParameterDirection.Input,
                    25,
                    venuePostalCode);
            SWPGenericDbParameter venueGeographicMapGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueGeographicMap",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    venueGeographicMap);
            SWPGenericDbParameter venueFacilityMapGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueFacilityMap",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    venueFacilityMap);
            SWPGenericDbParameter venueImageGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueImage",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    venueImage);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspVenueUpdate",
                    venueIDGenericDbParameter,
                    venueNameGenericDbParameter,
                    venueDescriptionGenericDbParameter,
                    venueStreetGenericDbParameter,
                    venueCityGenericDbParameter,
                    venueStateGenericDbParameter,
                    venuePostalCodeGenericDbParameter,
                    venueGeographicMapGenericDbParameter,
                    venueFacilityMapGenericDbParameter,
                    venueImageGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspVenueUpdate",
                    venueIDGenericDbParameter,
                    venueNameGenericDbParameter,
                    venueDescriptionGenericDbParameter,
                    venueStreetGenericDbParameter,
                    venueCityGenericDbParameter,
                    venueStateGenericDbParameter,
                    venuePostalCodeGenericDbParameter,
                    venueGeographicMapGenericDbParameter,
                    venueFacilityMapGenericDbParameter,
                    venueImageGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int venueID)
        {
            int retVal;
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspVenueDelete",
                    venueIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspVenueDelete",
                    venueIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

