# Visio 2010: Manipulate Raster Export Resolution Settings
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
* 2011-08-05 04:37:43
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>SetRasterExportSize
</strong>method and the <strong>GetRasterExportSize </strong>method to manipulate the Raster Export Resolution settings in Microsoft Visio 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Visio 2010 has two new methods to read and write Raster Export Size settings used by Visio when exporting documents to non-Visio raster file formats.<br>
<br>
To run this demo, open Visio 2010 and create a blank document. Open the VBA editor and paste this code into the existing&nbsp; ThisDocument module. With the cursor inside the ReadAndWriteRasterExportSize method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ReadAndWriteRasterExportSize()
    ' Access the Visio ApplicationSettings object.
    Dim appSettings As Visio.ApplicationSettings
    Set appSettings = Application.Settings
   
    ' Write out current Raster Export Resolution settings.
    Debug.Print &quot;*** Current Settings&quot;
    WriteOutCurrentExportSize appSettings
    
    ' Use the new SetRasterExportSize method
    ' to change the current Raster Export Resolution settings
    ' to a custom setting of a 4x5 inch document size.
    appSettings.SetRasterExportSize visRasterFitToCustomSize, 4, 5, visRasterInch
    
    ' Write out the Raster Export Size settings
    ' after changing the values to a custom setting.
    Debug.Print &quot;*** Settings after changing&quot;
    WriteOutCurrentExportSize appSettings
    
    ' Reset the Raster Export Size settings
    ' to the default (the source).
    appSettings.SetRasterExportSize visRasterFitToSourceSize
    
    ' Write out current settings after changing
    ' the values back to the default.
    Debug.Print &quot;*** Settings after resetting&quot;
    WriteOutCurrentExportSize appSettings
End Sub

Private Sub WriteOutCurrentExportSize(appSettings As Visio.ApplicationSettings)
    Dim expSize As VisRasterExportSize
    Dim width As Double
    Dim height As Double
    Dim units As VisRasterExportSizeUnits
    
    ' GetRasterExportSize is a new method to
    ' let you access the Raster Export Size data.
    appSettings.GetRasterExportSize expSize, width, height, units
    
    Debug.Print expSize
    Debug.Print width
    Debug.Print height
    Debug.Print units
    
    ' Figure out what value was returned for
    ' VisRasterExportSize and write out
    ' information to the Immediate Window.
   Select Case expSize
        Case VisRasterExportSize.visRasterFitToScreenSize
            Debug.Print &quot;Export Size: visRasterFitToScreenSize&quot;
        Case VisRasterExportSize.visRasterFitToPrinterSize
            Debug.Print &quot;Export Size: visRasterFitToPrinterSize&quot;
        Case VisRasterExportSize.visRasterFitToSourceSize
            Debug.Print &quot;Export Size: visRasterFitToSourceSize&quot;
        Case VisRasterExportSize.visRasterFitToCustomSize
            Debug.Print &quot;Export Size: visRasterFitToCustomSize&quot;
            ' Only if the VisRasterExportSize value is visRasterFitToCustomSize
            ' will the other values return something other than null.
            Debug.Print &quot;Width: &quot; &amp; width
            Debug.Print &quot;Height: &quot; &amp; height
            
            Select Case units
                Case VisRasterExportSizeUnits.visRasterInch
                    Debug.Print &quot;Export Size Units: visRasterInch&quot;
                Case VisRasterExportSizeUnits.visRasterCm
                    Debug.Print &quot;Export Size Units: visRasterCm&quot;
                Case VisRasterExportSizeUnits.visRasterPixel
                    Debug.Print &quot;Export Size Units: visRasterPixel&quot;
            End Select
    End Select
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ReadAndWriteRasterExportSize()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Access&nbsp;the&nbsp;Visio&nbsp;ApplicationSettings&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;appSettings&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.ApplicationSettings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;appSettings&nbsp;=&nbsp;Application.Settings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Write&nbsp;out&nbsp;current&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;settings.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;***&nbsp;Current&nbsp;Settings&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WriteOutCurrentExportSize&nbsp;appSettings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;the&nbsp;new&nbsp;SetRasterExportSize&nbsp;method</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;change&nbsp;the&nbsp;current&nbsp;Raster&nbsp;Export&nbsp;Resolution&nbsp;settings</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;a&nbsp;custom&nbsp;setting&nbsp;of&nbsp;a&nbsp;4x5&nbsp;inch&nbsp;document&nbsp;size.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appSettings.SetRasterExportSize&nbsp;visRasterFitToCustomSize,&nbsp;<span class="visualBasic__number">4</span>,&nbsp;<span class="visualBasic__number">5</span>,&nbsp;visRasterInch&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Write&nbsp;out&nbsp;the&nbsp;Raster&nbsp;Export&nbsp;Size&nbsp;settings</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;after&nbsp;changing&nbsp;the&nbsp;values&nbsp;to&nbsp;a&nbsp;custom&nbsp;setting.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;***&nbsp;Settings&nbsp;after&nbsp;changing&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WriteOutCurrentExportSize&nbsp;appSettings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reset&nbsp;the&nbsp;Raster&nbsp;Export&nbsp;Size&nbsp;settings</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;the&nbsp;default&nbsp;(the&nbsp;source).</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appSettings.SetRasterExportSize&nbsp;visRasterFitToSourceSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Write&nbsp;out&nbsp;current&nbsp;settings&nbsp;after&nbsp;changing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;values&nbsp;back&nbsp;to&nbsp;the&nbsp;default.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;***&nbsp;Settings&nbsp;after&nbsp;resetting&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WriteOutCurrentExportSize&nbsp;appSettings&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;WriteOutCurrentExportSize(appSettings&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.ApplicationSettings)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;expSize&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;VisRasterExportSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;width&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;height&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;units&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;VisRasterExportSizeUnits&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;GetRasterExportSize&nbsp;is&nbsp;a&nbsp;new&nbsp;method&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;let&nbsp;you&nbsp;access&nbsp;the&nbsp;Raster&nbsp;Export&nbsp;Size&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appSettings.GetRasterExportSize&nbsp;expSize,&nbsp;width,&nbsp;height,&nbsp;units&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;expSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;width&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;height&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;units&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Figure&nbsp;out&nbsp;what&nbsp;value&nbsp;was&nbsp;returned&nbsp;for</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;VisRasterExportSize&nbsp;and&nbsp;write&nbsp;out</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;information&nbsp;to&nbsp;the&nbsp;Immediate&nbsp;Window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;expSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSize.visRasterFitToScreenSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size:&nbsp;visRasterFitToScreenSize&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSize.visRasterFitToPrinterSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size:&nbsp;visRasterFitToPrinterSize&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSize.visRasterFitToSourceSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size:&nbsp;visRasterFitToSourceSize&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSize.visRasterFitToCustomSize&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size:&nbsp;visRasterFitToCustomSize&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Only&nbsp;if&nbsp;the&nbsp;VisRasterExportSize&nbsp;value&nbsp;is&nbsp;visRasterFitToCustomSize</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;will&nbsp;the&nbsp;other&nbsp;values&nbsp;return&nbsp;something&nbsp;other&nbsp;than&nbsp;null.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Width:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;width&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Height:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;height&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;units&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSizeUnits.visRasterInch&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size&nbsp;Units:&nbsp;visRasterInch&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSizeUnits.visRasterCm&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size&nbsp;Units:&nbsp;visRasterCm&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;VisRasterExportSizeUnits.visRasterPixel&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Export&nbsp;Size&nbsp;Units:&nbsp;visRasterPixel&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26208" href="/site/view/file/26208/1/Visio.ReadAndWriteRasterExportSize.txt">Visio.ReadAndWriteRasterExportSize.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26209" href="/site/view/file/26209/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905478">Visio Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
