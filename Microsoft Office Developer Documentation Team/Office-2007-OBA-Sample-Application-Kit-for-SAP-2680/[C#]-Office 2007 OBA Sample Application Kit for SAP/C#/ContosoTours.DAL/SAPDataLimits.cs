using System;
using System.Data.SqlClient;

namespace Microsoft.SAPSK.ContosoTours.DAL
{
    public partial class SAPDataLimits
    {
        public enum SAPLimits
        {
            MSFTDescription = 4000,
            MSFTImage = 2147483647,
            MSFTMoney = 19,
            MSFTName = 255,
            MSFTSmallName = 25
        }
    }
}