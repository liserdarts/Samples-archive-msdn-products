"use strict";

window.Wingtip = window.Wingtip || {};

Wingtip.PartialViewSupport = function () {

    //Attaches URL data to support calling back for partial view
    var attachData = function () {
        $("#searchButton").data("callbackUrl", { controller: window.location.href.split('?')[0], hostWebUrl: queryString("SPHostUrl") });
    },

    //Returns the partial view callback url
    get_callbackUrl = function () {
        return $("#searchButton").data("callbackUrl").controller + "?&SPHostUrl=" + $("#searchButton").data("callbackUrl").hostWebUrl + "&keyword=" + $("#searchText").val();
    },

    get_questionUrl = function (siteRelativeUrl) {
        return $("#searchButton").data("callbackUrl").controller + "/Questions" + "?&SPHostUrl=" + $("#searchButton").data("callbackUrl").hostWebUrl + "&siteRelativeUrl=" + siteRelativeUrl;
    },

    //Parase query string
    queryString = function (name) {
        try {
            var args = window.location.search.substring(1).split("&");
            var r = "";
            for (var i = 0; i < args.length; i++) {
                var n = args[i].split("=");
                if (n[0] == name)
                    r = decodeURIComponent(n[1]);
            }
            return r;
        }
        catch (err) {
            return 'undefined';
        }
    };

    return {
        attachdata: attachData,
        get_callbackUrl: get_callbackUrl,
        get_questionUrl: get_questionUrl
    };

}();


$(document).ready(function () {
    Wingtip.PartialViewSupport.attachdata();
});

//Handles a search request
$(document).on("click", "#searchButton", function (evt) {
    evt.stopPropagation();
    $.ajax({
        url: Wingtip.PartialViewSupport.get_callbackUrl(),
        type: "GET",
        success: function (data) {
            $("#mainBody").html(data);
            Wingtip.PartialViewSupport.attachdata();
        }
    });
});

//Handles a "Show Activity" link
$(document).on("click", "[id^='activityLink']", function (evt) {
    evt.stopPropagation();
    var index = $(this).attr("id").substring(12);
    var path = $("#siteLink" + index).attr("data-siteRelativeUrl");
    $.ajax({
        url: Wingtip.PartialViewSupport.get_questionUrl(path),
        type: "GET",
        success: function (data) {
            $("#activityList").html(data);
            $("#activityDialog").css("visibility", "visible");
            $("#activityDialog").draggable();
        }
    });
});

//Closes activity dialog
$(document).on("click", "#closeActivityLink", function (evt) {
    evt.stopPropagation();
    $("#activityDialog").css("visibility", "hidden");
});

//Enter key
$(document).keypress(function (evt) {
    if (evt.which === 13) {
        $("#searchButton").click();
    }
});
