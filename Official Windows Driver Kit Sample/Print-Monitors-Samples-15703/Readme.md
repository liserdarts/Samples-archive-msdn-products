# Print Monitors Samples
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:22
## Description

<div id="mainSection">
<p>The Print Monitors samples contain monitors which handle communications between the print spooler and other print control elements.
</p>
<p>The following print monitors are included in the sample:</p>
<table>
<tbody>
<tr>
<th>Sample</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>LocalMon</p>
</td>
<td>
<p>A port monitor server DLL is a user-mode DLL that is responsible for providing a communications path between the user-mode print spooler and the kernel-mode port drivers that access I/O port hardware.
</p>
</td>
</tr>
<tr>
<td>
<p>LocalUI</p>
</td>
<td>
<p>A port monitor's user interface DLL contains user interface functionality and runs on print client systems.
</p>
</td>
</tr>
<tr>
<td>
<p>PJLMon</p>
</td>
<td>
<p>A language monitor provides full-duplex communication between the print spooler and bidirectional printers that are capable of providing software-accessible status information. The language monitor adds printer control information, such as commands defined
 by a printer job language, to the data stream. </p>
</td>
</tr>
</tbody>
</table>
<p class="note"><b>Note</b>&nbsp;&nbsp;These samples demonstrate how to create print monitors for the v3 print driver model only.</p>
<p>For more information about port monitors, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559895(v=vs.85).aspx">
Port Monitors</a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff556450(v=vs.85).aspx">
Language Monitors</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>To build a driver solution using Windows&nbsp;8.1 driver kit (Windows Driver Kit (WDK)) and Visual Studio&nbsp;2013, perform the following steps.</p>
<dl><dd>
<p>1. Open the solution file in Visual Studio&nbsp;2013</p>
</dd><dd>
<p>2. Add all non-binary files (usually located in the \install directory of the sample) to the Package project</p>
<dl><dd>
<p>a. In the <b>Solution Explorer</b>, right click <b>Driver Files</b></p>
</dd><dd>
<p>b. Select <b>Add</b>, then click <b>Existing Item</b></p>
</dd><dd>
<p>c. Navigate to the location to which you downloaded the sample, and select all the files in the install directory, or the equivalent set of non-binary files such as INFs, INIs, GPD, PPD files, etc.</p>
</dd><dd>
<p>d. Click <b>Add</b></p>
</dd></dl>
</dd><dd>
<p>3. Configure these files to be added into the driver package</p>
<dl><dd>
<p>a. In the <b>Solution Explorer</b>, right click the Package project and select
<b>Properties</b></p>
</dd><dd>
<p>b. In the left pane, click <b>Configuration Properties</b> &gt; <b>Driver Install</b> &gt;
<b>Package Files</b>.</p>
</dd><dd>
<p>c. In the right pane, use the ellipsis button (...) to browse to the set of files that needs to be added to the driver package. All the data files that you added in
<b>Step 2-c</b>, except the INF file, should be added.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This configuration is per-architecture, so this configuration must be repeated for each architecture that will be built.</p>
</dd><dd>d. Click <b>OK</b> </dd></dl>
</dd><dd>
<p>4. Open the INF file and edit it to match the built output</p>
<dl><dd>
<p>a. Open the INF file</p>
</dd><dd>
<p>b. In the Version section, add a reference to a catalog file like this: CatalogFile=XpsDrvSmpl.cat
</p>
</dd><dd>
<p>c. In the SourceDisksFiles section, change the location of the DLL files you are building, to =1. This indicates that there is no architecture specific directory in this driver. If you ship multiple architectures simultaneously, you will need to collate
 the driver INF manually. </p>
</dd></dl>
</dd></dl>
<p></p>
<p>At this point, Visual Studio&nbsp;2013 will be able to build a driver package and output the files to disk. In order to configure driver signing and deployment, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
<p>For more information about how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>Use the following information to install and run the monitor samples.</p>
<h3><a id="LocalMon"></a><a id="localmon"></a><a id="LOCALMON"></a>LocalMon</h3>
<p>The INF file, monitor.inf, is used for installing the port monitor. You can install both the port monitor DLL and the port monitor's user interface DLL by using the monitor.inf file.</p>
<h3><a id="LocalMon"></a><a id="localmon"></a><a id="LOCALMON"></a>LocalMon</h3>
<p>You must install the port monitor's user interface DLL together with the port monitor DDKLocalmon.dll file that is built in output directory. The INF file, monitor.inf, that you need to install the port monitor is in the source directory.</p>
<h3><a id="PJLMon"></a><a id="pjlmon"></a><a id="PJLMON"></a>PJLMon</h3>
<p>To install a language monitor, its file name must be listed in an INF file by using a LanguageMonitor entry. This entry must be included for every printer driver that controls a printer that requires the use of the language monitor. All files that the language
 monitor requires must be listed in the CopyFiles entry for each driver. The Pjlmon.inf file shows how to use the LanguageMonitor entry.</p>
</div>
