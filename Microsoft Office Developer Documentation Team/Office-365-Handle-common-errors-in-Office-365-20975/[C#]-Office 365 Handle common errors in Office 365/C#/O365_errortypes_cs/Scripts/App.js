var hostWebUrl;
var appWebUrl;
var appContextSite;
var context;
var factory;
var web;

// This function is executed after the DOM is ready. This will load the javascript files. 
function initializeApp()
{
    // Get the URI decoded URLs.
    hostWebUrl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));
    appWebUrl = decodeURIComponent(getQueryStringParameter("SPAppWebUrl"));

    // The js files are in a URL in the form: web_url/_layouts/15/resource_file
    var scriptbase = hostWebUrl + "/_layouts/15/";

    // Load the js files.
    $.getScript(scriptbase + "SP.Runtime.js",
        function () {
            $.getScript(scriptbase + "SP.js",
                function () { $.getScript(scriptbase + "SP.RequestExecutor.js", null); }
                );
        });


    
    // Bind the click event of button createList.
    $("#createList").click(function (event) {
        createList();
        event.preventDefault();
    });


    // Bind the click event of button getList.
    $("#getList").click(function (event) {
        getList();
        event.preventDefault();
    });

    // Bind the click event of button insertListItem.
    $("#insertListItem").click(function (event) {
        insertItemToList();
        event.preventDefault();
    });


    // Bind the click event of button getItemById.
    $("#getItemById").click(function (event) {
        getItemByID();
        event.preventDefault();
    });

}

//  This function retrieves a query string value.
function getQueryStringParameter(paramToRetrieve) {
    //  URLs for the appweb and the host web are passed as querystring parameters in the URL. 
    //  example: https://<URL of aspx page of app>?SPHostUrl=<URL of host web>&SPAppWebUrl=<Url of app web>
    var params = document.URL.split("?")[1].split("&");  //split url
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("="); //get parameter name
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1]; //return parameter value
    }
}

// This function will load the appcontext to the host website. 
function loadAppContext()
{
    // context: The ClientContext object provides access to
    //      the web and lists objects.
    // factory: Initialize the factory object with the
    //      app web URL.
    context = new SP.ClientContext(appWebUrl);
    factory = new SP.ProxyWebRequestExecutorFactory(appWebUrl);
    context.set_webRequestExecutorFactory(factory);
    appContextSite = new SP.AppContextSite(context, hostWebUrl);
}

// This function tries to create a list on the host website.
function createList() {

    loadAppContext();
    var listName = "Announcements";

    // Get host website
    web = appContextSite.get_web();
    context.load(web);

    // App doesn't have permission to create a list, so the 
    // System.UnauthorizedAccessException exception will be thrown
    var listCreationInfo = new SP.ListCreationInformation();
    listCreationInfo.set_title(listName);
    listCreationInfo.set_templateType(SP.ListTemplateType.announcements);
    var announcementsList = web.get_lists().add(listCreationInfo);
    context.load(announcementsList);
    context.executeQueryAsync(onSuccessListCreation, onError);

}

// This function is called if the list creation is successful.
function onSuccessListCreation() {
    alert('List created successfully.');
}

// This function handles an error.
function onError(sender, args) {
    alert('Exception type: ' + args.get_errorTypeName() +
        '\n' + 'Error message: ' + args.get_message() +
        '\n' + 'Stack trace: ' + args.get_stackTrace() +
        '\n' + 'Error trace CorrelationId: ' + args.get_errorTraceCorrelationId() +
        '\n' + 'Error details: ' + args.get_errorDetails() +
        '\n' + 'Error code: ' + args.get_errorCode()
       );
}

// This function tries to insert an item into the list.
function insertItemToList()
{
    loadAppContext();
    web = appContextSite.get_web();
    context.load(web);
    var list = web.get_lists().getByTitle('Tasks');
    var listItemCreationInfo = new SP.ListItemCreationInformation();
    var newItem = list.addItem(listItemCreationInfo);

    //  User is trying to set AssignedTo field value to a user that doesn't exist,
    //  so the Microsoft.SharePoint.SPException exception will be thrown
    newItem.set_item('Title', "Document Approval Task");
    newItem.set_item('AssignedTo', 'testUser');
    newItem.update();
    context.executeQueryAsync(onSuccessInsertItem, onError);

}

// This function is called if the item is inserted in the list.
function onSuccessInsertItem() {
    alert('Item inserted successfully.');
}

// This function tries to access the Item with ID = 1000, which does not exist. 
function getItemByID()
{
    var executor;

    // Initialize the RequestExecutor with the app web URL.
    executor = new SP.RequestExecutor(appWebUrl);

    var restUrl = appWebUrl +
                "/_api/SP.AppContextSite(@target)/web/lists/getByTitle('Tasks')/items(1000)?@target='" +
                hostWebUrl + "'"

    // Issue the call against the host website.
    // To get the title using REST we can hit the endpoint:
    //      app_web_url/_api/SP.AppContextSite(@target)/web/lists/getByTitle('ListName')/items()?@target='siteUrl'
    // The response formats the data in the JSON format.
    // The functions successHandler and errorHandler are for the
    //      success and error events respectively.
    executor.executeAsync(
        {
            url: restUrl,
            method: "GET",
            headers: { "Accept": "application/json; odata=verbose" },
            success: onSuccessGetListItem,
            error: onErrorGetListItem
        }
    );
}

// Function to handle the error event of the getItemByID function.
function onErrorGetListItem(data, errorCode, errorMessage)
{

    var exceptionType; 
    var errorCodefrombody; 
    //  In the case of errors or exceptions in rest call,
    //  the body of the response contains the same error information 
    var jsonObject = JSON.parse(data.body);

    //  Get error message 
    var message = jsonObject.error.message.value;

    //  Get error Type 
    var errorcodefrombody = jsonObject.error.code;
    //it is in format of 'code, exceptiontype' 
    var codeArray = errorcodefrombody.split(',');
    if (codeArray != undefined)
    {
        exceptionType = codeArray[1];
        errorCodefrombody = codeArray[0]; 
    }

    alert('Exception type: ' + exceptionType +
         '\n' + 'HTTP status code: ' + data.statusCode +
         '\n' + 'Error message: ' + errorMessage +
         '\n' + 'Error code: ' + errorCode +
         '\n' + 'Exception detail: ' + message);
}

// This handles the success event of the getItemByID function.
function onSuccessGetListItem(data)
{
    var jsonObject = JSON.parse(data.body);
    alert("Title: " + jsonObject.d.Title);
}

// This gets the list from the host website.
function getList() {

    loadAppContext();
    var listName = "Announcements";
    web = appContextSite.get_web();
    context.load(web);

    //  Tries to access a list that not exist, so the
    //  System.ArgumentException exception will be thrown
    var list = web.get_lists().getByTitle(listName);
    context.load(list);
    context.executeQueryAsync(onSuccessGetList, onError);

}

// This handles the success event of the getList function.
function onSuccessGetList()
{
    alert('List fonund.');
}

