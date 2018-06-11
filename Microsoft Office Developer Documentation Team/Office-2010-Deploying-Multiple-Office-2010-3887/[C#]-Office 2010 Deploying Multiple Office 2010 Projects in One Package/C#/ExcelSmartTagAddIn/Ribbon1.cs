using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace ExcelSmartTagAddIn
{
  public partial class Ribbon1
  {
    private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
    {

    }

    private void TemperatureEditBox_TextChanged(object sender, RibbonControlEventArgs e)
    {
      InsertButton.Enabled = (TemperatureEditBox.Text.Length > 0);
    }

    private void InsertButton_Click(object sender, RibbonControlEventArgs e)
    {
      String temp = TemperatureEditBox.Text;
      switch (ScaleDropDown.SelectedItem.Label)
      {
        case "Fahrenheit":
          temp += "°F";
          break;

        case "Centigrade":
          temp += "°C";
          break;
      }
      var cell = Globals.ThisAddIn.Application.ActiveCell;
      cell.Value = temp;
    }

  }
}
