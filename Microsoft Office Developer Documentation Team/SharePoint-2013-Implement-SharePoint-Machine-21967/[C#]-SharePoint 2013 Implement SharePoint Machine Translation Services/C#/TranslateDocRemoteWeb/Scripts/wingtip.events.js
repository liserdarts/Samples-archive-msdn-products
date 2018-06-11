"use strict";

$(document).ready(function () {

    //Traps when a new language is selected
    //Posts the new setting to the controller for saving
    $("#languageList").change(function () {
        $("#messages").text("Saving...");
        $.ajax({
            url: $(this).data("url").split('?')[0],
            type: "POST",
            data: {
                appWebUrl: $(this).data("url").split('?')[1],
                title: "Culture",
                value: $(this).val()
            },
            success: function (data) {
                $("#messages").text(data.result);
            },
            error: function (err) {
                $("#messages").text(JSON.stringify(err));
            }
        });
    });

    //Traps when text box is updated
    $("#destinationFolder").change(function () {
        $("#messages").text("Saving...");
        $.ajax({
            url: $(this).data("url").split('?')[0],
            type: "POST",
            data: {
                appWebUrl: $(this).data("url").split('?')[1],
                title: "Destination",
                value: $(this).val()
            },
            success: function (data) {
                $("#messages").text(data.result);
            },
            error: function (err) {
                $("#messages").text(JSON.stringify(err));
            }
        });
    });

});