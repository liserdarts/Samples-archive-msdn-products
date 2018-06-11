# Bindview Network Configuration Utility
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
* 2013-06-25 10:13:56
## Description

<div id="mainSection">
<p>The Bindview sample demonstrates how to use INetCfg APIs to enumerate, install, uninstall, bind and unbind network components.
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
<h3><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h3>
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
