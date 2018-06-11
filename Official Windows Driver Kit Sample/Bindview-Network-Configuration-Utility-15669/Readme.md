# Bindview Network Configuration Utility
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* ndis
* Windows Driver
## Topics
* Networking
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:53
## Description

<div id="mainSection">
<p>The Bindview sample demonstrates how to use INetCfg APIs to enumerate, install, uninstall, bind and unbind network components.
</p>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p>Type <b>bindview.exe</b> at the command prompt to run the program. You can perform the following operations:</p>
<ul>
<li>Install a network protocol, service or client component. </li><li>Uninstall a network protocol, service or client component. </li><li>
<p>By clicking the right mouse button on a network protocol, service or client, you can perform the following operations:
</p>
<ul>
<li>Bind the network component to another component. </li><li>Unbind the network component from another component that is bound to it. </li></ul>
</li><li>
<p>By clicking the right mouse button on a binding path, you can perform the following operations.
</p>
<ul>
<li>Disable the binding path if it is enabled. </li><li>Enable the binding path if it is disabled. </li></ul>
</li><li>Save the binding information to a file. </li></ul>
<h2><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>BINDVIEW.CPP</td>
<td>Contains WinMain and dialog box related functions.</td>
</tr>
<tr>
<td>NetCfgAPI.cpp </td>
<td>Contains INetCfg functions.</td>
</tr>
<tr>
<td>BINDING.CPP </td>
<td>Contains binding path related functions. </td>
</tr>
<tr>
<td>Component.cpp</td>
<td>Contains network component related functions.</td>
</tr>
<tr>
<td>RESOURCE.H </td>
<td>Resource header.</td>
</tr>
<tr>
<td>BINDVIEW.H </td>
<td>Contains function prototypes.</td>
</tr>
<tr>
<td>NetCfgAPI.h </td>
<td>Contains function prototypes for NetCfgAPI.cpp</td>
</tr>
<tr>
<td>BindView.rc </td>
<td>Resources for Bindview</td>
</tr>
<tr>
<td>BindView.ico </td>
<td>Icon for the sample.</td>
</tr>
</tbody>
</table>
<p>For more information on the INetCfg interface, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559080">
Network Configuration Interfaces</a>.</p>
</div>
