var context;
var web;
var user;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {
    context = SP.ClientContext.get_current();
    web = context.get_web();

    getUserName();

    $("#SearchKey").click(function () {
        
        var searchKey = "";
        searchKey = $("#txtSearchKey").val();

        if (searchKey == "") {
            alert("Please enter search key");
            return;
        }
        var searchUrl = 'https://search.twitter.com/search.json?q=' + searchKey;
        $.support.cors = true;
        $.ajax({
            url: searchUrl,
            type: "GET",
            dataType: "jsonp",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                GetFeeds(result);
            },
            error: function (error) {
                alert("No twitter feeds found");
            }
        });
    });
});

//This function is used to iterate the JSON data
function GetFeeds(result) {
    try {
       
        var htmlForUl = "";
        $(result.results).each(function () {
            var renderHtml = '<li><span>' + $(this)[0].text + '<span></li>';
            htmlForUl += renderHtml
        });
        $("#ulTwitterFeeds").html(htmlForUl);

    }
    catch (e) { }
}

// This function prepares, loads, and then executes a SharePoint query to get the current users information
function getUserName() {
    user = web.get_currentUser();
    context.load(user);
    context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
}

// This function is executed if the above OM call is successful
// It replaces the contents of the 'helloString' element with the user name
function onGetUserNameSuccess() {
    $('#message').text('Hello ' + user.get_title());
}

// This function is executed if the above call fails
function onGetUserNameFail(sender, args) {
    alert('Failed to get user name. Error:' + args.get_message());
}
