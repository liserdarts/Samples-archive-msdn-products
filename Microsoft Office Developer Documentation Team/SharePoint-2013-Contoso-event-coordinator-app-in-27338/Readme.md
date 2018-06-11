# SharePoint 2013: Contoso event coordinator app in Azure web role using calendar
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* apps for SharePoint
* Windows Azure Cloud Services
## Topics
* apps for SharePoint
## IsPublished
* True
## ModifiedDate
* 2014-02-27 06:23:56
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Contoso event coordinator app in Azure web role using calendar</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This sample demonstrates how to create a provider-hosted app for SharePoint that runs as a Windows Azure web role. The app reads and writes to a Windows Azure SQL Database, uploads and manages binary files in Windows Azure Blob storage, and uses the Bing
 Maps API to provide map views for events.</p>
</div>
<div>
<p><strong>Last modified: </strong>February 23, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#sectionSection0">The app in action</a> <br>
<a href="#sectionSection1">Prerequisites</a> <br>
<a href="#sectionSection2">Key components of the sample</a> <br>
<a href="#sectionSection3">Configure the sample</a> <br>
<a href="#sectionSection4">Troubleshooting</a> <br>
<a href="#sectionSection5">Features in this sample</a> <br>
<a href="#sectionSection6">Change log</a> <br>
<a href="#sectionSection7">Additional resources</a></p>
<p>Wouldn't it be great to build a line of business app that integrates with SharePoint features such as calendars, and scheduling? The Contoso event coordinator app shows you just how to do that!</p>
<p>Contoso has an event coordination team that plans events such as company conferences, meetings, and morale events. The team often works remotely and they collaborate on event planning and management by using a SharePoint site. They have a calendar for tracking
 events, but they also want to track data that is relational for each event such as the list of presenters, attendees, agenda, and other items. They also want to store large binary data items such as posters, videos, PowerPoint decks and other event related
 collateral.</p>
<p>This sample is the app that Contoso built to meet the team's requirements for event planning and data handling. It is a provider-hosted app for SharePoint that runs in Windows Azure. It runs as a web role so that it can store and retrieve Blob items, such
 as videos, in Windows Azure Blob storage. It also uses a Windows Azure SQL Database to store the relational data. It integrates with the SharePoint site by adding a ribbon menu for creating new events, and it keeps the SharePoint calendar in sync with the
 database.</p>
</div>
<h1>The app in action</h1>
<div id="sectionSection0">
<p>To use the app to create an event, choose the <strong><span class="ui">Coordinate Event</span></strong> button (Figure 1) on the ribbon in a SharePoint calendar view.</p>
<strong>
<div class="caption">Figure 1. Calendar view showing the Coordinate Event ribbon button</div>
</strong><br>
<img src="/site/view/file/109535/1/image.png" alt="">
<p>The app opens, and you can use the <strong><span class="ui">Add Event</span></strong> button (Figure 2) to create an app.</p>
<strong>
<div class="caption">Figure 2. The Add Event button</div>
</strong><br>
<img src="/site/view/file/109536/1/image.png" alt="">
<p>Figure 3 shows the properties that you can specify for the event.</p>
<strong>
<div class="caption">Figure 3. The form used to create a new event</div>
</strong><br>
<img src="/site/view/file/109537/1/image.png" alt="">
<p>After an event is created, you can manage event properties and related items such as agendas, attendees, and coordinators using the toolbar shown in Figure 4.</p>
<strong>
<div class="caption">Figure 4. Events list with controls used to manage event details and related items</div>
</strong><br>
<img src="/site/view/file/109538/1/image.png" alt="">
<p>Figure 5 shows the <strong><span class="ui">Add Agenda Item</span></strong> button that you use to add an agenda item to the event.</p>
<strong>
<div class="caption">Figure 5. The Add Agenda Item button</div>
</strong><br>
<img src="/site/view/file/109539/1/image.png" alt="">
<p>Figure 6 shows the form you use to create an agenda item.</p>
<strong>
<div class="caption">Figure 6. The form to create new agenda item</div>
</strong><br>
<img src="/site/view/file/109540/1/image.png" alt="">
<p>You can drag and drop agenda items to reorder them, as shown in Figure 7.</p>
<strong>
<div class="caption">Figure 7. Reordering agenda items using drag and drop</div>
</strong><br>
<img src="/site/view/file/109541/1/image.png" alt="">
<p>You can drag and drop people to delegate coordinator duties, as shown in Figure 8.</p>
<strong>
<div class="caption">Figure 8. Adding and removing event coordinators using drag and drop</div>
</strong><br>
<img src="/site/view/file/109542/1/image.png" alt="">
<p>Events are synchronized in the site calendar, as shown in Figure 9.</p>
<strong>
<div class="caption">Figure 9. Events created from the app appear in the site calendar</div>
</strong><br>
<img src="/site/view/file/109543/1/image.png" alt=""></div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A Windows Azure subscription. You can get a free trial here: <a href="http://www.windowsazure.com/en-us/pricing/free-trial/" target="_blank">
Windows Azure Free Trial</a>.</p>
</li><li>
<p>A SharePoint environment configured to host apps. You can get a free trial here:
<a href="http://www.microsoft.com/en-cb/office365/free-office365-trial.aspx" target="_blank">
Try Office 365 for business</a>.</p>
</li><li>
<p>A team site created in your SharePoint environment</p>
</li><li>
<p>Visual Studio 2013 (Professional or Ultimate edition)</p>
</li><li>
<p>SQL Server Management Studio</p>
</li><li>
<p>The <a href="http://www.windowsazure.com/en-us/downloads/" target="_blank">Windows Azure SDK</a> for Visual Studio 2013.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample contains the following key components:</p>
<ul>
<li>
<p><strong>ContosoEvents</strong> app project, which contains the manifest used to deploy the app to a SharePoint site</p>
</li><li>
<p><strong>ContosoEventsWeb</strong> MVC project that runs as the provider</p>
</li><li>
<p><strong>ContosoEventsWeb.Azure</strong> cloud service project that deploys the provider to run as a Windows Azure web role</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure, build, and run the sample app.</p>
<h3>To create and configure a SQL Azure database</h3>
<div>
<ol>
<li>
<p>Log on to the <a href="https://manage.windowsazure.com" target="_blank">Windows Azure Management Portal</a> (<span>https://manage.windowsazure.com/</span>), and then choose
<strong><span class="ui">SQL DATABASES</span></strong> in the navigation pane.</p>
</li><li>
<p>On the toolbar at the bottom of the portal page, choose <strong><span class="ui">NEW</span></strong>&gt;<strong><span class="ui">QUICK CREATE</span></strong>.</p>
</li><li>
<p>Enter the following values.</p>
<strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Setting</p>
</th>
<th>
<p>Value</p>
</th>
</tr>
<tr>
<td>
<p>Database name</p>
</td>
<td>
<p>EventDB</p>
</td>
</tr>
<tr>
<td>
<p>Server</p>
</td>
<td>
<p>New SQL database server</p>
</td>
</tr>
<tr>
<td>
<p>Login name</p>
</td>
<td>
<p>EventDBUser</p>
</td>
</tr>
<tr>
<td>
<p>Password</p>
</td>
<td>
<p>P@ssw0rd</p>
</td>
</tr>
<tr>
<td>
<p>REGION</p>
</td>
<td>
<p>Select the region nearest to your location.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>Choose <strong><span class="ui">CREATE SQL DATABASE</span></strong>. The server and database are created, which may take several minutes.</p>
</li><li>
<p>Open the database from the list of databases, and then choose <strong><span class="ui">Show connection strings</span></strong> in the
<strong><span class="ui">quick glance</span></strong> section.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>If the <strong><span class="ui">Quick Start</span></strong> page opens, choose
<strong><span class="ui">Skip Quick Start the next time I visit</span></strong> and then reopen the database.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>In the <strong><span class="ui">Connection Strings</span></strong> dialog box, copy the contents of the ADO.NET box.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Important</strong> </th>
</tr>
<tr>
<td>
<p>Don't choose the <strong><span class="ui">X</span></strong> button at the bottom of the dialog box. Doing so would disallow the connection string in the firewall rules.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>Paste the connection string in a text editor, such as Notepad. Replace the <span>
{your_password_here}</span> placeholder text with <strong>P@ssw0rd</strong>. Remove the braces.</p>
</li><li>
<p>Choose the close icon in the upper-right corner of the dialog box.</p>
</li><li>
<p>In the <strong><span class="ui">quick glance</span></strong> section, copy the full server name to your text editor. The name uses the format
<span>server.database.windows.net</span>.</p>
</li><li>
<p>In the <strong><span class="ui">quick glance</span></strong> section, choose
<strong><span class="ui">Manage allowed IP addresses</span></strong>.</p>
</li><li>
<p>Enter the following values.</p>
<strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Setting</p>
</th>
<th>
<p>Value</p>
</th>
</tr>
<tr>
<td>
<p>Rule name</p>
</td>
<td>
<p>EventDB</p>
</td>
</tr>
<tr>
<td>
<p>Start IP address</p>
</td>
<td>
<p>0.0.0.0</p>
</td>
</tr>
<tr>
<td>
<p>End IP address</p>
</td>
<td>
<p>255.255.255.255</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>On the toolbar at the bottom of the portal page, choose <strong><span class="ui">SAVE</span></strong>.</p>
<p>Keep the management portal open. You'll use it later.</p>
</li></ol>
</div>
<h3>To run the database script</h3>
<div>
<ol>
<li>
<p>In SQL Server Management Studio, in the <strong><span class="ui">Connect to Server</span></strong> dialog box, enter the server name that you copied in the previous section.</p>
</li><li>
<p>In the <strong><span class="ui">Authentication</span></strong> menu, choose <strong>
<span class="ui">SQL Server Authentication</span></strong>, and then enter <span>
EventDBUser</span> as the log on name and <span>P@ssw0rd</span> as the password.</p>
</li><li>
<p>Choose the <strong><span class="ui">Remember password</span></strong> check box, and then choose the
<strong><span class="ui">Connect</span></strong> button.</p>
</li><li>
<p>Expand the <strong><span class="ui">Databases</span></strong> folder and choose
<strong><span class="ui">EventDB</span></strong>.</p>
</li><li>
<p>On the menu bar, choose <strong><span class="ui">File</span></strong>&gt;<strong><span class="ui">Open</span></strong>&gt;<strong><span class="ui">File</span></strong>.</p>
</li><li>
<p>Browse to the <strong><span class="ui">SQL Scripts</span></strong> folder included in the sample files.</p>
</li><li>
<p>Choose the <strong><span class="ui">Create EventDB Objects.sql</span></strong> script file, and then choose the
<strong><span class="ui">Open</span></strong> button.</p>
<p>The script creates several tables that are related through a series of primary keys and foreign key constraints. It also creates all the stored procedures that insert, update, delete, and run queries against the database.</p>
</li><li>
<p>On the menu bar, choose <strong><span class="ui">Query</span></strong>, <strong>
<span class="ui">Execute</span></strong>.</p>
</li><li>
<p>After the script finishes without any errors, close SQL Server Management Studio.</p>
</li></ol>
</div>
<h3>To create a Windows Azure storage account</h3>
<div>
<ol>
<li>
<p>In the Windows Azure management portal, choose <strong><span class="ui">STORAGE</span></strong> in the navigation pane.</p>
</li><li>
<p>On the toolbar at the bottom of the portal page, choose <strong><span class="ui">NEW</span></strong>, and then choose
<strong><span class="ui">QUICK CREATE</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">URL</span></strong> box, enter <span>eventstorage &lt;name&gt;</span>, where
<span>&lt;name&gt;</span> is a name that other users are unlikely to use. The URL must be unique and it must use all lowercase characters.</p>
</li><li>
<p>In the <strong><span class="ui">LOCATION/AFFINITY GROUP</span></strong> box, select the location nearest to you.</p>
</li><li>
<p>On the toolbar at the bottom of the portal page, choose <strong><span class="ui">CREATE STORAGE ACCOUNT</span></strong>. It may take several minutes for the storage account to be created.</p>
</li><li>
<p>Open the new storage account from the list of storage accounts.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>If the <strong><span class="ui">Quick Start</span></strong> page opens, choose the
<strong><span class="ui">DASHBOARD</span></strong> tab at the top of the portal page.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>From the <strong><span class="ui">services</span></strong> section, copy the endpoint for the
<strong><span class="ui">Blobs</span></strong> service to your text editor.</p>
</li><li>
<p>On the toolbar at the bottom of the portal page, choose <strong><span class="ui">MANAGE ACCESS KEYS</span></strong>.</p>
</li><li>
<p>Copy the storage account name to your text editor.</p>
</li><li>
<p>Copy the primary access key to your text editor.</p>
</li></ol>
</div>
<h3>To obtain a key for the Bing Maps service</h3>
<div>
<ol>
<li>
<p>In your web browser, browse to the <a href="http://www.microsoft.com/maps/create-a-bing-maps-key.aspx" target="_blank">
Create a Bing Maps Key</a> page (<span>http://www.microsoft.com/maps/create-a-bing-maps-key.aspx</span>) on the Bing Maps Platform site.</p>
</li><li>
<p>Choose <strong><span class="ui">Get the Trial Key</span></strong> near the bottom of the page.</p>
</li><li>
<p>Choose the <strong><span class="ui">Sign In</span></strong> button, and enter your Microsoft Account credentials.</p>
</li><li>
<p>In the navigation pane, in the <strong><span class="ui">My Account</span></strong> section, choose
<strong><span class="ui">Create or view keys</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Application name</span></strong> box, enter a name for the application (for example,
<span>Contoso Events App</span>).</p>
</li><li>
<p>In the <strong><span class="ui">Key type</span></strong> menu, choose <strong>
<span class="ui">Trial</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Application type</span></strong> menu, choose
<strong><span class="ui">Private website</span></strong>.</p>
</li><li>
<p>Enter the characters to validate your request, and then choose the <strong><span class="ui">Submit</span></strong> button. After the key is created, the application name and key details are shown at the bottom of the page.</p>
</li><li>
<p>Copy the key to your text editor.</p>
</li></ol>
</div>
<h3>To configure the Visual Studio solution</h3>
<div>
<ol>
<li>
<p>Run Visual Studio as administrator, and open the <strong>ContosoEvents.sln</strong> file.</p>
</li><li>
<p>In <strong><span class="ui">Solution Explorer</span></strong>, choose the <strong>
<span class="ui">ContosoEvents</span></strong> project.</p>
</li><li>
<p>In the <strong><span class="ui">Properties</span></strong> window, in the <strong>
<span class="ui">Site URL</span></strong> property, enter the URL of the SharePoint site that you want to deploy the app to. Be sure to include the closing forward slash.</p>
<p>If prompted, log on with your SharePoint or Office 365 credentials.</p>
</li><li>
<p>In <strong><span class="ui">Solution Explorer</span></strong>, expand the <strong>
<span class="ui">ContosoEventsWeb</span></strong> project, and then open the Web.config file.</p>
</li><li>
<p>In the <strong><span class="keyword">connectionStrings</span></strong> section, replace the placeholder values for connection string variables, as follows:</p>
<ul>
<li>
<p>For <strong><span class="keyword">EventDB</span></strong>, replace the <span>
{Your SQL Connection}</span> placeholder text with the ADO.NET connection string that you copied in the
<strong>To create and configure a SQL Azure database</strong> procedure.</p>
</li><li>
<p>For <strong><span class="keyword">EventAzureStroage</span></strong>, replace the
<span>{Your Azure Blob Account NAME}</span> placeholder text with the storage account name, and the
<span>{Your Azure Blob Account KEY}</span> placeholder text with the primary access key. You copied these values in the
<strong>To create a Windows Azure storage account</strong> procedure.</p>
</li><li>
<p>For <strong><span class="keyword">EventAzureURLBase</span></strong>, replace the
<span>{Your Azure Blob URL Base}</span> placeholder text with the Blobs service endpoint that you copied in the
<strong>To create a Windows Azure storage account</strong> procedure.</p>
</li></ul>
<p>Remove the braces when you replace the placeholder values.</p>
</li><li>
<p>In the <strong><span class="ui">ContosoEventsWeb</span></strong> project, expand the
<strong><span class="ui">Views</span></strong> folder, expand the <strong><span class="ui">Home</span></strong> folder, and then open the eVents.cshtml file.</p>
</li><li>
<p>For the <strong><span class="keyword">BINGCreds</span></strong> variable, replace the
<span>{Your Bing Maps API Key}</span> placeholder text with your Bing Maps key.</p>
</li><li>
<p>On the menu bar, choose <strong><span class="ui">File</span></strong>&gt;<strong><span class="ui">Save All</span></strong>.</p>
</li><li>
<p>Choose the <strong><span class="ui">F6</span></strong> key to build the solution.</p>
</li></ol>
</div>
<h3>To run the app in a test configuration</h3>
<div>
<ol>
<li>
<p>Choose the <strong><span class="ui">F5</span></strong> key to start the Windows Azure emulator and to deploy the app to you SharePoint site.</p>
<p>If prompted, log on with your SharePoint or Office 365 credentials.</p>
</li><li>
<p>In the <strong><span class="ui">Do you trust</span></strong> page, choose the
<strong><span class="ui">Trust It</span></strong> button to grant the app the required permissions.</p>
</li><li>
<p>In the <strong><span class="ui">Setup &amp; Configuration</span></strong> page, review the information and take any steps that are needed to set up the app. For example, you might have to choose
<strong><span class="ui">CREATE</span></strong> to create a calendar in the host site. This page is shown the first time that you run the app.</p>
</li><li>
<p>When all <strong><span class="ui">Status</span></strong> values read <strong>
<span class="ui">Success!</span></strong>, choose <strong><span class="ui">Back to Site</span></strong> to try the app. See
<a href="#AppInAction">The app in action</a> for an overview of how the app works.</p>
</li></ol>
</div>
<p>To deploy the app in a real, production environment, you'll have to complete many additional tasks, including the following:</p>
<ul>
<li>
<p>Register domain names</p>
</li><li>
<p>Purchase an SSL certificate and install it on your domain name</p>
</li><li>
<p>Create a Windows Azure cloud service, and use your domain name to point to the service</p>
</li><li>
<p>Add your SSL certificate to the Windows Azure cloud service</p>
</li><li>
<p>Configure the Windows Azure project in Visual Studio to use the SSL certificate</p>
</li></ul>
<p>You can find information about how to complete these tasks on the <a href="http://www.msdn.microsoft.com" target="_blank">
Microsoft Developer Network</a> (<span>http://www.msdn.microsoft.com</span>).</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure you have completed all the steps in the <a href="#ConfigureTheSample">
Configure this sample</a> section.</p>
</div>
<h1>Features in this sample</h1>
<div id="sectionSection5">
<p>This provider-hosted app is implemented as a Model-View-Controller (MVC) website hosted as a Windows Azure web role. Server-side logic and data access logic use Visual C#, and the modern, responsive user interface uses HTML 5, CSS3, JavaScript, and jQuery.
 Part of the event-planning process involves storing binary files in Windows Azure Blob storage, so this requirement means that the provider-hosted model running as a web role is a good fit.</p>
<p>The sample demonstrates how to use:</p>
<ul>
<li>
<p>HTML and Cascading Style Sheets (CSS) to define the basic UI for an app.</p>
</li><li>
<p>jQuery to add fluidity and a professional look-and-feel to the UI.</p>
</li><li>
<p>SharePoint .NET client object model to retrieve security-related information from the SharePoint host site, such as the current user's login name, the site groups the user belongs to, and membership information of other site groups.</p>
</li><li>
<p>SharePoint .NET client object model to create a calendar in the SharePoint host site.</p>
</li><li>
<p>SharePoint client-side object model to read and write data to a calendar in the SharePoint host site.</p>
</li><li>
<p>Model-View-Controller (MVC) web application (running as a Windows Azure web role) to read and write to SQL Azure databases and Windows Azure Blob storage.</p>
</li></ul>
</div>
<h1>Change log</h1>
<div id="sectionSection6"><strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>March 2014</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Additional resources</h1>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/office/dn594489" target="_blank">Showcase: Contoso event coordinator</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179933.aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163980.aspx" target="_blank">Get started developing apps for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
