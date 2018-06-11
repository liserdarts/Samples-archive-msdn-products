# MSPLOT Plotter Driver Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:18:23
## Description

<div id="mainSection">
<p>The MSPLOT sample is a printer driver that is designed to support all HPGL/2 compatible plotters.
</p>
<p>The full source code of this driver, which includes a parser and UI, is published in the WDK as an example of how to write a user-mode printer driver in Windows.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The MSPLOT sample is for the v3 print driver model only.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>To build a driver solution using Windows&nbsp;8 driver kit (WDK) and Microsoft Visual Studio 2012, perform the following steps.</p>
<dl><dd>
<p>1. Open the solution file in Visual Studio 2012</p>
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
<p>At this point, Visual Studio 2012 will be able to build a driver package and output the files to disk. In order to configure driver signing and deployment, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
<p>For more information about how to build a driver solution using Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>You can install all plotter samples by using the <i>plotter.inf</i> file.</p>
</div>
