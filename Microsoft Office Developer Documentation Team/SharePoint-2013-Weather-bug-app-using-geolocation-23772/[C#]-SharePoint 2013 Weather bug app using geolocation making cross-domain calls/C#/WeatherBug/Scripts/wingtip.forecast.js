"use strict";

var Wingtip = window.Wingtip || {}

Wingtip.Forecast = function (period, iconUrl, title, description) {

    var p = period,
        u = iconUrl,
        t = title,
        d = description,

        get_period = function () { return p; },
        get_iconUrl = function () { return u; },
        get_title = function () { return t; },
        get_description = function () { return d; };

    return {
        get_period: get_period,
        get_iconUrl: get_iconUrl,
        get_title: get_title,
        get_description: get_description
    }
}
