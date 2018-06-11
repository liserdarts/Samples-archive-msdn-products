# Kernel mode display-only miniport  driver (KMDOD) sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* display
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:07
## Description

<div id="mainSection">
<p>The kernel mode display-only miniport driver (KMDOD) sample implements most of the device driver interfaces (DDIs) that a display-only miniport driver should provide to the Windows Display Driver Model (WDDM). The code is useful to understand how to write
 a miniport driver for a display-only device, or how to develop a full WDDM driver.
</p>
<p>For more info on how a KMDOD works, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/jj673962">
Kernel Mode Display-Only Driver (KMDOD) Interface</a>. For more info on WDDM drivers, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff570593">Windows Vista Display Driver Model (WDDM) Design Guide</a>.</p>
<p>This code can also help you to understand the use and implementation of display-related DDIs. The INF file shows how to make a display miniport driver visible to other WDDM components.</p>
<p>The sample can be installed on top of a VESA-capable graphics adapter, or on top of a graphics device that supports access to frame buffer memory through the Unified Extensible Firmware Interface (UEFI).</p>
<p>The sample driver does not support the <i>sleep</i> power state. If it is placed in the sleep state, the driver will cause a system bugcheck to occur. There is no workaround available, by design.</p>
<p>If the current display driver is not a WDDM 1.2 compliant driver, the sample driver might fail to install, with error code 43 displayed. The KMDOD driver is actually installed, but it cannot be started. The workaround for this issue is to switch to the Microsoft
 Basic Display Adapter Driver before installing the KMDOD sample driver, or simply to reboot your system after installing the KMDOD sample.</p>
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
<p>For info on how to build a driver solution using Microsoft Visual Studio, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>In Microsoft Visual Studio, press <b>F5</b> to build the sample and then deploy it to a target machine. For more info, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>.</p>
<p>In some cases you might need to install the driver manually, as follows.</p>
<ol>
<li>Add the following files to the directory given by …\[x64]\C&#43;&#43;\Package:
<ul>
<li>SampleDriver.cat </li><li>SampleDriver.inf </li><li>SampleDriver.sys </li><li>SampleDriver.cer </li></ul>
</li><li>Unless you’ve provided a production certificate, you should manually install the SampleDriver.cer digital certificate with the following command:
<p><code>Certutil.exe -addstore root SampleDriver.cer</code></p>
</li><li>Then enable test signing by running the following BCDEdit command:
<p><code>Bcdedit.exe -set TESTSIGNING ON</code></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;After you change the TESTSIGNING boot configuration option, restart the computer for the change to take effect.</p>
<p>For more info, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff553484">
The TESTSIGNING Boot Configuration Option</a>.</p>
</li><li>Manually install the driver using Device Manager, which is available from Control Panel.
</li></ol>
<h3><a id="ACPI-based_GPUs"></a><a id="acpi-based_gpus"></a><a id="ACPI-BASED_GPUS"></a>ACPI-based GPUs</h3>
<p>To install the KMDOD sample driver on a GPU that is an Advanced Configuration and Power Interface (ACPI) device, add these lines to the
<code>[MS]</code>, <code>[MS.NTamd64]</code>, and <code>[MS.NTarm]</code> sections of the Sampledisplay.inf file:</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>Text</th>
</tr>
<tr>
<td>
<pre>&quot; Kernel mode display only sample driver &quot; = KDODSamp_Inst, ACPI\CLS_0003&amp;SUBCLS_0000
&quot; Kernel mode display only sample driver &quot; = KDODSamp_Inst, ACPI\CLS_0003&amp;SUBCLS_0001
&quot; Kernel mode display only sample driver &quot; = KDODSamp_Inst, ACPI\CLS_0003&amp;SUBCLS_0003</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>This new code provides generic identifiers for ACPI hardware.</p>
<p>You can optionally delete the original lines of code within these sections of the INF file.</p>
</div>
