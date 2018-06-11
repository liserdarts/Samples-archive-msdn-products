# Windows Biometric Driver Samples (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* Biometrics
* Devices and sensors
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:24:20
## Description

<div id="mainSection">
<p>The Windows Biometric Driver Samples contain the Windows Biometric Driver Interface sample and the Windows Biometric Service Adapter samples.
</p>
<p>The following table describes the samples contained in this sample set:</p>
<table>
<tbody>
<tr>
<th>Sample</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>Windows Biometric Driver Interface</p>
</td>
<td>
<p>This sample implements the Windows Biometric Driver Interface (WBDI). It contains skeleton code for handling the mandatory IOCTLs necessary to interoperate with the Windows Biometric Framework. A WBDI driver can be deployed in conjunction with an engine
 adapter DLL to allow a sensor to be exposed from the Windows Biometric Framework. This sample has been written to make use of the UMDF framework, which allows for ease of development and system stability.</p>
</td>
</tr>
<tr>
<td>
<p>Windows Biometric Service Adapters</p>
</td>
<td>
<p>These samples provide skeleton code that developers can use as a basis for writing Sensor, Engine, and Storage Adapters for the Windows Biometric Service. Note that the stubs in these samples are non-functional, and Adapter writers will need to follow the
 programming guidelines in the WinBio Service documentation in order produce a working Adapter component.</p>
</td>
</tr>
</tbody>
</table>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
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
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<h3><a id="Windows_Biometric_Driver_Interface"></a><a id="windows_biometric_driver_interface"></a><a id="WINDOWS_BIOMETRIC_DRIVER_INTERFACE"></a>Windows Biometric Driver Interface</h3>
<p>The sample requires the use of a suitable fingerprint sensor. It does not capture real data, but it does create a biometric unit in the Windows Biometric Framework.</p>
<h3><a id="Windows_Biometric_Service_Adapters"></a><a id="windows_biometric_service_adapters"></a><a id="WINDOWS_BIOMETRIC_SERVICE_ADAPTERS"></a>Windows Biometric Service Adapters</h3>
<p>To write and test an Adapter plug-in, it will be necessary to have a biometric device and a working WBDI driver for the device.</p>
<p>Adapters are generally installed along with the WBDI driver for the corresponding device. Consult the WinBio Service documentation for information on the INF file commands used for installing Adapters. Note that Adapters are trusted plug-in components, so
 they can only be installed using a privileged account.</p>
<h2><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h2>
<h3><a id="Windows_Biometric_Driver_Interface"></a><a id="windows_biometric_driver_interface"></a><a id="WINDOWS_BIOMETRIC_DRIVER_INTERFACE"></a>Windows Biometric Driver Interface</h3>
<p>This sample is taken from the UMDF FX2 sample and has been modified to expose WBDI. It has the necessary hooks to make this a WBDI driver:</p>
<ul>
<li>Installs WBDI driver, including correct class GUID settings and icons, and registry settings for Windows Biometric Framework configuration.
</li><li>Publishes WBDI device interface. </li><li>Supports all the mandatory <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536414">
WBDI IOCTLs</a>. </li><li>Supports cancellation. </li><li>Can be opened with exclusivity. </li></ul>
<p>All of these things are required for the Windows Biometric Framework service to recognize this device as a biometric device and set up a Biometric Unit. It allows the service to properly control the device.</p>
<p>The sample makes use of ATL support for simplified handling of COM objects with UMDF.
</p>
<p>The driver makes use of a parallel queue so that multiple requests can be outstanding at once.
</p>
<p>It uses device level-locking to simplify internal thread synchronization. This means that only one framework callback can be active at a time.</p>
<p>It supports cancellation of any IOCTL which may be I/O intensive, particularly a capture IOCTL. This sample does not have a real capture mechanism, so it is simulated by a 5 second delay returning a capture IOCTL. Cancellation is supported through the mechanism
 exposed by WUDF, with a callback for a request object. Cancellation support is required for all IOCTLs.</p>
<p>There are hooks for all <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536414">
WBDI IOCTLs</a>, including the optional IOCTLs.</p>
<p>PnP is very simple for this driver. It needs to only implement OnPrepareHardware and OnReleaseHardware from IPnpCallbackHardware.</p>
<p>Some device drivers may need to keep several pending reads to the WinUsb I/O target in order to properly flush all I/O that comes from the device during a capture.</p>
<h3><a id="Windows_Biometric_Service_Adapters"></a><a id="windows_biometric_service_adapters"></a><a id="WINDOWS_BIOMETRIC_SERVICE_ADAPTERS"></a>Windows Biometric Service Adapters</h3>
<p>WinBio Adapters are plug-in components that provide a standard interface layer between the Windows Biometric Service and a biometric device. The WinBio Service recognizes three types of Adapters:</p>
<ul>
<li>Sensor Adapters - expose the sample-capture capabilities of the biometric device.
</li><li>Engine Adapters - expose the sample manipulation, template generation, and matching capabilities of the device.
</li><li>Storage Adapters - expose the template storage and retrieval capabilities of the device.
</li></ul>
<p>For many simple biometric devices, it will only be necessary to write a WBDI driver for the device plus an Engine Adapter to perform matching operations. Consult the programming guidelines in the WinBio Service documentation for more details.</p>
<p>Each Adapter sample contains a well-known interface-discovery function, whose job is to return the address of a function dispatch table. When the WinBio Service loads an Adapter plug-in, it uses the interface-discovery function to locate the dispatch table,
 and then calls various methods in the table to communicate with the biometric device. The purpose, arguments, and return codes of each Adapter method are described in the WinBio Service programming guidelines. More information on adapter plug-ins is available
 at <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd401553(v=vs.85).aspx">
WBDI Plug-in Reference</a>.</p>
</div>
