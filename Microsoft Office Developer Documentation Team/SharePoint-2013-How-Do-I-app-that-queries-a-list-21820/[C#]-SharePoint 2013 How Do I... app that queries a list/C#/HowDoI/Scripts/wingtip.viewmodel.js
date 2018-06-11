"use strict";

var WingtipToys = window.WingtipToys || {};

WingtipToys.ListItem = function (id, title, question, answer) {

    //private members
    var i = id,
        t = title,
        q = question,
        a = answer,
        set_id = function (v) { i = v; },
        get_id = function () { return i; },
        set_title = function (v) { t = v; },
        get_title = function () { return t; },
        set_question = function (v) { q = v; },
        get_question = function () { return q; },
        set_answer = function (v) { a = v; },
        get_answer = function () { return a; };

    //public interface
    return {
        set_id: set_id,
        get_id: get_id,
        set_title: set_title,
        get_title: get_title,
        set_question: set_question,
        get_question: get_question,
        set_answer: set_answer,
        get_answer: get_answer
    };
}

WingtipToys.ViewModel = function () {

    //private members
    var items = ko.observableArray(),
        getItems = function () { return items; },

        load = function (listTitle) {

            //Load Items
            WingtipToys.ListItems.readAll(listTitle).then(
                function (listItems) {
                    items.removeAll();

                    var enumerator = listItems.getEnumerator();

                    while (enumerator.moveNext()) {
                        var listItem = enumerator.get_current();
                        items.push(new WingtipToys.ListItem(
                            listItem.get_item("ID"),
                            listItem.get_item("Title"),
                            listItem.get_item("Question"),
                            listItem.get_item("Answer")));
                    }

                },
                function (sender, args) {
                    items.removeAll();
                    alert(args.get_message());
                }
             );

        },

        loadByQuery = function (listTitle, freetext) {

            //Clear items
            items.removeAll();

            //Split the question into words
            var words = freetext.replace("?", "").split(" ");

            //Search for each word
            for (var i = 0; i < words.length; i++) {

                if (words[i].length > 5) {

                    //Query
                    WingtipToys.ListItems.query(listTitle, words[i]).then(
                        function (listItems) {

                            var enumerator = listItems.getEnumerator();

                            //Enumerate results
                            while (enumerator.moveNext()) {
                                var listItem = enumerator.get_current();
                                
                                //Check for duplicates
                                var match = ko.utils.arrayFirst(items(), function(item){
                                    return item.get_id() === listItem.get_item("ID");
                                });

                                //Add to the results
                                if (!match) {
                                    items.push(new WingtipToys.ListItem(
                                        listItem.get_item("ID"),
                                        listItem.get_item("Title"),
                                        listItem.get_item("Question"),
                                        listItem.get_item("Answer")));
                                }
                            }

                        },
                        function (sender, args) {
                            items.removeAll();
                            alert(args.get_message());
                        }
                    );

                }

            }

       };


    //public interface
    return {
        load: load,
        loadByQuery: loadByQuery,
        get_items: getItems
    };

}();
