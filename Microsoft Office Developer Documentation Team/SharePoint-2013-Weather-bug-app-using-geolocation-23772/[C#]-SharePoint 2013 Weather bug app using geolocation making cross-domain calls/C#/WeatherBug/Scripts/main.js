"use strict";

var Wingtip = window.Wingtip || {}

$(document).ready(function () {

    Wingtip.ContextInfo.init();
    Wingtip.ChromeControl.render();
    Wingtip.Geolocation.init();

});