"use strict";

var Wingtip = window.Wingtip || {}

Wingtip.ContextInfo = function () {

    var formDigestTimeoutMilliseconds = 'undefined',
        isFormDigestValid = false,
        formDigestValue = 'undefined',
        siteFullUrl = 'undefined',
        webFullUrl = 'undefined',
        libraryVersion = 'undefined',

        get_formDigestTimeoutMilliseconds = function () {
            return formDigestTimeoutMilliseconds;
        },

        get_isFormDigestValid = function () {
            return isFormDigestValid;
        },

        get_formDigestValue = function () {
            return formDigestValue;
        },

        get_siteFullUrl = function () {
            return siteFullUrl;
        },

        get_webFullUrl = function () {
            return webFullUrl;
        },

        get_libraryVersion = function () {
            return libraryVersion;
        },

        get_qsHostUrl = function () {
            return queryString("SPHostUrl");
        },

        get_qsLanguage = function () {
            return queryString("SPLanguage");
        },

        get_qsClientTag = function () {
            return queryString("SPClientTag");
        },

        get_qsProductNumber = function () {
            return queryString("SPProductNumber");
        },

        get_qsAppWebUrl = function () {
            return queryString("SPAppWebUrl");
        },

        get_qsSenderId = function () {
            return queryString("senderId");
        },

        get_qsChromeControlString = function () {
            return "SPHostUrl=" + get_qsHostUrl() +
                   "&SPLanguage=" + get_qsLanguage() +
                   "&SPClientTag=" + get_qsClientTag() +
                   "&SPProductNumber=" + get_qsProductNumber() +
                   "&SPAppWebUrl=" + get_qsAppWebUrl();
        },

        init = function () {
            $.ajax({
                url: "http://" +
                        window.location.hostname +
                        "/html5/WeatherBug/_api/contextinfo",
                type: "POST",
                headers: {
                    "accept": "application/json;odata=verbose",
                },
                async: false,
                success: function (data) {

                    formDigestTimeoutMilliseconds = data.d.GetContextWebInformation.FormDigestTimeoutSeconds * 1000
                    formDigestValue = data.d.GetContextWebInformation.FormDigestValue
                    siteFullUrl = data.d.GetContextWebInformation.SiteFullUrl
                    webFullUrl = data.d.GetContextWebInformation.WebFullUrl
                    libraryVersion = data.d.GetContextWebInformation.LibraryVersion
                    isFormDigestValid = true;

                    setTimeout(function () {
                        isFormDigestValid = false;
                    }, formDigestTimeoutMilliseconds);

                },
                error: function (err) {
                    alert(JSON.stringify(err));
                }
            });
        },

        queryString = function (name) {
            try {
                var args = window.location.search.substring(1).split("&");
                var r = "";
                for (var i = 0; i < args.length; i++) {
                    var n = args[i].split("=");
                    if (n[0] == name)
                        r = n[1];
                }
                return r;
            }
            catch (err) {
                return 'undefined';
            }
        }

    return {
        init: init,
        get_formDigestTimeoutMilliseconds: get_formDigestTimeoutMilliseconds,
        get_isFormDigestValid: get_isFormDigestValid,
        get_formDigestValue: get_formDigestValue,
        get_siteFullUrl: get_siteFullUrl,
        get_webFullUrl: get_webFullUrl,
        get_libraryVersion: get_libraryVersion,
        get_qsHostUrl: get_qsHostUrl,
        get_qsLanguage: get_qsLanguage,
        get_qsClientTag: get_qsClientTag,
        get_qsProductNumber: get_qsProductNumber,
        get_qsAppWebUrl: get_qsAppWebUrl,
        get_qsSenderId: get_qsSenderId,
        get_qsChromeControlString: get_qsChromeControlString
    }
}();

