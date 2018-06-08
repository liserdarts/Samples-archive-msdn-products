# LINQ to XML - XName Samples
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
* 2011-06-28 02:50:12
## Description

<h1>Introduction</h1>
<p>These samples demonstrate using XNamespace in LINQ to XML to work with XML Namespaces.</p>
<h1><span>Building the Sample</span></h1>
<ol>
<li>Open Program.cs </li><li>Uncomment the line for the sample that you want to run; comment ones you don't want to run
</li><li>Press Ctrl &#43; F5 </li></ol>
<h1>Screenshot</h1>
<p><img src="/site/view/file/23984/1/Screenshot.png" alt="" width="677" height="246"></p>
<h1>Sample Code</h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">            [Category(&quot;XName&quot;)]
            [Description(&quot;Find xsd:element with name=FullAddress&quot;)]
            public void XLinq87()
            {
                XDocument doc = XDocument.Load(&quot;nw_customers.xsd&quot;);
                XNamespace XSD = &quot;http://www.w3.org/2001/XMLSchema&quot;;
                XElement result = doc.Descendants(XSD &#43; &quot;element&quot;)
                                     .Where(e =&gt; (string)e.Attribute(&quot;name&quot;) == &quot;FullAddress&quot;)
                                     .First();
                Console.WriteLine(result);
            }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Category(<span class="cs__string">&quot;XName&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Description(<span class="cs__string">&quot;Find&nbsp;xsd:element&nbsp;with&nbsp;name=FullAddress&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;XLinq87()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XDocument&nbsp;doc&nbsp;=&nbsp;XDocument.Load(<span class="cs__string">&quot;nw_customers.xsd&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XNamespace&nbsp;XSD&nbsp;=&nbsp;<span class="cs__string">&quot;http://www.w3.org/2001/XMLSchema&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XElement&nbsp;result&nbsp;=&nbsp;doc.Descendants(XSD&nbsp;&#43;&nbsp;<span class="cs__string">&quot;element&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(e&nbsp;=&gt;&nbsp;(<span class="cs__keyword">string</span>)e.Attribute(<span class="cs__string">&quot;name&quot;</span>)&nbsp;==&nbsp;<span class="cs__string">&quot;FullAddress&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.First();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(result);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><a class="browseFile" href="sourcecode?fileId=23983&pathId=521232463">Program.cs</a>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ to XML: <a href="http://msdn.microsoft.com/en-us/library/bb387098.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/bb387098.aspx</a></p>
