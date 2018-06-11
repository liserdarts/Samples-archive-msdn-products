"use strict";

var Wingtip = window.Wingtip || {}

Wingtip.Geolocation = function () {

    var status = 'Not initialized',
        isValid = false,
        latitude = 'undefined',
        longitude = 'undefined',

    init = function () {
        if (typeof navigator.geolocation === 'undefined') {
            isValid = false;
            latitude = 'undefined';
            longitude = 'undefined';
            status = 'Not enabled';
        }
        else {
            navigator.geolocation.getCurrentPosition(positionSuccess, positionFailure);
        }
    },

    positionSuccess = function (position) {
        isValid = true;
        latitude = position.coords.latitude;
        longitude = position.coords.longitude;
        status = 'Success';

        Wingtip.WeatherBug.init(
            Wingtip.Geolocation.get_latitude(),
            Wingtip.Geolocation.get_longitude());


    },

    positionFailure = function (error) {
        isValid = false;
        latitude = 'undefined';
        longitude = 'undefined';
        switch (error.code) {
            case 1:
                status = 'Permission denied.';
                break;
            case 2:
                status = 'Position unavailable';
                break;
            case 3:
                status = 'Operation timed out';
                break;
            case 4:
                status = 'Unknown error';
                break;
        }

        Wingtip.WeatherBug.init("47.6063889", "-122.3308333");
    },

    get_isValid = function () {
        return isValid;
    },

    get_status = function () {
        return status;
    },

    get_latitude = function () {
        return latitude;
    },

    get_longitude = function () {
        return longitude;
    }

    return {
        init: init,
        get_isValid: get_isValid,
        get_status: get_status,
        get_latitude: get_latitude,
        get_longitude: get_longitude
    }

}();

