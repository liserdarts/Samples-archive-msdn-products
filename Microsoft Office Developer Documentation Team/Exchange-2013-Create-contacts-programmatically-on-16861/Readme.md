# Exchange 2013: Create contacts programmatically on Exchange servers
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* Exchange Online
* Microsoft Exchange Server 2007
* Microsoft Exchange Server 2010
* Microsoft Exchange Server 2013
## Topics
* Contact
## IsPublished
* True
## ModifiedDate
* 2013-07-22 03:18:54
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows you how to use the Exchange Web Services (EWS) Managed API to create a contact and populate it with contact information.</span></p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><span style="font-size:small">This sample authenticates an email address and password entered from the console, creates a contact, populates the contact object with contact information, and saves the contact to the Exchange server.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">A target server that is running a version of Exchange starting with Exchange Server 2007 Service Pack 1 (SP1), including Exchange Online as part of Office&nbsp;365.</span>
</li><li><span style="font-size:small">The .NET Framework 4.</span> </li><li><span style="font-size:small">The EWS Managed API assembly file, Microsoft.Exchange.WebServices.dll. You can download the assembly from the
<a href="http://go.microsoft.com/fwlink/?LinkID=255472">Microsoft Download Center</a>.</span>
</li></ul>
<p><span style="font-size:small">Note: </span><span style="font-size:small">This sample assumes that the assembly is in the default download directory. You will need to verify the path before you run the solution.</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2010 with the Visual Web Developer and C# components and an open Visual Studio 2010 solution.</span><br>
<span style="font-size:small">Or</span> </li><li><span style="font-size:small">A text editor to create and edit source code files and a command prompt window to run a .NET Framework command line compiler.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The Exchange 2013: Create contacts programmatically on Exchange servers sample contains the following files:</span></p>
<ul>
<li><span style="font-size:small">Ex15CreateContact.sln &mdash; The Visual Studio 2010 solution file for the Ex15CreateContact project.</span>
</li><li><span style="font-size:small">Ex15CreateContact.csproj &mdash; The Visual Studio 2010 project file for the
<strong>CreateContact </strong>function.</span> </li><li><span style="font-size:small">Ex15CreateContact.cs &mdash; Contains the using statements, namespace, class, and functions to create a contact and populate it with information.</span>
</li><li><span style="font-size:small">app.config &mdash; Contains configuration data for the Ex15CreateContact project.</span>
</li><li><span style="font-size:small">Authentication.csproj &mdash; The Visual Studio 2010 project file for the dependent authentication code.</span>
</li><li><span style="font-size:small">TextFileTraceListener.cs &mdash; Contains the using statements, namespace, class, and code to write the XML request and response to a text file.</span>
</li><li><span style="font-size:small">Service.cs &mdash; Contains the using statements, namespace, class, and functions necessary to acquire the ExchangeService object used in the Ex15CreateContact project.</span>
</li><li><span style="font-size:small">CertificateCallback.cs &mdash; Contains the using statements, namespace, class, and code to acquire an X509 certificate.</span>
</li><li><span style="font-size:small">UserData.cs &mdash; Contains the using statements, namespace, class, and functions necessary to acquire user information required by the service object.</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the Exchange 2013: Create contacts programmatically on Exchange servers sample.</span></p>
<ol>
<li><span style="font-size:small">Set the startup project to Ex15CreateContact by selecting the project in the Solution Explorer and choosing &quot;Set as StartUp Project&quot; from the Project menu.</span>
</li><li><span style="font-size:small">Insure the reference path for the Microsoft.Exchange.WebServices.dll points to where the DLL is installed on your local computer.</span>
</li></ol>
<h1>Build the sample</h1>
<ul>
<li><span style="font-size:small">Press F5 to build and deploy the sample.</span>
</li><li><span style="font-size:small">Run and test the sample</span> </li><li><span style="font-size:small">Press F5 to run the sample.</span> </li></ul>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://go.microsoft.com/fwlink/?LinkId=301827">Get Started with the EWS Managed API</a></span>
</li><li><span style="font-size:small"><a href="http://code.msdn.microsoft.com/Exchange-2013-Find-b1659b4d">Exchange 2013: Find contacts by their display names programmatically</a>
</span></li><li><span style="font-size:small"><a href="http://code.msdn.microsoft.com/Exchange-2013-Update-51ecf8e0">Exchange 2013: Update contacts programmatically on Exchange servers</a>
</span></li></ul>
