// Variables used in various functions and callbacks
var context;
var web;
var list;
var listItems;
var listItem;
var user;
var userName;

var currentWins = 0;
var currentLosses = 0;
var BEGINNER_LEVEL = 3;
var MEDIUM_LEVEL = 3;
var EXPERT_LEVEL = 3;

//variable use for matching the result.
var equationResult;
//Counters
var countCorrectAns = 0;
var countWrongAns = 0;
//Timer
var timeleft = 41; 

//Levels
var choseLevel = 0;
var levelup = 0;
var temp;


// The following code runs when the DOM is ready and creates a context object which is needed 
// to use the SharePoint object model. 
$(document).ready(function () {
    $(".ms-siteicon-a img").attr('src', '../Images/equationPageIcon.png');
    reSetTimer();
    newPuzzel();
    getUserNameAndList();
});

//This  function is for redirecting to Tutorial mode.

//creating new puzzel
function newPuzzel() {
    var seperator = 1;
    
    switch (choseLevel) {
        case 0: seperator = 10;
            break;
        case 1: seperator = 30;
            break;
        case 2: seperator = 50;
            break;
        default:
            seperator += 5;
            break;
    }

    var operand1 = Math.floor(Math.random() * seperator) + 1;

    var operand2 = Math.floor(Math.random() * seperator) + 1;

    var operand3 = Math.floor(Math.random() * seperator);
    var operand4 = Math.floor(Math.random() * seperator);
    var operand5 = Math.floor(Math.random() * seperator);
    var operatorSelected;

    //new operator for every new equation
    var operator = Math.floor(Math.random() * 3);
    equationResult = 0;
    switch (operator) {
        case 0: equationResult = parseInt(operand1) + parseInt(operand2);
            operatorSelected = "+";
            break;
        case 1: equationResult = parseInt(operand1) - parseInt(operand2);
            operatorSelected = "-";
            break;
        case 2: equationResult = parseInt(operand1) * parseInt(operand2);
            operatorSelected = "*";
            break;
    }
    //randomly assigning Result to one of the option
    var assignResult = Math.floor(Math.random() * 4);
    switch (assignResult) {
        case 0: document.getElementById("optionResultbtn1").innerText = equationResult;
            //Assigning random values to rest of the buttons
            document.getElementById("optionResultbtn2").innerText = operand3;
            document.getElementById("optionResultbtn3").innerText = operand4;
            document.getElementById("optionResultbtn4").innerText = operand5;
            break;
        case 1: document.getElementById("optionResultbtn2").innerText = equationResult;
            //Assigning random values to rest of the buttons
            document.getElementById("optionResultbtn1").innerText = operand3;
            document.getElementById("optionResultbtn3").innerText = operand4;
            document.getElementById("optionResultbtn4").innerText = operand5;
            break;
        case 2: document.getElementById("optionResultbtn3").innerText = equationResult;
            //Assigning random values to rest of the buttons
            document.getElementById("optionResultbtn2").innerText = operand3;
            document.getElementById("optionResultbtn1").innerText = operand4;
            document.getElementById("optionResultbtn4").innerText = operand5;
            break;
        case 3:
            document.getElementById("optionResultbtn4").innerText = equationResult;
            //Assigning random values to rest of the buttons
            document.getElementById("optionResultbtn2").innerText = operand3;
            document.getElementById("optionResultbtn3").innerText = operand4;
            document.getElementById("optionResultbtn1").innerText = operand5;
            break;
    }

    // set the display values of opeands,operator and one of the option btn
    document.getElementById("operand1valueLabel").innerText = operand1;
    document.getElementById("operand1valueLabe2").innerText = operand2;
    document.getElementById("operatorValueLabel").innerHTML = operatorSelected;
    document.getElementById("displayResultLabel").innerText = "?";


}

function resultSelected(number) {
    var id;
    if (number == 1) { id = "optionResultbtn1"; }
    if (number == 2) { id = "optionResultbtn2"; }
    if (number == 3) { id = "optionResultbtn3"; }
    if (number == 4) { id = "optionResultbtn4"; }

    //display the value of the selected btn the display label. 
    document.getElementById("displayResultLabel").innerText = document.getElementById(id).innerText;
    checkResult();
}

function checkResult() {
    var happyIcon = document.getElementById("happyEnoticonEquation");
    var sadIcon = document.getElementById("sadEnoticonEquation");
    
    var currentBtn;
    if (timeleft > 0) {
        // match the selected result with the actual result
        if (equationResult == document.getElementById("displayResultLabel").innerText) {
            // correct answer, increment the correctCounter and update the label
            countCorrectAns++;
            switch (countCorrectAns) {
                case 0:
                    // there will no case as countCorrectAns will be at-least 1 under this loop
                case 1:
                    happyIcon.style.height = '40px';
                    happyIcon.style.width = '40px';
                    break;
                case 2:
                    happyIcon.style.height = '50px';
                    happyIcon.style.width = '50px';
                    break;
                case 3:
                    happyIcon.style.height = '60px';
                    happyIcon.style.width = '60px';
                    break;
               
               
                default:
                    happyIcon.style.height = '20px';
                    happyIcon.style.width = '20px';
                    break;
            }
            
            document.getElementById("correctCounterLabel").innerText = countCorrectAns;
            if (choseLevel == 0 && countCorrectAns == BEGINNER_LEVEL) {

                showDialog("Stage 1 Complete!", "Good job!! You have completed the BEGINNER's level");
                //Changing the css to display level 2nd.
                document.getElementById("equationMode").setAttribute("class", "equation2");
                // reset the values of the counters and thier respective labels
                document.getElementById("correctCounterLabel").innerText = 0;
                countCorrectAns = 0;
                document.getElementById("wrongCounterLabel").innerText = 0;
                countWrongAns = 0;
                //increment the level
                choseLevel++;
                //reset the timer
                timeleft = 41;
                //reset sizes of correct and wrong smilies
                happyIcon.style.height = '20px';
                happyIcon.style.width = '20px';
                sadIcon.style.height = '20px';
                sadIcon.style.width = '20px';
                // create new  equation
                newPuzzel();
                levelup = 1;
            }
            if (choseLevel == 1 && countCorrectAns == MEDIUM_LEVEL) {
                showDialog("Stage 2 Complete!", "Great!! You have completed the MEDIUM level");
                //Changing the css to display level 3rd.
                document.getElementById("equationMode").setAttribute("class", "equation3");
                // reset the values of the counters and thier respective labels
                document.getElementById("correctCounterLabel").innerText = 0;
                countCorrectAns = 0;
                document.getElementById("wrongCounterLabel").innerText = 0;
                countWrongAns = 0;
                //increment the level
                choseLevel++;
                //reset the timer
                timeleft = 41;
                //reset sizes of correct and wrong smilies
                happyIcon.style.height = '20px';
                happyIcon.style.width = '20px';
                sadIcon.style.height = '20px';
                sadIcon.style.width = '20px';
                // create new  equation
                newPuzzel();
                levelup = 2;
            }
            if (choseLevel == 2 && countCorrectAns == EXPERT_LEVEL) {
                //Record the score into the SharePoint List.
                showDialog("Stage 3 Complete!", "Awesome - You won the game!!");
                //redirect the user to the main page of the game.
                window.location.href = "../Pages/Default.aspx";
                //reset the counters
                document.getElementById("correctCounterLabel").innerText = 0;
                countCorrectAns = 0;
                document.getElementById("wrongCounterLabel").innerText = 0;
                countWrongAns = 0;
                //reset sizes of correct and wrong smilies
                happyIcon.style.height = '20px';
                happyIcon.style.width = '20px';
                sadIcon.style.height = '20px';
                sadIcon.style.width = '20px';
                recordScore('Wins');
            }
            else {
                if (timeleft > 0) {
                    //give extra 5 sec each time the user give correct answer.
                    timeleft = timeleft + 5;
                    newPuzzel();

                }
               
            }

        }
        else {

            countWrongAns++;
            switch (countWrongAns) {
                case 0:
                    // there will no case as countCorrectAns will be at-least 1 under this loop
                case 1:
                    sadIcon.style.height = '40px';
                    sadIcon.style.width = '40px';
                    break;
                case 2:
                    sadIcon.style.height = '50px';
                    sadIcon.style.width = '50px';
                    break;
                case 3:
                    sadIcon.style.height = '60px';
                    sadIcon.style.width = '60px';
                    break;

                case 4:
                    sadIcon.style.height = '65px';
                    sadIcon.style.width = '65px';
                    break;
                case 5:
                    sadIcon.style.height = '70px';
                    sadIcon.style.width = '70px';
                    break;
               default:
                    sadIcon.style.height = '75px';
                    sadIcon.style.width = '75px';
                    break;
                
            }
           
            if (timeleft > 0) {

                document.getElementById("wrongCounterLabel").innerText = countWrongAns;
                newPuzzel();
            }
         

        }

    }
  
}

//Timer function
function reSetTimer() {

    var startTimer = setInterval(function () { initTimers() }, 1000);
}

function initTimers() {
    document.getElementById("Timer").innerText = 41;
    newCountdown();
}

function newCountdown() {
    var happyIcon = document.getElementById("happyEnoticonEquation");
    var sadIcon = document.getElementById("sadEnoticonEquation");
    timeleft--;
    if (timeleft >= 0) {
        document.getElementById("Timer").innerText = timeleft;
    }
    else {
        
        showDialog("Out of Time!", "Sorry, time's up. (Let's try again)");
        document.getElementById("correctCounterLabel").innerText = 0;
        document.getElementById("wrongCounterLabel").innerText = 0;
        countCorrectAns = 0;
        countWrongAns = 0;
        choseLevel = 0;
        //reset sizes of correct and wrong smilies
        happyIcon.style.height = '20px';
        happyIcon.style.width = '20px';
        sadIcon.style.height = '20px';
        sadIcon.style.width = '20px';
        document.getElementById("equationMode").setAttribute("class", "equation1");
        newPuzzel();
        timeleft = 41;
        recordScore('Losses');

    }
}

// This function prepares, loads, and then executes a SharePoint query to get the current users information and the score list
function getUserNameAndList() {
    context = SP.ClientContext.get_current();
    web = context.get_web();
    list = web.get_lists().getByTitle('Score');
    user = web.get_currentUser();
    context.load(user);
    context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
}

// This function is executed if the above call is successful
// It replaces the contents of the 'message' element with the user name
function onGetUserNameSuccess() {
    userName = user.get_title();
    
}

// This function is executed if the above call fails
function onGetUserNameFail(sender, args) {
    showDialog("Oops! Something Went Wrong!", "Failed to get SharePoint User Object. Error: " + args.get_message());
}

//This function stores the score, player, and game info in the Scores list
function recordScore(state) {
        var gameDate = new Date();
        // We'll create a row for the user and game,  and set the Win/Loss/Date status
        if (state == "Wins") {
            var itemCreateInfo = new SP.ListItemCreationInformation();
            listItem = list.addItem(itemCreateInfo);
            listItem.set_item("Title", "Equation Mode");
            listItem.set_item("Player", user.get_title());
            listItem.set_item("Won", true);
            listItem.set_item("GameDate", gameDate);
            listItem.update();
            context.load(listItem);
            context.executeQueryAsync(function () { playerListId = listItem.get_id(); },
                function () {
                    showDialog("Oops! Something Went Wrong!", "Score could not be stored");
                    
                });
        }
        if (state == "Losses") {
            var itemCreateInfo = new SP.ListItemCreationInformation();
            listItem = list.addItem(itemCreateInfo);
            listItem.set_item("Title", "Equation Mode");
            listItem.set_item("Player", user.get_title());
            listItem.set_item("Won", false);
            listItem.set_item("GameDate", gameDate);
            listItem.update();
            context.load(listItem);
            context.executeQueryAsync(function () { playerListId = listItem.get_id(); },
                function () {
                    showDialog("Oops! Something Went Wrong!", "Score could not be stored");
                   
                });

        }
    
}
function showDialog(dialogTitle, dialogMessage) {
    $(function () {
        $("#dialogDiv").attr("title", dialogTitle);
        $("#dialogDivMessage").text(dialogMessage);
        $("#dialogDiv").dialog({
            dialogClass: "no-close",
            modal: true,
            closeText: "x",
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
}






