# Windows Radio Management Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:36
## Description

<div id="mainSection">
<p>The Radio Manager sample demonstrates how to structure a Radio Manager for use with the Windows&nbsp;8 Radio Management APIs.
</p>
<p>Starting with Windows&nbsp;8, the operating system contains a set of APIs which are used as a software mechanism to control the various radios found on the machine. The APIs work by communicating with a Radio Manager, which is a COM object that relays commands
 from the APIs to turn the radio on or off, and reports back radio information to the APIs. This feature is designed in such a way that a separate Radio Manager is required for each radio media type. For example, a WLAN radio will be controlled by a different
 Radio Manager than a GPS radio. If there are 2 WLAN radios and a GPS radio, the 2 WLAN radios will be controlled by one Radio Manager, and the GPS radio will be controlled by a different Radio Manager. The Radio Manager must be able to run correctly within
 Local Service Account context. Under this context, the Radio Manager will have the minimum privilege on the local computer.</p>
<p>When the user turns the radio off (either by using the specific radio software switch or the airplane mode switch), radio transmission must be turned off. The device can be powered off as long as the radio switch does not disappear from the UI. It is very
 important that the radio manager developer ensures that when the device is powered off, the radio switch does not disappear from the UI. If the radio switch disappears from the UI when the radio is turned off by the user, then user has no way to turn the radio
 back on! If it is desired to conserve power by cutting power to the device when the radio is turned off, but the device cannot be completely powered off because it disappears from the UI, then the solution would be to put the device in a low power state (e.g.
 D3). </p>
<p class="note"><b>Important</b>&nbsp;&nbsp;The radio manager MUST be given a name. This is the name of the radio switch that is displayed to the user in the Wireless page of PC Settings. The name must be simple, yet descriptive of what the radio is. For example, for
 NFC radios, the value of the name field should be &quot;NFC&quot;, and for GPS radios, the value of the name field should be &quot;GPS&quot; or &quot;GNSS&quot;, whichever is more appropriate. The name must not include the word &quot;radio&quot; or the manufacturer's name or some other word related
 to the functionality of the radio (e.g. &quot;Location&quot; OR &quot;port&quot;). </p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>None supported </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>For information on how to build a sample solution, including non-driver components, using Microsoft Visual Studio and the WDK build environment, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>The sample contains a script,<i> install.cmd</i>, which copies the radio manager DLL to the system directory, registers as a COM component, and configures the registry.</p>
<p>Copy the <i>install.cmd</i>, <i>SampleRM.reg</i> and <i>SampleRM.dll</i> files to a directory. Run
<i>install.cmd</i>.</p>
<h3><a id="Code_Tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code Tour</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>install.cmd</td>
<td>Installation script. Copies and registers the dll and executes SampleRM.reg.</td>
</tr>
<tr>
<td>SampleRM.reg</td>
<td>script to install the Sample Radio Manager into the registry, along with 2 radio instances.</td>
</tr>
<tr>
<td>SampleRM.sln</td>
<td>The Visual Studio solution file for building the Sample Radio Manager dll.</td>
</tr>
<tr>
<td>sampleRM.idl</td>
<td>The interface definition for the Sample Radio Manager.</td>
</tr>
<tr>
<td>RadioMgr.idl</td>
<td>The interface definition for a Windows Radio Manager.</td>
</tr>
<tr>
<td>SampleRadioManager.h</td>
<td>Header file for the functions required for a Radio Manager.</td>
</tr>
<tr>
<td>SampleRadioInstance.h</td>
<td>Header file for the functions required for a Radio Instance.</td>
</tr>
<tr>
<td>SampleInstanceCollection.h</td>
<td>Header file for the functions required for a Collection of Radio Instances.</td>
</tr>
<tr>
<td>precomp.h</td>
<td>Common header file.</td>
</tr>
<tr>
<td>InternalInterfaces.h</td>
<td>Header file for internal interface used for this sample.</td>
</tr>
<tr>
<td>dllmain.cpp</td>
<td>Standard dllmain.</td>
</tr>
<tr>
<td>SampleRadioManager.cpp</td>
<td>Implementation details for the Sample Radio Manager. Important concepts include:
<ul>
<li>Utilizing <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406534">
IMediaRadioManagerNotifySink</a> for radio instance events </li><li>Adding/Removing radio instances </li><li>Queuing and deploying worker jobs for system events </li></ul>
</td>
</tr>
<tr>
<td>SampleRadioInstance.cpp</td>
<td>Implementation details for the Sample Radio Instance. Important concepts include:
<ul>
<li>Accessors &amp; Modifiers for radio information </li><li>Instance change functions </li></ul>
</td>
</tr>
<tr>
<td>SampleInstanceCollection.cpp</td>
<td>Implementation details for the Sample Instance Collection. Important concepts include:l
<ul>
<li>Radio Instance discovery and retrieval </li></ul>
</td>
</tr>
<tr>
<td>RadioMgr_interface.cpp</td>
<td>Helper source file to include the MIDL-generated files.</td>
</tr>
</tbody>
</table>
<h3>Run the sample</h3>
<h3><a id="Operation"></a><a id="operation"></a><a id="OPERATION"></a>Operation</h3>
<p>This sample Radio Manager does not operate on an actual radio. Instead, it uses registry keys to act as virtual radios.
</p>
<p>Each &quot;radio&quot; instance can have the following values:</p>
<dl><dd>Name </dd><dd>RadioState </dd><dd>PreviousRadioState </dd><dd>IsMultiComm </dd><dd>IsAssociatingDevice </dd></dl>
<p>*** It is required that the registry key has AT LEAST a Name value, otherwise the Sample Radio Manager will fail to initialize. ***
</p>
<p class="note"><b>Important</b>&nbsp;&nbsp;The radio manager must be given a name and the registry key must have, as a minimum, a Name value. Otherwise, the Sample Radio Manager will fail to initialize. This is the name of the radio switch that is displayed to the
 user in the Wireless page of PC Settings. The name must be simple, yet descriptive of what the radio is. For example, for NFC radios, the value of the name field should be &quot;NFC&quot;, and for GPS radios, the value of the name field should be &quot;GPS&quot; or &quot;GNSS&quot;, whichever
 is more appropriate. The name must not include the word &quot;radio&quot; or the manufacturer's name or some other word related to the functionality of the radio (e.g. &quot;Location&quot; OR &quot;port&quot;).</p>
<p>When the Radio Manager is initialized, it uses these registry keys to retrieve the &quot;radio&quot; information. The radio state values can be any of the following enum values:</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>C&#43;&#43;</th>
</tr>
<tr>
<td>
<pre>typedef enum _DEVICE_RADIO_STATE
{
    DRS_RADIO_ON                    = 0,
    DRS_SW_RADIO_OFF                = 1,
    DRS_HW_RADIO_OFF                = 2,
    DRS_SW_HW_RADIO_OFF             = 3,
    DRS_HW_RADIO_ON_UNCONTROLLABLE  = 4,
    DRS_RADIO_INVALID               = 5,
    DRS_HW_RADIO_OFF_UNCONTROLLABLE = 6,
    DRS_RADIO_MAX                   = DRS_HW_RADIO_OFF_UNCONTROLLABLE
} DEVICE_RADIO_STATE;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>For IsMultiComm and IsAssociatingDevice, a value of 0 means 'no' and 1 means 'yes'.
</p>
<h3><a id="Adding_and_Setting_a_Radio_Instance"></a><a id="adding_and_setting_a_radio_instance"></a><a id="ADDING_AND_SETTING_A_RADIO_INSTANCE"></a>Adding and Setting a Radio Instance</h3>
<p>To add a new radio instance, add a new instance key to the registry key like the following entry:</p>
<pre class="syntax"><code>[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\RadioManagement\Misc\SampleRadioManager\SampleRadioX]
&quot;RadioState&quot;=dword:00000000
&quot;Name&quot;=&quot;SampleRadioX&quot;
&quot;IsMultiComm&quot;=dword:00000000</code></pre>
<h3><a id="Editing_a_Radio_Instance"></a><a id="editing_a_radio_instance"></a><a id="EDITING_A_RADIO_INSTANCE"></a>Editing a Radio Instance</h3>
<p>Simply change the values in the registry. For example, change the radio state from DRS_RADIO_ON to DRS_SW_RADIO_OFF by changing the 'RadioState' value from 0 to 1.
</p>
<h3><a id="Removing_a_Radio_Instance"></a><a id="removing_a_radio_instance"></a><a id="REMOVING_A_RADIO_INSTANCE"></a>Removing a Radio Instance</h3>
<p>Delete the corresponding registry key.</p>
</div>
