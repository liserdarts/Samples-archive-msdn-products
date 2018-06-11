# SharePoint 2013: Use the REST and CSOM Social APIs in provider-hosted apps
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* REST
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* social computing
## IsPublished
* True
## ModifiedDate
* 2013-02-28 05:22:44
## Description

<p id="header">This sample demonstrates how to use the Representational State Transfer (REST) and SharePoint 2013 client object model (CSOM) Social APIs.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=be34f5b5-a1d1-47e1-971d-cfdda319992c" target="_blank">Scot Hillier</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>This sample creates an app that adds a button to the ribbon that is associated with a task list. Clicking the button starts the app, which displays posts from the current user's feed that may represent a new task request. These requests are identified because
 the current user is mentioned in the post and the post is tagged with <span><span class="keyword">#Assignment</span></span>. The user can view the posts and click a button to turn a selected post into a new task, which is added to a task list on the current
 site.</p>
<p>There are two versions of the sample: REST and CSOM. Both versions are designed as provider-hosted apps for SharePoint Online. Both versions of the sample interact with the feed portions of the Social API and request appropriate permissions. The sample can
 be used as a starting point for many Social apps.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint Online developer site collection with a sub site containing both a Site Feed and a Task list.</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Basic familiarity with Social capabilities and user profiles in SharePoint.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The Visual Studio solution <strong>Feed2Tasks.sln</strong> contains both the REST and CSOM version of the app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample app.</div>
<ol>
<li>
<div>Edit the <span><span class="keyword">Site URL</span></span> property for the Feed2TasksREST project to refer to your SharePoint Online site where a Task list and Site Feed are located.</div>
</li><li>
<div>Edit the <span><span class="keyword">Site URL</span></span> property for the Feed2TasksCSOM project to refer to your SharePoint Online site where a Task list and Site Feed are located.</div>
</li><li>
<div>Go to the SharePoint site containing a Task list and Site Feed.</div>
</li><li>
<div>In the Site Feed, make a new post that mentions a user, and tag it with <span>
<span class="keyword">#Assignment</span></span>.</div>
<div>&nbsp;</div>
<div><strong>Note:</strong> You must sign in as a different user than the one who will use the app.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Build the entire solution.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<ol>
<li>
<div>Select either <span class="ui">Feed2TasksREST</span> or <span class="ui">
Feed2TasksCSOM</span> as the Startup project.</div>
</li><li>
<div>Press F5.</div>
</li><li>
<div>When the app appears, navigate back to the site containing a Task list and Site Feed.</div>
</li><li>
<div>Click the <span class="ui">List</span> tab.</div>
</li><li>
<div>On the ribbon, choose the <span class="ui">Create Tasks</span> button.</div>
</li><li>
<div>View the posts that mention you and contain the <span><span class="keyword">#Assignment</span></span> tag.</div>
</li><li>
<div>Choose the <span class="ui">Create Task</span> button to make a new task.</div>
</li><li>
<div>Return to the Task list and verify that the new task is created.</div>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the project does not deploy properly, make sure that the target environment is a developer-enabled site collection in SharePoint Online.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142381.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163280.aspx" target="_blank">Social and collaboration features in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163130.aspx" target="_blank">Follow people in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj193046.aspx" target="_blank">Social client class library</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/gg317460(v=office.14).aspx" target="_blank">SharePoint Online: An Overview for Developers</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142385.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj164022.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 REST endpoints</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></div>
</li></ul>
</div>
</div>
</div>
</div>
