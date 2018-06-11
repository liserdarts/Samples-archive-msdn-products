var context;
var web;
var user;

// This function is executed after the DOM is ready and SharePoint scripts are loaded
// Place any code you want to run when Default.aspx is loaded in this function
// The code creates a context object which is needed to use the SharePoint object model
function sharePointReady() {
    context = new SP.ClientContext.get_current();
    web = context.get_web();

    getUserName();
   // GetLists();
}

// This function prepares, loads, and then executes a SharePoint query to get the current users information
function getUserName() {
    user = web.get_currentUser();
    context.load(user);
    context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
}

// This function is executed if the above OM call is successful
// It replaces the content of the 'welcome' element with the user name
function onGetUserNameSuccess() {
    $('#message').text('Hello ' + user.get_title());
}

// This function is executed if the above OM call fails
function onGetUserNameFail(sender, args) {
    alert('Failed to get user name. Error:' + args.get_message());
}
//function GetLists() {
//    alert("i am in ");
//    var context = new SP.ClientContext.get_current();
//    var web = context.get_web();
//    this.lists = web.get_lists();
//    context.load(lists);
//    context.executeQueryAsync(Function.createDelegate(this, this.IsListsExists), Function.createDelegate(this, this.onQueryFaild));
//}

////Check whether the lists already exists or not?  
//function IsListsExists() {

//    var isListAvail = false;
//    var listEnumerator = lists.getEnumerator();
//    while (listEnumerator.moveNext()) {
//        list = listEnumerator.get_current();
//        if (list.get_title() == 'Project Documents') {
//            isListAvail = true;
//        }
//    }
//    alert(isListAvail);
//    //If list not exists, create the list  
//    if (!isListAvail) {

//        createList();
//    }
//}
//function onQueryFaild(sender, args) { }
//function createList() {
//    var clientContext = new SP.ClientContext.get_current();
//    var oWebsite = clientContext.get_web();
//    //var rootWebsite = clientContext.get_site().get_rootWeb();
//    var listCreationInfo = new SP.ListCreationInformation();
//    listCreationInfo.set_title('Project Documents'); // list name
//    listCreationInfo.set_description('Master project documents. '); // list description
//    listCreationInfo.set_templateType(SP.ListTemplateType.documentlibrary); //list type
    
//    oWebsite.get_lists().add(listCreationInfo);
//    //clientContext.load(oWebsite);
//    clientContext.executeQueryAsync(
//        Function.createDelegate(this, this.onQuerySucceeded1),// when success
//        Function.createDelegate(this, this.onQueryFailed1) // when failed
//        );
//}
//function onQuerySucceeded1() {

//    // $('#peopleDiv').text("Refresh the page to view Announcements.");
//    alert('success');

//}

//function onQueryFailed1(sender, args) { alert(args.get_message()); }