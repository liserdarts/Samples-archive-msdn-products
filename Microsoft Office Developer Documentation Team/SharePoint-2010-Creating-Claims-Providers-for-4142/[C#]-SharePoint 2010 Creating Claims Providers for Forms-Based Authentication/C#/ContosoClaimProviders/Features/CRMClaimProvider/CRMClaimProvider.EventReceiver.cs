using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration.Claims;

namespace ContosoClaimProviders.Features.CRMClaimProvider
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("f443aa41-9993-44fe-ace6-8bf37a32474b")]
    public class CRMClaimProviderEventReceiver : SPClaimProviderFeatureReceiver
    {
        private string providerDisplayName = "CRM Claim Provider";
        private string providerDescription = "Provides Claims from Contoso CRM System";

        public override string ClaimProviderAssembly
        {
            get { return typeof(ContosoClaimProviders.CRMClaimProvider).Assembly.FullName; }
        }
        public override string ClaimProviderType
        {
            get { return typeof(ContosoClaimProviders.CRMClaimProvider).FullName; }
        }
        public override string ClaimProviderDisplayName
        {
            get
            {
                return providerDisplayName;
            }
        }
        public override string ClaimProviderDescription
        {
            get
            {
                return providerDescription;
            }
        }

    }
}
