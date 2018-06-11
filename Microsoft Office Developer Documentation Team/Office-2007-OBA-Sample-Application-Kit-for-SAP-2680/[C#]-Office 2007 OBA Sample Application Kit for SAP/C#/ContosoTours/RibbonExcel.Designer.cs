namespace Microsoft.SAPSK.ContosoTours
{
    partial class RibbonExcel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonExcel));
            this.tab1 = new Microsoft.Office.Tools.Ribbon.RibbonTab();
            this.groupSAP = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.buttonLogin = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.groupMode = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.toggleButtonSwitchMode = new Microsoft.Office.Tools.Ribbon.RibbonToggleButton();
            this.groupPackages = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.toggleButtonBuyPackage = new Microsoft.Office.Tools.Ribbon.RibbonToggleButton();
            this.toggleButtonPackageOutline = new Microsoft.Office.Tools.Ribbon.RibbonToggleButton();
            this.buttonPackageManagement = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.groupEvents = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.toggleButtonSearchEvents = new Microsoft.Office.Tools.Ribbon.RibbonToggleButton();
            this.buttonEventManagement = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.buttonVenues = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.groupListings = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.buttonListofCustomers = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.buttonFlightList = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.groupAnalysis = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.buttonRevenueForecast = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.menuSales = new Microsoft.Office.Tools.Ribbon.RibbonMenu();
            this.buttonPackageSales = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.buttonPackageType = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.buttonTicket = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.tab1.SuspendLayout();
            this.groupSAP.SuspendLayout();
            this.groupMode.SuspendLayout();
            this.groupPackages.SuspendLayout();
            this.groupEvents.SuspendLayout();
            this.groupListings.SuspendLayout();
            this.groupAnalysis.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupSAP);
            this.tab1.Groups.Add(this.groupMode);
            this.tab1.Groups.Add(this.groupPackages);
            this.tab1.Groups.Add(this.groupEvents);
            this.tab1.Groups.Add(this.groupListings);
            this.tab1.Groups.Add(this.groupAnalysis);
            this.tab1.Label = "Contoso Tours";
            this.tab1.Name = "tab1";
            // 
            // groupSAP
            // 
            this.groupSAP.Items.Add(this.buttonLogin);
            this.groupSAP.Label = "SAP";
            this.groupSAP.Name = "groupSAP";
            // 
            // buttonLogin
            // 
            this.buttonLogin.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonLogin.Label = "Login";
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.OfficeImageId = "DatabasePermissionsMenu";
            this.buttonLogin.ShowImage = true;
            this.buttonLogin.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonLogin_Click);
            // 
            // groupMode
            // 
            this.groupMode.Items.Add(this.toggleButtonSwitchMode);
            this.groupMode.Label = "Mode";
            this.groupMode.Name = "groupMode";
            // 
            // toggleButtonSwitchMode
            // 
            this.toggleButtonSwitchMode.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonSwitchMode.Label = "Switch to Analysis";
            this.toggleButtonSwitchMode.Name = "toggleButtonSwitchMode";
            this.toggleButtonSwitchMode.OfficeImageId = "ChartRefresh";
            this.toggleButtonSwitchMode.ShowImage = true;
            this.toggleButtonSwitchMode.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.toggleButtonSwitchMode_Click);
            // 
            // groupPackages
            // 
            this.groupPackages.Items.Add(this.toggleButtonBuyPackage);
            this.groupPackages.Items.Add(this.toggleButtonPackageOutline);
            this.groupPackages.Items.Add(this.buttonPackageManagement);
            this.groupPackages.Label = "Packages";
            this.groupPackages.Name = "groupPackages";
            // 
            // toggleButtonBuyPackage
            // 
            this.toggleButtonBuyPackage.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonBuyPackage.Label = "Buy";
            this.toggleButtonBuyPackage.Name = "toggleButtonBuyPackage";
            this.toggleButtonBuyPackage.OfficeImageId = "AddOrRemoveAttendees";
            this.toggleButtonBuyPackage.ShowImage = true;
            this.toggleButtonBuyPackage.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.toggleButtonBuyPackage_Click);
            // 
            // toggleButtonPackageOutline
            // 
            this.toggleButtonPackageOutline.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonPackageOutline.Label = "Outline";
            this.toggleButtonPackageOutline.Name = "toggleButtonPackageOutline";
            this.toggleButtonPackageOutline.OfficeImageId = "SmartArtOrganizationChartRightHanging";
            this.toggleButtonPackageOutline.ShowImage = true;
            this.toggleButtonPackageOutline.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.toggleButtonPackageOutline_Click);
            // 
            // buttonPackageManagement
            // 
            this.buttonPackageManagement.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonPackageManagement.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.proj_management;
            this.buttonPackageManagement.Label = "Manage";
            this.buttonPackageManagement.Name = "buttonPackageManagement";
            this.buttonPackageManagement.OfficeImageId = "OutlookGlobe";
            this.buttonPackageManagement.ShowImage = true;
            this.buttonPackageManagement.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonPackageManagement_Click);
            // 
            // groupEvents
            // 
            this.groupEvents.Items.Add(this.toggleButtonSearchEvents);
            this.groupEvents.Items.Add(this.buttonEventManagement);
            this.groupEvents.Items.Add(this.buttonVenues);
            this.groupEvents.Label = "Events";
            this.groupEvents.Name = "groupEvents";
            // 
            // toggleButtonSearchEvents
            // 
            this.toggleButtonSearchEvents.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonSearchEvents.Label = "Search";
            this.toggleButtonSearchEvents.Name = "toggleButtonSearchEvents";
            this.toggleButtonSearchEvents.OfficeImageId = "ViewAppointmentInCalendar";
            this.toggleButtonSearchEvents.ShowImage = true;
            this.toggleButtonSearchEvents.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.toggleButtonSearchEvents_Click);
            // 
            // buttonEventManagement
            // 
            this.buttonEventManagement.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonEventManagement.Label = "Manage";
            this.buttonEventManagement.Name = "buttonEventManagement";
            this.buttonEventManagement.OfficeImageId = "SetLanguage";
            this.buttonEventManagement.ShowImage = true;
            this.buttonEventManagement.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonEventManagement_Click);
            // 
            // buttonVenues
            // 
            this.buttonVenues.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonVenues.Image = ((System.Drawing.Image)(resources.GetObject("buttonVenues.Image")));
            this.buttonVenues.Label = "Venues";
            this.buttonVenues.Name = "buttonVenues";
            this.buttonVenues.OfficeImageId = "ViewWebLayoutView";
            this.buttonVenues.ShowImage = true;
            this.buttonVenues.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonVenues_Click);
            // 
            // groupListings
            // 
            this.groupListings.Items.Add(this.buttonListofCustomers);
            this.groupListings.Items.Add(this.buttonFlightList);
            this.groupListings.Label = "Lists";
            this.groupListings.Name = "groupListings";
            // 
            // buttonListofCustomers
            // 
            this.buttonListofCustomers.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonListofCustomers.Label = "Customers";
            this.buttonListofCustomers.Name = "buttonListofCustomers";
            this.buttonListofCustomers.OfficeImageId = "AddressBook";
            this.buttonListofCustomers.ShowImage = true;
            this.buttonListofCustomers.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonListofCustomers_Click);
            // 
            // buttonFlightList
            // 
            this.buttonFlightList.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonFlightList.Image = global::Microsoft.SAPSK.ContosoTours.Properties.Resources.list_of_flights;
            this.buttonFlightList.Label = "Flights";
            this.buttonFlightList.Name = "buttonFlightList";
            this.buttonFlightList.OfficeImageId = "SetLanguage";
            this.buttonFlightList.ShowImage = true;
            this.buttonFlightList.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonFlightList_Click);
            // 
            // groupAnalysis
            // 
            this.groupAnalysis.Items.Add(this.buttonRevenueForecast);
            this.groupAnalysis.Items.Add(this.menuSales);
            this.groupAnalysis.Label = "Analysis";
            this.groupAnalysis.Name = "groupAnalysis";
            // 
            // buttonRevenueForecast
            // 
            this.buttonRevenueForecast.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonRevenueForecast.Label = "Revenue and Forecasts";
            this.buttonRevenueForecast.Name = "buttonRevenueForecast";
            this.buttonRevenueForecast.OfficeImageId = "ChartAreaChart";
            this.buttonRevenueForecast.ShowImage = true;
            this.buttonRevenueForecast.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonRevenueForecast_Click);
            // 
            // menuSales
            // 
            this.menuSales.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menuSales.Items.Add(this.buttonPackageSales);
            this.menuSales.Items.Add(this.buttonPackageType);
            this.menuSales.Items.Add(this.buttonTicket);
            this.menuSales.Label = "Sales and Distribution";
            this.menuSales.Name = "menuSales";
            this.menuSales.OfficeImageId = "ChartTypeOtherInsertGallery";
            this.menuSales.ShowImage = true;
            // 
            // buttonPackageSales
            // 
            this.buttonPackageSales.Label = "Package Sales";
            this.buttonPackageSales.Name = "buttonPackageSales";
            this.buttonPackageSales.ShowImage = true;
            this.buttonPackageSales.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonPackageSales_Click);
            // 
            // buttonPackageType
            // 
            this.buttonPackageType.Label = "Package Sales Per Promo Type";
            this.buttonPackageType.Name = "buttonPackageType";
            this.buttonPackageType.ShowImage = true;
            this.buttonPackageType.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonPackageType_Click);
            // 
            // buttonTicket
            // 
            this.buttonTicket.Label = "Ticket Sales";
            this.buttonTicket.Name = "buttonTicket";
            this.buttonTicket.ShowImage = true;
            this.buttonTicket.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.buttonTicket_Click);
            // 
            // RibbonExcel
            // 
            this.Name = "RibbonExcel";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonUIEventArgs>(this.RibbonExcel_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupSAP.ResumeLayout(false);
            this.groupSAP.PerformLayout();
            this.groupMode.ResumeLayout(false);
            this.groupMode.PerformLayout();
            this.groupPackages.ResumeLayout(false);
            this.groupPackages.PerformLayout();
            this.groupEvents.ResumeLayout(false);
            this.groupEvents.PerformLayout();
            this.groupListings.ResumeLayout(false);
            this.groupListings.PerformLayout();
            this.groupAnalysis.ResumeLayout(false);
            this.groupAnalysis.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSAP;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupMode;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSwitchMode;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupPackages;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonBuyPackage;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonPackageOutline;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonPackageManagement;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupEvents;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonSearchEvents;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonEventManagement;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonVenues;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupListings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonListofCustomers;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonFlightList;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupAnalysis;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRevenueForecast;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuSales;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonPackageSales;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonPackageType;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonTicket;
    }
}
