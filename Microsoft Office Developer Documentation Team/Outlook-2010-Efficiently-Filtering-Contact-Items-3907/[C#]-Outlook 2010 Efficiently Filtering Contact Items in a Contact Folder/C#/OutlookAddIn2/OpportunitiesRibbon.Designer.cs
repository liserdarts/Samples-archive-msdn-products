namespace OutlookAddIn2
{
    partial class OpportunitiesRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public OpportunitiesRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.myAddInTab = this.Factory.CreateRibbonTab();
            this.importExportGroup = this.Factory.CreateRibbonGroup();
            this.importDataButton = this.Factory.CreateRibbonButton();
            this.exportButton = this.Factory.CreateRibbonButton();
            this.reportsGroup = this.Factory.CreateRibbonGroup();
            this.getReportButton = this.Factory.CreateRibbonButton();
            this.myAddInTab.SuspendLayout();
            this.importExportGroup.SuspendLayout();
            this.reportsGroup.SuspendLayout();
            // 
            // myAddInTab
            // 
            this.myAddInTab.Groups.Add(this.importExportGroup);
            this.myAddInTab.Groups.Add(this.reportsGroup);
            this.myAddInTab.Label = "My Add-in";
            this.myAddInTab.Name = "myAddInTab";
            // 
            // importExportGroup
            // 
            this.importExportGroup.Items.Add(this.importDataButton);
            this.importExportGroup.Items.Add(this.exportButton);
            this.importExportGroup.Label = "Import/Export";
            this.importExportGroup.Name = "importExportGroup";
            // 
            // importDataButton
            // 
            this.importDataButton.Label = "Import Data";
            this.importDataButton.Name = "importDataButton";
            this.importDataButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.importDataButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Label = "Export Data";
            this.exportButton.Name = "exportButton";
            this.exportButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.exportDataButton_Click);
            // 
            // reportsGroup
            // 
            this.reportsGroup.Items.Add(this.getReportButton);
            this.reportsGroup.Label = "Reports";
            this.reportsGroup.Name = "reportsGroup";
            // 
            // getReportButton
            // 
            this.getReportButton.Label = "Get Report";
            this.getReportButton.Name = "getReportButton";
            this.getReportButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.getReportButton_Click);
            // 
            // OpportunitiesRibbon
            // 
            this.Name = "OpportunitiesRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.myAddInTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.myAddInTab.ResumeLayout(false);
            this.myAddInTab.PerformLayout();
            this.importExportGroup.ResumeLayout(false);
            this.importExportGroup.PerformLayout();
            this.reportsGroup.ResumeLayout(false);
            this.reportsGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab myAddInTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup importExportGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton importDataButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton exportButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup reportsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton getReportButton;

    }

    partial class ThisRibbonCollection
    {
        internal OpportunitiesRibbon Ribbon1
        {
            get { return this.GetRibbon<OpportunitiesRibbon>(); }
        }
    }
}
