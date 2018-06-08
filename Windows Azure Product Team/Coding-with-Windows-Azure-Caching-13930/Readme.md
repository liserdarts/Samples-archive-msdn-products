# Coding with Windows Azure Caching
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* ASP.NET
* Microsoft Azure
* Caching Service
## Topics
* Caching
## IsPublished
* True
## ModifiedDate
* 2011-12-13 12:02:50
## Description

<p>&lt;!--CTYPE html PUBLIC &quot;-//W3C//DTD XHTML 1.0 Transitional//EN&quot; &quot;http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dt--&gt;</p>
<p>&lt;!-- .style3 { background-color: #FFFFFF; } a:link {color:blue; text-decoration:underline; text-underline:single; } --&gt;</p>
<h1><strong><span style="font-size:24.0pt; line-height:115%">Windows Azure Caching API Cloud Sample
</span></strong></h1>
<p><span class="style3">This sample shows how to use Windows Azure Caching features to save session state data and output page caching for your ASP.NET web role. For more information about using Caching with ASP.NET applications, see
<a href="https://portal.appfabriclabs.com/" target="_blank">http://go.microsoft.com/fwlink/?LinkId=202673</a>.
<br>
<br>
Note: Samples are provided for educational purposes only. They are not intended to be used in a production environment and have not been tested in a production environment. Microsoft does not provide technical support for these samples.
</span></p>
<h2>Prerequisites</h2>
<p><span class="style3">This sample assumes that the Windows Azure SDK is installed and all Caching-related references are resolved in the sample's solution. The SDK can be installed from
<a href="http://go.microsoft.com/fwlink/?LinkId=216119">http://go.microsoft.com/fwlink/?LinkId=216119</a>. The Caching-related binaries can be found in the following path:
<br>
<br>
&lt;SDK install path&gt;\V1.0\Assemblies\NET4.0\Cache&nbsp;</span></p>
<h2>Sample Flow</h2>
<p><span style="font-size:12.0pt; line-height:115%">This sample has the following characteristics:</span></p>
<ol>
<li>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">There is Profile link on the Default.aspx page, once you click it will display Profile.aspx page.</span></p>
</li><li>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">If you have already signed in, it will show the user details such as user name, e-mail address and logged in time<br>
If not signed in, it will direct you to login.apsx. Please note that if you have not signed up earlier, you can register yourself using registration link provided from the login.aspx page.
</span></p>
</li></ol>
<div class="section" id="sectionSection4">
<h2 class="heading">Running the Sample</h2>
<div class="section" id="sectionSection5">
<p class="style3">Before you run this sample, <span style="font-size:12.0pt; line-height:115%">
you must provision a cache on the <a href="http://go.microsoft.com/fwlink/?LinkId=213161">
Windows Azure Management Portal</a>. The properties window for your cache contains necessary information for connecting this sample. The Caching service URL and the authentication token for the cache you have provisioned need to be put in the web.config file.
 For more details on how to provision a cache on the portal, see <a href="https://portal.appfabriclabs.com/" target="_blank">
http://go.microsoft.com/fwlink/?LinkId=202670</a>. <br>
<br>
<span style="font-size:12.0pt; line-height:115%">After you obtain a Caching service URL and authentication token, use the following steps to update the web.config file</span>:
</span></p>
<ol>
<li>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">Open the web.config file.</span></p>
</li><li>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">Navigate to the hosts element in the dataCacheClient element.</span></p>
</li><li>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">Change the name attribute of the host element to the value of the Caching service URL.</span></p>
</li><li>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">Place the authentication token in the authorizationInfo attribute of the messageSecurity element.</span></p>
</li></ol>
<p class="style3"><span style="font-size:12.0pt; line-height:115%">Build the solution in Visual Studio or from the command line, and then press F5 to debug the solution in the browser. This should open the Default.aspx page.</span></p>
</div>
<p><strong>Expected Output</strong></p>
<p>The page demonstrates the use of session state and output page caching. It stores logged in user information in session state for display purposes.</p>
</div>
