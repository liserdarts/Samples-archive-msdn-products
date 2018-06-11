# SharePoint 2010: Creating Event Receivers in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* event receivers
## IsPublished
* True
## ModifiedDate
* 2011-08-04 11:15:30
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create an event receiver for Microsoft SharePoint 2010 by using Microsoft Visual Studio 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg252010.aspx">Creating SharePoint 2010 Events Receivers in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Visual Studio 2010 provides a project type that enables you to build event receivers that perform actions before or after selected events on a Microsoft SharePoint 2010 site. This example shows how to add an event to the adding and updating actions
 for custom list items.</p>
<p>This sample demonstrates the following steps for creating and deploying an event receiver in Visual Studio 2010:</p>
<ol>
<li>Overriding the <strong>itemAdding</strong> event and the <strong>itemUpdating</strong> event.
</li><li>Verifying that the list to which the item is being added is the <strong>Open Position</strong> list.
</li><li>Elevating permissions so that the code can access a secure site to retrieve approved job titles.
</li><li>Comparing approved <strong>Job Titles</strong> with the title of a new item that is created in the
<strong>Open Position</strong> list. </li><li>Canceling the event when the <strong>Job Title</strong> is not approved. </li></ol>
<p>In this example, a secure subsite contains a list named <strong>Job Definitions</strong> that specifies allowed job titles for roles in the organization. Along with job titles, the list also contains confidential salary information for the job title and
 is therefore secured from users. In the main site, a list named <strong>Open Positions</strong> tracks vacancies in the organization. You create two event receivers for the
<strong>itemAdding</strong> and <strong>itemUpdating</strong> events that verify that the title of the open position matches one of the approved titles in the
<strong>Job Definitions</strong> list.</p>
<p>The solution overrides the <strong>ItemAdding</strong> and <strong>ItemUpdating</strong> methods and verifies whether the list that is being added to is the
<strong>Open Positions</strong> list. If it is, a call is made to the <strong>CheckItem</strong> method, passing in the properties that are associated with the event.</p>
<p>In the <strong>CheckItem</strong> method, the permissions are elevated to ensure successful access to the secured subsite. The job titles that are in the approved list are compared to the job title of the
<strong>properties.AfterProperties</strong> property associated with the event. If any title matches, the
<strong>allowedBoolean </strong>variable is set to <strong>true</strong>, and the method returns.</p>
<p>Depending on the value of the allowed variable, the calling method either permits the event or sets the
<strong>properties.ErrorMessage</strong> property and then cancels the event using
<strong>properties.cancel</strong>.</p>
