# SharePoint 2013: Rough-cut video editor app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* Javascript
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-07-08 04:24:23
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Rough-cut video editor app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Demonstrates how to use JavaScript, jQuery, and the HTML5 video API in an app for SharePoint to edit HTML5 videos, and how to use a drag-and-drop operation to create new video productions from the edited clips.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>You don't have to create new video files using expensive, difficult-to-use editing tools if you just want to re-purpose existing video content.
<span>Rough-cut</span> is an industry-standard term for splicing together pieces of existing video footage to effectively create a new video or montage. Microsoft used this approach to enable broadcasters in the Winter Olympics to output event highlights and
 footage on their websites.</p>
<p>In this app, the user first adds a set of video files from a SharePoint library to the app's &quot;video bin&quot; and then selects one or more of those videos to put into the &quot;production timeline.&quot; Once in the timeline, each video can be clipped with start and end
 times. The resulting production, composed of clips, is called a <span>rough cut</span>.</p>
<p>This app also demonstrates the use of a drag-and-drop operation to upload video files to a production, and to specify the order of clips within a new production. The app calls the HTML5 video APIs for choosing and playing selected frame sequences within
 an edited clip.</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent the original videos, definitions of new productions, and edited video clips in each production.</p>
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
<p>The <span><strong><span class="keyword">Default.aspx</span></strong></span> webpage, which is used to create productions, manage videos clips on production timelines, and edit video clips on production timelines. This page is also used to play the productions.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.js</span></strong></span> file in the
<span><strong><span class="keyword">scripts</span></strong></span> folder, which is used to retrieve and manage production information and video clip data by using the JavaScript implementation of the client object model (JSOM). The
<span><strong><span class="keyword">App.js</span></strong></span> file also contains the UI logic that is implemented in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
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
<p>Open the <span><strong><span class="keyword">SP_RoughCutEditor_js.sln</span></strong></span> file with Visual Studio 2012.</p>
</li><li>
<p>In the <span><strong><span class="keyword">Properties</span></strong></span> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site Collection to the
<span><strong><span class="keyword">Site URL</span></strong></span> property. You may be prompted to provide credentials if you have added a URL to an Office 365 site.</p>
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
<img src="/site/view/file/92036/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Production</span></strong> to enter a new production name, as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. New Production form</div>
</strong><br>
<img src="/site/view/file/92037/1/image.png" alt=""> </li><li>
<p>Figure 3 shows that five productions have been created.</p>
<strong>
<div class="caption">Figure 3. New productions listed</div>
</strong><br>
<img src="/site/view/file/92038/1/image.png" alt=""> </li><li>
<p>Next to each production name is a button to edit the contents of the production, as shown in Figure 4.</p>
<strong>
<div class="caption">Figure 4. Edit button for a production</div>
</strong><br>
<img src="/site/view/file/92039/1/image.png" alt=""> </li><li>
<p>Click the <strong><span class="ui">Edit</span></strong> button for a production to display its contents. Initially, there are no videos in a production. To add a video file, click the
<strong><span class="ui">Upload</span></strong> button shown in Figure 5.</p>
<strong>
<div class="caption">Figure 5. Edit Production form</div>
</strong><br>
<img src="/site/view/file/92040/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Upload</span></strong> to see the document library that is included in this app. This is a standard document library, so you can upload video files using any of the standard methods. Figure 6 shows how to use a drag-and-drop
 operation to upload four MP4 files from a local folder.</p>
<strong>
<div class="caption">Figure 6. Uploading video files</div>
</strong><br>
<img src="/site/view/file/92041/1/image.png" alt=""> </li><li>
<p>Figure 7 shows the four files that have been uploaded and are now listed in the
<strong><span class="ui">Video Bin</span></strong> for a production. Next to each file name is an icon for adding the file to the current production's timeline.</p>
<strong>
<div class="caption">Figure 7. Files in the Video Bin</div>
</strong><br>
<img src="/site/view/file/92042/1/image.png" alt=""> </li><li>
<p>Figure 8 shows that three videos have been added to the production timeline. Each video displays its current clip start and end times. By default, each video is included in its entirety. Click a video's
<strong><span class="ui">[Edit]</span></strong> link to change the clip times, or
<strong><span class="ui">[Remove]</span></strong> to remove a video from the timeline.</p>
<strong>
<div class="caption">Figure 8. Video clips in the Production Timeline</div>
</strong><br>
<img src="/site/view/file/92043/1/image.png" alt=""> </li><li>
<p>Figure 9 shows how you can re-order the videos in the production by using a drag-and-drop operation. In this case, the Seals.mp4 video is being moved from the end of the production to the second position.</p>
<strong>
<div class="caption">Figure 9. Changing the order of video clips</div>
</strong><br>
<img src="/site/view/file/92044/1/image.png" alt=""> </li><li>
<p>Click the <strong><span class="ui">[Edit]</span></strong> link for a video on the timeline to see the video. The video is rendered and paused as shown in Figure 10. You can then use the timeline sliders to set the start and end times for the clip. The
 video frames are displayed as you adjust the timeline sliders, which enables you to set the start and end times to exact frames. Click
<strong><span class="ui">Save</span></strong> to save the current clip times and return to the Edit Production screen, as shown in Figure 10.</p>
<strong>
<div class="caption">Figure 10. Editing a video clip timeline</div>
</strong><br>
<img src="/site/view/file/92045/1/image.png" alt=""> </li><li>
<p>Figure 11 shows how the start and end times for the Seals.mp4 clip have been applied. Click
<strong><span class="ui">Save</span></strong> to save your changes to the production.</p>
<strong>
<div class="caption">Figure 11. Revised clip in the Production Timeline</div>
</strong><br>
<img src="/site/view/file/92046/1/image.png" alt=""> </li><li>
<p>Next to each production name is a button to play the production, as shown in Figure 12.</p>
<strong>
<div class="caption">Figure 12. Play button for a production</div>
</strong><br>
<img src="/site/view/file/92047/1/image.png" alt=""> </li><li>
<p>Click the play button for a production to see the clips on that production's timeline played in sequence, using the start and end times of each of those clips. The result is that it appears as if a new video has been created from subsets of multiple videos.
 This is a <span>rough cut</span>. Figure 13 shows an example screen shot.</p>
<strong>
<div class="caption">Figure 13. Play a production</div>
</strong><br>
<img src="/site/view/file/92048/1/image.png" alt=""> </li><li>
<p>Figure 14 shows that you can add the same video to the production timeline more than once. In this case, the Horses.mp4 video has been added three times, where each instance presents different cuts of that video.</p>
<strong>
<div class="caption">Figure 14. Multiple copies of a clip in the Production Timeline</div>
</strong><br>
<img src="/site/view/file/92049/1/image.png" alt=""> </li></ol>
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
