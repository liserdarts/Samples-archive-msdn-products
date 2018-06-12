namespace WeatherReportMvcWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml;

    public class WeatherDataModel
    {
        public string Weatherlocationname { get; set; }
        public string Temperature { get; set; }
        public string Skycode { get; set; }
        public string Skytext { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public string Observationtime { get; set; }
        public string Observationpoint { get; set; }
        public string Feelslike { get; set; }
        public string Humidity { get; set; }
        public string Windspeed { get; set; }
        public string Winddisplay { get; set; }
        public List<ForecastWeatherdataModel> Forecast { get; set; }
    }

    public class ForecastWeatherdataModel
    {
        public string Low { get; set; }
        public string High { get; set; }
        public string Skycodeday { get; set; }
        public string Skytextday { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public string ShortDay { get; set; }
        public string Precip { get; set; }
    }

    public class Weatherdata
    {
        //msn weather service api uri
        private readonly string weatherSrvUri = "http://weather.service.msn.com/data.aspx?src=vista&weadegreetype=C&culture=en-US&wealocations={0}";
        private readonly string locationSrvUri = "http://weather.service.msn.com/find.aspx?outputview=search&weasearchstr={0}";

        //parse weather data from XML format
        public WeatherDataModel GetWeatherdata(string location)
        {
            XmlDocument xdocLocation = Common.getXMLDocumentFromXMLTemplate(string.Format(locationSrvUri, location));

            if (xdocLocation == null || xdocLocation.SelectNodes("//weather").Count == 0) return null;

            string locationCode = xdocLocation.SelectNodes("//weather")[0].Attributes["weatherlocationcode"].Value;

            XmlDocument xdocWeather = Common.getXMLDocumentFromXMLTemplate(string.Format(weatherSrvUri, locationCode));

            if (xdocWeather == null || xdocWeather.SelectNodes("//current").Count == 0) return null;

            XmlNodeList forecastWeatherData = xdocWeather.SelectNodes("//forecast");

            List<ForecastWeatherdataModel> forecastWeatherdataModels = new List<ForecastWeatherdataModel>();

            foreach (XmlNode forecast in forecastWeatherData)
            {
                forecastWeatherdataModels.Add(new ForecastWeatherdataModel
                {
                    Low = forecast.Attributes["low"].Value,
                    High = forecast.Attributes["high"].Value,
                    Skycodeday = forecast.Attributes["skycodeday"].Value,
                    Skytextday = forecast.Attributes["skytextday"].Value,
                    Date = forecast.Attributes["date"].Value,
                    Day = forecast.Attributes["day"].Value,
                    Precip = forecast.Attributes["precip"].Value,
                    ShortDay = forecast.Attributes["shortday"].Value
                });
            }

            XmlNode currentWeatherData = xdocWeather.SelectSingleNode("//weather/current");

            WeatherDataModel weatherDataModel = new WeatherDataModel
            {
                Weatherlocationname = xdocWeather.SelectSingleNode("//weather/@weatherlocationname").Value,
                Temperature = currentWeatherData.Attributes["temperature"].Value,
                Skycode = currentWeatherData.Attributes["skycode"].Value,
                Skytext = currentWeatherData.Attributes["skytext"].Value,
                Date = currentWeatherData.Attributes["date"].Value,
                Day = currentWeatherData.Attributes["day"].Value,
                Observationtime = currentWeatherData.Attributes["observationtime"].Value,
                Observationpoint = currentWeatherData.Attributes["observationpoint"].Value,
                Feelslike = currentWeatherData.Attributes["feelslike"].Value,
                Humidity = currentWeatherData.Attributes["humidity"].Value,
                Windspeed = currentWeatherData.Attributes["windspeed"].Value,
                Winddisplay = currentWeatherData.Attributes["winddisplay"].Value,
                Forecast = forecastWeatherdataModels
            };

            return weatherDataModel;
        }
    }
}