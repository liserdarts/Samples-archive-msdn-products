# Caching API Sample: Getting started with Windows Azure Caching APIs
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* Caching Service
## Topics
* Caching
## IsPublished
* True
## ModifiedDate
* 2011-11-14 01:50:42
## Description

<h1>Introduction</h1>
<p><em><span class="style3">This sample shows how to use various Windows Azure&nbsp;Caching APIs.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
Note: Samples are provided for educational purposes only. They are not intended to be used in a production environment and have not been&nbsp;tested in a production environment. Microsoft does not provide technical support&nbsp;for these samples.
</span></em></p>
<h1><span>Building the Sample</span></h1>
<p>Before you run this sample, you must provision a cache on the portal (<a href="https://portal.windows.net/">https://portal.windows.net/</a>). The cache endpoint URI and the Authentication Token for the cache you have provisioned need to be put in the PrepareClient()
 method in the Program.cs source file. For more details on how to provision a cache on the portal, see
<a href="http://go.microsoft.com/fwlink/?LinkId=202670">http://go.microsoft.com/fwlink/?LinkId=202670</a>.<br>
&nbsp;After you obtain a cache endpoint URI and Authentication Token, use the following steps to update the sample:
<br>
1.Open the Program.cs source file, and navigate to the PrepareClient() method.<br>
2.Assign the cache endpoint URI to the hostName string.<br>
3.Assign the Authentication Token to the authenticationToken string.<br>
4.Build the solution in Visual Studio or from the command line</p>
<p>&nbsp;<span style="font-size:20px; font-weight:bold">Description</span></p>
<p>This sample exercises the following caching capabilities:<br>
1. Simple Add/Get to the default cache.<br>
2. Add/Get/GetAndLock/GetIfNewer/Put/PutAndUnlock to the default cache/region. This includes the following variations:<br>
&nbsp;1.GetAndLock trying to access a locked object.<br>
&nbsp;2.PutAndUnlock trying to unlock an object that is not locked.<br>
3. Add/Get/Put of a versioned object.<br>
&nbsp;1. Explicitly modify an item only if versions match.<br>
&nbsp;2. Fail to modify an item if a newer item is available in the cache..&nbsp;&nbsp;&nbsp;</p>
