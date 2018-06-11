# Create a UCWA Windows Store app using C#/XAML and XML
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Lync Server 2013
* Lync 2013
## Topics
* Unified Communications Web API
## IsPublished
* True
## ModifiedDate
* 2014-02-04 05:44:24
## Description

<div class="content">
<div>
<div class="topic">
<div class="majorTitle"></div>
<h1 class="title">Start creating UCWA Windows Store apps</h1>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p>In this article, you will learn how to build a simple Windows Store app in C#/XAML and XML. The steps illustrated here are required of all UCWA applications. It is intended as a self-contained introduction to building and deploying a UCWA application targeted
 for Windows Store.</p>
</div>
<div class="introduction">
<p><strong>Last modified: </strong>March 18, 2014</p>
<p><em><strong>Applies to: </strong>Lync 2013&nbsp;| Lync Server 2013</em></p>
<p><strong>In this article</strong><br>
<a href="#sectionSection0">What is involved in signing in a user to UCWA?</a><br>
<a href="#sectionSection1">In this article</a><br>
<a href="#bk_addresources">Additional resources</a><br>
</p>
<p>Demonstrated UCWA programming features include how to make asynchronous calls to sign in and then to get the self-presence, personal note and the list of telephone numbers of the signed-in user. Some basic requirements of Windows Store apps are also examined
 in the context of a UCWA application. Specifically, you will learn how to carry out the following programming tasks:</p>
<ul>
<li>
<p><strong>Create and Test a Windows Store application using C#/XAML</strong></p>
<p>The application has a UI component based on the basic Windows Store app UI template. It takes a user name and password from a user and logs the user in to UCWA. The application takes advantage of the Windows Store app API features to cache or persist the
 user login information and other application data to render fluid user experiences.</p>
<p>Creating and deploying Windows Store apps involves a somewhat different process than a Windows desktop application. This tutorial is self-contained and does not require extensive prior knowledge of using Windows Store app APIs. However, if you’re new to
 building Windows Store apps, you may find it helpful to read the articles listed in the
<a href="#bk_addresources">Additional resources</a> section.</p>
</li><li>
<p><strong>Sign in to UCWA and inspect the local user data and presence</strong></p>
<p>Sign-in is the first step that every UCWA application must perform before other UCWA features become available. The process involves discovering the UCWA service address, getting the user authenticated, and creating a UCWA
<span class="code">application</span> resource.</p>
<p>Programming using UCWA involves initiating HTTP requests and processing responses to access and operate UCWA resources. To do so, this application uses
<a href="http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest(v=vs.110).aspx" target="_blank">
HttpWebRequest</a> and <a href="http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse(v=vs.110).aspx" target="_blank">
HttpWebResponse</a> as well as other classes in the <a href="http://msdn.microsoft.com/en-us/library/System.Net(v=vs.110).aspx" target="_blank">
System.Net namespace</a> for Windows Store apps. To showcase the versatility of UCWA, the application chooses XML as the content type for transporting the HTTP messages and uses the
<a href="http://msdn.microsoft.com/en-us/library/system.xml.linq(v=vs.110).aspx" target="_blank">
System.Xml.Linq</a> namespace to parse the UCWA resources. </p>
</li></ul>
</div>
<a id="sectionSection0"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">What is involved in signing in a user to UCWA?</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle0"></a>
<p>Signing in is the first step to create any UCWA application. It amounts to getting a user authenticated using one of the following authentication types:</p>
<ul>
<li>
<p>Using the user-supplied Lync account name and password</p>
</li><li>
<p>Using integrated Windows credentials</p>
</li><li>
<p>Using user’s account name and a conference ID for joining a meeting anonymously</p>
</li><li>
<p>Using passive authentication supported by Active Directory Federation Service</p>
</li></ul>
<p>In this article, the password-based authentication is used. Once the user is authenticated, the application is returned an OAuth security token. The UCWA application must provide this security token in any of the subsequent HTTP requests. When the token
 expires, the application must be renew it.</p>
<p>Before the authentication can take place, the UCWA application must first locate the UCWA service enabled on the user’s domain. The process is known as auto-discovery of the UCWA
<a href="http://ucwa.lync.com/documentation/GettingStarted-RootURL" target="_blank">
root</a> resource and will be illustrated in this article.</p>
<p>A successful authentication must also be followed by obtaining a UCWA <a href="http://ucwa.lync.com/documentation/Resources-application" target="_blank">
application</a> resource representing an instance of the application bound to the local endpoint of the signed-in user. When the application times out, the
<span class="code">application</span> resource must be updated. This may involve getting the user re-authenticated.</p>
<p>Because UCWA-specific operations are carried out as HTTP requests and responses, it is natural to encapsulate them in a separate type. In this article, they are exposed as asynchronous methods on a
<span class="code">Transport</span> class, which can be reused for other applications as well. The asynchronous behavior is built upon the
<span class="code">async/await</span><a href="http://msdn.microsoft.com/en-us/library/vstudio/hh191443.aspx" target="_blank">pattern</a>. It is required to ensure that the Windows Store app be responsive.</p>
<p>A Windows Store app undergoes a much frequent change of states than traditional desktop applications. Managing the application’s states and life cycle is important to enable the Windows Store app fluid and to comply with the REST-ful requirements of UCWA.</p>
</div>
<a id="sectionSection1"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">In this article</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle1"></a>
<p>This article contains the following topics.</p>
<ul>
<li>
<p><span><a href="http://msdn.microsoft.com/EN-US/library/dn551188.aspx">Create a UCWA Windows Store app project</a></span></p>
</li><li>
<p><span><a href="http://msdn.microsoft.com/EN-US/library/dn551189.aspx">Enable fluid user interface</a></span></p>
</li><li>
<p><span><a href="http://msdn.microsoft.com/EN-US/library/dn551193.aspx">Ensure responsive HTTP operations</a></span></p>
</li><li>
<p><span><a href="http://msdn.microsoft.com/EN-US/library/dn551191.aspx">Implement the UCWA sign-in workflow</a></span></p>
</li><li>
<p><span><a href="http://msdn.microsoft.com/EN-US/library/dn551194.aspx">Putting it all together</a></span></p>
</li></ul>
</div>
<a id="bk_addresources"></a>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">Additional resources</span>
<div class="LW_CollapsibleArea_HrDiv">
<hr class="LW_CollapsibleArea_Hr">
</div>
</div>
<a id="sectionToggle2"></a>
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh974581.aspx" target="_blank">Create your first Windows Store app using C# or Visual Basic</a>: If you’re new to building Windows Store apps, I highly recommend this step-by-step guide with clear
 instructions to creating a Windows RT application using C# or Visual Basic.</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh974578.aspx" target="_blank">Get a developer license (Windows Store apps)</a>: To install and test a Windows RT application before submitting it to Windows Store, you can get a developer license
 for each machine on which you intent to run the app. This article tells you how to get and renew a developer license, valid for a 30-day period, from Visual Studio or at a command prompt.</p>
</li><li>
<p><a href="http://technet.microsoft.com/en-us/library/hh852635.aspx" target="_blank">How to Add and Remove Apps</a>: This article details the requirements and steps necessary for deploying an enterprise (or LOB) application using the mechanism of side-loading
 and, therefore, bypassing Windows Store all together. This could be a common scenario for UCWA apps.</p>
</li><li>
<p><a href="http://technet.microsoft.com/en-us/library/hh832040.aspx" target="_blank">Manage Client Access to the Windows Store</a>: In addition to configuring side-loading for deploying your Windows RT application without going through Windows Store, this
 article provides an entry point to other information useful for managing app access to Windows Store in an enterprise environment.</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh986968.aspx" target="_blank">Manage app lifecycle and state (Windows Store apps using C#/VB and XAML)</a>: It offers an introductory discussion of Windows app lifecycle and state management
 issues, with a hands-on tutorial.</p>
</li><li>
<p><a href="http://ucwa.lync.com/documentation/api-reference" target="_blank">UCWA API Resource reference library</a>: This is the official UCWA documentation site where you can find detailed specification of all the UCWA resources.</p>
</li><li>
<p><a href="http://ucwa.lync.com/" target="_blank">UCWA Dev Portal</a>: This is the official UCWA development portal where you can find interactive demo and sample code, written in JavaScript and JSON. You can also access the community forum for UCWA developers.
</p>
</li><li>
<p><a href="http://ucwa.lync.com/documentation/what-is-lync-ucwa-api" target="_blank">What is UCWA API?</a>: This is the landing page of the official UCWA documentation, where you can find more detailed discussions of the requirements, features and architecture
 of UCWA. </p>
</li></ul>
</div>
</div>
</div>
</div>
</div>
</div>
