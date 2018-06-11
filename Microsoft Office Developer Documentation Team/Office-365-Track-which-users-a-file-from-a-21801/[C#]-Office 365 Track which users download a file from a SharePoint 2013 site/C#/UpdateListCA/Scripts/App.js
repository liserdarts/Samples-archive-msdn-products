var context;
var web;
var user;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {
    context = SP.ClientContext.get_current();
    web = context.get_web();

    getUserName();
    getItemDetails();
});

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

//This code will get the selected item of the documemnt library details
function getItemDetails() {

    appWebContext = new SP.ClientContext.get_current();

    hostWebContext = new SP.AppContextSite(appWebContext, decodeURIComponent($.getUrlVar("SPHostUrl")));
    username = hostWebContext.get_web().get_currentUser();
    appWebContext.load(username);
    selList = hostWebContext.get_web().get_lists().getById(decodeURIComponent($.getUrlVar("SPListId")));
    selListItem = selList.getItemById(decodeURIComponent($.getUrlVar("SPListItemId")));
    selListItemUrl = decodeURIComponent($.getUrlVar("SPListItemUrl"));
    appWebContext.load(selList);
    appWebContext.load(selListItem, 'Title');
    appWebContext.load(selListItem, 'FileLeafRef');
    var context = new SP.ClientContext.get_current();
    var oList = context.get_web().get_lists().getByTitle('MyList');
    var itemCreateInfo = new SP.ListItemCreationInformation();
    this.oListItem = oList.addItem(itemCreateInfo);
    appWebContext.executeQueryAsync(onGetListItemSucceeded, onFail);

}
//This code will update MyList with the file name of the document and the username
function onGetListItemSucceeded(sender, args) {
    var filename = selListItem.get_item('FileLeafRef');
    var name = filename.split(".");
    download(selListItemUrl);
    oListItem.set_item('FileName', name[0]);
    oListItem.set_item('UserName', username.get_title());
    oListItem.update();
    appWebContext.load(oListItem);
    appWebContext.executeQueryAsync(onQuerySucceeded, onQueryFailed
     );


}
function onQuerySucceeded() {
    //alert('updated');
    //alert("Successfully downloaded.")
}

function onQueryFailed(sender, args) {
    //alert('Submit failed, please try again! ' + args.get_message() + '\n' + args.get_stackTrace());
}
function onFail(sender, args) {
   // alert('Failed to execute the request. Error:' + args.get_message());
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
   // alert('Failed to get user name. Error:' + args.get_message());
}
function download(itemurl) {

    var url = itemurl;
    window.open(url, 'Download');
    //alert("Successfully downloaded.Click go back link to go back.");
    $("#message").text("Successfully downloaded. ");
}