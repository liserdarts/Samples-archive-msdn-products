# SharePoint 2013: Create and manage a navigation term set using CSOM
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2012
* SharePoint Server 2013
* apps for SharePoint
* SharePoint 2013
## Topics
* Navigation
* enterprise content management (ECM)
* SharePoint client object model (CSOM)
## IsPublished
* False
## ModifiedDate
* 2012-07-15 04:08:57
## Description

<p><span style="font-size:small">The code that shows how the navigation term set of managed navigation works is in the
<strong>NavigationTermSetClient.cs</strong> file.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">A SharePoint 2013 Preview development environment</span>
</li><li><span style="font-size:small">Publishing site</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following files.</span></p>
<ul>
<li><span style="font-size:small"><strong>NavigationTermSetClient.cs</strong>, which create a new taxonomy session (<strong>TaxonomySession</strong>), and checks for an existing term set (<strong>TermSet</strong>)in the first available term store (<strong>TermStore</strong>),
 creates a navigation term set (<strong>NavigationTermSet</strong>), and completes operations on
<strong>Term</strong> objects belonging to that navigation term set.&nbsp;</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the sample.</span></p>
<ul>
<li><span style="font-size:small">Update the <strong>SimpleLinkUrl</strong> type with the URL of your choice.</span>
</li></ul>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Press F5 to compile and run the sample.</span> </li></ol>
<p><span style="font-size:small">You should see a SharePoint 2013 Preview publishing site in a view that hides deprecated terms. A previously viewable term is now hidden.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://technet.microsoft.com/library/3b372a63-7cdf-462a-abb4-750e611e967d.aspx">Build SharePoint 2013 publishing sites</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/46b5a79c-962f-4a07-8316-d5005eabd0e0">Develop with managed metadata and the term store</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/4a2811dc-25fd-4eb2-b0ab-1edded64c556">How to: Customize managed metadata pinning</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/f36645da-77c5-47f1-a2ca-13d4b62b320d">API sets of SharePoint 2013</a></span>
</li></ul>
