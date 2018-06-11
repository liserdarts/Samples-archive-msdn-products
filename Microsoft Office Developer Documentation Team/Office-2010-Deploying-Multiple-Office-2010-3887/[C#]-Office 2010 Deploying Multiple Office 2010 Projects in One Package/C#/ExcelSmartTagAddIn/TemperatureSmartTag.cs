using System.Globalization;
using Excel = Microsoft.Office.Tools.Excel;
using System.Text.RegularExpressions;
using MSExcel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;

namespace ExcelSmartTagAddIn
{
  class TemperatureSmartTag : Excel.ISmartTagExtension
  {
    const string TEMPERATURE_CELSIUS = "C";
    Excel.Action temperatureAction = null;
    private Excel.SmartTag smartTagDemo = null;

    public TemperatureSmartTag()
    {
      this.smartTagDemo = Globals.Factory.CreateSmartTag(
        "http://www.microsoft.com#TemperatureSmartTag", "Temperature", this);

      // Add a regular expression for the Temperature SmartTag recognition.
      smartTagDemo.Expressions.Add(new Regex(
        "\\s*(?'number'[-+]?[0-9]+)°?\\s?(?'temperature'(F|C|f|c))\\s*"));
      // Create an instance of the temperature action.
      temperatureAction = Globals.Factory.CreateAction("Convert");
      // Add the action to the Actions array for the smart tag.
      smartTagDemo.Actions = new Excel.Action[] { temperatureAction };
      temperatureAction.BeforeCaptionShow += TemperatureAction_BeforeCaptionShow;
      temperatureAction.Click += TemperatureAction_Click;
    }

    public void TemperatureAction_BeforeCaptionShow(object sender, ActionEventArgs e)
    {
      string temperature = e.Properties.Read[
        "temperature"].ToUpper(CultureInfo.CurrentCulture);
      if (temperature == TEMPERATURE_CELSIUS)
      {
        ((Excel.Action)sender).Caption = "Convert to Fahrenheit";
      }
      else
      {
        ((Excel.Action)sender).Caption = "Convert to Celsius";
      }
    }

    public void TemperatureAction_Click(object sender, ActionEventArgs e)
    {
      double fahrenheit = 0;
      double celsius = 0;
      //Read the temperature type and value from the smart tag properties.
      string value = e.Properties.Read["number"];
      string temperature = e.Properties.Read[
        "temperature"].ToUpper(CultureInfo.CurrentCulture);
      string resultText = null;
      if (temperature == TEMPERATURE_CELSIUS)
      {
        celsius = System.Convert.ToDouble(value, 
          NumberFormatInfo.CurrentInfo);
        fahrenheit = System.Math.Round((celsius * 9 / 5.0) + 32);
        resultText = string.Format(" {0}° F ", 
          fahrenheit.ToString("0", NumberFormatInfo.CurrentInfo));
      }
      else
      {
        fahrenheit = System.Convert.ToDouble(value, 
          NumberFormatInfo.CurrentInfo);
        celsius = System.Math.Round((fahrenheit - 32) * 5 / 9.0);
        resultText = string.Format(" {0}° C ", 
          celsius.ToString("0", NumberFormatInfo.CurrentInfo));
      }
      e.Range.Value=resultText;
    }

    public void Recognize(string text, Microsoft.Office.Interop.SmartTag.ISmartTagRecognizerSite site, Microsoft.Office.Interop.SmartTag.ISmartTagTokenList tokenList, SmartTagRecognizeContext context)
    {
    }

    public object ExtensionBase
    {
      get { return smartTagDemo; }
    }

    public Excel.SmartTag Base
    {
      get { return smartTagDemo; }
    }
  }
}
