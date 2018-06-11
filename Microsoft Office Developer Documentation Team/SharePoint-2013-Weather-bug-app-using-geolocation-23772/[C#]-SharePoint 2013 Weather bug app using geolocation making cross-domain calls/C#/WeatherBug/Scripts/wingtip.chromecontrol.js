"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.ChromeControl = function () {

     var render = function () {
        var options = {
            "appIconUrl": "../Images/AppIcon.png",
            "appTitle": "My Weather",
            "appHelpPageUrl": "../Help",
            "settingsLinks": [
                {
                    "linkUrl": "Default.html?" + Wingtip.ContextInfo.get_qsChromeControlString(),
                    "displayName": "Home"
                }
            ]
            };

        var libraryUrl = "http://" +
                   window.location.hostname + "/html5/WeatherBug/_layouts/15/sp.ui.controls.js";

        $.getScript(libraryUrl, function () {
            var nav = new SP.UI.Controls.Navigation(
                                    "chrome_ctrl_placeholder_id",
                                    options
                              );
            nav.setVisible(true);
        });
    },

    getQueryStringParameter = function (p) {
        var params =
            document.URL.split("?")[1].split("&");
        var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == p)
                return singleParam[1];
        }
    }

    return {
        render: render
    }
}();

