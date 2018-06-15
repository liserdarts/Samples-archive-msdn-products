# LINQ to XML - Miscellaneous Samples
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
* 2011-06-28 02:47:47
## Description

<h1>Introduction</h1>
<p>These samples demonstrate various ways of working with XML data.</p>
<h1><span>Building the Sample</span></h1>
<p><em>Are there special requirements or instructions for building the sample?</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>These samples include:</p>
<ul>
<li>Get the outer XML of a node </li><li>Get the inner text of a node </li><li>Check if an element has attributes </li><li>Check if an element has element children </li><li>Check if an element is empty </li><li>Get the name of an element </li><li>Get the name of an attribute </li><li>Get the XML declaration </li><li>Find the type of the node </li><li>Verify that the phone numbers are of the format xxx-xxx-xxxx </li><li>Validate file structure </li><li>Calculate sum, average, min, and max of freight of all orders </li></ul>
<h1>Screenshot</h1>
<p><img src="23923-screenshot.png" alt="" width="677" height="102"></p>
<h1>Sample Code</h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">            [Category(&quot;Misc&quot;)]
            [Description(&quot;Check if an element has element children&quot;)]
            public void XLinq93()
            {
                XDocument doc = XDocument.Load(&quot;nw_customers.xml&quot;);
                XElement e = doc.Element(&quot;Root&quot;)
                                .Element(&quot;Customers&quot;);
                Console.WriteLine(&quot;Customers has elements? {0}&quot;, e.HasElements);

            }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Category(<span class="cs__string">&quot;Misc&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Description(<span class="cs__string">&quot;Check&nbsp;if&nbsp;an&nbsp;element&nbsp;has&nbsp;element&nbsp;children&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;XLinq93()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XDocument&nbsp;doc&nbsp;=&nbsp;XDocument.Load(<span class="cs__string">&quot;nw_customers.xml&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XElement&nbsp;e&nbsp;=&nbsp;doc.Element(<span class="cs__string">&quot;Root&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Element(<span class="cs__string">&quot;Customers&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Customers&nbsp;has&nbsp;elements?&nbsp;{0}&quot;</span>,&nbsp;e.HasElements);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><a class="browseFile" href="sourcecode?fileId=23920&pathId=1063173858">Program.cs</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23920&pathId=870926035">config.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23920&pathId=991438596">nw_customers.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=23920&pathId=1723911859">nw_orders.xml</a>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ to XML: <a href="http://msdn.microsoft.com/en-us/library/bb387098.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/bb387098.aspx</a></p>
