# Excel 2010: Work with Header and Footer Properties Using Excel.PagesAndPage
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* page object
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:34:56
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the header and footer properties of the
<strong>Pages </strong>object and <strong>Page </strong>object in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the WorkWithPages procedure, and then press F8 to start debugging. Arrange the VBA and Excel windows side by side so you
 can see the results of running the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WorkWithPages()
    ' Fill random data:
    Range(&quot;A1&quot;, &quot;R100&quot;).Formula = &quot;=RANDBETWEEN(1, 100)&quot;
   
    Dim pgs As Pages
    Set pgs = PageSetup.Pages
   
    PageSetup.DifferentFirstPageHeaderFooter = True
   
    ' Look in the Immediate window for this output:
    Debug.Print &quot;The current sheet can be printed on &quot; &amp; _
     pgs.Count &amp; &quot; page(s).&quot;
    
    Dim pg As Page
    Set pg = pgs(1)
    pg.CenterHeader.Text = &quot;This is the first page's header&quot;
   
    Set pg = pgs(2)
    pg.CenterFooter.Text = &quot;This is the second page's footer&quot;
      
    Set pg = pgs(pgs.Count)
    pg.CenterFooter.Text = &quot;This is the last page's center footer.&quot;
    pg.LeftHeader.Text = &quot;This is the last page's header&quot;
   
    ' Note that Excel supports only distinct headers/footers
    ' for the first page, so headers and footers on the second
    ' and other pages are combined--the last value set overwrites
    ' the header/footer.
   
    ' See the values in the Immediate window.
    ' Note that the code disregards errors that occur--attempting
    ' to retrieve a header/footer setting that doesn't exist raises an error:
    On Error Resume Next
    Debug.Print &quot;First page (CenterHeader) : &quot; &amp; pgs(1).CenterHeader.Text
    Debug.Print &quot;Second page (CenterHeader): &quot; &amp; pgs(2).CenterHeader.Text
    Debug.Print &quot;Second page (CenterFooter): &quot; &amp; pgs(2).CenterFooter.Text
    Debug.Print &quot;Third page (CenterFooter) : &quot; &amp; pgs(3).CenterFooter.Text
    Debug.Print &quot;Last page (LeftHeader)    : &quot; &amp; pgs(pgs.Count).LeftHeader.Text
    Debug.Print &quot;Last page (CenterFooter)  : &quot; &amp; pgs(pgs.Count).CenterFooter.Text
   
    ' In conclusion, use the Page class to retrieve information about headers
    ' and footers for specific pages. Use the PageSetup object to set the headers
    ' and footers, as it's clearer to set them there.
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithPages()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;random&nbsp;data:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;R100&quot;</span>).Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=RANDBETWEEN(1,&nbsp;100)&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pgs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Pages&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pgs&nbsp;=&nbsp;PageSetup.Pages&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;PageSetup.DifferentFirstPageHeaderFooter&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Look&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window&nbsp;for&nbsp;this&nbsp;output:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;The&nbsp;current&nbsp;sheet&nbsp;can&nbsp;be&nbsp;printed&nbsp;on&nbsp;&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pgs.Count&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;page(s).&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pg&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Page&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pg&nbsp;=&nbsp;pgs(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pg.CenterHeader.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;first&nbsp;page's&nbsp;header&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pg&nbsp;=&nbsp;pgs(<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pg.CenterFooter.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;second&nbsp;page's&nbsp;footer&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pg&nbsp;=&nbsp;pgs(pgs.Count)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pg.CenterFooter.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;last&nbsp;page's&nbsp;center&nbsp;footer.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pg.LeftHeader.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;the&nbsp;last&nbsp;page's&nbsp;header&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;Excel&nbsp;supports&nbsp;only&nbsp;distinct&nbsp;headers/footers</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;the&nbsp;first&nbsp;page,&nbsp;so&nbsp;headers&nbsp;and&nbsp;footers&nbsp;on&nbsp;the&nbsp;second</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;other&nbsp;pages&nbsp;are&nbsp;combined--the&nbsp;last&nbsp;value&nbsp;set&nbsp;overwrites</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;header/footer.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;See&nbsp;the&nbsp;values&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;the&nbsp;code&nbsp;disregards&nbsp;errors&nbsp;that&nbsp;occur--attempting</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;retrieve&nbsp;a&nbsp;header/footer&nbsp;setting&nbsp;that&nbsp;doesn't&nbsp;exist&nbsp;raises&nbsp;an&nbsp;error:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">Resume</span>&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;First&nbsp;page&nbsp;(CenterHeader)&nbsp;:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pgs(<span class="visualBasic__number">1</span>).CenterHeader.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Second&nbsp;page&nbsp;(CenterHeader):&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pgs(<span class="visualBasic__number">2</span>).CenterHeader.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Second&nbsp;page&nbsp;(CenterFooter):&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pgs(<span class="visualBasic__number">2</span>).CenterFooter.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Third&nbsp;page&nbsp;(CenterFooter)&nbsp;:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pgs(<span class="visualBasic__number">3</span>).CenterFooter.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Last&nbsp;page&nbsp;(LeftHeader)&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pgs(pgs.Count).LeftHeader.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Last&nbsp;page&nbsp;(CenterFooter)&nbsp;&nbsp;:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pgs(pgs.Count).CenterFooter.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;conclusion,&nbsp;use&nbsp;the&nbsp;Page&nbsp;class&nbsp;to&nbsp;retrieve&nbsp;information&nbsp;about&nbsp;headers</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;footers&nbsp;for&nbsp;specific&nbsp;pages.&nbsp;Use&nbsp;the&nbsp;PageSetup&nbsp;object&nbsp;to&nbsp;set&nbsp;the&nbsp;headers</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;footers,&nbsp;as&nbsp;it's&nbsp;clearer&nbsp;to&nbsp;set&nbsp;them&nbsp;there.</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25902" href="/site/view/file/25902/1/Excel.PagesAndPage.txt">Excel.PagesAndPage.txt</a>&nbsp;- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25903" href="/site/view/file/25903/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
