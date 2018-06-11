"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.ViewModel = function () {

    var notes = ko.observableArray(),
        activeNote = null,
        get_notes = function () { return notes; },
        get_note = function (id) {

            var matchedNote = ko.utils.arrayFirst(notes(), function (note) {
                return note.get_id() == id;
            });

            return matchedNote;
        },
        get_activeNote = function () { return activeNote; },
        set_activeNote = function (v) {
            activeNote = v;
            if (activeNote !== null) {
                activeNote.get_canvas().active(true);
            }
        },

        load = function () {
            $.ajax(
                    {
                        url: _spPageContextInfo.webServerRelativeUrl +
                            "/_api/web/lists/getByTitle('StickyNotes')/items/?$orderby=ID",
                        type: "GET",
                        headers: {
                            "accept": "application/json;odata=verbose",
                        },
                        success: function (data) {
                            var results = data.d.results;
                            notes.removeAll();
                            for (var i = 0; i < results.length; i++) {
                                notes.push(new Wingtip.StickyNote(results[i].Id, results[i].Title));
                            }

                        },
                        error: function (err) {
                            alert(JSON.stringify(err));
                        }
                    }
                );
        },

        create = function () {

            $.ajax({
                url: _spPageContextInfo.webServerRelativeUrl +
                     "/_api/web/lists/getByTitle('StickyNotes')/items",
                type: "POST",
                contentType: "application/json;odata=verbose",
                data: JSON.stringify(
                    {
                        '__metadata': {
                            'type': 'SP.Data.StickyNotesListItem'
                        },
                        'Title': 'New Note'
                    }),
                headers: {
                    "accept": "application/json;odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val()
                },
                success: function (data) {
                    for (var i = 0; i < notes().length; i++) {
                        notes()[i].get_canvas().clear();
                    }
                    load();
                },
                error: function (err) {
                    alert(JSON.stringify(err));
                }
            });
        },

        remove = function (id) {
            $.ajax(
                {
                    url: _spPageContextInfo.webServerRelativeUrl +
                        "/_api/web/lists/getByTitle('StickyNotes')/getItemByStringId('" +
                        id + "')",
                    type: "DELETE",
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                        "IF-MATCH": "*"
                    },
                    success: function (data) {
                        for (var i = 0; i < notes().length; i++) {
                            notes()[i].get_canvas().clear();
                        }
                        load();
                    },
                    error: function (err) {
                        alert(JSON.stringify(err));
                    }
                }
            );
        },

        update = function (id, title) {
            var deferred = $.ajax(
                {
                    url: _spPageContextInfo.webServerRelativeUrl +
                        "/_api/web/lists/getByTitle('StickyNotes')/getItemByStringId('" +
                        id + "')",
                    type: "POST",
                    contentType: "application/json;odata=verbose",
                    data: JSON.stringify(
                    {
                        '__metadata': {
                            'type': 'SP.Data.StickyNotesListItem'
                        },
                        'Title': title
                    }),
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                        "IF-MATCH": "*",
                        "X-Http-Method": "PATCH"
                    }
                }
            );

            return deferred
        },

        render = function (target) {
            if (typeof (target) !== 'undefined') {
                var id = target[1].id.replace("canvas", "");
                var canvas = target[1].childNodes[0];
                var note = get_note(id);
                var stickyNoteCanvas = new Wingtip.StickyNoteCanvas(7.5, 32.5, 185, 125, 10, note.get_body(), canvas);
                stickyNoteCanvas.draw();
                note.set_canvas(stickyNoteCanvas);
            }
        },

        deactivateAll = function () {
            for (var i = 0; i < notes().length; i++) {
                notes()[i].get_canvas().active(false);
            }
        };

    return {
        get_notes: get_notes,
        get_note: get_note,
        get_activeNote: get_activeNote,
        set_activeNote: set_activeNote,
        load: load,
        create: create,
        remove: remove,
        update: update,
        render: render,
        deactivateAll: deactivateAll
    };


}();