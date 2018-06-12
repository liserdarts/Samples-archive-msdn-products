var BINGCreds = ""; //Enter your Bing API key here. See readme for details.
var latlong = [];
var locationsList = [];
var positions=[];
var locationsToGeocode = "";
var latLngList = '';
var map = null;
var pinInfobox = null; 

// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to a button
Office.initialize = function (reason) {
    $(document).ready(function () {
        // When getDataBtn is clicked, addresses are read from current selection of the document 
        //  and the Bing Maps API is used to geocode the addresses and then map them
        $('#getDataBtn').bind("click", function () {
            getData();
        });
    });
};

// This method reads addresses from current selection of the document 
// and uses Bing Maps API to geocode them, one after another
function getData() {
$('#mapDiv').empty();
    Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
    function (result) {
        if (result.status === 'succeeded') {
		   	var locations =  result.value;
		   	locationsList = locations.split("\n");
            // Call the following function to get the Latitude/Longitude values
		   	getLatLngLocations();
        }
    });
}

// This function actually performs the geocoding 
function getLatLngLocations() {
    latLngList = '';
    // The latitude and longitude of each of the addresses are stored in a global array for later use
    // in the showMap() function
    latlong = [];
    for (j = 0; j < locationsList.length; j++) {
        $.ajax({
            url: "https://dev.virtualearth.net/REST/v1/Locations",
            dataType: "jsonp",
            data: {
                key: BINGCreds,
                q: locationsList[j]
            },
            jsonp: "jsonp",
            success: function (result) {
                if (result &&
                 result.resourceSets &&
                 result.resourceSets.length > 0 &&
                 result.resourceSets[0].resources &&
                 result.resourceSets[0].resources.length > 0) {
                    latitude = result.resourceSets[0].resources[0].point.coordinates[0];
                    longitude = result.resourceSets[0].resources[0].point.coordinates[1]
                    latLngList = latitude + "," + longitude;
                    latlong.push(latLngList);
                }
                else {
                    latitude = "N/A";
                    longitude = "N/A";
                    latLngList = latitude + "," + longitude;
                    latlong.push(latLngList);
                }                
                showMap();
            }
        });
    }
}

function showMap() {
    $('#mapDiv').empty();
    map = null;
    if (latlong.length > 0) {
        console.log(latlong.length);
        map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
        {
            credentials: BINGCreds, height: 400, width: 400, mapTypeId: Microsoft.Maps.MapTypeId.road
        }
        );
        // Creates a collection to store multiple pins
        var pins = new Microsoft.Maps.EntityCollection();
        positions.length = 0;
        // Creates pins & determine zoom level to show all locations within map
        for (var j = 0; j < latlong.length; j++) {
            //the latitude and longitude which fetched as a comma seperated string are broken to be used as parameters
            var coords = latlong[j].split(",");
            positions[j] = new Microsoft.Maps.Location(coords[0], coords[1]);
            // Create a Pushpin
            var pin = new Microsoft.Maps.Pushpin(positions[j]);
            // Adds the pin to the collection instead of adding it directly to the Map
            pins.push(pin);
        }
        var viewBoundaries = Microsoft.Maps.LocationRect.fromLocations(positions);
        map.setView({ bounds: viewBoundaries });
        // Adds all pins at once
        map.entities.push(pins);
    }
}