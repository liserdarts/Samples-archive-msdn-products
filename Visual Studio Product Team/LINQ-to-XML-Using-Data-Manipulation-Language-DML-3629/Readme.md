# LINQ to XML - Using Data Manipulation Language (DML)
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* LINQ
* LINQ to XML
* .NET Framework 4.0
## Topics
* LINQ
* LINQ to XML
* XML
* Console Window
* XML Documentation
## IsPublished
* True
## ModifiedDate
* 2011-06-28 02:44:57
## Description

<h1>Introduction</h1>
<p>These samples show how to use Data Manipulation Language (DML) using LINQ to XML.<em><br>
</em></p>
<h1><span>Building the Sample</span></h1>
<ol>
<li>Open Program.cs </li><li>Uncomment the line for the sample that you want to run; comment ones you don't want to run
</li><li>Press Ctrl &#43; F5 </li></ol>
<h1>Screenshot</h1>
<p><img src="23885-screenshot.png" alt="" width="429" height="239"></p>
<h1>Sample Code</h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">            [Category(&quot;DML&quot;)]
            [Description(&quot;Add an element as the last child&quot;)]
            public void XLinq60()
            {
                XDocument doc = XDocument.Load(&quot;config.xml&quot;);
                XElement config = doc.Element(&quot;config&quot;);
                config.Add(new XElement(&quot;logFolder&quot;, &quot;c:\\log&quot;));
                Console.WriteLine(config);

            }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Category(<span class="cs__string">&quot;DML&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Description(<span class="cs__string">&quot;Add&nbsp;an&nbsp;element&nbsp;as&nbsp;the&nbsp;last&nbsp;child&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;XLinq60()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XDocument&nbsp;doc&nbsp;=&nbsp;XDocument.Load(<span class="cs__string">&quot;config.xml&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XElement&nbsp;config&nbsp;=&nbsp;doc.Element(<span class="cs__string">&quot;config&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;config.Add(<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;logFolder&quot;</span>,&nbsp;<span class="cs__string">&quot;c:\\log&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(config);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><a class="browseFile" href="sourcecode?fileId=23890&pathId=729749173">Program.cs</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23890&pathId=441838143">config.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23890&pathId=1913299725">nw_customers.xml</a>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ to XML: <a href="http://msdn.microsoft.com/en-us/library/bb387098.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/bb387098.aspx</a></p>
