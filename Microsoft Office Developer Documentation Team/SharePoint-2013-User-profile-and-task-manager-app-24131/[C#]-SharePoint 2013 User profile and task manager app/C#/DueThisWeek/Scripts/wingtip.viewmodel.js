"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.Task = function (title, description) {

    var _title = title,
        _description = description,
        get_title = function () { return _title; },
        set_title = function (v) { _title = v; },
        get_description = function () { return _description; },
        set_description = function (v) {
            _description = v;
            if (v === null || v.length === 0) {
                _description = "No description available";
            }
        };

    return {
        get_title: get_title,
        set_title: set_title,
        get_description: get_description,
        set_description: set_description
    }

};

Wingtip.Person = function () {

    var accountName = null,
        displayName = null,
        photourl = null,
        reports = null,
        tasks = ko.observableArray(),
        get_accountName = function () { return accountName; },
        set_accountName = function (v) { accountName = v; },
        get_displayName = function () { return displayName; },
        set_displayName = function (v) { displayName = v; },
        get_photoUrl = function () { return photourl; },
        set_photoUrl = function (v) {
            photourl = v;
            if (v === null || v.length === 0) {
                photourl = "/_layouts/15/images/PersonPlaceholder.42x42x32.png";
            }
        },
        get_directReports = function () { return reports; },
        set_directReports = function (v) { reports = v; },
        get_tasks = function () { return tasks; },
        set_tasks = function (v) { tasks = v; };

    return {
        get_accountName: get_accountName,
        set_accountName: set_accountName,
        get_displayName: get_displayName,
        set_displayName: set_displayName,
        get_photoUrl: get_photoUrl,
        set_photoUrl: set_photoUrl,
        get_directReports: get_directReports,
        set_directReports: set_directReports,
        get_tasks: get_tasks,
        set_tasks: set_tasks
    };

};

Wingtip.ViewModel = function () {

    var people = ko.observableArray(),
        get_people = function () { return people; },

        load = function () {

            //This promise will resolve when all of the data and tasks
            //for both the curren user and any direct reports is gathered
            var deferred = $.Deferred();

            new Wingtip.PeopleManager().get_myProperties().then(

                //Success: Data - current user
                function (data) {

                    people.removeAll();

                    var me = new Wingtip.Person();
                    me.set_accountName(data.get_userProfileProperties()["AccountName"]);
                    me.set_displayName(data.get_displayName());
                    me.set_photoUrl(data.get_userProfileProperties()["PictureURL"]);
                    me.set_directReports(data.get_directReports());
                    people.push(me);

                    //Tasks - current user
                    new Wingtip.PeopleManager().get_userTasks(me.get_displayName()).then(

                        //Success: Tasks - current user
                        function (data) {

                            me.get_tasks().removeAll();

                            if (data !== null) {
                                for (var r = 0; r < data.RowCount; r++) {
                                    var task = new Wingtip.Task();
                                    task.set_title(data.ResultRows[r].Title);
                                    task.set_description(data.ResultRows[r].Description);
                                    me.get_tasks().push(task);
                                }
                            }
                            if (me.get_tasks()().length === 0) {
                                var task = new Wingtip.Task();
                                task.set_title("No tasks due this week");
                                me.get_tasks().push(task);
                            }

                            //Check App Settings
                            new Wingtip.PeopleManager().get_appSetting("ShowDirectReports").then(

                                //Success: Check App Settings
                                function (data) {

                                    if (data.toUpperCase() === "YES" && me.get_directReports() !== null) {

                                        //Get Data - direct report
                                        for (var d = 0; d < me.get_directReports().length; d++) {

                                            new Wingtip.PeopleManager().get_userProperties(me.get_directReports()[d]).then(

                                                //Success: Get Data - direct report
                                                function (data) {

                                                    var person = new Wingtip.Person();
                                                    person.set_accountName(data[0]);
                                                    person.set_displayName(data[1]);
                                                    person.set_photoUrl(data[2]);
                                                    people.push(person);

                                                    //Tasks - direct report
                                                    new Wingtip.PeopleManager().get_userTasks(person.get_displayName()).then(

                                                        //Success: Tasks - direct report
                                                        function (data) {
                                                            person.get_tasks().removeAll();

                                                            if (data !== null) {
                                                                for (var r = 0; r < data.RowCount; r++) {
                                                                    var task = new Wingtip.Task();
                                                                    task.set_title(data.ResultRows[r].Title);
                                                                    task.set_description(data.ResultRows[r].Description);
                                                                    person.get_tasks().push(task);
                                                                }
                                                            }
                                                            if (person.get_tasks()().length === 0) {
                                                                var task = new Wingtip.Task();
                                                                task.set_title("No tasks due this week");
                                                                person.get_tasks().push(task);
                                                            }

                                                        },
                                                        //Failure: Tasks - direct report
                                                        function (sender, args) {
                                                            deferred.reject(args.get_message());
                                                        }
                                                   );
                                                },
                                                //Failure: Get Data - direct report
                                                function (sender, args) {
                                                    deferred.reject(args.get_message());
                                                }
                                            );

                                        }

                                        //Finished gathering all data, resolve the promise 
                                        deferred.resolve();
                                    }
                                    else {

                                        //ShowDirectReports setting is "No", so resolve promise now
                                        deferred.resolve();
                                    }
                                },
                                //Failure: Check App Settings
                                function (sender, args) {
                                    deferred.reject(args.get_message());
                                }
                            );

                        },
                        //Failure: Tasks - current user
                        function (sender, args) {
                            deferred.reject(args.get_message());
                        }
                    );

                },
                //Failure: Data - current user
                function (sender, args) {
                    deferred.reject(args.get_message());
                }
            );

            //Return the promise
            return deferred.promise();

        },

        animate = function (target) {
            $("#tasksDisplay").accordion();
            $("#tasksDisplay").accordion("destroy");
            $("#tasksDisplay").accordion();
        };

    return {
        load: load,
        animate: animate,
        get_people: get_people
    };

}();