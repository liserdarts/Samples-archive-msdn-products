'use strict';

// Variables used to hold various SharePoint objects
var context = SP.ClientContext.get_current();
var web = context.get_web();
var user = web.get_currentUser();
var courseList;
var currentCourseItem;
var moduleList;
var currentModuleItem;
var topicList;
var currentTopicItem;

var courseList;
var moduleList;
var topicList;

var moduleTitlesArray = [];
var moduleObjectivesArray = [];
var topicTitlesArray = [];
var topicObjectivesArray = [];
var topicUrlsArray = [];
var topicTypesArray = [];

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("GeneralError");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Load the web and user objects, and then see if the current user is to be considered a course administrator/designer or a student.
    context.load(web, 'EffectiveBasePermissions');
    context.load(user);
    user.retrieve();
    context.executeQueryAsync(
        function () {
            // If current user has ManageWeb permissions, we'll consider them a course administrator.
            // Otherwise, we'll consider them a student.
            if (web.get_effectiveBasePermissions().has(SP.PermissionKind.manageWeb)) {
                showCourseDesigner();
            }
            else {
                showCourseList();
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get started. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
});

// This function hides all main DIV elements. The caller is then responsible 
// for re-showing the one that needs to be displayed.
function hideAllPanels() {
    $('#AddCourse').hide();
    $('#CourseDetails').hide();
    $('#NewModule').hide();
    $('#ModuleDetails').hide();
    $('#NewTopic').hide();
    $('#TopicDetails').hide();
}

/**********************************************************
    Until noted otherwise, the functions that follow 
    apply to the courseware design process
**********************************************************/

// This function shows existing courses and enable the user to create new courses
function showCourseDesigner() {
    hideAllPanels();

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("ErrAllCourses");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasCourses = false;
    courseList = web.get_lists().getByTitle('TrainingCourse');
    moduleList = web.get_lists().getByTitle('Module');
    topicList = web.get_lists().getByTitle('Topic');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = courseList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            // Remove all nodes from the course <DIV> so we have a clean space to write to
            var courseTable = document.getElementById("AllCoursesList");
            while (courseTable.hasChildNodes()) {
                courseTable.removeChild(courseTable.lastChild);
            }

            // Iterate through the Course list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                    // Create a DIV to display the course name 
                    var course = document.createElement("div");
                    var courseLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    course.appendChild(courseLabel);

                    // Add an ID to the course DIV
                    course.id = listItem.get_id();

                    // Add an class to the lead DIV
                    course.className = "item";

                    // Add an onclick event to show the lead details
                    $(course).click(function (sender) {
                        showCourseDetails(sender.target.id);
                    });

                    // Add the lead div to the UI
                    courseTable.appendChild(course);
                    hasCourses = true;
            }
            if (!hasCourses) {
                var noCourses = document.createElement("div");
                noCourses.appendChild(document.createTextNode("There are currently no courses."));
                courseTable.appendChild(noCourses);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get courses. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
    $('#AllCoursesView').fadeIn(500, null);
}

// This function shows the UI for adding new courses
function addNewCourse() {
    hideAllPanels();
    $('#AddCourse').fadeIn(500, null);
}

// This function runs when the user attmempts to save a new course
function saveNewCourse() {
    if (($('#newCourseName').val() == "") || ($('#newCourseObjective').val() == "")) {
        var errArea = document.getElementById("ErrAllCourses");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("All fields are required."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var listItem = courseList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#newCourseName').val());
        listItem.set_item("CourseObjective", $('#newCourseObjective').val());
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            clearNewCourseForm();
            showCourseDesigner();
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllCourses");

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

// This function runs when the user cancels the creation of a new course
function cancelNewCourse() {
    clearNewCourseForm();
}

// This function clears all input elements in the 'New Course' UI
function clearNewCourseForm() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddCourse').fadeOut(500, function () {
        $('#AddCourse').hide();
        $('#newCourseName').val("");
        $('#newCourseObjective').val("");
    });
}

// This function shows the details of a previously-saved course
function showCourseDetails(itemID) {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var errModuleArea = document.getElementById("ErrNewModule");
    while (errModuleArea.hasChildNodes()) {
        errModuleArea.removeChild(errModuleArea.lastChild);
    }
    hideAllPanels();
    $('#AddModuleClicker').fadeIn();
    $('#deleteEditCourseButton').fadeIn();
    $('#cancelEditCourseButton').fadeIn();
    $('#saveEditCourseButton').fadeIn();
    $('#validateCourseButton').fadeIn();
    $('#EditCourseModuleList').fadeIn();
    currentCourseItem = courseList.getItemById(itemID);
    context.load(currentCourseItem);
    context.executeQueryAsync(
        function () {
            $('#editCourseName').val(currentCourseItem.get_fieldValues()["Title"]);
            $('#editCourseObjective').val(currentCourseItem.get_fieldValues()["CourseObjective"]);
            $('#CourseDetails').fadeIn(500, null);
            PopulateModules(itemID);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user attempts to delete a previously-saved course
function deleteEditCourse() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var moduleList = web.get_lists().getByTitle('Module');
    var moduleQuery = new SP.CamlQuery();
    moduleQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='CourseID' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentCourseItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var moduleItems = moduleList.getItems(moduleQuery);
    context.load(moduleItems);
    context.executeQueryAsync(
        function () {
            if (moduleItems.get_count() >= 1) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("This course has modules and cannot be deleted. Please delete all modules before deleting this course."));
                errArea.appendChild(divMessage);
            }
            else {
                currentCourseItem.deleteObject();
                context.executeQueryAsync(
                    function () {
                        clearEditCourseForm();
                        showCourseDesigner();
                    },
                    function (sender, args) {
                        var divMessage = document.createElement("DIV");
                        divMessage.setAttribute("style", "padding:5px;");
                        divMessage.appendChild(document.createTextNode(args.get_message()));
                        errArea.appendChild(divMessage);
                    });
            }
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to check modules. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user cancels the editing of course data for a previously-saved course
function cancelEditCourse() {
    clearEditCourseForm();
}

// This function runs when the user attempts to update the details of a previously-saved course
function saveEditCourse() {
    if (($('#editCourseName').val() == "") || ($('#editCourseObjective').val() == "")) {
        var errArea = document.getElementById("ErrAllCourses");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("All fields are required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentCourseItem.set_item("Title", $('#editCourseName').val());
        currentCourseItem.set_item("CourseObjective", $('#editCourseObjective').val());
        currentCourseItem.update();
        context.load(currentCourseItem);
        context.executeQueryAsync(function () {
            clearEditCourseForm();
            showCourseDesigner();
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllCourses");

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

// This function clears all input elements in the 'Edit Course' UI
function clearEditCourseForm() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#CourseDetails').fadeOut(500, function () {
        $('#CourseDetails').hide();
        $('#editCourseName').val("");
        $('#editCourseObjective').val("");
    });
}

// This function shows the UI for adding a new module to a course
function addNewModule() {
    var errArea = document.getElementById("ErrNewModule");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewModuleTitle').val("");
    $('#NewModuleObjective').val("");
    $('#AddModuleClicker').fadeOut();
    $('#deleteEditCourseButton').fadeOut();
    $('#cancelEditCourseButton').fadeOut();
    $('#EditCourseModuleList').fadeOut();
    $('#saveEditCourseButton').fadeOut();
    $('#validateCourseButton').fadeOut(500, function () { $('#NewModule').fadeIn(500, null) });
}

// This function runs when the user attempts to save a new module
function saveNewModule() {
    var errArea = document.getElementById("ErrNewModule");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if (($('#NewModuleTitle').val() == "") || ($('#NewModuleObjective').val() == "")) {
        var divMessage = document.createElement("DIV");
        divMessage.appendChild(document.createTextNode("All fields are required!"));
        errArea.appendChild(divMessage);
    }
    else {
        var itemID = currentCourseItem.get_id()
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var moduleList = web.get_lists().getByTitle('Module');
        var listItem = moduleList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#NewModuleTitle').val());
        listItem.set_item("ModuleObjective", $('#NewModuleObjective').val());
        listItem.set_item("CourseID", itemID);
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            var errArea = document.getElementById("ErrNewModule");
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            $('#NewModule').fadeOut(500, function () {
                $('#AddModuleClicker').fadeIn();
                $('#deleteEditCourseButton').fadeIn();
                $('#cancelEditCourseButton').fadeIn();
                $('#saveEditCourseButton').fadeIn();
                $('#validateCourseButton').fadeIn();
                $('#EditCourseModuleList').fadeIn();
                var itemID = currentCourseItem.get_id()
                PopulateModules(itemID);
            });
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllCourses");

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

// This function runs when the user cancels the creation of a new module
function cancelNewModule() {
    var errArea = document.getElementById("ErrNewModule");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewModule').fadeOut(500, function () {
        $('#AddModuleClicker').fadeIn();
        $('#deleteEditCourseButton').fadeIn();
        $('#cancelEditCourseButton').fadeIn();
        $('#saveEditCourseButton').fadeIn();
        $('#validateCourseButton').fadeIn();
        $('#EditCourseModuleList').fadeIn();
        var itemID = currentCourseItem.get_id()
        PopulateModules(itemID);
    });
    
}

// This function shows a list of modules (which can be clicked to edit them)
function PopulateModules(itemID) {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Create a CAML query that retrieves the modules for the current course
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='CourseID' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = moduleList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync 
            var moduleTable = document.getElementById("EditCourseModuleList");

            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (moduleTable.hasChildNodes()) {
                moduleTable.removeChild(moduleTable.lastChild);
            }

            // Iterate through the Courses
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Module text
                var module = document.createElement("div");
                var moduleLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                module.appendChild(moduleLabel);

                // Add an ID to the Module DIV
                module.id = listItem.get_id();

                // Add an class to the Module DIV
                module.className = "moduleItem";

                // Add an onclick event to show the PO details
                $(module).click(function (sender) {
                    showModuleDetails(sender.target.id);
                });

                //Add the module div to the UI
                moduleTable.appendChild(module);
                
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get modules. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function shows the details of a module for editing when the user clicks a module in the course designer
function showModuleDetails(itemID) {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var errTopicArea = document.getElementById("ErrNewTopic");
    while (errTopicArea.hasChildNodes()) {
        errTopicArea.removeChild(errTopicArea.lastChild);
    }
    hideAllPanels();
    $('#NewTopic').fadeOut(500, function () {
        $('#AddTopicClicker').fadeIn();
        $('#deleteEditModuleButton').fadeIn();
        $('#cancelEditModuleButton').fadeIn();
        $('#saveEditModuleButton').fadeIn();
        $('#EditModuleTopicList').fadeIn();
    });
    currentModuleItem = moduleList.getItemById(itemID);
    context.load(currentModuleItem);
    context.executeQueryAsync(
        function () {
            $('#editModuleText').val(currentModuleItem.get_fieldValues()["Title"]);
            $('#editModuleObjective').val(currentModuleItem.get_fieldValues()["ModuleObjective"]);
            $('#ModuleDetails').fadeIn(500, null);
            PopulateTopics(itemID);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function shows a list of modules (which can be clicked to edit them)
function PopulateTopics(itemID) {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Create a CAML query that retrieves the topics for the current module
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ModuleID' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = topicList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync 
            var topicTable = document.getElementById("EditModuleTopicList");

            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (topicTable.hasChildNodes()) {
                topicTable.removeChild(topicTable.lastChild);
            }

            // Iterate through the Courses
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Module text
                var topic = document.createElement("div");
                var topicLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                topic.appendChild(topicLabel);

                // Add an ID to the Module DIV
                topic.id = listItem.get_id();

                // Add an class to the Module DIV
                topic.className = "moduleItem";

                // Add an onclick event to show the PO details
                $(topic).click(function (sender) {
                    showTopicDetails(sender.target.id);
                });

                //Add the module div to the UI
                topicTable.appendChild(topic);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get topics. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user attempts to delete a previously-saved module
function deleteEditModule() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var topicList = web.get_lists().getByTitle('Topic');
    var topicQuery = new SP.CamlQuery();
    topicQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='ModuleID' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentModuleItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var topicItems = topicList.getItems(topicQuery);
    context.load(topicItems);
    context.executeQueryAsync(
        function () {
            if (topicItems.get_count() >= 1) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("This module has topics and cannot be deleted. Please delete all topics before deleting this module."));
                errArea.appendChild(divMessage);
            }
            else {
                currentModuleItem.deleteObject();
                context.executeQueryAsync(
                    function () {
                        clearEditModuleForm();
                        var itemID = currentCourseItem.get_id()
                        PopulateModules(itemID);
                        showCourseDesigner();
                    },
                    function (sender, args) {
                        var divMessage = document.createElement("DIV");
                        divMessage.setAttribute("style", "padding:5px;");
                        divMessage.appendChild(document.createTextNode(args.get_message()));
                        errArea.appendChild(divMessage);
                    });
            }
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to check topics. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user cancels the editing of an existing module
function cancelEditModule() {
    clearEditModuleForm();
}

// This function runs when the user attempts to update the details of a previously-saved module
function saveEditModule() {
    if (($('#editModuleText').val() == "") || ($('#editModuleObjective').val() == "")) {
        var errArea = document.getElementById("ErrAllCourses");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("All fields are required."));
        errArea.appendChild(divMessage);
    }
    else {
        currentModuleItem.set_item("Title", $('#editModuleText').val());
        currentModuleItem.set_item("ModuleObjective", $('#editModuleObjective').val());
        currentModuleItem.update();
        context.load(currentModuleItem);
        context.executeQueryAsync(function () {
            clearEditModuleForm();
            var itemID = currentCourseItem.get_id()
            PopulateModules(itemID);
            showCourseDesigner();
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllCourses");

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

// This function clears all input elements in the 'Edit Module' UI
function clearEditModuleForm() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#ModuleDetails').fadeOut(500, function () {
        $('#ModuleDetails').hide();
        $('#editModuleText').val("");
        $('#editModuleObjective').val("");
        $('#CourseDetails').fadeIn();
    });
}

// This function shows the UI for adding a new topic to a module
function addNewTopic() {
    var errArea = document.getElementById("ErrNewTopic");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewTopicTitle').val("");
    $('#NewTopicObjective').val("");
    $('#NewTopicUrl').val("");
    $('#NewTopicType').val("Concept");
    $('#NewTopicTypeHelp').hide();
    $('#AddTopicClicker').fadeOut();
    $('#deleteEditModuleButton').fadeOut();
    $('#cancelEditModuleButton').fadeOut();
    $('#EditModuleTopicList').fadeOut();
    $('#saveEditModuleButton').fadeOut(500, function () { $('#NewTopic').fadeIn(500, null) });
}

// This function runs when the user attempts to save a new topic
function saveNewTopic() {
    var errArea = document.getElementById("ErrNewTopic");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if (($('#NewTopicTitle').val() == "") || ($('#NewTopicObjective').val() == "")) {
        var divMessage = document.createElement("DIV");
        divMessage.appendChild(document.createTextNode("All fields are required!"));
        errArea.appendChild(divMessage);
    }
    else if (!($('#NewTopicUrl').val().startsWith("http://")) && !($('#NewTopicUrl').val().startsWith("https://"))) {
        var divMessage = document.createElement("DIV");
        divMessage.appendChild(document.createTextNode("Please enter a full URL, including http:// or https://"));
        errArea.appendChild(divMessage);
    }
    else {
        var itemID = currentModuleItem.get_id()
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var topicList = web.get_lists().getByTitle('Topic');
        var listItem = topicList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#NewTopicTitle').val());
        listItem.set_item("TopicObjective", $('#NewTopicObjective').val());
        listItem.set_item("Type", $('#NewTopicType').val());
        listItem.set_item("ModuleID", itemID);
        listItem.set_item("WebPage", $('#NewTopicUrl').val());
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            var errArea = document.getElementById("ErrNewTopic");
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            $('#NewTopic').fadeOut(500, function () {
                $('#NewTopicTitle').val("");
                $('#NewTopicObjective').val("");
                $('#NewTopicType').val("Concept");
                $('#AddTopicClicker').fadeIn();
                $('#deleteEditModuleButton').fadeIn();
                $('#cancelEditModuleButton').fadeIn();
                $('#EditModuleTopicList').fadeIn();
                $('#saveEditModuleButton').fadeIn();
                var itemID = currentModuleItem.get_id()
                PopulateTopics(itemID);
            });
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllCourses");

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

// This function runs when the user cancels the creation of a new topic
function cancelNewTopic() {
    var errArea = document.getElementById("ErrNewTopic");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewTopic').fadeOut(500, function () {
        $('#AddTopicClicker').fadeIn();
        $('#deleteEditModuleButton').fadeIn();
        $('#cancelEditModuleButton').fadeIn();
        $('#saveEditModuleButton').fadeIn();
        $('#EditModuleTopicList').fadeIn();
        var itemID = currentModuleItem.get_id()
        PopulateTopics(itemID);
    });

}

// This function shows the details of an topic for editing when the user clicks a topic for a module
function showTopicDetails(itemID) {
    var errArea = document.getElementById("ErrAllCourses");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var errTopicArea = document.getElementById("ErrNewTopic");
    while (errTopicArea.hasChildNodes()) {
        errTopicArea.removeChild(errTopicArea.lastChild);
    }
    hideAllPanels();
    currentTopicItem = topicList.getItemById(itemID);
    context.load(currentTopicItem);
    context.executeQueryAsync(
        function () {
            $('#editTopicTitle').val(currentTopicItem.get_fieldValues()["Title"]);            
            $('#editTopicObjective').val(currentTopicItem.get_fieldValues()["TopicObjective"]);
            $('#editTopicUrl').val(currentTopicItem.get_fieldValues()["WebPage"].get_url());
            $('#editTopicType').val(currentTopicItem.get_fieldValues()["Type"]);
            $('#TopicDetails').fadeIn(500, null);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user attempts to delete a previously-saved topic
function deleteEditTopic() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    currentTopicItem.deleteObject();
    context.executeQueryAsync(
        function () {
            clearEditTopicForm();
            var itemID = currentModuleItem.get_id()
            PopulateTopics(itemID);
            showModuleDetails(itemID);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user cancels the editing of an existing topic
function cancelEditTopic() {
    clearEditTopicForm();
}

// This function runs when the user attempts to update the details of a previously-saved topic
function saveEditTopic() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if (($('#editTopicTitle').val() == "") || ($('#editTopicObjective').val() == "")) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("All fields are required!"));
        errArea.appendChild(divMessage);
    }
    else if (!($('#editTopicUrl').val().startsWith("http://")) && !($('#editTopicUrl').val().startsWith("https://"))) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Please enter a full URL, including http:// or https://"));
        errArea.appendChild(divMessage);
    }
    else {
        currentTopicItem.set_item("Title", $('#editTopicTitle').val());
        currentTopicItem.set_item("TopicObjective", $('#editTopicObjective').val());
        currentTopicItem.set_item("Type", $('#editTopicType').val());
        currentTopicItem.set_item("WebPage", $('#editTopicUrl').val());
        currentTopicItem.update();
        context.load(currentTopicItem);
        context.executeQueryAsync(function () {
            clearEditTopicForm();
            var itemID = currentModuleItem.get_id()
            PopulateTopics(itemID);
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllCourses");

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

// This function clears all input elements in the 'Edit Topic' UI
function clearEditTopicForm() {
    var errArea = document.getElementById("ErrAllCourses");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#TopicDetails').fadeOut(500, function () {
        $('#TopicDetails').hide();
        $('#editTopicTitle').val("");
        $('#editTopicObjective').val("");
        $('#editTopicUrl').val("");
        $('#editTopicType').val("Concept");
        $('#EditTopicTypeHelp').hide();
        $('#ModuleDetails').fadeIn();
    });
}

// This function validates the data for a course to see if it is consistent with a few simple rules.
// The rules for validating a course are:
// 1. There must be at least two modules. There is no upper limit.
// 2. Each module in the course must have at least two topics. There is no upper limit.
function validateAndPublishCurrentCourse() {
    var courseID = currentCourseItem.get_id();
    var isValidAllModulesHaveAtLeastTwoTopics = true;
    var moduleList = web.get_lists().getByTitle('Module');
    var moduleQuery = new SP.CamlQuery();
    moduleQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='CourseID' LookupId='TRUE' /><Value Type='Lookup'>"
        + courseID
        + "</Value></Eq></Where></Query></View>");
    var moduleItems = moduleList.getItems(moduleQuery);
    context.load(moduleItems);
    var topicList = web.get_lists().getByTitle('Topic');
    var topicQuery = SP.CamlQuery.createAllItemsQuery();
    var topicItems = topicList.getItems(topicQuery);
    context.load(topicItems);
    context.executeQueryAsync(
        function () {
            var moduleItemEnumerator = moduleItems.getEnumerator();
            var moduleCount = 0;
            while (moduleItemEnumerator.moveNext()) {
                moduleCount++;
                var moduleItem = moduleItemEnumerator.get_current();
                var moduleID = moduleItem.get_id();
                var topicItemEnumerator = topicItems.getEnumerator();
                var topicCount = 0;
                while (topicItemEnumerator.moveNext()) {
                    var answerItem = topicItemEnumerator.get_current();
                    if (answerItem.get_fieldValues()["ModuleID"].get_lookupId() == moduleID) {
                        topicCount++;
                    }
                }
                if (topicCount < 2) {
                    isValidAllModulesHaveAtLeastTwoTopics = false;
                }
            }
            if (moduleCount < 2) {
                alert("Course is NOT valid. A course requires at least two modules.");
            }
            else if (!isValidAllModulesHaveAtLeastTwoTopics) {
                alert("Course is NOT valid. Each module requires at least two topics.");
            }
            else {
                alert("Course is valid.");
            }
        },
        function (sender, args) {
            alert("An error occurred when attempting to validate the course. Validation cannot continue because of the the following error: " + args.get_message());

        });
}

function showEditTopicTypeHelp() {
    $('#EditTopicTypeHelp').fadeIn(500, null);
}

function showNewTopicTypeHelp() {
    $('#NewTopicTypeHelp').fadeIn(500, null);
}

function hideEditTopicTypeHelp() {
    $('#EditTopicTypeHelp').fadeOut(500, null);
}

function hideNewTopicTypeHelp() {
    $('#NewTopicTypeHelp').fadeOut(500, null);
}

//
function testNewTopicUrl() {
    if (($('#NewTopicUrl').val().startsWith("http://")) || ($('#NewTopicUrl').val().startsWith("https://"))) {
        window.open($('#NewTopicUrl').val(), "_blank");
    }
    else {
        alert("Please enter a full URL, including http:// or https://");
    }
}

function testEditTopicUrl() {
    if (($('#editTopicUrl').val().startsWith("http://")) || ($('#editTopicUrl').val().startsWith("https://"))) {
        window.open($('#editTopicUrl').val(), "_blank");
    }
    else {
        alert("Please enter a full URL, including http:// or https://");

    }
}

/**********************************************************
    The functions that follow apply to the process of a 
    student studying a course
**********************************************************/
// This function runs if the current user does not have 'ManageWeb' permissions. We consider that that means they are a student
// rather than being able to create courses.
function showCourseList() {
    hideAllPanels();

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("ErrAllCoursesStudent");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasCourses = false;
    courseList = web.get_lists().getByTitle('TrainingCourse');
   // moduleList = web.get_lists().getByTitle('Module');
   // topicList = web.get_lists().getByTitle('Topic');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = courseList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            // Remove all nodes from the course <DIV> so we have a clean space to write to
            var courseTable = document.getElementById("AllCoursesStudentList");
            while (courseTable.hasChildNodes()) {
                courseTable.removeChild(courseTable.lastChild);
            }

            // Iterate through the Quiz list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the course name 
                var course = document.createElement("div");
                var courseLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                course.appendChild(courseLabel);

                // Add an ID to the course DIV
                course.id = listItem.get_id();

                // Add an class to the lead DIV
                course.className = "item";

                // Add an onclick event to show the lead details
                $(course).click(function (sender) {
                    startCourse(sender.target.id);
                });

                // Add the lead div to the UI
                courseTable.appendChild(course);
                hasCourses = true;
            }
            if (!hasCourses) {
                var noCourses = document.createElement("div");
                noCourses.appendChild(document.createTextNode("There are currently no courses."));
                courseTable.appendChild(noCourses);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get Courses. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
    $('#AllCoursesStudent').fadeIn(500, null);
}

// To start with, this function validates the data for a course to see if it is consistent with a few simple rules.
// The rules for validating a course are:
// 1. There must be at least two modules. There is no upper limit.
// 2. Each module in the course must have at least two topics. There is no upper limit.

function startCourse(courseID) {
    currentCourseItem = courseList.getItemById(courseID);
    var courseName;
    var courseObjective;
    context.load(currentCourseItem);
    var isValidAllModulesHaveAtLeastTwoTopics = true;
    var moduleList = web.get_lists().getByTitle('Module');
    var moduleQuery = new SP.CamlQuery();
    moduleQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='CourseID' LookupId='TRUE' /><Value Type='Lookup'>"
        + courseID
        + "</Value></Eq></Where></Query></View>");
    var moduleItems = moduleList.getItems(moduleQuery);
    context.load(moduleItems);
    var topicList = web.get_lists().getByTitle('Topic');
    var topicQuery = SP.CamlQuery.createAllItemsQuery();
    var topicItems = topicList.getItems(topicQuery);
    context.load(topicItems);
    context.executeQueryAsync(
        function () {
            courseName = currentCourseItem.get_fieldValues()["Title"];
            courseObjective = currentCourseItem.get_fieldValues()["CourseObjective"];
            moduleTitlesArray = [];
            moduleObjectivesArray = [];
            topicTitlesArray = [];
            topicObjectivesArray = [];
            topicUrlsArray = [];
            topicTypesArray = [];
            var moduleItemEnumerator = moduleItems.getEnumerator();
            var moduleCount = 0;
            while (moduleItemEnumerator.moveNext()) {
                moduleCount++;
                var moduleItem = moduleItemEnumerator.get_current();
                var moduleID = moduleItem.get_id();
                moduleTitlesArray.push(moduleItem.get_fieldValues()["Title"]);
                moduleObjectivesArray.push(moduleItem.get_fieldValues()["ModuleObjective"]);
                var topicItemEnumerator = topicItems.getEnumerator();
                var topicCount = 0;
                var topicTitles = [];
                var topicObjectives = [];
                var topicUrls = [];
                var topicTypes = [];
                while (topicItemEnumerator.moveNext()) {
                    var topicItem = topicItemEnumerator.get_current();
                    if (topicItem.get_fieldValues()["ModuleID"].get_lookupId() == moduleID) {
                        topicCount++;
                        topicTitles.push(topicItem.get_fieldValues()["Title"]);
                        topicObjectives.push(topicItem.get_fieldValues()["TopicObjective"]);
                        topicUrls.push(topicItem.get_fieldValues()["WebPage"].get_url());
                        topicTypes.push(topicItem.get_fieldValues()["Type"]);
                    }
                }
                topicTitlesArray.push(topicTitles);
                topicObjectivesArray.push(topicObjectives);
                topicUrlsArray.push(topicUrls);
                topicTypesArray.push(topicTypes);
                if (topicCount < 2) {
                    isValidAllModulesHaveAtLeastTwoTopics = false;
                }
            }
            if (moduleCount < 2) {
                alert("Course is NOT valid. A courseware designer has not yet finished creating this course.");
            }
            else if (!isValidAllModulesHaveAtLeastTwoTopics) {
                alert("Course is NOT valid. A courseware designer has not yet finished creating this course.");
            }
            else {
                buildCourseUI(courseName, courseObjective);
            }
        },
        function (sender, args) {
            alert("An error occurred when attempting to validate the course. Validation cannot continue because of the the following error: " + args.get_message());

        });
}

//
function buildCourseUI(courseName, courseObjective) {

    // Remove all nodes from the Nav <DIV> so we have a clean space to write to 
    var navArea = document.getElementById("PageNavigation");
    while (navArea.hasChildNodes()) {
        navArea.removeChild(navArea.lastChild);
    }

    // Clear the content area
    var contentArea = document.getElementById("PageContent");
    while (contentArea.hasChildNodes()) {
        contentArea.removeChild(contentArea.lastChild);
    }
    var welcome = document.createElement("h1");
    var welcomeMessage = document.createTextNode("Welcome to " + courseName + "!");
    welcome.appendChild(welcomeMessage);
    contentArea.appendChild(welcome);

    var courseObjectives = document.createElement("h2");
    var courseObjectivesMessage = document.createTextNode("After completing this course, you will be able to:");
    courseObjectives.appendChild(courseObjectivesMessage);
    contentArea.appendChild(courseObjectives); 

    var objectiveList = document.createElement("ul");

    // Build the table of contents links
    for (var modEnum = 0; modEnum < moduleTitlesArray.length; modEnum++) {
        var module = document.createElement("div");
        var moduleLabel = document.createTextNode(moduleTitlesArray[modEnum]);
        module.appendChild(moduleLabel);

        var objective = document.createElement("li");
        var objectiveText = document.createTextNode(moduleObjectivesArray[modEnum]);
        objective.appendChild(objectiveText);
        objectiveList.appendChild(objective);

        // Add an ID to the Module DIV
        module.id = "ModuleClicker" + modEnum;

        // Add an class to the Module DIV
        module.className = "moduleNavigationItem";

        // Add an onclick event to show the PO details
        $(module).click(function (sender) {
            showModulePage(sender.target.id);
        });

        //Add the module div to the UI
        navArea.appendChild(module);

        // Add topics for the current module to the UI
        for (var topicEnum = 0; topicEnum < topicTitlesArray[modEnum].length; topicEnum++) {
            var topic = document.createElement("div");
            var topicLabel = document.createTextNode(topicTitlesArray[modEnum][topicEnum]);
            topic.appendChild(topicLabel);

            // Add an ID to the Module DIV
            topic.id = "TopicClicker" + modEnum + "%" + topicEnum;

            // Add an class to the Module DIV
            switch (topicTypesArray[modEnum][topicEnum]) {
                case "Concept":
                    topic.className = "topicConcept";
                    break;
                case "Fact":
                    topic.className = "topicFact";
                    break;
                case "Principle":
                    topic.className = "topicPrinciple";
                    break;
                case "Procedure":
                    topic.className = "topicProcedure";
                    break;
                case "Process":
                    topic.className = "topicProcess";
                    break;
                default:
                    break;
            }
            

            // Add an onclick event to show the PO details
            $(topic).click(function (sender) {
                showTopicPage(sender.target.id);
            });

            //Add the module div to the UI
            navArea.appendChild(topic);
        }
    }


    // Describe the course
    $('#PageTitle').text("Course: " + courseName);
    $('#PageObjective').text("Course Objective: " + courseObjective);
    contentArea.appendChild(objectiveList);

    $('#CourseUI').fadeIn(500, null);
}

function showModulePage(module) {
   var moduleNumber = parseInt(module.replace("ModuleClicker", ""));
    $('#PageTitle').text("Module: " + moduleTitlesArray[moduleNumber]);
    $('#PageObjective').text("Module Objective: " + moduleObjectivesArray[moduleNumber]);
    // Clear the content area
    var contentArea = document.getElementById("PageContent");
    while (contentArea.hasChildNodes()) {
        contentArea.removeChild(contentArea.lastChild);
    }
    var moduleMessage = document.createElement("h2");
    var moduleMessageText = document.createTextNode("After completing this module, you will be able to: ");
    moduleMessage.appendChild(moduleMessageText);
    contentArea.appendChild(moduleMessage);
    var objectiveList = document.createElement("ul");
    for (var topicEnum = 0; topicEnum < topicObjectivesArray[moduleNumber].length; topicEnum++) {
        var topicObjective = document.createElement("li");
        var topicObjectiveText = document.createTextNode(topicObjectivesArray[moduleNumber][topicEnum]);
        topicObjective.appendChild(topicObjectiveText);
        objectiveList.appendChild(topicObjective);
    }

    contentArea.appendChild(objectiveList);
}

function showTopicPage(topic) {
    /*var moduleTitlesArray = [];
   var moduleObjectivesArray = [];
   var topicTitlesArray = [];
   var topicObjectivesArray = [];
   var topicUrlsArray = [];*/
    var topicData = topic.replace("TopicClicker", "");
    var iPosSeperator = topicData.indexOf("%");
    var moduleNumber = parseInt(topicData.substr(0, iPosSeperator));
    var topicNumber = parseInt(topicData.substr(iPosSeperator + 1));
    $('#PageTitle').text(topicTitlesArray[moduleNumber][topicNumber]);
    $('#PageObjective').text("Topic Objective: " + topicObjectivesArray[moduleNumber][topicNumber]);
    var contentArea = document.getElementById("PageContent");
    while (contentArea.hasChildNodes()) {
        contentArea.removeChild(contentArea.lastChild);
    }
    var pageFrame = document.createElement("IFRAME");
    pageFrame.setAttribute("src", topicUrlsArray[moduleNumber][topicNumber]);
    pageFrame.setAttribute("frameBorder", "0");
    pageFrame.style.width = 1024 + "px";
    pageFrame.style.height = 800 + "px";
    contentArea.appendChild(pageFrame);
}