function showPersonas() {
    $("#personaDlg").dialog({ modal: true, title: 'Personas', width: 900, position: { my: "left top", at: "left+50 top+20", of: header } });
}
function showSchedule() {
    $("#scheduleDlg").dialog({ modal: true, title: 'Schedule Event', width: 675, position: { my: "left top", at: "left+10 top+10", of: UC2Block } });
}
function showAgenda() {
    $("#agendaDlg").dialog({ modal: true, title: 'Manage Agenda', width: 550, position: { my: "left top", at: "left+10 top+10", of: UC2Block } });
}
function showFacilities() {
    $("#facilitiesDlg").dialog({ modal: true, title: 'Manage Facilities', width: 550, position: { my: "left top", at: "left+10 top+10", of: UC6Block } });
}
function showCatering() {
    $("#cateringDlg").dialog({ modal: true, title: 'Manage Catering', width: 550, position: { my: "left top", at: "left+10 top+10", of: UC8Block } });
}
function showScheduleUC3() {
    $("#scheduleDlg").dialog({ modal: true, title: 'Schedule Event', width: 675, position: { my: "left top", at: "left+10 top+10", of: UC3Table } });
}
function showAgendaUC3() {
    $("#agendaDlg").dialog({ modal: true, title: 'Manage Agenda', width: 550, position: { my: "left top", at: "left+10 top+10", of: UC3Table } });
}
//addFacilityItem
function addFacilityItem() {
    if ($("#newFItem1").is(':visible') == false) {
        $("#newFItem1").show();
        return;
    }
    if ($("#newFItem2").is(':visible') == false) {
        $("#newFItem2").show();
        return;
    }
    if ($("#newFItem3").is(':visible') == false) {
        $("#newFItem3").show();
        return;
    }
    if ($("#newFItem4").is(':visible') == false) {
        $("#newFItem4").show();
        return;
    }
    if ($("#newFItem5").is(':visible') == false) {
        $("#newFItem5").show();
        return;
    }



}

function addCateringItem() {
    if ($("#catering1").is(':visible') == false) {
        $("#catering1").show();
        return;
    }
    if ($("#catering2").is(':visible') == false) {
        $("#catering2").show();
        return;
    }
    if ($("#catering3").is(':visible') == false) {
        $("#catering3").show();
        return;
    }
    if ($("#catering4").is(':visible') == false) {
        $("#catering4").show();
        return;
    }
    if ($("#catering5").is(':visible') == false) {
        $("#catering5").show();
        return;
    }



}


function addAgendaItem() {
    if ($("#newItem1").is(':visible') == false) {
        $("#newItem1").show();
        return;
    }
    if ($("#newItem2").is(':visible') == false) {
        $("#newItem2").show();
        return;
    }
    if ($("#newItem3").is(':visible') == false) {
        $("#newItem3").show();
        return;
    }
    if ($("#newItem4").is(':visible') == false) {
        $("#newItem4").show();
        return;
    }
    if ($("#newItem5").is(':visible') == false) {
        $("#newItem5").show();
        return;
    }



}
function showEventUC3() {
    $("#UC3Event").dialog({ modal: true, title: 'Event Details', width: 675, position: { my: "left top", at: "left+10 top+10", of: uc3Cal } });

}
function showScheduleUC3Dlg() {
    $("#scheduleDlg").dialog({ modal: true, title: 'Schedule Event', width: 675, position: { my: "left top", at: "left+10 top+10", of: UC3Table } });
}
function showAgendaUC3Dlg() {
    $("#agendaDlg").dialog({ modal: true, title: 'Manage Agenda', width: 550, position: { my: "left top", at: "left+10 top+10", of: UC3Table } });
}

function showScheduleUC4() {
    $("#scheduleDlg").dialog({ modal: true, title: 'Schedule Event', width: 675, position: { my: "left top", at: "left+10 top+10", of: UC4Table } });
}
function showAgendaUC4() {
    $("#agendaDlg").dialog({ modal: true, title: 'Manage Agenda', width: 550, position: { my: "left top", at: "left+10 top+10", of: UC4Table } });
}
function showBudgetUC4() {
    $("#budgetDlg").dialog({ modal: true, title: 'Manage Budget', width: 600, position: { my: "left top", at: "left+10 top+10", of: UC4Table } });
}

function showSpeakers() {
    $("#speakerDlg").dialog({ modal: true, title: 'Manage Speakers', width: 675, position: { my: "left top", at: "left+10 top+10", of: UCBlock9 } });
}

function showAudience() {
    $("#audienceDlg").dialog({ modal: true, title: 'Manage Attendees', width: 675, position: { my: "left top", at: "left+10 top+10", of: UCBlock9 } });
}

function addQuestion() {
    $('#newQuestion').fadeIn(500, null);
}

function qTypeChange() {
    if ($('#qType').val() == "6") {
        $('#mChoiceArea').fadeIn(500, null);
    }
    else {
        $('#mChoiceArea').fadeOut(500, null);
    }
}