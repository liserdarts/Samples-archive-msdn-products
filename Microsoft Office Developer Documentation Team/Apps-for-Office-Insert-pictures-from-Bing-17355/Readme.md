# Apps for Office: Insert pictures from Bing
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2012
* Word 2013
* apps for Office
## Topics
* Development
* Bing
* Bing Search API
## IsPublished
* False
## ModifiedDate
* 2012-07-19 02:43:58
## Description

<p><span style="font-size:small">This sample demonstrates how you can create an app that gets images from a web-based image repository and inserts these pictures onto a Word 2013 Preview document surface apart from using the built-in picture-insertion feature
 in Word. This allows a greater range of image repositories to be used for inserting pictures into a Word document.</span></p>
<p><span style="font-size:small">End users enter a search term in the text box on the task pane. The app fills the task pane with a set of images from the Bing image repository that match the user&rsquo;s search term. The user can then select an image to insert
 onto the Word document surface.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Word 2013 Preview.</span> </li><li><span style="font-size:small">Visual Studio 2012, apps for Office project templates.</span>
</li><li><span style="font-size:small">Basic familiarity with JavaScript, CSS, jQuery, and HTML5.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The Apps for Office: Insert pictures from Bing sample includes the following important files:</span></p>
<ul>
<li><span style="font-size:small">InsertBingImage project, including:</span>
<ul>
<li><span style="font-size:small">InsertBingImage.xml manifest file</span> </li><li><span style="font-size:small">InsertBingImage.js file</span> </li><li><span style="font-size:small">InsertBingImage.html file</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">You will need to get a Bing AppId to use the Bing Search 2.0 API. For more information, see
<a href="http://www.bing.com/developers/s/APIBasics.html">Bing API Basics</a>.&nbsp;</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Choose the F5 key in Visual Studio 2012 to build and deploy the app.</span></p>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Choose the F5 key to build and deploy the app. Word 2013 Preview opens with the app task pane displayed.</span>
</li><li><span style="font-size:small">In the text box on the task pane, enter a search term (for example &ldquo;Windows Phone&rdquo;), and then choose the
<strong>Search</strong> button. The task pane displays 15 thumbnail images from the Bing image repository that match the search term.</span>
</li><li><span style="font-size:small">Choose one of the returned images. A smaller, modal dialog box appears with a thumbnail of the selected image.
</span></li><li><span style="font-size:small">Choose the <strong>Insert</strong> button. The full-size version of the thumbnail image is inserted into the Word document at the insertion point.</span>
</li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If no images are returned, ensure that you have a valid Bing AppId.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/dd250847.aspx">Working with SourceTypes (Bing, Version 2)</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/dd250942.aspx">Image SourceType (Bing, Version 2.0)</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142294(v=office.15)">Document.setSelectedDataAsync method</a></span>
</li></ul>
