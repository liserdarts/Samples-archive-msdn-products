using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPPackageEventReadOnly : SWPDataReadOnlyBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspPackageEventSelectAll";
            }
        }

        public SAPPackageEventReadOnly(string dbConnectionName)
            : base(dbConnectionName)
        {
        }

        private string _tableName = "PackageEvent";

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

        public const string _venueCityColumnName = "VenueCity";

        public const string _venueStateColumnName = "VenueState";

        public const string _packageIDColumnName = "PackageID";
    }
}

