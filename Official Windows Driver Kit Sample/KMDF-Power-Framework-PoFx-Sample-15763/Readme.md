# KMDF Power Framework (PoFx) Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* Power Management
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:52:14
## Description

<div id="mainSection">
<p>This solution consists of two samples that demonstrate how a KMDF driver can implement F-state-based power management. The SingleComp sample demonstrates how a KMDF driver can implement F-state-based power management for a device that has only a single component.
 The MultiComp sample demonstrates how a KMDF driver can implement F-state-based power management for a device that has an arbitrary number of components that can be individually power-managed.
</p>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh451017">Supporting Functional Power States</a>
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
<h2><a id="SingleComp_Overview"></a><a id="singlecomp_overview"></a><a id="SINGLECOMP_OVERVIEW"></a>SingleComp Overview</h2>
<p>This sample demonstrates how a KMDF driver can implement F-state-based power management for a device that has only a single component.</p>
<p>The sample illustrates the use of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh451097">
<b>WdfDeviceWdmAssignPowerFrameworkSettings</b></a> method to specify power framework settings for the single component that represents the entire device. The power framework settings that can be specified include the F-states for the component and the power
 framework callbacks that are invoked when the component's active/idle condition or its F-state changes.</p>
<p>The sample also illustrates the use of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545903">
<b>WdfDeviceAssignS0IdleSettings</b></a> method to instruct KMDF to begin power-management of the device (and the component that represents the entire device).</p>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>The driver can be installed on a root-enumerated device using the devcon.exe tool.</p>
<ol>
<li>Obtain the devcon.exe tool from the WDK </li><li>Copy the driver binary, INF file and the KMDF coinstaller to a directory on your test machine.
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">You can obtain redistributable framework updates by downloading the Wdfcoinstaller.msi package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed.
 You can verify that the redistributables have been installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<p></p>
</li><li>Run the command &quot;devcon.exe install SingleComponentFStateSample.inf root\SingleComponentFStateDevice&quot;.
</li></ol>
<p>Use the PowerFxApp.exe application to send I/O requests to the driver. Running the command &quot;PowerFxApp.exe /?&quot; displays detailed usage information.</p>
<p>For detailed information about implementing F-state-based power management for a single component device, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh451032">Supporting Multiple Functional Power States for Single-Component Devices</a>.</p>
<h2><a id="MultiComp_Overview"></a><a id="multicomp_overview"></a><a id="MULTICOMP_OVERVIEW"></a>MultiComp Overview</h2>
<p>This sample demonstrates how a KMDF driver can implement F-state-based power management for a device that has an arbitrary number of components that can be individually power-managed.</p>
<p>The sample driver statically links to a helper library (WdfPoFx.lib) that encapsulates all of the generic code to interact with the power framework. The device-specific code is implemented in the driver itself, outside of the helper library. The idea behind
 this organizing the code in this manner is make the helper library reusable by other drivers. The directory structure for the sample is as follows:</p>
<ul>
<li>The helper library is implemented in the 'lib' subdirectory. </li><li>The interface between the helper library and the rest of the driver code is defined in the 'inc' subdirectory.
</li><li>The driver is implemented in the 'driver' subdirectory. </li></ul>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>The driver can be installed on a root-enumerated device using the devcon.exe tool.</p>
<ol>
<li>Obtain the devcon.exe tool from the WDK </li><li>Copy the driver binary, INF file and the KMDF coinstaller to a directory on your test machine.
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading theWdfcoinstaller.msi package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>Run the command &quot;devcon.exe install WdfMultiComp.inf WDF\WdfMultiComp&quot;. </li></ol>
<h2><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p>Use the PowerFxApp.exe application to send I/O requests to the driver. Running the command &quot;PowerFxApp.exe /?&quot; displays detailed usage information.</p>
<h2><a id="Design_overview"></a><a id="design_overview"></a><a id="DESIGN_OVERVIEW"></a>Design overview</h2>
<p>The driver controls a device that has more than one component. It needs to access one of those components for processing each I/O request that it receives. The specific component that it needs to access depends on the I/O request that it receives.</p>
<p>In order to support this, the driver creates one top-level, power-managed queue to receive all its requests. It also creates one secondary, power-managed queue for each of its components. These secondary queues are called component queues. This is shown
 in the diagram below.</p>
<img src="/windowshardware/site/view/file/112062/1/image.png" alt="" align="middle">
<p>When the driver's dispatch routine for the top-level queue is invoked, it examines the request to determine which component it needs to access in order to process the request. Then, it forwards the request to the component queue for the component that it
 needs to access for that request. When the driver's dispatch routine for the component queue is invoked, it accesses the component hardware to process the request.</p>
<p>The driver's top-level queue and component queues are all power-managed so KMDF ensures that the device is in D0 while the queues are in a dispatching state. The key point to note is that the driver is designed to maintain a component queue in a dispatching
 state only when the component is active. In order to achieve this, the driver stops the component queue when the component becomes idle and starts the component queue when the component becomes active. (To be precise, this mechanism of stopping and starting
 queues is encapsulated in the power framework helper library used by the driver). Thus, the driver is able to ensure that when the component queue is in a dispatching state, not only is the device in D0 but the component corresponding to that queue is also
 active. Thus it is safe to access the component hardware when the component queue's dispatch routine is invoked.</p>
<h2><a id="Implementation_notes"></a><a id="implementation_notes"></a><a id="IMPLEMENTATION_NOTES"></a>Implementation notes</h2>
<p>The driver uses the power framework helper library to manage most of its interactions with the power framework. In order to achieve this, during device initialization, the driver performs the following tasks in its
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff541693"><i>EvtDriverDeviceAdd</i></a> callback.</p>
<ul>
<li>Enables the helper library to register its own KMDF callbacks for PNP and power-management of the device.
</li><li>Provides the helper library with power-framework-related information about the device.
</li><li>Provides the helper library with information about the component queues. </li></ul>
<p>During I/O request processing, the driver uses routines provided by helper library to forward requests to component queues and also to complete requests.</p>
<p>The main tasks performed by the power framework helper library on behalf of the driver are:</p>
<ul>
<li>Registration and unregistration with the power framework. </li><li>Stopping component queues when the corresponding components become idle and starting them when the corresponding components become active.
</li><li>Notifying the power framework when the device returns to its working state (D0) in response to the system returning from a low-power state to the working state (S0).
</li></ul>
<p>The power framework helper library does not have any hardware-specific information, so any tasks that are specific to the device's hardware are performed by the driver. In this sample, the device hardware is represented by a very simple simulation. The notable
 hardware-specific tasks in this sample are:</p>
<ul>
<li>Accessing component hardware to process I/O requests. </li><li>Accessing component hardware to change the component's F-state. </li></ul>
<p>As mentioned earlier, the hardware access shown is this sample is entirely simulated in software. This sample does not work with a real device – it installs on a root-enumerated software device.</p>
<h2><a id="S0-idle_power_management_support"></a><a id="s0-idle_power_management_support"></a><a id="S0-IDLE_POWER_MANAGEMENT_SUPPORT"></a>S0-idle power management support</h2>
<p>The power framework helper library implements support for S0-idle power management for the device. Note that this is different from the component power management support that is enabled by the power framework. Component power management enables individual
 components of the device to be power-managed by putting them in different F-states while the device is in a working state (D0). S0-idle power management for the device enables the device as a whole to be power-managed by putting it into different D-states
 while the system is in a working state (S0).</p>
<p>The code to implement S0-idle power management support for the device is conditionally compiled based on the value of the PFH_S0IDLE_SUPPORTED compiler switch. If the switch is set to a nonzero value, the code to implement S0-idle power management support
 is included. If set to zero, the code is omitted thereby resulting in a smaller binary size. Thus, a driver that requires S0-idle power management for the device can use the power framework helper library's support for it but a driver that does not require
 it can reduce its binary size by omitting the code that is specific to S0-idle power management.</p>
<h2><a id="Additional_Information"></a><a id="additional_information"></a><a id="ADDITIONAL_INFORMATION"></a>Additional Information</h2>
<p>For detailed information about implementing F-state-based power management for a multiple component device, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh451028">Supporting Multiple Functional Power States for Multiple-Component Devices</a>.</p>
</div>
