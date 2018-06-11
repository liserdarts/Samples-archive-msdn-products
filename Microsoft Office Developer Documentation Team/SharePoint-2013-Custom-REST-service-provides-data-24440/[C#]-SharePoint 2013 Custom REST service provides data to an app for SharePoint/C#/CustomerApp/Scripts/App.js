'use strict';
var Wingtip = window.Wingtip || {};

jQuery(document).ready(function () {
    Wingtip.CustomerViewModel.load();
    ko.applyBindings(Wingtip.CustomerViewModel, document.getElementById("resultsTable"));
});
