"use strict";

var Wingtip = window.Wingtip || {};
Wingtip.FlightStatus = Wingtip.FlightStatus || {};

Wingtip.FlightStatus.OperationalTime = function (arrivalAirportCode, departureAirportCode, arrivalDate, departureDate) {

    var arrivalAirportFsCode = arrivalAirportCode,
        departureAirportFsCode = departureAirportCode,
        publishedArrival = arrivalDate,
        publishedDeparture = departureDate,

        getArrivalAirportCode = function () { return arrivalAirportFsCode; },
        setArrivalAirportCode = function (v) { arrivalAirportFsCode = v; },
        getDepartureAirportCode = function () { return departureAirportFsCode; },
        setDepartureAirportCode = function (v) { departureAirportFsCode = v; },
        getArrivalDate = function () { return publishedArrival; },
        setArrivalDate = function (v) { publishedArrival = v; },
        getDepartureDate = function () { return publishedDeparture; },
        setDepartureDate = function (v) { publishedDeparture = v; };

    return {
        get_arrivalAirportCode: getArrivalAirportCode,
        set_arrivalAirportCode: setArrivalAirportCode,
        get_departureAirportCode: getDepartureAirportCode,
        set_departureAirportCode: setDepartureAirportCode,
        get_arrivalDate: getArrivalDate,
        set_arrivalDate: setArrivalDate,
        get_departureDate: getDepartureDate,
        set_departureDate: setDepartureDate
};

};

Wingtip.FlightStatus.OperationsViewModel = function () {

    var operationalTimes = ko.observableArray(),
        getOperationalTimes = function () { return operationalTimes; },

        clear = function() {
            operationalTimes.removeAll();
        },
        
        load = function (flightId) {
            Wingtip.FlightStatus.Service.get_status(flightId).then(
                function (data) {
                    var flightStatus = data.flightStatus;

                    operationalTimes.push(
                        new Wingtip.FlightStatus.OperationalTime(
                            flightStatus.arrivalAirportFsCode,
                            flightStatus.departureAirportFsCode,
                            flightStatus.operationalTimes.publishedArrival.dateLocal.replace(":00.000",""),
                            flightStatus.operationalTimes.publishedDeparture.dateLocal.replace(":00.000", "")
                        ));

                },
                function (err) {
                    alert(JSON.stringify(err));
                }
            );
        };

    return {
        get_operationalTimes: getOperationalTimes,
        clear: clear,
        load: load
    };

}();

Wingtip.FlightStatus.Airport = function (code, title, location) {

    var airportCode = code,
        airportTitle = title,
        airportLocation = location,
        get_code = function () { return airportCode; },
        set_code = function (v) { airportCode = v; },
        get_title = function () { return airportTitle; },
        set_title = function (v) { airportTitle = v; },
        get_location = function () { return airportLocation; },
        set_location = function (v) { airportLocation = v; };

    return {
        get_code: get_code,
        set_code: set_code,
        get_title: get_title,
        set_title: set_title,
        get_location: get_location,
        set_location: set_location
    };
};

Wingtip.FlightStatus.AirportViewModel = function () {

    var airports = ko.observableArray(),
    get_airports = function () { return airports; },

    load = function () {

        var deferred = $.Deferred();

        Wingtip.FlightStatus.Service.get_airports().then(
            function (data) {
                
                var airportData = data.airports;

                airports.removeAll();

                //Fill the observable array
                for (var a = 0; a < airportData.length; a++) {
                    if (airportData[a].classification < 3) {
                        airports.push(
                            new Wingtip.FlightStatus.Airport(
                                airportData[a].fs,
                                airportData[a].name,
                                airportData[a].city + ", " + airportData[a].stateCode
                            ));
                    }
                }

                //Sort the observable array
                airports.sort(function (left, right) {
                    return left.get_code() === right.get_code() ? 0 : (left.get_code() < right.get_code() ? -1 : 1) 
                });

                //Bind
                ko.applyBindings(Wingtip.FlightStatus.OperationsViewModel, document.getElementById("statusTable"));
                ko.applyBindings(Wingtip.FlightStatus.AirportViewModel, document.getElementById("airportCode"));

                deferred.resolve();
            },
            function (err) {
                deferred.reject(err);
            }
        );

        return deferred.promise();
    };

    return {
        get_airports: get_airports,
        load: load
    };

}();

Wingtip.FlightStatus.Service = function () {

    var appId = "", //Obtained from https://developer.flightstats.com
        appKey = "", //Obtained from https://developer.flightstats.com
        rootUrl = "https://api.flightstats.com:443/flex",

        getAirports = function () {
            var url = rootUrl + "/airports/rest/v1/json/countryCode/US";
            return executeRequest(url);
        },

        getTracks = function (carrierCode, flightNumber, year, month, day) {
            var url = rootUrl + "/flightstatus/rest/v2/json/flight/tracks/" + carrierCode + "/" + flightNumber + "/dep/" + year + "/" + month + "/" + day + "?utc=false&maxPositions=6";
            return executeRequest(url);
        },

        getStatus = function (flightId) {
            var url = rootUrl + "/flightstatus/rest/v2/json/flight/status/" + flightId;
            return executeRequest(url);
        },

        executeRequest = function (url) {

            var deferred = $.Deferred();

            $.ajax({
                url: url,
                type: "GET",
                headers: {
                    "appId": appId,
                    "appKey": appKey
                },
                success: function (data) {
                    //jsonp requires a callback function
                    deferred.resolve(data);
                },
                error: function (err) {
                    deferred.reject(err);
                }
            });

            return deferred.promise();

        };

    return {
        get_airports: getAirports,
        get_tracks: getTracks,
        get_status: getStatus
    };

}();