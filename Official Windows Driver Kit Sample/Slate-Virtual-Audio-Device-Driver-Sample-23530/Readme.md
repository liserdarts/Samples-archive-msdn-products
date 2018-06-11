# Slate Virtual Audio Device Driver Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Audio
## IsPublished
* True
## ModifiedDate
* 2014-09-03 06:19:22
## Description

<div id="mainSection">
<p>The Microsoft Slate Virtual Audio Device Driver (SYSVAD) shows how to develop a WDM audio driver that exposes support for multiple audio devices.
</p>
<p>Some of these audio devices are embedded in the system (for example, speakers, microphone arrays) while others are pluggable (like headphones, speakers, microphones, Bluetooth headsets etc.). The driver uses WaveRT and audio offloading for rendering devices.
 The driver uses a &quot;virtual audio device&quot; instead of an actual hardware-based adapter, and highlights the different aspects of the audio offloading WDM audio driver architecture.
</p>
<p>Driver developers can use the framework in this sample to provide support for various audio devices without concern for hardware dependencies. The framework includes implementations of the following interfaces:</p>
<ul>
<li>
<p>The CAdapterCommon interface gives the miniports access to virtual mixer hardware. It also implements the
<b>IAdapterPowerManagement</b> interface.</p>
</li><li>
<p>The CMiniportTopologyMSVAD interface is the base class for all sample topologies. It has very basic common functions. In addition, this class contains common topology property handlers.</p>
</li></ul>
<p></p>
<p>The following table shows the features that are implemented in the various subdirectories of this sample.
<table>
<tbody>
<tr>
<th>Subdirectory </th>
<th>Description </th>
</tr>
<tr>
<td>SlateAudioSample </td>
<td>A slate system with multiple embedded and external audio devices.</td>
</tr>
<tr>
<td>SwapAPO</td>
<td>Files for exploring the audio processing objects feature.</td>
</tr>
</tbody>
</table>
</p>
<p>For more information about the Windows audio engine, see <a href="http://msdn.microsoft.com/en-us/windows/hardware/br259116">
Exposing Hardware-Offloaded Audio Processing in Windows</a>, and note that audio hardware that is offload-capable replicates the architecture that is presented in the diagram shown in the topic.</p>
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
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>If you simply want to Build this sample driver and don't intend to run or test it, then you do not need a target computer (also called a test computer). If, however, you would like to deploy, run and test this sample driver, then you need a second computer
 that will server as your target computer. Instructions are provided in the <b>Run the sample</b> section to show you how to set up the target computer - also referred to as
<i>provisioning</i> a target computer.</p>
<p>Perform the following steps to build this sample driver.</p>
<p><b>1. Donwload and extract the sample</b></p>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click
<i>Microsoft slate system virtual audio device driver sample.zip</i>, and choose <b>
Extract All</b>. Specify a new folder, or browse to an existing one that will store the extracted files. For example, you could specify
<i>c:\SlateAudioSample</i> as the new folder into which the files will be extracted.</p>
<p><b>2. Open the driver solution in Visual Studio</b></p>
<p>In Microsoft Visual Studio, Click <b>File</b> &gt; <b>Open</b> &gt; <b>Project/Solution...</b> and navigate to the folder that contains the extracted files (for example,
<i>c:\SlateAudioSample</i>). Double-click the <i>sysvad</i> solution file.</p>
<p>In Visual Studio locate the Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has four (4) projects. Note that the project titled SwapAPO is actually a folder that contains two projects - APO and PropPageExtensions.</p>
<p><b>3. Set the sample's configuration and platform</b></p>
<p>In Solution Explorer, right-click <b>Solution 'sysvad' (4 projects)</b>, and choose
<b>Configuration Manager</b>. Make sure that the configuration and platform settings are the same for the four projects. By default, the configuration is set to &quot;Win8.1 Debug&quot;, and the platform is set to &quot;Win32&quot; for all the projects. If you make any configuration
 and/or platform changes for one project, you must make the same changes for the remaining three projects.</p>
<p>Here are the explanations for some of the configuration and platform options.</p>
<table>
<tbody>
<tr>
<th>Configuration</th>
<th>Platform</th>
<th>Description</th>
</tr>
<tr>
<td>Win8.1 Debug</td>
<td>x64</td>
<td>The driver will run on an x64 hardware platform that is running Windows 8.1. The driver will not run on any earlier versions of Windows.</td>
</tr>
<tr>
<td>Win7 Debug</td>
<td>x64</td>
<td>The driver will run on an x64 hardware platform that is running Windows 7 or a later version of Windows. The driver will not run on any earlier versions of Windows.</td>
</tr>
</tbody>
</table>
<p><b>4. Build the sample using Visual Studio</b></p>
<p>In Visual Studio, click <b>Build</b> &gt; <b>Build Solution</b>.</p>
<p><b>5. Locate the built driver package</b></p>
<p>In File Explorer, navigate to the folder that contains the extracted files for the sample. For example, you would navigate to
<i>c:\SlateAudioSample</i>, if that's the folder you specified in the preceding Step 1.</p>
<p>In the folder, the location of the driver package varies depending on the configuration and platform settings that you selected in the
<b>Configuration Manager</b>. For example, if you left the default settings unchanged, then the built driver package will be saved to a folder named
<i>Win8.1Debug</i> inside the same folder as the extracted files. Double-click the folder for the built driver package, and then double-click the folder named
<i>package</i>.</p>
<p>The package should contain these files:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>PropPageExt.dll</td>
<td>A sample driver extension for a property page.</td>
</tr>
<tr>
<td>SlateAudioSample.sys</td>
<td>The driver file.</td>
</tr>
<tr>
<td>SwapAPO.dll</td>
<td>A sample driver extension for a UI to manage APOs.</td>
</tr>
<tr>
<td>sysvad.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
<tr>
<td>sysvad.inf</td>
<td>An information (INF) file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>WdfCoinstaller01011.dll</td>
<td>The coinstaller for version 1.xx of KMDF.</td>
</tr>
</tbody>
</table>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from the computer on which you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying</i> the driver. You can deploy the sample driver, SlateAudioSample, automatically or manually.</p>
<p><b>Automatic deployment</b></p>
<p>Before you automatically deploy a driver, you must provision the target computer. Verify that the target computer has an ethernet cable connecting it to your local network, and that your host and target computers can ping each other. Then perform the following
 steps to prepare your host and target computers.</p>
<p><b>1. Provision the target computer</b></p>
<p>On the target computer install the latest <a href="http://msdn.microsoft.com/en-us/windows/hardware/gg454513.aspx">
Windows Driver Kit</a> (WDK), and then when the installation is completed, navigate to the following folder:</p>
<dl><dd>\Program Files (x86)\Windows Kits\8.1\Remote\&lt;architecture&gt;\ </dd></dl>
<p>For example, if your target computer is an x64 machine, you would navigate to:</p>
<dl><dd>\Program Files (x86)\Windows Kits\8.1\Remote\x64\ </dd></dl>
<p>Double-click the &quot;<i>WDK Test Target Setup x64-x64_en-us.msi</i>&quot; file to run it. This program prepares the target computer for provisioning.</p>
<p>On the host computer, in Visual Studio click <b>Driver</b> &gt; <b>Test</b> &gt;
<b>Configure Computers...</b>, and then click <b>Add a new computer</b>.</p>
<p>Type the name of the target computer, select <b>Provision computer and choose debugger settings</b>, and click
<b>Next</b>. In the next window, verify that the <b>Connection Type</b> is set to Network. Leave the other (default) settings as they are, and click
<b>Next</b>. For more information about the settings in this window, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450944(v=vs.85).aspx">
Getting Set Up for Debugging</a>.</p>
<p><b>2. Prepare the host computer</b></p>
<p>If you haven't already done so, then preform the steps in the <b>Build the sample</b> section, to build the sample driver.</p>
<p>In Visual Studio, in Solution Explorer, right click <b>package</b> (lower case), and choose
<b>Properties</b>. Navigate to <b>Configuration Properties</b> &gt; <b>Driver Install</b> &gt;
<b>Deployment</b>.</p>
<p>Check , <b>Enable deployment</b> and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Hardware ID Driver Update</b>, and enter <i>*SYSVAD_SLATEAUDIO</i> for the hardware ID. Click
<b>OK</b>. </p>
<p>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>. This will deploy the sample driver to your target computer.</p>
<p>On the target computer, perform the steps in the <b>Test the sample</b> section to test the sample driver.</p>
<p><b>Manual deployment</b></p>
<p>Before you manually deploy a driver, you must prepare the target computer by turning on test signing and by installing a certificate. You also need to locate the DevCon tool in your WDK installation. After that you’re ready to run the built driver sample.</p>
<p><b>1. Prepare the target computer</b></p>
<p>Open a Command Prompt window as Administrator. Then enter the following command:</p>
<dl><dd><b>bcdedit /set TESTSIGNING ON</b> </dd></dl>
<p>Reboot the target computer. Then navigate to the Tools folder in your WDK installation and locate the DevCon tool. For example, look in the following folder:</p>
<dl><dd><i>C:\Program Files (x86)\Windows Kits\8.1\Tools\x64\devcon.exe</i> </dd></dl>
<p>Copy <i>devcon.exe</i> to a folder on the target computer where it is easier to find. For example, create a
<i>C:\Tools</i> folder and copy <i>devcon.exe</i> to that folder.</p>
<p>Create a folder on the target for the built driver package (for example, <i>C:\SysvadDriver</i>). Copy all the files from the built driver package on the host computer and save them to the folder that you created on the target computer.</p>
<p>Create a folder on the target computer for the certificate created by the build process. For example, you could create a folder named
<i>C:\Certificates</i> on the target computer, and then copy <i>package.cer</i> to it from the host computer. You can find this certificate in the same folder on the host computer, as the
<i>package</i> folder that contains the built driver files. On the target computer, right-click the certificate file, and click
<b>Install</b>, then follow the prompts to install the test certificate.</p>
<p>If you need more detailed instructions for setting up the target computer, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571(v=vs.85).aspx">
Preparing a Computer for Manual Driver Deployment</a>.</p>
<p><b>2. Install the driver</b></p>
<p>The SlateAudioSample driver package contains a sample driver and 2 driver extension samples. The following instructions show you how to install and test the sample driver. Here's the general syntax for the devcon tool that you will use to install the driver:</p>
<dl><dd><i>devcon install &lt;INF file&gt; &lt;hardware ID&gt;</i> </dd></dl>
<p>The INF file required for installing this driver is <i>sysvad.inf</i>. Here's how to find the hardware ID for installing the
<i>SlateAudioSample.sys</i> sample: On the target computer, navigate to the folder that contains the files for your driver (for example,
<i>C:\SysvadDriver</i>). Then right-click the INF file (<i>sysvad.inf</i>) and open it with Notepad. Use Ctrl&#43;F to find the [MicrosoftDS] section. Note that there is a comma-separated element at the end of the row. The element after the comma shows the hardware
 ID. So for this sample, the hardware ID is *SYSVAD_SLATEAUDIO.</p>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter the following command:</p>
<dl><dd><b>devcon install sysvad.inf *SYSVAD_SLATEAUDIO</b> </dd></dl>
<p>If you get an error message about <i>devcon</i> not being recognized, try adding the path to the
<i>devcon</i> tool. For example, if you copied it to a folder called <i>C:\Tools</i>, then try using the following command:</p>
<dl><dd><b>c:\tools\devcon install sysvad.inf *SYSVAD_SLATEAUDIO</b> </dd></dl>
<p>For more detailed instructions, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272(v=vs.85).aspx">
Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<p>After successfully installing the sample driver, you're now ready to test it.</p>
<p><b>Test the driver</b></p>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <i>Microsoft Virtual Audio Device (WDM) - Slate Sample</i>. This is typically under the
<b>Sound, video and game controllers</b> node.</p>
<p>On the target computer, open Control Panel and navigate to <b>Hardware and Sound</b> &gt;
<b>Manage audio devices</b>. In the Sound dialog box, select the speaker icon labeled as
<i>Microsoft Virtual Audio Device (WDM) - Slate Sample</i>, then click <b>Set Default</b>, but do not click
<b>OK</b>. This will keep the Sound dialog box open.</p>
<p>Locate an MP3 or other audio file on the target computer and double-click to play it. Then in the Sound dialog box, verify that there is activity in the volume level indicator associated with the
<i>Microsoft Virtual Audio Device (WDM) - Slate Sample</i> driver.</p>
</div>
