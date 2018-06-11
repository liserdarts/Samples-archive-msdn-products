# NDIS Virtual Miniport Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* ndis
* Windows Driver
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:18:57
## Description

<div id="mainSection">
<p>The NDIS Virtual Miniport Driver sample illustrates the functionality of a deserialized NDIS miniport driver without requiring a physical network adapter.
</p>
<p>Because the driver does not interact with any hardware, it makes it very easy to understand the miniport interface and the usage of various NDIS functions without the clutter of hardware-specific code that is normally found in a fully functional driver.
 The driver can be installed either manually using the Add Hardware wizard as a root enumerated virtual miniport driver or on a virtual bus (like toaster bus).
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample provides an example of minimal driver intended for education purposes. The driver and its sample test programs are not intended for use in a production environment.
</p>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3>Run the sample</h3>
<p>This sample demonstrates a deserialized NDIS miniport driver. If a single instance of the virtual miniport exists, it simply drops the send packets and completes the send operation successfully. If there are multiple virtual miniports, it indicates the incoming
 send packets to the other instances after doing checks.</p>
<p>To test the miniport driver, install more than one instance of the miniport driver. You can repeat the earlier installation steps to install more than one instance of the miniport. Then, install NDIS Tester from the WHQL Web site and run all Client tests.</p>
<p>You can test the IOCTL interface that is provided by the miniport by using the testapp.exe application. testapp is a PnP-friendly application, because it registers for PnP notification on the NDIS device and closes the handles at the right time to enable
 the device from being removed. Please note the PnP manager vetoes any request to disable or uninstall the device and displays a dialog box to restart the machine if there are any open handles to the device.</p>
<p>The device object that is registered by the miniport by using <code>NdisMRegsiterDevice</code> is a stand-alone control device object and not part of the actual miniport's PnP device stack. When the device is removed, disabled, or uninstalled, the PnP notifications
 are sent only to the interface that is registered on the PnP stack and not to the control device object. As a result, if an application opens an handle to the control device object, it also needs to have another handle open to the PnP interface that is registered
 by NDIS by using <b>GUID_NDIS_LAN_CLASS</b> to listen for PnP device-change notification.</p>
<p>The GUI application illustrates how to do this. First, the application registers for interface-change notification on
<b>GUID_NDIS_LAN_CLASS</b>. When it gets notified about an existing device-interface, it opens an handle to the device and registers for device-change notification by using that handle. This registration is required to get notified about any kind of PnP activity
 on the target device. Now, you can optionally open the control device object and send IOCTLs to it. When the application gets notified about device removal, it not only closes the handle to the NDIS device but also to the control device so that the PNP manager
 can remove the device.</p>
<h3><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Sources</td>
<td>Generic file for building the code sample.</td>
</tr>
<tr>
<td>netVmini.inf</td>
<td>INF file for installing the driver.</td>
</tr>
<tr>
<td>netVmini.RC</td>
<td>Resource file to specify driver version, and so on.</td>
</tr>
<tr>
<td>miniport.h</td>
<td>Include file for defining structures, constants, and function prototypes.</td>
</tr>
<tr>
<td>miniport.c</td>
<td>Main file that contains driver entry and other miniport functions.</td>
</tr>
<tr>
<td>init.c</td>
<td>Source file for allocating and initializing resources.</td>
</tr>
<tr>
<td>sendrcv.c</td>
<td>Source file for handling send and packets.</td>
</tr>
<tr>
<td>ioctl.c</td>
<td>Source file for handling IOCTLs</td>
</tr>
<tr>
<td>request.c</td>
<td>Source file for handling Set and Query Information requests</td>
</tr>
<tr>
<td>public.h</td>
<td>Include file for defining IOCTLs</td>
</tr>
</tbody>
</table>
<p>For more information on creating NDIS Miniport Drivers, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff565949">
NDIS Miniport Drivers</a>.</p>
</div>
