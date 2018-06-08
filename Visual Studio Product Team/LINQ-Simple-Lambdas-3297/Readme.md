# LINQ - Simple Lambdas
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* LINQ
## Topics
* LINQ
* Data Access
## IsPublished
* True
## ModifiedDate
* 2011-06-28 05:23:26
## Description

<h1>Introduction</h1>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">Several examples of how to write and use lambda expressions.</span></p>
<h1><span>Building the Sample</span></h1>
<p>Press F5</p>
<div class="section" id="demonstratesSection">
<h1>Description</h1>
<p>This sample provides a brief introduction to the use of lambda expressions.</p>
<p>There are ten samples in this project. By default, the first sample will run. By changing the
<span class="code">Main()</span> method in Program.vb, you can modify the code to run one of the other samples. For instance, by default,
<span class="code">Main</span> makes the following call:</p>
<div class="code"><span>&nbsp;</span></div>
</div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<td colspan="2">
<pre>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Samples.Sample1()</pre>
</td>
</tr>
</tbody>
</table>
<p>Modify this code as follows to call <span class="code">Sample2()</span>:</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<td colspan="2">
<pre>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Samples.Sample2()</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The samples are named <span class="code">Sample1()</span>, <span class="code">
Sample2()</span>, and so forth, up to <span class="code">Sample10()</span>.</p>
<h1>Screenshot</h1>
<p><img src="/site/view/file/22793/1/Screenshot.png" alt="" width="341" height="227"></p>
<h1>Sample Code</h1>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public static void Sample10() {
        // use query expressions to simplify
        var q = from p in persons 
                orderby p.Level, p.Name
                group p.Name by p.Level into g
                select new {Level = g.Key, Persons = g};
        
        foreach(var g in q) {
            Console.WriteLine(&quot;Level: {0}&quot;, g.Level);
            foreach(var p in g.Persons) {
                Console.WriteLine(&quot;Person: {0}&quot;, p);
            }
        }                
    }</pre>
<div class="preview">
<pre id="codePreview" class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Sample10()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;use&nbsp;query&nbsp;expressions&nbsp;to&nbsp;simplify</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;q&nbsp;=&nbsp;from&nbsp;p&nbsp;<span class="cs__keyword">in</span>&nbsp;persons&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;orderby&nbsp;p.Level,&nbsp;p.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;group&nbsp;p.Name&nbsp;by&nbsp;p.Level&nbsp;into&nbsp;g&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;select&nbsp;<span class="cs__keyword">new</span>&nbsp;{Level&nbsp;=&nbsp;g.Key,&nbsp;Persons&nbsp;=&nbsp;g};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>(var&nbsp;g&nbsp;<span class="cs__keyword">in</span>&nbsp;q)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Level:&nbsp;{0}&quot;</span>,&nbsp;g.Level);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>(var&nbsp;p&nbsp;<span class="cs__keyword">in</span>&nbsp;g.Persons)&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Person:&nbsp;{0}&quot;</span>,&nbsp;p);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<p></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">    Public Shared Sub Sample1()
        ' use Where() to filter out elements matching a particular condition       
        Dim fnums = numbers.Where(Function(n) n &lt; 5)

        Console.WriteLine(&quot;Numbers &lt; 5&quot;)
        For Each x As Integer In fnums
            Console.WriteLine(x)
        Next
    End Sub

    Public Shared Sub Sample2()
        ' use First() to find the one element matching a particular condition       
        Dim v As String = strings.First(Function(s) s(0) = &quot;o&quot;c)

        Console.WriteLine(&quot;string starting with 'o': {0}&quot;, v)
    End Sub

    Public Shared Sub Sample3()
        ' use Select() to convert each element into a new value
        Dim snums = numbers.Select(Function(n) strings(n))

        Console.WriteLine(&quot;Numbers&quot;)
        For Each s As String In snums
            Console.WriteLine(s)
        Next
    End Sub</pre>
<div class="preview">
<pre id="codePreview" class="vb">&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Shared</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Sample1()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;use&nbsp;Where()&nbsp;to&nbsp;filter&nbsp;out&nbsp;elements&nbsp;matching&nbsp;a&nbsp;particular&nbsp;condition&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;fnums&nbsp;=&nbsp;numbers.Where(<span class="visualBasic__keyword">Function</span>(n)&nbsp;n&nbsp;&lt;&nbsp;<span class="visualBasic__number">5</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="visualBasic__string">&quot;Numbers&nbsp;&lt;&nbsp;5&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;x&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;fnums&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(x)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Shared</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Sample2()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;use&nbsp;First()&nbsp;to&nbsp;find&nbsp;the&nbsp;one&nbsp;element&nbsp;matching&nbsp;a&nbsp;particular&nbsp;condition&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;v&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;strings.First(<span class="visualBasic__keyword">Function</span>(s)&nbsp;s(<span class="visualBasic__number">0</span>)&nbsp;=&nbsp;<span class="visualBasic__string">&quot;o&quot;</span>c)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="visualBasic__string">&quot;string&nbsp;starting&nbsp;with&nbsp;'o':&nbsp;{0}&quot;</span>,&nbsp;v)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Shared</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Sample3()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;use&nbsp;Select()&nbsp;to&nbsp;convert&nbsp;each&nbsp;element&nbsp;into&nbsp;a&nbsp;new&nbsp;value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;snums&nbsp;=&nbsp;numbers.<span class="visualBasic__keyword">Select</span>(<span class="visualBasic__keyword">Function</span>(n)&nbsp;strings(n))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="visualBasic__string">&quot;Numbers&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;s&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;snums&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(s)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li>C#
<ul>
<li><a class="browseFile" href="sourcecode?fileId=24064&pathId=1357046241">Program.cs</a>
</li><li><a class="browseFile" href="sourcecode?fileId=24064&pathId=1841004270">Samples.cs</a>
</li></ul>
</li><li>VB
<ul>
<li><a class="browseFile" href="sourcecode?fileId=22792&pathId=1173531666">Program.vb</a>
</li><li><a class="browseFile" href="sourcecode?fileId=22792&pathId=823407690">Samples.vb</a>
</li></ul>
</li></ul>
<h1>More Information</h1>
<p>For more information on LINQ: <a href="http://msdn.microsoft.com/en-us/netframework/aa904594.aspx" target="_blank">
http://msdn.microsoft.com/en-us/netframework/aa904594.aspx</a></p>
