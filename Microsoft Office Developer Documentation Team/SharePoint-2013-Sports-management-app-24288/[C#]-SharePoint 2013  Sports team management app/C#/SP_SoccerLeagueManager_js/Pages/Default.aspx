<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.10.2.js"></script>
    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link rel="Stylesheet" type="text/css" href="../Content/jquery-ui-1.10.2.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <div id="tableHome" style="background-color: white; color: black; width: 200px; height: 50px;" onclick="displayHome();">
        <div class="homeHeadingText" unselectable="on"><u>Home</u></div>
    </div>

</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div id="Home" style="display: none; width: 50%;">
        <div class="tile tileEvent" id="TablesTile" onclick="showTables();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Create New League</div>
        </div>
        <div class="tile tileEvent" id="TeamsTile" onclick="showAllLeague();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">View Leagues</div>
        </div>

        <div id="errGeneral" class="errorClass"></div>
    </div>
    <div class="clear">&nbsp;</div>

    <div style="margin-left: 10px; float: left; width: 800px">

        <!--All Tables View-->
        <div id="allTables" style="display: none; float: left; width: 100%;">
            <div class="tab" onclick="showAllTable();">View All Leagues</div>
            <div class="tab" onclick="showTables();">Create New League</div>

        </div>
        <!--end All Table-->

        <!--Show All table View-->
        <div id="showAllLeague" style="display: none; width: 100%; height: 80%; background-color: lightgray;">
            <div class="clear" style="height: 50px;">&nbsp;</div>
            <div class="formLabel" style="float: left; margin-left: 40px;">Select League to view</div>
            <select id="allLeague" style="width: 48%; height: 40px; margin-left: 10px; font-size: 24px;"></select>
            <div class="clear" style="height: 80px;">&nbsp;</div>
            <div class="viewTableButton" onclick="showLeague();" style="margin-left: 230px;">View League</div>
            <div class="viewTableButton" onclick="deleteTables();" style="margin-left: 20px;">Delete League</div>
            <div class="clear" style="height: 30px;">&nbsp;</div>
        </div>

        <!--end Show All Table-->

        <!--New Table View -->
        <div id="AddTables" style="display: none; height: auto; width: 100%; background-color: lightgray;">
            <div id="errAllTables" class="errorClass"></div>
            <div class="clear" style="height: 20px">&nbsp;</div>
            <div id="TablesHeading" class="formLabel">Enter League Name</div>
            <input type="text" id="newTableName" style="width: 35%; float: left; height: 30px; font-family: 'sans-serif','Segoe UI'; font-size: 24px;" />

            <div id="addNewTable" class="addTeamButton" onclick="saveNewTable();">Add League</div>
            <div class="clear" style="height: 10px">&nbsp;</div>

            <div id="advancedOptions" class="clicker" onclick="addAdvancedOption();">
                <u>+Advanced Options</u>
                <div class="clear">&nbsp;</div>
                <div id="editOptions" style="display: none; float: left;">
                    <div class="formLabel">Win Points</div>
                    <input type="text" id="winPoints" disabled="disabled" value="3" />
                    <div class="clear">&nbsp;</div>
                    <div class="formLabel">Lose Points</div>
                    <input type="text" id="losePoints" disabled="disabled" value="0" />
                    <div class="clear">&nbsp;</div>
                    <div class="formLabel">Draw Points</div>
                    <input type="text" id="drawPoints" disabled="disabled" value="1" />
                    <div class="clear">&nbsp;</div>
                    <div class="formLabel">No Score Draw Points</div>
                    <input type="text" id="noScoreDrawPoints" disabled="disabled" value="1" />
                    <div class="clear">&nbsp;</div>
                    <div class="clear">&nbsp;</div>
                    <div class="clear">&nbsp;</div>
                    <div class="clear">&nbsp;</div>
                </div>
            </div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div id="DeleteTable" class="addButton" onclick="showDelete();">Delete Existing League</div>
            <div class="clear" style="height: 30px">&nbsp;</div>

            <div id="deleteTables" style="display: none; float: left; background-color: lightgray; width: 100%;">
                <select id="showTables" style="float: left; height: 50px; width: 30%; font-family: 'sans-serif', 'Segoe UI'; font-size: 24px; margin-left: 14px;"></select>
                <div class="clear" style="height: 30px">&nbsp;</div>
                <div class="addButton" onclick="deleteTable();">Delete League</div>
                <div class="clear" style="height: 30px">&nbsp;</div>
            </div>
        </div>
        <!-- end Add Table-->

        <!-- Table Details view -->
        <div id="addTeamForm" style="display: none; background-color: lightgray; width: 804px">
            <div class="tile" id="TableName" unselectable="on"></div>
            <div class="tabs" onclick="showLeagueTable();">View League</div>

            <!-- start add team-->
            <div class="tabs" onclick="addTeams();">Add Teams</div>

            <!-- start Results-->
            <div class="tabs" onclick="addResult();">Add Results</div>

            <!-- end Results-->
        </div>

        <div class="clear" style="height: 10px;">&nbsp;</div>

        <div id="AllLeagues" style="display: none; float: left; width: 100%">
            <div id="errAllLeagues" class="errorClass"></div>
            <div id="LeaguesHeading" class="listHeading">Events</div>
            <div id="LeagueList">
            </div>
        </div>

        <div id="LeagueTableDetails" style="display: none; background-color: whitesmoke; width: 800px">
            <table id="pointTable" cellspacing="10" cellpadding="10" border="1" class="leagueTable" style="clear: both; margin-bottom: 3px;">
                <thead>
                    <tr class="tableHeader">
                        <th style="width: 50px">Pos</th>
                        <th style="width: 150px">Team Names</th>
                        <th style="width: 50px">P</th>
                        <th style="width: 50px">W</th>
                        <th style="width: 50px">L</th>
                        <th style="width: 50px">D</th>
                        <th style="width: 50px">NSD</th>
                        <th style="width: 50px">F</th>
                        <th style="width: 50px">A</th>
                        <th style="width: 50px">Pts</th>
                        <th style="width: 50px">Fixture</th>
                    </tr>
                </thead>
                <tbody id="tablebody">
                </tbody>
            </table>
        </div>


        <!-- start add team-->
        <div id="addTeams" style="display: none; width: 100%; height: auto; background-color: lightgray;margin-bottom:10px;">
            <div class="clear" style="height: 30px;">&nbsp;</div>
            <div id="TeamHeading" class="formLabel">Enter Team Name</div>
            <input type="text" id="newTeamName" style="width: 25%; float: left; height: 30px; font-family: 'sans-serif','Segoe UI'; font-size: 24px;" />
            <div id="addNewTeam" class="addTeamButton" onclick="saveNewTeam();">Add Team</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div id="noTeams"></div>
            <div class="formLabel">Newly Added Teams</div>
            <div class="clear">&nbsp;</div>
            <div id="deleteTeamsList"></div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Played Teams</div>
            <div class="clear">&nbsp;</div>
            <div id="teamsList"></div>
            <div class="clear">&nbsp;</div>
            
        </div>
        <!-- end add team-->

        <div id="saveResult" style="display: none; width: 100%; background-color: lightgray;">
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel" style="margin-left: 70px; font-size: 26px; float: left">Home</div>
            <div class="formLabel" style="margin-left: 155px; font-size: 26px;">Away</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <select id="team1" style="width: 37%; height: 30px; font-size: 18px; float: left; margin-left: 70px;"></select>
            <select id="team2" style="width: 37%; height: 30px; font-size: 18px; margin-left: 100px;"></select>

            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div style="font-size: 24px; margin-left: 400px; color: white;">VS</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <input id="team1Score" type="text" value="0" style="width: 35.5%; height: 30px; font-size: 18px; float: left; margin-left: 70px;" />
            <input id="team2Score" type="text" value="0" style="width: 35.5%; height: 30px; font-size: 18px; margin-left: 100px;" />
            <div class="clear" style="height: 40px;">&nbsp;</div>

            <div class="formLabel" style="margin-left: 75px">Match Date</div>
            <input type="text" id="date" readonly="readonly" style="width: 25%; height: 30px; font-size: 18px; margin-left: 5px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel" style="margin-left: 75px">Match Summary</div>
            <textarea id="matchSummary" rows="3" cols="42" style="margin-left: 5px; font-size: 18px;"></textarea>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel" style="margin-left: 75px">Notable Performance</div>
            <input id="notablePerformance" type="text" style="width: 423px; height: 30px; font-size: 18px; float: left; margin-left: 5px;" />
            <div class="clear" style="height: 20px;">&nbsp;</div>

            <div id="submitResult" class="button" onclick="submitResult();">Submit</div>
            <div class="clear">&nbsp;</div>

            <div id="Attachment" style="float: left; display: none; background-color: lightgray; width: 800px;">
                <div id="completeResult" class="button" onclick="completeResult();" style="float: right; margin-right: 40px;">Complete</div>
                <div class="formLabel" style="margin-left: 75px">Add Assets :</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
                <input id="matchUpload" type="file" style="width: 460px; margin-left: 75px;" />
                <div class="clear" style="height: 30px">&nbsp;</div>

                <div id="MatchAttachment"></div>

                <div class="clear">&nbsp;</div>

            </div>

        </div>


        <div class="clear" style="height: 20px;">&nbsp;</div>

        <div id="TeamFixtureDetails" style="display: none; background-color: whitesmoke; width: 800px">
            <div id="fixtureTeamName" class="formLabel"></div>
            <table id="fixtureTable" cellspacing="10" cellpadding="10" border="1" class="leagueTable" style="clear: both; margin-bottom: 3px;">
                <thead>
                    <tr class="tableHeader">
                        <th style="width: 70px">Date</th>
                        <th style="width: 25px">Home Team</th>
                        <th style="width: 25px">Away Team</th>
                        <th style="width: 25px">Home Score</th>
                        <th style="width: 25px">Away Score</th>
                        <th style="width: 70px">Match Summary</th>
                        <th style="width: 60px">Notable Performace</th>
                        <th style="width: 40px">Assets</th>
                    </tr>
                </thead>
                <tbody id="fixtureBody">
                </tbody>
            </table>
        </div>

        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="Assets" style="display: none; font-size: 20px; float: left; font-family: Segoe UI Light, Segoe UI, sans-serif; margin-right: 20px">Assets</div>
        <div id="AssetAttachments" style="display: none; width: 800px; height: 20px">
        </div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="attachmentDetails"></div>
    </div>
</asp:Content>
