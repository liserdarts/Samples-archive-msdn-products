# AVStream simulated hardware sample driver (Avshws)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Streaming Media
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:47
## Description

<div id="mainSection">
<p>The AVStream simulated hardware sample driver (Avshws) provides a pin-centric <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554240">
AVStream</a> capture driver for a simulated piece of hardware. This streaming media driver performs video captures at 320 x 240 pixels in either RGB24 or YUV422 format using direct memory access (DMA) into capture buffers. The purpose of the sample is to demonstrate
 how to write a pin-centric AVStream minidriver. The sample also shows how to implement DMA by using the related functionality provided by the AVStream class driver.
</p>
<p>This sample features enhanced parameter validation and overflow detection.</p>
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
<h2><a id="Install_the_sample_files"></a><a id="install_the_sample_files"></a><a id="INSTALL_THE_SAMPLE_FILES"></a>Install the sample files</h2>
<p>To install the sample files in preparation for building and running the sample, follow these steps:</p>
<ol>
<li>Click the blue <b>C&#43;&#43;</b> Download button above. </li><li>The files will be downloaded to a temporary folder. Move them to a convenient location on your hard drive.
</li><li>Unzip the files by right-clicking the folder, and then select <b>Extract All…</b> and select another folder location.
</li></ol>
<h2><a id="Provision_a_target_computer_"></a><a id="provision_a_target_computer_"></a><a id="PROVISION_A_TARGET_COMPUTER_"></a>Provision a target computer
</h2>
<p>After you’ve installed the sample on your host computer, run Visual Studio&nbsp;2013, and from the
<b>File</b> menu, select <b>Open</b>, then <b>Project/Solution…</b>, navigate to the directory where you’ve copied the Avshws sample, then to the C&#43;&#43; folder, and select
<b>avshws.vcxproj</b> (the VC&#43;&#43; Project).</p>
<p>In the <b>Solution Explorer</b> pane in Visual Studio, at the top is <b>Solution ‘avshws’</b>. Right-click this and select
<b>Configuration Manager</b>. Follow the instructions in <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver with the WDK</a> to set the platform, operating system, and debug configuration you want to use, and to build the sample. This sample project will automatically sign the driver package.</p>
<p>Provision your target computer using instructions in, for example, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265573">
Preparing a Computer for Provisioning (WDK 8.1)</a>. Ensure that in the <b>Network and Sharing Center</b> control panel your target computer has
<b>Network Discovery</b> and <b>File and Printer Sharing</b> enabled.</p>
<h2><a id="Deploy_the_driver_to_the_target_computer"></a><a id="deploy_the_driver_to_the_target_computer"></a><a id="DEPLOY_THE_DRIVER_TO_THE_TARGET_COMPUTER"></a>Deploy the driver to the target computer</h2>
<p>Now you can deploy the Avshws driver that you’ve just built to the target computer, using guidance in
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>. Specifically, find the package file under the
<b>Package</b> folder in the Avshws solution. Right-click <b>package</b> and select
<b>Properties</b>. Under Configuration Properties, click <b>Driver install</b> and then
<b>Deployment</b>. Here you must click the check box for <b>Enable deployment</b>, and then click the button to the right of
<b>&lt;Configure Computer…&gt;</b>. In the next dialog you enter the <b>Target Computer Name</b> and can let the host computer automatically provision the target computer and set up debugger options.</p>
<p>Finally, in Visual Studio&nbsp;2013, from the <b>Build</b> menu select <b>Deploy Solution</b> to deploy the sample to the target computer. On the target computer, you can see the deployed package in the
<b>%Systemdrive%\drivertest\drivers</b> folder.</p>
<h2><a id="Install_the_driver"></a><a id="install_the_driver"></a><a id="INSTALL_THE_DRIVER"></a>Install the driver</h2>
<p>On the target computer, open Device Manager, and follow these steps:</p>
<ol>
<li>In the <b>Action</b> menu, click <b>Add Legacy Hardware</b>, and the <b>Add Hardware Wizard</b> appears. Click
<b>Next</b> and then <b>Next</b> again. </li><li>In the <b>Add Hardware</b> window, select <b>Show All Devices</b>. </li><li>In the <b>Manufacturer</b> list in the left pane, click <b>Microsoft</b>. </li><li>You should see the <b>AVStream Simulated Hardware Sample</b> in the <b>Model</b> pane on the right. Click this and then click
<b>Next</b>. </li><li>Click <b>Next</b> again to install the driver, and then click <b>Finish</b> to exit the wizard.
</li></ol>
<p>The sample driver now appears in the Device Manager console tree under <b>Sound, video and game controllers</b>. The Avshws INF file will be on the system drive at, for example,
<b>…windows\System32\DriverStore\FileRepository\</b>.</p>
<h2><a id="Sample_code_hierarchy"></a><a id="sample_code_hierarchy"></a><a id="SAMPLE_CODE_HIERARCHY"></a>Sample code hierarchy</h2>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff558717"><b>DriverEntry</b></a> in Device.cpp is the initial point of entry into the driver. This routine passes control to AVStream by calling the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562683"><b>KsInitializeDriver</b></a> function. In this call, the minidriver passes the device descriptor, an AVStream structure that recursively defines the AVStream object hierarchy for a
 driver. This is common behavior for an AVStream minidriver.</p>
<p>At device start time, a simulated piece of capture hardware is created (the <b>
CHardwareSimulation</b> class), and a DMA adapter is acquired from the operating system and is registered with AVStream by calling the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff561687"><b>KsDeviceRegisterAdapterObject</b></a> function. This call is required for a sample that performs DMA access directly into the capture buffers, instead of using DMA access to write
 to a common buffer. The driver creates the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff567644">
KS Filter</a> for this device dynamically by calling the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff561650">
<b>KsCreateFilterFactory</b></a> function.</p>
<p>Filter.cpp is where the sample lays out the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff563534">
<b>KSPIN_DESCRIPTOR_EX</b></a> structure for the single video pin. In addition, a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562554"><b>KSFILTER_DISPATCH</b></a> structure and a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562553"><b>KSFILTER_DESCRIPTOR</b></a> structure are provided in this source file. The filter dispatch provides only a create dispatch, a routine that is included in Filter.cpp. The process
 dispatch is provided on the pin because this is a pin-centric sample.</p>
<p>Capture.cpp contains source for the video capture pin on the capture filter. This is where the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff563535"><b>KSPIN_DISPATCH</b></a> structure for the unique pin is provided. This dispatch structure specifies a
<i>Process</i> callback routine, also defined in this source file. This routine is where stream pointer manipulation and cloning occurs.</p>
<p>The process callback is one of two routines of interest in Capture.cpp that demonstrate how to perform DMA transfers with AVStream functionality. The other is the
<b>CCapturePin::CompleteMappings</b> method. These two methods show how to use the queue, obtain clone pointers, use scatter/gather lists, and perform other DMA-related tasks.</p>
<p>For more information, see the comments in all .cpp files.</p>
<h2>Run the sample</h2>
<p>Follow these steps to see how the sample driver functions:</p>
<ol>
<li>After installation has completed, access the driver through the Graphedt tool. Graphedt.exe is available in the
<i>tools</i> directory of the WDK. </li><li>Before running GraphEdit, use the regsvr32 utility to register the proppage.dll DLL and to enable GraphEdit to display property pages for some of the built-in Microsoft DirectShow filters. Open an elevated command window with Administrator privileges, and
 navigate to the WDK or SDK <i>tools</i> directory that contains proppage.dll. </li><li>On the command line, type regsvr32 proppage.dll. If the registration succeeds, you’ll get a message, “DllRegisterServer in proppage.dll succeeded.” Click OK.
</li><li>In the Graphedt tool, click the <b>Graph</b> menu and click <b>Insert Filters</b>. The sample appears under &quot;WDM Streaming Capture Devices&quot; as &quot;avshws Source.&quot;
</li><li>Click <b>Insert Filter</b>. The sample appears in the graph as a single filter labeled, &quot;avshws Source.&quot; There is one output pin, which is the video capture pin. This pin emits video in YUY2 format.
</li><li>Attach this filter to either a DirectShow Video Renderer or to the VMR default video renderer. Then click
<b>Play</b>. </li></ol>
<p></p>
<p>The output that is produced by the sample is a 320 x 240 pixel image of standard EIA-189-A color bars. In the middle of the image near the bottom, a clock appears over the image. This clock displays the elapsed time since the graph was introduced into the
 run state following the last stop. The clock display format is MINUTES:SECONDS.HUNDREDTHS.</p>
<p>In the upper-left corner of the image, a counter counts the number of frames that have been dropped since the graph was introduced into the run state after the last stop.</p>
<h2><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h2>
<p><b>File manifest</b> </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Avshws.h</td>
<td>
<p>Main header file for the sample</p>
</td>
</tr>
<tr>
<td>Avshws.inf </td>
<td>
<p>Sample installation file</p>
</td>
</tr>
<tr>
<td>Avshws.rc </td>
<td>
<p>Resource file, mainly to provide version information</p>
</td>
</tr>
<tr>
<td>Capture.cpp</td>
<td>
<p>Pin-level code for the capture pin and DMA handling</p>
</td>
</tr>
<tr>
<td>Capture.h </td>
<td>
<p>Header file for Capture.cpp</p>
</td>
</tr>
<tr>
<td>Device.cpp </td>
<td>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff558717"><b>DriverEntry</b></a>, Plug and Play (PnP) handling, initialization, and device level code</p>
</td>
</tr>
<tr>
<td>Device.h </td>
<td>
<p>Header file for Device.cpp</p>
</td>
</tr>
<tr>
<td>Filter.cpp </td>
<td>
<p>Filter level code for the capture filter</p>
</td>
</tr>
<tr>
<td>Filter.h </td>
<td>
<p>Header file for Filter.cpp</p>
</td>
</tr>
<tr>
<td>Hwsim.cpp </td>
<td>
<p>Hardware simulation code that fills scatter/gather mappings and performs other tasks</p>
</td>
</tr>
<tr>
<td>Hwsim.h </td>
<td>
<p>Header file for Hwsim.cpp</p>
</td>
</tr>
<tr>
<td>Image.cpp</td>
<td>
<p>RGB24 and UYVY image synthesis and overlay code</p>
</td>
</tr>
<tr>
<td>Image.h </td>
<td>
<p>Header file for Image.cpp</p>
</td>
</tr>
<tr>
<td>Sources </td>
<td>
<p>Generic file for building the code sample</p>
</td>
</tr>
</tbody>
</table>
</div>
