using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;
using Microsoft.SAPSK.ContosoTours.Schema;

namespace Microsoft.SAPSK.ContosoTours.Controls
{
    public partial class PackageListControl : UserControl
    {
        private List<TreeNode> _trees = new List<TreeNode>();
        PackageInfo packageInfo;

        public PackageListControl()
        {
            InitializeComponent();
        }

        private void PackageListControl_Load(object sender, EventArgs e)
        {
            SAPRefTravelAgencyReadWrite agency = 
                new SAPRefTravelAgencyReadWrite(Config._dbConnectionName);

            SAPDataSetRefTravelAgency.RefTravelAgencyDataTable agencyData = 
                agency.SelectAll().RefTravelAgency;

            SAPDataSetRefTravelAgency.RefTravelAgencyRow agencyRow =
                agencyData.NewRefTravelAgencyRow();

            agencyRow.AgencyNumber = "0";
            agencyRow.AgencyName = "All";

            agencyData.Rows.InsertAt(agencyRow, 0);
            comboBoxAgency.DisplayMember = SAPRefTravelAgencyReadWrite._agencyNameColumnName;
            comboBoxAgency.ValueMember = SAPRefTravelAgencyReadWrite._agencyNumberColumnName;
            comboBoxAgency.DataSource = agencyData;
            comboBoxAgency.SelectedValue = "00000110"; //Bavarian Castle

            RefreshData(comboBoxAgency.SelectedValue.ToString());
        }

        private void AddFlightNodes(TreeNode root, DataView dataView)
        {
            if (dataView.Count > 0)
            {
                foreach (DataRowView view in dataView)
                {
                    PackageInfo.FlightRow packageInfoFlightRow = view.Row as PackageInfo.FlightRow;

                    string airportNodeKey = string.Format("From: {0}, {1}",
                                                          packageInfoFlightRow.AirportFrom,
                                                          packageInfoFlightRow.CityFrom);
                    int airportIndex =
                        root.Nodes.IndexOfKey(airportNodeKey);

                    TreeNode airportNode;
                    if (airportIndex == -1)
                    {
                        airportNode = root.Nodes.Add(airportNodeKey, airportNodeKey);
                        airportNode.ImageIndex = 7;
                        airportNode.SelectedImageIndex = 7;
                    }
                    else
                    {
                        airportNode = root.Nodes[airportIndex];
                    }

                    if(!airportNode.Nodes.ContainsKey(packageInfoFlightRow.FlightDate))
                    {
                        int seatsFree = 0;
                        switch (root.Text)
                        {
                            case "Gold":
                                seatsFree = packageInfoFlightRow.FirstFree;
                                break;
                            case "Silver":
                                seatsFree = packageInfoFlightRow.BusinessFree;
                                break;
                            case "Bronze":
                                seatsFree = packageInfoFlightRow.EconomyFree;
                                break;
                        }
                        TreeNode flightNode = airportNode.Nodes.Add(packageInfoFlightRow.FlightDate,
                            string.Format("[Seats: {0:000}], {1}", seatsFree, packageInfoFlightRow.FlightDate));
                        flightNode.ImageIndex = 6;
                        flightNode.SelectedImageIndex = 6;
                        flightNode.ToolTipText =
                            string.Format("Airline: {0}", packageInfoFlightRow.Airline);
                    }
                }
            }
            else
            {
                root.Parent.Tag = null;
                root.Nodes.Add("[No Available Flights]");
            }
        }

        private void RefreshData()
        {
            RefreshData("0");
        }

        private void RefreshData(string agencyNumber)
        {
            PseudoProgressForm _pseudoProgress = new PseudoProgressForm();
            _pseudoProgress.ProgressLabel = "Querying SAP...";
            BackgroundWorker background = new BackgroundWorker();            

            background.DoWork +=
                delegate(object sender, DoWorkEventArgs e)
                {
                    if(agencyNumber == "0")
                    {
                        packageInfo = DataHelper.GetPackageInfo();                        
                    }
                    else
                    {
                        packageInfo = DataHelper.GetPackageInfo(agencyNumber);
                    }

                    DataView flightView = packageInfo.Flight.DefaultView;
                    _trees.Clear();

                    foreach (PackageInfo.PackageRow packageInfoPackageRow in packageInfo.Package.Rows)
                    {
                        TreeNode packageRoot = new TreeNode(packageInfoPackageRow.PackageName);
                        packageRoot.ToolTipText = packageInfoPackageRow.PackageDescription;
                        packageRoot.Tag = string.Format("P:{0}:{1}", packageInfoPackageRow.PackageID, 0);
                        packageRoot.ImageIndex = 1;
                        packageRoot.SelectedImageIndex = 1;

                        foreach (DataRow eventDataRow in packageInfoPackageRow.GetChildRows("Package_Event"))
                        {
                            PackageInfo.EventRow packageInfoEventRow = eventDataRow as PackageInfo.EventRow;

                            TreeNode eventRoot = packageRoot.Nodes.Add(packageInfoEventRow.EventName);
                            eventRoot.ToolTipText = string.Format("Description: {0}\nVenue: {1}", packageInfoEventRow.EventDescription, packageInfoEventRow.VenueName);
                            eventRoot.Tag =
                                string.Format("E:{0}:{1}", packageInfoEventRow.PackageID,
                                              packageInfoEventRow.EventID);
                            eventRoot.ImageIndex = 2;
                            eventRoot.SelectedImageIndex = 2;

                            TreeNode goldNode = eventRoot.Nodes.Add("Gold");
                            goldNode.ImageIndex = 3;
                            goldNode.SelectedImageIndex = 3;

                            flightView.RowFilter =
                                string.Format("EventID = {0} AND FirstFree > 0", packageInfoEventRow.EventID);

                            AddFlightNodes(goldNode, flightView);

                            TreeNode silverNode = eventRoot.Nodes.Add("Silver");
                            silverNode.ImageIndex = 4;
                            silverNode.SelectedImageIndex = 4;

                            flightView.RowFilter =
                                string.Format("EventID = {0} AND BusinessFree > 0", packageInfoEventRow.EventID);

                            AddFlightNodes(silverNode, flightView);

                            TreeNode bronzeNode = eventRoot.Nodes.Add("Bronze");
                            bronzeNode.ImageIndex = 5;
                            bronzeNode.SelectedImageIndex = 5;

                            flightView.RowFilter =
                                string.Format("EventID = {0} AND EconomyFree > 0", packageInfoEventRow.EventID);

                            AddFlightNodes(bronzeNode, flightView);
                        }
                        _trees.Add(packageRoot);
                    }
                };

            background.RunWorkerCompleted +=
                delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (_trees.Count > 0)
                    {
                        treeViewPackages.Nodes.Clear();
                        treeViewPackages.Nodes.AddRange(_trees.ToArray());
                    }
                    _pseudoProgress.Close();                    
                };

            background.RunWorkerAsync();
            _pseudoProgress.ShowDialog(this);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshData(comboBoxAgency.SelectedValue.ToString());
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            TreeNode selected = treeViewPackages.SelectedNode;
                string[] nodeTypeInfo = selected.Tag.ToString().Split(':');

            PackageInfo package = new PackageInfo();
            string nodeType = nodeTypeInfo[0];
            string packageID = nodeTypeInfo[1];
            string eventID = nodeTypeInfo[2];
            foreach (DataRow packageRow in packageInfo.Package.Select("PackageID = " + packageID))
            {
                package.Package.Rows.Add(packageRow.ItemArray);

                string criteria = "PackageID = {0}";
                if(nodeType == "E")
                {
                    criteria += " AND EventID = {1}";
                }
                foreach (DataRow eventRow in packageInfo.Event.Select(string.Format(criteria, packageID, eventID)))
                {
                    package.Event.Rows.Add(eventRow.ItemArray);
                    foreach (DataRow flightRow in packageInfo.Flight.Select(string.Format("EventID = {0} AND (FirstFree > 0 OR BusinessFree > 0 OR EconomyFree > 0)", eventID)))
                    {
                        package.Flight.Rows.Add(flightRow.ItemArray);
                    }
                }
                break; //we assume query returns only 1 package
            }

            switch (nodeType)
            {
                case "P":
                    ExcelHelper.LoadExcelSheet("PackageAvailability", Resources.PackageAvailability, package);
                    break;
                case "E":
                    ExcelHelper.LoadExcelSheet("EventFlightAvailability", Resources.EventFlightAvailability, package);
                    break;
            }
        }

        private void treeViewPackages_AfterSelect(object sender, TreeViewEventArgs e)
        {
            buttonDetails.Enabled = e.Node.Tag != null;
        }

        private void comboBoxAgency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshData(comboBoxAgency.SelectedValue.ToString());
        }
    }
}
