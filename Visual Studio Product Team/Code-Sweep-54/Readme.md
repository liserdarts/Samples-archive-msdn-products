# Code Sweep
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
* 2011-03-03 11:24:19
## Description

<h2>Summary</h2>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">This sample allows the user to specify a set of terms to search for by specifying a set of XML files containing the term definitions. The user-configurable settings are stored in the
 project file. The scan can be invoked either on command or as an integrated part of the build process. When the scan is performed, a custom task provider causes hits, if any, to be shown in the task list.<br>
</span></p>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">&nbsp;</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h2>Introduction</h2>
<p>This sample will:<br>
<br>
* Demonstrates writing MSBuild tasks, which run as part of the build.<br>
* Demonstrates using a host object to communicate with MSBuild tasks from within the integrated development environment (IDE).<br>
* Shows how to store and retrieve information in both MSBuild projects and non-MSBuild projects.<br>
* Shows how to implement a custom task provider, including a custom toolbar and shortcut menu.<br>
* Demonstrates how to place commands on the Project menu and the Solution Explorer shortcut menu, and how to handle them.<br>
* Includes an algorithm to search for multiple terms across multiple streams of characters.<br>
<br>
This sample is divided into several projects:<br>
1. Scanner: class library that implements the scanning functionality.<br>
2. BuildTask: MSBuild task implementation that allows the scan to run as part of the build process.<br>
3. VsPackage: Visual Studio Package (VSPackage) implementation that provides a UI for the scanning functionality.<br>
4. VsPackageUI: resource definitions for the native satellite DLL used by VsPackage.<br>
5. Utilities: generally useful utility functions used by other projects in the solution.<br>
6. Setup: builds a MSI module which can be used to install CodeSweep on any computer with Visual Studio 2005.<br>
7. DevenvSetupCustomAction: builds a custom action executable used by the setup project that runs &quot;devenv.exe /setup&quot; to merge the new menu items defined by CodeSweep into the Visual Studio menu structure.<br>
Additionally, there is a second solution (TDD\TDD.sln) that contains the unit<br>
tests written for CodeSweep.<br>
<br>
The terms to search for are defined in XML files. For an example of the supported format, see<br>
VsPackage\sample<em>term</em>table.xml. A term may have zero or more &quot;exclusions&quot;, which define<br>
contexts in which it will not count as a hit. The list of term tables to use is specified on<br>
a per-project basis, and can be configured in the dialog box invoked by the CodeSweep command (on<br>
the Project menu or Solution Explorer shortcut menu).<br>
<br>
Only files with supported extensions can be scanned; others are ignored. This restriction<br>
exists so that large binary files can be avoided if desired. The default list of supported<br>
extensions is found in extensions.xml, which is copied to the user's Application Data\Microsoft\CodeSweep<br>
folder on first run. To support new extensions, add them to that file.<br>
<br>
A scan can be invoked either by MSBuild during the build process, or explicitly<br>
by the user at any time. In either case - to invoke explicitly or to control invocation during builds - use the configuration dialog box. If the scan is enabled to run during<br>
the build, it will be run in command-line builds as well as builds in the IDE.<br>
<br>
When a scan is performed, the results are sent to the Task List. To see them, click CodeSweep in the<br>
provider drop-down list on the Task List toolbar. In the list of results, you can double-click a result to go to its location. The CodeSweep Task List toolbar contains four buttons: Stop Scan,<br>
Repeat Last Scan, Ignore, and Show Ignored Instances. The Ignore command marks the selected<br>
result(s) as ignored, which means that they will not be shown in the Task List (even in<br>
future scans). To see the ignored instances, click the Show Ignored Instances button. The<br>
shortcut menu also contains these commands.<br>
<br>
</p>
<h2>Requirements</h2>
<ul>
<li>Visual Studio 2010 </li><li>Visual Studio 2010 SDK </li></ul>
<p>&nbsp;</p>
<h2>Download and install</h2>
<ul>
<li>Download the zip file associated with the sample </li><li>Unzip the sample to your machine </li><li>Double click on the .sln file to launch the solution </li></ul>
<p>&nbsp;</p>
<h2>Building and running the sample</h2>
<ul>
<li>To build and execute the sample, press F5 after the sample is loaded </li></ul>
