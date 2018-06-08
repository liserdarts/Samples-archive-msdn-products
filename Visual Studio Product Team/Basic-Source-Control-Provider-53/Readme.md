# Basic Source Control Provider
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010 SDK
## Topics
* MSBuild
* VSX
## IsPublished
* True
## ModifiedDate
* 2011-02-28 01:01:04
## Description

<h2>Introduction</h2>
<p><span style="font-size:small">This sample demonstrates how to create a source control provider that registers with Visual Studio and can be selected as active source control provider.</span></p>
<ul>
<li><span style="font-size:small">Implement a source control provider package </span>
</li><li><span style="font-size:small">Expose an Options page visible only when the provider is active
</span></li><li><span style="font-size:small">Expose a tool window visible only when the provider is active
</span></li><li><span style="font-size:small">Display menu items only when the provider is active
</span></li></ul>
<p>&nbsp;</p>
<h2>Quick Navigation</h2>
<table>
<tbody>
<tr>
<td><span style="font-size:small">1 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#Req">
Requirements</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">2 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#downloadAndInstall">
Download and Install</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">3 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#BuildAndRun">
Building and Running</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#GettingStarted">
Getting Started</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4.1 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#ProjFiles">
Project Files</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4.2 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#UnitTests">
Unit Tests</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4.3 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#FuncTests">
Functional Tests</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4.4 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#Status">
Status</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4.5 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#History">
History</a></span></td>
</tr>
<tr>
<td><span style="font-size:small">4.6 <a href="http://code.msdn.microsoft.com/BasicSCCProvider#AddResx">
Additional Resources</a></span></td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<h2>Requirements</h2>
<p><span style="font-size:small"><a class="externalLink" href="http://msdn.com/vstudio">Visual Studio 2010 and Visual Studio 2010 SDK</a></span><br>
<br>
</p>
<h2>Download and Install</h2>
<ul>
<li><span style="font-size:small">Go to the &quot;Downloads tab&quot; and download the zip file associated with this sample
</span></li><li><span style="font-size:small">Unzip the sample to your machine </span></li><li><span style="font-size:small">Double click on the .sln file to launch the solution
</span></li></ul>
<p>&nbsp;</p>
<h2>Building and Running</h2>
<p><span style="font-size:small">To build and execute the sample, press F5 after the sample is loaded. This will launch the experimental hive which will demonstrate the sample's function.</span><br>
<br>
</p>
<h3>Getting Started</h3>
<p><span style="font-size:small">This sample has a package (BasicSccProvider) and the source control service (SccProviderService). This sample exposes one tool window (SccProviderToolWindow) and one Options page (SccProviderOptions).</span><br>
<br>
<span style="font-size:small">The source control provider can be selected on the Tools, Options, Source Control, Plug-In Selection pae as &quot;Managed Source Control Sample Basic Provider&quot;. Selecting it will make it the active source control provider.</span><br>
<br>
<span style="font-size:small">When the provider is selected as the active source control provider, the command &quot;Scc Command&quot; will be added to the Tools menu and the command &quot;Source control provider toolwindow&quot; will be added to the View manu, and the Tools,
 Options, Source Control, Sample Options Page will become visible. When &quot;View/Source control provider toolwindow&quot; is clicked, the tool window will be created. The visibility of menu commands, Options page, and tool window is controlled by the active stateof
 the provider. All of the UI elements will be hidden automatically when the provider is deactivated.</span><br>
<br>
</p>
<h2>Project Files</h2>
<table border="1" style="border:1px solid #000000; width:1885px; height:388px">
<tbody>
<tr>
<th>
<p><span style="font-size:small">File Name</span></p>
</th>
<th>
<p><span style="font-size:small">Description</span></p>
</th>
</tr>
<tr>
<td><span style="font-size:small"><strong>BasicSccProvider</strong></span></td>
<td><span style="font-size:small">This file contains the Package implementation. It also is responsible for handling the enabling and execution of source control commands.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>SccProviderInterface</strong></span></td>
<td><span style="font-size:small">This file contains the source control service implementation. The class implements the IVsSccProvider interface that enables source control provider activation and switching.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>SccProviderOptions</strong></span></td>
<td><span style="font-size:small">This class derives from MsVsShell.DialogPage and provider the Options page. It is responsible for handling the Option page events, such as activation, apply, and close. It hosts the SccProviderOptionsControl.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>SccProviderOptionsControl</strong></span></td>
<td><span style="font-size:small">This class is a UserControl that will be hosted on the Options page. It has a label to demonstrate display of controls in the page.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>SccProviderToolWindow</strong></span></td>
<td><span style="font-size:small">This class derives from ToolWindowPane, which provides the IVsWindowPane implementation. It is responsible for defining the window frame properties such as caption and bitmap. It hosts the SccProviderToolWindowControl.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>SccProviderToolWindowControl</strong></span></td>
<td><span style="font-size:small">This class is a UserControl that will be hosted in the tool window. It has a label to demonstrate display of controls in the page.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>CommandId</strong></span></td>
<td><span style="font-size:small">This is a list of GUIDs specific to this sample, especially the package GUID and the commands group GUID. It also includes GUIDs of other elements used by the sample.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>Guids</strong></span></td>
<td><span style="font-size:small">This is the list of command IDs that the sample defines</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>PkgCmd.vsct</strong></span></td>
<td><span style="font-size:small">This file describes the menu structure and commands for this sample</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>Resources (.cs/.vb/.resx/.Designer.cs/.Designer.vb)</strong></span></td>
<td><span style="font-size:small">These files are used to support localized strings.</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>Resources\Images_24bit.bmp</strong></span></td>
<td><span style="font-size:small">This bitmap defines the icons that are used for tool windows</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>Resources\Images_32bit.bmp</strong></span></td>
<td><span style="font-size:small">This bitmap defines the icons that are used for toolbars and menu commands</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>Resources\Product.ico</strong></span></td>
<td><span style="font-size:small">This file defines the icon to be used for the product</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>ProvideSourceControlProvider</strong></span></td>
<td><span style="font-size:small">This file contains the implementation of a custom registration attribute that registers a source control provider. It is used to make the source control provider visisble on the Tools, Options, SourceControl, Plugsins page</span></td>
</tr>
<tr>
<td><span style="font-size:small"><strong>ProvideToolsOptionsPageVisibility</strong></span></td>
<td><span style="font-size:small">Thi file contains the implementation of a custom registration attribute that defines the visibility of a tool window. It is used to make the tool window implemented by the provider visible only when the provider is active (that
 is, when the provider context UI has been asserted)</span></td>
</tr>
</tbody>
</table>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel"><br>
</span></p>
<h2>Unit Tests</h2>
<table border="1" style="border:1px solid black">
<tbody>
<tr>
<th><span style="font-size:small">File Description </span></th>
</tr>
<tr>
<td><span style="font-size:small">Verify that the package can be created and sited and that it implements IVsPackage</span></td>
</tr>
<tr>
<td><span style="font-size:small">Verify that the source control provider service can be ceated, activated, and deactivated</span></td>
</tr>
<tr>
<td><span style="font-size:small">Test the &quot;Scc Command&quot; command on the Tools menu by toggling it twice</span></td>
</tr>
<tr>
<td><span style="font-size:small">Test the Options-page Activate, Deactivate, Apply, and Close events</span></td>
</tr>
<tr>
<td><span style="font-size:small">Test that the tool window can be displayed and that the toolbar command can be executed</span></td>
</tr>
</tbody>
</table>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel"><br>
</span></p>
<h2>Functional Tests</h2>
<table border="1" style="border:1px solid black">
<tbody>
<tr>
<th><span style="font-size:small">File Description </span></th>
</tr>
<tr>
<td><span style="font-size:small">Verify sample builds in all configurations</span></td>
</tr>
<tr>
<td><span style="font-size:small">Verify that the sample was registered. The About box should list the product as installed</span></td>
</tr>
<tr>
<td><span style="font-size:small">Verify that the provider is accessible in Tools, Options, SourceControl</span></td>
</tr>
<tr>
<td><span style="font-size:small">Verify that the menu commands are visible only when the provider is active, after it was displayed once</span></td>
</tr>
<tr>
<td><span style="font-size:small">Verify that the Options page is visible only when the provider is active</span></td>
</tr>
</tbody>
</table>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel"><br>
</span></p>
<h2>Status</h2>
<table border="1" style="border:1px solid black">
<tbody>
<tr>
<th><span style="font-size:small">Function </span></th>
<th><span style="font-size:small">Status </span></th>
</tr>
<tr>
<td><span style="font-size:small">Demonstrates Accessibility</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
<tr>
<td><span style="font-size:small">Includes Architecture Diagram</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
<tr>
<td><span style="font-size:small">Demonstrates Error Handling</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
<tr>
<td><span style="font-size:small">Follows SDK Coding Standards</span></td>
<td><span style="font-size:small">No</span></td>
</tr>
<tr>
<td><span style="font-size:small">Demonstrates Localization</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
<tr>
<td><span style="font-size:small">Implements Functional Tests</span></td>
<td><span style="font-size:small">No</span></td>
</tr>
<tr>
<td><span style="font-size:small">Samples supported by Microsoft</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
<tr>
<td><span style="font-size:small">Implements Unit Tests</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
</tbody>
</table>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel"><br>
</span></p>
<h2>History</h2>
<table border="1" style="border:1px solid black">
<tbody>
<tr>
<th><span style="font-size:small">Date </span></th>
<th><span style="font-size:small">Activity </span></th>
</tr>
<tr>
<td><span style="font-size:small">2005-10-01</span></td>
<td><span style="font-size:small">Created this sample for the Visual Studio 2005 SDK</span></td>
</tr>
<tr>
<td><span style="font-size:small">2010-03-05</span></td>
<td><span style="font-size:small">Ported this sample to work in Visual Studio 2010</span></td>
</tr>
</tbody>
</table>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel"><br>
</span></p>
<h2>Additional Resources</h2>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/bb166434%28VS.100%29.aspx">SCC Provider Integration</a></span></p>
