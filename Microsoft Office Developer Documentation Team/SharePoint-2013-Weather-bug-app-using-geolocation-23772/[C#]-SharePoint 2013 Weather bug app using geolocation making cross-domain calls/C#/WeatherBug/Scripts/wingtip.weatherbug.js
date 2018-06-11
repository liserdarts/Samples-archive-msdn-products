"use strict";

var Wingtip = window.Wingtip || {}

Wingtip.WeatherBug = function () {

    var forecasts = ko.observableArray(),

        get_forecasts = function () { return forecasts; },

        key = "",//Obtain a key from http://developer.weatherbug.com
        latitude,
        longitude,
        city,
        country,
        state,
        locationType,
        timeZoneLong,
        timeZoneShort,
        zip,

        get_latitude = function () {
            return latitude;
        },
        set_latitude = function (v) {
            latitude = v;
        },
        get_longitude = function () {
            return longitude;
        },
        set_longitude = function (v) {
            longitude = v;
        },
        get_city = function () {
            return city;
        },
        set_city = function (v) {
            city = v;
        },
        get_country = function () {
            return country;
        },
        set_country = function (v) {
            country = v;
        },
        get_state = function () {
            return state;
        },
        set_state = function (v) {
            state = v;
        },
        get_locationType = function () {
            return locationType;
        },
        set_locationType = function (v) {
            locationType = v;
        },
        get_timeZoneLong = function () {
            return timeZoneLong;
        },
        set_timeZoneLong = function (v) {
            timeZoneLong = v;
        },
        get_timeZoneShort = function () {
            return timeZoneShort;
        },
        set_timeZoneShort = function (v) {
            timeZoneShort = v;
        },
        get_zip = function () {
            return zip;
        },
        set_zip = function (v) {
            zip = v;
        },

        init = function (lat, long) {

            latitude = lat;
            longitude = long;

            var uri = "http://api.wunderground.com/api/" +
                      key + "/geolookup/q/" +
                      lat + "," + long + ".json";

            $.ajax({
                url: uri,
                dataType: "jsonp",
                success: function (data) {

                    city = data.location.city;
                    country = data.location.country;
                    state = data.location.state;
                    locationType = data.location.type;
                    timeZoneLong = data.location.tz_long;
                    timeZoneShort = data.location.tz_short;
                    zip = data.location.zip;

                    ko.applyBindings(Wingtip.WeatherBug, document.getElementById("weatherTableCaption"));

                    forecast10day();
                },
                error: function (err) {
                    alert("Error retrieving location information");
                }

            });
        },

        forecast10day = function () {

            var uri = "http://api.wunderground.com/api/" +
                       key + "/forecast10day/q/" +
                       state + "/" + city.replace(/ /g, "_") + ".json";

            $.ajax({
                url: uri,
                dataType: "jsonp",
                success: function (data) {

                    var forecastData = data.forecast.txt_forecast.forecastday;

                    for (var p = 0; p < 10; p++) {
                        forecasts.push(new Wingtip.Forecast(
                            forecastData[p].period,
                            forecastData[p].icon_url,
                            forecastData[p].title,
                            forecastData[p].fcttext));
                    }

                    ko.applyBindings(Wingtip.WeatherBug, document.getElementById("weatherTableBody"));

                },
                error: function (err) {
                    alert("Error retrieving 10-day forecast");
                }

            });
        }

    return {
        init: init,
        get_latitude: get_latitude,
        set_latitude: set_latitude,
        get_longitude: get_longitude,
        set_longitude: set_longitude,
        get_city: get_city,
        set_city: set_city,
        get_country: get_country,
        set_country: set_country,
        get_state: get_state,
        set_state: set_state,
        get_locationType: get_locationType,
        set_locationType: set_locationType,
        get_timeZoneLong: get_timeZoneLong,
        set_timeZoneLong: set_timeZoneLong,
        get_timeZoneShort: get_timeZoneShort,
        set_timeZoneShort: set_timeZoneShort,
        get_zip: get_zip,
        set_zip: set_zip,
        get_forecasts: get_forecasts
    }

}();
