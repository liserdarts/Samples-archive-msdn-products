using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.SharePoint.UserCode;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace Contoso.SharePoint.Administration
{
    /// <summary>
    /// Contoso custom solution validator.
    /// </summary>
    [Guid("BCDC91D0-F064-4EE0-B6D1-2AE4E0A37B53")]
    public class SolutionValidator : SPSolutionValidator
    {
        /// <summary>
        /// Initializes a new instance of the SolutionValidator class. 
        /// </summary>
        public SolutionValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SolutionValidator class. 
        /// </summary>
        /// <param name="sandboxService">Sandboxed service instance.</param>
        public SolutionValidator(SPUserCodeService sandboxService) :
            base("ContosoSandboxedSolutionValidator", sandboxService)
        {
            Signature = 10000;
        }

        /// <summary>
        /// Checks if the solution has JavaScript files.
        /// </summary>
        /// <param name="properties">Solution validation properties.</param>
        public override void ValidateSolution(SPSolutionValidationProperties properties)
        {
            base.ValidateSolution(properties);

            bool isValidSolution = true;
            // Check if the solution file contains any JavaScript file.
            foreach (SPSolutionFile file in properties.Files)
            {
                if (file.Location.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
                {
                    isValidSolution = false;
                    properties.ValidationErrorMessage = "Contoso IT : Sandboxed solutions should not contain JavaScript files.";
                    properties.ValidationErrorUrl = "/_layouts/Contoso.SharePoint.Administration/ContosoError.aspx";
                    break;
                }
            }

            properties.Valid = isValidSolution;
        }
    }
}
