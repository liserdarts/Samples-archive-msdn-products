# LINQ - RSS Aggregator using LINQ to XML
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* LINQ
* XML
## Topics
* RSS
* LINQ to XML
## IsPublished
* True
## ModifiedDate
* 2011-07-08 11:01:52
## Description

<h1>Introduction</h1>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">This sample acts as a tiny web server that aggregates several RSS feeds.</span></p>
<h1><span>Building the Sample</span></h1>
<p>Press F5</p>
<h1>Description</h1>
<p>This sample acts as a small Web server that aggregates several RSS feeds. An Internet connection is required. On Windows Vista, you may have to run this program as Administrator.</p>
<p>*Note: In the sample code below there is a list of feeds.&nbsp; If you run this sample and it returns a 404 error, browse to each URL individually and verify that each one still exists.</p>
<h1>Sample Code</h1>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    static XElement GetReplyBody() {
        return new XElement(&quot;rss&quot;,
            new XAttribute(&quot;version&quot;, &quot;2.0&quot;),
            new XElement(&quot;channel&quot;,
              new XElement(&quot;title&quot;, &quot;C# Geeks&quot;),
              new XElement(&quot;link&quot;, feedUrl),
              new XElement(&quot;description&quot;, &quot;C# Team Members&quot;),
              new XElement(&quot;generator&quot;, &quot;LinqToXml-based RSS aggregator&quot;),
              GetItems().ToArray()
              ));

    }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;XElement&nbsp;GetReplyBody()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;rss&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XAttribute(<span class="cs__string">&quot;version&quot;</span>,&nbsp;<span class="cs__string">&quot;2.0&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;channel&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;title&quot;</span>,&nbsp;<span class="cs__string">&quot;C#&nbsp;Geeks&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;link&quot;</span>,&nbsp;feedUrl),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;description&quot;</span>,&nbsp;<span class="cs__string">&quot;C#&nbsp;Team&nbsp;Members&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;XElement(<span class="cs__string">&quot;generator&quot;</span>,&nbsp;<span class="cs__string">&quot;LinqToXml-based&nbsp;RSS&nbsp;aggregator&quot;</span>),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetItems().ToArray()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Function GetReplyBody() As XElement
        Dim feeds() As String = {&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=Amanda&#43;Silver&amp;AndTags=1&quot;, _
                                 &quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=Beth&#43;Massi&amp;AndTags=1&quot;, _
                                 &quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=Matt&#43;Gertz&amp;AndTags=1&quot;, _
                                 &quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=LINQ_2F00_VB9&amp;AndTags=1&quot;, _
                                 &quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=VB6_5F00_Migration_2F00_Interop&amp;AndTags=1&quot;, _
                                 &quot;http://www.panopticoncentral.net/Rss.aspx&quot;, _
                                 &quot;http://blogs.msdn.com/vsdata/rss.xml&quot;, _
                                 &quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=IDE&amp;AndTags=1&quot;}

        Return &lt;rss version=&quot;2.0&quot;&gt;
                   &lt;channel&gt;
                       &lt;title&gt;VB Genius&lt;/title&gt;
                       &lt;link&gt;http://&#43;:8086/VBfeeds/&lt;/link&gt;
                       &lt;description&gt;VB Team Members&lt;/description&gt;
                       &lt;generator&gt;XLinq-based RSS aggregator using VB Only Super Cool XML Literals Feature&lt;/generator&gt;
                       &lt;%= From f In feeds Let feed = XDocument.Load(f) Select feeditem = feed.Root.Element(&quot;channel&quot;).Elements(&quot;item&quot;) From si In feeditem Select si %&gt;
                   &lt;/channel&gt;
               &lt;/rss&gt;
    End Function</pre>
<div class="preview">
<pre id="codePreview" class="vb"><span class="visualBasic__keyword">Function</span>&nbsp;GetReplyBody()&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;XElement&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;feeds()&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;{<span class="visualBasic__string">&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=Amanda&#43;Silver&amp;AndTags=1&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=Beth&#43;Massi&amp;AndTags=1&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=Matt&#43;Gertz&amp;AndTags=1&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=LINQ_2F00_VB9&amp;AndTags=1&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=VB6_5F00_Migration_2F00_Interop&amp;AndTags=1&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://www.panopticoncentral.net/Rss.aspx&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://blogs.msdn.com/vsdata/rss.xml&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;http://blogs.msdn.com/vbteam/rss.aspx?Tags=IDE&amp;AndTags=1&quot;</span>}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;&lt;rss&nbsp;version=<span class="visualBasic__string">&quot;2.0&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;channel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;title&gt;VB&nbsp;Genius&lt;/title&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;link&gt;http://&#43;:<span class="visualBasic__number">8086</span>/VBfeeds/&lt;/link&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;description&gt;VB&nbsp;Team&nbsp;Members&lt;/description&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;generator&gt;XLinq-based&nbsp;RSS&nbsp;aggregator&nbsp;using&nbsp;VB&nbsp;Only&nbsp;Super&nbsp;Cool&nbsp;XML&nbsp;Literals&nbsp;Feature&lt;/generator&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;%=&nbsp;From&nbsp;f&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;feeds&nbsp;<span class="visualBasic__keyword">Let</span>&nbsp;feed&nbsp;=&nbsp;XDocument.Load(f)&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;feeditem&nbsp;=&nbsp;feed.Root.Element(<span class="visualBasic__string">&quot;channel&quot;</span>).Elements(<span class="visualBasic__string">&quot;item&quot;</span>)&nbsp;From&nbsp;si&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;feeditem&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;si&nbsp;%&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/channel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/rss&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li>C#
<ul>
<li><a class="browseFile" href="sourcecode?fileId=24062&pathId=302787165">Program.cs</a>
</li></ul>
</li><li>VB
<ul>
<li><a class="browseFile" href="sourcecode?fileId=22787&pathId=2129924719">Module1.vb</a>
</li></ul>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ to XML: <a href="http://msdn.microsoft.com/en-us/library/bb387098.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/bb387098.aspx</a></p>
