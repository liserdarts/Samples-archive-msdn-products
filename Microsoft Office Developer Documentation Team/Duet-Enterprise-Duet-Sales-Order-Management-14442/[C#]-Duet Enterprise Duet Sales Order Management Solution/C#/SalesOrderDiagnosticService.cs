using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace DuetSalesOrderSolution
{
    // Categories for Duet Sales Order Management Solution logging and tracing.
    public enum CategoryID
    {
        None,
        Deployment,
        Activation,
        Configuration,
        Deactivation
    }

    class SalesOrderDiagnosticService : SPDiagnosticsServiceBase
    {
        private static string diagAreaName = "Sales Order Management";

        public SalesOrderDiagnosticService() : base("Sales Order Diagnostic Service", SPFarm.Local)
        { 
        }

        public SalesOrderDiagnosticService(string name, SPFarm farm) : base(name, farm)
        {
        }

        /// <summary>
        /// This methods adds areas and categories to the diagnostic service.
        /// </summary>
        /// <returns>A collection of areas to be added.</returns>
        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsCategory> diagCategories = new List<SPDiagnosticsCategory>();

            foreach (string catName in Enum.GetNames(typeof(CategoryID)))
            {
                // Keep default least levels of TraceSeverity and EventSeverity to Medium and Information .
                diagCategories.Add(new SPDiagnosticsCategory(catName, TraceSeverity.Medium, EventSeverity.Information));
            }

            yield return new SPDiagnosticsArea(diagAreaName, diagCategories);

        }

        /// <summary>
        /// To allow multiple users to update the persisted object.
        /// </summary>
        /// <returns>TRUE to allow multiple users; FALSE otherwise.</returns>
        protected override bool HasAdditionalUpdateAccess()
        {
            return true;
        }

        /// <summary>
        /// Gets an object of the service.
        /// </summary>
        public static SalesOrderDiagnosticService Local
        {
            get
            {
                return SPDiagnosticsServiceBase.GetLocal<SalesOrderDiagnosticService>();
            }
        }

        /// <summary>
        /// Gets the SPDiagnosticsCategory corresponding to a specified CategoryID enum value.
        /// </summary>
        /// <param name="id">A CategoryID enum value.</param>
        /// <returns>A corresponding SPDiagnosticsCategory.</returns>
        public SPDiagnosticsCategory this[CategoryID id]
        {
            get
            {
                return Areas[diagAreaName].Categories[id.ToString()];
            }
        }

        /// <summary>
        /// Writes an informational message to the logging system.
        /// </summary>
        /// <param name="categoryId">The category in which the message is to be logged</param>
        /// <param name="message">The text of the message.</param>
        public static void LogMessage(CategoryID categoryId, string message)
        {
            SPDiagnosticsCategory category = SalesOrderDiagnosticService.Local[categoryId];
            SalesOrderDiagnosticService.Local.WriteTrace(0, category, TraceSeverity.Medium, message);
        }

        /// <summary>
        /// Writes an error message to the logging system.
        /// </summary>
        /// <param name="categoryId">The category in which the message is to be logged.</param>
        /// <param name="message">The text of the message.</param>
        public static void LogError(CategoryID categoryId, string message)
        {
            SPDiagnosticsCategory category = SalesOrderDiagnosticService.Local[categoryId];
            SalesOrderDiagnosticService.Local.WriteTrace(0, category, TraceSeverity.Unexpected, message);
        }
    }
}
