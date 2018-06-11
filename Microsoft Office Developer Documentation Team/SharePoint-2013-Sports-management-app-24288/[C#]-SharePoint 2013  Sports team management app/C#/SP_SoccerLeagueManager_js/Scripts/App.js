// Variable that will hold the SharePoint ClientContext object
var context;

// Variable that will hold the SharePoint App Web object
var web;

// Variable that will hold various SharePoint List objects 
var list;
var teamList;
var eventList;
var assetList;

// Variable that will hold various SharePoint ListItem objects
var currentItem;
var listItem;
var teamItem;
var matchItem;
var assetItem;
var attachment;

// Variable that will hold SharePoint username
var user;
var userName;
var employeeName;

// Variable that will hold the contents of League Points
var wonPoints = 0;
var lostPoints = 0;
var drawPoints = 0;
var noScoreDrawPoints = 0;

// Variable that will hold a file selected by the user for uploading
var file;

// Variable that will hold the contents of a file selected by the user for uploading
var contents;

// Variable that will hold LeagueID
var tableID;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {

    // Wire up events to two file input elements.
    // NOTE: IE 8 does not support .addEventListener, so if that's
    // not supported use .attachEvent instead.
    var matchUpload = document.getElementById("matchUpload");
    if (!matchUpload.addEventListener) {
        matchUpload.attachEvent("onchange", assetAttach);
    }
    else {
        matchUpload.addEventListener("change", assetAttach, false);
    }

    $('#date').datepicker({
        showOn: "both",
        buttonImage: "../images/calendar.gif",
        buttonImageOnly: true,
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        dateFormat: "MM dd, yy"
    });

    context = SP.ClientContext.get_current();
    web = context.get_web();
    userName = web.get_currentUser();
    userName.retrieve();
    context.load(web, 'EffectiveBasePermissions');
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        $('#Home').fadeIn(500, null);

    },
    function (sender, args) {
        var errArea = document.getElementById("errAllTables");
        // Failure returned from executeQueryAsync
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("Failed to get started. Error: " + args.get_message()));
        errArea.appendChild(divMessage);
        $('#Home').fadeIn(500, null);
    });
});

// This function hides all main DIV elements. The caller is then responsible 
// for re-showing the one that needs to be displayed.
function hideAllPanels() {
    $('#LeagueTableDetails').hide();
    $('#AddTables').hide();
    $('#AllLeagues').hide();
    $('#addTeams').hide();
    $('#allTables').hide();
    $('#showAllLeague').hide();
    $('#allTables').hide();
    $('#showAllLeague').hide();
    $('#saveResult').hide();
    $('#addTeamForm').hide();
    $('#editOptions').slideUp(500, null);
    $('#addTeamForm').hide();
    $('#newTableName').val("");
    $('#TeamFixtureDetails').hide();
    $('#AssetAttachments').hide();
    $('#attachmentDetails').hide();
    $('#Assets').hide();
    clearAddResultForm();
}

// This function shows home page of app
function displayHome() {
    hideAllPanels();
    $('#Home').fadeIn(500, null);
}

// This function shows View Table form
function showTables() {
    $('#allTables').show();
    $('#AddTables').show();
    $('#showAllLeague').hide();
    $('#Home').hide();
    $('#deleteTables').slideUp();
}

// This function adds the league name in the select field
function showAllTable() {
    $('#AddTables').hide();
    $('#showAllLeague').show();
    var tableNames = "Title";
    var list = web.get_lists().getByTitle('Table');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        var listItemEnumerator = listItems.getEnumerator();
        var addTeam = document.getElementById('allLeague');

        // Remove all nodes from the lead <DIV> so we have a clean sp
        while (addTeam.hasChildNodes()) {
            addTeam.removeChild(addTeam.lastChild);
        }

        // Iterate through the Table list
        while (listItemEnumerator.moveNext()) {
            var listItem = listItemEnumerator.get_current();
            var title = listItem.get_fieldValues()["Title"];
            var id = listItem.get_fieldValues()["ID"];
            var option = document.createElement('option');
            option.innerHTML = title;
            option.value = id;
            addTeam.appendChild(option);
        }
    }, function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error:" + args.get_message());
    });
}

// This function shows selected league details form
function showLeague() {
    var tableDiv = document.getElementById('TableName');
    tableDiv.innerHTML = "";   
    var selectTeam1 = document.getElementById("allLeague");
    tableID = $('#allLeague').val();
    var tableName = document.createElement('Div');   
    if (tableID != null) {
        tableName.innerHTML = selectTeam1.options[selectTeam1.selectedIndex].text;
        tableName.className = "tileHeadingText";
        tableDiv.appendChild(tableName);        
        showLeagueDetails(tableID);
        $('#addTeams').hide();
        $('#saveResult').hide();
        $('#allTables').hide();
        $('#showAllLeague').hide();
        $('#addTeamForm').fadeIn(500, null);
        $('#LeagueTableDetails').fadeIn(500, null);
    }
    else
    {
        alert("There are no leagues.");
    }
}

// This function shows the league table
function showLeagueDetails(tableID) {
    $('#LeagueTableDetails').show();
    var hasTeams = false;
    var positionCount = 0;
    teamList = web.get_lists().getByTitle('Teams');
    var teamQuery = new SP.CamlQuery();
    teamQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + tableID
        + "</Value></Eq></Where><OrderBy><FieldRef Name='Points' Ascending='FALSE'></FieldRef><FieldRef Name='For' Ascending='FALSE'></FieldRef></OrderBy></Query></View>");
    var teamItems = teamList.getItems(teamQuery);
    context.load(teamItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var teamTable = document.getElementById("tablebody");

            // Remove all nodes from the Teams <DIV> so we have a clean space to write to
            while (teamTable.hasChildNodes()) {
                teamTable.removeChild(teamTable.lastChild);
            }

            // Iterate through the Teams list
            var listItemEnumerator = teamItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                // Create a DIV to display the Teams name
                var team = document.createElement("div");
                var teamLabel = listItem.get_fieldValues()["Title"];
                var teamWon = listItem.get_fieldValues()["Won"];
                var teamLost = listItem.get_fieldValues()["Lost"];
                var teamDraw = listItem.get_fieldValues()["Draw"];
                var teamNoScoreDraw = listItem.get_fieldValues()["NoScoreDraw"];
                var teamFor = listItem.get_fieldValues()["For"];
                var teamAgainst = listItem.get_fieldValues()["Against"];
                var teamPlayed = listItem.get_fieldValues()["Played"];
                var teamPoints = listItem.get_fieldValues()["Points"];

                // Add an ID to the Teams DIV
                team.id = listItem.get_id();
                positionCount = positionCount + 1;

                // Add an class to the Teams DIV
                team.className = "attachmentItem";

                // Add the Teams div to the UI
                teamTable.appendChild(team);
                $('#tablebody').append("<tr style='text-align:center'><td>" + positionCount + "</td><td>" + teamLabel + "</td><td>" + teamPlayed + "</td><td>" + teamWon + "</td><td>" + teamLost + " </td><td>" + teamDraw + "</td><td>" + teamNoScoreDraw + " </td><td>" + teamFor + "</td><td>" + teamAgainst + "</td><td>" + teamPoints + "</td><td><div id='" + teamLabel + "'class='fixtureClicker' onclick='showFixture(this)'><u>Fixture</u></div></td></tr>");

                hasTeams = true;
            }

            $('#LeagueTableDetails').fadeIn(500, null);
        }, function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error in showing league details: " + args.get_message());
        });
}

// This function deletes table in the View Table form
function deleteTables() {
    var deleteTableID = $('#allLeague').val();
    if (deleteTableID == null) {
        alert("There are no leagues to delete.");
    }
    else {
        list = web.get_lists().getByTitle('Table');
        var deleteTable = list.getItemById(deleteTableID);
        teamList = web.get_lists().getByTitle('Teams');
        var teamQuery = new SP.CamlQuery();
        teamQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='TRUE' /><Value Type='Lookup'>"
            + deleteTableID
            + "</Value></Eq></Where></Query></View>");
        var listItems = teamList.getItems(teamQuery);
        context.load(listItems);
        context.executeQueryAsync(
            function () {
                // Success returned from executeQueryAsync
                var listItemEnumerator = listItems.getEnumerator();
                deleteMatch(deleteTableID);
                while (listItemEnumerator.moveNext()) {
                    var listItem = listItemEnumerator.get_current();
                    var teamID = listItem.get_id();
                    deleteTeams(teamID);
                }
                
                deleteTable.deleteObject();
                showAllLeague();

            },
            function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Error in deleting table: " + args.get_message());
            });
    }
}

// This function shows the fixture table
function showFixture(fixtureDiv) {
    $('#Assets').hide();
    $('#AssetAttachments').hide();
    $('#attachmentDetails').hide();
    var teamName = fixtureDiv.id;
    var fixtureTable = document.getElementById("fixtureBody");
    var fixtureTeam = document.getElementById('fixtureTeamName');
    fixtureTeam.innerText = "";
    // Remove all nodes from the Teams <DIV> so we have a clean space to write to
    while (fixtureTable.hasChildNodes()) {
        fixtureTable.removeChild(fixtureTable.lastChild);
    }
    var matchList = web.get_lists().getByTitle('Match');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = matchList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync

        // Iterate through the Match list
        var listItemEnumerator = listItems.getEnumerator();
        while (listItemEnumerator.moveNext()) {
            var listItem = listItemEnumerator.get_current();
            var matchId = listItem.get_id();
            var homeTeam = listItem.get_fieldValues()["Team1"].get_lookupValue();
            var awayTeam = listItem.get_fieldValues()["Team2"].get_lookupValue();

            if (teamName == homeTeam || teamName == awayTeam) {
                var homeScore = listItem.get_fieldValues()["HomeScore"];
                var awayScore = listItem.get_fieldValues()["AwayScore"];
                var date = new Date(listItem.get_fieldValues()["date"]).format("MMMM dd, yyyy");
                var matchSummary = listItem.get_fieldValues()["MatchSummary"];
                if (matchSummary == null) {
                    matchSummary = "-";
                }
                var notablePerformace = listItem.get_fieldValues()["NotablePerformance"];
                if (notablePerformace == null) {
                    notablePerformace = "-";
                }
                $('#fixtureBody').append("<tr style='text-align:center'><td>" + date + "</td><td>" + homeTeam + "</td><td>" + awayTeam + "</td><td>" + homeScore + "</td><td>" + awayScore + "</td><td style='width:50px;word-wrap:break-word'>" + matchSummary + " </td><td style='width:50px'>" + notablePerformace + "</td><td><div id='" + matchId + "'class='fixtureClicker' onclick='showAssets(this)'><u>Assets</u></div></td></tr>");
            }
        }
        $('#fixtureTeamName').show();

        fixtureTeam.innerText = teamName;
        $('#TeamFixtureDetails').show();
    }, function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error: " + args.get_message());
    });
}

// This function shows match attachments
function showAssets(matchID) {
    var assetID = matchID.id;
    var attachmentCount = 0;
    $('#AssetAttachments').show();
    $('#Assets').show();
    var assetList = document.getElementById("AssetAttachments");
    while (assetList.hasChildNodes()) {
        assetList.removeChild(assetList.lastChild);
    }
    var asset = document.getElementById("attachmentDetails");
    while (asset.hasChildNodes()) {
        asset.removeChild(asset.lastChild);
    }
    var attachmentFolder = web.getFolderByServerRelativeUrl("Lists/Match/Attachments/" + assetID);
    var attachments = attachmentFolder.get_files();
    context.load(attachments);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        // Enumerate and list the Match Attachments if they exist
        var attachementEnumerator = attachments.getEnumerator();
        while (attachementEnumerator.moveNext()) {

            attachment = attachementEnumerator.get_current();
            attachmentCount = attachmentCount + 1;
            var countDiv = document.createElement("div");
            countDiv.className = "assetItem";
            countDiv.innerText = attachmentCount;
            countDiv.id = attachment.get_serverRelativeUrl();
            if (attachmentCount == 1) {
                showAssetDetails(attachment.get_serverRelativeUrl());
            }
            $(countDiv).click(function (sender) {
                showAssetDetails(sender.target.id);
            });
            assetList.appendChild(countDiv);
        }

    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("There are no Assets");
    });

}

// This function checks attachment type and displays it
function showAssetDetails(assetName) {
    $('#attachmentDetails').show();
    var asset = document.getElementById("attachmentDetails");
    while (asset.hasChildNodes()) {
        asset.removeChild(asset.lastChild);
    }
    var extension = assetName.split('.').pop();
    if (extension == "mp4" || extension == "ogg" || extension == "webm") {
        var attachVideo = document.createElement("video");
        attachVideo.preload = true;
        attachVideo.src = assetName;
        attachVideo.type = 'video/mp4';
        attachVideo.controls = 'autoplay';
        attachVideo.className = "asset";
        asset.appendChild(attachVideo);
        //attachVideo.play();
    }
    if (extension == "jpg" || extension == "jpeg" || extension == "png" || extension == "bmp" || extension == "gif") {
        var attachImage = document.createElement("img");
        attachImage.src = assetName;

        attachImage.className = "asset";
        asset.appendChild(attachImage);

    }
}

// This function shows the view table form
function showAllLeague() {
    hideAllPanels();
    showAllTable();
    $('#Home').hide();
    $('#allTables').show();
}

//This function saves the newly-added table
function saveNewTable() {
    if ($('#newTableName').val() == "") {
        var errArea = document.getElementById("errAllTables");
        // Remove all nodes from the errAllTables <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'League Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        $('#AddTables').hide();
        $('#allTables').hide();
        var leagueName = document.getElementById("TableName");
        // Remove all nodes from the errAllTables <DIV> so we have a clean space to write to
        while (leagueName.hasChildNodes()) {
            leagueName.removeChild(leagueName.lastChild);
        }
        list = web.get_lists().getByTitle('Table');
        var itemCreateInfo = new SP.ListItemCreationInformation();
        currentItem = list.addItem(itemCreateInfo);

        wonPoints = parseInt($('#winPoints').val());
        lostPoints = parseInt($('#losePoints').val());
        drawPoints = parseInt($('#drawPoints').val());
        noScoreDrawPoints = parseInt($('#noScoreDrawPoints').val());

        currentItem.set_item("Title", $('#newTableName').val());
        currentItem.set_item("WinPoints", wonPoints);
        currentItem.set_item("LostPoints", lostPoints);
        currentItem.set_item("DrawPoints", drawPoints);
        currentItem.set_item("NoScoreDrawPoints", noScoreDrawPoints);
        currentItem.update();
        context.load(currentItem);
        context.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            $('#addTeamForm').show();
            tableID = currentItem.get_id();
            var tableDiv = document.getElementById('TableName');
            var tableName = document.createElement('Div');
            tableName.innerHTML = $('#newTableName').val();
            tableName.className = "tileHeadingText";
            tableDiv.appendChild(tableName);
        },
        function () {
            // Failure returned from executeQueryAsync
            alert("error");
        });
    }
}

// This function shows advanced options form
function addAdvancedOption() {
    $('#editOptions').slideDown(500, null);
    $('#winPoints').removeAttr('disabled');
    $('#losePoints').removeAttr('disabled');
    $('#drawPoints').removeAttr('disabled');
    $('#noScoreDrawPoints').removeAttr('disabled');
}

// This function populates tables in delete table select field
function showDelete() {
    var addTables = document.getElementById('showTables');
    // Remove all nodes from the showTables <DIV> so we have a clean space to write to
    while (addTables.hasChildNodes()) {
        addTables.removeChild(addTables.lastChild);
    }

    list = web.get_lists().getByTitle('Table');
    var camlQuery = SP.CamlQuery.createAllItemsQuery();
    var listItems = list.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            if (listItems.get_count() > 0) {
                var listItemEnumerator = listItems.getEnumerator();
                while (listItemEnumerator.moveNext()) {
                    var listItem = listItemEnumerator.get_current();
                    var option = document.createElement('option');
                    var tableName = listItem.get_fieldValues()["Title"];
                    var tableID = listItem.get_fieldValues()["ID"];
                    option.innerHTML = tableName;
                    option.value = tableID;
                    addTables.appendChild(option);
                }
                $('#deleteTables').slideDown(500, null);
            }
            else {
                alert("There are no Leagues to delete");
            }
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error in populating Tables " + args.get_message());
        });
}

// This function deletes selected league
function deleteTable() {
    var deleteTableID = $('#showTables').val();
    list = web.get_lists().getByTitle('Table');
    var deleteTable = list.getItemById(deleteTableID);
    teamList = web.get_lists().getByTitle('Teams');
    var teamQuery = new SP.CamlQuery();
    teamQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + deleteTableID
        + "</Value></Eq></Where></Query></View>");
    var listItems = teamList.getItems(teamQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();            
                var teamID = listItem.get_id();
                deleteTeams(teamID);
            }
            deleteTable.deleteObject();
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error in deleting table: " + args.get_message());
        }

    );

    $('#deleteTables').slideUp();

}

// This function deletes all teams related to the league
function deleteTeams(teamID) {
    list = web.get_lists().getByTitle("Teams");
    var teamItem = list.getItemById(teamID);
    teamItem.deleteObject();
}

// This function deletes all matches related to the league
function deleteMatch(deleteTableID) {
    teamList = web.get_lists().getByTitle('Match');
    var teamQuery = new SP.CamlQuery();
    teamQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + deleteTableID
        + "</Value></Eq></Where></Query></View>");
    var listItems = teamList.getItems(teamQuery);
    context.load(listItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var listItemEnumerator = listItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();
                var teamID = listItem.get_id();
                deleteRecord(teamID);
            }
            
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error:"+args.get_message());
        });
}

// This function deletes team record from Match list
function deleteRecord(matchID) {
    var mlist = web.get_lists().getByTitle("Match");
    var matchItem = mlist.getItemById(matchID);
    matchItem.deleteObject();   
    context.executeQueryAsync(function () {  });
}

// This function shows Add new Team form
function addNewTeam() {
    $('#addTeamForm').show();
}

// This function shows League details form 
function showLeagueTable() {
    $('#addTeamForm').show();
    $('#LeagueTableDetails').show();
    $('#addTeams').hide();
    $('#saveResult').hide();
    showLeagueDetails(tableID);

}

// This function shows the Add Team form 
function addTeams() {

    showLeagueTeams(tableID);
    $('#addTeams').show();
    $('#saveResult').hide();
    $('#LeagueTableDetails').hide();
    $('#TeamFixtureDetails').hide();
    $('#AssetAttachments').hide();
    $('#attachmentDetails').hide();
    $('#Assets').hide();
}

// This function retrieves all teams under a league  
function showLeagueTeams(tableLookup) {
    var hasTeams = false;
    var noTeamDiv = document.getElementById("noTeams");
    while (noTeamDiv.hasChildNodes()) {
        noTeamDiv.removeChild(noTeamDiv.lastChild);
    }
    teamList = web.get_lists().getByTitle('Teams');
    var teamQuery = new SP.CamlQuery();
    teamQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + tableLookup
        + "</Value></Eq></Where></Query></View>");
    var teamItems = teamList.getItems(teamQuery);
    context.load(teamItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var teamTable = document.getElementById("teamsList");
            var deleteTeamTable = document.getElementById("deleteTeamsList");
            // Remove all nodes from the Teams <DIV> so we have a clean space to write to
            while (teamTable.hasChildNodes()) {
                teamTable.removeChild(teamTable.lastChild);
            }
            while (deleteTeamTable.hasChildNodes()) {
                deleteTeamTable.removeChild(deleteTeamTable.lastChild);
            }
            // Iterate through the Teams list
            var listItemEnumerator = teamItems.getEnumerator();
            while (listItemEnumerator.moveNext()) {
                var listItem = listItemEnumerator.get_current();

                var teamNames = listItem.get_fieldValues()["Title"];
                var played = listItem.get_fieldValues()["Played"];
                if (played == 0) {
                    var teamDelete = document.createElement("span");
                    teamDelete.appendChild(document.createTextNode("X"));
                    teamDelete.className = "deleteButton";
                    teamDelete.id = listItem.get_id();
                    $(teamDelete).click(function (sender) {
                        deleteTeam(sender.target.id);
                    });
                    deleteTeamTable.appendChild(teamDelete);
                    var teamLink = document.createElement("div");

                    teamLink.appendChild(document.createTextNode(teamNames));
                    teamLink.className = "item";

                    deleteTeamTable.appendChild(teamLink);
                }
                else {
                    var teamDelete = document.createElement("span");
                    teamDelete.appendChild(document.createTextNode("X"));
                    teamDelete.className = "noDeleteButton";
                    teamDelete.id = listItem.get_id();
                    $(teamDelete).click(function (sender) {
                        deleteTeam(sender.target.id);
                    });
                    teamTable.appendChild(teamDelete);
                    var teamLink = document.createElement("div");

                    teamLink.appendChild(document.createTextNode(teamNames));
                    teamLink.className = "item";

                    teamTable.appendChild(teamLink);
                }

                hasTeams = true;
            }
            if (!hasTeams) {
                var noTeams = document.createElement("div");
                noTeams.appendChild(document.createTextNode("There are no Teams. You can add new team from here."));
                noTeamDiv.appendChild(noTeams);
            }
            $('#addTeamForm').fadeIn(500, null);
        },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error in delete team " + args.get_message());
    });
}

// This function deletes a team in Add Team form
function deleteTeam(itemID) {
    teamList = web.get_lists().getByTitle("Teams");
    var deleteTeam = teamList.getItemById(itemID);
    deleteTeam.deleteObject();
    showLeagueTeams(tableID);
}

// This function saves newly added team
function saveNewTeam() {

    if ($('#newTeamName').val() == "") {
        var errArea = document.getElementById("errAllTables");
        // Remove all nodes from the errAllTables <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("'Team Name' field is required."));
        errArea.appendChild(divMessage);
    }
    else {
        teamList = web.get_lists().getByTitle('Teams');
        var itemCreateInfo = new SP.ListItemCreationInformation();
        teamItem = teamList.addItem(itemCreateInfo);
        teamItem.set_item("Title", $('#newTeamName').val());
        teamItem.set_item("TableLookup", tableID);
        teamItem.set_item("Won", 0);
        teamItem.set_item("Lost", 0);
        teamItem.set_item("Draw", 0);
        teamItem.set_item("NoScoreDraw", 0);
        teamItem.set_item("For", 0);
        teamItem.set_item("Against", 0);
        teamItem.set_item("Played", 0);
        teamItem.set_item("Points", 0);
        teamItem.update();
        context.load(teamItem);
        context.executeQueryAsync(function () {
            // Success returned from executeQueryAsync
            $('#newTeamName').val("");
            showLeagueTeams(tableID);
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert(args.get_message());
        });
    }
}

// This function shows the Add Result form
function addResult() {
    fillTeams();
    $('#saveResult').show();
    $('#LeagueTableDetails').hide();
    $('#addTeams').hide();
    $('#addTeamForm').show();
    $('#submitResult').show();
    $('#team1').attr("disabled", false);
    $('#team2').attr("disabled", false);
    $('#team1Score').attr("disabled", false);
    $('#team2Score').attr("disabled", false);
    $('#date').attr("disabled", false);
    $('#matchSummary').attr("disabled", false);
    $('#notablePerformance').attr("disabled", false);
}

// This function add team names in the select field to the Add Result form
function fillTeams() {
    hideAllPanels();
    teamList = web.get_lists().getByTitle('Teams');
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='True' /><Value Type='Lookup'>"
        + tableID
        + "</Value></Eq></Where></Query></View>");
    var listItems = teamList.getItems(camlQuery);
    context.load(listItems);
    context.executeQueryAsync(function () {
        // Success returned from executeQueryAsync
        var listItemEnumerator = listItems.getEnumerator();
        var team1 = document.getElementById('team1');
        var team2 = document.getElementById('team2');

        // Remove all nodes from the team1 <DIV> so we have a clean space to write to
        while (team1.hasChildNodes()) {
            team1.removeChild(team1.lastChild);
        }

        // Remove all nodes from the team2 <DIV> so we have a clean space to write to
        while (team2.hasChildNodes()) {
            team2.removeChild(team2.lastChild);
        }

        // Iterate through the Teams list
        while (listItemEnumerator.moveNext()) {
            var listItem = listItemEnumerator.get_current();
            var title = listItem.get_fieldValues()["Title"];
            var id = listItem.get_fieldValues()["ID"];
            var option1 = document.createElement('option');
            option1.innerHTML = title;
            option1.value = id;
            team1.appendChild(option1);
            var option2 = document.createElement('option');
            option2.innerHTML = title;
            option2.value = id;
            team2.appendChild(option2);
        }
    }, function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Failure " + args.get_message());
    });
}

// This function shows the league table
function showTeams() {
    $('#LeagueTableDetails').show();
}

// This function populates teams in delete team select field
function showDeleteTeams() {
    var hasTeams = false;
    teamList = web.get_lists().getByTitle('Teams');
    var teamQuery = new SP.CamlQuery();
    teamQuery.set_viewXml("<View><Query><Where><Eq><FieldRef Name='TableLookup' LookupId='TRUE' /><Value Type='Lookup'>"
        + tableID
        + "</Value></Eq></Where></Query></View>");
    var teamItems = teamList.getItems(teamQuery);
    context.load(teamItems);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            var addTables = document.getElementById('showTeams');
            if (teamItems.get_count() > 0) {
                while (addTables.hasChildNodes()) {
                    addTables.removeChild(addTables.lastChild);
                }
                var listItemEnumerator = teamItems.getEnumerator();
                while (listItemEnumerator.moveNext()) {
                    var listItem = listItemEnumerator.get_current();
                    var option = document.createElement('option');
                    var teamName = listItem.get_fieldValues()["Title"];
                    var teamID = listItem.get_fieldValues()["ID"];
                    option.innerHTML = teamName;
                    option.value = teamID;
                    addTables.appendChild(option);
                }
                $('#deleteTeams').slideDown();
            }
            else {
                alert("There are no teams to delete");
            }
        },
        function (sender, args) {
            // Failure returned from executeQueryAsync
            alert("Error in populating teams " + args.get_message());
        });
}

// This function fetches League points
function getTablePoints(tableID) {
    var tableList = web.get_lists().getByTitle('Table');
    var currentItem1 = tableList.getItemById(tableID);
    context.load(currentItem1);
    context.executeQueryAsync(
        function () {
            // Success returned from executeQueryAsync
            wonPoints = parseInt(currentItem1.get_fieldValues()["WinPoints"]);
            lostPoints = parseInt(currentItem1.get_fieldValues()["LostPoints"]);
            drawPoints = parseInt(currentItem1.get_fieldValues()["DrawPoints"]);
            noScoreDrawPoints = parseInt(currentItem1.get_fieldValues()["NoScoreDrawPoints"]);
        });
}

// This function updates the point table
function submitResult() {
    // Fetches League points
    getTablePoints(tableID);
    
    if (isNaN($('#team1Score').val()) || isNaN($('#team2Score').val()) || $('#date').val() == "") {
        alert("Score is not numeric or date field is invalid");
    }
    else {
        var team1ID, team2ID;
        var selectTeam1 = document.getElementById("team1");
        var team1name = selectTeam1.options[selectTeam1.selectedIndex].text;
        var selectTeam2 = document.getElementById("team2");
        var team2name = selectTeam2.options[selectTeam2.selectedIndex].text;

        var team1Score = parseInt($('#team1Score').val());
        var team2Score = parseInt($('#team2Score').val());

        var wonTeamName = "";
        var lostTeamName = "";
        var drawTeamName1 = "";
        var drawTeamName2 = "";
        var noScoreDrawTeamName1 = "";
        var noScoreDrawTeamName2 = "";
        var forScore = 0;
        var againstScore = 0;

        if (team1name == team2name) {
            alert("A team can't play against itself");
        }

        else {
            if (team1Score > team2Score) {
                forScore = team1Score;
                againstScore = team2Score;
                wonTeamName = team1name;
                lostTeamName = team2name;
            }
            if (team1Score < team2Score) {
                forScore = team2Score;
                againstScore = team1Score;
                wonTeamName = team2name;
                lostTeamName = team1name;
            }
            if (team1Score > 0 && team1Score == team2Score) {
                forScore = team1Score;
                againstScore = team1Score;
                drawTeamName1 = team1name;
                drawTeamName2 = team2name;
            }
            if (team1Score == 0 && team1Score == team2Score) {
                forScore = 0;
                againstScore = 0;
                noScoreDrawTeamName1 = team1name;
                noScoreDrawTeamName2 = team2name;
            }

            teamList = web.get_lists().getByTitle('Teams');
            var camlQuery = SP.CamlQuery.createAllItemsQuery();
            var listItems = teamList.getItems(camlQuery);
            context.load(listItems);
            context.executeQueryAsync(function () {
                // Success returned from executeQueryAsync

                // Iterate through the Teams list
                var listItemEnumerator = listItems.getEnumerator();
                while (listItemEnumerator.moveNext()) {
                    var listItem = listItemEnumerator.get_current();
                    var _teamName = listItem.get_fieldValues()["Title"];
                    if (_teamName == wonTeamName) {
                        var played = parseInt(listItem.get_fieldValues()["Played"]) + 1;
                        var _won = parseInt(listItem.get_fieldValues()["Won"]) + 1;
                        var _lost = parseInt(listItem.get_fieldValues()["Lost"]);
                        var _draw = parseInt(listItem.get_fieldValues()["Draw"]);
                        var _noScoredraw = parseInt(listItem.get_fieldValues()["NoScoreDraw"]);
                        var totalPoints = _won * wonPoints + _lost * lostPoints + _draw * drawPoints + _noScoredraw * noScoreDrawPoints;
                        var _for = parseInt(listItem.get_fieldValues()["For"]) + forScore;
                        var _against = parseInt(listItem.get_fieldValues()["Against"]) + againstScore;
                        listItem.set_item("Won", _won);
                        listItem.set_item("Lost", _lost);
                        listItem.set_item("Draw", _draw);
                        listItem.set_item("NoScoreDraw", _noScoredraw);
                        listItem.set_item("For", _for);
                        listItem.set_item("Against", _against);
                        listItem.set_item("Played", played);
                        listItem.set_item("Points", totalPoints);
                        team1ID = listItem.get_id();
                    }
                    if (_teamName == lostTeamName) {
                        var played = parseInt(listItem.get_fieldValues()["Played"]) + 1;
                        var _won = parseInt(listItem.get_fieldValues()["Won"]);
                        var _lost = parseInt(listItem.get_fieldValues()["Lost"]) + 1;
                        var _draw = parseInt(listItem.get_fieldValues()["Draw"]);
                        var _noScoredraw = parseInt(listItem.get_fieldValues()["NoScoreDraw"]);
                        var totalPoints = _won * wonPoints + _lost * lostPoints + _draw * drawPoints + _noScoredraw * noScoreDrawPoints;
                        var _for = parseInt(listItem.get_fieldValues()["For"]) + againstScore;
                        var _against = parseInt(listItem.get_fieldValues()["Against"]) + forScore;
                        listItem.set_item("Won", _won);
                        listItem.set_item("Lost", _lost);
                        listItem.set_item("Draw", _draw);
                        listItem.set_item("NoScoreDraw", _noScoredraw);
                        listItem.set_item("For", _for);
                        listItem.set_item("Against", _against);
                        listItem.set_item("Played", played);
                        listItem.set_item("Points", totalPoints);
                        team2ID = listItem.get_id();
                    }
                    if (_teamName == drawTeamName1) {
                        var played = parseInt(listItem.get_fieldValues()["Played"]) + 1;
                        var _won = parseInt(listItem.get_fieldValues()["Won"]);
                        var _lost = parseInt(listItem.get_fieldValues()["Lost"]);
                        var _draw = parseInt(listItem.get_fieldValues()["Draw"]) + 1;
                        var _noScoredraw = parseInt(listItem.get_fieldValues()["NoScoreDraw"]);
                        var totalPoints = _won * wonPoints + _lost * lostPoints + _draw * drawPoints + _noScoredraw * noScoreDrawPoints;
                        var _for = parseInt(listItem.get_fieldValues()["For"]) + forScore;
                        var _against = parseInt(listItem.get_fieldValues()["Against"]) + againstScore;
                        listItem.set_item("Won", _won);
                        listItem.set_item("Lost", _lost);
                        listItem.set_item("Draw", _draw);
                        listItem.set_item("NoScoreDraw", _noScoredraw);
                        listItem.set_item("For", _for);
                        listItem.set_item("Against", _against);
                        listItem.set_item("Played", played);
                        listItem.set_item("Points", totalPoints);
                        team1ID = listItem.get_id();
                    }
                    if (_teamName == drawTeamName2) {
                        var played = parseInt(listItem.get_fieldValues()["Played"]) + 1;
                        var _won = parseInt(listItem.get_fieldValues()["Won"]);
                        var _lost = parseInt(listItem.get_fieldValues()["Lost"]);
                        var _draw = parseInt(listItem.get_fieldValues()["Draw"]) + 1;
                        var _noScoredraw = parseInt(listItem.get_fieldValues()["NoScoreDraw"]);
                        var totalPoints = _won * wonPoints + _lost * lostPoints + _draw * drawPoints + _noScoredraw * noScoreDrawPoints;
                        var _for = parseInt(listItem.get_fieldValues()["For"]) + forScore;
                        var _against = parseInt(listItem.get_fieldValues()["Against"]) + againstScore;
                        listItem.set_item("Won", _won);
                        listItem.set_item("Lost", _lost);
                        listItem.set_item("Draw", _draw);
                        listItem.set_item("NoScoreDraw", _noScoredraw);
                        listItem.set_item("For", _for);
                        listItem.set_item("Against", _against);
                        listItem.set_item("Played", played);
                        listItem.set_item("Points", totalPoints);
                        team2ID = listItem.get_id();
                    }
                    if (_teamName == noScoreDrawTeamName1) {
                        var played = parseInt(listItem.get_fieldValues()["Played"]) + 1;
                        var _won = parseInt(listItem.get_fieldValues()["Won"]);
                        var _lost = parseInt(listItem.get_fieldValues()["Lost"]);
                        var _draw = parseInt(listItem.get_fieldValues()["Draw"]);
                        var _noScoredraw = parseInt(listItem.get_fieldValues()["NoScoreDraw"]) + 1;
                        var totalPoints = _won * wonPoints + _lost * lostPoints + _draw * drawPoints + _noScoredraw * noScoreDrawPoints;
                        var _for = parseInt(listItem.get_fieldValues()["For"]) + forScore;
                        var _against = parseInt(listItem.get_fieldValues()["Against"]) + againstScore;
                        listItem.set_item("Won", _won);
                        listItem.set_item("Lost", _lost);
                        listItem.set_item("Draw", _draw);
                        listItem.set_item("NoScoreDraw", _noScoredraw);
                        listItem.set_item("For", _for);
                        listItem.set_item("Against", _against);
                        listItem.set_item("Played", played);
                        listItem.set_item("Points", totalPoints);
                        team1ID = listItem.get_id();
                    }

                    if (_teamName == noScoreDrawTeamName2) {
                        var played = parseInt(listItem.get_fieldValues()["Played"]) + 1;
                        var _won = parseInt(listItem.get_fieldValues()["Won"]);
                        var _lost = parseInt(listItem.get_fieldValues()["Lost"]);
                        var _draw = parseInt(listItem.get_fieldValues()["Draw"]);
                        var _noScoredraw = parseInt(listItem.get_fieldValues()["NoScoreDraw"]) + 1;
                        var totalPoints = _won * wonPoints + _lost * lostPoints + _draw * drawPoints + _noScoredraw * noScoreDrawPoints;
                        var _for = parseInt(listItem.get_fieldValues()["For"]) + forScore;
                        var _against = parseInt(listItem.get_fieldValues()["Against"]) + againstScore;
                        listItem.set_item("Won", _won);
                        listItem.set_item("Lost", _lost);
                        listItem.set_item("Draw", _draw);
                        listItem.set_item("NoScoreDraw", _noScoredraw);
                        listItem.set_item("For", _for);
                        listItem.set_item("Against", _against);
                        listItem.set_item("Played", played);
                        listItem.set_item("Points", totalPoints);
                        team2ID = listItem.get_id();
                    }
                    listItem.update();
                    context.load(listItem);
                    context.executeQueryAsync(function () {
                    }, function (sender, args) {
                        // Failure returned from executeQueryAsync
                        alert("Failure " + args.get_message());
                    });
                }
                saveMatchInfo(team1ID, team2ID, team1Score, team2Score);

            }, function (sender, args) {
                // Failure returned from executeQueryAsync
                alert("Failure " + args.get_message());
            });

        }
    }
}

// This functions closes the Result form
function completeResult() {
    $('#saveResult').hide();
    showLeagueTable();
}

// This function clears the inputs on the add result form
function clearAddResultForm() {
    $('#team1Score').val(0);
    $('#team2Score').val(0);
    $('#date').val("");
    $('#matchSummary').val("");
    $('#notablePerformance').val("");
    $('#Attachment').hide();
    var assetList = document.getElementById("MatchAttachment");
    while (assetList.hasChildNodes()) {
        assetList.removeChild(assetList.lastChild);
    }
}

//This function deletes an attachment from a Match list and then refreshed the Add Results form
function deleteAssetAttachment(url, itemID) {
    var attachment = web.getFileByServerRelativeUrl(url);
    attachment.deleteObject();
    showAttachments(itemID);
}

// This function runs when a file is successfully loaded and read by the PO file input.
// It references SP.RequestExecutor.js which will upload the file as an attachment by using the REST API.
// NOTE: This is safer and more capabale (in terms of file size) than using JSOM file creation for uploading files as attachments.
function assetFileOnload(event) {
    contents = event.target.result;
    
    // The storePOAsAttachment function is called to do the actual work after we have a reference to SP.RequestExecutor.js
    $.getScript(SP.ClientContext.get_current().get_url() + "/_layouts/15/SP.RequestExecutor.js", storeAssetAsAttachment);
}

// This helper function ensures that the byte array passed in is returned in a format 
// that's required for the contents of a file sent to SharePoint for storage as a list item attachment.
function fixBuffer(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return binary;
}

// This function runs when the file input is used to uplaod an asset for a match
function assetAttach(event) {
    var errArea = document.getElementById("errGeneral");
    // Remove all nodes from the error <DIV> so we have a clean space to write to
    while (errArea.hasChildNodes()) {
        errArea.removeChild(errArea.lastChild);
    }
    if (!event.target) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("The FileSystem APIs are not supported in this browser."));
        errArea.appendChild(divMessage);
        return (false);
    }
    var files = event.target.files;
    if (!window.FileReader) {
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("The FileSystem APIs are not supported in this browser."));
        errArea.appendChild(divMessage);
        return (false);
    }
    if (files.length > 0) {

        // Get the first file. In this case, only one file can be selected but because the file input could support
        // multi-file selection in some browsers we still need to access the file as the 0th member of the files collection
        file = files[0];

        // Wire up the HTML5 FileReader to read the selected file
        var reader = new FileReader();
        reader.onload = assetFileOnload;
        reader.onerror = function (event) {
            var errArea = document.getElementById("errGeneral");
            // Remove all nodes from the error <DIV> so we have a clean space to write to
            while (errArea.hasChildNodes()) {
                errArea.removeChild(errArea.lastChild);
            }
            var divMessage = document.createElement("DIV");
            divMessage.setAttribute("style", "padding:5px;");
            divMessage.appendChild(document.createTextNode("Error reading file: " + event.target.error.code));
            errArea.appendChild(divMessage);
        };

        // Reading the file triggers the assetFileOnload function that was wired up above
        reader.readAsArrayBuffer(file);
    }
    return false;
}

// This function runs after we are sure we have a reference to SP.RequestExecutor.js.
// It uses the REST API to upload the file as an attachment 
function storeAssetAsAttachment() {
    var fileContents = fixBuffer(contents);
    var createitem = new SP.RequestExecutor(SP.ClientContext.get_current().get_url());
    createitem.executeAsync({
        url: SP.ClientContext.get_current().get_url() + "/_api/web/lists/GetByTitle('Match')/items(" + matchItem.get_id() + ")/AttachmentFiles/add(FileName='" + file.name + "')",
        method: "POST",
        binaryStringRequestBody: true,
        body: fileContents,
        success: storeAssetSuccess,
        error: storeAssetFailure,
        state: "Update"
    });
    function storeAssetSuccess(data) {

        // Success callback
        var errArea = document.getElementById("errGeneral");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }

        // Workaround to clear the value in the file input.
        // What we really want to do is clear the text of the input=file element. 
        // However, we are not allowed to do that because it could allow malicious script to interact with the file system. 
        // So we’re not allowed to read/write that value in JavaScript (or jQuery)
        // So what we have to do is replace the entire input=file element with a new one (which will have an empty text box). 
        // However, if we just replaced it with HTML, then it wouldn’t be wired up with the same events as the original.
        // So we replace it with a clone of the original instead. 
        // And that’s what we need to do just to clear the text box but still have it work for uploading a second, third, fourth file.
        $('#matchUpload').replaceWith($('#matchUpload').val('').clone(true));
        var matchUpload = document.getElementById("matchUpload");
        matchUpload.addEventListener("change", assetAttach, false);
        showAttachments(matchItem.get_id());
    }
    function storeAssetFailure(data) {

        // Failure callback
        var errArea = document.getElementById("errGeneral");

        // Remove all nodes from the error <DIV> so we have a clean space to write to
        while (errArea.hasChildNodes()) {
            errArea.removeChild(errArea.lastChild);
        }
        var divMessage = document.createElement("DIV");
        divMessage.setAttribute("style", "padding:5px;");
        divMessage.appendChild(document.createTextNode("File upload failed."));
        errArea.appendChild(divMessage);
    }
}

// This function adds attachments
function addAttachments() {
    $('#submitResult').hide();
    $('#Attachment').show();
    $('#team1').attr("disabled", true);
    $('#team2').attr("disabled", true);
    $('#team1Score').attr("disabled", true);
    $('#team2Score').attr("disabled", true);
    $('#date').attr("disabled", true);
    $('#matchSummary').attr("disabled", true);
    $('#notablePerformance').attr("disabled", true);
}

// This function saves the match details 
function saveMatchInfo(team1name, team2name, team1Score, team2Score) {
    addAttachments();
    list = web.get_lists().getByTitle('Match');
    var itemCreateInfo = new SP.ListItemCreationInformation();
    matchItem = list.addItem(itemCreateInfo);
    matchItem.set_item("Team1", team1name);
    matchItem.set_item("Team2", team2name);
    matchItem.set_item("HomeScore", team1Score);
    matchItem.set_item("AwayScore", team2Score);
    matchItem.set_item("date", $('#date').val());
    matchItem.set_item("MatchSummary", $('#matchSummary').text());
    matchItem.set_item("NotablePerformance", $('#notablePerformance').val());
    matchItem.set_item("TableLookup", tableID);
    matchItem.update();
    context.load(matchItem);
    context.executeQueryAsync(function () {
    },
    function (sender, args) {
        // Failure returned from executeQueryAsync
        alert("Error in saving Match: "+args.get_message());
    });
}

// This function shows attchments to Match
function showAttachments(itemID) {
    var matchList = document.getElementById("MatchAttachment");
    while (matchList.hasChildNodes()) {
        matchList.removeChild(matchList.lastChild);
    }
    var attachmentFolder = web.getFolderByServerRelativeUrl("Lists/Match/Attachments/" + itemID);
    var attachments = attachmentFolder.get_files();
    context.load(attachments);
    context.executeQueryAsync(function () {
        // Enumerate and list the Asset Attachments if they exist
        var attachementEnumerator = attachments.getEnumerator();
        while (attachementEnumerator.moveNext()) {
            var attachment = attachementEnumerator.get_current();

            var assetDelete = document.createElement("span");
            assetDelete.appendChild(document.createTextNode("X"));
            assetDelete.className = "deleteButton";
            assetDelete.id = attachment.get_serverRelativeUrl();


            $(assetDelete).click(function (sender) {
                deleteAssetAttachment(sender.target.id, itemID);
            });
            matchList.appendChild(assetDelete);
            var assetLink = document.createElement("a");
            assetLink.setAttribute("target", "_blank");
            assetLink.setAttribute("href", attachment.get_serverRelativeUrl());
            assetLink.appendChild(document.createTextNode(attachment.get_name()));
            matchList.appendChild(assetLink);
            matchList.appendChild(document.createElement("br"));
            matchList.appendChild(document.createElement("br"));
        }
    },
    function (sender, args) {
        alert("Error in showing attachments:"+ args.get_message());
    });

}

