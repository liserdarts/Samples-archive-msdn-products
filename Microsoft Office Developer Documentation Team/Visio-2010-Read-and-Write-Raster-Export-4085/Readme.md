# Visio 2010: Read and Write Raster Export Resolution Settings
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
* Visio 2010
## Topics
* Office 2010 101 code samples
* raster file format
## IsPublished
* True
## ModifiedDate
* 2011-08-05 04:34:02
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates methods to read and write Raster Export Resolution settings used by Microsoft Visio 2010 when exporting documents to non-Visio raster file formats.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Visio 2010 has two new methods to read and write Raster Export Resolution settings used by Visio when exporting documents to non-Visio raster file formats.<br>
<br>
To run this demo, open Visio 2010 and create a blank document. Open the VBA editor and paste this code into the existing&nbsp; ThisDocument module. With the cursor inside the ReadAndWriteRasterExportResolution method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ReadAndWriteRasterExportResolution()
    ' Access the Visio ApplicationSettings object.
    Dim appSettings As Visio.ApplicationSettings
    Set appSettings = Application.Settings
   
    ' Write out current Raster Export Resolution settings.
    Debug.Print &quot;*** Current Settings&quot;
    WriteOutCurrentExportResolution appSettings
    
    ' Use the new SetRasterExportResolution method
    ' to change the current Raster Export Resolution settings
    ' to a custom setting of 800x600 using Pixels per Centimeter as the
    ' units of measurement.
    appSettings.SetRasterExportResolution _
        visRasterUseCustomResolution, 800, 600, visRasterPixelsPerCm
    
    ' Write out current Raster Export Resolution settings
    ' after changing the values to a custom setting.
    Debug.Print &quot;*** Settings after changing&quot;
    WriteOutCurrentExportResolution appSettings
    
    ' Reset the Raster Export Resolution settings
    ' to the default (the screen).
    appSettings.SetRasterExportResolution visRasterUseScreenResolution
    
    ' Write out current settings after changing
    ' the values back to the default.
    Debug.Print &quot;*** Settings after resetting&quot;
    WriteOutCurrentExportResolution appSettings
End Sub

Private Sub WriteOutCurrentExportResolution(appSettings As Visio.ApplicationSettings)
    Dim res As VisRasterExportResolution
    Dim width As Double
    Dim height As Double
    Dim units As VisRasterExportResolutionUnits
    
    ' GetRasterExportResolution is a new method to
    ' let you access the Raster Export Resolution data.
    appSettings.GetRasterExportResolution res, width, height, units
    
    ' Figure out what value was returned for
    ' VisRasterExportResolution and write out
    ' the information to the Immediate Window.
    Select Case res
        Case VisRasterExportResolution.visRasterUseScreenResolution
            Debug.Print &quot;RasterExportResolution: visRasterUseScreenResolution&quot;
        Case VisRasterExportResolution.visRasterUsePrinterResolution
            Debug.Print &quot;RasterExportResolution: visRasterUseScreenResolution&quot;
        Case VisRasterExportResolution.visRasterUseSourceResolution
            Debug.Print &quot;RasterExportResolution: visRasterUseSourceResolution&quot;
        Case VisRasterExportResolution.visRasterUseCustomResolution
            Debug.Print &quot;Export Resolution: visRasterUseCustomResolution&quot;
            ' Only if the VisRasterExportResolution value is visRasterUseCustomResolution
            ' will the other values return something other than null.
            Debug.Print &quot;Width: &quot; &amp; width
            Debug.Print &quot;Height: &quot; &amp; height
            
            Select Case units
                Case VisRasterExportResolutionUnits.visRasterPixelsPerInch
                    Debug.Print &quot;Export Resolution Units: visRasterPixelsPerInch&quot;
                Case VisRasterExportResolutionUnits.visRasterPixelsPerCm
                    Debug.Print &quot;Export Resolution Units: visRasterPixelsPerCm&quot;
            End Select
    End Select
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ReadAndWriteRasterExportResolution()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Access&nbsp;the&nbsp;Visio&nbsp;ApplicationSettings&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;appSettings&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.ApplicationSettings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;appSettings&nbsp;=&nbsp;Application.Settings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Write&nbsp;out&nbsp;current&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;settings.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;***&nbsp;Current&nbsp;Settings&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WriteOutCurrentExportResolution&nbsp;appSettings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;the&nbsp;new&nbsp;SetRasterExportResolution&nbsp;method</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;change&nbsp;the&nbsp;current&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;settings</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;a&nbsp;custom&nbsp;setting&nbsp;of&nbsp;800x600&nbsp;using&nbsp;Pixels&nbsp;per&nbsp;Centimeter&nbsp;as&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;units&nbsp;of&nbsp;measurement.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appSettings.SetRasterExportResolution&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;visRasterUseCustomResolution,&nbsp;<span class="visualBasic__number">800</span>,&nbsp;<span class="visualBasic__number">600</span>,&nbsp;visRasterPixelsPerCm&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Write&nbsp;out&nbsp;current&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;settings</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;after&nbsp;changing&nbsp;the&nbsp;values&nbsp;to&nbsp;a&nbsp;custom&nbsp;setting.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;***&nbsp;Settings&nbsp;after&nbsp;changing&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WriteOutCurrentExportResolution&nbsp;appSettings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reset&nbsp;the&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;settings</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;the&nbsp;default&nbsp;(the&nbsp;screen).</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appSettings.SetRasterExportResolution&nbsp;visRasterUseScreenResolution&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Write&nbsp;out&nbsp;current&nbsp;settings&nbsp;after&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;values&nbsp;back&nbsp;to&nbsp;the&nbsp;default.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;***&nbsp;Settings&nbsp;after&nbsp;resetting&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WriteOutCurrentExportResolution&nbsp;appSettings&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;WriteOutCurrentExportResolution(appSettings&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.ApplicationSettings)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;res&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;VisRasterExportResolution&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;width&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;height&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;units&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;VisRasterExportResolutionUnits&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;GetRasterExportResolution&nbsp;is&nbsp;a&nbsp;new&nbsp;method&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;let&nbsp;you&nbsp;access&nbsp;the&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appSettings.GetRasterExportResolution&nbsp;res,&nbsp;width,&nbsp;height,&nbsp;units&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Figure&nbsp;out&nbsp;what&nbsp;value&nbsp;was&nbsp;returned&nbsp;for</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;VisRasterExportResolution&nbsp;and&nbsp;write&nbsp;out</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;information&nbsp;to&nbsp;the&nbsp;Immediate&nbsp;Window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;res&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportResolution.visRasterUseScreenResolution&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;RasterExportResolution:&nbsp;visRasterUseScreenResolution&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportResolution.visRasterUsePrinterResolution&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;RasterExportResolution:&nbsp;visRasterUseScreenResolution&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportResolution.visRasterUseSourceResolution&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;RasterExportResolution:&nbsp;visRasterUseSourceResolution&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportResolution.visRasterUseCustomResolution&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Resolution:&nbsp;visRasterUseCustomResolution&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Only&nbsp;if&nbsp;the&nbsp;VisRasterExportResolution&nbsp;value&nbsp;is&nbsp;visRasterUseCustomResolution</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;will&nbsp;the&nbsp;other&nbsp;values&nbsp;return&nbsp;something&nbsp;other&nbsp;than&nbsp;null.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Width:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;width&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Height:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;height&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;units&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportResolutionUnits.visRasterPixelsPerInch&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Resolution&nbsp;Units:&nbsp;visRasterPixelsPerInch&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportResolutionUnits.visRasterPixelsPerCm&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Resolution&nbsp;Units:&nbsp;visRasterPixelsPerCm&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26206" href="/site/view/file/26206/1/Visio.ReadAndWriteRasterExportResolution.txt">Visio.ReadAndWriteRasterExportResolution.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26207" href="/site/view/file/26207/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905478">Visio Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
