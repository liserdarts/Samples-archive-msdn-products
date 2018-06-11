using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace Contoso.SharePoint.Administration.Features.SandboxedSolutionValidator
{
    /// <summary>
    /// Add/Remove Contoso Solution Validator
    /// </summary>
    [Guid("eb5e7d7f-ca1d-45cb-989b-7637c42456ad")]
    public class SandboxedSolutionValidatorEventReceiver : SPFeatureReceiver
    {
        /// <summary>
        /// Register Contoso Solution Validator.
        /// </summary>        
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPUserCodeService sandboxService = SPUserCodeService.Local;
            SPSolutionValidator contosoSolutionValidator = new SolutionValidator(sandboxService);
            sandboxService.SolutionValidators.Add(contosoSolutionValidator);
        }

        /// <summary>
        /// Remove Contoso Solution Validator.
        /// </summary>        
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPUserCodeService sandboxService = SPUserCodeService.Local;
            Guid contosoSolutionValidatorId = typeof(SolutionValidator).GUID;
            sandboxService.SolutionValidators.Remove(contosoSolutionValidatorId);
        }
    }
}
