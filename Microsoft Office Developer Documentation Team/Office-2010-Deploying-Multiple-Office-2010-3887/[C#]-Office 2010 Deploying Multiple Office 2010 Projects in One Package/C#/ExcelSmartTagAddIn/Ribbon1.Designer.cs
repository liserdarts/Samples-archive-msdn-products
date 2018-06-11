namespace ExcelSmartTagAddIn
{
  partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public Ribbon1()
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
      Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
      Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
      this.TabCustom = this.Factory.CreateRibbonTab();
      this.temperatureGroup = this.Factory.CreateRibbonGroup();
      this.TemperatureEditBox = this.Factory.CreateRibbonEditBox();
      this.ScaleDropDown = this.Factory.CreateRibbonDropDown();
      this.InsertButton = this.Factory.CreateRibbonButton();
      this.TabCustom.SuspendLayout();
      this.temperatureGroup.SuspendLayout();
      // 
      // TabCustom
      // 
      this.TabCustom.Groups.Add(this.temperatureGroup);
      this.TabCustom.Label = "Custom";
      this.TabCustom.Name = "TabCustom";
      // 
      // temperatureGroup
      // 
      this.temperatureGroup.Items.Add(this.TemperatureEditBox);
      this.temperatureGroup.Items.Add(this.ScaleDropDown);
      this.temperatureGroup.Items.Add(this.InsertButton);
      this.temperatureGroup.Label = "Temperature";
      this.temperatureGroup.Name = "temperatureGroup";
      // 
      // TemperatureEditBox
      // 
      this.TemperatureEditBox.Label = "Temperature";
      this.TemperatureEditBox.Name = "TemperatureEditBox";
      this.TemperatureEditBox.Text = null;
      this.TemperatureEditBox.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.TemperatureEditBox_TextChanged);
      // 
      // ScaleDropDown
      // 
      ribbonDropDownItemImpl1.Label = "Centigrade";
      ribbonDropDownItemImpl2.Label = "Fahrenheit";
      this.ScaleDropDown.Items.Add(ribbonDropDownItemImpl1);
      this.ScaleDropDown.Items.Add(ribbonDropDownItemImpl2);
      this.ScaleDropDown.Label = "Scale:";
      this.ScaleDropDown.Name = "ScaleDropDown";
      // 
      // InsertButton
      // 
      this.InsertButton.Enabled = false;
      this.InsertButton.Label = "Insert Temperature";
      this.InsertButton.Name = "InsertButton";
      this.InsertButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.InsertButton_Click);
      // 
      // Ribbon1
      // 
      this.Name = "Ribbon1";
      this.RibbonType = "Microsoft.Excel.Workbook";
      this.Tabs.Add(this.TabCustom);
      this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
      this.TabCustom.ResumeLayout(false);
      this.TabCustom.PerformLayout();
      this.temperatureGroup.ResumeLayout(false);
      this.temperatureGroup.PerformLayout();

    }

    #endregion

    internal Microsoft.Office.Tools.Ribbon.RibbonTab TabCustom;
    internal Microsoft.Office.Tools.Ribbon.RibbonGroup temperatureGroup;
    internal Microsoft.Office.Tools.Ribbon.RibbonEditBox TemperatureEditBox;
    internal Microsoft.Office.Tools.Ribbon.RibbonDropDown ScaleDropDown;
    internal Microsoft.Office.Tools.Ribbon.RibbonButton InsertButton;

  }

  partial class ThisRibbonCollection
  {
    internal Ribbon1 Ribbon1
    {
      get { return this.GetRibbon<Ribbon1>(); }
    }
  }
}
