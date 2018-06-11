'use strict';

var Wingtip = window.Wingtip || {};

$(document).ready(function () {

    //Bind notes to page
    Wingtip.ViewModel.load();
    ko.applyBindings(Wingtip.ViewModel, $get("notesDiv"));

    //Disable delete button
    $("#delNote").attr("disabled", true);

    //Handle new note creation
    $("#newNote").click(function (e) {

        //Save the active note
        //and then Create a New Note
        var stickyNote = Wingtip.ViewModel.get_activeNote();
        if (stickyNote !== null) {
            Wingtip.ViewModel.update(stickyNote.get_id(), stickyNote.get_body()).then(
                function (data) {
                    Wingtip.ViewModel.set_activeNote(null);
                    Wingtip.ViewModel.create();
                },
                function (err) {
                    alert(JSON.stringify(err));
                }
            );
        }
        else {
            Wingtip.ViewModel.create();
        }

        return false;
    });

    //Handle note deletion
    $("#delNote").click(function (e) {
        var activeId = Wingtip.ViewModel.get_activeNote().get_id();
        Wingtip.ViewModel.set_activeNote(null);
        Wingtip.ViewModel.remove(activeId);
        return false;
    });

    //Handle note selection
    $("div").on("click", "canvas", function (e) {

        /* The user clicked on a note.
           Save any changes and activate the new note. */

        e.stopPropagation();

        //Save the active note
        //Activate the selected note
        var stickyNote = Wingtip.ViewModel.get_activeNote();
        if (stickyNote !== null) {
            Wingtip.ViewModel.update(stickyNote.get_id(), stickyNote.get_body()).then(
                function (data) {
                    Wingtip.ViewModel.set_activeNote(null);
                    Wingtip.ViewModel.deactivateAll();
                    Wingtip.ViewModel.set_activeNote(Wingtip.ViewModel.get_note($(e.target).parent().attr("id").replace("canvas", "")));
                    $("#delNote").attr("disabled", false);
                },
                function (err) {
                    alert(JSON.stringify(err));
                }
            );
        }
        else {
            Wingtip.ViewModel.deactivateAll();
            Wingtip.ViewModel.set_activeNote(Wingtip.ViewModel.get_note($(e.target).parent().attr("id").replace("canvas", "")));
            $("#delNote").attr("disabled", false);
        }

        return false;
    });

    //Handle clicking away from notes
    $(window).click(function (e) {

        /* The user clicked somewhere other than
           a sticky note. So deactivate all the notes.
           Be sure to save any changes */

        var stickyNote = Wingtip.ViewModel.get_activeNote();
        if (stickyNote !== null) {
            Wingtip.ViewModel.update(stickyNote.get_id(), stickyNote.get_body()).then(
                function (data) {
                    Wingtip.ViewModel.set_activeNote(null);
                    Wingtip.ViewModel.deactivateAll();
                    $("#delNote").attr("disabled", true);
                },
                function (err) {
                    alert(JSON.stringify(err));
                }
            );
        }
        else {
            Wingtip.ViewModel.deactivateAll();
            $("#delNote").attr("disabled", true);
        }

        return false;
    });

    //Handle typing
    $(window).keypress(function (e) {
        var charCode = e.keyCode;
        var charStr = String.fromCharCode(charCode);
        var stickyNote = Wingtip.ViewModel.get_activeNote();
        if (stickyNote !== null && charStr !== null && charCode !== 13) {
            stickyNote.get_canvas().keypress(charStr);
        }
    });

    //Handle backspace
    $(window).keydown(function (e) {
        var charCode = e.keyCode;
        var stickyNote = Wingtip.ViewModel.get_activeNote();
        if (stickyNote !== null) {
            stickyNote.get_canvas().keydown(charCode);
        }
    });

});

