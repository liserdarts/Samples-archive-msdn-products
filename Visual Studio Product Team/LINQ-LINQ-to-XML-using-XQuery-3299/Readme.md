# LINQ - LINQ to XML using XQuery
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* LINQ
* LINQ to XML
## Topics
* LINQ
* LINQ to XML
* XML
* Data Access
## IsPublished
* True
## ModifiedDate
* 2011-06-28 05:28:20
## Description

<h1>Introduction</h1>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">Another simple LINQ to XML sample query. This sample shows the minimal code necessary to write a LINQ to XML query.</span></p>
<h1><span>Building the Sample</span></h1>
<p>Press F5</p>
<h1>Description</h1>
<p>This sample demonstrates the code that is required to write a LINQ to XML query. A directory named data is included in this sample. The XML file that is queried is included in that directory.</p>
<h1>Screenshot</h1>
<p><img src="/site/view/file/22799/1/Screenshot.png" alt="" width="677" height="462"></p>
<h1>Sample Code</h1>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">        static public string SetDataPath()
        {
            string path = Environment.CommandLine;
            while (path.StartsWith(&quot;\&quot;&quot;))
            {
                path = path.Substring(1, path.Length - 2);
            }
            while (path.EndsWith(&quot;\&quot;&quot;) || path.EndsWith(&quot; &quot;))
            {
                path = path.Substring(0, path.Length - 2);
            }
            path = Path.GetDirectoryName(path);

            return Path.Combine(path, &quot;data\\&quot;);
        }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;SetDataPath()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;path&nbsp;=&nbsp;Environment.CommandLine;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(path.StartsWith(<span class="cs__string">&quot;\&quot;&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path&nbsp;=&nbsp;path.Substring(<span class="cs__number">1</span>,&nbsp;path.Length&nbsp;-&nbsp;<span class="cs__number">2</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(path.EndsWith(<span class="cs__string">&quot;\&quot;&quot;</span>)&nbsp;||&nbsp;path.EndsWith(<span class="cs__string">&quot;&nbsp;&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path&nbsp;=&nbsp;path.Substring(<span class="cs__number">0</span>,&nbsp;path.Length&nbsp;-&nbsp;<span class="cs__number">2</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path&nbsp;=&nbsp;Path.GetDirectoryName(path);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;Path.Combine(path,&nbsp;<span class="cs__string">&quot;data\\&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">    Sub Main()
        SetDataPath()
        ' List all books by Serge and Peter with co-authored books repeated
        Dim doc = XDocument.Load(SetDataPath() &amp; &quot;bib.xml&quot;)

        Dim b1 = From b In doc...&lt;book&gt; _
                 Aggregate author In b.&lt;author&gt;.&lt;first&gt; _
                 Into AnyAuthorNamedSerge = Any(author.Value = &quot;Serge&quot;) _
                 Where AnyAuthorNamedSerge = True

        Dim b2 = From b In doc...&lt;book&gt; _
                 Aggregate author In b.&lt;author&gt;.&lt;first&gt; _
                 Into AnyAuthorNamedSerge = Any(author.Value = &quot;Peter&quot;) _
                 Where AnyAuthorNamedSerge = True

        Dim books = b1.Concat(b2)

        For Each b In books
            Console.WriteLine(b)
        Next

        Console.ReadLine()

    End Sub</pre>
<div class="preview">
<pre id="codePreview" class="vb">&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Main()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetDataPath()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;List&nbsp;all&nbsp;books&nbsp;by&nbsp;Serge&nbsp;and&nbsp;Peter&nbsp;with&nbsp;co-authored&nbsp;books&nbsp;repeated</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc&nbsp;=&nbsp;XDocument.Load(SetDataPath()&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;bib.xml&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;b1&nbsp;=&nbsp;From&nbsp;b&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;doc...&lt;book&gt;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aggregate&nbsp;author&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;b.&lt;author&gt;.&lt;first&gt;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Into&nbsp;AnyAuthorNamedSerge&nbsp;=&nbsp;Any(author.Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Serge&quot;</span>)&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Where&nbsp;AnyAuthorNamedSerge&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;b2&nbsp;=&nbsp;From&nbsp;b&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;doc...&lt;book&gt;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aggregate&nbsp;author&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;b.&lt;author&gt;.&lt;first&gt;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Into&nbsp;AnyAuthorNamedSerge&nbsp;=&nbsp;Any(author.Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Peter&quot;</span>)&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Where&nbsp;AnyAuthorNamedSerge&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;books&nbsp;=&nbsp;b1.Concat(b2)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;b&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;books&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(b)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.ReadLine()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li>C#
<ul>
<li><a class="browseFile" href="sourcecode?fileId=24066&pathId=915416040">Program.cs</a>
</li><li><a class="browseFile" href="sourcecode?fileId=24066&pathId=476013371">bib.xml</a>
</li></ul>
</li><li>VB
<ul>
<li><a class="browseFile" href="sourcecode?fileId=22798&pathId=2006997828">Module1.vb</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=476013371">bib.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=1627714617">book.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=1680506275">books.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=1660115237">doc.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=1669913106">ns1.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=497786265">ns2.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=1407086730">opml.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=1828515045">prices.xml</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22798&pathId=8750335">reviews.xml</a>
</li></ul>
</li></ul>
<h1>More Information</h1>
<p>For more information on XQuery: <a href="http://msdn.microsoft.com/en-us/library/ms190262.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/ms190262.aspx</a></p>
