// Variable that will hold the SharePoint ClientContext object
var context;

// Variable that will hold the SharePoint App Web object
var web;

// Variable that will hold various SharePoint List objects
var list;

// Variable that will hold the SharePoint user
var user;

// Variable that will hold the SharePoint user name
var userName;

// Variables that will hold various SharePoint ListItem objects
var currentItem;
var userItem;
var initiativeItem;

// Variable that will hold the Sharepoint list ID
var initiativeID;
var myInitID;

// Variable that will hold initiative Status value
var initStatus;

// This function runs when the DOM is ready and wires up events to two file input elements.
// It also applies jQuery methods to turn various text input elements into date pickers.
// It also creates a context object which is needed to use the SharePoint object model.
$(document).ready(function () {
    var errArea = document.getElementById("errGeneral");

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Reference and load the basic SharePoint objects needed to start with
    context = SP.ClientContext.get_current();
    web = context.get_web();
    userName = web.get_currentUser();
    userName.retrieve();
    context.load(web);
    context.executeQueryAsync(function () {

        // Success returned from executeQueryAsync
        hideAllPanels();
        user = userName.get_title();
        $('#Home').fadeIn(500, null);
    },
    function (sender, args) {

        // Failure returned from executeQueryAsync
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Failed to get started. Error: " + args.get_message()));
        errArea.appendChild(divMessage);
        $('#Home').fadeIn(500, null);
    });
    // Initialize Date Picker
    $('#newStartDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#newEndDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#editStartDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });
    $('#editEndDate').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });

});

// This function hides all main DIV elements. The caller is then responsible 
// for re-showing the one that needs to be displayed.
function hideAllPanels() {
    $('#MyInitiatives').hide();
    $('#MyInitiative').hide();
    $('#AllInitiative').hide();
    $('#AddInitiative').hide();
    $('#InitiativeDetails').hide();
    $('#editInitiative').hide();
    $('#MyRole').hide();
}

// This function retrieves all initiatives created by current user
function showMyInitiatives() {
    $('#MyInitiativesTile').css("background-color", "burlywood");
    $('#AllInitiativesTile').css("background-color", "white");
    $('#MyRolesTile').css("background-color", "white");
    var errArea = document.getElementById("errMyInitiatives");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasInitiatives = false;
    hideAllPanels();
    $('#MyInitiative').show();
    list = web.get_lists().getByTitle('Initiative');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Initiator' /><Value Type='Text'>"
        + user
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var initiativeTable = document.getElementById("MyInitiativeList");

            // Remove all nodes from the MyInitiativeList <DIV> so we have a clean space to write to
            while (initiativeTable.hasChildNodes()) {
                initiativeTable.removeChild(initiativeTable.lastChild);
            }

            // Iterate through the initiative list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the initiative name
                var initiative = document.createElement("div");
                var initiativeLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                initiative.appendChild(initiativeLabel);

                // Add an ID to the initiative DIV
                initiative.id = listItem.get_id();

                // Add an class to the initiative DIV
                initiative.className = "item";

                // Add an onclick event to show the initiative details
                $(initiative).click(function (sender) {
                    showMyInitiativeDetails(sender.target.id);
                });

                // Add the initiative div to the UI
                initiativeTable.appendChild(initiative);
                hasInitiatives = true;
            }
            if (!hasInitiatives) {
                var noInitiatives = document.createElement("div");
                noInitiatives.appendChild(document.createTextNode("There are no initiatives. You can add a new initiative from here."));
                initiativeTable.appendChild(noInitiatives);
            }
            $('#MyInitiatives').fadeIn(500, null);
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get initiatives. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#MyInitiativeList').fadeIn(500, null);
        });
}

// This function shows the details for a specific initiative created by current user
function showMyInitiativeDetails(itemID) {
    var inArray = new Array();
    var errArea = document.getElementById("errMyInitiatives");
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    // Remove all nodes from the remainingRoles <select>
    var remainRoleDiv = document.getElementById("remainingRoles");
    while (remainRoleDiv.hasChildNodes()) {
        remainRoleDiv.removeChild(remainRoleDiv.lastChild);
    }
    myInitID = itemID;

    $('#AddInitiative').hide();
    $('#editInitiative').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            $('#editInitiativeName').val(currentItem.get_fieldValues()["Title"]);
            $('#editStartDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MM dd, yy"));
            $('#editEndDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MM dd, yy"));
            $('#editFundTargets').val(currentItem.get_fieldValues()["FundTargets"]);
            $('#editManHours').val(currentItem.get_fieldValues()["ManHours"]);
            initStatus = currentItem.get_fieldValues()["_Status"];
            var roleValues = currentItem.get_fieldValues()["Roles"];
            var roleDiv = document.getElementById("editRoles");
            while (roleDiv.hasChildNodes()) {
                roleDiv.removeChild(roleDiv.lastChild);
            }
            if (roleValues != null) {
                $('#editRoles').show();
                inArray = roleValues.split(";");
                inArray.pop();
                for (var i in inArray) {
                    if (inArray[i] == ";") {
                        delete inArray[i];
                    }
                    if (inArray[i] == " " || inArray[i] == null) {
                        delete inArray[i];
                    }
                }
                for (var i = 0; i < inArray.length; i++) {
                    if (inArray[i] != ";" || inArray[i] != null) {
                        var opt = document.createElement('option');
                        opt.innerHTML = inArray[i];
                        opt.value = inArray[i];
                        roleDiv.appendChild(opt);
                    }
                }

            }
            else {
                $('#editRoles').hide();
                $('#remainingRoles').append("None");
            }
            $('#editVolunteers').val(currentItem.get_fieldValues()["Volunteers"]);
            showAchievements(itemID);
            $('#editInitiative').fadeIn(500, null);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var errArea = document.getElementById("errMyInitiatives");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function tracks progress of specified initiative
function showAchievements(itemID) {
    // Initialize variables for fund tragets, man hours and number of volunteers
    var achFundTargets=0;
    var achManHours=0;
    var achVolunteers=0;

    var userList=web.get_lists().getByTitle("User");
    var camlQuery=new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='InitiativeLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        +"</Value></Eq></Where></Query></View>");
    var listItems = userList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            // Remove all nodes from the achievedVolunteer <DIV> so we have a clean space to write to
            var volunteerTable = document.getElementById("achievedVolunteer");
            while (volunteerTable.hasChildNodes()) {
                volunteerTable.removeChild(volunteerTable.lastChild);
            }
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                achFundTargets = achFundTargets + listItem.get_fieldValues()["Donation"];
                achManHours = achManHours + listItem.get_fieldValues()["WorkHours"];
                    
                achVolunteers = achVolunteers + 1;

               
                // Remove all nodes from the volunteer <DIV> so we have a clean space to write to

                var volunteer = document.createElement("div");
                var volunteerLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);

                volunteer.appendChild(volunteerLabel);

                // Add an ID to the volunteer DIV
                volunteer.id = listItem.get_id();

                // Add an class to the volunteer DIV
                volunteer.className = "volunteerLabel";

                var newLine = document.createElement("div");
                newLine.className = "clearLabel";
                
                // Add the volunteer div to the UI
                volunteerTable.appendChild(newLine);
                volunteerTable.appendChild(volunteer);
               

                 $('#achievedVolunteer').fadeIn(500, null);


            }
            $('#achievedFundTargets').val(achFundTargets);
            $('#achievedManHours').val(achManHours);
            $('#achievedVolunteers').val(achVolunteers);
            if (initStatus != "Achieved") {
                if (achFundTargets >= $('#editFundTargets').val() && achManHours >= $('#editManHours').val() && achVolunteers >= $('#editVolunteers').val()&& $('#editRoles').is(':empty')) {
                    $('#saveEditInitiative').hide();
                    $('#achieveInitiative').show();
                }
                else {
                    $('#saveEditInitiative').show();
                    $('#achieveInitiative').hide();
                }
                $('#initAchieved').hide();
            }
            else {
                $('#saveEditInitiative').hide();
                $('#achieveInitiative').hide();
                $('#initAchieved').show();
            }
        },
        function (sender, args) {
            //Failure from executeQueryAsync
            alert("Error: "+args.get_message());
        }
        );
}

//This function sets initiative to "Achieved"
function achieveInitiative() {
    var initList=web.get_lists().getByTitle("Initiative");
    var initItem = initList.getItemById(myInitID);
    initItem.set_item("_Status", "Achieved");
    initItem.update();
    context.load(initItem);
    context.executeQueryAsync(
        function () {
            //Success returned from executeQueryAsync
            cancelEditInitiative();
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error: "+args.get_message());
        }
        );
}

// This function shows the form for adding a new initiative
function addNewInitiative() {
    clearNewInitiativeForm();
    $('#AddInitiative').hide();
    $('#editInitiative').hide();
    $('#AddInitiative').fadeIn(500, null);
}

// This function saves the new initiative
function saveNewInitiative() {
    if ($('#newInitiative').val() == "") {
        var errArea = document.getElementById("errMyInitiatives");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Initiative' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        initiativeItem = list.addItem(itemCreateInfo);
        initiativeItem.set_item("Title", $('#newInitiative').val());
        initiativeItem.set_item("EventDate", $('#newStartDate').val());
        initiativeItem.set_item("EndDate", $('#newEndDate').val());
        initiativeItem.set_item("FundTargets", $('#newFundTargets').val());
        initiativeItem.set_item("ManHours", $('#newManHours').val());
        initiativeItem.set_item("Volunteers", $('#newVolunteers').val());
        initiativeItem.set_item("Initiator", user);
        initiativeItem.set_item("Roles", $('#newRoles').val());
        initiativeItem.update();
        context.load(initiativeItem);
        context.executeQueryAsync(function () {
            //Success returned from executeQueryAsync
            clearNewInitiativeForm();
            showMyInitiatives();
            var initID = initiativeItem.get_id();
            saveInitiator(initID);
        },
            function (sender, args) {
                //Failure returned from executeQueryAsync
                var errArea = document.getElementById("errMyInitiatives");
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (errArea.hasChildNodes()) {
                    errArea.removeChild(errArea.lastChild);
                }
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function saves the initiator of initiative
function saveInitiator(initID) {
    var userList = web.get_lists().getByTitle("User");
    var itemCreateInfo = new SP.ListItemCreationInformation();
    userItem = userList.addItem(itemCreateInfo);
    userItem.set_item("Title", user);
    userItem.set_item("InitiativeLookup", initID);
    userItem.set_item("SelectedRole", "Initiator");
    userItem.update();
    context.load(userItem);
    context.executeQueryAsync(function () {
        //Success returned from executeQueryAsync

    },
    function (sender, args) {
        //Failure returned from executeQueryAsync
        var errArea = document.getElementById("errMyInitiatives");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode(args.get_message()));
        errArea.appendChild(divMessage);
    });
}

// This function adds roles
function addRoles() {
    var roleText = $('#newRoles').val();
    roleText = roleText + ";";
    $('#newRoles').val(roleText);
}

// This function cancels the new initiative
function cancelNewInitiative() {
    clearNewInitiativeForm();
}

// This function clears the inputs on the new initiative form
function clearNewInitiativeForm() {
    var errArea = document.getElementById("errMyInitiatives");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddInitiative').fadeOut(500, function () {
        $('#AddInitiative').hide();
        $('#newInitiative').val("");
        $('#newStartDate').val("");
        $('#newEndDate').val("");
        $('#newFundTargets').val("");
        $('#newManHours').val("");
        $('#newRoles').val("");
        $('#newVolunteers').val("");
    });
}

// This function cancels the editing of an existing initiative
function cancelEditInitiative() {
    clearEditInitiativeForm();
}

// This function clears the inputs on the edit initiative form
function clearEditInitiativeForm() {
    var errArea = document.getElementById("errMyInitiatives");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#editInitiative').fadeOut(500, function () {
        $('#editInitiative').hide();
        $('#editInitiativeName').val("");
        $('#editStartDate').val("");
        $('#editEndDate').val("");
        $('#editFundTargets').val("");
        $('#editManHours').val("");
        $('#editRoles').val("");
        $('#editVolunteers').val("");
    });
}

// This function updates an existing initiative's details
function saveEditInitiative() {
    if ($('#editInitiativeName').val() == "") {
        var errArea = document.getElementById("errMyInitiatives");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Initiative Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentItem.set_item("Title", $('#editInitiativeName').val());
        currentItem.set_item("EventDate", $('#editStartDate').val());
        currentItem.set_item("EndDate", $('#editEndDate').val());
        currentItem.set_item("FundTargets", $('#editFundTargets').val());
        //currentItem.set_item("Roles", $('#editRoles').val());
        currentItem.set_item("Volunteers", $('#editVolunteers').val());
        currentItem.set_item("ManHours", $('#editManHours').val());
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            //Success returned from executeQueryAsync
            clearEditInitiativeForm();
            showMyInitiatives();
        },
            function (sender, args) {
                //Failure returned from executeQueryAsync
                var errArea = document.getElementById("errMyInitiatives");
                // Remove all nodes from the error <DIV> so we have a clean space to write to
                while (errArea.hasChildNodes()) {
                    errArea.removeChild(errArea.lastChild);
                }
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode(args.get_message()));
                errArea.appendChild(divMessage);
            });
    }
}

// This function retrieves all initiatives created by other than current user
function showAllInitiatives() {
    $('#MyInitiativesTile').css("background-color", "white");
    $('#AllInitiativesTile').css("background-color", " skyblue ");
    $('#MyRolesTile').css("background-color", "white");
    
    var errArea = document.getElementById("errAllInitiatives");
    
    
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasInitiatives = false;
    hideAllPanels();
    $('#AllInitiative').show();

    var initiativeArray = new Array();
    var removedInitArray = new Array();
    var origLen;
    var found;
    var x, y;
    var newArr = new Array();

    list = web.get_lists().getByTitle('User');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            var initiativeTable = document.getElementById("AllInitiativeList");

            // Remove all nodes from the initiative <DIV> so we have a clean space to write to
            while (initiativeTable.hasChildNodes()) {
                initiativeTable.removeChild(initiativeTable.lastChild);
            }

            // Iterate through the initiative list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var initiativeLookup = listItem.get_fieldValues()["InitiativeLookup"].get_lookupValue();
                var username = listItem.get_fieldValues()["Title"];

                initiativeArray.push(initiativeLookup);

                if (username == user) {
                    initiativeArray.pop();
                    removedInitArray.push(initiativeLookup);
                }
                
            }

            for (var i in initiativeArray) {
                for (var j in removedInitArray) {
                    if (initiativeArray[i] == removedInitArray[j]) {
                        delete initiativeArray[i];
                    }
                }
            }

            origLen = initiativeArray.length;
            for (x = 0; x < origLen; x++) {
                found = undefined;
                for (y = 0; y < newArr.length; y++) {
                    if (initiativeArray[x] == newArr[y]) {
                        found = true;
                        break;
                    }
                }
                if (!found) newArr.push(initiativeArray[x]);
            }
            initiativeArray = newArr;

            for (var i in initiativeArray) {              
                if (initiativeArray[i] != null) {
                    getInitiativeTile(initiativeArray[i]);
                }
            }
            for (var i in initiativeArray) {
                if (initiativeArray[i] == null)
                    initiativeArray.pop();
            }

            if (initiativeArray.length == 0) {
                var noInitiatives = document.createElement("div");
                noInitiatives.appendChild(document.createTextNode("There are no initiatives."));
                initiativeTable.appendChild(noInitiatives);
            }
        },
        function (sender, args) {
            //Failure returned from executeQueryAsync
            alert("Error in showallinitiatives"+args.get_message());
        });
}

// This function creates initiative div 
function getInitiativeTile(initID) {
    var errArea=document.getElementById("errAllInitiatives");
    while(errArea.hasChildNodes()){
        errArea.removeChild(errArea.lastChild);
    }
    
    list=web.get_lists().getByTitle("Initiative");
    var camlQuery=new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Text'>"
        +initID
        +"</Value></Eq></Where></Query></View>");
    var listItems=list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Success returned from executeQueryAsync
                var initiativeTable = document.getElementById("AllInitiativeList");

                var status = listItem.get_fieldValues()["_Status"];
                if (status != "Achieved") {
                    // Remove all nodes from the employee <DIV> so we have a clean space to write to
                    var initiative = document.createElement("div");
                    var initiativeLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);

                    initiative.appendChild(initiativeLabel);

                    // Add an ID to the initiative DIV
                    initiative.id = listItem.get_id();

                    // Add an class to the initiative DIV
                    initiative.className = "item";

                    // Add an onclick event to show the initiative details
                    $(initiative).click(function (sender) {
                        showAllInitiativeDetails(sender.target.id);
                    });

                    // Add the initiative div to the UI
                    initiativeTable.appendChild(initiative);
                }
            }
            $('#AllInitiativeList').fadeIn(500, null);
       
        },
        function (sender, args) {
            // Success returned from executeQueryAsync
            alert("Error in creating divs"+args.get_message());
        }
        );
}

// This function shows the details for a specific initiative
function showAllInitiativeDetails(itemID) {
    $('#workHours').val("");
    $('#donation').val("");
    $('#enrollInitiative').hide();
    var inArray = new Array();
    var errArea = document.getElementById("errAllInitiatives");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    initiativeID = itemID;
    $('#InitiativeDetails').hide();
    currentItem = list.getItemById(itemID);
    context.load(currentItem);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            $('#initiativeName').val(currentItem.get_fieldValues()["Title"]);
            $('#startDate').val(new Date(currentItem.get_fieldValues()["EventDate"]).format("MM dd, yyyy"));
            $('#endDate').val(new Date(currentItem.get_fieldValues()["EndDate"]).format("MM dd, yyyy"));
            $('#fundTargets').val(currentItem.get_fieldValues()["FundTargets"]);
            $('#manHours').val(currentItem.get_fieldValues()["ManHours"]);

            var roleValues = currentItem.get_fieldValues()["Roles"];
            var roleDiv = document.getElementById("roles");
            while (roleDiv.hasChildNodes()) {
                roleDiv.removeChild(roleDiv.lastChild);
            }
            if (roleValues != null) {
                $('#enrollToInitiative').show();
                inArray = roleValues.split(";");
                inArray.pop();
                for (var i = 0; i < inArray.length; i++) {
                    var opt = document.createElement('option');
                    opt.innerHTML = inArray[i];
                    opt.value = inArray[i];
                    roleDiv.appendChild(opt);
                }
                $('#achieveLabel').hide();
            }
            else {
                $('#enrollToInitiative').hide();
                $('#achieveLabel').show();
            }
            $('#volunteers').val(currentItem.get_fieldValues()["Volunteers"]);
            $('#InitiativeDetails').fadeIn(500, null);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            var errArea = document.getElementById("errAllInitiatives");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function cancels the initiative
function cancelInitiative() {
    clearInitiativeForm();
}

// This function clears the inputs on the initiative form
function clearInitiativeForm() {
    var errArea = document.getElementById("errAllInitiatives");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#InitiativeDetails').fadeOut(500, function () {
        $('#InitiativeDetails').hide();
        $('#initiativeName').val("");
        $('#startDate').val("");
        $('#endDate').val("");
        $('#fundTargets').val("");
        $('#manHours').val("");
        $('#roles').val("");
        $('#volunteers').val("");
        $('#workHours').val("");
        $('#donation').val("");
    });
}

// This function shows form for enrolling user to initiative
function enrollToInitiative() {
    $('#enrollInitiative').fadeIn(500, null);
    $('#enrollToInitiative').hide();
    $('#saveEnrollInitiative').show();
}

// This function saves the user enrollement to the initiative
function saveEnrollInitiative() {
    var userList = web.get_lists().getByTitle("User");
    var inArray = [];
    var Roles = "";
    var itemCreateInfo = new SP.ListItemCreationInformation();
    userItem = userList.addItem(itemCreateInfo);
    userItem.set_item("Title", user);
    userItem.set_item("InitiativeLookup", initiativeID);
    userItem.set_item("SelectedRole", $('#roles').val());
    if ($('#donation').val() != "") {
        userItem.set_item("Donation", $('#donation').val());
       
    }
    if ($('#workHours').val() != "") {
        userItem.set_item("WorkHours", $('#workHours').val());
        
    }
    var selectedRole= $('#roles').val();
    userItem.update();
    context.load(userItem);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        var initList = web.get_lists().getByTitle("Initiative");
        var initItem = initList.getItemById(initiativeID);
        var roleItem = initItem.get_fieldValues()["Roles"];
        inArray = roleItem.split(";");
        inArray.pop();
        for (var i in inArray) {
            if (inArray[i] == selectedRole) {
                delete inArray[i];
            }
        }
        for (var i in inArray) {
            Roles += inArray[i] + ";";
        }
        initItem.set_item("Roles", Roles);
        initItem.update();
        context.load(initItem);
        context.executeQueryAsync(
            function () {
                // Success returned from executeQueryAsync
                clearInitiativeForm();
                showAllInitiatives();
            },
            function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Error"+args.get_message());
            });
        
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        var errArea = document.getElementById("errAllInitiatives");
        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode(args.get_message()));
        errArea.appendChild(divMessage);
    });
    
}

// This function shows enrolled initiatives with role for current user 
function showMyRoles() {
    $('#MyInitiativesTile').css("background-color", "white");
    $('#AllInitiativesTile').css("background-color", "white");
    $('#MyRolesTile').css("background-color", "#FFCCCC");
    var errArea = document.getElementById("errMyRoles");
    var initArray = new Array();
    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasRoles = false;
    hideAllPanels();
    $('#MyRole').show();
    list = web.get_lists().getByTitle('User');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
        + user
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var roleTable = document.getElementById("MyRoleList");

            // Remove all nodes from the initiative <DIV> so we have a clean space to write to
            while (roleTable.hasChildNodes()) {
                roleTable.removeChild(roleTable.lastChild);
            }

            // Iterate through the initiative list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var selectedRole = listItem.get_fieldValues()["SelectedRole"];
                var workHours = listItem.get_fieldValues()["WorkHours"];
                var donation = listItem.get_fieldValues()["Donation"];
                
                if (selectedRole != "Initiator") {
                    // Create a DIV to display the initiative name
                    var initLookup = listItem.get_fieldValues()["InitiativeLookup"].get_lookupValue();
                    getInitName(initLookup, selectedRole, workHours, donation);
                    hasRoles = true;
                }
            }

            if (!hasRoles) {
                var noRoles = document.createElement("div");
                noRoles.appendChild(document.createTextNode("There are no roles."));
                roleTable.appendChild(noRoles);
            }
            
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get initiatives. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
            $('#MyRoleList').fadeIn(500, null);
        });
}

// This function creates initiative div for enrolled initiatives
function getInitName(initID,selectedRole,workHours,donation) {

    var errArea = document.getElementById("errMyRoles");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    list = web.get_lists().getByTitle("Initiative");
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Text'>"
        + initID
        + "</Value></Eq></Where></Query></View>");
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var employeeTable = document.getElementById("MyRoleList");

                // Remove all nodes from the employee <DIV> so we have a clean space to write to
                var employee = document.createElement("div");
                var employeeLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                var eventid = listItem.get_fieldValues()["ID"];
               
                employee.appendChild(employeeLabel);
                //employee.appendChild(roleLabel);

                // Add an ID to the employee DIV
                employee.id = listItem.get_id();

                // Add an class to the employee DIV
                employee.className = "itemTable";

                // Add the employee div to the UI
                employeeTable.appendChild(employee);
                $('#MyRoleList').append("<div class='itemTable'>" + selectedRole + "</div><div class='itemTable'>" + workHours + " hours</div> <div class='itemTable'> $" + donation + "</div><div class='clear'>&nbsp;</div>");
                $('#MyRoleList').fadeIn(500, null);
            }

        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error: "+args.get_message());
        });
}
