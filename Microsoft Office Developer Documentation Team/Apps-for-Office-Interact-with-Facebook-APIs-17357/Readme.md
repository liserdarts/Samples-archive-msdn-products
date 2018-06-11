# Apps for Office: Interact with Facebook APIs
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Excel 2013
* apps for Office
## Topics
* sites and content
* social computing
## IsPublished
* True
## ModifiedDate
* 2013-04-23 02:37:50
## Description

<div id="header"><span class="label">Summary:</span> This sample task pane app for Excel shows how to log on to Facebook from an Excel 2013 task pane, and then how to retrieve some basic user data from Facebook by using the Facebook API.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p>This sample first displays the Facebook logon screen. Once the user has logged on to their existing Facebook account, they choose
<span class="ui">Display User Info</span> to display basic public information about themselves, such as their name, gender, user ID, birthdate, and friend count.</p>
<p>The sample makes use of standard Facebook API sample code, first in an anonymous function to load the Facebook SDK APIs asynchronously, and then in the
<span><span class="keyword">window.fbAysncInit()</span></span> function, to initialize the Facebook SDK API and authorize the user. Both functions are in the Home.html file.</p>
<p>Choosing <span class="ui">Display User Info</span> executes the <span><span class="keyword">Get Friends</span></span> function, in the Home.js file. This function gets the count of user friends, and then it passes the result to the
<span><span class="keyword">DisplayUserInfo</span></span> function. <span><span class="keyword">DisplayUserInfo</span></span> gets the rest of the basic user data and displays all the data in a table in Excel, starting at the currently selected cell, by
 using the <a href="http://msdn.microsoft.com/en-us/library/fp142294(v=office.15)" target="_blank">
setSelectedDataAynsc</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp142295(v=office.15)" target="_blank">
Document</a> object of the JavaScript API for Office.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Excel 2013.</p>
</li><li>
<p>Visual Studio 2012; apps for Office project template.</p>
</li><li>
<p>Internet Explorer 9 or Internet Explorer 10.</p>
</li><li>
<p>Basic familiarity with JavaScript and HTML.</p>
</li><li>
<p>A Facebook developer account and a Facebook app ID. To get these, see the <a href="https://developers.facebook.com/web/" target="_blank">
Facebook for Web Developers</a> documentation.</p>
</li><li>
<p>The URL of the web server where you will post the HTML page that is part of this sample. This can be of the form
<em>//localhost:port_number/</em> if you are hosting the page in IIS on your development computer. Supply this URL to Facebook when you create your Facebook app.</p>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection2">
<p>The <em>Apps for Office: Interact with Facebook APIs</em> sample is created by the FBFriendFinder solution, which contains the following projects and important files:</p>
<ul>
<li>
<p>The FBFriendFinder project, including the following file:</p>
<ul>
<li>
<p>FBFriendFinder.xml manifest file</p>
</li></ul>
</li><li>
<p>The FBFriendFinderWeb project, including the following files:</p>
<ul>
<li>
<p>Home.html file</p>
</li><li>
<p>Home.js file</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>In the <span><span class="keyword">window.fbAysncInit()</span></span> function, in the Home.html file, replace the
<em>appID</em> placeholder with the ID of your Facebook app.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Choose the F5 key in Visual Studio 2012 to build and deploy the app and open it in Excel 2013.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>&nbsp;</p>
<ol>
<li>
<p>Open the FBFriendFinder.sln file in Visual Studio 2012.</p>
</li><li>
<p>Choose the F5 key to build and deploy the app.</p>
</li><li>
<p>Enter user information in the Facebook logon dialog box.</p>
</li><li>
<p>Choose <span class="ui">Display User Info</span> in the Excel task pane to display user information in Excel.</p>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to display the Facebook logon dialog box, ensure that you are using the correct ID for the Facebook app you created. Also check that you enter the correct URL of the HTML page that is part of this sample in the Facebook App information screen.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>Second release.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142185(office.15).aspx" target="_blank">JavaScript API for Office</a></p>
</li></ul>
</div>
</div>
</div>
