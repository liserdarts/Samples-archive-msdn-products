"use strict";

window.Wingtip = window.Wingtip || {};

Wingtip.ChromeControl = function () {

    var init = function () {

        var hostWebUrl = queryString("SPHostUrl");
        $.getScript(hostWebUrl + "/_layouts/15/SP.UI.Controls.js", render);

    },

    render = function () {
        var options = {
            "appIconUrl": "../Images/AppIcon.png",
            "appTitle": "App Title",
            "appHelpPageUrl": "../Help",
            "settingsLinks": [
                {
                    "linkUrl": "../Page1/Method1?" + document.URL.split("?")[1],
                    "displayName": "Page1"
                },
                {
                    "linkUrl": "../Page2/Method1?" + document.URL.split("?")[1],
                    "displayName": "Page2"
                }
            ]
        };

        var nav = new SP.UI.Controls.Navigation(
                                "chrome_ctrl_placeholder_id",
                                options
                          );
        nav.setVisible(true);
    },

    queryString = function (p) {
        var params =
            document.URL.split("?")[1].split("&");
        var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == p)
                return decodeURIComponent(singleParam[1]);
        }
    }

    return {
        init: init,
    }
}();

$(document).ready(function () {
    Wingtip.ChromeControl.init();
});