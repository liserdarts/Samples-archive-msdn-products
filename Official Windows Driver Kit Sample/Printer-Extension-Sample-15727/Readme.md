# Printer Extension Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* True
## ModifiedDate
* 2014-07-28 02:19:23
## Description

<div id="mainSection">
<p>This sample demonstrates how to use .NET to build a customized, desktop UI for a v4 print driver. This .NET app uses PrintTicket, PrintCapabilities and Bidi in order to communicate with the print system and is suitable for inclusion in a v4 print driver.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample is for the v4 print driver model.</p>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/hh464103(v=vs.85).aspx">v4 Print Driver Interfaces and Enumerations</a>
</dt></dl>
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
<p>To build, run and test this sample, you must have a v4 print driver and a print queue already installed. The sample should be saved to c:\apps.</p>
<p>Perform the following steps to build the sample.</p>
<p><b>Add your PrinterDriverID to the PrinterExtensionSample</b> </p>
<dl><dd>1. Find your PrinterDriverID
<dl><dd>a. Open your PowerShell script editor. </dd><dd>b. Copy the following script and paste it into your PowerShell script editor.
<div class="code"><span>
<table>
<tbody>
<tr>
<th>cmd</th>
</tr>
<tr>
<td>
<pre>$outputColl = new-object System.Collections.ArrayList

foreach ($printer in Get-Printer) { 
    $output = New-Object PSObject
    $driver = Get-PrinterDriver $printer.DriverName
    $manifest = $driver.DependentFiles | Where-Object -FilterScript:{$_ -like &quot;*manifest.ini&quot;}
    if ($manifest -ne $null) {
        $driverID = (Get-Content $manifest | Where-Object -FilterScript:{$_ -like &quot;PrinterDriverId*&quot;} -ErrorAction Ignore).Split(&quot;=&quot;)[1]
    
        $output | Add-Member PrinterName $printer.Name 
        $output | Add-Member DriverName $printer.DriverName
        $output | Add-Member PrinterDriverID $driverID 
        $null = $outputColl.Add($output); #$null suppresses a count that is returned
    }

}
Write-Output $outputColl | Format-Table
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</dd><dd>c. Save the script, and make sure that the file extension is &quot;.ps1&quot;. </dd><dd>d. Open PowerShell, and navigate to the folder where you just saved your PowerShell script.
</dd><dd>e. Type &quot;./&lt;filename&gt;&quot; to run the PowerShell script. For example, if you chose &quot;printerid.ps1&quot; as the filename for your script, then type the following command and press Enter to run the script:
<dl><dd>./printerid.ps1 </dd></dl>
</dd><dd>f. Find your printer in the table that is displayed, and copy the corresponding GUID value, including both curly braces {}.
</dd></dl>
</dd><dd>2. Add the PrinterDriverID to the printer extension
<dl><dd>a. Open Microsoft Visual Studio. </dd><dd>b. In <b>Solution Explorer</b>, expand PrinterExtensionSample, then right click on App.xaml. Choose View Code.
</dd><dd>c. Scroll to the bottom of the file, and edit the variable PrinterDriverID. Select everything inside the double-quotes and paste.
</dd></dl>
</dd><dd>3. Build the project
<dl><dd>a. In <b>Solution Explorer</b>, right click on the PrinterExtensionSample solution and choose
<b>Build Solution</b>. </dd></dl>
</dd></dl>
<p><b>Register your printer extension</b> </p>
<dl><dd>
<p>1. Open Notepad</p>
</dd><dd>
<p>2. Type the following:</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>cmd</th>
</tr>
<tr>
<td>
<pre>Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\OfflinePrinterExtensions]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\OfflinePrinterExtensions\{PrinterExtensionId}]
&quot;AppPath&quot;=&quot;c:\\apps\\Printer extension sample\\C#\\ExtensionSample\\bin\\Debug\\PrinterExtensionSample.exe&quot;

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\OfflinePrinterExtensions\{PrinterExtensionId}\{PrinterDriverId}]

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\OfflinePrinterExtensions\{PrinterExtensionId}\{PrinterDriverId}\{EC8F261F-267C-469F-B5D6-3933023C29CC}]
@=&quot;1&quot;
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</dd><dd>
<p>3. Click <b>Edit</b> &gt; <b>Replace</b>.</p>
</dd><dd>
<p>4. In the <b>Find</b> textbox type {PrinterDriverId}, and in the <b>Replace</b> textbox type the PrinterDriverID value that you found using Powershell.</p>
</dd><dd>
<p>5. Open Visual Studio, click <b>Tools</b> &gt; <b>Create Guid</b>. Select <b>Registry Format</b> &gt;
<b>Copy</b>.</p>
</dd><dd>
<p>6. Go back to Notepad and click <b>Edit</b> &gt; <b>Replace</b>. Find {PrinterExtensionId} and replace it with the GUID you just created.</p>
</dd><dd>
<p>7. Click <b>File</b> &gt; <b>Save</b>, and Save this file in C:\Apps\PrinterExtensionSample. Name the file register.reg. Change the file type to All files.</p>
</dd><dd>
<p>8. Open a Windows Explorer window and navigate to C:\apps\PrinterExtensionSample.</p>
</dd><dd>
<p>9. Double-click the register.reg file and click yes on any prompts.</p>
</dd></dl>
<p>For information on how to build a driver solution using Visual Studio, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p></p>
<dl><dd>
<p>1. Open an app that supports printing, for example Notepad. Then click <b>File</b> &gt;
<b>Print</b>.</p>
</dd><dd>
<p>2. In the print window, select your print queue, and then click <b>Preferences</b>. This should launch your PrinterExtensionSample app.</p>
</dd></dl>
<p></p>
<p>If your app is not launched, the registration request may have failed, or the request may not have been processed in time. Try launching the app again, and if it continues to fail, confirm that the PrintNotify service is running.</p>
<p><b>Debugging the sample</b> </p>
<p>In order to debug your sample at startup, it is useful to create a MessageBox in the beginning of the OnDriverEvent method of App.xaml.cs. Once your app is running and the MessageBox is visible, you can attach to it using Visual Studio.</p>
<dl><dd>1. Debug your sample using Visual Studio.
<dl><dd>a. In Visual Studio, click <b>Debug</b> &gt; <b>Attach to Process</b>. </dd><dd>b. Find PrinterExtensionSample.exe in the list, then select <b>Attach</b>. </dd><dd>c. Set any relevant breakpoints, then click <b>OK</b> in the MessageBox to continue.
</dd></dl>
</dd></dl>
<p></p>
</div>
