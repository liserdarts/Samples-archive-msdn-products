# Reference Service - VS 2013
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2013
## Topics
* Extensibility
## IsPublished
* True
## ModifiedDate
* 2014-05-29 06:43:19
## Description

<div id="longDesc">
<p>This sample shows how to be a Visual Studio service provider and how to consume Visual Studio services.</p>
<h2>Introduction:</h2>
<p><br>
There are three samples associated with this project, one for each programming language.<br>
<br>
<strong>C&#43;&#43;</strong><br>
<br>
The sample contains three main classes. A service provider package called ServiceProviderPackage,<br>
a service consumer package called ServiceConsumerPackage, and a local service provider called<br>
LocalServiceProvider, which belongs to the service provider package.<br>
<br>
ServiceProviderPackage registers its service in the Visual Studio registry so that it will be<br>
loaded when any package queries for the service. In this sample ServiceConsumerPackage will<br>
do this. Additionally, ServiceProviderPackage offers this service to Visual Studio when it is<br>
loaded. Simply registering the service in the Visual Studio registry is insufficient to allow a<br>
client to successfully query for it.<br>
<br>
ServiceConsumerPackage registers its menu and provides three menu items on the Visual Studio Tools<br>
menu entitled:<br>
<br>
</p>
<ul>
<li>&quot;VSSDK C&#43;&#43; Execute Global Service&quot; </li><li>&quot;VSSDK C&#43;&#43; Execute Local Service&quot; </li><li>&quot;VSSDK C&#43;&#43; Execute Local using Global Service&quot; </li></ul>
<p><br>
The command handlers for these menu items are provided by ServiceConsumerPackage. Each handler<br>
attempts to query for either the globally offered service provided by ServiceProviderPackage<br>
or the local service provided by ServiceProviderPackage. Querying for the local service is<br>
expected to fail. Utilizing each menu item will cause a text string to be sent to the Output<br>
window. (NOTE - There is a bug in the Output window. If you don't see the text, then simply restart Visual Studio).<br>
<br>
<br>
<strong>C# and VB</strong><br>
<br>
This sample shows how to create and expose services inside Visual Studio. It creates two services with two
<br>
different levels of visibility. The service with global visibility is available for any of Visual Studio's other components. The service with
<br>
local visibility is available only from within the package itself or when the IServiceProvider interface
<br>
implemented by the package is inside the chain of active providers.<br>
<br>
The sample creates two assemblies, one with the definition of the interfaces used and one with the actual
<br>
implementation of the packages and services. The assembly with the implementation, created by the Reference.Services
<br>
project, defines two packages. One package exposes the services and a second package uses them. The services are
<br>
implemented with two helper classes.<br>
<br>
The part of interest about how to proffer services is the code inside the ServicesPackage class. Specifically:<br>
<br>
1. In the declaration of the class we use the ProvideServiceAttribute registration attribute defined in the
<br>
Managed Package Framework to add the information about the global service proffered
<br>
by the package to the registry.<br>
<br>
2. In the constructor of the package we add the types of the proffered services to the list of the services<br>
provided by the package. Notice that at this point we don&rsquo;t create any instance of the service, but we
<br>
provide a callback function that will be called the first time a client queries for a specific service.
<br>
We do this to optimize performance. We don&rsquo;t want to construct something that might never
<br>
be used.<br>
<br>
3. The callback function used to create a new instance of the services.<br>
<br>
<br>
<br>
<br>
<br>
</p>
<h3>Screenshot:</h3>
<h3><img id="115678" src="/vstudio/site/view/file/115678/1/Services.jpg" alt="" width="1211" height="800"></h3>
<h3>Requirements:</h3>
<ul>
<li>Visual Studio 2013 </li><li>Visual Studio 2013 SDK </li></ul>
<p>&nbsp;</p>
<h3>Download and install:</h3>
<ul>
<li>Download the zip file associated with the sample </li><li>Unzip the sample to your machine </li><li>Double click on the .sln file to launch the solution </li></ul>
<p>&nbsp;</p>
<h3>Building and running the sample</h3>
<ul>
<li>To build and execute the sample, press F5 after the sample is loaded </li><li>Add any other special information here.&nbsp; </li></ul>
</div>
