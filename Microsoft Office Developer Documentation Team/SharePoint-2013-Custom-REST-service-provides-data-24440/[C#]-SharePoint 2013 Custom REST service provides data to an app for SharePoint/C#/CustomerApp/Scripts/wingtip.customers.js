"use strict";

var Wingtip = window.Wingtip || {};

Wingtip.Customer = function (key, fname, lname, email) {

    var id = key,
        firstName = fname,
        lastName = lname,
        emailAddress = email,

        get_id = function () { return id; },
        set_id = function (v) { id = v; },
        get_firstName = function () { return firstName; },
        set_firstName = function (v) { firstName = v; },
        get_lastName = function () { return lastName; },
        set_lastName = function (v) { lastname = v; },
        get_emailAddress = function () { return emailAddress; },
        set_emailAddress = function (v) { emailAddress = v; };

    return {
        get_id: get_id,
        set_id: set_id,
        get_firstName: get_firstName,
        set_firstName: set_firstName,
        get_lastName: get_lastName,
        set_lastName: set_lastName,
        get_emailAddress: get_emailAddress,
        set_emailAddress: set_emailAddress
    };
};

Wingtip.CustomerViewModel = function () {

    //private members
    var customers = ko.observableArray(),
        get_customers = function () { return customers; },

        readItem = function (id) {

            $.ajax(
                    {
                        url: "http://localhost:49317/api/Customers(" + id + ")",
                        type: "GET",
                        headers: {
                            "accept": "application/json;odata=verbose",
                        },
                        success: onSuccess,
                        error: onError
                    }
                );
        },

        load = function () {

            $.ajax(
                    {
                        url: "http://localhost:49317/api/Customers?$orderby=LastName",
                        type: "GET",
                        headers: {
                            "accept": "application/json;odata=verbose",
                        },
                        success: onSuccess,
                        error: onError
                    }
                );
        },

        onSuccess = function (data) {
            var results = data.d.results;

            customers.removeAll();

            for (var i = 0; i < results.length; i++) {
                customers.push(
                    new Wingtip.Customer(
                    results[i].ID,
                    results[i].FirstName,
                    results[i].LastName,
                    results[i].EmailAddress));
            }

        },

        onError = function (err) {
            alert(JSON.stringify(err));
        };


    //public interface
    return {
        load: load,
        readItem: readItem,
        get_customers: get_customers
    };

}();
