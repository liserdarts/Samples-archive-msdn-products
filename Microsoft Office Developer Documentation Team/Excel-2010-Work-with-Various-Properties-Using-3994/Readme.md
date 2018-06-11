# Excel 2010: Work with Various Properties Using Excel.PageSetup Object
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* page setup object
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:35:21
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use various properties of the
<strong>PageSetup </strong>object in a Microsoft Excel 2010 workbook.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the PageSetupDemo procedure, and then press F8 to single-step through the code. Arrange the VBA and Excel windows side by
 side so you can follow the instructions in the comments.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub PageSetupDemo()
 
  Range(&quot;A1:H100&quot;).Formula = &quot;=RANDBETWEEN(1, 100)&quot;
  With PageSetup
    .LeftMargin = 100
    .RightMargin = 100
       
    ' Set the page header and footer for all the pages. You can see these
    ' by clicking File and then clicking the Print button.
    .CenterHeader = &quot;This is the page header.&quot;
    .CenterFooter = &quot;This is the page footer.&quot;
   
    ' Use distinct even and odd page headers:
    .OddAndEvenPagesHeaderFooter = True
    .EvenPage.CenterHeader.Text = &quot;This is an even page header&quot;
    .EvenPage.CenterFooter.Text = &quot;This is an even page footer&quot;
   
    ' When you preview the workbook, you'll see different headers on the
    ' even and odd pages.
   
    ' Now make the first page have a specific header/footer,
    ' as well:
    .DifferentFirstPageHeaderFooter = True
    .FirstPage.LeftFooter.Text = &quot;This is the bottom of the first page&quot;
    .FirstPage.LeftHeader.Text = &quot;This is the top of the first page.&quot;
   
    ' Watch carefully as you modify this property. It affects
    ' whether the headers and footers respect the margins
    ' you set in the PageSetup object, earlier in the
    ' procedure:
    .AlignMarginsHeaderFooter = True
    ' Now set the value to False:
    .AlignMarginsHeaderFooter = False
   
    ' Ensure that the header and footer size scales with the printed
    ' document scaling:
    .ScaleWithDocHeaderFooter = True
  End With
  ActiveWorkbook.PrintPreview
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;PageSetupDemo()&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1:H100&quot;</span>).Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=RANDBETWEEN(1,&nbsp;100)&quot;</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;PageSetup&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.LeftMargin&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.RightMargin&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;page&nbsp;header&nbsp;and&nbsp;footer&nbsp;for&nbsp;all&nbsp;the&nbsp;pages.&nbsp;You&nbsp;can&nbsp;see&nbsp;these</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;by&nbsp;clicking&nbsp;File&nbsp;and&nbsp;then&nbsp;clicking&nbsp;the&nbsp;Print&nbsp;button.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.CenterHeader&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;page&nbsp;header.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.CenterFooter&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;page&nbsp;footer.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;distinct&nbsp;even&nbsp;and&nbsp;odd&nbsp;page&nbsp;headers:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.OddAndEvenPagesHeaderFooter&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.EvenPage.CenterHeader.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;an&nbsp;even&nbsp;page&nbsp;header&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.EvenPage.CenterFooter.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;an&nbsp;even&nbsp;page&nbsp;footer&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;When&nbsp;you&nbsp;preview&nbsp;the&nbsp;workbook,&nbsp;you'll&nbsp;see&nbsp;different&nbsp;headers&nbsp;on&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;even&nbsp;and&nbsp;odd&nbsp;pages.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;make&nbsp;the&nbsp;first&nbsp;page&nbsp;have&nbsp;a&nbsp;specific&nbsp;header/footer,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;as&nbsp;well:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DifferentFirstPageHeaderFooter&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.FirstPage.LeftFooter.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;bottom&nbsp;of&nbsp;the&nbsp;first&nbsp;page&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.FirstPage.LeftHeader.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;top&nbsp;of&nbsp;the&nbsp;first&nbsp;page.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Watch&nbsp;carefully&nbsp;as&nbsp;you&nbsp;modify&nbsp;this&nbsp;property.&nbsp;It&nbsp;affects</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;whether&nbsp;the&nbsp;headers&nbsp;and&nbsp;footers&nbsp;respect&nbsp;the&nbsp;margins</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;set&nbsp;in&nbsp;the&nbsp;PageSetup&nbsp;object,&nbsp;earlier&nbsp;in&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;procedure:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.AlignMarginsHeaderFooter&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;set&nbsp;the&nbsp;value&nbsp;to&nbsp;False:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.AlignMarginsHeaderFooter&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Ensure&nbsp;that&nbsp;the&nbsp;header&nbsp;and&nbsp;footer&nbsp;size&nbsp;scales&nbsp;with&nbsp;the&nbsp;printed</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;document&nbsp;scaling:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ScaleWithDocHeaderFooter&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;ActiveWorkbook.PrintPreview&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25904" href="/site/view/file/25904/1/Excel.PageSetup.txt">Excel.PageSetup.txt</a>&nbsp;- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25905" href="/site/view/file/25905/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
