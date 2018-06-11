var yammerHelper = {
    checkLoginStatus: function (callback) {
        yam.getLoginStatus(function (response) {
            if (response.authResponse) {
                alert("Already Logged In");
                callback();
            }
            else {
                yammerHelper.login(callback);
            }
        });
    },
    logout: function () {
        yam.logout(function (response) { });
    },
    postToActivityFeed: function () {
        var testFeed = {
            "activity": {
                "actor": { "name": "Alex Darrow", "email": "alex@contoso.com" },
                "action": "create",
                "object": {
                    "url": location.href
                },
                "private": "false",
                "message": "Yammer Integration with O365."
            }
        };

        yam.request({
            url: "/api/v1/activity.json",
            method: "POST",
            data: testFeed,
            success: function (msg) {
                alert("Activity Feed Posted Successfully");
            },
            error: function (msg) {
                alert("Activity Feed Posting Error: " + msg);
            }
        });
    },
    postAMessage: function () {
        var testMessage = {
            "body": "Hey ! have you seen this link: " + location.href
        };

        yam.request({
            url: "/api/v1/messages.json",
            method: "POST",
            data: testMessage,
            success: function (msg) {
                alert("Message Posted Successfully");
            },
            error: function (msg) {
                alert("Message Posting Error: " + msg);
            }
        });
    },
    login: function (callback) {
        yam.login(function (response) {
            if (response.authResponse) {
                alert("Login Successfully");
                callback();
            }
            else {
                alert(response.authResponse);
            }
        });
    },
    loginAndPostMessage: function () {
        yammerHelper.checkLoginStatus(function () {
            yammerHelper.postAMessage();
        });
    }
}