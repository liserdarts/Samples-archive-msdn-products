namespace ExchangeRateCalculatorMVCWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml;
    public class RateModel
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Rate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Ask { get; set; }
        public string Bid { get; set; }
        public double BaseValue { get; set; }
        public double TargetValue { get; set; }
    }

    public class ExchangeRateCalculator
    {
        //YQL API Uri
        private readonly string YahooFinanceUrl = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22{0}%22)&env=store://datatables.org/alltableswithkeys";
        public RateModel GetExchangeRate(string id, string baseValue)
        {
            XmlDocument xdoc = Common.getXMLDocumentFromXMLTemplate(string.Format(YahooFinanceUrl, id));
            if (xdoc != null)
            {
                var rateModel = new RateModel();
                rateModel.id = xdoc.SelectSingleNode("//rate/@id").Value;
                rateModel.Name = xdoc.SelectSingleNode("//Name").InnerText;
                rateModel.Rate = xdoc.SelectSingleNode("//Rate").InnerText;
                rateModel.Date = xdoc.SelectSingleNode("//Date").InnerText;
                rateModel.Time = xdoc.SelectSingleNode("//Time").InnerText;
                rateModel.Ask = xdoc.SelectSingleNode("//Ask").InnerText;
                rateModel.Bid = xdoc.SelectSingleNode("//Bid").InnerText;
                rateModel.BaseValue = Math.Round(Convert.ToDouble(baseValue), 2);
                rateModel.TargetValue = Math.Round(rateModel.BaseValue * Convert.ToDouble(rateModel.Rate), 2);
                return rateModel;
            }

            return null;
        }
    }
}