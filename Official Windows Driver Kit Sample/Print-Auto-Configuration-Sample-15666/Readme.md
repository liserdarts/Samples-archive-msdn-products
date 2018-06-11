# Print Auto Configuration Sample
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
* 2014-04-02 12:44:42
## Description

<div id="mainSection">
<p>The auto configuration sample demonstrates how to implement Unidrv-based and PScript5-based drivers to leverage the inbox support for auto-configuration. The sample works only when used with the standard TCP/IP port monitor or the Network-Connected Device
 (NCD) port monitor. </p>
<p><b>Auto-Configuration Basics</b> </p>
<p>Prior to Microsoft® Windows® Vista, the settings of a print queue were set initially to the driver's default settings, rather than to the appropriate settings based on the device. For Unidrv or Pscript5, this means that static default values specified in
 the GPD or PPD file are used for the initial print queue setup. This static set of defaults must, necessarily, represent the minimum configuration that a printer can ship with. For instance, if a stapling unit is optional for a device, then by default, such
 a device cannot have stapling capability enabled, otherwise the user interface for devices without the stapling unit would show stapling as an option. A customer who selected the stapling option but found that it did not work would likely be confused.</p>
<p>For any device that comes with features not present in the basic model, a user or administrator must manually configure these features on the print queue after installation. This can at times be a confusing and non-intuitive experience. The configuration
 process is easy to get wrong, particularly with regard to internal parameters such as memory and hard disk size, which significantly can affect printing speed and quality.
</p>
<p>Auto-configuration solves this problem by automatically configuring the print queue according to the installable features on the device, rather than simply using the driver's static default settings. The main target for auto-configuration is network printers.
 They are the most likely ones to have multiple optional features and require more manual configuration. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560774(v=vs.85).aspx">
Printer Autoconfiguration</a>.</p>
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
<p>The auto configuration sample doesn’t have any binaries to be built. It may be installed by using
<b>Add Printer Wizard</b> and supplying the AutoCnfg.INF as the INF file.</p>
<p>But to build a signed driver package using Windows&nbsp;8.1 driver kit (Windows Driver Kit (WDK)) and Visual Studio&nbsp;2013, for the project file (csproj) that ships with the auto configuration sample, perform the following steps.</p>
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
<p>To test the auto-configuration feature you need to install the driver to a Standard TCP/IP port. You need a printer, which understands and responds to the bidi SNMP queries, connected to the port. The tcpbidi.xml file located in system32 directory has information
 about the SNMP OIDs used for each query. The installable options in the device settings will reflect the information obtained by querying the printer.</p>
</div>
