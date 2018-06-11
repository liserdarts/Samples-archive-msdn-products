
var Wingtip = window.Wingtip || {};
Wingtip.FlightStatus = Wingtip.FlightStatus || {};

$(document).ready(function () {

    var mapsKey = ""; //Obtained from http://www.bingmapsportal.com

    //Show processing message
    Wingtip.FlightStatus.waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose("Flight Status", "Retrieving Airport Codes...");

    //Wait one second for dialog to draw
    //then process request
    setTimeout(function () {

        $("#status").hide();
        $("#map").hide();
        $("#departureDate").datepicker();

        //Bind data to the display
        Wingtip.FlightStatus.AirportViewModel.load().then(
            function () {
                Wingtip.FlightStatus.waitDialog.close();
            },
            function (err) {
                alert(JSON.stringify(err));
            }
        );

    }, 1000);

    $("#submitFlight").click(function (e) {

        //Show processing message
        Wingtip.FlightStatus.waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose("Flight Status", "Retrieving Flight Info...");

        //Wait one second for dialog to draw
        //then process request
        setTimeout(function () {

            //hide display
            $("#status").hide();
            $("#map").hide();

            //Get submitted values
            var airportCode = $("#airportCode").val().toUpperCase();
            var carrierCode = $("#carrierCode").val().toUpperCase();
            var flightNumber = $("#flightNumber").val();
            var departureDate = new Date($("#departureDate").val());
            var departureYear = departureDate.getFullYear();
            var departureMonth = departureDate.getMonth() + 1;
            var departureDay = departureDate.getDate();

            //Validate
            if (carrierCode.length === 0 || flightNumber.length === 0 || departureDate === "Invalid Date") {
                alert("Invalid flight information!");
                return false;
            }

            //Get flight data
            Wingtip.FlightStatus.requestSuccessful = false;
            Wingtip.FlightStatus.Service.get_tracks(
                carrierCode,
                flightNumber,
                departureYear,
                departureMonth,
                departureDay).then(
                    function (data) {

                        var flightTracks = data.flightTracks;
                        Wingtip.FlightStatus.OperationsViewModel.clear();

                        for (var t = 0; t < flightTracks.length; t++) {

                            //Get the flight status for each segment
                            Wingtip.FlightStatus.OperationsViewModel.load(flightTracks[t].flightId);

                            //Only show map data for the departure airport requested
                            if (flightTracks[t].departureAirportFsCode === airportCode) {

                                //Get starting and ending points
                                if (flightTracks[t].waypoints.length > 0) {

                                    Wingtip.FlightStatus.requestSuccessful = true;
                                    var startLat = flightTracks[t].waypoints[0].lat;
                                    var startLon = flightTracks[t].waypoints[0].lon;

                                    //Show map
                                    var options = {
                                        credentials: mapsKey,
                                        center: new Microsoft.Maps.Location(
                                            startLat,
                                            startLon),
                                        mapTypeId: Microsoft.Maps.MapTypeId.road,
                                        zoom: 7
                                    };

                                    var map = new Microsoft.Maps.Map(document.getElementById("map"), options);

                                    //Add waypoints to map
                                    for (var w = 0; w < flightTracks[t].waypoints.length; w++) {

                                        //Get current position
                                        var lat2 = flightTracks[t].waypoints[w].lat;
                                        var lon2 = flightTracks[t].waypoints[w].lon;

                                        //Determine what icon to show based on bearing
                                        var bearing = flightTracks[t].bearing;

                                        if (w > 0) {
                                            var lat1 = flightTracks[t].waypoints[w - 1].lat;
                                            var lon1 = flightTracks[t].waypoints[w - 1].lon;
                                            var ypos = Math.sin((lon2 - lon1) * (3.14 / 180)) * Math.cos(lat2 * (3.14 / 180));
                                            var xpos = Math.cos(lat1 * (3.14 / 180)) * Math.sin(lat2 * (3.14 / 180)) - Math.sin(lat1 * (3.14 / 180)) * Math.cos(lat2 * (3.14 / 180)) * Math.cos((lon2 - lon1) * (3.14 / 180));
                                            bearing = Math.atan2(ypos, xpos) * (180 / 3.14);
                                        }

                                        var iconUrl = "../Images/NorthPin.ico";
                                        if (bearing >= 45 && bearing <= 135)
                                            iconUrl = "../Images/EastPin.ico"
                                        if (bearing > 135 && bearing <= 225)
                                            iconUrl = "../Images/SouthPin.ico"
                                        if (bearing > 225 && bearing <= 315)
                                            iconUrl = "../Images/WestPin.ico"

                                        //Add a pushpin
                                        var loc = new Microsoft.Maps.Location(lat2, lon2);
                                        var pushpin = new Microsoft.Maps.Pushpin(loc, { icon: iconUrl, width: 32, height: 32 });
                                        map.entities.push(pushpin);
                                    }
                                }
                            }
                        }

                        //Close the processing message
                        Wingtip.FlightStatus.waitDialog.close();

                        //Show display
                        if (Wingtip.FlightStatus.requestSuccessful === true) {
                            $("#status").show();
                            $("#map").show();
                        }
                        else {
                            alert("No data available");
                        }

                    },
                    function (err) {
                        alert(JSON.stringify(err));
                    }
            );

        }, 1000);

        return false;
    });

    function processRequest() {

    };

});

