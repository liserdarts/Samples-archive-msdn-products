namespace SalesOrderAddIn
{
    partial class SalesOrderRibbon : Microsoft.Office.Tools.Ribbon.OfficeRibbon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SalesOrderRibbon()
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
            this.tab1 = new Microsoft.Office.Tools.Ribbon.RibbonTab();
            this.group1 = new Microsoft.Office.Tools.Ribbon.RibbonGroup();
            this.box1 = new Microsoft.Office.Tools.Ribbon.RibbonBox();
            this.cmbSalesOrderNumbers = new Microsoft.Office.Tools.Ribbon.RibbonComboBox();
            this.txtOrderAmount = new Microsoft.Office.Tools.Ribbon.RibbonEditBox();
            this.separator1 = new Microsoft.Office.Tools.Ribbon.RibbonSeparator();
            this.UpdateLines = new Microsoft.Office.Tools.Ribbon.RibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.box1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.box1);
            this.group1.Items.Add(this.separator1);
            this.group1.Items.Add(this.UpdateLines);
            this.group1.Label = "Sales Orders";
            this.group1.Name = "group1";
            // 
            // box1
            // 
            this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box1.Items.Add(this.cmbSalesOrderNumbers);
            this.box1.Items.Add(this.txtOrderAmount);
            this.box1.Name = "box1";
            // 
            // cmbSalesOrderNumbers
            // 
            this.cmbSalesOrderNumbers.Label = "Order Number:";
            this.cmbSalesOrderNumbers.Name = "cmbSalesOrderNumbers";
            this.cmbSalesOrderNumbers.Text = null;
            this.cmbSalesOrderNumbers.TextChanged += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.UpdateSalesOrderDetails);
            // 
            // txtOrderAmount
            // 
            this.txtOrderAmount.Enabled = false;
            this.txtOrderAmount.Label = "Order Amount:";
            this.txtOrderAmount.Name = "txtOrderAmount";
            this.txtOrderAmount.Text = null;
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // UpdateLines
            // 
            this.UpdateLines.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.UpdateLines.Label = "Update Discount";
            this.UpdateLines.Name = "UpdateLines";
            this.UpdateLines.OfficeImageId = "FileSave";
            this.UpdateLines.ShowImage = true;
            this.UpdateLines.Click += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs>(this.UpdateLineItems);
            // 
            // SalesOrderRibbon
            // 
            this.Name = "SalesOrderRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new System.EventHandler<Microsoft.Office.Tools.Ribbon.RibbonUIEventArgs>(this.SalesOrderRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cmbSalesOrderNumbers;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton UpdateLines;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox txtOrderAmount;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
    }

    partial class ThisRibbonCollection : Microsoft.Office.Tools.Ribbon.RibbonReadOnlyCollection
    {
        internal SalesOrderRibbon SalesOrderRibbon
        {
            get { return this.GetRibbon<SalesOrderRibbon>(); }
        }
    }
}
