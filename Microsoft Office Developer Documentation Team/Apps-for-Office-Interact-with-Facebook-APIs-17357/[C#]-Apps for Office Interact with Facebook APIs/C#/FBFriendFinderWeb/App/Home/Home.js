/// <reference path="../App.js" />

(function () {
    "use strict";

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();
            $('#display-data').click(GetFriends);
        });
    };

   function GetFriends() {
        FB.api('/me/friends', function (response) {
            var myFriendCount;
            myFriendCount = response.data.length;
            DisplayUserInfo(myFriendCount);
        })
    }

    function DisplayUserInfo(friendNum) {

        var myName;
        var myGender;
        var myID;
        var myBDay;
        var myFCount = friendNum;

        FB.api('/me', function (me) {

            if (me.name) {

                myName = me.name;
                myGender = me.gender;
                myID = me.username;
                myBDay = me.birthday;
            }

            Office.context.document.setSelectedDataAsync([
            ["Name: ", myName],
            ["Gender: ", myGender],
            ["User ID: ", myID],
            ["Birthdate: ", myBDay],
            ["Friend Count: ", myFCount]],
            { coercionType: "matrix" });

        })

    }
})();