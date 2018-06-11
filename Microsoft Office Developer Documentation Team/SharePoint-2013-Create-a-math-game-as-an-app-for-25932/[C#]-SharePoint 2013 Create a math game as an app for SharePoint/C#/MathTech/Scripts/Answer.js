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

//constants use to for changing the levels.
var BEGINNER_LEVEL = 3;
var MEDIUM_LEVEL = 3;
var EXPERT_LEVEL = 3;

//variables to store values for the equation.
var operand1InTheEquationlbl;
var operand2InTheEquationlbl;
var operatorInTheEquationlbl;

//count down variables
var countCorrectAns = 0;
var countWrongAns = 0;

//countdown timer
var timeleft = 41;
var choseLevel = 0;
var currentLevel = 0;

// stores the calculated value of the operands and operator selected by the user.
var userAnswered = 0;

// selected operator code
var operatorCode;
//


// The following code runs when the DOM is ready and creates a context object which is needed 
// to use the SharePoint object model. We have encapsulated that operation in the start()
// function.
$(document).ready(function () {
    $(".ms-siteicon-a img").attr('src', '../Images/AnswerPageIcon.png');
    timeleft = 41;
    reSetTimer();
    createNewAnswer();
    getUserNameAndList();
});

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
        listItem.set_item("Title", "Answer Mode");
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
        listItem.set_item("Title", "Answer Mode");
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


//Populate the operands and the operators on game load
function createNewAnswer() {
    //divider- to increase the range of the operands
    var divider = 10;
    switch (currentLevel) {
        case 0: devider = 10;
            break;
        case 1: devider = 19;
            break;
        case 2: devider = 39;
            break;
        default:
            devider = 49;
            break;
    }
    var operand1 = Math.floor(Math.random() * divider) + 1;
    var operand2 = Math.floor(Math.random() * divider) + 1;
    var operand3 = Math.floor(Math.random() * divider) + 1;
    var operand4 = Math.floor(Math.random() * divider) + 1;
    var operand5 = Math.floor(Math.random() * divider) + 1;
    var operand6 = Math.floor(Math.random() * divider) + 1;
    var operand7 = Math.floor(Math.random() * divider) + 1;
    var operand8 = Math.floor(Math.random() * divider) + 1;
    //new operator for every new equation
    var operator = Math.floor(Math.random() * 3);
    var resultant = 0;
    switch (operator) {
        case 0: resultant = parseInt(operand1) + parseInt(operand2);
            break;
        case 1: resultant = parseInt(operand1) - parseInt(operand2);
            break;
        case 2: resultant = parseInt(operand1) * parseInt(operand2);
            break;

    }
    // Assigning value to the Answer of the equation
    document.getElementById("answerLbl").innerText = resultant;
    // Assigning the correct operands values in  the option buttons
    document.getElementById("operandOption2").innerText = operand1;
    document.getElementById("operandOption7").innerText = operand2;
    //assigning values to the operands btns
    document.getElementById("operandOption1").innerText = operand3;
    document.getElementById("operandOption4").innerText = operand4;
    document.getElementById("operandOption5").innerText = operand5;
    document.getElementById("operandOption6").innerText = operand6;
    document.getElementById("operandOption3").innerText = operand7;
    document.getElementById("operandOption8").innerText = operand8;
}
function  equationFormed() {
    if (document.getElementById("operand1InTheEquationlbl").innerText != "?" && (document.getElementById("operand2InTheEquationlbl").innerText != "?") && (document.getElementById("operatorInTheEquationlbl").innerText != "?"))
    { return true; }
    else
        return false;
}

// Check the answer formed by the user.
function checkResult() {
   
  var isEquationFormed= equationFormed();
  if (isEquationFormed) {
      var happyIcon = document.getElementById("happyEnoticonAnswer");
      var sadIcon = document.getElementById("sadEnoticonAnswer");
      var userSelectedOperator = document.getElementById("operatorInTheEquationlbl").innerText;
      // check the check of the equation formed by the user
      switch (operatorCode) {
          case 0: userAnswered = parseInt(document.getElementById("operand1InTheEquationlbl").innerText) + parseInt(document.getElementById("operand2InTheEquationlbl").innerText);
              break;
          case 1: userAnswered = parseInt(document.getElementById("operand1InTheEquationlbl").innerText) - parseInt(document.getElementById("operand2InTheEquationlbl").innerText);
              break;
          case 2: userAnswered = parseInt(document.getElementById("operand1InTheEquationlbl").innerText) * parseInt(document.getElementById("operand2InTheEquationlbl").innerText);
              break;
      }
        
        if (timeleft > 0) {
            //matching answer of the equation formed by the user with the given answer.
            if (userAnswered == document.getElementById("answerLbl").innerText)
            {
                // Opeands and the operator selected by the user is correct, increment the correct counter
                countCorrectAns++;
                document.getElementById("correctCounterLbl").innerText = countCorrectAns;
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
                
                //Implementing Levels
                if (currentLevel == 0 && countCorrectAns == BEGINNER_LEVEL) {
                    showDialog("Stage 1 Complete!", "Good job!! You have completed the BEGINNER's level");
                    //Changing the css to display level 2 on the top right corner of the game.
                    document.getElementById("answerMode").setAttribute("class", "answer2");
                    // reset the values of all the operand and the operator selected by the user.
                    document.getElementById("operand1InTheEquationlbl").innerText = "?";
                    document.getElementById("operand2InTheEquationlbl").innerText = "?";
                    document.getElementById("operatorInTheEquationlbl").innerText = "?";
                    //reset the correct counter
                    document.getElementById("correctCounterLbl").innerText = 0;
                    countCorrectAns = 0;
                    //reset wrong counter
                    document.getElementById("wrongCounterLbl").innerText = 0;
                    countWrongAns = 0;
                    //increment the current level count.
                    //reset sizes of correct and wrong smilies
                    happyIcon.style.height = '20px';
                    happyIcon.style.width = '20px';
                    sadIcon.style.height = '20px';
                    sadIcon.style.width = '20px';
                    currentLevel++;
                    timeleft = 41;
                    //create new Answer.
                    createNewAnswer();

                }
                else if (currentLevel == 1 && countCorrectAns == MEDIUM_LEVEL) {
                    showDialog("Stage 2 Complete!", "Great!! You have completed the MEDIUM level");
                    //Changing the css to display level 3 on the top right corner of the game.
                    document.getElementById("answerMode").setAttribute("class", "answer3");
                    // reset the values of all the operand and the operator selected by the user.
                    document.getElementById("operand1InTheEquationlbl").innerText = "?";
                    document.getElementById("operand2InTheEquationlbl").innerText = "?";
                    document.getElementById("operatorInTheEquationlbl").innerText = "?";
                    //reset the correct counter
                    document.getElementById("correctCounterLbl").innerText = 0;
                    countCorrectAns = 0;
                    //reset wrong counter
                    document.getElementById("wrongCounterLbl").innerText = 0;
                    countWrongAns = 0;
                    //increment the current level count.
                    currentLevel++;
                    //reset sizes of correct and wrong smilies
                    happyIcon.style.height = '20px';
                    happyIcon.style.width = '20px';
                    sadIcon.style.height = '20px';
                    sadIcon.style.width = '20px';
                    timeleft = 41;
                    //create new Answer.
                    createNewAnswer();
                }
                else if (currentLevel == 2 && countCorrectAns == EXPERT_LEVEL) {
                    //Record the score into the SharePoint List.
                    showDialog("Stage 3 Complete!", "Awesome - You won the game!!");
                    recordScore('Wins');
                    //redirect the user to the main page of the game.
                    window.location.href = "../Pages/Default.aspx";
                    //reset current level.
                    currentLevel = 0;
                    //reset the background image for level 1
                    document.getElementById("answerMode").setAttribute("class", "answer1");
                    //reset the values of the selected operands and operator.
                    document.getElementById("operand1InTheEquationlbl").innerText = "?";
                    document.getElementById("operand2InTheEquationlbl").innerText = "?";
                    document.getElementById("operatorInTheEquationlbl").innerText = "?";
                    //reset correct and wrong counters.
                    document.getElementById("correctCounterLbl").innerText = 0;
                    countWrongAns = 0;
                    document.getElementById("wrongCounterLbl").innerText = 0;
                    countCorrectAns = 0;
                    //reset sizes of correct and wrong smilies
                    happyIcon.style.height = '20px';
                    happyIcon.style.width = '20px';
                    sadIcon.style.height = '20px';
                    sadIcon.style.width = '20px';

                }
                    // correct answer but still in the same level
                else {

                    document.getElementById("operand1InTheEquationlbl").innerText = "?";
                    document.getElementById("operand2InTheEquationlbl").innerText = "?";
                    document.getElementById("operatorInTheEquationlbl").innerText = "?";
                    //give extra 5 sec each time the user give correct answer.
                    timeleft = timeleft + 5;
                    createNewAnswer();
                }
            }
                //Opeands and the operator selected by the user is wrong, increment the wrong counter and reset time to 15 sec
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
                   
                    default:
                        sadIcon.style.height = '70px';
                        sadIcon.style.width = '70px';
                        break;

                }
                document.getElementById("wrongCounterLbl").innerText = countWrongAns;
                timeleft = 15;
                document.getElementById("operand1InTheEquationlbl").innerText = "?";
                document.getElementById("operand2InTheEquationlbl").innerText = "?";
                document.getElementById("operatorInTheEquationlbl").innerText = "?";
                createNewAnswer();

            }
        }
    }
}


//capture the id of the operator selected
function operatorSelected(number) {
    //add operators to the 
    
  
    switch (number) {
        case 0: document.getElementById("operatorInTheEquationlbl").innerText = "+";
            operatorCode=0;
            break;
        case 1: document.getElementById("operatorInTheEquationlbl").innerText = "-";
            operatorCode=1;
            break;
        case 2: document.getElementById("operatorInTheEquationlbl").innerText = "*";
            operatorCode=2;
            break;
    }

  
    checkResult();
}

//Capture the id of the operand option selected by the user.
function operandSelected(number) {
    var id;
    for (var i = number; i < 9; i++) {
        if (i == 1) { id = "operandOption1"; break; }
        if (i == 2) { id = "operandOption2"; break; }
        if (i == 3) { id = "operandOption3"; break }
        if (i == 4) { id = "operandOption4"; break }
        if (i == 5) { id = "operandOption5"; break }
        if (i == 6) { id = "operandOption6"; break }
        if (i == 7) { id = "operandOption7"; break }
        if (i == 8) { id = "operandOption8"; break }
          }
   
    // left hand side operans will make first operand
    if (number <= 4) {
        document.getElementById("operand1InTheEquationlbl").innerText = document.getElementById(id).innerText;
    }

    //Right hand side options are for right hand side operand
    if (number >= 5) {
        document.getElementById("operand2InTheEquationlbl").innerText = document.getElementById(id).innerText;
    }
  
    checkResult();
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
    var happyIcon = document.getElementById("happyEnoticonAnswer");
    var sadIcon = document.getElementById("sadEnoticonAnswer");
    timeleft--;
    if (timeleft >= 0) {
        document.getElementById("Timer").innerText = timeleft;
    }
    else {
        showDialog("Out of Time!", "Sorry, time's up. (Let's try again)");
        document.getElementById("correctCounterLbl").innerText = 0;
        document.getElementById("wrongCounterLbl").innerText = 0;
        document.getElementById("operand1InTheEquationlbl").innerText = "?";
        document.getElementById("operand2InTheEquationlbl").innerText = "?";
        document.getElementById("operatorInTheEquationlbl").innerText = "?";
        countCorrectAns = 0;
        countWrongAns = 0;
        choseLevel = 0;
        timeleft = 41;
        happyIcon.style.height = '20px';
        happyIcon.style.width = '20px';
        sadIcon.style.height = '20px';
        sadIcon.style.width = '20px';
        createNewAnswer();
        recordScore("Losses");
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
