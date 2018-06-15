# LINQ to XML - Construction Samples
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
* 2011-06-28 02:44:33
## Description

<h1>Introduction</h1>
<p>These samples show how to construct XML elements and documents from code using LINQ to XML.</p>
<h1><span>Building the Sample</span></h1>
<ol>
<li>Open Program.cs </li><li>Uncomment the line for the sample that you want to run; comment ones you don't want to run
</li><li>Press Ctrl &#43; F5 </li></ol>
<h1>Screenshot</h1>
<p><img src="23878-screenshot.png" alt="" width="677" height="342"></p>
<h1>Sample Code</h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">            [Category(&quot;Construction&quot;)]
            [Description(&quot;Computed element name&quot;)]
            public void XLinq8()
            {
                XDocument customers = XDocument.Load(&quot;nw_customers.xml&quot;);
                string name = (string)customers.Elements(&quot;Root&quot;)
                                               .Elements(&quot;Customers&quot;)
                                               .First()
                                               .Attribute(&quot;CustomerID&quot;);
                XElement result = new XElement(name,
                                                &quot;Element with a computed name&quot;);
                Console.WriteLine(result);

            }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Category(<span class="cs__string">&quot;Construction&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Description(<span class="cs__string">&quot;Computed&nbsp;element&nbsp;name&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;XLinq8()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XDocument&nbsp;customers&nbsp;=&nbsp;XDocument.Load(<span class="cs__string">&quot;nw_customers.xml&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;name&nbsp;=&nbsp;(<span class="cs__keyword">string</span>)customers.Elements(<span class="cs__string">&quot;Root&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Elements(<span class="cs__string">&quot;Customers&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.First()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Attribute(<span class="cs__string">&quot;CustomerID&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XElement&nbsp;result&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(name,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;Element&nbsp;with&nbsp;a&nbsp;computed&nbsp;name&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(result);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><a class="browseFile" href="sourcecode?fileId=23879&pathId=1469735100">Program.cs</a>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ to XML: <a href="http://msdn.microsoft.com/en-us/library/bb387098.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/bb387098.aspx</a></p>
