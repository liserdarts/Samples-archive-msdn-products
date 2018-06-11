var hostWebUrl;
var appWebUrl;
var user;
var appContextSite;
var context;
var factory;
var web;
var listName;
var listDescription;


// This function is executed after the DOM is ready.
function initializeErrorHandler()
{
    context = SP.ClientContext.get_current();
    web = context.get_web();

    // Get the URI decoded URLs.
    hostWebUrl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));
    appWebUrl = decodeURIComponent(getQueryStringParameter("SPAppWebUrl"));

    // resources are in URLs in the form:
    // web_url/_layouts/15/resource.
    var scriptbase = hostWebUrl + "/_layouts/15/";

    // Load the js files and continue to the successHandler.
    $.getScript(scriptbase + "SP.Runtime.js",
        function () {
            $.getScript(scriptbase + "SP.js",
                function () { $.getScript(scriptbase + "SP.RequestExecutor.js", null); }
                );
        }
        );

    // Bind click event to button updateListDescription
    $("#updateListDescription").click(function (event) {
        updateListDescription();
        event.preventDefault();
    });
}

// This function  will be called when user clicks on update button
function updateListDescription() {


    listName = $("#listName").val();
    listDescription = $("#listDescription").val();

    if (listName == null || listName == undefined || listName == '') {
        alert("Please enter list name.");
        return;
    }

    if (listDescription == null || listDescription == undefined || listDescription == '') {
        alert("Please enter list description.");
        return;
    }

    // context: The ClientContext object provides access to
    //      the web and lists objects.
    // factory: Initialize the factory object with the
    //      app web URL.
    context = new SP.ClientContext(appWebUrl);
    factory = new SP.ProxyWebRequestExecutorFactory(appWebUrl);
    context.set_webRequestExecutorFactory(factory);
    appContextSite = new SP.AppContextSite(context, hostWebUrl);

    // Get host web
    web = appContextSite.get_web();
    context.load(web);
    context.executeQueryAsync(onSuccess, onError);
}

// This function will log error in ErrorLog list.
function onError(sender, args)
{
    //Create a new record
    var errorList = web.get_lists().getByTitle("ErrorLog");
    var listItemCreationInformation = new SP.ListItemCreationInformation();
    var listItem = errorList.addItem(listItemCreationInformation);
    listItem.set_item('Source', 'List Description Update App');
    listItem.set_item('ErrorMessage', args.get_message());
    listItem.update();
    context.load(listItem);
    context.executeQueryAsync(onSuccessLogError, onErrorLogError);
}

//function will update list description
function onSuccess()
{
    var list = web.get_lists().getByTitle(listName);
    list.set_description(listDescription);
    list.update();
    context.load(list);
    context.executeQueryAsync(onSuccessListDescriptionUpdate, onError);
}

// This is success event of log error into ErrorLog list. 
function onSuccessLogError() {
    alert("Failed to update list description. Please check ErrorLog list.");
}

// This is success event of list description updation
function onSuccessListDescriptionUpdate() {
    alert('List description updated successfully.');
}

// This is error event of log error into ErrorLog list. 
function onErrorLogError() {
    alert("Failed to log error in ErrorLog list.");
}

// function to retrieve a query string value.
function getQueryStringParameter(paramToRetrieve)
{
    // URLs for the appweb and the host web are passed as querystring parameters in the URL 
    //example: https://<URL of aspx page of app>?SPHostUrl=<URL of host web>&SPAppWebUrl=<Url of app web>
    
    var params = document.URL.split("?")[1].split("&");  //split url
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1)
    {
        var singleParam = params[i].split("="); //get parameter name
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1]; //return parameter value
    }
}






