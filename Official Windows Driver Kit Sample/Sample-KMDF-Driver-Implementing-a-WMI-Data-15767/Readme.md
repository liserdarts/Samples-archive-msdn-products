# Sample KMDF Driver Implementing a WMI Data Provider
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* Windows Management Interface (WMI)
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:52:39
## Description

<div id="mainSection">
<p>WmiSamp WMI Provider is a sample KMDF driver that implements a WMI data provider.
</p>
<p>The sample demonstrates how to register the WMI providers and create provider instances for the Framework device object. It also illustrates how to handle the WMI queries sent to the device.</p>
<p>The <a href="gallery_samples.48_gallery#1">Firefly</a>, <a href="gallery_samples.32_gallery#1">
PCIDRV</a>, and <a href="gallery_samples.37_gallery#1">Toaster</a> sample drivers also implement WMI data providers.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff557565">Kernel-Mode Driver Framework</a>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
<h2><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>In Visual Studio, you can press F5 to build the sample and then deploy it to a target machine. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; </p>
<p class="note">You can obtain redistributable framework updates by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed. You can verify that the redistributables have been
 installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<p></p>
<h2><a id="testing"></a><a id="TESTING"></a>Testing </h2>
<p>To test the WmiSamp driver, run the generated WmiSamp.vbs script file. This will cause WMI to query all data blocks and properties, and put the result in a .log file. For more sophisticated testing, the VBScript can be extended by hand. The WBEMTest tool
 (located in %windir%\system32\wbem\) can also be used.</p>
<h2><a id="_______File_Manifest"></a><a id="_______file_manifest"></a><a id="_______FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>Makefile</p>
</td>
<td>
<p>Standard Windows NT makefile.</p>
</td>
</tr>
<tr>
<td>
<p>Makefile.inc </p>
</td>
<td>
<p>Includes instructions for generating the inf file and generating the header file from the mof file.</p>
</td>
</tr>
<tr>
<td>
<p>sources</p>
</td>
<td>
<p>Lists the files that need to be compiled.</p>
</td>
</tr>
<tr>
<td>
<p>WmiHandler.c</p>
</td>
<td>
<p>Handles the WMI queries for the classes defined in the mof file.</p>
</td>
</tr>
<tr>
<td>
<p>WmiSamp.c </p>
</td>
<td>
<p>Registers the WMI providers and creates provider instances for the Framework device object.</p>
</td>
</tr>
<tr>
<td>
<p>WmiSamp.h</p>
</td>
<td>
<p>Header file containing the structure and method declarations.</p>
</td>
</tr>
<tr>
<td>
<p>WmiSamp.inx </p>
</td>
<td>
<p>Used for generating the inf file.</p>
</td>
</tr>
<tr>
<td>
<p>WmiSamp.mof</p>
</td>
<td>
<p>Managed Object Format file that contains descriptions of the data blocks events and methods implemented by the driver.</p>
</td>
</tr>
<tr>
<td>
<p>WmiSamp.rc </p>
</td>
<td>
<p>Resource file containing version information.</p>
</td>
</tr>
</tbody>
</table>
<h2><a id="wmi_mof_check_tool"></a><a id="WMI_MOF_CHECK_TOOL"></a>WMI Mof Check Tool</h2>
<p>WmiMofCk validates that the classes, properties, methods and events specified in a binary mof file (.bmf) are valid for use with WMI. It also generates useful output files needed to build and test the WMI data provider.</p>
<ul>
<li>
<p>If the -h parameter is specified, a C language header file is created that defines the GUIDs, data structures, and method indices specified in the MOF file.</p>
</li><li>
<p>If the -t parameter is specified, a VBScript applet is created that will query all data blocks and properties specified in the .mof file. This can be useful for testing WMI data providers.</p>
</li><li>
<p>If the -x parameter is specified, a text file is created that contains the text representation of the binary .mof data. This can be included in the source of the driver if the driver supports reporting the binary .mof via a WMI query rather than a resource
 on the driver image file.</p>
</li><li>
<p>Usage: wmimofck -h&lt;C Header output file&gt; -x&lt;Hexdump output file&gt; -t&lt;VBScript test output file&gt; &lt;binary mof input file&gt;</p>
</li></ul>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp; A byproduct of compiling the .mof file is a .vbs file. This is a VBScript file that is run from the command line on the target machine running the new device driver. It will cause WMI to query all data blocks and properties,
 and put the results into a .log file. This can be very useful for testing WMI support in your driver. For more sophisticated testing, the VBScript can be extended by hand.</p>
<p></p>
</div>
