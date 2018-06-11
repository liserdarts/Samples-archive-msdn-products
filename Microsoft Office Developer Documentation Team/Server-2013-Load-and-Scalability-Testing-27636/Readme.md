# Project Server 2013: Load and Scalability Testing
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Project Server 2013
## Topics
* Scalability
* Testing
## IsPublished
* True
## ModifiedDate
* 2014-03-11 02:01:25
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Project Server 2013: Load and Scalability Testing</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Get started with load and scalability testing on Project Server 2013 with sample web performance tests.</p>
</div>
<div>
<p><b>Last modified: </b>March 05, 2014</p>
<p><b>In this article</b> <br>
<a href="#sectionSection0">Prerequisites</a> <br>
<a href="#sectionSection1">To run the sample web performance tests</a> <br>
<a href="#sectionSection2">Troubleshooting</a> <br>
<a href="#sectionSection3">Change log</a> <br>
<a href="#sectionSection4">Additional resources</a> <br>
</p>
<p>This sample includes a set of Visual Studio web performance tests for a Project Server 2013 on-premises environment. The tests include simple scenarios such as navigating to a Project Center page, submitting status for tasks assigned to users, and saving
 timesheets. The tests are not comprehensive, but they provide a starting point for developing a more complex set of tests.</p>
<p><span>Provided by:</span> Chris Elwell, Microsoft Corporation</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0" name="collapseableSection">
<p>To run the web performance tests in this sample, you'll need:</p>
<ul>
<li>
<p>Project Server 2013 on a farm that uses standard Windows Authentication. These tests cannot be used with Project Online, and they are unlikely to work with Project Server 2010.</p>
</li><li>
<p>Visual Studio 2010, Visual Studio 2012, or Visual Studio 2013</p>
</li></ul>
<div>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Note</b> </th>
</tr>
<tr>
<td>
<p>If the farm is configured to use a custom claims provider, you'll have to modify the tests.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>To run the sample web performance tests</h1>
<div id="sectionSection1" name="collapseableSection">
<div>
<ol>
<li>
<p>Extract the downloaded sample files to a directory of your choice.</p>
</li><li>
<p>Open the PWAWebTests.sln file in Visual Studio.</p>
</li><li>
<p>Build the solution.</p>
<p>It's important to build the solution before you try to run the sample tests. Some tests use extraction rules that aren't included in the set that is provided by Visual Studio. These additional extraction rules and plug-ins are built into a DLL that must
 be deployed with the tests.</p>
</li><li>
<p>In <b><span class="ui">Solution Explorer</span></b>, in the <b><span class="ui">PWAWebTests</span></b>&gt;<b><span class="ui">SupportFiles</span></b> folder, replace the placeholder test parameters in the following files to match your topology. These
 parameters represent user names and passwords, the server name, and relative paths.</p>
<ul>
<li>
<p><b><span class="ui">Administrators.csv</span> </b>Stores user names and passwords for users in the default
<b><span class="ui">Administrators</span></b> security group for Project Web App (PWA). The first row is a header, and the subsequent rows are comma-separated pairs that use the format
<span>&lt;DOMAIN\USERNAME&gt;,&lt;PASSWORD&gt;</span></p>
</li><li>
<p><b><span class="ui">ProjectManagers.csv</span> </b>Stores user names and passwords for users in the default
<b><span class="ui">Project Managers</span></b> security group for PWA. The format is identical to Administrators.csv.</p>
</li><li>
<p><b><span class="ui">ResourceManagers.csv</span> </b>Stores user names and passwords for users in the default
<b><span class="ui">Resource Managers</span></b> security group for PWA. The format is identical to Administrators.csv.</p>
</li><li>
<p><b><span class="ui">TeamMembers.csv</span> </b>Stores user names and passwords for users in the default
<b><span class="ui">Team Members</span></b> security group for PWA. The format is identical to Administrators.csv.</p>
</li><li>
<p><b><span class="ui">Servers.csv</span> </b>Contains a header row, followed by the name of the server that you want to test. For example, if your PWA deployment is located at
<span>http://MYSERVER/sites/pwa1</span>, the value should be <span>MYSERVER</span>.</p>
</li><li>
<p><b><span class="ui">SiteUrls.csv</span> </b>Contains a header row, followed by the server-relative URLs of your PWA instances. For example, if your deployment is located at
<span>http://MYSERVER/sites/pwa1</span>, the value should be <span>sites/pwa1</span>.</p>
</li></ul>
</li><li>
<p>Run or debug the web tests that you plan to use. In the <b><span class="ui">PWAWebTests</span></b>&gt;<b><span class="ui">WebTests</span></b> folder, open the .webtest file, and then choose
<b><span class="ui">Run Test</span></b>&gt;<b><span class="ui">Run Test</span></b> or
<b><span class="ui">Run Test</span></b>&gt;<b><span class="ui">Debug Test</span></b>.</p>
<p>You should run or debug web tests to make sure that they run without errors before you create load tests.</p>
</li><li>
<p>Create load tests.</p>
<p>After you've verified that the web tests work, and authored any additional web tests that you require for your environment, you should create load tests. Load tests let you run one or more web tests multiple times, with controlled concurrency. This lets
 you evaluate how the environment behaves under various levels of load. Instructions for authoring a load test is beyond the scope of this document, but see
<a href="http://msdn.microsoft.com/en-us/library/ms182594.aspx" target="_blank">Create and run a load test</a> (http://msdn.microsoft.com/en-us/library/ms182594.aspx) for in-depth information.</p>
</li><li>
<p>If you're using a test rig to run tests, ensure that deployment is enabled for the DLL that contains additional extraction rules and the CSV support files, as follows:</p>
<ol>
<li>
<p>Open the Local.testsettings file. In the <b><span class="ui">Test Settings</span></b> dialog box, choose the
<b><span class="ui">Deployment</span></b> page.</p>
</li><li>
<p>Ensure the <b><span class="ui">Enable Deployment</span></b> box is selected.</p>
</li><li>
<p>Ensure the following files are added to the <b><span class="ui">Additional files and directories to deploy</span></b> box:</p>
<ul>
<li>
<p><span>&lt;Solution Directory&gt;\PWAWebTests\bin\Release\PWAWebTests.dll</span>
</p>
</li><li>
<p><span>&lt;Solution Directory&gt;\PWAWebTests\SupportFiles\</span> </p>
</li></ul>
</li></ol>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection2" name="collapseableSection">
<p>If you're using a test rig and you receive errors that a file cannot be found, ensure the PWAWebTests DLL and supporting CSV files are deployed, as described in step 7.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection3" name="collapseableSection"><b>
<div class="caption"></div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
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
<div id="sectionSection4" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/dn250793.aspx" target="_blank">Run performance tests on an application before a release</a> (http://msdn.microsoft.com/en-us/library/dn250793.aspx)</p>
</li><li>
<p><a href="http://technet.microsoft.com/en-us/projectserver/fp123546.aspx" target="_blank">Project Server 2013 for IT pros</a> (http://technet.microsoft.com/en-us/projectserver/fp123546.aspx)</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-US/office/aa905469" target="_blank">Project for developers</a> (http://msdn.microsoft.com/en-US/office/aa905469)</p>
</li></ul>
</div>
</div>
</div>
