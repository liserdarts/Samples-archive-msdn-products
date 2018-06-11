# Apps for Office: Persist custom settings
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* Excel 2013
* Project Professional 2013
* apps for Office
* PowerPoint 2013
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-03-08 02:53:53
## Description

<p><span style="font-size:small">This sample app for Office demonstrates how to save custom settings in an app in Word 2013, Excel 2013,&nbsp;PowerPoint 2013, or Project Professional 2013. The app stores data as key/value pairs, using the JavaScript API for
 Office property bag, browser cookies, web storage (localStorage and sessionStorage), or by storing the data in a hidden div in the document. The app also demonstrates best practices for implementing multiple-page navigation in an app for Office.</span></p>
<p><span style="font-size:small">The code that sets and gets data settings is contained in the StorageLibrary.js file. The file contains the following functions:</span></p>
<ul>
<li><span style="font-size:small"><strong>StorageLibrary.saveToPropertyBag</strong> and
<strong>getFromPropertyBag</strong>: Get and set data as a key/value pairs in the JavaScript API for Office property bag. The data can be persisted across browser (app) sessions if the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp161133.aspx">Settings.saveAsync</a></strong> method is called to save the data to the hosting file.</span>
</li><li><span style="font-size:small"><strong>StorageLibrary.saveToBrowserCookies</strong> and
<strong>getFromBrowserCookies</strong>: Get and set data as key/value pairs from the browser&rsquo;s cookies. Both functions require cookies to be enabled in Internet Explorer 9. The data persists across browser (app) sessions.</span>
</li><li><span style="font-size:small"><strong>StorageLibrary.saveToLocalStorage </strong>
and <strong>getFromLocalStorage</strong>: Get and set data as key/value pairs using the long-term web browser storage (localStorage object). This data persists across browser (app) sessions and instances.</span>
</li><li><span style="font-size:small"><strong>StorageLibrary.saveTo.SessionStorage </strong>
and <strong>getFromSessionStorage</strong>: Get and set data as key/value pairs using web browser storage that is limited to the browser (app) session lifetime (sessionStorage object).</span>
</li><li><span style="font-size:small"><strong>StorageLibrary.saveToDocument </strong>
and <strong>getFromDocument</strong>: Get and set the data as key/value pairs in a hidden div dynamically generated in the app interface. This data does not persist across webpage (app interface) refreshes.</span>
</li></ul>
<p><span style="font-size:small"><strong>Note:</strong>&nbsp;The <strong>Settings
</strong>object and its </span><span style="font-size:small">members are not available in Project 2013. Thus, the
<strong>StorageLibrary.saveToPropertyBag </strong></span><span style="font-size:small">and
<strong>StorageLibrary.getFromPropertyBag </strong>methods will not work in a task pane app
</span><span style="font-size:small">inserted into a Project 2013 file.</span></p>
<p><span style="font-size:small">The app interface also demonstrates how to create a multiple-page navigation effect by using HTML div elements, CSS classes, and JavaScript. It is a best practice to use this method for building apps for Office that use multiple
 pages.</span></p>
<p><span style="font-size:small">In addition, the app makes use of a dynamically generated div to display messages to users. This is also considered a best practice for surfacing errors to users.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Word 2013, Excel 2013, PowerPoint 2013, or Project Professional 2013.</span>
</li><li><span style="font-size:small">Visual Studio 2012, apps for Office project templates.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Basic familiarity with JavaScript, CSS, jQuery, and HTML5.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The Apps for Office: Persist custom settings sample app contains the following notable files:</span></p>
<ul>
<li><span style="font-size:small">The CodeSample_PersistCustomSettings project, including:</span>
<ul>
<li><span style="font-size:small">CodeSample_PersistCustomSettings.xml manifest</span>
</li><li><span style="font-size:small">CodeSample_PersistCustomSettings.js file</span>
</li><li><span style="font-size:small">CodeSample_PersistCustomSettings.html file</span>
</li><li><span style="font-size:small">StorageLibrary.js file</span> </li><li><span style="font-size:small">toast.js file</span> </li><li><span style="font-size:small">App.css file</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">No additional configuration is necessary to run the sample.</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Choose the F5 key in Visual Studio 2012 to build and deploy the app.</span></p>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Choose the F5 key to build and deploy the app.</span>
</li><li><span style="font-size:small">Use the app&rsquo;s interface to save data as key/value pairs and to retrieve a stored value using its key.</span>
</li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If the app fails to install, ensure that the <strong>
SourceLocation</strong> element in the CodeSample_PersistCustomSettings.xml has the correct URL value for the DefaultValue attribute.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/cc197062(VS.85).aspx">Introduction to Web Storage</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142179(v=office.15)">Settings object</a></span>
</li></ul>
