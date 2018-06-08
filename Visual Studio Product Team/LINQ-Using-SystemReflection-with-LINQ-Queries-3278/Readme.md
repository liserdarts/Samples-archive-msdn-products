# LINQ - Using System.Reflection with LINQ Queries
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* LINQ to SQL
## Topics
* LINQ to SQL
* System.Reflection
* Console Window
## IsPublished
* True
## ModifiedDate
* 2011-06-28 05:13:54
## Description

<h1>Introduction</h1>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">Use LINQ to right queries against the objects in your code using the Reflection APIs.</span></p>
<h1><span>Building the Sample</span></h1>
<p>Press F5</p>
<div class="section" id="demonstratesSection">
<h1>Description</h1>
<p>The sample shows how to use methods from the System.Reflection namespace to run queries over types from System.Xml.Linq.dll. The by-product is an HTML document outlining the public APIs for the assembly.</p>
<p>The Reflector application generates an HTML document that outlines the public API for a given assembly. The task is achieved in two phases. First, an XML document is emitted in memory. (See method
<span class="code">Emit*()</span>.) The document captures the relevant aspects of the assembly's metadata. In the second phase, an HTML document is extracted from the XML document. (See method
<span class="code">Extract*()</span>.) Note that the two phases use the same query constructions to handle similar information in different representations.</p>
<h1>Sample Code</h1>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">        XElement EmitAssembly(Assembly assembly) 
        {
            return new XElement(&quot;assembly&quot;,
                        new XAttribute(&quot;name&quot;, assembly.ManifestModule.Name),
                        from type in assembly.GetTypes()
                        where GetVisible(type)
                        group type by GetNamespace(type) into g
                        orderby g.Key
                        select EmitNamespace(g.Key, g));
        }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XElement&nbsp;EmitAssembly(Assembly&nbsp;assembly)&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;assembly&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XAttribute(<span class="cs__string">&quot;name&quot;</span>,&nbsp;assembly.ManifestModule.Name),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;from&nbsp;type&nbsp;<span class="cs__keyword">in</span>&nbsp;assembly.GetTypes()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;where&nbsp;GetVisible(type)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;group&nbsp;type&nbsp;by&nbsp;GetNamespace(type)&nbsp;into&nbsp;g&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderby&nbsp;g.Key&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;select&nbsp;EmitNamespace(g.Key,&nbsp;g));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">    Public Function EmitAssembly(ByVal assembly As Assembly) As XElement
        Return &lt;assembly name=&lt;%= assembly.ManifestModule.Name %&gt;&gt;
                   &lt;%= From type In assembly.GetTypes() _
                       Where GetVisible(type) _
                       Group type By GetNamespace = GetNamespace(type) Into g = Group _
                       Order By GetNamespace _
                       Select EmitNamespace(GetNamespace, g) %&gt;
               &lt;/assembly&gt;
    End Function</pre>
<div class="preview">
<pre id="codePreview" class="vb">&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;EmitAssembly(<span class="visualBasic__keyword">ByVal</span>&nbsp;assembly&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Assembly)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;XElement&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;&lt;assembly&nbsp;name=&lt;%=&nbsp;assembly.ManifestModule.Name&nbsp;%&gt;&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;%=&nbsp;From&nbsp;type&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;assembly.GetTypes()&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Where&nbsp;GetVisible(type)&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Group&nbsp;type&nbsp;By&nbsp;GetNamespace&nbsp;=&nbsp;GetNamespace(type)&nbsp;Into&nbsp;g&nbsp;=&nbsp;Group&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Order&nbsp;By&nbsp;GetNamespace&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;EmitNamespace(GetNamespace,&nbsp;g)&nbsp;%&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/assembly&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li>C#
<ul>
<li><a class="browseFile" href="sourcecode?fileId=24060&pathId=1842281619">Program.cs</a>
</li><li><a class="browseFile" href="sourcecode?fileId=24060&pathId=16606386">Reflector.cs</a>
</li></ul>
</li><li>VB
<ul>
<li><a class="browseFile" href="sourcecode?fileId=22731&pathId=1555370156">Program.vb</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22731&pathId=1348274588">Reflector.vb</a>
</li></ul>
</li></ul>
