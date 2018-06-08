# LINQ to XML - Sorting Operator Samples
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
* 2011-06-28 02:47:00
## Description

<h1>Introduction</h1>
<p>These samples demonstrate how to sort results using &quot;orderby&quot; in LINQ to XML.</p>
<h1><span>Building the Sample</span></h1>
<ol>
<li>Open Program.cs </li><li>Uncomment the line for the sample that you want to run; comment ones you don't want to run
</li><li>Press Ctrl &#43; F5 </li></ol>
<h1>Screenshot</h1>
<p><img src="/site/view/file/23940/1/Screenshot.png" alt="" width="677" height="342"></p>
<h1>Sample Code</h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">            [Category(&quot;Sort&quot;)]
            [Description(&quot;Sort customers by name in ascending order&quot;)]
            public void XLinq56()
            {
                XDocument doc = XDocument.Load(&quot;nw_customers.xml&quot;);
                var query =
                    from
                        customer in doc.Descendants(&quot;Customers&quot;)
                    orderby
                        (string)customer.Elements(&quot;ContactName&quot;).First()
                    select
                        customer.Element(&quot;ContactName&quot;);
                XElement result =
                    new XElement(&quot;SortedCustomers&quot;, query);
                Console.WriteLine(result);
            }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Category(<span class="cs__string">&quot;Sort&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Description(<span class="cs__string">&quot;Sort&nbsp;customers&nbsp;by&nbsp;name&nbsp;in&nbsp;ascending&nbsp;order&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;XLinq56()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XDocument&nbsp;doc&nbsp;=&nbsp;XDocument.Load(<span class="cs__string">&quot;nw_customers.xml&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;query&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;from&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;customer&nbsp;<span class="cs__keyword">in</span>&nbsp;doc.Descendants(<span class="cs__string">&quot;Customers&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderby&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cs__keyword">string</span>)customer.Elements(<span class="cs__string">&quot;ContactName&quot;</span>).First()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;select&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;customer.Element(<span class="cs__string">&quot;ContactName&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XElement&nbsp;result&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;SortedCustomers&quot;</span>,&nbsp;query);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(result);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><a class="browseFile" href="sourcecode?fileId=23937&pathId=928624056">Program.cs</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23937&pathId=187441210">nw_customers.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23937&pathId=1107396148">nw_orders.xml</a>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ to XML: <a href="http://msdn.microsoft.com/en-us/library/bb387098.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/bb387098.aspx</a></p>
