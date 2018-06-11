// Add any initialization logic to this function.
Office.initialize = function (reason) {

    // Checks for the DOM to load.
    $(document).ready(function () {
        Office.context.document.bindings.getByIdAsync('binding1', function (asyncResult) {
            if (asyncResult.status == Office.AsyncResultStatus.Failed) {
                debug("Please select a cell on a blank sheet and click Refresh to get started");
            }
        });
    });
}


$(document).ready(function () {

    // Event handler for button onclick event 
    $('#btnGetTweets').click(function () {


        var searchTerm = $('#txtSearch').val();

        if (searchTerm == "" || searchTerm == "Enter search term...") {
            debug("Please enter a Twitter search term.");
        }
        else {
            debug("Refreshing...please wait");
            // Create URL with Twitter search query term 
            var requestStr = "http://search.twitter.com/search.json?q=_QUERY&callback=twitterCallback2";
            var completeURL = requestStr.replace('_QUERY', searchTerm);

            // Get around cross-origin stuff with dynamically added script tag 
            var script = document.createElement("script");
            script.setAttribute("src", completeURL);
            document.getElementsByTagName('head')[0].appendChild(script);

        } // End if-else 

    }); // End of button onclick event handler 


}); // End of $(document).ready 

function twitterCallback2(data) {

    if (data.results.length > 0) {

        var table = new Office.TableData();
        var maxResults;

        if ($('#tweetAmount').val() < data.results.length) {
            maxResults = $('#tweetAmount').val();
        }
        else {
            maxResults = data.results.length;
            debug("The number of tweets requested exceeeded the number available.");
        }
        //var maxResults = Math.min(Math.floor((Math.random() * 10) + 5), data.results.length);
        var myMatrix = new Array(maxResults);
        var theResult = null;
        var j = 0;
        // Loop through collection of search results i.e., tweets 

        for (i = 0, j = 0; i < maxResults; i++, j++) {
            theResult = data.results[i];
            // Get the username, text, and time tweeted for each result 
            myMatrix[j] = new Array(theResult.from_user_name, theResult.text, theResult.created_at);

        } // End for loop 

        table.headers = new Array(1);
        table.headers[0] = new Array('Twitter Username', 'Tweet text', 'Time of Tweet');
        table.rows = myMatrix;


        //check if table exists if so, refresh it
        Office.context.document.bindings.getByIdAsync('binding1', function (asyncResult) {
            if (asyncResult.status == Office.AsyncResultStatus.Succeeded) {
                var binding = asyncResult.value;
                binding.getDataAsync(
                {
                    coercionType: Office.CoercionType.Table,
                    valueFormat: Office.ValueFormat.Formatted,
                    filterType: Office.FilterType.OnlyVisible
                },
                function (asyncResult) {
                    if (asyncResult.status == "succeeded") {
                        //refresh table
                        RefreshTable(myMatrix);
                    }
                    else {
                        Office.context.document.setSelectedDataAsync(table, { coercionType: "table" }, function (result) {
                            //bind to table
                            Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, { id: "binding1" }, function (bindResult) {
                                if (bindResult.status == "succeeded") {
                                    debug("Returned " + bindResult.value.rowCount + " results.");
                                };
                            });
                        });
                    }
                });
            }
            else {
                Office.context.document.setSelectedDataAsync(table, { coercionType: "table" }, function (result) {
                    //bind to table
                    Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, { id: "binding1" }, function (bindResult) {
                        if (bindResult.status == "succeeded") {
                            debug("Returned " + bindResult.value.rowCount + " results.");
                        };
                    });
                });
            }
        });
    } // End of twitterCallback2
}

function RefreshTable(data) {

    Office.context.document.bindings.getByIdAsync('binding1', function (asyncResult) {
        if (asyncResult.status == Office.AsyncResultStatus.Succeeded) {

            var binding = asyncResult.value;

            if (binding.hasHeaders == false) {
                debug("I can't refresh the table that doesn't have headers. Turn on the table header and try again.");
            }
            else {
                binding.deleteAllDataValuesAsync(function (deleteResult) {
                    if (deleteResult.status == "succeeded") {
                        binding.addRowsAsync(data, function () {
                            debug("Returned " + data.length + " results.");
                        });

                    }
                });
            }
        }
    });
}

function debug(message) {
    document.getElementById("tweet").innerHTML = message;
}