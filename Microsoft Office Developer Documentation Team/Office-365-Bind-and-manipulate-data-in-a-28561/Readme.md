# Office 365: Bind and manipulate data in a SharePoint Access app
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* apps for Office
* Access 2013 SP1
## Topics
* Access Services
## IsPublished
* True
## ModifiedDate
* 2014-07-18 02:25:55
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><strong><span id="nsrTitle" style="font-size:large">Office 365: Bind and manipulate data in a SharePoint Access app</span></strong></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><strong>Last modified:&nbsp;</strong>July 18, 2014</p>
<p><strong>In this article</strong>&nbsp;<br>
<a href="#sectionSection0">Prerequisites</a>&nbsp;<br>
<a href="#sectionSection1">Create an app for Access based on the Visual Studio template</a>&nbsp;<br>
<a href="#sectionSection2">Examine the Home.js file&nbsp;</a><br>
<a href="#sectionSection3">Add global variables to contain data that is used across all functions&nbsp;</a><br>
<a href="#sectionSection4">Create or detect the binding to the target data</a>&nbsp;<br>
<a href="#sectionSection5">Using the Bing map control</a>&nbsp;<br>
<a href="#sectionSection6">Additional resources</a>&nbsp;</p>
<p>This article contains preliminary information, and will be updated in MSDN when the documentation is finalized. A link will be added here when that documentation is available.</p>
<p>This article is based on the apps for Office and SharePoint sample&nbsp;BingRoute, which uses the&nbsp;<a href="http://msdn.microsoft.com/en-us/library/ff701713.aspx" target="_blank">Bing map REST service</a>&nbsp;to calculate the distance between two geographical
 points. You can find the sample on the&nbsp;<a href="http://code.msdn.microsoft.com/officeapps/site/search?f[0].Type=Technology&f[0].Value=apps for Office" target="_blank">Apps for Office and SharePoint samples</a>&nbsp;page, or at the download location:<a href="http://code.msdn.microsoft.com/Office-365-Bind-and-4876274e/edit?newSession=True" target="_blank">,
 http://code.msdn.microsoft.com/Office-365-Bind-and-4876274e</a>. The idea for this app came from an original Access app called Mile Tracker that was included with SharePoint Online. This app enabled a user to record miles driven for billing or tax reasons.
 In the app the user would record odometer readings for the start and end of a trip, and store that information and a calculated distance traveled in an associated database. In practice the app was not very usable - it was unreasonable to expect the user to
 remember those specific numbers after the trip. This sample simplifies the process by letting the user enter addresses or landmarks, and uses Bing to calculate a route and a driving distance.</p>
<p>This sample only uses the Bing service to calculate route - it would be an interesting exercise to add code to display the calculated route on a map. Most of the code used to communicate with the Bing control comes with little modification from the&nbsp;<a href="http://msdn.microsoft.com/en-us/library/ff701713.aspx" target="_blank">Bing
 map REST service</a>&nbsp;documentation page, and you can look there for more information about the service.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>This article does not go into the mechanics of creating an app for Office, or using it in a SharePoint Access app. The topic&nbsp;<a href="45f487f1-b34c-41cf-aab2-fcbe13a605b0" target="_blank">How to: Create a content app for Access</a>&nbsp;has detailed
 information on this and this article assumes that you understand that information and are now ready to add code to communicate with an Access database.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Prerequisites</h2>
<div id="sectionSection0">
<p>To write an app for Office targeting Access you need:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2013</p>
</li><li>
<p><a href="http://aka.ms/officedevtoolsforvs2013" target="_blank">Microsoft Office Developer Tools for Visual Studio 2013 (March 2014 update)</a></p>
</li><li>
<p>A SharePoint Online site (included in many Office 365 subscriptions). This site must have an App Catalog. For more information, see&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/fp123530.aspx" target="_blank">How to: Set up an App Catalog
 on SharePoint</a>. Also see&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/fp179924(v=office.15).aspx" target="_blank">Sign up for an Office 365 Developer Site</a>&nbsp;for more information about getting a test site for SharePoint Online.</p>
</li><li>
<p>A valid user ID for the Bing Maps REST Services, see:&nbsp;<a href="http://msdn.microsoft.com/en-us/library/ff428642.aspx" target="_blank">Getting a Bing Maps Key</a></p>
</li></ul>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>This app for Office requires SharePoint Online. The Microsoft Access 2013 desktop client doesn't support apps for Office.</p>
<p>Apps for Office targeting Access are supported in version 1.1 of Office.js.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Create an app for Access based on the Visual Studio template</h2>
<div id="sectionSection1">
<p>When you install the&nbsp;<a href="http://aka.ms/officedevtoolsforvs2013" target="_blank">Microsoft Office Developer Tools for Visual Studio 2013 (March 2014 update)</a>&nbsp;and Microsoft Visual Studio 2013, you can use Visual Studio to create an app for
 Office targeting Access. This procedure is described in&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/dn605890(v=office.15).aspx">How to: Create a content app for Access</a>. Follow the steps in that topic to create and run an Access content
 app based on the&nbsp;Basic&nbsp;template, and then return to this topic to learn what steps to take to make your app talk to an Access database. The following steps add features to the basic app template to bind to data in the Access app, and communicate
 with the Bing REST Service.</p>
</div>
<h2>Examine the Home.js file</h2>
<div id="sectionSection2">
<p>to create the app you will modify the Home.js file to add functionality. Open the project in Microsoft Visual Studio 2013, and examine the file as it comes in the basic template project.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>/// &lt;reference path=&quot;../App.js&quot; /&gt;

(function () {
    &quot;use strict&quot;;

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            $('#get-data-from-selection').click(getDataFromSelection);
        });
    };

    // Reads data from current document selection and displays a notification
    function getDataFromSelection() {
        if (Office.context.document.getSelectedDataAsync) {
            Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
                function (result) {
                    if (result.status === Office.AsyncResultStatus.Succeeded) {
                        app.showNotification('The selected text is:', '&quot;' &#43; result.value &#43; '&quot;');
                    } else {
                        app.showNotification('Error:', result.error.message);
                    }
                }
            );
        } else {
            app.showNotification('Error:', 'Reading selection data is not supported by this host application.');
        }
    }
})();</pre>
</td>
</tr>
</tbody>
</table>
</div>
<strong>
<div class="caption">What do the functions in Home.js do?</div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Function name</p>
</th>
<th>
<p>Function behavior</p>
</th>
</tr>
<tr>
<td>
<p>Office.initialize</p>
</td>
<td>
<p>The initialize function is run on initialization, on start, on page refresh and on insertion.</p>
</td>
</tr>
<tr>
<td>
<p>getDataFromSelection</p>
</td>
<td>
<p>This function is called when the button with the id &quot;get-data-from-selection&quot; is clicked. It gets data from the selection in the SharePoint OnlineAccess app that hosts the app for Office, and displays that data in the app for Office. This function is not
 used in the bingroute app, and will be replaced with new functions as you progress through this article.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>In general terms, you will add code to do the following:</p>
<ul>
<li>
<p>Connect (bind) to the data in the Access app that will host the app for Office.</p>
</li><li>
<p>Listen for events from the binding when the data in the Access app changes, and react to those changes.</p>
</li><li>
<p>Write data back to the users form when the user selects the appropriate button.</p>
</li></ul>
<p>&nbsp;</p>
</div>
<h2>Add global variables to contain data that is used across all functions</h2>
<div id="sectionSection3">
<p>At the very top if the Home.js file add the following global variables. These values pass information between functions and between the application code and the UI. All the variables are pretty self-explanatory, other than the&nbsp;BindingName. This is a
 value that is used to identify the specific binding when it is saved as part of the Access app.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>//Variable initialization
var BindingName = &quot;RouteBinding&quot;;
var ToAddress = &quot;&quot;;
var FromAddress = &quot;&quot;;
var map = null;
var PostField = &quot;&quot;;
var MilesTraveled = 0;
</pre>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Create or detect the binding to the target data</h2>
<div id="sectionSection4">
<p>Apps for Office and SharePoint that target Access are used to manipulate and visualize data in an Access database hosted in SharePoint Online. to connect the app with the data in the Access database, the data must be bound to variables in the app. These
 are set in the&nbsp;Office.initialize&nbsp;function that is called in response to the&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/fp161139(v=office.1501401).aspx" target="_blank">Office.initialize</a>&nbsp;event. A stub for this function is
 provided in the template that you examined. You will want to modify the stub to include the following code. This code checks for an existing binding, and if a binding with the correct name exists the app registers handlers for binding messages and updates
 the data. If the binding does not exist, the code creates a new binding.</p>
<p>A binding is simply a named pointer to data in the host application. Once created, a binding is saved as part of the host application (form), and is available for all users. In this way end-users do not need to go through the binding process every time.
 Since the binding may already have been created in a previous session, you must check for it first.</p>
<p>Access supports table binding, which lets you read and write data to the table or query in the database. All you need to do is write data to the binding and the host maps the information to the table.The binding also provides events when the data or the
 selection in the table changes.</p>
<p>You can use three styles of binding:</p>
<ul>
<li>
<p>Bind by name if you know the name of the table</p>
</li><li>
<p>Bind by selection</p>
</li><li>
<p>Bind by prompt</p>
</li></ul>
<p>This sample uses the third option, bind by prompt. This prompts to the user with a dialog which contains some example data, and lists available data found in the database. The user browses the available data fields and selects the data that most closely
 resembles the example data. When all appropriate fields have been filled in the dialog box is closed and the binding is created. Even if a binding is already present, it is a good idea to add UI that lets the user rebind the data to allow different values
 to be used.</p>
<p>In either case (creating or re-using a binding) handlers must be registered.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>Office.initialize = function (reason) {
    // This async method will check to see if a binding already exists. 
    Office.context.document.bindings.getByIdAsync(BindingName, function (callback) {
        //check the callback.status field to see if the call succeeded. If the callback succeeded we know a binding already exists
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //We have the binding so call functions to register handlers and update the map
            RegisterHandlers(callback.value);
            RecalculateDistance();
        }
        else {
            //We don't have a binding yet so call functions to create a binding
            BindToData();
        }
    });
    //Register button events
    $(&quot;#btnBind&quot;).click(BindToData);
    $(&quot;#setdistance&quot;).click(SetDistance);
};
</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>At the end of the function two button click events are registered to connect the user interface to functions that will be created in the next section. The UI is very simple and will not be described in this article. To save typing, use the UI code from the
 sample application, as well as a bitmap of the Bing logo that is used in the UI.</p>
<p>There are several functions that are mentioned but not defined in the initialize function:</p>
<ul>
<li>
<p>BindToData</p>
</li><li>
<p>RegisterHandlers</p>
</li><li>
<p>RecalculateDistance</p>
</li><li>
<p>SetDistance</p>
</li></ul>
<p>BindToData&nbsp;is called if the binding does not exist, or if the user wants to change the binding. This function manages the process of binding by prompt. It creates the example data mentioned before, and calls<a href="http://msdn.microsoft.com/en-us/library/office/fp142150(v=office.1501401).aspx" target="_blank">Office.context.document.bindings.addFromPromptAsync</a>&nbsp;and
 creates the new binding when the user has entered the appropriate values. This function also calls the function&nbsp;RegisterHandlers.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>//Show the binding prompt with sample text and bind to the data the user selects
function BindToData() {
    //TableData to hold the sample data
    var sampleDataTable = new Office.TableData();
    //Sample headers
    sampleDataTable.headers = [[&quot;From Address&quot;, &quot;To Address&quot;, &quot;Distance Field&quot;]];
    //Sample data rows
    sampleDataTable.rows = [[&quot;White House&quot;, &quot;Seattle&quot;, &quot;2716.2&quot;], [&quot;Houston&quot;,&quot;Dallas&quot;,&quot;293.3&quot;], [&quot;400 Broad St., Seattle, WA&quot;, &quot;1 Microsoft Way, Redmond WA&quot;, &quot;13.3&quot;]];

    //Show the binding promp with the sample data and create a binding with id:BindingName
    Office.context.document.bindings.addFromPromptAsync(Office.BindingType.Table, {
        id: BindingName,
        sampleData: sampleDataTable
    }, function (bindingCallback) {
        //Check if the callback succeeded
        if (bindingCallback.status == Office.AsyncResultStatus.Succeeded) {
            //Register handlers for events
            RegisterHandlers(bindingCallback.value)
        }
    });
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>The&nbsp;RegisterHandlers&nbsp;function does two things - first it adds handlers for two messages that are generated by the binding:&nbsp;BindingSelectionChanged, and&nbsp;BindingDataChanged. In both cases the handler will callRecalculateDistance. The second
 thing the function does is to get the appropriate table header value to post data to, and stores that in the string global variable&nbsp;PostField. This value can then be used to update the distance value every time that the route changes.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>//Register handlers for events
function RegisterHandlers(binding) {
    //Map Binding Selection Changed event to the HandleRecordChange function
    binding.addHandlerAsync(Office.EventType.BindingSelectionChanged, RecalculateDistance);
    //Map Binding Data Changed event to the HandleRecordChange function
    binding.addHandlerAsync(Office.EventType.BindingDataChanged, RecalculateDistance);

    //Get the name of the field to set data into. This is the third field in the binding
    Office.select(&quot;bindings#&quot; &#43; BindingName).getDataAsync({
        coercionType: Office.CoercionType.Table,
        //Get the data for the current row
        rows: &quot;thisRow&quot;
    }, function (callback) {
        //Check if the call succeeded
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //Store the name(header) of the field
            PostField = callback.value.headers[0][2];
        }
    });
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>&nbsp;</p>
</div>
<h2>Using the Bing map control</h2>
<div id="sectionSection5">
<p>RecalculateDistance&nbsp;is the heart of the sample. It requests and stores the two addresses that the Bing service needs to calculate the route and the route distance. The actual call to the Bing service is delegated to the&nbsp;CallBingMapsService&nbsp;function
 at the bottom of this function.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>//Recalulate distance with current row's data. To be called on app initial load, selectionchanged or datachanged
function RecalculateDistance() {
    //Get the To and From address from the current row
    Office.select(&quot;bindings#&quot; &#43; BindingName).getDataAsync({
        coercionType: Office.CoercionType.Table,
        //Specify to get the data from the currently selected data row
        rows: &quot;thisRow&quot;
    }, function (callback) {
        // Check to see if the function was successful
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //Store the from address
            FromAddress = callback.value.rows[0][0];
            //Store the to address
            ToAddress = callback.value.rows[0][1];
            //Update the UI to reflect the new data
            $(&quot;#fromaddress&quot;).text(FromAddress);
            $(&quot;#toaddress&quot;).text(ToAddress);
            //Call the Bing service
            CallBingMapsService();
        }
    });
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>The&nbsp;CallBingMapsService&nbsp;and the&nbsp;MakeRouteRequest&nbsp;functions encapsulate the specific code needed to talk to the Bing service. Note the 'X's in the credentials parameter. This is where you must insert your own credentials, as described
 in the Prerequisites section earlier in this article.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function CallBingMapsService() {
    //Create new Microsoft Maps object
    map = new Microsoft.Maps.Map(document.getElementById(&quot;mapDiv&quot;), { credentials: &quot;XXXXXXXXXXXXXX-XXXXXXXXXXXXX_XXXXXXXXXX_XXXXXXX&quot;, mapTypeId: Microsoft.Maps.MapTypeId.r });
    map.getCredentials(MakeRouteRequest);
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>Send the REST request to the Bing service by using the helper function&nbsp;CallRestService.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function MakeRouteRequest(credentials) {
    //Send request to bing. Specify the to and from Address and the callback function
    var routeRequest = &quot;https://dev.virtualearth.net/REST/v1/Routes?wp.0=&quot; &#43; FromAddress &#43; &quot;&amp;wp.1=&quot; &#43; ToAddress &#43; &quot;&amp;routePathOutput=Points&amp;output=json&amp;jsonp=RouteCallback&amp;key=&quot; &#43; credentials;

    CallRestService(routeRequest);
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>This function is called by the Bing service and handles the return values from the service request. Note that the travel distance is returned in kilometers and is converted to miles using the constant 6.2137119.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>//To called by Bing Maps Response
function RouteCallback(result) {
    //Check to see if all the data we need exists
    if (result &amp;&amp;
          result.resourceSets &amp;&amp;
          result.resourceSets.length &gt; 0 &amp;&amp;
          result.resourceSets[0].resources &amp;&amp;
          result.resourceSets[0].resources.length &gt; 0) {

        //Store the distance between addresses
        MilesTraveled = Math.round(result.resourceSets[0].resources[0].travelDistance * 6.2137119) / 10;
        //Update the Apps for Office UI
        $(&quot;#distance&quot;).text(MilesTraveled);
    }
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>And finally the distance value is posted to the appropriate location in the table in the Access app usingPostField.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function SetDistance(eventArgs) {
    if (MilesTraveled !== 0) {
        //Set data in the host
        Office.select(&quot;bindings#&quot; &#43; BindingName).setDataAsync([[MilesTraveled]], {
            //On the current row
            rows: &quot;thisRow&quot;,
            //In the PostField
            columns: [PostField]
        }, function (callback) {
            //Set Data Callback
        });
    }
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>A convenient helper function for making REST calls.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>//Function to call rest service by injecting script tag
function CallRestService(request) {
    var script = document.createElement(&quot;script&quot;);
    script.setAttribute(&quot;type&quot;, &quot;text/javascript&quot;);
    script.setAttribute(&quot;src&quot;, request);
    document.body.appendChild(script);
}</pre>
</td>
</tr>
</tbody>
</table>
</div>
<p>All apps for Office work in similar ways, and you can find additional information in the links at the end of this section.</p>
</div>
<h2>Additional resources</h2>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/ff701713.aspx" target="_blank">Bing Maps REST Services</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/fp161507.aspx" target="_blank">Apps for Office and SharePoint</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps" target="_blank">Apps for Office and SharePoint samples</a></p>
</li></ul>
</div>
</div>
</div>
