# Apps for Office: Enable communication between apps
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Excel 2013
* apps for Office
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-03-08 02:56:17
## Description

<p><span style="font-size:small">The code in the task pane app for Excel demonstrates how to build a simple user interface that saves data to cross-session web browser storage (localStorage). It also shows how to use a dynamically generated div to surface errors
 to the user.</span></p>
<p><span style="font-size:small">The code in the content app for Excel demonstrates how to detect when the selection in the spreadsheet changes, how to get the data selected after that event, and how to monitor the shared data source for changes. It captures
 the data from the spreadsheet in an array, evaluates the spreadsheet data with the data from the task pane app, and then displays the results in a table.</span></p>
<p><span style="font-size:small"><img id="59998" src="/site/view/file/59998/1/AppsForOffice1a.png" alt="" width="645" height="382">&nbsp;</span><br>
&nbsp;<br>
<span style="font-size:small">The content app also displays some best practices around how to use a dynamically-applied CSS to modify the behavior of the UI.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Excel 2013.</span> </li><li><span style="font-size:small">Visual Studio 2012, apps for Office project templates.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Basic familiarity with JavaScript, CSS, jQuery, and HTML5.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The <em>Apps for Office: Enable communication between apps</em> sample app contains the following important files:</span></p>
<ul>
<li><span style="font-size:small">CodeSample_ConnectedApps project, including:</span>
<ul>
<li><span style="font-size:small">CodeSample_ConnectedApps.xml manifest</span> </li><li><span style="font-size:small">CodeSample_ConnectedApps.js file</span> </li><li><span style="font-size:small">CodeSample_ConnectedApps.html file</span> </li><li><span style="font-size:small">toast.js file</span> </li></ul>
</li><li><span style="font-size:small">CodeSample_ConsumerApp project, including:</span>
<ul>
<li><span style="font-size:small">CodeSample_ConsumerApp.xml manifest</span> </li><li><span style="font-size:small">CodeSample_ConsumerApp.js file</span> </li><li><span style="font-size:small">CodeSample_ConsumerApp.html file</span> </li><li><span style="font-size:small">MortgageCalculator.js file</span> </li><li><span style="font-size:small">App.css file</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">No additional configuration is necessary to run the sample.</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Choose the F5 key to build and deploy the apps.</span></p>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Choose the F5 key to build and deploy the apps.</span><br>
<span style="font-size:small">Two instances of Excel 2013 will open, one with the content app (&ldquo;Mortgage Calculator&rdquo;) displayed and the other with the task pane app (&ldquo;Mortgage Info App&rdquo;) displayed.</span>
</li><li><span style="font-size:small">In one of the two instances of Excel 2013, on the
<strong>Insert </strong>tab, in the <strong>Apps for Office</strong> group, choose the arrow below
<strong>App</strong>, and then choose the app that you want to insert.</span><br>
<span style="font-size:small">The other app will be inserted into the current instance of Excel. Both apps should now be inserted in the same Excel session.</span>
</li><li><span style="font-size:small">In the content app (&ldquo;Mortgage Calculator&rdquo;), choose
<strong>Connect to Data</strong> to establish a connection between the two apps (listening for changes in the data source).</span>
</li><li><span style="font-size:small">In the task pane app, enter numbers into the two text box inputs and select an option from the drop-down list. Choose the
<strong>Submit</strong> button when you have entered your data.</span> </li><li><span style="font-size:small">In the Excel spreadsheet, enter numbers into one or more rows in a single column. Select the rows singly or as a range in a single column.</span>
</li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If the app fails to install, ensure that the <strong>
SourceLocation</strong> element in the CodeSample_ConnectedApps.xml and CodeSample_ConsumerApp.xml files has the correct URL value for the DefaultValue attribute.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/cc197062(VS.85).aspx">Introduction to Web Storage</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/4cbc527c-a1d5-4fb0-b6db-28cc40c5d5e2">Document.SelectionChanged event</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142294(v=office.15)">Document.getSelectedDataAsync method</a></span>
</li></ul>
