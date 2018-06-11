using System;
using Microsoft.SharePoint;

namespace DuetSalesOrderSolution.Customizations
{
    /// <summary>
    /// This class helps in enabling reporting on the sales order Web site.
    /// </summary>
    class Reporting
    {
        //Identifier for the Duet Enterprise Reporting Feature.
        internal const string SAPReportingFeatureId = "B8C75454-5807-4edd-AEE8-8551302F4FE6";

        /// <summary>
        /// Enable reporting on the site.
        /// </summary>
        /// <param name="spWeb">Web site on which reporting is to be activated.</param>
        internal void Activate(SPWeb spWeb)
        {
            // Assuming here that the necessary Reporting Features are activated for the site collection.
            spWeb.Features.Add(new Guid(SAPReportingFeatureId), true);
        }

        /// <summary>
        /// Deactivates reporting on the target Web site.
        /// </summary>
        /// <param name="spWeb">Web site on which reporting is to be deactivated.</param>
        internal void Deactivate(SPWeb spWeb)
        {
            spWeb.Features.Remove(new Guid(SAPReportingFeatureId));
        }
    }
}
