# Outlook Social Connector 2013: Provider templates
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Office Outlook 2007
* Outlook 2010
* Visual Studio 2012
* Word 2013
* Excel 2013
* Outlook 2013
## Topics
* OSC provider
* Templates
## IsPublished
* True
## ModifiedDate
* 2012-11-12 01:03:39
## Description

<p><span style="font-size:small">The provider templates are available in C&#43;&#43;, C#, and Visual Basic. These templates provide a starting point for your provider development. You can subsequently write the implementation code and create a setup package for your
 provider.</span></p>
<h1>Prerequisites</h1>
<ul>
<li><span style="font-size:small">Outlook 2013</span> </li><li><span style="font-size:small">Visual Studio 2010</span> </li><li><span style="font-size:small">Technical familiarity with Visual Studio and C&#43;&#43;, C#, or Visual Basic</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">There are 3 sets of provider templates, one for each of the following languages: C&#43;&#43;, C#, and Visual Basic:</span></p>
<ul>
<li><span style="font-size:small">OSCProvider_CPP: contains the Visual Studio 2010 solution file OSCProvider_CPP.sln and code files for the provider template in C&#43;&#43;</span>
</li><li><span style="font-size:small">OSCProvider_CS: contains the Visual Studio 2010 solution file OSCProvider_CS.sln and code files for the provider template in C&#43;&#43;</span>
</li><li><span style="font-size:small">OSCProvider_VB: contains the Visual Studio 2010 solution file OSCProvider_VB.sln and code files for the provider template in C&#43;&#43;</span>
</li></ul>
<h1>Build the sample</h1>
<p><span style="font-size:small">To apply an OSC provider template</span></p>
<ol>
<li><span style="font-size:small">On the Start menu, right-click Microsoft Visual Studio 2010 and click the Run as administrator command. When prompted, click Yes to run Visual Studio as an administrator.</span>
</li><li><span style="font-size:small">Change the project name and namespace in the template to your project name and namespace identifiers.</span>
</li><li><span style="font-size:small">Modify the AssemblyInfo class to specify the appropriate assembly information.</span>
</li><li><span style="font-size:small">Implement the interface members marked as To-Do and add more dependencies and references, as required.</span>
</li><li><span style="font-size:small">Build the project.</span> </li><li><span style="font-size:small">Ensure that the provider assembly ProgID is listed as a key under HKEY_CURRENT_USER\Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders.</span>
</li><li><span style="font-size:small">To distribute the setup project, create a setup project in Visual Studio or a setup tool of your choice.</span>
</li><li><span style="font-size:small">Your setup project should complete COM registration for your assembly and also create the ProgID key as listed in step 5.</span>
</li></ol>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ff759451(v=office.15)">Applying a Sample Provider Template</a></span>
</li></ul>
