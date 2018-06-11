# Outlook 2010: Outlook Social Connector (OSC) Provider Templates
## Requires
* Visual Studio 2008
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
* Outlook Social Connector
* OSC
## Topics
* OSC provider
## IsPublished
* True
## ModifiedDate
* 2011-08-02 03:43:23
## Description

<p>This project contains several Outlook Social Connector (OSC) provider templates (in C&#43;&#43;, C#, and Visual Basic) for Microsoft Outlook 2010, Microsoft Office Outlook 2007, and Microsoft Office Outlook 2003.</p>
<p>The provider templates are available for use when you build a provider in the language of your choice. To minimize the download size of the provider and to minimize provider load time, you should build a production provider by using C&#43;&#43;. If you are developing
 a provider for users who already have the .NET Framework 3.5 SP1 installed on their computers, you should also consider writing a managed provider by using Visual C# or Visual Basic. These samples accompany the article
<a href="http://msdn.microsoft.com/en-us/library/ff759418.aspx">OSC Sample Provider and Templates</a> in the
<a href="http://msdn.microsoft.com/en-us/library/ee829696.aspx">Outlook Social Connector 1.1 Provider Reference</a> in the MSDN library.</p>
<p><strong>To download the templates</strong></p>
<ol>
<li>Download the .zip file in Visual Basic, C#, or C&#43;&#43;. </li><li>Extract the .zip file into the folder of your choice. In Windows Vista or Windows 7, the default path for Visual Studio 2008 projects is C:\Users\user\Documents\Visual Studio 2008\Projects. If you are using Microsoft Visual Studio 2010, the default path
 is C:\Users\user\Documents\Visual Studio 2010\Projects. </li><li>After extracting the .zip file of your choice, you will find one the following projects in your projects folder:
<ul>
<li>OL2010OSCProvider_CPP&mdash;Contains the C&#43;&#43; provider template. </li><li>OL2010OSCProvider_CS&mdash;Contains the C# provider template. </li><li>OL2010OSCProvider_VB&mdash;Contains the Visual Basic provider template. </li></ul>
</li></ol>
<p><strong>To apply a provider template</strong></p>
<ol>
<li>Change the project name and namespace in the template to your project name and namespace identifiers.
</li><li>Modify the <strong>AssemblyInfo</strong> class to specify the appropriate assembly information.
</li><li>Implement the interface members marked as <strong>To-Do</strong> and add more dependencies and references, as required.
</li><li>Build the project. </li><li>Ensure that the provider assembly <strong>ProgID</strong> is listed as a key under
<strong>HKEY_CURRENT_USER\Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders</strong>.
</li><li>To distribute the setup project, create a setup project in Visual Studio or a setup tool of your choice.
</li><li>
<p>Your setup project should complete COM registration for your assembly and also create the
<strong>ProgID</strong> key as listed in step 5.</p>
</li></ol>
