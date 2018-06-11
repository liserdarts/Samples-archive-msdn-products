# Visual C# 2010 - IronPython Sample ReadMe
## Requires
* Visual Studio 2010
## License
* MS-LPL
## Technologies
* .NET Framework
* IronPython
## Topics
* Language Samples
* IronPythion
## IsPublished
* True
## ModifiedDate
* 2011-11-28 07:12:15
## Description

<h1>Visual C# 2010 - IronPython Sample ReadMe</h1>
<p>This example shows how to integrate Visual C# 2010 and IronPython using the new 'dynamic' feature added in C# 4.0
</p>
<p>To run the sample, you will need to install <a class="externalLink" href="http://ironpython.codeplex.com/releases/view/36280">
IronPython 2.6.1</a>, which is a available as a separate download. It may also work with more recent versions of IronPython.</p>
<p>After installing IronPython you may need to update this sample by replacing the following assembly references:</p>
<ul>
<li><strong>IronPython</strong> </li><li><strong>IronPython.Modules</strong> </li><li><strong>Microsoft.Dynamic</strong> </li><li><strong>Microsoft.Scripting</strong> </li></ul>
<p>To do so:</p>
<ol>
<li>Open the <strong>Solution Explorer</strong> and navigate to the <strong>References</strong> node.
</li><li>Remove the assemblies listed above. </li><li>Right-Click the <strong>References</strong> node and choose <strong>Add Reference</strong>.
</li><li>Browse to the directory where you installed IronPython.<br>
<em>By default, IronPython is installed under &quot;C:\Program Files\IronPython 2.6 for .NET 4.0&quot; on 32-bit systems and &quot;C:\Program Files (x86)\IronPython 2.6 for .NET 4.0&quot; on 64-bit systems.</em>
</li><li>Select and add new references to the assemblies listed above. </li></ol>
<p>After you have updated the project references, the program should compile and run without error.</p>
