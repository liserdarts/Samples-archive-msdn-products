"use strict";
var WingtipToys = window.WingtipToys || {};

$(document).ready(function () {
    //Check to see if the list exists
    WingtipToys.Lists.read("FAQ")
    .then(function (list) {

        //List exists, show search interface
        $("#mainDiv").show();

        //Bind data
        WingtipToys.ViewModel.loadByQuery("FAQ", "");
        ko.applyBindings(WingtipToys.ViewModel, document.getElementById("resultsTable"));

        //Resize App part Window
        var params = document.URL.split("?")[1].split("&");
        var senderid = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0].toLowerCase() === "senderid") {
                senderid = decodeURIComponent(singleParam[1]);
            }       
        }

        //Be sure to resize in 30px increments
        window.parent.postMessage("<message senderId=" + senderid + ">resize(600,600)</message>","*");

   },
    function (sender, args) {
        //List does not exist, app must be configured
        $("#mainDiv").hide();
        alert("App has not been properly configured. Please launch the app to complete configuration.");
    });

    //Bind Events
    $("#submitQuestion").click(function () {
        var freetext = $("#questionText").attr("value");
        WingtipToys.ViewModel.loadByQuery("FAQ", freetext);
    });


});

