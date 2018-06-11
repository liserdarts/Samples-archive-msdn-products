using System;
using System.Data;
using System.Text;

using SoftwarePronto.CodeGenerator.DatabaseDriverCommon;


namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPActorEventReadOnly : SWPDataReadOnlyBase
    {
        public override string SelectAllSPCommandText
        {
            get
            {
                return "uspActorEventSelectAll";
            }
        }

        public SAPActorEventReadOnly(string dbConnectionName)
            : base(dbConnectionName)
        {
        }

        private string _tableName = "ActorEvent";

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }
        
        public const string _eventActorIDColumnName = "EventActorID";

        public const string _eventActorNameColumnName = "EventActorName";

        public const string _eventIDColumnName = "EventID";

        public const string _eventActorMapIDColumnName = "EventActorMapID";
    }
}

