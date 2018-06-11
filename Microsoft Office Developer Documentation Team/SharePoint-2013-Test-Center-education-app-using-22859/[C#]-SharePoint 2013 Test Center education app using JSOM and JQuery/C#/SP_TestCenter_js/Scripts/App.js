'use strict';

// Variables used to hold various SharePoint objects
var context = SP.ClientContext.get_current();
var web = context.get_web();
var user = web.get_currentUser();
var quizList;
var currentQuizItem;
var questionList;
var currentQuestionItem;
var answerList;
var currentAnswerItem;
var resultList;

// Variable used to hold quiz data as a student takes a test
var currentQuestion = 0;
var testQuestions = 0;
var currentChoices = [];
var Questions;
var CorrectFeedbacks;
var IncorrectFeedbacks;
var ChoiceSets;
var AnswerSets;
var UserName;
var QuizName;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("GeneralError");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Load the web and user objects, and then see if the current user is to be considered a test administrator or a student.
    context.load(web, 'EffectiveBasePermissions');
    context.load(user);
    user.retrieve();
    context.executeQueryAsync(
        function () {

            // If current user has ManageWeb permissions, we'll consider them a test administrator.
            // Otherwise, we'll consider them a student.
            if (web.get_effectiveBasePermissions().has(SP.PermissionKind.manageWeb)) {
                showTestDesigner();
            }
            else {
                showTestList();
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
    $('#AddTest').hide();
    $('#TestDetails').hide();
    $('#NewQuestion').hide();
    $('#QuestionDetails').hide();
    $('#NewAnswer').hide();
    $('#AnswerDetails').hide();
    $('#ScoreReports').hide();
}

/**********************************************************
    Until noted otherwise, the functions that follow 
    apply to the test design process
**********************************************************/

// This function runs if the current user has 'ManageWeb' permissions. We consider that that means they can create tests
// rather than being a student
function showTestDesigner() {
    hideAllPanels();

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("ErrAllTestsAdmin");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasTests = false;
    quizList = web.get_lists().getByTitle('Quiz');
    questionList = web.get_lists().getByTitle('Question');
    answerList = web.get_lists().getByTitle('Answer');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = quizList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            // Remove all nodes from the test <DIV> so we have a clean space to write to
            var testTable = document.getElementById("AllTestsAdminList");
            while (testTable.hasChildNodes()) {
                testTable.removeChild(testTable.lastChild);
            }

            // Iterate through the Quiz list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                    // Create a DIV to display the test name 
                    var test = document.createElement("div");
                    var testLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                    test.appendChild(testLabel);

                    // Add an ID to the test DIV
                    test.id = listItem.get_id();

                    // Add an class to the lead DIV
                    test.className = "item";

                    // Add an onclick event to show the lead details
                    $(test).click(function (sender) {
                        showTestDetails(sender.target.id);
                    });

                    // Add the lead div to the UI
                    testTable.appendChild(test);
                    hasTests = true;
            }
            if (!hasTests) {
                var noTests = document.createElement("div");
                noTests.appendChild(document.createTextNode("There are currently no tests."));
                testTable.appendChild(noTests);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get tests. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
    $('#AllTestsAdmin').fadeIn(500, null);
}

// This function shows the UI for adding new tests
function addNewTest() {
    hideAllPanels();
    $('#AddTest').fadeIn(500, null);
}

// This function runs when the user attmempts to save a new test
function saveNewTest() {
    if ($('#newTestName').val() == "") {
        var errArea = document.getElementById("ErrAllTestsAdmin");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Test Name is a required field."));
        errArea.appendChild(divMessage);
    }
    else {
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var listItem = quizList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#newTestName').val());
        listItem.set_item("Level", $('#newTestLevel').val());
        if ($('#newTestAllowMultipleAttempts').is(':checked')) {
            listItem.set_item("AllowMultipleAttempts", true);
        }
        else {
            listItem.set_item("AllowMultipleAttempts", false);
        }
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            clearNewTestForm();
            showTestDesigner();
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllTestsAdmin");

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

// This function runs when the user cancels the creation of a new test
function cancelNewTest() {
    clearNewTestForm();
}

// This function clears all input elements in the 'New Test' UI
function clearNewTestForm() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AddTest').fadeOut(500, function () {
        $('#AddTest').hide();
        $('#newTestName').val("");
        $('#newTestLevel').val("100");
        $('#newTestAllowMultipleAttempts').removeAttr('checked');
    });
}

// This function shows the details of a previously-saved test
function showTestDetails(itemID) {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var errQuestionArea = document.getElementById("ErrNewQuestion");
    while (errQuestionArea.hasChildNodes()) {
        errQuestionArea.removeChild(errQuestionArea.lastChild);
    }
    hideAllPanels();
    $('#AddQuestionClicker').fadeIn();
    $('#deleteEditTestButton').fadeIn();
    $('#cancelEditTestButton').fadeIn();
    $('#saveEditTestButton').fadeIn();
    $('#validateTestButton').fadeIn();
    $('#EditTestQuestionList').fadeIn();
    currentQuizItem = quizList.getItemById(itemID);
    context.load(currentQuizItem);
    context.executeQueryAsync(
        function () {
            $('#editTestName').val(currentQuizItem.get_fieldValues()["Title"]);
            $('#editTestLevel').val(currentQuizItem.get_fieldValues()["Level"]);
            if (currentQuizItem.get_fieldValues()["AllowMultipleAttempts"]) {
                $('#editTestAllowMultipleAttempts').prop('checked', true);
            }
            else {
                $('#editTestAllowMultipleAttempts').prop('checked', false);
            }
            $('#TestDetails').fadeIn(500, null);
            PopulateQuestions(itemID);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user attempts to delete a previously-saved test
function deleteEditTest() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var questionList = web.get_lists().getByTitle('Question');
    var questionQuery = new SP.CamlQuery();
    questionQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuizID' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentQuizItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var QuestionItems = questionList.getItems(questionQuery);
    context.load(QuestionItems);
    context.executeQueryAsync(
        function () {
            if (QuestionItems.get_count() >= 1) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("This test has questions and cannot be deleted. Please delete all questions before deleting this test."));
                errArea.appendChild(divMessage);
            }
            else {
                currentQuizItem.deleteObject();
                context.executeQueryAsync(
                    function () {
                        clearEditTestForm();
                        showTestDesigner();
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
            divMessage.appendChild(document.createTextNode("Failed to check questions. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user cancels the editing of test data for a previously-saved test
function cancelEditTest() {
    clearEditTestForm();
}

// This function runs when the user attempts to update the details of a previously-saved test
function saveEditTest() {
    if ($('#editTestName').val() == "") {
        var errArea = document.getElementById("ErrAllTestsAdmin");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Test Name is a required field."));
        errArea.appendChild(divMessage);
    }
    else {
        currentQuizItem.set_item("Title", $('#editTestName').val());
        currentQuizItem.set_item("Level", $('#editTestLevel').val());
        if ($('#editTestAllowMultipleAttempts').is(':checked')) {
            currentQuizItem.set_item("AllowMultipleAttempts", true);
        }
        else {
            currentQuizItem.set_item("AllowMultipleAttempts", false);
        }
        currentQuizItem.update();
        context.load(currentQuizItem);
        context.executeQueryAsync(function () {
            clearEditTestForm();
            showTestDesigner();
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllTestsAdmin");

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

// This function clears all input elements in the 'Edit Test' UI
function clearEditTestForm() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#TestDetails').fadeOut(500, function () {
        $('#TestDetails').hide();
        $('#editTestName').val("");
        $('#editTestLevel').val("100");
        $('#editTestAllowMultipleAttempts').removeAttr('checked');
    });
}

// This function shows the UI for adding a new question to a test
function addNewQuestion() {
    var errArea = document.getElementById("ErrNewQuestion");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewQuestionText').val("");
    $('#AddQuestionClicker').fadeOut();
    $('#deleteEditTestButton').fadeOut();
    $('#cancelEditTestButton').fadeOut();
    $('#EditTestQuestionList').fadeOut();
    $('#saveEditTestButton').fadeOut();
    $('#validateTestButton').fadeOut(500, function () { $('#NewQuestion').fadeIn(500, null) });
}

// This function runs when the user attempts to save a new question
function saveNewQuestion() {
    var errArea = document.getElementById("ErrNewQuestion");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if (($('#NewQuestionText').val() == "") || ($('#NewQuestionCorrectFeedback').val() == "") || ($('#NewQuestionIncorrectFeedback').val() == "")) {
        var divMessage = document.createElement("DIV");
        divMessage.appendChild(document.createTextNode("All fields are required!"));
        errArea.appendChild(divMessage);
    }
    else {
        var itemID = currentQuizItem.get_id()
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var questionList = web.get_lists().getByTitle('Question');
        var listItem = questionList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#NewQuestionText').val());
        listItem.set_item("CorrectFeedback", $('#NewQuestionCorrectFeedback').val());
        listItem.set_item("IncorrectFeedback", $('#NewQuestionIncorrectFeedback').val());
        listItem.set_item("QuizID", itemID);
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            var errArea = document.getElementById("ErrNewQuestion");
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            $('#NewQuestion').fadeOut(500, function () {
                $('#AddQuestionClicker').fadeIn();
                $('#deleteEditTestButton').fadeIn();
                $('#cancelEditTestButton').fadeIn();
                $('#saveEditTestButton').fadeIn();
                $('#validateTestButton').fadeIn();
                $('#EditTestQuestionList').fadeIn();
                var itemID = currentQuizItem.get_id()
                PopulateQuestions(itemID);
            });
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllTestsAdmin");

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

// This function runs when the user cancels the creation of a new question
function cancelNewQuestion() {
    var errArea = document.getElementById("ErrNewQuestion");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewQuestion').fadeOut(500, function () {
        $('#AddQuestionClicker').fadeIn();
        $('#deleteEditTestButton').fadeIn();
        $('#cancelEditTestButton').fadeIn();
        $('#saveEditTestButton').fadeIn();
        $('#validateTestButton').fadeIn();
        $('#EditTestQuestionList').fadeIn();
        var itemID = currentQuizItem.get_id()
        PopulateQuestions(itemID);
    });
    
}

// This function shows a list of questions (which can be clicked to edit them)
function PopulateQuestions(itemID) {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Create a CAML query that retrieves the questions for the current quiz
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuizID' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = questionList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync 
            var questionTable = document.getElementById("EditTestQuestionList");

            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (questionTable.hasChildNodes()) {
                questionTable.removeChild(questionTable.lastChild);
            }

            // Iterate through the Questions
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Question text
                var question = document.createElement("div");
                var questionLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                question.appendChild(questionLabel);

                // Add an ID to the Question DIV
                question.id = listItem.get_id();

                // Add an class to the Question DIV
                question.className = "questionItem";

                // Add an onclick event to show the PO details
                $(question).click(function (sender) {
                    showQuestionDetails(sender.target.id);
                });

                //Add the question div to the UI
                questionTable.appendChild(question);
                
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get questions. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function shows the details of a question for editing when the user clicks a question in the test designer
function showQuestionDetails(itemID) {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var errAnswerArea = document.getElementById("ErrNewAnswer");
    while (errAnswerArea.hasChildNodes()) {
        errAnswerArea.removeChild(errAnswerArea.lastChild);
    }
    hideAllPanels();
    $('#NewAnswer').fadeOut(500, function () {
        $('#AddAnswerClicker').fadeIn();
        $('#deleteEditQuestionButton').fadeIn();
        $('#cancelEditQuestionButton').fadeIn();
        $('#saveEditQuestionButton').fadeIn();
        $('#EditQuestionAnswerList').fadeIn();
    });
    currentQuestionItem = questionList.getItemById(itemID);
    context.load(currentQuestionItem);
    context.executeQueryAsync(
        function () {
            $('#editQuestionText').val(currentQuestionItem.get_fieldValues()["Title"]);
            $('#editQuestionCorrectFeedback').val(currentQuestionItem.get_fieldValues()["CorrectFeedback"]);
            $('#editQuestionIncorrectFeedback').val(currentQuestionItem.get_fieldValues()["IncorrectFeedback"]);
            $('#QuestionDetails').fadeIn(500, null);
            PopulateAnswers(itemID);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function shows a list of questions (which can be clicked to edit them)
function PopulateAnswers(itemID) {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }

    // Create a CAML query that retrieves the answers for the current question
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuestionID' LookupId='TRUE' /><Value Type='Lookup'>"
        + itemID
        + "</Value></Eq></Where></Query></View>");
    var listItems = answerList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync 
            var answerTable = document.getElementById("EditQuestionAnswerList");

            // Remove all nodes from the PO <DIV> so we have a clean space to write to
            while (answerTable.hasChildNodes()) {
                answerTable.removeChild(answerTable.lastChild);
            }

            // Iterate through the Questions
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Question text
                var answer = document.createElement("div");
                var answerLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                answer.appendChild(answerLabel);

                // Add an ID to the Question DIV
                answer.id = listItem.get_id();

                // Add an class to the Question DIV
                answer.className = "questionItem";

                // Add an onclick event to show the PO details
                $(answer).click(function (sender) {
                    showAnswerDetails(sender.target.id);
                });

                //Add the question div to the UI
                answerTable.appendChild(answer);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get answers. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user attempts to delete a previously-saved question
function deleteEditQuestion() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var answerList = web.get_lists().getByTitle('Answer');
    var answerQuery = new SP.CamlQuery();
    answerQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuestionID' LookupId='TRUE' /><Value Type='Lookup'>"
        + currentQuestionItem.get_id()
        + "</Value></Eq></Where></Query></View>");
    var answerItems = answerList.getItems(answerQuery);
    context.load(answerItems);
    context.executeQueryAsync(
        function () {
            if (answerItems.get_count() >= 1) {
                var divMessage = document.createElement("DIV");
                divMessage.setAttribute("style", "padding:5px;");
                divMessage.appendChild(document.createTextNode("This question has answers and cannot be deleted. Please delete all answers before deleting this question."));
                errArea.appendChild(divMessage);
            }
            else {
                currentQuestionItem.deleteObject();
                context.executeQueryAsync(
                    function () {
                        clearEditQuestionForm();
                        var itemID = currentQuizItem.get_id()
                        PopulateQuestions(itemID);
                        showTestDesigner();
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
            divMessage.appendChild(document.createTextNode("Failed to check answers. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user cancels the editing of an existing question
function cancelEditQuestion() {
    clearEditQuestionForm();
}

// This function runs when the user attempts to update the details of a previously-saved question
function saveEditQuestion() {
    if (($('#editQuestionText').val() == "") || ($('#editQuestionCorrectFeedback').val() == "") || ($('#editQuestionIncorrectFeedback').val() == "")) {
        var errArea = document.getElementById("ErrAllTestsAdmin");

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
        currentQuestionItem.set_item("Title", $('#editQuestionText').val());
        currentQuestionItem.set_item("CorrectFeedback", $('#editQuestionCorrectFeedback').val());
        currentQuestionItem.set_item("IncorrectFeedback", $('#editQuestionIncorrectFeedback').val());
        currentQuestionItem.update();
        context.load(currentQuestionItem);
        context.executeQueryAsync(function () {
            clearEditQuestionForm();
            var itemID = currentQuizItem.get_id()
            PopulateQuestions(itemID);
            showTestDesigner();
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllTestsAdmin");

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

// This function clears all input elements in the 'Edit Question' UI
function clearEditQuestionForm() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#QuestionDetails').fadeOut(500, function () {
        $('#QuestionDetails').hide();
        $('#editQuestionText').val("");
        $('#editQuestionCorrectFeedback').val("");
        $('#editQuestionIncorrectFeedback').val("");
        $('#TestDetails').fadeIn();
    });
}

// This function shows the UI for adding a new answer to a question
function addNewAnswer() {
    var errArea = document.getElementById("ErrNewAnswer");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewAnswerText').val("");
    $('#AnswerIsCorrect').prop('checked', false);
    $('#AddAnswerClicker').fadeOut();
    $('#deleteEditQuestionButton').fadeOut();
    $('#cancelEditQuestionButton').fadeOut();
    $('#EditQuestionAnswerList').fadeOut();
    $('#saveEditQuestionButton').fadeOut(500, function () { $('#NewAnswer').fadeIn(500, null) });
}

// This function runs when the user attempts to save a new answer
function saveNewAnswer() {
    var errArea = document.getElementById("ErrNewAnswer");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if ($('#NewAnswerText').val() == "")  {
        var divMessage = document.createElement("DIV");
        //divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Answer Text is required!"));
        errArea.appendChild(divMessage);
    }
    else {
        var itemID = currentQuestionItem.get_id()
        var itemCreateInfo = new SP.ListItemCreationInformation();
        var answerList = web.get_lists().getByTitle('Answer');
        var listItem = answerList.addItem(itemCreateInfo);
        listItem.set_item("Title", $('#NewAnswerText').val());
        if ($('#AnswerIsCorrect').is(':checked')) {
            listItem.set_item("IsCorrect", true);
        }
        else {
            listItem.set_item("IsCorrect", false);
        }
        listItem.set_item("QuestionID", itemID);
        listItem.update();
        context.load(listItem);
        context.executeQueryAsync(function () {
            var errArea = document.getElementById("ErrNewAnswer");
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            $('#NewAnswer').fadeOut(500, function () {
                $('#NewAnswerText').val("");
                $('#AnswerIsCorrect').prop('checked', false);
                $('#AddAnswerClicker').fadeIn();
                $('#deleteEditQuestionButton').fadeIn();
                $('#cancelEditQuestionButton').fadeIn();
                $('#EditQuestionAnswerList').fadeIn();
                $('#saveEditQuestionButton').fadeIn();
                var itemID = currentQuestionItem.get_id()
                PopulateAnswers(itemID);
            });
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllTestsAdmin");

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

// This function runs when the user cancels the creation of a new answer
function cancelNewAnswer() {
    var errArea = document.getElementById("ErrNewAnswer");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#NewAnswer').fadeOut(500, function () {
        $('#AddAnswerClicker').fadeIn();
        $('#deleteEditQuestionButton').fadeIn();
        $('#cancelEditQuestionButton').fadeIn();
        $('#saveEditQuestionButton').fadeIn();
        $('#EditQuestionAnswerList').fadeIn();
        var itemID = currentQuestionItem.get_id()
        PopulateAnswers(itemID);
    });

}

// This function shows the details of an answer for editing when the user clicks a answer for a question
function showAnswerDetails(itemID) {
    var errArea = document.getElementById("ErrAllTestsAdmin");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var errAnswerArea = document.getElementById("ErrNewAnswer");
    while (errAnswerArea.hasChildNodes()) {
        errAnswerArea.removeChild(errAnswerArea.lastChild);
    }
    hideAllPanels();
    currentAnswerItem = answerList.getItemById(itemID);
    context.load(currentAnswerItem);
    context.executeQueryAsync(
        function () {
            $('#editAnswerText').val(currentAnswerItem.get_fieldValues()["Title"]);
            if (currentAnswerItem.get_fieldValues()["IsCorrect"]) {
                $('#editAnswerIsCorrect').prop('checked', true);
            }
            else {
                $('#editAnswerIsCorrect').prop('checked', false);
            }
            $('#AnswerDetails').fadeIn(500, null);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user attempts to delete a previously-saved answer
function deleteEditAnswer() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    currentAnswerItem.deleteObject();
    context.executeQueryAsync(
        function () {
            clearEditAnswerForm();
            var itemID = currentQuestionItem.get_id()
            PopulateAnswers(itemID);
            showQuestionDetails(itemID);
        },
        function (sender, args) {
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode(args.get_message()));
            errArea.appendChild(divMessage);
        });
}

// This function runs when the user cancels the editing of an existing answer
function cancelEditAnswer() {
    clearEditAnswerForm();
}

// This function runs when the user attempts to update the details of a previously-saved answer
function saveEditAnswer() {
    if ($('#editAnswerText').val() == "") {
        var errArea = document.getElementById("ErrAllTestsAdmin");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Answer text is required!"));
        errArea.appendChild(divMessage);
    }
    else {
        currentAnswerItem.set_item("Title", $('#editAnswerText').val());
        if ($('#editAnswerIsCorrect').is(':checked')) {
            currentAnswerItem.set_item("IsCorrect", true);
        }
        else {
            currentAnswerItem.set_item("IsCorrect", false);
        }
        currentAnswerItem.update();
        context.load(currentAnswerItem);
        context.executeQueryAsync(function () {
            clearEditAnswerForm();
            var itemID = currentQuestionItem.get_id()
            PopulateAnswers(itemID);
        },
            function (sender, args) {
                var errArea = document.getElementById("ErrAllTestsAdmin");

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

// This function clears all input elements in the 'Edit Answer' UI
function clearEditAnswerForm() {
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    $('#AnswerDetails').fadeOut(500, function () {
        $('#AnswerDetails').hide();
        $('#editAnswerText').val("");
        $('#editAnswerIsCorrect').prop('checked', false);
        $('#QuestionDetails').fadeIn();
    });
}

// This function validates the data for a test to see if it is consistent with a few simple rules.
// The rules for validating a test are:
// 1. There must be at least one question. There is no upper limit.
// 2. Each question in the test must have at least two answers. There is no upper limit.
// 3. Each question in the test must have at least one answer that is marked as correct. 
// Note - It is of course OK to have multiple correct answers, and it is also OK for ALL answers to be correct if desired.
function validateCurrentTest() {
    var quizID = currentQuizItem.get_id();
    var isValidAtLeastOneQuestion = false;
    var isValidAllQuestionsHaveAtLeastTwoAnswers = true;
    var isValidAllQuestionsHaveAtLeastOneCorrectAnswer = true;
    var questionList = web.get_lists().getByTitle('Question');
    var questionQuery = new SP.CamlQuery();
    questionQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuizID' LookupId='TRUE' /><Value Type='Lookup'>"
        + quizID
        + "</Value></Eq></Where></Query></View>");
    var questionItems = questionList.getItems(questionQuery);
    context.load(questionItems);
    var answerList = web.get_lists().getByTitle('Answer');
    var answerQuery = SP.CamlQuery.createAllItemsQuery();
    var answerItems = answerList.getItems(answerQuery);
    context.load(answerItems);
    context.executeQueryAsync(
        function () {
            var questionItemEnumerator = questionItems.getEnumerator();
            while (questionItemEnumerator.moveNext()) {
                isValidAtLeastOneQuestion = true;
                var questionItem = questionItemEnumerator.get_current();
                var questionID = questionItem.get_id();
                var answerItemEnumerator = answerItems.getEnumerator();
                var answerCount = 0;
                var correctAnswerCount = 0;
                while (answerItemEnumerator.moveNext()) {
                    var answerItem = answerItemEnumerator.get_current();
                    if (answerItem.get_fieldValues()["QuestionID"].get_lookupId() == questionID) {
                        answerCount++;
                        if (answerItem.get_fieldValues()["IsCorrect"]) {
                            correctAnswerCount++;
                        }
                    }
                }
                if (answerCount < 2) {
                    isValidAllQuestionsHaveAtLeastTwoAnswers = false;
                }
                if (correctAnswerCount < 1) {
                    isValidAllQuestionsHaveAtLeastOneCorrectAnswer = false;
                }
            }
            if (!isValidAtLeastOneQuestion) {
                alert("Test is NOT valid. A test requires at least one question.");
            }
            else if (!isValidAllQuestionsHaveAtLeastTwoAnswers) {
                alert("Test is NOT valid. Each question requires at least two answers.");
            }
            else if (!isValidAllQuestionsHaveAtLeastOneCorrectAnswer) {
                alert("Test is NOT valid. Each question requires at least one correct answer.");
            }
            else {
                alert("Test is valid.");
            }
        },
        function (sender, args) {
            alert("An error occurred when attempting to validate the test. Validation cannot continue because of the the following error: " + args.get_message());

        });
}

// This function retrieved and renders scores and other result details for each student
// that has completed the selected test.
function showTestResults() {
    $('#TestDetails').fadeOut(500, function () {
        $('#ScoreReports').fadeIn(500, null);
    });
    $('#ScoreReportsTitle').text(currentQuizItem.get_fieldValues()["Title"] + " - Users/Scores");
    var quizID = currentQuizItem.get_id();
    var errArea = document.getElementById("ErrAllTestsAdmin");

    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    resultList = web.get_lists().getByTitle('Result');


    // Create a CAML query that retrieves the results for the current quiz
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuizID' LookupId='TRUE' /><Value Type='Lookup'>"
        + quizID
        + "</Value></Eq></Where></Query></View>");
    var listItems = resultList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync 
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            var listArea = document.getElementById("TestReportList");
            var hasResults = false;

            // Remove all nodes from the list <DIV> so we have a clean space to write to
            while (listArea.hasChildNodes()) {
                listArea.removeChild(listArea.lastChild);
            }
            

            // Iterate through the results
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                hasResults = true;
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the report data
                var score = document.createElement("div");
                var scoreLabel = document.createTextNode(listItem.get_fieldValues()["Student"] + ": " + listItem.get_fieldValues()["Score"] + "% (" + listItem.get_fieldValues()["TestDate"] + ")");
                score.appendChild(scoreLabel);

                // Add an class to the report DIV
                score.className = "formLabel";

                //Add the report div to the UI
                listArea.appendChild(scoreLabel);
                listArea.appendChild(document.createElement("br"));
            }
            if (!hasResults) {
                var score = document.createElement("div");
                var scoreLabel = document.createTextNode("No users have yet completed this test.");
                score.appendChild(scoreLabel);

                // Add an class to the reports DIV
                score.className = "formLabel";

                //Add the report div to the UI
                listArea.appendChild(scoreLabel);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync.
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get results. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
}

/**********************************************************
    The functions that follow apply to the process of a 
    student taking a test
**********************************************************/
// This function runs if the current user does not have 'ManageWeb' permissions. We consider that that means they are a student
// rather than being able to create tests.
function showTestList() {
    hideAllPanels();

    // Remove all nodes from the error <DIV> so we have a clean space to write to in case of errors
    var errArea = document.getElementById("ErrAllTestsStudent");
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    var hasTests = false;
    quizList = web.get_lists().getByTitle('Quiz');
    questionList = web.get_lists().getByTitle('Question');
    answerList = web.get_lists().getByTitle('Answer');
    resultList = web.get_lists().getByTitle('Result');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = quizList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {

            // Success returned from executeQueryAsync
            // Remove all nodes from the test <DIV> so we have a clean space to write to
            var testTable = document.getElementById("AllTestsStudentList");
            while (testTable.hasChildNodes()) {
                testTable.removeChild(testTable.lastChild);
            }

            // Iterate through the Quiz list
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the test name 
                var test = document.createElement("div");
                var testLabel = document.createTextNode(listItem.get_fieldValues()["Title"]);
                test.appendChild(testLabel);

                // Add an ID to the test DIV
                test.id = listItem.get_id();

                // Add an class to the lead DIV
                test.className = "item";

                // Add an onclick event to show the lead details
                $(test).click(function (sender) {
                    startTest(sender.target.id);
                });

                // Add the lead div to the UI
                testTable.appendChild(test);
                hasTests = true;
            }
            if (!hasTests) {
                var noTests = document.createElement("div");
                noTests.appendChild(document.createTextNode("There are currently no tests."));
                testTable.appendChild(noTests);
            }
        },
        function (sender, args) {

            // Failure returned from executeQueryAsync
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Failed to get tests. Error: " + args.get_message()));
            errArea.appendChild(divMessage);
        });
    $('#AllTestsStudent').fadeIn(500, null);
}

// The test process is:
// 1. Get student name. Then check whether the student has taken test before and whether re-takes are allowed
// 2. Then validate test to see if it is consistent with a few simple rules (as explained in one of the comments above).
// 3. If so, build some arrays to hold the test data and then randomize them so that they are not presented in the same way everytime
// 4. Build UI
// This function does the checking and validation parts of the above list
function startTest(quizID) {
    currentQuizItem = quizList.getItemById(quizID);
    var quizName;
    context.load(currentQuizItem);
    var userName = user.get_title();
    // Create a CAML query that retrieves the users who have already taken the current quiz
    var resultCamlQuery = new SP.CamlQuery();
    resultCamlQuery.set_viewXml("<View><Query><Where><And><Eq><FieldRef Name='Student' /><Value Type='Text'>"
        + userName
        + "</Value></Eq><Eq><FieldRef Name='QuizID' LookupId='TRUE' /><Value Type='Lookup'>"
        + quizID
        + "</Value></Eq></And></Where></Query></View>");
    var resultItems = resultList.getItems(resultCamlQuery);
    context.load(resultItems);
    var isValidAtLeastOneQuestion = false;
    var isValidAllQuestionsHaveAtLeastTwoAnswers = true;
    var isValidAllQuestionsHaveAtLeastOneCorrectAnswer = true;
    var questionList = web.get_lists().getByTitle('Question');
    var questionQuery = new SP.CamlQuery();
    questionQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='QuizID' LookupId='TRUE' /><Value Type='Lookup'>"
        + quizID
        + "</Value></Eq></Where></Query></View>");
    var questionItems = questionList.getItems(questionQuery);
    context.load(questionItems);
    var answerList = web.get_lists().getByTitle('Answer');
    var answerQuery = SP.CamlQuery.createAllItemsQuery();
    var answerItems = answerList.getItems(answerQuery);
    context.load(answerItems);
    context.executeQueryAsync(
        function () {
            if (!currentQuizItem.get_fieldValues()["AllowMultipleAttempts"]) {
                if (resultItems.get_count() > 0) {
                    alert("This test does not allow retakes and you have already completed it.");
                    return;
                }
            }
            quizName = currentQuizItem.get_fieldValues()["Title"];
            var questionsArray = [];
            var correctFeedbackArray = [];
            var incorrectFeedbackArray = [];
            var choicesArray = [];
            var answersArray = [];
            var questionItemEnumerator = questionItems.getEnumerator();
            while (questionItemEnumerator.moveNext()) {
                isValidAtLeastOneQuestion = true;
                var questionItem = questionItemEnumerator.get_current();
                var questionID = questionItem.get_id();
                questionsArray.push(questionItem.get_fieldValues()["Title"]);
                correctFeedbackArray.push(questionItem.get_fieldValues()["CorrectFeedback"]);
                incorrectFeedbackArray.push(questionItem.get_fieldValues()["IncorrectFeedback"]);
                var answerItemEnumerator = answerItems.getEnumerator();
                var answerCount = 0;
                var correctAnswerCount = 0;
                var choices = [];
                var answers = [];
                while (answerItemEnumerator.moveNext()) {
                    var answerItem = answerItemEnumerator.get_current();
                    if (answerItem.get_fieldValues()["QuestionID"].get_lookupId() == questionID) {
                        answerCount++;
                        choices.push(answerItem.get_fieldValues()["Title"]);
                        if (answerItem.get_fieldValues()["IsCorrect"]) {
                            correctAnswerCount++;
                            answers.push(1);
                        }
                        else {
                            answers.push(0);
                        }
                    }
                }
                choicesArray.push(choices);
                answersArray.push(answers);
                if (answerCount < 2) {
                    isValidAllQuestionsHaveAtLeastTwoAnswers = false;
                }
                if (correctAnswerCount < 1) {
                    isValidAllQuestionsHaveAtLeastOneCorrectAnswer = false;
                }
            }
            if (!isValidAtLeastOneQuestion) {
                alert("Test is NOT valid. A test requires at least one question. The test author must fix this issue before you can take the test.");
            }
            else if (!isValidAllQuestionsHaveAtLeastTwoAnswers) {
                alert("Test is NOT valid. Each question requires at least two answers. The test author must fix this issue before you can take the test.");
            }
            else if (!isValidAllQuestionsHaveAtLeastOneCorrectAnswer) {
                alert("Test is NOT valid. Each question requires at least one correct answer. The test author must fix this issue before you can take the test.");
            }
            else {
                buildTest(questionsArray, correctFeedbackArray, incorrectFeedbackArray, choicesArray, answersArray, userName, quizName);
            }
        },
        function (sender, args) {
            alert("An error occurred when attempting to validate the test. Validation cannot continue because of the the following error: " + args.get_message());

        });
}

// The following function randomizes the order of the questions, and the order of the answers in each question
function buildTest(questionsArray, correctFeedbackArray, incorrectFeedbackArray, choicesArray, answersArray, userName, quizName) {
    // Hold the number of questions in a variable because we're going to monkey around
    // with the arrays, so questionsArray won't always hold the same number of questions
    var numQuestions = questionsArray.length;

    // We're going to populate the following arrays with randomly REMOVED elements from the
    // original data sets. There are array functions in JavaScript that could help us,
    // but the sort of shuffling that they do puts you completely at the mercy of the often-poor 
    // randomizing algorithms of the browser. For example, if you then analyze the frequencies
    // of location in the set over a *decent number* of shuffles, then you'll
    // see that the positions of each item are biased towards their original
    // position (sometimes heavily so). And you'll probably get different biases in 
    // different browsers which would make testing a nightmare. (Or more specifically
    // the issue would fly straight over the head of every tester I've ever known, and
    // would go unreported.) Not a huge deal for something like this, where what you *really* want 
    // is 'generally different positions to stop the student from remebering sequences' rather than 
    // 'statistically random positions' but it's best to at least be aware of such things.
    // Here endeth the preaching, so we'll just get on with it:-)

    var randomizedQuestions = [];
    var randomizedCorrectFeedbacks = [];
    var randomizedIncorrectFeedbacks = [];
    var randomizedChoiceSets = [];
    var randomizedAnswerSets = [];

    // We'll randomly choose a question from the set, and we'll remove it from arrQuestions
    // and put it in randomizedQuestions
    for (var questionCounter = 0; questionCounter < numQuestions - 1; questionCounter++) {
        var element = Math.floor(Math.random() * questionsArray.length)
        var questionElement = element;
        randomizedQuestions.push(questionsArray.splice(questionElement, 1));

        // We need to make sure we bring the corresponding correct and incorrect feedbacks with us as we randomize the questions
        randomizedCorrectFeedbacks.push(correctFeedbackArray.splice(questionElement, 1));
        randomizedIncorrectFeedbacks.push(incorrectFeedbackArray.splice(questionElement, 1));

        // We also need to make sure we bring the corresponding sub-array data for choices and answers with us
        // as we move a specific question, which we'll do below. But before we do that, we'll randomize the order of the data
        // in the sub-array data for choices and answers, ensuring we also keep them synchronized with each other. We'll use
        // exactly the same approach to randomize the choices/answers as we do for the questions, which is to say 
        // we'll randomly choose a choice from the set, and we'll remove it from choicesArray
        // and put it in randomizedChoices. At the same time, we'll make sure we do the exact same operation for the answers.
        var numAnswers = choicesArray[questionElement].length;
        var randomizedChoices = [];
        var randomizedAnswers = [];
        for (var answerCounter = 0; answerCounter < numAnswers - 1; answerCounter++) {
            var answerElement = Math.floor(Math.random() * choicesArray[questionElement].length);
            randomizedChoices.push(choicesArray[questionElement].splice(answerElement, 1)[0]);
            randomizedAnswers.push(answersArray[questionElement].splice(answerElement, 1)[0]);
        }

        // We've left the last choice and last answer behind for this specific question, so 
        // we'll deal with that now.
        randomizedChoices.push(choicesArray[questionElement][0]);
        randomizedAnswers.push(answersArray[questionElement][0]);
        choicesArray[questionElement] = randomizedChoices;
        answersArray[questionElement] = randomizedAnswers;
        randomizedChoiceSets.push(choicesArray.splice(questionElement, 1)[0]);
        randomizedAnswerSets.push(answersArray.splice(questionElement, 1)[0]);
    }

    // We've left the last question & correct feedback & incorrect feedback & set of choices & set or answers behind, so 
    // we'll deal with that now.
    randomizedQuestions.push(questionsArray[0]);
    randomizedCorrectFeedbacks.push(correctFeedbackArray[0]);
    randomizedIncorrectFeedbacks.push(incorrectFeedbackArray[0]);
    var numAnswers = choicesArray[0].length;
    var randomizedChoices = [];
    var randomizedAnswers = [];
    for (var answerCounter = 0; answerCounter < numAnswers - 1; answerCounter++) {
        var answerElement = Math.floor(Math.random() * choicesArray[0].length);
        randomizedChoices.push(choicesArray[0].splice(answerElement, 1)[0]);
        randomizedAnswers.push(answersArray[0].splice(answerElement, 1)[0]);
    }
    randomizedChoices.push(choicesArray[0][0]);
    randomizedAnswers.push(answersArray[0][0]);
    choicesArray[questionElement] = randomizedChoices;
    answersArray[questionElement] = randomizedAnswers;
    randomizedChoiceSets.push(choicesArray.splice(questionElement, 1)[0]);
    randomizedAnswerSets.push(answersArray.splice(questionElement, 1)[0]);
    randomizedChoiceSets.push(choicesArray[0]);
    randomizedAnswerSets.push(answersArray[0]);

    // Now run the test
    currentQuestion = 1;
    testQuestions = randomizedQuestions.length;
    currentChoices = [];
    Questions = randomizedQuestions;
    CorrectFeedbacks = randomizedCorrectFeedbacks;
    IncorrectFeedbacks = randomizedIncorrectFeedbacks;
    ChoiceSets = randomizedChoiceSets;
    AnswerSets = randomizedAnswerSets;
    UserName = userName;
    QuizName = quizName;
    buildUI();
}

// This function builds the UI for the test
function buildUI() {

    // Enable/Disable/Show/Hide buttons as appropriate
    if (currentQuestion == 1) {
        $('#Previous').attr('disabled', 'disabled');
        $('#Previous').css('color', '#CFCFCF');
        $('#Previous').css('background-color', '#AAAAAA');
    }
    else {
        $('#Previous').removeAttr('disabled');
        $('#Previous').css('color', '#FFFFFF');
        $('#Previous').css('background-color', '#0072C6');
    }
    if (currentQuestion == testQuestions) {
        $('#Next').attr('disabled', 'disabled');
        $('#Next').css('color', '#CFCFCF');
        $('#Next').css('background-color', '#AAAAAA');
        $('#EndTest').show();
        $('#QuitTest').hide();
    }
    else {
        $('#Next').removeAttr('disabled');
        $('#Next').css('color', '#FFFFFF');
        $('#Next').css('background-color', '#0072C6');
        $('#EndTest').hide();
        $('#QuitTest').show();
    }


    // Set Question Info
    $('#TestName').text(QuizName);
    $('#TestPosition').text("Question " + currentQuestion.toString() + " of " + testQuestions.toString());

    // Set Question Text
    var questionLabel = document.getElementById("TestQuestionText");
    while (questionLabel.hasChildNodes()) {
        questionLabel.removeChild(questionLabel.lastChild);
    }
    var questionLabelText = document.createTextNode(Questions[currentQuestion - 1]);
    questionLabel.appendChild(questionLabelText);

    // Remove previous answers
    var answersList = document.getElementById("TestAnswerList");
    while (answersList.hasChildNodes()) {
        answersList.removeChild(answersList.lastChild);
    }

    //Build answers
    var numAnswers = AnswerSets[currentQuestion-1].length;
    var correctAnswers = 0;
    for (var answerEnum = 0; answerEnum < numAnswers; answerEnum++) {
        correctAnswers += AnswerSets[currentQuestion-1][answerEnum];
    }
    if (correctAnswers > 1) {
        // Build checkboxes
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            var answerText = ChoiceSets[currentQuestion - 1][choiceEnum];
            var answerCheckbox = document.createElement("input");
            answerCheckbox.type = "checkbox";
            answerCheckbox.id = "answer" + choiceEnum.toString();
            answerCheckbox.style.cursor = "pointer";
            if ((currentChoices.length) > (currentQuestion - 1)) {
                if (currentChoices[currentQuestion-1][choiceEnum] == 1) {
                    $(answerCheckbox).prop('checked', true);
                }
            }
            var answerLabel = document.createTextNode(answerText);
            answersList.appendChild(answerCheckbox);
            answersList.appendChild(answerLabel);
            answersList.appendChild(document.createElement("br"));
        }
    }
    else {
        // Build radio buttons
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            var answerText = ChoiceSets[currentQuestion - 1][choiceEnum];
            var answerRadio = document.createElement("input");
            answerRadio.type = "radio";
            answerRadio.name = "answers";
            answerRadio.id = "answer" + choiceEnum.toString();
            answerRadio.style.cursor = "pointer";
            if ((currentChoices.length) > (currentQuestion - 1)) {
                if (currentChoices[currentQuestion-1][choiceEnum] == 1) {
                    $(answerRadio).prop('checked', true);
                }
            }
            var answerLabel = document.createTextNode(answerText);
            answersList.appendChild(answerRadio);
            answersList.appendChild(answerLabel);
            answersList.appendChild(document.createElement("br"));
        }
    }
    $('#AllTestsStudent').fadeOut(500, function () { $('#TestUI').fadeIn(500, null) });
}

// This function runs when the user has been through all the questions and clicked 'Finish'.
function endTest() {
    // First, ensure the current question is marked
    if ((currentChoices.length - 1) < (currentQuestion - 1)) {
        var currentAnswer = [];
        var numAnswers = AnswerSets[currentQuestion - 1].length;
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            if ($('#answer' + choiceEnum.toString()).is(':checked')) {
                currentAnswer.push(1);
            }
            else {
                currentAnswer.push(0);
            }
        }
        currentChoices.push(currentAnswer);
    }
    else {
        var currentAnswer = [];
        var numAnswers = AnswerSets[currentQuestion - 1].length;
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            if ($('#answer' + choiceEnum.toString()).is(':checked')) {
                currentAnswer.push(1);
            }
            else {
                currentAnswer.push(0);
            }
        }
        currentChoices[currentQuestion - 1] = currentAnswer;
    }

    // Then calculate and display score and details
    var testReportTitle = document.getElementById("TestReportTitle");
    while (testReportTitle.hasChildNodes()) {
        testReportTitle.removeChild(testReportTitle.lastChild);
    }
    var testReportScore = document.getElementById("TestReportScore");
    while (testReportScore.hasChildNodes()) {
        testReportScore.removeChild(testReportScore.lastChild);
    }
    var testReportDetails = document.getElementById("TestReportDetails");
    while (testReportDetails.hasChildNodes()) {
        testReportDetails.removeChild(testReportDetails.lastChild);
    }
    var score = 0;
    for (var questionChecker = 0; questionChecker < testQuestions; questionChecker++) {
        var numAnswers = AnswerSets[questionChecker].length;
        var isThisAnswerCorrect = true;
        var hr = document.createElement("hr");
        testReportDetails.appendChild(hr);
        var qText = document.createElement("div");
        qText.className = "reportQuestion";
        qText.appendChild(document.createTextNode(Questions[questionChecker]));
        testReportDetails.appendChild(qText);
        var clear = document.createElement("div");
        clear.className = "clear";
        clear.appendChild(document.createTextNode(" "));
        testReportDetails.appendChild(clear);
        var answeredText = document.createElement("div");
        answeredText.appendChild(document.createTextNode("You answered:"));
        testReportDetails.appendChild(answeredText);
        for (var answerChecker = 0; answerChecker < numAnswers; answerChecker++) {
            //Build answers
            var numAnswers = AnswerSets[questionChecker].length;
            if (AnswerSets[questionChecker][answerChecker] != currentChoices[questionChecker][answerChecker]) {
                isThisAnswerCorrect = false;
            }
        }

        // Build checkboxes
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            var answerText = ChoiceSets[questionChecker][choiceEnum];
            var answerCheckbox = document.createElement("input");
            answerCheckbox.type = "checkbox";
            answerCheckbox.id = "answerReport" + choiceEnum.toString();
            answerCheckbox.disabled = true;
            if (currentChoices[questionChecker][choiceEnum] == 1) {
                    $(answerCheckbox).prop('checked', true);
                }
            var answerLabel = document.createTextNode(answerText);
            testReportDetails.appendChild(answerCheckbox);
            testReportDetails.appendChild(answerLabel);
            testReportDetails.appendChild(document.createElement("br"));
        }
        if (isThisAnswerCorrect) {
            score++;
            var feedbackText = document.createElement("div");
            feedbackText.appendChild(document.createTextNode(CorrectFeedbacks[questionChecker]));
            testReportDetails.appendChild(feedbackText);
        }
        else {
            var feedbackText = document.createElement("div");
            feedbackText.appendChild(document.createTextNode(IncorrectFeedbacks[questionChecker]));
            testReportDetails.appendChild(feedbackText);
        }
    }

    // Set Question Info
    $('#TestReportTitle').text("Score Report for " + QuizName);
    $('#TestReportScore').text("You scored " + score.toString() + " out of " + testQuestions.toString() + " (" + (score / testQuestions) * 100 + "%)");

    // Store the score for the current user
    var dtm = new Date();
    var itemCreateInfo = new SP.ListItemCreationInformation();
    var listItem = resultList.addItem(itemCreateInfo);
    listItem.set_item("Student", UserName);
    listItem.set_item("TestDate", dtm);
    listItem.set_item("Score", (score / testQuestions) * 100);
    listItem.set_item("QuizID", currentQuizItem.get_id());
    listItem.update();
    context.load(listItem);
    context.executeQueryAsync(function () {
        $('#TestUI').fadeOut(500, function () { $('#TestReport').fadeIn(500, null) });
    },
        function (sender, args) {
            var errArea = document.getElementById("ErrAllTestsStudent");

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

// This function ends a test without calculating or recording the score, and re-shows the lists of tests
function quitTest() {
    $('#TestUI').fadeOut(500, function () { $('#AllTestsStudent').fadeIn(500, null) });
}

// This function runs when the user clicks the 'Previous' button in a test
function showPreviousQuestion() {
    var currentAnswer = [];
    var numAnswers = AnswerSets[currentQuestion - 1].length;
    for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
        if ($('#answer' + choiceEnum.toString()).is(':checked')) {
            currentAnswer.push(1);
        }
        else {
            currentAnswer.push(0);
        }
    }
    currentChoices[currentQuestion - 1] = currentAnswer;
    currentQuestion--;
    buildUI();
}

// This function runs when the user clicks the 'Next' button in a test
function showNextQuestion() {
    if ((currentChoices.length - 1) < (currentQuestion - 1)) {
        var currentAnswer = [];
        var numAnswers = AnswerSets[currentQuestion - 1].length;
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            if ($('#answer' + choiceEnum.toString()).is(':checked')) {
                currentAnswer.push(1);
            }
            else {
                currentAnswer.push(0);
            }
        }
        currentChoices.push(currentAnswer);
    }
    else {
        var currentAnswer = [];
        var numAnswers = AnswerSets[currentQuestion - 1].length;
        for (var choiceEnum = 0; choiceEnum < numAnswers; choiceEnum++) {
            if ($('#answer' + choiceEnum.toString()).is(':checked')) {
                currentAnswer.push(1);
            }
            else {
                currentAnswer.push(0);
            }
        }
        currentChoices[currentQuestion - 1] = currentAnswer;
    }
    currentQuestion++;
    buildUI();
}

// This function runs when the user clicks the 'OK' button in a test report
function closeTestReport() {
    $('#TestReport').fadeOut(500, function () { $('#AllTestsStudent').fadeIn(500, null) });
}