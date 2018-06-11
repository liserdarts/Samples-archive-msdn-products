'use strict';

var Wingtip = window.Wingtip || {};

$(document).ready(function () {
    Wingtip.ViewModel.load();
    ko.applyBindings(Wingtip.ViewModel, document.getElementById("displayDiv"));
    $("#postButton").click(function (e) {
        new Wingtip.Social().post_message($("#postMessage").val()).then(
            function () {
                Wingtip.ViewModel.load();
            },
            function (sender, args) {
                alert(args.get_message());
            }
        );
        return false;
    });
});

