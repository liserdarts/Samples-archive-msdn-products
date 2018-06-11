# Lync 2010: Create and Use a Lync WPF Control
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010 SDK
* Microsoft Lync 2010
* Windows Presentation Foundation
## Topics
* WPF
## IsPublished
* True
## ModifiedDate
* 2012-01-13 03:03:26
## Description

<h2>Introduction</h2>
<p><span style="font-size:small">This Microsoft Lync 2010 SDK sample is the Microsoft Windows Presentation Foundation (WPF) User Control Library code that is used in the&nbsp;<a href="http://channel9.msdn.com/posts/Create-and-Use-a-Lync-WPF-Control ">Create
 and Use a Lync WPF Control</a>&nbsp;video demonstration. Use this code and the following procedures to create and deploy your own custom Lync 2010 WPF control.</span></p>
<h2>Prerequisites</h2>
<ul>
<li><span style="font-size:small"><a href="http://www.microsoft.com/download/en/details.aspx?id=18898 ">Microsoft Lync 2010 SDK</a></span>
</li><li><span style="font-size:small">Microsoft Lync 2010 SDK Runtime (LyncSdkRedist.msi). Included with the Lync 2010 SDK download.</span>
</li></ul>
<h2>Description</h2>
<p><span style="font-size:small">This code sample provides the basis for a custom WPF Lync 2010 control. For more information, see the following procedures and the items that appear in the More Information section.</span></p>
<h3>To use the WPF Lync 2010 control in a new application</h3>
<ol>
<li><span style="font-size:small">In Microsoft Visual Studio 2010 development system, open and build the sample project, which uses the WPF User Control Library Application template.</span>
</li><li><span style="font-size:small">Open a new project by using the WPF Application template.</span>
</li><li><span style="font-size:small">In the WPF application, right-click the <strong>
Toolbox</strong>, select <strong>Choose Items</strong>, and then add the user control library application .dll file.</span>
</li><li><span style="font-size:small">Build the WPF application.</span> </li><li><span style="font-size:small">In the Toolbox, find the WPF control that is added in step 3.</span>
</li><li><span style="font-size:small">Drag the control to the XAML Design pane.</span>
</li><li><span style="font-size:small">Build the new WPF application.</span> </li></ol>
<h3><span>To deploy the new application to another computer</span></h3>
<ol>
<li><span style="font-size:small">Install the Lync 2010 SDK Runtime on the target computer.</span>
</li><li><span style="font-size:small">Copy the following two files to the target computer.</span>
</li></ol>
<ul>
<li><span style="font-size:small">WPF application .exe file </span></li><li><span style="font-size:small">User Control Library application .dll file</span>
</li></ul>
<h2>More Information</h2>
<ul>
<li><span style="font-size:small">Channel 9 Video: <a href="http://channel9.msdn.com/posts/Create-and-Use-a-Lync-WPF-Control">
Create and Use a Lync WPF Control</a></span> </li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/bb514641(VS.90).aspx">
How to: Create a WPF User Control Library Project</a></span> </li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/bb514703(VS.90).aspx">
How to: Use a Third-Party WPF Control in a WPF Application</a></span> </li></ul>
