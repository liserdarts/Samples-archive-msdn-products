# Working with Web Services in SharePoint 2013 Workflows using Visual Studio 2012
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* Workflows
## IsPublished
* True
## ModifiedDate
* 2014-01-20 03:01:24
## Description

<div class="content">
<div>
<div class="topic">
<div class="majorTitle"></div>
<h1 class="title">Working with Web Services in SharePoint 2013 Workflows using Visual Studio 2012</h1>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p>Demonstrates how to use web services in SharePoint 2013 workflows using Visual Studio 2012.</p>
</div>
<div class="introduction">
<p><strong>Last modified: </strong>September 17, 2015</p>
<p><em><strong>Applies to: </strong>SharePoint Foundation 2013&nbsp;| SharePoint Online&nbsp;| SharePoint Server 2013</em></p>
<p><strong>In this article</strong><br>
<a href="#sec1">Scenarios for using web services in SharePoint 2013 workflows</a><br>
<a href="#sec2">Leveraging web services in workflows</a><br>
<a href="#sec3">Creating web services for SharePoint 2013 workflows</a><br>
<a href="#sec4">Walkthrough: Create a workflow with Visual Studio 2012</a><br>
<a href="#sec5">Conclusion</a><br>
<a href="#bk_addresources">Additional resources</a><br>
</p>
<p><span class="label">Provided by: </span><a href="http://social.msdn.microsoft.com/profile/andrew%20connell%20[mvp]/" target="_blank">Andrew Connell</a>,
<a href="http://www.andrewconnell.com" target="_blank">AndrewConnell.com</a></p>
<p><br>
</p>
<div class="alert">
<table>
<tbody>
<tr>
<th align="left"><img id="alert_note" alt="Note" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.alert_note(v=office.15).gif" title="Note"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>This article is accompanied by an end-to-end code sample that you can use to follow the article, or as a starter for your own SharePoint workflow projects. You can find the downloadable code in the MSDN Code Gallery, here:
<a href="http://code.msdn.microsoft.com/Working-with-Web-in-46148199" target="_blank">
Working with Web Services in SharePoint 2013 Workflows using Visual Studio 2012</a>.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p><br>
</p>
<p>Microsoft has taken a very different approach to workflow in SharePoint Server 2013 than in previous versions of SharePoint. The workflow team worked with the Azure team to create a new product called Workflow Manager. Workflow Manager serves the role of
 hosting the latest version of the Windows Workflow Foundation (version 4) runtime and all the necessary services in a highly available and scalable way. It takes advantage of Microsoft Azure Service Bus for performance and scalability, and when deployed, it
 runs the same whether in an on-premises deployment or a deployment in the cloud. SharePoint 2013 is then connected and configured to hand off all workflow execution and related tasks to the Workflow Manager farm.</p>
<p>One of the more important changes in the new workflow architecture is that all custom workflows in SharePoint 2013 completely declarative, including those built using Visual Studio 2012. In previous versions of SharePoint, workflows developed with Visual
 Studio 2012 were not exclusively declarative. Instead, they were a pairing of declarative XAML with a compiled assembly. The managed assembly contained the workflow’s business logic.</p>
<p>This might come as a shock to seasoned SharePoint developers who may be asking, &quot;so how do I implement my custom business logic without a compiled assembly?&quot;. Microsoft suggests that instead you create a custom web service, ideally a WCF, OData, or RESTful
 web service that returns data in the JavaScript Object Notation (JSON) format, and to use some of the new activities and objects in this new version.
</p>
</div>
<a id="sec1"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Scenarios for using web services in SharePoint 2013 workflows</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle0"></a>
<p>It is not difficult to conceive of scenarios where you would leverage a custom web services in a SharePoint 2013 workflow. Developers who authored workflows using SharePoint 2007 or SharePoint 2010 are accustomed to working with custom code, since these
 workflows were inherently programmatic. You were not required to add custom code to these workflows, but doing so was quite common.</p>
<p>With SharePoint 2013 workflows to being purely declarative, many cases where you may have written custom code must now be handled with code written in an external web service that is called and consumed by the workflow.
</p>
<p>SharePoint 2013 workflows can consume any sort of web service. That said, it is easiest for workflows to interact with web services that pass data using the Open Data protocol (<strong>OData</strong>), as provided in either of the formats
<strong>Atom</strong> or <strong>json</strong>. OData is the best approach because it is fully supported by the SharePoint 2013 workflow authoring tools (both SharePoint Designer 2013 and Visual Studio 2012).</p>
<p>In addition, both anonymous web services as well as those protected with different types of authentication are supported. In fact, you have full control over the request and response handling for each service call. Thus, for example, you can use a series
 of activities within a workflow to first authenticate using one service to obtain an OAuth token, and then include that token in future requests to services secured using
<a href="http://oauth.net/2/" target="_blank">OAuth 2.0</a>.</p>
</div>
<a id="sec2"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Leveraging web services in workflows</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle1"></a>
<p>Working with web services in SharePoint 2013 workflows involves two stages. The first is simply calling the web service, which you do by using a new
<span class="input">HttpSend</span> activity introduced with SharePoint 2013. <span class="input">
HttpSend</span> lets you call into the simplest web services or, for more complex tasks, provides HTTP verbs and provides specific HTTP headers. Figure 1 shows many of the properties that are available on the
<span class="input">HttpSend</span> activity.</p>
<div class="caption">Figure 1. Properties Tool Window for the HttpSend Activity</div>
<br>
<img id="ngWSSP2013WorkflowVS201201" alt="Figure 1. Properties Tool Window for the HttpSend" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201201(v=office.15).png" title="Figure 1. Properties Tool Window for the HttpSend">
<p>You must also specify the method type you wish to use in the service request. Notice in Figure 1 that in the
<span class="label">Request</span> block you can specify the method type (in this case,
<span class="input">GET</span>). Available options include <span class="input">
GET</span>, <span class="input">PUT</span>, <span class="input">POST</span>, and
<span class="input">DELETE</span> (although there are others). This is the primary way to tell web services, specifically RESTful services, what to do on the resource defined in the URI of the activity.</p>
<p>For instance, to get all the properties of a specific item, the <span class="input">
Uri</span> would contain the unique address of the item, and the method would be set to
<span class="input">GET</span>. To delete the item, the <span class="input">Uri</span> would remain the same unique address of the item but the method would be set to
<span class="input">DELETE</span>. The same is true for updating an item except the method would be set to
<span class="input">POST</span>. In creating an item, the <span class="input">
Uri</span> would point to the unique address of the collection where the item is to be created, and the method would be set to
<span class="input">POST</span>. When creating or updating items, services require the data to use what is passed along as content in the request, indicated using the
<span class="input">RequestContent</span> property on the <span class="input">
HttpSend</span> activity.</p>
<p>The second stage of working with web services that we’re going to cover involves submitting or receiving data from a web service. Regardless of whether you use the
<span class="input">RequestContent</span> or <span class="input">ResponseContent</span> properties on the
<span class="input">HttpSend</span> activity) you can pass the data as a complex structure, which are formatted as JavaScript Object Notation (JSON) strings. The good news is, you don’t have to create and manipulate these json strings manually. Instead, Microsoft
 gives you a new object type, the <a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj193446(v=azure.10).aspx" target="_blank">
DynamicValue</a>, that makes your task much easier. </p>
<p><span class="input">DynamicValue</span> objects can store hierarchal data as well as store the response of a web service call. Furthermore, there is a series of activities associated with
<span class="input">DynamicValue</span> objects that you can use to count the number of items in the response, extract values from the response, or build up a new structure for updating or creating items.</p>
</div>
<a id="sec3"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Creating web services for SharePoint 2013 workflows</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle2"></a>
<p>With the support for calling web services and the lack of supporting custom code within workflows, developers will now need to know how to create services. There are plenty of options for creating custom web services for use in SharePoint 2013 workflows.
 The <span class="input">HttpSend</span> activity and <span class="input">DynamicValue</span> data type are best suited for RESTful services and those that conform to the OData Protocol.</p>
<p>OData is a protocol for creating and consuming data based on the principles of REST services. It was developed in an effort to standardize exchanging data using the mature, reliable, and robust HTTP protocol. Once the OData specification was complete, different
 organizations implemented the protocol on their own technology stacks. Microsoft implemented its own version of OData and branded it
<a href="http://msdn.microsoft.com/en-us/library/hh487257(v=vs.103).aspx" target="_blank">
Windows Communication Foundation (WCF) Data Services 5.0</a>.</p>
<p>The RESTful services implemented by SharePoint 2013 actually support OData because they were built using WCF Data Services, specifically WCF Data Services 5.0, which implements the OData 3.0 specification.</p>
<h3 class="subHeading">Implement OData Service CRUD-Q operations</h3>
<div class="subsection">
<p>A common use for web services is performing simple create, read, update, delete, and query (CRUD-Q) operations on data within a database. Creating an OData service for use with a SharePoint 2013 workflow is quite simple using WCF. Assuming you have an existing
 database there are four short steps that require very little coding:</p>
<div class="subSection">
<ol>
<li>
<p>Create a model of your database using the <a href="http://msdn.microsoft.com/en-us/library/bb399567(v=vs.110).aspx" target="_blank">
Entity Framework</a>. There is no code required (Visual Studio, provides a wizard).</p>
</li><li>
<p>Create a new WCF data service. There is no code required (Visual Studio provides a wizard).</p>
</li><li>
<p>In the service code file, set the name of the entity model (created in step #1) to the source of the service, then set the accessibility and permission for the entities in the model. Both steps require as little as two lines of code.</p>
</li><li>
<p>Publish the service to a location that Workflow Manager can access.</p>
</li></ol>
</div>
</div>
<h3 class="subHeading">Implement OData service operations</h3>
<div class="subsection">
<p>Another task you’ll want to accomplish using web services is running business logic that may not fit into the CRUDQ model. For example, consider an OData service that supports CRUD-Q operations for creating new bank loans. Suppose this service also supports
 consumers calling the service and providing a credit score to retrieve a current interest rate for a prospective loan. This type of task does not fall into the CRUDQ model, since it calls a method and passes in an integer to receive a response.</p>
<p>OData and WCF data services support this scenario by providing you with <a href="http://msdn.microsoft.com/en-us/library/cc668788(v=vs.110).aspx" target="_blank">
service operations</a>. Service operations are common and are even used within SharePoint 2013 services, for instance, when retrieving a specific list using the address
<span class="code">http://[..]/_api/web/lists/GetByTitle(‘ListTitle’)</span>. The
<span class="input">GetByTitle</span> method is a service operator the SharePoint 2013 team created. Developers create their own custom service operations in custom web services created using WCF Data Services.</p>
</div>
</div>
<a id="sec4"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Walkthrough: Create a workflow with Visual Studio 2012</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle3"></a>
<p>The following walkthrough demonstrates how to create a custom workflow that calls an OData web service on the Northwind database. You can find the Northwind database hosted at
<a href="http://www.odata.org" target="_blank">OData.org</a>. </p>
<p>When the workflow is completed, users will enter a customer ID, then start the workflow. When started, the workflow retrieves additional customer information and updates the list item with the data it has retrieved.</p>
<div class="subSection">
<ol>
<li>
<p>Start Visual Studio 2012 and create a new SharePoint-hosted app project.</p>
</li><li>
<p>In this project, create a new custom list and name it &quot;Customers&quot;.</p>
</li><li>
<p>In this new list, create the following fields. Leave the default data type for each field as
<span class="input">string</span>:</p>
<ul>
<li>
<p>CustomerId (renamed from the default &quot;Title&quot; field)</p>
</li><li>
<p>Customer Name</p>
</li><li>
<p>Job Title</p>
</li><li>
<p>Address</p>
</li><li>
<p>Country/Region</p>
</li><li>
<p>Business Phone</p>
</li><li>
<p>Fax Number</p>
</li></ul>
</li><li>
<p>Now, add a workflow to the project by clicking in <span class="label">Solution Explorer</span> on
<span class="label">Add</span> &gt; <span class="label">New Item</span>; then, in the
<span class="label">Add New Item</span> dialog box, select the <span class="label">
Workflow</span> project item from the <span class="label">Office/SharePoint</span> category.
</p>
</li><li>
<p>Name the workflow &quot;CompleteCustomerDetails&quot; and click <span class="label">Next</span>.</p>
</li><li>
<p>When prompted by the <span class="label">Customization wizard</span>, name the workflow &quot;Complete Customer Details&quot; and set it to be a
<span class="label">List</span> workflow. Cick <span class="label">Next</span>.</p>
</li><li>
<p>On the next wizard page, check the box to create an association, select the <span class="label">
Customer</span> list, then select <span class="label">Create New</span> for the workflow history and task lists. Click
<span class="label">Next</span>.</p>
</li><li>
<p>On the final wizard page, check the box to start the workflow manually; leave the option to start automatically
<strong>un</strong>-checked. Click <span class="label">Finish</span>.</p>
</li><li>
<p>At this point, Visual Studio displays the workflow designer surface that contains a single
<span class="input">Sequence</span> activity.</p>
</li><li>
<p>Change the name of the <span class="label">Sequence</span> activity to <span class="label">
Root</span>.</p>
</li><li>
<p>Add four more <span class="input">Sequence</span> activities inside the Root activity and name them as follows:</p>
<ul>
<li>
<p>Init</p>
</li><li>
<p>Get Customer Data From Service</p>
</li><li>
<p>Process Service Response</p>
</li><li>
<p>Update List Item<br>
</p>
</li></ul>
</li><li>
<p>At this point, the workflow will appear as shown in Figure 2.</p>
<div class="caption">Figure 2. Complete Customer Details Workflow with Four Empty Sequences</div>
<br>
<img id="ngWSSP2013WorkflowVS201202" alt="Figure 2. Complete Customer Details Workflow" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201202(v=office.15).png" title="Figure 2. Complete Customer Details Workflow"></li></ol>
</div>
<h3 class="subHeading">Get the customer ID entered by the user</h3>
<div class="subsection">
<p>The first thing the workflow needs to do is retrieve the customer ID, as entered by the user. To do this, you need to create two variable.</p>
<div class="subSection">
<ol>
<li>
<p>Click the <span class="label">Variables</span> tab at near the bottom of the workflow designer and create two variables</p>
<ul>
<li>
<p><span class="input">CustomerItemProperties</span> (data type = <span class="input">
DynamicValue</span>; scope = <span class="input">Init</span>). Use this variable to store the result set returned by the activity that gets all properties from the list item.</p>
<div class="alert">
<table>
<tbody>
<tr>
<th align="left"><img id="alert_note" alt="Note" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.alert_note(v=office.15).gif" title="Note"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>The <span class="input">DynamicValue</span> data type is not shown by default. To find it, select the
<span class="label">Browse for Types</span> option in the <span class="label">
Variable Type</span> column. In the search box at the top of the dialog, enter <span class="label">
DynamicValue</span>, and then select the <span class="input">Microsoft.Activities.DynamicValue</span>.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p><span class="input">CustomerId</span> (data type = <span class="input">String</span>; scope =
<span class="input">Root</span>): Use this variable to store the customer ID entered by the user.</p>
</li></ul>
</li><li>
<p>Locate the <span class="label">LookupSpListItem</span> activity in the <span class="label">
SP – List</span> section of the toolbox and drag it to the <span class="label">
Init</span> sequence. Set the activity properties as shown in Figure 3.</p>
<div class="caption">Figure 3. Properties Tool Window for the LookupSPListItem Activity</div>
<br>
<img id="ngWSSP2013WorkflowVS201203" alt="Figure 3. Properties Tool Window" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201203(v=office.15).png" title="Figure 3. Properties Tool Window">
<p>This activity tells Workflow Manager to use the SharePoint REST API to retrieve the properties of the current list item and to store the
<strong>JSON</strong> response in the <span class="input">DynamicValue</span> variable that you just created.</p>
</li><li>
<p>Retrieve the customer ID from the list item by clicking the Get Properties link in the
<span class="label">LookupSpListItem</span> activity. Doing this adds a <span class="label">
GetDynamicValueProperties</span> activity to the design surface.</p>
</li><li>
<p>In the <span class="label">Properties</span> dialog box, click the ellipsis (<span class="label">…</span>) to open the Property selector, shown in Figure 4. In the wizard, set the
<span class="label">Entity Type</span> to <span class="label">List Item of Customers</span>, then add a single property, CustomerId, with the Path set to CustomerId and Assign To set to CustomerId (the variable previously created), as shown in the following
 figure.</p>
</li><li>
<p>Click <span class="label">Create Property</span> and enter <span class="label">
CustomerId</span> in the <span class="label">Path</span> column.</p>
</li><li>
<p>In the <span class="label">Assign To</span> column, enter <span class="label">
CustomerId</span>, which is the variable we created earlier. Figure 4 shows the completed
<span class="label">Properties</span> dialog box.</p>
<div class="caption">Figure 4. Properties dialog for the GetDynamicValueProperties Activity.</div>
<br>
<img id="ngWSSP2013WorkflowVS201204" alt="Figure 4. Properties dialog from the activity" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201204(v=office.15).png" title="Figure 4. Properties dialog from the activity"></li></ol>
</div>
</div>
<h3 class="subHeading">Call the Northwind OData web service</h3>
<div class="subsection">
<p>The workflow now has a reference to the customer ID, so the next step is to call the web service. To do this, we’ll be working primarily with the
<span class="label">Get Customer Data from Service</span> sequence.</p>
<div class="subSection">
<ol>
<li>
<p>Select the <span class="label">Get Customer Data from Service</span> sequence and create two new variables:</p>
<ul>
<li>
<p><span class="input">NorthwindServiceUri</span> (data type = <span class="input">
String</span>; scope = <span class="label">Get Customer Data from Service</span>). This variable stores the URI that is used to query the web service.</p>
</li><li>
<p><span class="input">NorthwindServiceResponse</span> (data type = <span class="input">
DynamicValue</span>; scope = <span class="input">Root</span>): This variable will store the web service response.</p>
</li></ul>
</li><li>
<p>To create the URL to query the web service, start by locating an <span class="label">
Assign</span> activity in the workflow toolbox and drag it to the <span class="label">
Get Customer Data from Service</span> sequence. Notice that the <span class="label">
Assign</span> activity has two parts representing a name-value pair.</p>
</li><li>
<p>Set the left portion of the <span class="label">Assign</span> activity to <span class="label">
NorthwindServiceUri</span>.</p>
</li><li>
<p>Set the right portion of the activity to the string <span class="code">&quot;http://services.odata.org/Northwind/Northwind.svc/Customers('&quot; &#43; CustomerId &#43; &quot;')?$format=json&quot;</span>. Figure 5 shows the completed activity.</p>
<div class="caption">Figure 5. Assign Activity Used to Set a Variable Containing the OData Service</div>
<br>
<img id="ngWSSP2013WorkflowVS201205" alt="FIgure 5. Assign Activity" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201205(v=office.15).png" title="FIgure 5. Assign Activity"></li><li>
<p>Drag an <span class="input">HttpSend</span> activity from the toolbox to the
<span class="label">Get Customer Data from Service</span> sequence, immediately following the
<span class="label">Assign</span> activity.</p>
</li><li>
<p>Set the properties on the <span class="label">HttpSend</span> activity using the values shown in Figure 6.</p>
<div class="caption">Figure 6. HttpSend Properties</div>
<br>
<img id="ngWSSP2013WorkflowVS201206" alt="Figure 6. HttpSend Properties" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201206(v=office.15).png" title="Figure 6. HttpSend Properties"></li></ol>
</div>
</div>
<h3 class="subHeading">Process the Northwind OData web service response</h3>
<div class="subsection">
<p>Once the web service request has been made and the results are stored in a local variable, the next step is to process the response. Each value in the response needs to be added to a different variable.
</p>
<div class="subSection">
<ol>
<li>
<p>Create a variable for each of the fields that we created at the start of this walkthrough (except the customer ID field), shown here:</p>
<ul>
<li>
<p>Customer Name</p>
</li><li>
<p>Job Title</p>
</li><li>
<p>Address</p>
</li><li>
<p>Country/Region</p>
</li><li>
<p>Business Phone</p>
</li><li>
<p>Fax Number</p>
</li></ul>
</li><li>
<p>Name each of these variables according to its respective field name.</p>
</li><li>
<p>All of the variables should be of type <span class="input">String</span>; all of the variables should be scoped to
<span class="input">Root</span>.</p>
</li><li>
<p>Add a <span class="input">GetDynamicValueProperties</span> activity to the <span class="label">
Process Service Request</span> sequence.</p>
</li><li>
<p>In the <span class="label">Properties</span> window, set the <span class="label">
Source</span> value to <span class="input">NorthwindServiceResponse</span>, as shown in Figure 7.</p>
</li><li>
<p>Click the ellipsis button (<span class="label">…</span>) button on the <span class="label">
Properties</span> property and then provide values in the <span class="label">Path</span> and
<span class="label">Assign To</span> columns as shown in Figure 7. Notice that the values in the
<span class="label">Assign To</span> column are the variable you created for each of the
<span class="label">Customers</span> list fields.</p>
<div class="caption">Figure 7. Properties tool window for GetDynamicValueProperties and contents for Properties dialog</div>
<br>
<img id="ngWSSP2013WorkflowVS201207" alt="Figure 7. Properties tool window" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201207(v=office.15).png" title="Figure 7. Properties tool window"></li></ol>
</div>
</div>
<h3 class="subHeading">Update the customer list item</h3>
<div class="subsection">
<p>The last step is to update the list item. </p>
<div class="subSection">
<ol>
<li>
<p>Add an <span class="input">UpdateListItem</span> activity to the <span class="label">
Update List Item</span> sequence and use the <span class="label">Properties</span> window to set the following values:</p>
<ul>
<li>
<p><span class="label">ListId</span>: (current list)</p>
</li><li>
<p><span class="label">ItemId</span>: (current item)</p>
</li></ul>
</li><li>
<p>Click the ellipsis button (<span class="label">…</span>) button on the <span class="input">
ListItemPropertiesDynamicValues</span> property and in the resulting dialog box, set
<span class="label">Entity Type</span> to <span class="label">List Item of Customers</span>.
</p>
</li><li>
<p>Finally, for each of the values extracted from the web service, set the values on the list item to the variables in the workflow, as shown in Figure 8.</p>
<div class="caption">Figure 8. ListItemPropertiesDynamicValue Dialog with Values Set</div>
<br>
<img id="ngWSSP2013WorkflowVS201208" alt="Figure 8. ListItemPropertiesDynamicValue Dialog" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201208(v=office.15).png" title="Figure 8. ListItemPropertiesDynamicValue Dialog"></li></ol>
</div>
</div>
<h3 class="subHeading">Test the workflow</h3>
<div class="subsection">
<p>The workflow is now complete and should function properly. To confirm its stability, you should test it.</p>
<div class="subSection">
<ol>
<li>
<p>Press <span class="label">F5</span> to start debugging; Visual Studio builds and deploys the SharePoint-hosted app.</p>
</li><li>
<p>When the browser opens, navigate to the <span class="label">Customers</span> list, create a single customer record with a
<span class="label">Customer Id</span> of &quot;ALFKI&quot;, as shown in Figure 9, and then save the item.</p>
<div class="caption">Figure 9. New List Item</div>
<br>
<img id="ngWSSP2013WorkflowVS201209" alt="Figure 9. New List Item" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201209(v=office.15).png" title="Figure 9. New List Item"></li><li>
<p>Next, manually start the workflow and then go back to the list item. Keep refreshing the page to see the workflow update the list item, as shown in Figure 10</p>
<div class="caption">Figure 10. Updated List Item</div>
<br>
<img id="ngWSSP2013WorkflowVS201210" alt="Figure 10 Updated List Item" src="https://i-msdn.sec.s-msft.com/en-us/library/dn532193.ngWSSP2013WorkflowVS201210(v=office.15).png" title="Figure 10 Updated List Item">
<p>Notice that the list item was updated by the SharePoint hosted app on behalf of the person who started the workflow. In this walkthrough, however, it was started by the administrator.</p>
</li></ol>
</div>
</div>
</div>
<a id="sec5"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Conclusion</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle4"></a>
<p>SharePoint 2013 introduced a new workflow architecture facilitated by a new product: Workflow Manager 1.0. To ensure that all custom workflows worked regardless of the SharePoint 2013 deployment choice, either on-premises or hosted in Office 365, all workflows
 are now 100-percent declarative. Therefore, custom business logic previously implemented as custom code in Visual Studio-authored workflows in previous versions of SharePoint are no longer supported.
</p>
<p>Microsoft introduced support for calling web services in Workflow Manager using the new
<span class="input">HttpSend</span> activity. Workflow Manager also introduced support for creating structures to submit to web services as well as consuming their responses called the
<span class="input">DynamicValue</span> data type. When creating workflows, use this data type and associated actions to facilitate creating and leveraging robust business processes in SharePoint 2013 workflows by using external web services.</p>
</div>
<a id="bk_addresources"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Additional resources</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle5"></a>
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj193446(v=azure.10).aspx" target="_blank">Working with complex data in a workflow</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163986.aspx" target="_blank">Workflows in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
</div>
</div>
</div>
