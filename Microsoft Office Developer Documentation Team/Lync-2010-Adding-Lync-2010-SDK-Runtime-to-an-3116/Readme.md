# Lync 2010: Adding Lync 2010 SDK Runtime to an Application Installation Program
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010 SDK
* Microsoft Lync 2010
* Microsoft Lync 2010 API
## Topics
* Installation
* Deployment
* Bootstrap
## IsPublished
* True
## ModifiedDate
* 2012-02-14 03:21:09
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This article describes how to create an executable program that installs the MSDNArticleIM sample application. The MSDNArticleIM setup.exe file installs the Microsoft Lync 2010 SDK runtime as a prerequisite. After the prerequisite
 is installed, the MSDNArticleIM application is installed.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">An application that references Microsoft Lync 2010 API must install Microsoft.Office.Uc.dll before run time. To ensure that the Lync 2010 API runtime DLL is installed before a user starts the application, you can create a custom
 setup executable program that combines multiple dependent installation programs in one setup executable program.</span></p>
<p><span style="font-size:small">Before you install a Lync 2010 API runtime DLL, you must install the Microsoft Lync 2010 SDK LyncSDKRedist.msi. Microsoft Software License Terms are specified in LyncSDKRedist.msi. After LyncSDKRedist.msi is installed, the setup
 executable program can install the runtime DLLs.</span></p>
<p><span style="font-size:small">In this article, the MSDNArticleIM application shows how to develop an installation program that installs LyncSDKRedist.msi, Microsoft.Office.Uc.dll, and Microsoft.Lync.Model.dll. The LyncSDKRedist.msi file is chained with the
 default Visual Studio setup .msi file.</span></p>
<p>&nbsp;</p>
<h1>More Information</h1>
<p><span style="font-size:small">For more information, see the technical article published on MSDN,&nbsp;
</span><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/hh145598.aspx">Adding Lync 2010 SDK Runtime to an Application Installation Program</a>.</span></p>
