using System;
using System.Data.Common;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    public interface ISWPTransactionDBConnection 
    {
        Database TransactionDatabase
        {
            get;
        }

        DbTransaction Transaction
        {
            get;
        }
    }
}
