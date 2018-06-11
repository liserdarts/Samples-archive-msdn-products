# Lync 2010: Create and Use a Lync Silverlight Control
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Silverlight
* Microsoft Lync 2010
## Topics
* Controls
* SDK
* application development
## IsPublished
* True
## ModifiedDate
* 2012-02-06 02:36:45
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This Microsoft Lync 2010 SDK sample contains the Microsoft Silverlight Class Library code that is used in the
<a href="http://channel9.msdn.com/posts/Create-and-Use-a-Lync-Silverlight-Control">
Create and Use a Lync Silverlight Control</a> video demonstration. Use this code and the following procedures to create and deploy your own custom Lync 2010 Silverlight control.</span></p>
<h1><span>Prerequisites</span></h1>
<ul>
<li><span style="font-size:small"><a href="http://www.microsoft.com/download/en/details.aspx?id=18898">Microsoft Lync 2010 SDK</a></span>
</li><li><span style="font-size:small"><a href="http://www.microsoft.com/download/en/details.aspx?id=7335">Microsoft Silverlight 4 SDK</a></span>
</li><li><span style="font-size:small"><a href="http://www.microsoft.com/download/en/details.aspx?id=18149">Microsoft Silverlight 4 Tools for Visual Studio 2010</a></span>
</li></ul>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">This code sample provides the basis for a custom Silverlight Lync control. For more information, see the following procedures and the More Information section.</span></p>
<h2><span style="font-size:small">To use the Silverlight Lync 2010 control in a new application</span></h2>
<ol>
<li><span style="font-size:small">In Microsoft Visual Studio 2010 development system, open and build the sample project, which uses the Silverlight Class Library Application template.</span>
</li><li><span style="font-size:small">Open a new project by using the Silverlight Application template.</span>
</li><li><span style="font-size:small">Copy the libraries from the Bin/Debug folder in the class library project, to the Silverlight application folder.</span>
</li><li><span style="font-size:small">In the Silverlight application, right-click the
<strong>Toolbox</strong>, select <strong>Choose Items</strong>, and then add the class library application .dll file.</span>
</li><li><span style="font-size:small">Build the Silverlight application.</span> </li><li><span style="font-size:small">In the Toolbox, find the Silverlight control that is added in step 4.</span>
</li><li><span style="font-size:small">Drag the control to the XAML Design pane.</span>
</li><li><span style="font-size:small">Build and run the new Silverlight application.</span>
</li></ol>
<h2><span style="font-size:small">To deploy the new application to another computer</span></h2>
<ul>
<li><span style="font-size:small">Copy the Silverlight application .xap file and .html file to a web server.&nbsp;</span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small">Channel 9 Video: <a href="http://channel9.msdn.com/posts/Create-and-Use-a-Lync-Silverlight-Control">
Create and Use a Lync Silverlight Control</a></span> </li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/lync/default.aspx">Lync Developer Center</a></span>
</li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/gg421054.aspx">
Lync 2010 SDK Documentation</a></span><em><br>
</em></li></ul>
