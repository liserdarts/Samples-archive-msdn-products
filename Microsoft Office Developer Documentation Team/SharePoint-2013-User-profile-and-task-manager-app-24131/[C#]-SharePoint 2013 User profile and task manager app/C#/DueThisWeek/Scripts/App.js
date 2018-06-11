'use strict';
var Wingtip = window.Wingtip || {};

$(document).ready(function () {
    Wingtip.ViewModel.load().then(
        function () {
            ko.applyBindings(Wingtip.ViewModel, document.getElementById("tasksDisplay"));
            $("#test").accordion();
        },
        function (errorMessage) {
            alert(errorMessage);
        }
    );
});

