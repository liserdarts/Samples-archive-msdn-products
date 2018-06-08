# Windows Azure Diagnostics Sample
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* Microsoft Azure
## Topics
* Diagnostics
## IsPublished
* True
## ModifiedDate
* 2011-04-26 02:57:42
## Description

<p><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>Before you install and use&nbsp;Windows Azure&nbsp;Diagnostics Sample you must:</strong></span></p>
<ol>
<li><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>Review the&nbsp;Windows Azure&nbsp;Diagnostics Sample&nbsp;license terms by clicking&nbsp;the Custom link above.</strong></span>
</li><li><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>Print and retain a copy of the license terms for your records.</strong></span>
</li></ol>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>By downloading and using&nbsp;the&nbsp;Windows Azure&nbsp;Diagnostics Sample,&nbsp;you agree to such license terms.&nbsp; If you do not accept them, do not use the software.</strong></span></p>
<h1><span style="font-family:verdana,geneva; font-size:large"><strong>Introduction</strong></span></h1>
<p><span style="font-family:verdana,geneva; font-size:x-small">Windows Azure provides capabilities that allow you to diagnose data such as performance counters, event logs and IIS logs. The Windows Azure Diagnostics sample shows how to capture performance counter
 diagnostic data&nbsp;in your Windows Azure application.</span></p>
<p><span style="font-family:verdana,geneva; font-size:x-small">The process of implementing diagnostics in your application requires the following:</span></p>
<ul>
<li><span style="font-family:verdana,geneva; font-size:x-small">Importing the Diagnostics module into your service definition.</span>
</li><li><span style="font-family:verdana,geneva; font-size:x-small">Specifying the diagnostics connection string to use for your application.</span>
</li><li><span style="font-family:verdana,geneva; font-size:x-small">Setting configuration settings that determine which data will be monitored, at what interval will it be transferred to your storage account, and the maximum amount of file buffer size allocated
 to the data buffer.</span> </li></ul>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span class="LinkText"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">You
 can use the following information to understand and run the&nbsp;Diagnostics sample:</span></span></span></span></span></span></span></span></span></span></span></p>
<ul>
<li><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span class="LinkText"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/gg433048.aspx">Collecting
 Logging Data by Using Windows Azure Diagnostics</a></span></span></span></span></span></span></span></span></span></span></span>
</li><li><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span class="LinkText"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/hh180875.aspx">Code
 Quick Start: Capturing diagnostics in your Windows Azure application</a></span></span></span></span></span></span></span></span></span></span></span>
</li><li><a href="http://www.microsoft.com/windowsazure/sdk/"><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span class="LinkText"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span class="LinkText"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%">Windows
 Azure Tools</span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span class="LinkText"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">
</span></span></span></span></span></span></span></span></span></span></span></a></li></ul>
<h1><span style="font-size:large">Prerequisites</span></h1>
<ul>
<li><span style="font-size:x-small"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><strong>Windows Azure SDK</strong>
<span style="line-height:150%">&ndash; provides tools and files that are needed to complete the development process of an application for Windows Azure.</span></span></span></span></span></span></span></span>
</li><li><span style="font-size:x-small"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><strong>Visual
 Studio 2010</strong> <span style="line-height:150%">&ndash; the sample provides a Visual Studio 2010 project that you can use to deploy the sample.</span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span>
</li></ul>
<h1><strong>Building and Running the Sample</strong></h1>
<p><strong><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%">To run the sample from Visual Studio 2010</span></span></strong></p>
<ol>
<li><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%">Open Visual Studio 2010 as an administrator.</span></span></span></span></span>
</li><li><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="font-size:20px"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="font-size:x-small">Browse
 to the folder where you extracted the sample and open DiagnosticsSample.sln.</span></span></span></span></span></span></span></span></span></span>
</li><li><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="font-size:20px"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="font-size:x-small"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Press
 F6 to build the solution.</span></span></span></span></span></span></span></span></span></span></span></span></span></span></span>
</li><li><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="font-size:20px"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="font-size:x-small"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Press
 F5 to debug the solution. When you debug or run the application from Visual Studio, Visual Studio packages the application, starts the Windows Azure Compute Emulator<span style="line-height:150%">, deploys the application to the Compute Emulator<span style="line-height:150%">,</span></span></span></span></span></span></span><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">and
 launches the browser to </span></span></span></span></span></span></span><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">&nbsp;</span></span></span></span></span><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">display
 the default web page defined by the web role.</span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span></span>
</li></ol>
