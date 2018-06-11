/// <reference path="../App.js" />
var _mailbox;
var _xhr;

(function () {
    "use strict";

    // The Office initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            _mailbox = Office.context.mailbox;
            _mailbox.getUserIdentityTokenAsync(getUserIdentityTokenCallback);
        });
    };

    function getUserIdentityTokenCallback(asyncResult) {
        var token = asyncResult.value;

        _xhr = new XMLHttpRequest();
        _xhr.open("POST", "https://localhost:44311/api/IdentityToken/");
        _xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        _xhr.onreadystatechange = readyStateChange;

        var request = new Object();
        request.token = token;

        _xhr.send(JSON.stringify(request));
    }

    function readyStateChange() {
        if (_xhr.readyState == 4 && _xhr.status == 200) {

            var response = JSON.parse(_xhr.responseText);

            if (undefined == response.errorMessage) {
                document.getElementById("msexchuid").value = response.token.msexchuid;
                document.getElementById("amurl").value = response.token.amurl;
                document.getElementById("uniqueID").value = response.token.uniqueID;
                document.getElementById("aud").value = response.token.aud;
                document.getElementById("iss").value = response.token.iss;
                document.getElementById("x5t").value = response.token.x5t;
                document.getElementById("nbf").value = response.token.nbf;
                document.getElementById("exp").value = response.token.exp;

                document.getElementById("rsp").value = _xhr.responseText;
                document.getElementById("error").value = "Complete.";
            }
            else {
                document.getElementById("error").value = response.error;
                app.showNotification("Error!", response.errorMessage);
            }
        }
    }
})();