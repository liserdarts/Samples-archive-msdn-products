# MSDN Search - VS 2013
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2013
## Topics
* Extensibility
## IsPublished
* True
## ModifiedDate
* 2014-05-29 08:40:34
## Description

<div id="longDesc">
<h2>Introduction</h2>
<p><span>This sample demonstrates how to extend Quick Launch and add a search provider to search outside Tools Options / Menus / Open Documents (the built-in providers).</span></p>
<ul>
<li><span>In this sample Quick Launch display results from MSDN.</span> </li></ul>
<p>&nbsp;</p>
<h2>Quick Navigation</h2>
<table>
<tbody>
<tr>
<td><span>1 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#Req">
Requirements</a></span></td>
</tr>
<tr>
<td><span>2 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#downloadAndInstall">
Download and Install</a></span></td>
</tr>
<tr>
<td><span>3 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#BuildAndRun">
Building and Running</a></span></td>
</tr>
<tr>
<td><span>4 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#GettingStarted">
Getting Started</a></span></td>
</tr>
<tr>
<td><span>4.1 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#ProjFiles">
Project Files</a></span></td>
</tr>
<tr>
<td><span>4.4 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#Status">
Status</a></span></td>
</tr>
<tr>
<td><span>4.5 <a href="http://code.msdn.microsoft.com/QuickLaunchExtensionMSDNSearchProvider#AddResx">
Additional Resources</a></span></td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<h2>Requirements</h2>
<p><span><a class="externalLink" href="http://msdn.com/vstudio">Visual Studio 2013 and Visual Studio 2013 SDK</a></span><br>
<br>
</p>
<h2>Download and Install</h2>
<ul>
<li><span>Go to the &quot;Downloads tab&quot; and download the zip file associated with this sample
</span></li><li><span>Unzip the sample to your machine </span></li><li><span>Double click on the .sln file to launch the solution </span></li></ul>
<p>&nbsp;</p>
<h2>Building and Running</h2>
<p><span>To build and execute the sample, press F5 after the sample is loaded. This will launch the experimental hive which will demonstrate the sample's function.</span><br>
<br>
</p>
<h3>Getting Started</h3>
<p><br>
<span>Once this sample is installed, when using Quick Launch, MSDN is one of the search providers for which results are displayed. You will also see MSDN Search in Tools Options &gt; Quick Launch under the list of providers. To scope search to MSDN, prefix
 search string with @msdn, e.g. &quot;@msdn Hello World&quot;. </span><br>
<br>
</p>
<h2>Project Files</h2>
<table border="1">
<tbody>
<tr>
<th>
<p><span>File Name</span></p>
</th>
<th>
<p><span>Description</span></p>
</th>
</tr>
<tr>
<td><span><strong>MSDNSearchPackage</strong></span></td>
<td><span>This file contains the ExtensionPointPackage implementation. Declares the supported extension points (the Quick Launch search provider). It also is responsible for declaring About Box registration data.</span></td>
</tr>
<tr>
<td><span><strong>MSDNSearchProvider</strong></span></td>
<td><span>This file contains the search provider implementation. The class implements the IVsSearchProvider interface that enables Quick Launch integration.</span></td>
</tr>
<tr>
<td><span><strong>MSDNSearchTask</strong></span></td>
<td><span>This class derives from Microsoft.VisualStudio.Shell.VsSearchTask and implements the task used to perform the actual searches. It queries the MSDN online server, process the returned RSS feed and creates search results to be displayed in the Quick
 Launch popup.</span></td>
</tr>
<tr>
<td><span><strong>MSDNSearchResult</strong></span></td>
<td><span>This class represents a search result from MSDN Search category. It exposes properties such as DisplayText, Tooltip, and implements the action to be executed when the search result is selected by the user.</span></td>
</tr>
<tr>
<td><span><strong>Guids</strong></span></td>
<td><span>This is the list of command IDs that the sample defines</span></td>
</tr>
<tr>
<td><span><strong>Resources (.resx/.Designer.cs)</strong></span></td>
<td><span>These files are used to support localized strings and search results icons.</span></td>
</tr>
<tr>
</tr>
<tr>
<td><span><strong>Resources\ResultsIcon.ico</strong></span></td>
<td><span>This file defines the icon to be used for the search results in MSDN category</span></td>
</tr>
<tr>
</tr>
<tr>
<td><span><strong>Resources\Product.ico</strong></span></td>
<td><span>This file defines the icon to be used for the product, in About Box and in Extensions and Updates dialog.</span></td>
</tr>
<tr>
<td><span><strong>VSPackage.resx</strong></span></td>
<td><span>This file is used to support localized strings and images for About Box registration.</span></td>
</tr>
<tr>
<td><span><strong>Example.MSDNSearch.png</strong></span></td>
<td><span>This file defines the preview image to be used by the VSIX extension in the Extensions and Updates dialog.</span></td>
</tr>
</tbody>
</table>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel"><br>
</span></p>
<h2>Status</h2>
<table border="1">
<tbody>
<tr>
<th><span>Function </span></th>
<th><span>Status </span></th>
</tr>
<tr>
<td><span>Demonstrates Accessibility</span></td>
<td><span>No</span></td>
</tr>
<tr>
<td><span>Demonstrates Error Handling</span></td>
<td><span>Yes</span></td>
</tr>
<tr>
<td><span>Follows SDK Coding Standards</span></td>
<td><span>No</span></td>
</tr>
<tr>
<td><span>Demonstrates Localization</span></td>
<td><span>No</span></td>
</tr>
<tr>
<td><span>Implements Functional Tests</span></td>
<td><span>No</span></td>
</tr>
<tr>
<td><span>Samples supported by Microsoft</span></td>
<td><span>Yes</span></td>
</tr>
<tr>
<td><span>Implements Unit Tests</span></td>
<td><span>No</span></td>
</tr>
</tbody>
</table>
<p><span id="Span3"><br>
</span></p>
</div>
