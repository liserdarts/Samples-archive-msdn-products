# SharePoint 2013: Picture Editor App
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* apps for SharePoint
## IsPublished
* True
## ModifiedDate
* 2014-01-07 01:57:01
## Description

<div id="header"><span>Provided by:</span> Vivek Soni, <a href="http://www.microsoft.com/india/msindia/msindia_aboutus_msgd.aspx" target="_blank">
Microsoft Services Global Delivery</a></div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This sample picture editor app enables you to select an image and perform basic image editing operations without using a standalone image editor.</p>
<ul>
<li>
<p>You can select an image from the parent site's picture libraries, capture from the device's webcam, or browse or drag an image file from the file system. The image can be saved to a picture library in the parent site, or can be downloaded locally.</p>
</li><li>
<p>This app uses the new HTML5 capabilities for the <strong><span class="keyword">Canvas</span></strong> to enable image editing operations such as drawing objects, changing the color palette, and adding image effects. HTML5 also supports client-side image
 renditions such as scaling and touch-enabled devices.</p>
</li><li>
<p>The app attempts to read geolocation data from the image binary's Exchangeable image file format (Exif) record. If the host SharePoint library has a geolocation field, the data is saved there.</p>
</li><li>
<p>The app also adds two custom actions and places them on the <strong><span class="ui">Edit Control Block</span></strong> (ECB) and in the
<strong><span class="ui">Files</span></strong> tab in the ribbon on the parent site's
<strong><span class="ui">Picture</span></strong> libraries. These custom actions will start the app for the selected picture.</p>
</li></ul>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample app requires the following:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2012</p>
</li><li>
<p>Microsoft Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>SharePoint Server 2013</p>
</li><li>
<p>A team or publishing site with a Picture library (not a Document library). Optionally, the picture library should contain a geolocation field.</p>
</li><li>
<p>An HTML5-compliant browser, such as Internet Explorer 10 (or later), Google Chrome, or FireFox.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1">
<p>The sample app contains the following key components:</p>
<ul>
<li>
<p>The <strong><span class="keyword">Default.aspx</span></strong> webpage, which is used to open, edit, and save images.</p>
</li><li>
<p>The <strong><span class="keyword">scripts</span></strong> folder, which contains several scripts that can be reused for other apps for SharePoint.</p>
<ul>
<li>
<p><strong><span class="keyword">SPPictureEditor_CanvasEditor.js</span> </strong>
leverages the <a href="http://jquer.in/jquery-plugins-for-html5-canvas/websanova-paint/" target="_blank">
Websanova Paint jQuery plug-in</a>, extended with additional tools.</p>
</li><li>
<p><strong><span class="keyword">SPPictureEditor_AssetPicker.js</span> </strong>
, <strong><span class="keyword">SPPictureEditor_FileBrowser.js</span></strong>,
<strong><span class="keyword">SPPictureEditor_DragDrop.js</span></strong>, and <strong>
<span class="keyword">SPPictureEditor_Cam.js</span></strong> provide the data sources from SharePoint, the browser, the file system, or the camera.</p>
</li><li>
<p><strong><span class="keyword">SPPictureEditor_SPDataLibrary.js</span> </strong>
loads and saves an image in a SharePoint site and attempts to add the geolocation data.</p>
</li></ul>
</li></ul>
</div>
<h1>Build the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to build the sample.</p>
<div>
<ol>
<li>
<p>Press Ctrl&#43;Shift&#43;B to build the solution.</p>
</li><li>
<p>Press F5 to run the app.</p>
</li><li>
<p>If you are prompted by the browser, sign in to your SharePoint Server 2013 or Office 365 Enterprise site.</p>
</li></ol>
</div>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection3">
<p>&nbsp;</p>
<div>
<ol>
<li>
<p>Select one of the displayed image sources.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Some HTML5 Web browsers do not support capturing Webcam devices, so the <strong>
<span class="ui">Device</span></strong> camera option will be disabled if you are using such as browser (for example Internet Explorer 10), or you may be notified that the feature is not supported (for example Firefox).</p>
</td>
</tr>
</tbody>
</table>
</div>
<strong>
<div class="caption">Figure 1. Start screen</div>
</strong><br>
<strong></strong><img src="/site/view/file/106705/1/image.png" alt=""> </li><li>
<p>For example, using the SharePoint asset picker, you can browse and select an image file from any of the Picture libraries on the host site.</p>
<strong>
<div class="caption">Figure 2. Select an image file</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/106706/1/image.png" alt=""> </li><li>
<p>Use the HTML5 canvas editor to draw objects and lines, add text, and choose colors.</p>
<strong>
<div class="caption">Figure 3. Canvas editor</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/106707/1/image.png" alt=""> </li><li>
<p>Save the edited image back to the source library or another picture library.</p>
<strong>
<div class="caption">Figure 4. Edited image saved</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/106708/1/image.png" alt=""> </li><li>
<p>The app installs some custom actions on the host site's Picture libraries for quick image selection. For example, notice the new action on the
<strong><span class="ui">Edit Control</span></strong> block:</p>
<strong>
<div class="caption">Figure 5. Custom action in the Edit Control Block</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/106709/1/image.png" alt="">
<p>Note also the change on the Ribbon:</p>
<strong>
<div class="caption">Figure 6. Custom action in the Picture library ribbon</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/106710/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Verify that you have access to a SharePoint site.</p>
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
<p>January 2014</p>
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
<p><a href="http://jquer.in/jquery-plugins-for-html5-canvas/websanova-paint/" target="_blank">Websanova Paint jQuery plug-in</a></p>
</li><li>
<p><a href="http://blog.nihilogic.dk/2008/05/reading-exif-data-with-javascript.html" target="_blank">Exif Reader library</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
