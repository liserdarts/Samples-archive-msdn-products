# Native Wifi IHV Service
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:23:18
## Description

<div id="mainSection">
<p>This sample code demonstrates IHV extensibility for Native WiFi. </p>
<p>In particular, this sample contains the following features:</p>
<ul>
<li>IHV profile validation </li><li>IHV discovery profile creation </li><li>IHV extension for interactive UI and Profile UI </li><li>802.1x extension </li></ul>
<p>The sample, after you compile and install it, enables you to connect to an Open WEP Network by using 802.1X through IHV Service extension.</p>
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
<p>The fully compiled sample consists of two DLLs: IHVSample.dll and IHVSampleUI.dll. The functions of those DLLs are outlined below:</p>
<h4><a id="IHVSample.dll"></a><a id="ihvsample.dll"></a><a id="IHVSAMPLE.DLL"></a>IHVSample.dll</h4>
<p>The IHVSample DLL supports connecting to a wireless network by using Open authentication and WEP encryption. The sample is capable of connecting to an 802.1X network and a non-802.1X network.</p>
<p>During the discovery phase, the sample generates a temporary profile that the operating system uses to establish a wireless connection. The operating system requests that the IHV provide a temporary profile to use when trying to connect through Discovery.
 In this case, IHVSample returns a list of usable profiles for connecting by using open-WEP with and without 802.1X. The RC4 algorithm is implemented in a DLL that is provided as part of the sample.</p>
<p>The sample is loaded by the IHV process and uses the public interfaces that are provided by the same process. The IHV process host initializes IHVSample in its process.</p>
<p>The sample gets called to perform pre-associate security and post-associate security. The sample does not implement any of the pre-associate security. For the post-associate security, in the case of open-WEP without 802.1X, IHVSample prompts for UI in case
 the profile does not exist or does not already have a valid key. In the case of 802.1X networks, the post-associate security portion sets the driver packet exemptions, starts up the Microsoft 802.1X module authentication, and waits for the result that indicates
 a success or a failure. During this period, IHVSample forwards all 802.1X packets to the IHV process. However, it caches the EAPOL key packets. In either case, after the key is obtained, IHVSample sends it to the driver and establishes the connection.</p>
<p>The operating system validates network profiles before they can be applied and persisted. The operating system performs validation of the non-IHV portion of the profile and passes the rest of data to IHVSample if IHV settings exist. IHVSample does limited
 XML schema validation.</p>
<h4><a id="UI_Sample__IHVSampleUI.dll_"></a><a id="ui_sample__ihvsampleui.dll_"></a><a id="UI_SAMPLE__IHVSAMPLEUI.DLL_"></a>UI Sample (IHVSampleUI.dll)</h4>
<p>The IHVSampleUI DLL extends the Wireless Profile UI to display IHV connectivity and security information. The security information that is displayed is for both security types based on IHV proprietary security and those based on Microsoft 802.1X. The sample
 adds custom IHV authentication and encryption types to illustrate the different ways the various types can be embedded in the UI. The configuration UI saves the IHV portion of the profile in accordance with the IHV schema. The sample pages that enable modification
 of the IHV parameters are displayed from the configuration UI. The sample also displays a wizard-based connection time UI with three sample pages. This connection time UI integrates in both the wizard and non-wizard flows.</p>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>After the sample is compiled, you must copy the binaries on to the target system and associate them with the matching Native Wifi-capable adapter. You can copy the binaries by adding an appropriate
<b>CopyFiles</b> directive in the <b>DDInstall</b> section in the INF file for installing the adapter. You can associate the binaries by adding an appropriate
<b>AddReg</b> directive in the <b>DDInstall</b> section in the INF file for installing the adapter.</p>
<h4><a id="CopyFiles_Directive"></a><a id="copyfiles_directive"></a><a id="COPYFILES_DIRECTIVE"></a>CopyFiles Directive</h4>
<p>The CopyFiles Directive should name a File-List-Section. The contents of this section should have the following:</p>
<p>IHVSpecifiedDLLName,,,2</p>
<p>IHVSpecifiedOtherFile,,,2</p>
<p>There should be an associated entry in the DestinationDirs section that specifies the destination to copy the file to. This section should have a directive like one of the following:</p>
<p>File-List-Section= 11 ; \system32 directory</p>
<p>DefaultDestDir= 11 ; \system32 directory</p>
<h4><a id="AddReg_Directive"></a><a id="addreg_directive"></a><a id="ADDREG_DIRECTIVE"></a>AddReg Directive</h4>
<p>The AddReg directive should name an Add-Registry-Section.</p>
<p>The contents of the Miniport INF file must include the following, in order for the correct IHV Service to be started:</p>
<ul>
<li>
<p>HKR,Ndi\IHVExtensions, ExtensibilityDLL,0,&quot;%SystemRoot%\system32\IhvExt.dll&quot;</p>
<p>This registry key is used to determine the location of the IHVSample.dll.</p>
</li><li>
<p>HKR,Ndi\IHVExtensions,UIExtensibilityCLSID,0, &quot;&lt;CLSID&gt;&quot;</p>
<p>This registry key is used to determine the class ID of the COM interface that extends the 802.11 configuration UI.</p>
</li><li>HKR,Ndi\IHVExtensions,GroupName,0, &quot;IHV provided group name&quot; </li><li>
<p>HKR,Ndi\IHVExtensions, AdapterOUI, 0x00010001, 0x00123456</p>
<p>This registry key is used to verify the OUI when the profile is applied to the adapter. If the AdapterOUI value is 0x??123456 in the registry, it needs to look like the following in the profile:</p>
<p>&lt;OUIHeader&gt;</p>
<p>&lt;OUI&gt;123456&lt;/OUI&gt;</p>
<p>&lt;type&gt;??&lt;/type&gt;</p>
<p>&lt;/OUHeader&gt;</p>
<p>Note that ?? stands for bits ignored.</p>
</li><li>HKR,Ndi\IHVExtensions, DiagnosticsID,0, &quot;&lt;Diagnostics ID GUID&gt;&quot; </li></ul>
<h4><a id="Uninstallation_Instructions"></a><a id="uninstallation_instructions"></a><a id="UNINSTALLATION_INSTRUCTIONS"></a>Uninstallation Instructions</h4>
<p>To uninstall this sample, you must undo the AddReg directive and undo the CopyFiles directive.</p>
<p>For more information about creating a Native Wi-Fi package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560690">
Native 802.11 Wireless LAN</a>.</p>
</div>