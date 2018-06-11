
namespace SalesOrderAddIn
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.BusinessData.Infrastructure;
    using Microsoft.BusinessData.MetadataModel;
    using Microsoft.BusinessData.MetadataModel.Collections;
    using Microsoft.BusinessData.Runtime;
    using Microsoft.Office.BusinessData.MetadataModel;
    using Microsoft.Office.BusinessData.Infrastructure;
    using Microsoft.Office.BusinessApplications.Runtime.Deployment;


    /// <summary>
    /// Interfaces with External System using BCS OM
    /// for Reading and updating Entities.
    /// </summary>
    public class SalesDataManager
    {
        private const string SalesOrderHeader =
            "SalesOrderHeader";
        private const string SalesOrderLine =
            "SalesOrderLine";
        private const string SalesOrderHeaderFinderName =
            "SalesOrderHeaderReadList";
        private const string SalesOrderSpecificFinderName =
            "SalesOrderHeaderReadItem";
        private const string SalesOrderLineSpecificFinderName =
            "SalesOrderDetailReadItem";
        private const string SalesOrderLobSystemName =
            "AdventureWorks";
        private const string SalesOrderLobSystemInstanceName =
            "AdventureWorks";
        private const string SalesOrderAssociationName =
            "SalesOrderDetailNavigateAssociation";
        private const string SalesOrderHeaderNamespace =
            "http://intranet.contoso.com";

        private const string DependentDataSolutionID = 
            "SalesOrderSolution";
        private const string DependentDataSolutionVersion = 
            "1.0.0.0";

        private DataTable SalesOrderTable = null;
        private DataTable SalesOrderLineTable = null;
        private IEntity entitySalesOrderHeader = null;
        private IEntity entitySalesOrderLine = null;
        private ILobSystemInstance lobInstance = null;
        private IIdentifierCollection salesOrderLineEntityIds = null;
        private List<DataRow> changedSalesLineRows = null;
        private RemoteSharedFileBackedMetadataCatalog catalog = null;

        /// <summary>
        /// Constructor and initializes the addin as it loads for first time.
        /// </summary>
        public SalesDataManager()
        {
            InitializeMetadataStore();
        }

        /// <summary>
        /// Initializes the Catalog
        /// </summary>
        private void InitializeMetadataStore()
        {
            // Open the shared metadatacatalog cache on the client to 
            // read LOB information. This class represnts the Office 
            // client shared storage for BCS metadata. Once on client,
            // this metadata can be accessed from any Office application
            catalog = new RemoteSharedFileBackedMetadataCatalog();

            // Get the SalesOrderHeader entity instance using 
            // namespace and name
            entitySalesOrderHeader = catalog.GetEntity(
                SalesOrderHeaderNamespace,
                SalesOrderHeader);

            // Get the SalesOrderLine entity instance using 
            // namespace and name                
            entitySalesOrderLine = catalog.GetEntity(
                SalesOrderHeaderNamespace,
                SalesOrderLine);

            salesOrderLineEntityIds =
                entitySalesOrderLine.GetIdentifiers();

            // Get the handle to LobSystem
            lobInstance =
            catalog.GetLobSystem(SalesOrderLobSystemName).
            GetLobSystemInstances()[SalesOrderLobSystemInstanceName];

            // Create list to hold changed rows
            changedSalesLineRows = new List<DataRow>();
        }

        /// <summary>
        /// Gets the EntityInstances for SalesOrderHeader Entity and 
        /// populate the DataTable
        /// </summary>
        /// <returns>Populated DataTable</returns>
        public DataTable GetSalesOrderHeaderItems()
        {
            if ((entitySalesOrderHeader != null)
                && (lobInstance != null))
            {
                // Get the default filters
                IFilterCollection filters =
                    entitySalesOrderHeader.GetMethodInstance(
                    SalesOrderHeaderFinderName,
                    MethodInstanceType.Finder).GetFilters();

                // Execute the FindFiltered method online.
                IEntityInstanceEnumerator enumerator =
                    entitySalesOrderHeader.FindFiltered(
                    filters,
                    SalesOrderHeaderFinderName,
                    lobInstance,
                    OperationMode.Online);

                SalesOrderTable = null;
                SalesOrderTable = catalog.Helper.CreateDataTable(enumerator);                
            }

            return SalesOrderTable;
        }

        /// <summary>
        /// Get SalesOrderLine EntityInstances for a specific 
        /// SalesOrderHeader. 
        /// </summary>
        /// <param name="SalesOrderId">Id of SalesOrderHeader</param>
        /// <returns>Populated SalesOrderLine Instances</returns>
        public DataTable GetSalesOrderLineItems(int SalesOrderId)
        {

            Identity salesOrderIdentity = new Identity(SalesOrderId);

            // Get the specific SalesOrderHeader using FindSpecific 
            IEntityInstance salesOrderinstance =
                entitySalesOrderHeader.FindSpecific(
                salesOrderIdentity,
                SalesOrderSpecificFinderName,
                lobInstance,
                OperationMode.Online);

            // Get the association
            IAssociation associationSalesOrderDetail =
                entitySalesOrderHeader.GetSourceAssociations()
                [SalesOrderAssociationName];

            // Get the associated SalesOrderLine Instances
            IEntityInstanceEnumerator enumerator =
                salesOrderinstance.GetAssociatedInstances(
                associationSalesOrderDetail,
                OperationMode.Online);

            // Populate the SalesOrderLine table
            SalesOrderLineTable = null;

            // Clear the dirty rows, if any
            changedSalesLineRows.Clear();

            SalesOrderLineTable = catalog.Helper.CreateDataTable(enumerator,true);               

            if (SalesOrderLineTable != null)
            {
                SalesOrderLineTable.RowChanged +=
                       new DataRowChangeEventHandler(SalesOrderLineTable_RowChanged);
            }

            return SalesOrderLineTable;
        }

        /// <summary>
        /// Track the row which has been edited by end user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SalesOrderLineTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            changedSalesLineRows.Add(e.Row);
        }

        /// <summary>
        /// Updates the SalesOrderLine Entities in the External System.
        /// </summary>
        public void UpdateSalesOrderLineItems()
        {
            DataRow row = null;

            // Loop through and call Updater on each changed SalesOrderLine
            IEnumerator enumerator = changedSalesLineRows.GetEnumerator();

            while (enumerator.MoveNext())
            {
                row = (DataRow)enumerator.Current;

                // BCS OM adds a column with name BdcIdentity when 
                // it returns the DataTable
                object[] ids = EntityInstanceIdEncoder.
                    DecodeEntityInstanceId(row["BdcIdentity"].ToString());
                
                Identity salesOrderLineIdentity = new Identity(ids);

                // Get the specific SalesOrderLine using FindSpecific 
                IEntityInstance salesOrderLineinstance =
                    entitySalesOrderLine.FindSpecific(
                    salesOrderLineIdentity,
                    SalesOrderLineSpecificFinderName,
                    lobInstance,
                    OperationMode.Online);

                salesOrderLineinstance["UnitPriceDiscount"] =
                    row["UnitPriceDiscount"];

                salesOrderLineinstance.Update();
            }

            return;
        }

        /// <summary>
        /// Checks if DataOnly solution deployed on the client 
        /// </summary>
        /// <returns>true if installed else false</returns>
        static public bool CheckDependentDataSolution()
        {            
            string expectedDataSolutionVersion = 
                SolutionRegistry.GetCurrentSolutionVersion(
                DependentDataSolutionID);

            bool ifDependentDataSolutioReady = true;

            while ((expectedDataSolutionVersion != DependentDataSolutionVersion) 
                && ifDependentDataSolutioReady)
            {

                SolutionDeploymentStatus dataSoltuionStatus = 
                    SolutionRegistry.GetPendingSolutionDeploymentStatus(DependentDataSolutionID);

                switch (dataSoltuionStatus)
                {

                    case SolutionDeploymentStatus.None:

                        //Why in this stage:
                        //Case 1: DependentDataSolution is not installed 
                        //Case 2: DependentDataSolution is installed and activated, 
                        // but current DependentDataSolution is not expected verson

                        //What to do:
                        //Step 1: Display UI to indecate end user to install DependentDataSolution,                         
                        //Step 2: set ifDependentDataSolutioReady = false to return without any BCS related opration

                        System.Windows.Forms.DialogResult resoultForNone = 
                            System.Windows.Forms.MessageBox.Show(
                            "Expected BCS Data Solution not installed, Install the solution.", 
                            "Missing Dependent Solution", 
                            System.Windows.Forms.MessageBoxButtons.OKCancel);                        

                        // In this case, this addin should not using BCS Cached data, 
                        // until the addin be load again by reopening new excel 

                        ifDependentDataSolutioReady = false;

                        break;

                    case SolutionDeploymentStatus.InstallationStarted:

                        //Why in this stage:
                        //Case 1: DependentDataSolution activation is in progress, 
                        // solution should stay in this stage very shortly. 
                        // End user should hit this cases in a very small chance

                        //What to do:
                        //Step 1: Sleep for very short time and try again. 
                        // For safety, could add a Max check count for this while loop

                        System.Threading.Thread.Sleep(1000);

                        break;

                    case SolutionDeploymentStatus.PendingActivation:
                        //Why in this stage:
                        //Case 1: DependentDataSolution is in pending stage due to at 
                        // least one Application is connected with BCS local cache. 
                        //case 2: DependentDataSolution just recovered for pending stage 
                        // and activation is in progress, solution should stay in this 
                        // stage very shortly. End user should hit this cases in a very small chance

                        //What to do:
                        //Step 1: call StartSolutionActivation there for the BCS Runtime will 
                        // display the balloon to indicate user close Application who connected 
                        // with BCS local cache. If the End user follow the indication, 
                        // DependentDataSolution Will be activated 

                        //Step 2: set ifDependentDataSolutioReady = false to return without 
                        // any BCS related opration

                        SolutionRegistry.StartSolutionActivation(DependentDataSolutionID);

                        // in both case, after call StartSolutionActivation, this addin should 
                        // not using BCS Cached data until the addin be loaded again by open 
                        // new excel
                        ifDependentDataSolutioReady = false;

                        break;

                    case SolutionDeploymentStatus.PendingDeactivation:

                        //Why in this stage:
                        //Case 1: a different version of the DependentDataSolution is uninstalled 
                        //  but stuck in PendingDeactivation stage

                        //What to do:
                        //Step 1: need display UI to indecate end User to:
                        //  a. close Applications who connected with BCS local cache, 
                        //      therefore the PendingDeactivation solutions could be uninstalled properly
                        //  b. Install peroper version of the DependentDataSolution

                        //Step 2: set ifDependentDataSolutioReady = false to return without any 
                        //  BCS related opration

                        System.Windows.Forms.MessageBox.Show(
                            "Please Close all Applications and install DependentDataSolution");

                        ifDependentDataSolutioReady = false;

                        break;

                    case SolutionDeploymentStatus.InError:

                        //Why in this stage:
                        //Case 1:  DependentDataSolution is installed but have some issuse to be activated

                        //What to do:
                        //Step 1: need display UI to indicate end user to:
                        //  option a: install properversion DependentDataSolution solution again 

                        //  option b. Uninstall the DependentDataSolution form add/remove programs, 
                        //      and install DependentDataSolution solution again 
                        //Step 2: set ifDependentDataSolutioReady = false to return without any BCS related opration

                        System.Windows.Forms.MessageBox.Show(
                            "DependentDataSolution failed to activate, please install DependentDataSolution");

                        ifDependentDataSolutioReady = false;

                        break;
                }

                //check ExpectedDataSolutionVersion again for try again cases

                expectedDataSolutionVersion = SolutionRegistry.GetCurrentSolutionVersion(DependentDataSolutionID);
            }

            return ifDependentDataSolutioReady;
        }
    }
}
