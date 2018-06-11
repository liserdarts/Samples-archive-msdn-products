using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using DuetSalesOrderSolution.Customizations;

namespace DuetSalesOrderSolution.Features.SalesOrderSiteFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("237f1b5c-e8fe-4e4e-b02b-d8c6f9b483b4")]
    public class SalesOrderSiteFeatureEventReceiver : SPFeatureReceiver
    {
        private string[] externalListTitles = {"Sales Order Headers", "Sales Order Items" };
        
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            base.FeatureActivated(properties);

            SalesOrderDiagnosticService.LogMessage(CategoryID.Activation,
                    String.Format("Activating feature: {0}", properties.Definition.DisplayName));

            try
            {
                SPWeb spWeb = (SPWeb)properties.Feature.Parent;

                // Create currency list for custom lookup.
                CurrencyList currencyList = new CurrencyList();
                currencyList.Activate(spWeb);

                //Enable list customizations.
                ListCustomizations listCustomizations = new ListCustomizations(externalListTitles);
                listCustomizations.Activate(spWeb);

                // Enable reporting.
                Reporting reporting = new Reporting();
                reporting.Activate(spWeb);

                SalesOrderDiagnosticService.LogMessage(CategoryID.Activation,
                        String.Format("Feature activation succeeded for {0}", properties.Definition.DisplayName));
            }
            catch (Exception exception)
            {
                SalesOrderDiagnosticService.LogError(CategoryID.Activation,
                        String.Format("Failed to activate {0}: {1}", properties.Definition.DisplayName, exception.Message));
            }
        }

  
        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SalesOrderDiagnosticService.LogMessage(CategoryID.Deactivation,
                    String.Format("Deactivating feature: {0}", properties.Definition.DisplayName));

            try
            {
                SPWeb spWeb = (SPWeb)properties.Feature.Parent;

                Reporting reporting = new Reporting();
                reporting.Deactivate(spWeb);

                ListCustomizations listCustomizations = new ListCustomizations(externalListTitles);
                listCustomizations.Deactivate(spWeb);

                base.FeatureDeactivating(properties);

                SalesOrderDiagnosticService.LogMessage(CategoryID.Activation,
                        String.Format("Feature deactivation succeeded for {0}", properties.Definition.DisplayName));
            }
            catch (Exception exception)
            {
                SalesOrderDiagnosticService.LogError(CategoryID.Deactivation,
                        String.Format("Failed to deactivate {0}: {1}", properties.Definition.DisplayName, exception.Message));
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
