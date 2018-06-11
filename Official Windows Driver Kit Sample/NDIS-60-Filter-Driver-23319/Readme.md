# NDIS 6.0 Filter Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* ndis
* Windows Driver
## Topics
* Networking
* ndis
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:15:24
## Description

<div id="mainSection">
<p>The Ndislwf sample is currently a do-nothing pass-through NDIS 6 filter driver that demonstrates the basic principles underlying an NDIS 6.0 Filter driver. The sample is a replacement of NDIS 5 Sample Intermediate Driver (Passthru driver).
</p>
<p>Although the filter driver is a Modifying filter driver, the filter driver currently doesn’t modify any packets and it only re-packages and sends down all OID requests. You may easily update this filter driver to change packets before passing them along.
 Or you may use the filter to originate new packets to send or receive. For example, the filter could encrypt/compress outgoing and decrypt/decompress incoming data.</p>
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
<h3><a id="INSTALLING_THE_SAMPLE"></a><a id="installing_the_sample"></a>INSTALLING THE SAMPLE</h3>
<p>Ndislwf is installed as a service (called <b>NDIS Sample LightWeight Filter</b> in the supplied INF). To install, follow the steps below:</p>
<ol>
<li>Prepare a an installation directory that contains these files: netlwf.inf and ndislwf.sys.
</li><li>On the desktop, click <b>Start</b>, then open <b>Control Panel</b>, then open
<b>Network and Internet Connections</b>, then open <b>Network Connections</b>, then right-click on the relevant Local Area Connection icon and choose
<b>Properties</b>. </li><li>Click Install, then Service, then Add, then Have Disk. </li><li>Browse to the drive/directory containing the files listed above. ClickOK. This should show “NDIS Sample LightWeight Filter” in a list of Network Services. Highlight this and click OK. This should install the Ndislwf filter driver.
</li><li>Click OK or Yes each time if the system prompts with a warning regarding installation of unsigned files. This is necessary because binaries generated via the LDK build environment are not signed.
</li></ol>
<h3><a id="Basic_steps_in_attaching_and_detaching_of_Ndislwf_driver_"></a><a id="basic_steps_in_attaching_and_detaching_of_ndislwf_driver_"></a><a id="BASIC_STEPS_IN_ATTACHING_AND_DETACHING_OF_NDISLWF_DRIVER_"></a>Basic steps in attaching and detaching
 of Ndislwf driver:</h3>
<ol>
<li>During <code>DriverEntry</code>, the ndislwf driver registers as a NDIS 6 filter driver.
</li><li>Later on, NDIS calls Ndislwf <code>FilterAttach</code> handler, for each underlying NDIS adapter on which it is configured to attach.
</li><li>In the context of FilterAttach Handler, filter driver call <code>NdisFAttribute</code> to register its filter module context with NDIS. After that, filter driver can read its own setting in registry by calling
<code>NdisOpenConfigurationEx</code>, and calls <code>NdisXXX</code> functions. </li><li>After FilterAttach successfully returns, NDIS restarts the filter later by calling
<code>FilterRestart</code> handler. FilterRestart should prepare to handle send/receive data. After restart return successfully, filter driver should be able to process send/receive.
</li><li>All requests and sends coming from overlying drivers for the Ndislwf filter driver are repackaged if necessary and sent down to NDIS, to be passed to the underlying NDIS driver.
</li><li>All indications arriving from an underlying NDIS driver are forwarded up by Ndislwf filter driver.
</li><li>NDIS calls <code>FilterPause</code> handler when NDIS needs to detach the filter from the stack or there is some configuration changes in the stack. In processing the pause request from NDIS, the Ndislwf driver waits for all its own outstanding requests
 to be completed before it completes the pause request. </li><li>NDIS calls the Ndislwf driver’s <code>FilterDetach</code> entry point when NDIS needs to detach a filter module from NDIS stack. FilterDetach handler should free all the memory allocation done in FilterAttach, and undo the operations it did in FilterAttach
 Handler. </li></ol>
<h3><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>makefile</td>
<td>Used during compilation to create the object and sys files</td>
</tr>
<tr>
<td>filter.c</td>
<td>Filter driver entry points and related functions in the Ndislwf filter driver</td>
</tr>
<tr>
<td>netlwf.inf</td>
<td>Installation INF for the service</td>
</tr>
<tr>
<td>device.c</td>
<td>Virtual device related routines such as registering a device and handling IOCTLs</td>
</tr>
<tr>
<td>filter.h</td>
<td>Prototypes of all functions and data structures used by the Ndislwf driver</td>
</tr>
<tr>
<td>filter.htm</td>
<td>Documentation for the filter driver (this file)</td>
</tr>
<tr>
<td>filter.rc</td>
<td>Resource file for the Ndislwf driver</td>
</tr>
<tr>
<td>precomp.h</td>
<td>Precompile header file</td>
</tr>
<tr>
<td>flt_dbg.c</td>
<td>Debug-related code</td>
</tr>
<tr>
<td>flt_dbg.h</td>
<td>Debug code definitions and structures</td>
</tr>
<tr>
<td>sources</td>
<td>List of source files that are compiled and linked to create the ndislwf driver.</td>
</tr>
</tbody>
</table>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff565492">
NDIS Filter Drivers</a> in the network devices design guide.</p>
</div>
