using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPEventAttendeeTripReadOnly : SWPDataReadOnlyBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspEventAttendeeTripSelectAll";
            }
        }

        public SAPEventAttendeeTripReadOnly(string dbConnectionName)
            : base(dbConnectionName)
        {
        }

        private string _tableName = "EventAttendeeTrip";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }
        
        public const string _eventAttendeeIDColumnName = "EventAttendeeID";

        public const string _packageIDColumnName = "PackageID";

        public const string _customerNumberColumnName = "CustomerNumber";

        public const string _dateOfBirthColumnName = "DateOfBirth";

        public const string _createdColumnName = "Created";

        public const string _eventAttendeeAgencyMapIDColumnName = "EventAttendeeAgencyMapID";

        public const string _agencyNumberColumnName = "AgencyNumber";

        public const string _tripNumberColumnName = "TripNumber";
    }
}

