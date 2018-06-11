/*
 * File     : itemPropsRS.js
 * Author   : Microsoft
 */

Type.registerNamespace('Olk.ItemProps');

// This function determines and displays
// properties values that are available 
// on the selected item. This function
// assumes that the mail app has restricted
// permission.
Olk.ItemProps.Ext = function ItemPropsExt(appOm)
{
    var _item = appOm.item;
    
    var _msgOnly = 
        "Only available on the MESSAGE object.";
    var _apptOnly = 
        "Only available on the APPOINTMENT object.";
    var _calOnly = 
        "Only available on appointment or meeting request items.";
    
    // Check if an item is an appointment or meeting request.
    var _isCalendarItem = function()
    {
        if ( (_item.itemType == Office.MailboxEnums.ItemType.Appointment) ||
             (_item.itemClass.indexOf("IPM.Schedule") != -1) )
        {
            return true;
        }
        
        return false;
    }
    
    var _showAPIReady = function()
    {
        $("div#apiReady").text("API IS READY");
        $("body").css("background-color", "#298ed6");
        $("body").css("color", "#ffffff");
    }

    // This property is available on the Message or
    // Appointment object.    
    var _getSubject = function()
    {
        $("td#subject").text(_item.subject);
    }
    
    // This property is available on the Message or
    // Appointment object.
    var _getNormalizedSubject = function()
    {
        $("td#normalizedSubject").text(_item.normalizedSubject);
    }
    
    // This property is available on the Message or
    // Appointment object.
    var _getItemClass = function()
    {
        $("td#itemClass").text(_item.itemClass);
    }
    
    // This property is available on the Message or
    // Appointment object.
    var _getItemType = function()
    {
        $("td#itemType").text(_item.itemType);
    }
    
    // This property is available on the Message or
    // Appointment object.
    // It represents a JavaScript Date object.
    var _getDateTimeCreated = function()
    {
        var date = _item.dateTimeCreated;
        
        // Show how to extract the different components of 
        // a date-time value.
        var info = "";
        info += "Full Date : " + date.toString() + "<br/>";
        info += "Full Year : " + date.getFullYear() + "<br/>";
        // date.getMonth returns 0-based values, so increment by 1.
        info += "Month : " + (date.getMonth()+1) + "<br/>";
        info += "Day of Month : " + date.getDate() + "<br/>";
        info += "Day of Week : " + date.getDay() + "<br/>";
        info += "Hours : " + date.getHours() + "<br/>";
        info += "Minutes : " + date.getMinutes() + "<br/>";
        info += "Seconds : " + date.getSeconds() + "<br/>";
        
        $("td#dateTimeCreated").html(info);
    }
    
    // This property is available on the Message or
    // Appointment object.
    // It represents a JavaScript Date object.
    var _getDateTimeModified = function()
    {
        var date = _item.dateTimeModified;
        
        // Show how to extract the different components of 
        // a date-time value.
        var info = "";
        info += "Full Date : " + date.toString() + "<br/>";
        info += "Full Year : " + date.getFullYear() + "<br/>";
        // date.getMonth returns 0-based values, so increment by 1.
        info += "Month : " + (date.getMonth()+1) + "<br/>";
        info += "Day of Month : " + date.getDate() + "<br/>";
        info += "Day of Week : " + date.getDay() + "<br/>";
        info += "Hours : " + date.getHours() + "<br/>";
        info += "Minutes : " + date.getMinutes() + "<br/>";
        info += "Seconds : " + date.getSeconds() + "<br/>";
        
        $("td#dateTimeModified").html(info);
    }
    
    // This property is available on the Message or
    // Appointment object.
    var _getItemId = function()
    {
        $("td#itemId").text(_item.itemId);
    }
    
    // This property is available on only the Message object.
    var _getConversationId = function()
    {
        if (_item.itemType == Office.MailboxEnums.ItemType.Message)
        {
            $("td#conversationId").text(_item.conversationId);
        }
        else
        {
            $("td#conversationId").text(_msgOnly);
        }
    }
    
    // This property is available on only the Message object.
    var _getInternetMessageId = function()
    {
        if (_item.itemType == Office.MailboxEnums.ItemType.Message)
        {
            $("td#internetMessageId").text(_item.internetMessageId);
        }
        else
        {
            $("td#internetMessageId").text(_msgOnly);
        }
    }
    
    // This property is available on only a meeting request,
    // response or cancellation, or an appointment item.
    // It represents a JavaScript Date object.
    var _getStart = function()
    {
        if (_isCalendarItem())
        {
            var date = _item.start;
            
            // Show how to extract the different components of 
            // a date-time value.
            var info = "";
            info += "Full Date : " + date.toString() + "<br/>";
            info += "Full Year : " + date.getFullYear() + "<br/>";
            // date.getMonth returns 0-based values, so increment by 1.
            info += "Month : " + (date.getMonth()+1) + "<br/>";
            info += "Day of Month : " + date.getDate() + "<br/>";
            info += "Day of Week : " + date.getDay() + "<br/>";
            info += "Hours : " + date.getHours() + "<br/>";
            info += "Minutes : " + date.getMinutes() + "<br/>";
            info += "Seconds : " + date.getSeconds() + "<br/>";
            
            $("td#start").html(info);
        }
        else
        {
            $("td#start").text(_calOnly);
        }
    }
    
    // This property is available on only a meeting request,
    // response or cancellation, or an appointment item.
    // It represents a JavaScript Date object.
    var _getEnd = function()
    {
        if (_isCalendarItem())
        {
            var date = _item.end;
            
            var info = "";
            info += "Full Date : " + date.toString() + "<br/>";
            info += "Full Year : " + date.getFullYear() + "<br/>";
            // date.getMonth returns 0-based values, so increment by 1.
            info += "Month : " + (date.getMonth()+1) + "<br/>";
            info += "Day of Month : " + date.getDate() + "<br/>";
            info += "Day of Week : " + date.getDay() + "<br/>";
            info += "Hours : " + date.getHours() + "<br/>";
            info += "Minutes : " + date.getMinutes() + "<br/>";
            info += "Seconds : " + date.getSeconds() + "<br/>";
            
            $("td#end").html(info);
        }
        else
        {
            $("td#end").text(_calOnly);
        }
    }
    
    // This property is available on only a meeting request,
    // response or cancellation, or an appointment item.
    var _getLocation = function()
    {
        if (_isCalendarItem())
        {
            $("td#location").text(_item.location);
        }
        else
        {
            $("td#location").text(_calOnly);
        }
    }
    
    this.start = function()
    {
        _showAPIReady();
        // The following properties are available on
        // appropriate items for all mail apps 
        // independent of permission.
        _getSubject();
        _getNormalizedSubject();
        _getItemClass();
        _getItemType();
        _getDateTimeCreated();
        _getDateTimeModified();
        _getItemId();
        _getConversationId();
        _getInternetMessageId();
        _getStart();
        _getEnd();
        _getLocation();
    }
}

Olk.ItemProps.Ext.init = function InitExt()
{
    // Event handler for the Office.initialize event
    // to make sure the runtime environment is ready.
    Office.initialize = function ()
    {
        // Instantiates an instance of the object Ext.
        var myExt = 
            new Olk.ItemProps.Ext(Office.context.mailbox);

        // Checks for the DOM to load.
        $(document).ready (function() {
            // Both runtime environment and
            // DOM are ready, start mail app code.
            myExt.start();
        });
    }
}

Olk.ItemProps.Ext.init();