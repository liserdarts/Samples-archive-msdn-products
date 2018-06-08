function insert(item, user, request) {
    var insertInQueue = function() {
        var azure = require('azure');
        var serviceBusService = azure.createServiceBusService('<namespace name>', '<namespace key>');

        serviceBusService.createQueueIfNotExists('<queue name>', function(error) {
            if (!error) {
                serviceBusService.sendQueueMessage('<queue name>', '"' + item.text + '"', function(error) {
                    if (!error) {
                        console.log('sent message: ' + item.id);
                    }
                });
            }
        });
    };

    request.execute({
        success: function () {
            insertInQueue();
            request.respond();
        }
    });
}