using System;
using System.Data;
using System.Text;
using System.Data.Common;



using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventReadWrite : SWPDataReadWriteBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventSelectAll";
            }
        }

        public override string InsertSPCommandText
        {
            get
            {
                return "uspEventInsert";
            }
        }

        public override string UpdateSPCommandText
        {
            get
            {
                return "SAPEventUpdate";
            }
        }

        public override string DeleteSPCommandText
        {
            get
            {
                return "uspEventDelete";
            }
        }

        public SAPEventReadWrite(string dbConnectionName)
            :
            base(dbConnectionName)
        {
        }

        public SAPEventReadWrite(ISWPTransactionDBConnection dbTransactionConnection)
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
                                "@EventID",
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
                                "@VenueID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventTypeID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@EventDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@EventPhoto",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@EventDate",
                                DbType.DateTime,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@GoldPackagePrice",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@SilverPackagePrice",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@BronzePackagePrice",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@GoldPackageTrueCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@SilverPackageTrueCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@BronzePackageTrueCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventTotalCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventID",
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
                                "@EventID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@VenueID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventTypeID",
                                DbType.Int32,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventName",
                                DbType.String,
                                ParameterDirection.Input,
                                255),
                            new SWPGenericDbParameter(
                                "@EventDescription",
                                DbType.String,
                                ParameterDirection.Input,
                                4000),
                            new SWPGenericDbParameter(
                                "@EventPhoto",
                                DbType.Binary,
                                ParameterDirection.Input,
                                2147483647),
                            new SWPGenericDbParameter(
                                "@EventDate",
                                DbType.DateTime,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@GoldPackagePrice",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@SilverPackagePrice",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@BronzePackagePrice",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@GoldPackageTrueCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@SilverPackageTrueCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@BronzePackageTrueCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                            new SWPGenericDbParameter(
                                "@EventTotalCost",
                                DbType.Currency,
                                ParameterDirection.Input,
                                0),
                        };
                }

                return _updateSPParameters;
            }
        }

        public const string _tableName = "Event";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public const string _eventIDColumnName = "EventID";

        public const string _venueIDColumnName = "VenueID";

        public const string _eventNameColumnName = "EventName";

        public const string _eventDescriptionColumnName = "EventDescription";

        public const string _eventPhotoColumnName = "EventPhoto";

        public const string _eventDateColumnName = "EventDate";

        public const string _goldPackagePriceColumnName = "GoldPackagePrice";

        public const string _silverPackagePriceColumnName = "SilverPackagePrice";

        public const string _bronzePackagePriceColumnName = "BronzePackagePrice";

        public const string _goldPackageTrueCostColumnName = "GoldPackageTrueCost";

        public const string _silverPackageTrueCostColumnName = "SilverPackageTrueCost";

        public const string _bronzePackageTrueCostColumnName = "BronzePackageTrueCost";

        public const string _eventTotalCostColumnName = "EventTotalCost";

        public const string _eventTypeNameColumnName = "EventTypeName";

        public const string _venueNameColumnName = "VenueName";

        public int Insert(
            int venueID,
            int eventTypeID,
            string eventName,
            string eventDescription,
            byte [] eventPhoto,
            DateTime eventDate,
            decimal goldPackagePrice,
            decimal silverPackagePrice,
            decimal bronzePackagePrice,
            decimal goldPackageTrueCost,
            decimal silverPackageTrueCost,
            decimal bronzePackageTrueCost,
            decimal eventTotalCost,
            out int eventID)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventName",
                eventName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "eventDescription",
                eventDescription.Length, 
                4000,
                validationErrorReport);
            ValidateParameterLength(
                "eventPhoto",
                eventPhoto.Length, 
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
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);
            SWPGenericDbParameter eventNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventName);
            SWPGenericDbParameter eventDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    eventDescription);
            SWPGenericDbParameter eventPhotoGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventPhoto",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    eventPhoto);
            SWPGenericDbParameter eventDateGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventDate",
                    DbType.DateTime,
                    ParameterDirection.Input,
                    0,
                    eventDate);
            SWPGenericDbParameter goldPackagePriceGenericDbParameter =
                new SWPGenericDbParameter(
                    "@GoldPackagePrice",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    goldPackagePrice);
            SWPGenericDbParameter silverPackagePriceGenericDbParameter =
                new SWPGenericDbParameter(
                    "@SilverPackagePrice",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    silverPackagePrice);
            SWPGenericDbParameter bronzePackagePriceGenericDbParameter =
                new SWPGenericDbParameter(
                    "@BronzePackagePrice",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    bronzePackagePrice);
            SWPGenericDbParameter goldPackageTrueCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@GoldPackageTrueCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    goldPackageTrueCost);
            SWPGenericDbParameter silverPackageTrueCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@SilverPackageTrueCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    silverPackageTrueCost);
            SWPGenericDbParameter bronzePackageTrueCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@BronzePackageTrueCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    bronzePackageTrueCost);
            SWPGenericDbParameter eventTotalCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTotalCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    eventTotalCost);
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Output,
                    0);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventInsert",
                    venueIDGenericDbParameter,
                    eventTypeIDGenericDbParameter,
                    eventNameGenericDbParameter,
                    eventDescriptionGenericDbParameter,
                    eventPhotoGenericDbParameter,
                    eventDateGenericDbParameter,
                    goldPackagePriceGenericDbParameter,
                    silverPackagePriceGenericDbParameter,
                    bronzePackagePriceGenericDbParameter,
                    goldPackageTrueCostGenericDbParameter,
                    silverPackageTrueCostGenericDbParameter,
                    bronzePackageTrueCostGenericDbParameter,
                    eventTotalCostGenericDbParameter,
                    eventIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventInsert",
                    venueIDGenericDbParameter,
                    eventTypeIDGenericDbParameter,
                    eventNameGenericDbParameter,
                    eventDescriptionGenericDbParameter,
                    eventPhotoGenericDbParameter,
                    eventDateGenericDbParameter,
                    goldPackagePriceGenericDbParameter,
                    silverPackagePriceGenericDbParameter,
                    bronzePackagePriceGenericDbParameter,
                    goldPackageTrueCostGenericDbParameter,
                    silverPackageTrueCostGenericDbParameter,
                    bronzePackageTrueCostGenericDbParameter,
                    eventTotalCostGenericDbParameter,
                    eventIDGenericDbParameter);
            }

            eventID = (int)eventIDGenericDbParameter.Value;

            return retVal;
        }

        public int Update(
            int eventID,
            int venueID,
            int eventTypeID,
            string eventName,
            string eventDescription,
            byte [] eventPhoto,
            DateTime eventDate,
            decimal goldPackagePrice,
            decimal silverPackagePrice,
            decimal bronzePackagePrice,
            decimal goldPackageTrueCost,
            decimal silverPackageTrueCost,
            decimal bronzePackageTrueCost,
            decimal eventTotalCost)
        {
            StringBuilder validationErrorReport = new StringBuilder();

            ValidateParameterLength(
                "eventName",
                eventName.Length, 
                255,
                validationErrorReport);
            ValidateParameterLength(
                "eventDescription",
                eventDescription.Length, 
                4000,
                validationErrorReport);
            ValidateParameterLength(
                "eventPhoto",
                eventPhoto.Length, 
                2147483647,
                validationErrorReport);

            if (validationErrorReport.Length > 0)
            {
                throw new ApplicationException(validationErrorReport.ToString());
            }

            int retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            SWPGenericDbParameter venueIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@VenueID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    venueID);
            SWPGenericDbParameter eventTypeIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTypeID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventTypeID);
            SWPGenericDbParameter eventNameGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventName",
                    DbType.String,
                    ParameterDirection.Input,
                    255,
                    eventName);
            SWPGenericDbParameter eventDescriptionGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventDescription",
                    DbType.String,
                    ParameterDirection.Input,
                    4000,
                    eventDescription);
            SWPGenericDbParameter eventPhotoGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventPhoto",
                    DbType.Binary,
                    ParameterDirection.Input,
                    2147483647,
                    eventPhoto);
            SWPGenericDbParameter eventDateGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventDate",
                    DbType.DateTime,
                    ParameterDirection.Input,
                    0,
                    eventDate);
            SWPGenericDbParameter goldPackagePriceGenericDbParameter =
                new SWPGenericDbParameter(
                    "@GoldPackagePrice",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    goldPackagePrice);
            SWPGenericDbParameter silverPackagePriceGenericDbParameter =
                new SWPGenericDbParameter(
                    "@SilverPackagePrice",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    silverPackagePrice);
            SWPGenericDbParameter bronzePackagePriceGenericDbParameter =
                new SWPGenericDbParameter(
                    "@BronzePackagePrice",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    bronzePackagePrice);
            SWPGenericDbParameter goldPackageTrueCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@GoldPackageTrueCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    goldPackageTrueCost);
            SWPGenericDbParameter silverPackageTrueCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@SilverPackageTrueCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    silverPackageTrueCost);
            SWPGenericDbParameter bronzePackageTrueCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@BronzePackageTrueCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    bronzePackageTrueCost);
            SWPGenericDbParameter eventTotalCostGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventTotalCost",
                    DbType.Currency,
                    ParameterDirection.Input,
                    0,
                    eventTotalCost);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventUpdate",
                    eventIDGenericDbParameter,
                    venueIDGenericDbParameter,
                    eventTypeIDGenericDbParameter,
                    eventNameGenericDbParameter,
                    eventDescriptionGenericDbParameter,
                    eventPhotoGenericDbParameter,
                    eventDateGenericDbParameter,
                    goldPackagePriceGenericDbParameter,
                    silverPackagePriceGenericDbParameter,
                    bronzePackagePriceGenericDbParameter,
                    goldPackageTrueCostGenericDbParameter,
                    silverPackageTrueCostGenericDbParameter,
                    bronzePackageTrueCostGenericDbParameter,
                    eventTotalCostGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventUpdate",
                    eventIDGenericDbParameter,
                    venueIDGenericDbParameter,
                    eventTypeIDGenericDbParameter,
                    eventNameGenericDbParameter,
                    eventDescriptionGenericDbParameter,
                    eventPhotoGenericDbParameter,
                    eventDateGenericDbParameter,
                    goldPackagePriceGenericDbParameter,
                    silverPackagePriceGenericDbParameter,
                    bronzePackagePriceGenericDbParameter,
                    goldPackageTrueCostGenericDbParameter,
                    silverPackageTrueCostGenericDbParameter,
                    bronzePackageTrueCostGenericDbParameter,
                    eventTotalCostGenericDbParameter);
            }

            return retVal;
        }

        public int Delete(
            int eventID)
        {
            int retVal;
            SWPGenericDbParameter eventIDGenericDbParameter =
                new SWPGenericDbParameter(
                    "@EventID",
                    DbType.Int32,
                    ParameterDirection.Input,
                    0,
                    eventID);
            if (_dbTransactionConnection == null)
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbConnectionName,
                    "uspEventDelete",
                    eventIDGenericDbParameter);
            }

            else
            {
                retVal = SWPDBHelper.StoredProcExecuteNonQuery(
                    _dbTransactionConnection,
                    "uspEventDelete",
                    eventIDGenericDbParameter);
            }

            return retVal;
        }
    }
}

