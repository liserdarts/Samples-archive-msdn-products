# SharePoint 2013: Drag-and-drop video playlist creator
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-07-11 12:34:21
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Drag-and-drop video playlist creator</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Demonstrates how to use JavaScript, jQuery, and the HTML5 video API in an app for SharePoint that manages HTML5 playlists and their playlist items, and how to use a drag-and-drop operation to add and rearrange the playlist items.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Martin%20Harwar-4025664" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>This app also demonstrates the use of a drag-and-drop operation to upload video files to create a playlist, and to specify the order of video files in a playlist. The app calls the HTML5 video API to play the selected videos.</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent playlists and their associated video files.</p>
<p>The lists are related to each other through lookup fields, and the user interface (UI) ensures that all data operations synchronize with their list items so that the relationships are maintained. The UI is implemented with HTML elements and cascading style
 sheet (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the UI, and the solution contains no server-side code.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either of the following:</p>
<ul>
<li>
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended).</p>
</li><li>
<p>SharePoint Server 2013 configured to host apps, and with a Developer Site Collection already created.</p>
</li></ul>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <span><strong><span class="keyword">Default.aspx</span></strong></span> webpage, which is used to create and edit playlists from MP4 video files, and which is also used to play those playlists.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.js</span></strong></span> file in the
<span><strong><span class="keyword">scripts</span></strong></span> folder, which is used to retrieve and manage playlist and video data by using the JavaScript implementation of the client object model (JSOM). The
<span><strong><span class="keyword">App.js</span></strong></span> file also contains the UI logic that is implemented in
<span><strong><span class="keyword">Default.aspx</span></strong></span>, such as providing a drag-and-drop user interface and providing HTML5
<span><strong><span class="keyword">Media</span></strong></span> elements for playing playlists.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.css</span></strong></span> file in the
<span><strong><span class="keyword">contents</span></strong></span> folder, which contains style definitions used by the elements in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>Two list definitions and instances: one for productions and one for the edited clips in the productions. The lists are linked together by lookup fields. A document library is also included to let the user upload videos for inclusion in productions.</p>
</li><li>
<p>All other files are automatically provided by the Visual Studio 2012 project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to configure the sample.</p>
<div>
<ol>
<li>
<p>Open the <span><strong><span class="keyword">SP_VideoPlaylist_js.sln</span></strong></span> file in Visual Studio 2012.</p>
</li><li>
<p>In the <span><strong><span class="keyword">Properties</span></strong></span> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site Collection to the
<span><strong><span class="keyword">Site URL</span></strong></span> property. You may be prompted to provide credentials if you add a URL to an Office 365 site.</p>
</li><li>
<p>No other configuration is necessary.</p>
</li></ol>
</div>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3">
<p>&nbsp;</p>
<div>
<ol>
<li>
<p>Press Ctrl&#43;Shift&#43;B to build the solution.</p>
</li><li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 or Office 365 Enterprise site if you are prompted to do so by the browser.</p>
</li><li>
<p>When the app appears, the starting screen resembles Figure 1.</p>
<strong>
<div class="caption">Figure 1. Start screen</div>
</strong><br>
<img src="/site/view/file/92231/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Playlist</span></strong> to enter a new playlist name, as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. New Playlist form</div>
</strong><br>
<img src="/site/view/file/92232/1/image.png" alt=""> </li><li>
<p>Figure 3 shows that three playlists have been created. Each playlist name includes a button to play the contents of the playlist and a button to edit its contents.</p>
<strong>
<div class="caption">Figure 3. List of playlists</div>
</strong><br>
<img src="/site/view/file/92233/1/image.png" alt=""> </li><li>
<p>Click an <strong><span class="ui">edit</span></strong> button to see that playlist's details, as shown in Figure 4. At this point, no videos have been uploaded or assigned yet.</p>
<strong>
<div class="caption">Figure 4. Edit playlist form</div>
</strong><br>
<img src="/site/view/file/92234/1/image.png" alt=""> </li><li>
<p>Click the <strong><span class="ui">Upload Files</span></strong> link to open the document library that is included in the app. This is a standard document library, so you can upload video files using any of the standard methods. Figure 5 shows how to use
 a drag-and-drop operation to upload four MP4 files from a local folder.</p>
<strong>
<div class="caption">Figure 5. Uploading video files</div>
</strong><br>
<img src="/site/view/file/92235/1/image.png" alt=""> </li><li>
<p>Now when you edit a playlist, the videos that have been uploaded are available as shown in Figure 6.</p>
<strong>
<div class="caption">Figure 6. List of available videos</div>
</strong><br>
<img src="/site/view/file/92236/1/image.png" alt=""> </li><li>
<p>Figure 7 shows that you can use a drag-and-drop operation to move a video into the playlist.</p>
<strong>
<div class="caption">Figure 7. Moving a video into a playlist</div>
</strong><br>
<img src="/site/view/file/92237/1/image.png" alt=""> </li><li>
<p>Figure 8 shows that three videos have been added to the playlist.</p>
<strong>
<div class="caption">Figure 8. Playlist with three video files</div>
</strong><br>
<img src="/site/view/file/92238/1/image.png" alt=""> </li><li>
<p>Figure 9 shows how the order of the videos in the playlist can be managed, again with drag-and-drop techniques. In this case, the
<span><strong><span class="keyword">Birds.mp4</span></strong></span> video has been moved after
<span><strong><span class="keyword">Seals.mp4</span></strong></span>.</p>
<strong>
<div class="caption">Figure 9. Changing the order of videos in a playlist</div>
</strong><br>
<img src="/site/view/file/92239/1/image.png" alt=""> </li><li>
<p>After arranging the videos in a playlist, click <strong><span class="ui">Save</span></strong> to apply your changes. Click the
<strong><span class="ui">Play</span></strong> button for a playlist to see the first video in that playlist rendered and played. Subsequent videos in the playlist are also queued up and ready to play when the current video ends. You can see the first video
 playing in Figure 10.</p>
<strong>
<div class="caption">Figure 10. Playing a playlist</div>
</strong><br>
<img src="/site/view/file/92240/1/image.png" alt=""> </li><li>
<p>Figure 11 shows that the second video in the playlist automatically plays after the first video ends.</p>
<strong>
<div class="caption">Figure 11. Playing the next video in a playlist</div>
</strong><br>
<img src="/site/view/file/92241/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure that you have SharePoint Server 2013 properly configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Enterprise site configured to host apps.</p>
<p>&nbsp;</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5"><strong>
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
<p>July 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://www.jQuery.com" target="_blank">jQuery</a></p>
</li><li>
<p><a href="http://bing.com?q=HTML5" target="_blank">HTML5</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ie/hh924820(v=vs.85).aspx" target="_blank">HTML5 Video API</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
