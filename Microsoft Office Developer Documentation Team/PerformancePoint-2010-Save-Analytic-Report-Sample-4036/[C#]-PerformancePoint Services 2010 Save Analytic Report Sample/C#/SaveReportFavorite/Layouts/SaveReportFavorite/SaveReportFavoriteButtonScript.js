//*********************************************************
//
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

function findAnalyticReports() {
    var reports = getReportsFromWebPartRecords();
    return (reports.length > 0); 
}

// Get report information that is stored in ViewState.
function getReportsFromWebPartRecords() { 
    var webPartRecords = PPSMA.ClientConnectionManager.get_instance().get_connectionManagerRecord().WebPartRecords;
    var reports = new Array();
    var index = 0;
    for (var i = 0; i < webPartRecords.length; i++) {
        var wP = webPartRecords[i];
        if ((wP.ViewState["D90A8712A3FA4649A25B7AB942FBCF20"]) &&
           ((wP.ViewState["D90A8712A3FA4649A25B7AB942FBCF20"] == "OLAPGrid") ||
           (wP.ViewState["D90A8712A3FA4649A25B7AB942FBCF20"] == "AnalyticChart"))) {

            reports[index] = new Array();

            // The lastReportId property contains the identifier of the current version of the report. For navigated reports, 
            // the ID is a GUID and the report definition is stored in the back-end database.
            reports[index].lastReportId = wP.ViewState["F775B8BE98A540C2AB08B34D53460E4B"];
            reports[index].clientId = wP.ClientId;

            var clientWP = PPSMA.ClientConnectionManager.get_instance().findClientWebPart(wP.ClientId);
            reports[index].name = clientWP.webPartTitle;
            index++;
        }
    }
    return reports;
}

function saveAnalyticReports() {
    var reportsOnPage = getReportsFromWebPartRecords();
    var reportCount = reportsOnPage.length;
    var reportIds = "reportCount=" + reportCount;
    for (i = 0; i < reportCount; i++) {
        if (reportsOnPage[i]) {
            var indexOfComma = reportsOnPage[i].lastReportId.indexOf(',');
            reportIds += "&reportId" + i + "=" 
                         + reportsOnPage[i].lastReportId.substr(12, (indexOfComma - 12))
                         + "," + reportsOnPage[i].name;
        }
    }
    var options = {
        url: "/_layouts/SaveReportFavorite/SaveReport.aspx?" + reportIds,
        width: 500,
        height: 400
    };
    SP.UI.ModalDialog.showModalDialog(options);
}
