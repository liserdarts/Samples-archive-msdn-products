# Apps for Office: Insert Twitter search results
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* Excel 2013
* apps for Office
## Topics
* sites and content
* social computing
## IsPublished
* True
## ModifiedDate
* 2013-03-08 02:57:38
## Description

<p><span style="font-size:small">This sample demonstrates how you can create an app that gets tweets from Twitter based on a given search term, and then insert those results into a table on a worksheet in Excel 2013. You can also modify this sample to insert
 the results into a table in a Word 2013 document.</span></p>
<p><span style="font-size:small">End users enter a search term in the text box on the task pane. The app searches Twitter for tweets that match the search term and then inserts a table with the first page of search results into the worksheet that has the insertion
 point.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Excel 2013 or Word 2013.</span> </li><li><span style="font-size:small">Visual Studio 2012, apps for Office project templates.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Basic familiarity with JavaScript, CSS, jQuery, and HTML5.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The Apps for Office: Insert Twitter search results sample includes the following important files:</span></p>
<ul>
<li><span style="font-size:small">TwitterSearch project, including:</span>
<ul>
<li><span style="font-size:small">TwitterSearch.xml manifest file</span> </li><li><span style="font-size:small">TwitterSearch.js file</span> </li><li><span style="font-size:small">TwitterSearch.html file</span><span style="font-size:small">&nbsp;</span>
</li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">No additional configuration is necessary to run the app.</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Choose the F5 key in Visual Studio 2012 to build and deploy the app.</span></p>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Choose the F5 key to build and deploy the app. Excel 2013 or Word 2013 opens with the app task pane displayed.</span>
</li><li><span style="font-size:small">In the text box on the task pane, enter a search term (for example, &ldquo;Windows Phone&rdquo;), and then choose the
<strong>Search</strong> button. The app inserts a table that contains the first page of tweet results that match the search term onto the worksheet (or document surface).</span>
</li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If the app is unable to return tweet results, verify that the Twitter search URL is still valid. For more information, see the
<a href="http://dev.twitter.com">Twitter Developers site</a>.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://dev.twitter.com">Twitter Developers site</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142145(v=office.15)">Document.setSelectedDataAsync method</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp161002(v=office.15)">TableData object</a></span>
</li></ul>
