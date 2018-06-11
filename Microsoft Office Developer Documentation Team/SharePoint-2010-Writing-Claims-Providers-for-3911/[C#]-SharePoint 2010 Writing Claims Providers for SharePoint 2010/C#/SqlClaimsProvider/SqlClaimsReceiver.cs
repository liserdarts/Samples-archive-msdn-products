using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.Diagnostics;
using System.Diagnostics;


namespace SqlClaimsProvider
{
    public class SqlClaimsReceiver : SPClaimProviderFeatureReceiver
    {

        private void ExecBaseFeatureActivated(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            //Wrapper function for base FeatureActivated. Used because base
            //keywork can lead to unverifiable code inside lambda expression.
            base.FeatureActivated(properties);
        }

        public override string ClaimProviderAssembly
        {
            get 
            {
                return typeof(SqlClaims).Assembly.FullName;
            }
        }

        public override string ClaimProviderDescription
        {
            get 
            { 
                return "A sample provider written by Steve Peschka"; 
            }
        }

        public override string ClaimProviderDisplayName
        {
            get 
            {
                return SqlClaims.ProviderDisplayName; 
            }
        }

        public override string ClaimProviderType
        {
            get 
            {
                return typeof(SqlClaims).FullName; 
            }
        }

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            ExecBaseFeatureActivated(properties);
        }
    }
}
