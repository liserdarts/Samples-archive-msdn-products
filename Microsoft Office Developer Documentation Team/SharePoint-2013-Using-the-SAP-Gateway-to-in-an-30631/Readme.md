# SharePoint 2013: Using the SAP Gateway to Microsoft in an app for SharePoint
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* apps for SharePoint
## Topics
* integrating SAP
## IsPublished
* True
## ModifiedDate
* 2014-11-06 06:47:04
## Description

<div class="summary">
<p><strong><span class="label">Summary:</span>&nbsp;&nbsp;</strong>This sample surfaces SAP data in provider-hosted app for SharePoint by means of the SAP Gateway to Microsoft (GWM). Authentication and authorization is by means of Microsoft Azure Active Directory.
 The languages used are C# and ASP.NET markup. The tools used to setup the sample are Visual Studio and the Azure Portal.</p>
</div>
<div class="introduction">
<p><strong>Last modified:&nbsp;</strong>August 04, 2014</p>
<p><em><strong>Applies to:&nbsp;</strong>SharePoint Online</em></p>
<p><strong>In this article</strong><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/rickki_SAP_GWM/ReadMe.htm#sectionSection0">Prerequisites</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/rickki_SAP_GWM/ReadMe.htm#O15Readme_components">Key components of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/rickki_SAP_GWM/ReadMe.htm#O15Readme_config">Register and modify the sample for your development environment</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/rickki_SAP_GWM/ReadMe.htm#sectionSection3">Run and test the sample</a></p>
<p>The sample includes a remote ASP.NET page that makes a REST/OData call to a SAP endpoint by means of the GWM.</p>
</div>
<p><a name="sectionSection0"></a></p>
<h2 class="heading">Prerequisites</h2>
<div class="section" id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2013 Update 2 or later.</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2013 March, 2014, version or later. (This is included in Update 2 of Visual Studio 2013.)</p>
</li><li>
<p>A Office 365 Developer Site in an Office 365 domain that is associated with a Azure AD tenancy. See&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/fp179924(v=office.15).aspx" target="_blank">Sign up for an Office 365 Developer Site, set up
 your tools and environment, and start deploying apps</a>&nbsp;or<a href="http://msdn.microsoft.com/en-us/library/office/jj692554(v=office.15).aspx" target="_blank">How to: Create a Developer Site within your existing Office 365 subscription</a>.</p>
</li><li>
<p>A Gateway for Microsoft (GWM) deployed and configured in Microsoft Azure. See the document&nbsp;<strong>SAP Gateway to Microsoft Implementation Guide</strong>.</p>
</li><li>
<p>An organization account in Microsoft Azure. See&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/dn605894(v=office.15).aspx#bk_CreateOrganizationAccount" target="_blank">Create an organizational user account</a>.</p>
</li><li>
<p>A SAP OData endpoint with sample data in it. See the document&nbsp;<strong>SAP Gateway to Microsoft Implementation Guide</strong>.</p>
</li></ul>
</div>
<p><a name="O15Readme_components"></a></p>
<h2 class="heading"><a name="O15Readme_components">Key components of the sample</a></h2>
<p><a name="O15Readme_components"></a></p>
<p><a name="O15Readme_components"></a></p>
<p><a name="O15Readme_components"></p>
<div class="section" id="sectionSection1">
<p>The Visual Studio solution contains the following:</p>
<ul>
<li>
<p><strong>SAP2SharePoint</strong>&nbsp;project, which contains no significant SharePoint-hosted components.</p>
</li><li>
<p><strong>SAP2SharePointWeb</strong>&nbsp;project, which contains the following components:</p>
<ul>
<li>
<p>Default.aspx page and associated C# code behind page.</p>
</li><li>
<p>A C# file that defines the data model class.</p>
</li><li>
<p>A C# class file that defines a helper class for handling Azure AD authorization.</p>
</li><li>
<p>Two C# Microsoft Azure Access Control Service (ACS) authorization helper files. These are added to the project automatically by the Office Developer Tools for Visual Studio 2013 and they are not used in this sample, but they can be used if you extend the
 sample to have it get data from SharePoint as well as SAP.</p>
</li></ul>
</li></ul>
</div>
</a><a name="O15Readme_config">
<h2 class="heading">Register and modify the sample for your development environment</h2>
</a>
<p></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="section" id="sectionSection2"><a name="O15Readme_config">
<p>The following procedures need to be carried out before the sample can work with your Azure AD tenancy and SAP endpoint.</p>
<h3 class="procedureSubHeading">Modify Visual Studio solution and the code and markup in the application</h3>
<div class="subSection">
<ol>
<li>
<p>Unzip the sample and open the *.sln file in Visual Studio.</p>
</li><li>
<p>Update the&nbsp;<strong>SiteUrl</strong>&nbsp;property of the&nbsp;<span class="ui">SAP2SharePoint</span>&nbsp;project with the URL of your Office 365 Developer Site.</p>
</li><li>
<p>Open the web.config file of the ASP.NET project and change the &quot;localhost:<em>&lt;port&gt;</em>&quot; part of the value of the&nbsp;<span class="keyword">AppRedirectUrl</span>&nbsp;key in the&nbsp;<span class="keyword">appSettings</span>&nbsp;section to the
 value of the&nbsp;<span class="ui">SSL URL</span>&nbsp;property of the<span class="ui">SAP2SharePointWeb</span>&nbsp;project. (It may already be the same.)</p>
</li><li>
<p>Set the value of the&nbsp;<span class="keyword">Authority</span>&nbsp;key to the Office 365 domain of your organization account. For example,&nbsp;<span class="code">my_domain.onmicrosoft.com</span>. Note this is a onmicrosoft.com domain, not a sharepoint.com
 domain.</p>
</li><li>
<p>Set the value of the&nbsp;<span class="keyword">ResourceUrl</span>&nbsp;key to the APP ID URI of your GWM application as it is registered in Azure AD. Obtain this value from the GWM administrator. The following is an example.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>&lt;add key=&quot;ResourceUrl&quot; value=&quot;http://gwmdemo.cloudapp.net/&quot; /&gt;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Open the DataModel.cs class and change the fields of the&nbsp;<span class="code">DataModel</span>&nbsp;class to match the data that you will be getting from the SAP OData endpoint. For example, if you are getting event data from SAP with name, date, and
 location values, you can create fields in the class for Name, Date, and Location. Delete fields from the class that do not apply to your data.</p>
</li><li>
<p>Open the Default.aspx.cs file and change the value of the&nbsp;<span class="code">SAP_ODATA_URL</span>&nbsp;constant to the stem of your SAP OData endpoint.</p>
</li><li>
<p>In the same file, change the parameter that is passed to the&nbsp;<span class="code">GetSAPData</span>&nbsp;method to an OData query that is appropriate for the data you will be getting from SAP. For example, if you are getting information from a dataset
 called Events, you could use&nbsp;<span class="code">Events?$select=name,date,location</span></p>
</li><li>
<p>In the same method, find the line &quot;<span class="code">return item.Brand &#43; &quot; &quot; &#43; item.Model &#43; &quot; &quot; &#43; item.Price;</span>&quot;. Change the property names to match the property names you used in the DataModel class; for example &quot;<span class="code">return item.Name
 &#43; &quot; &quot; &#43; item.Date &#43; &quot; &quot; &#43; item.Location;</span>&quot;. (You can, of course, display more or fewer properties.)</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">Register the ASP.NET application with Azure AD</h3>
</a>
<div class="subSection"><a name="O15Readme_config"></a>
<ol>
<a name="O15Readme_config"></a>
<li><a name="O15Readme_config"></a>
<p><a name="O15Readme_config">Login into&nbsp;</a><a href="https://manage.windowsazure.com/" target="_blank">Azure Management portal</a>&nbsp;with your Azure administrator account.</p>
</li><li>
<p>Choose&nbsp;<span class="ui">Active Directory</span>&nbsp;on the left side.</p>
</li><li>
<p>Click on the directory that GWM is using.</p>
</li><li>
<p>Choose&nbsp;<span class="ui">APPLICATIONS</span>&nbsp;(on the top navigation bar).</p>
</li><li>
<p>Choose&nbsp;<span class="ui">ADD</span>&nbsp;on the toolbar at the bottom of the screen.</p>
</li><li>
<p>On the dialog that opens, choose&nbsp;<span class="ui">Add an application my organization is developing</span>.</p>
</li><li>
<p>On the&nbsp;<span class="ui">ADD APPLICATION</span>&nbsp;dialog, give the application a name that is appropriate for the type of data the sample app for SharePoint will be getting from SAP. For example, if you will use the sample to get company events
 data from SAP, use &quot;Contoso Events.&quot;</p>
</li><li>
<p>Choose&nbsp;<span class="ui">Web Application And/Or Web API</span>&nbsp;as the application type, and then click the right arrow button.</p>
</li><li>
<p>On the second page of the dialog, use the SSL debugging URL from the ASP.NET project in the Visual Studio solution as the value for&nbsp;<span class="ui">SIGN-ON URL</span>. You can find the URL using the following steps.</p>
<ol>
<li>
<p>Highlight the ASP.NET project in&nbsp;<span class="ui">Solution Explorer</span>.</p>
</li><li>
<p>In the&nbsp;<span class="ui">Properties</span>&nbsp;window, copy the value of the&nbsp;<span class="ui">SSL URL</span>&nbsp;property. An example is&nbsp;<span class="input">https://localhost:44300/</span>.</p>
</li><li>
<p>Paste it into the&nbsp;<span class="ui">SIGN-ON URL</span>&nbsp;on the&nbsp;<span class="ui">ADD APPLICATION</span>&nbsp;dialog.</p>
</li></ol>
</li><li>
<p>For the&nbsp;<span class="ui">APP ID URI</span>, give the application a unique URI, such as the application name appended to the end of the SSL URL; for example&nbsp;<span class="input">https://localhost:44300/ContosoEvents</span>.</p>
</li><li>
<p>Click the checkmark button. The Azure dashboard for the application opens with a success message.</p>
</li><li>
<p>Choose&nbsp;<span class="ui">CONFIGURE</span>&nbsp;on the top of the page.</p>
</li><li>
<p>Scroll to the&nbsp;<span class="ui">CLIENT ID</span>&nbsp;and make a copy of it. You will need it for a later procedure.</p>
</li><li>
<p>In the&nbsp;<span class="ui">keys</span>&nbsp;section, create a key. It won't appear initially. Click&nbsp;<span class="ui">SAVE</span>&nbsp;at the bottom of the page and the key will be visible. Make a copy of it. You will need it for a later procedure.</p>
</li><li>
<p>Scroll to&nbsp;<span class="ui">permissions to other applications</span>&nbsp;and select your GWM service application.</p>
</li><li>
<p>Open the&nbsp;<span class="ui">Delegated Permissions</span>&nbsp;drop down list and enable the boxes for the permissions to the GWM service that your app for SharePoint will need.</p>
</li><li>
<p>Click&nbsp;<span class="ui">Save</span>&nbsp;at the bottom of the screen.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">Configure the web.config with the Azure AD client ID and client key</h3>
<div class="subSection">
<ol>
<li>
<p>In Visual Studio, return to the web.config file. Insert the client ID that you saved from your Azure AD directory in the earlier procedure as the value of the&nbsp;<span class="keyword">ida:ClientID</span>&nbsp;key. Leave the casing and punctuation exactly
 as you copied it and be careful not to include a space character at the beginning or end of the value. For the&nbsp;<span class="keyword">ida:ClientKey</span>&nbsp;key use the&nbsp;<em>key</em>&nbsp;that you saved from the directory. Again, be careful not
 to introduce any space characters or change the value in anyway. The&nbsp;<span class="code">&lt;appSettings&gt;</span>&nbsp;section should now look something like the following. (The&nbsp;<span class="keyword">ClientId</span>&nbsp;key may have a GUID
 or no value at all.)</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>&lt;appSettings&gt;
  &lt;add key=&quot;ClientId&quot; value=&quot;&quot; /&gt;
  &lt;add key=&quot;ClientSecret&quot; value=&quot;LypZu2yVajlHfPLRn5J2hBrwCk5aBOHxE4PtKCjIQkk=&quot; /&gt;
  &lt;add key=&quot;ida:ClientID&quot; value=&quot;4da99afe-08b5-4bce-bc66-5356482ec2df&quot; /&gt;
  &lt;add key=&quot;ida:ClientKey&quot; value=&quot;URwh/oiPay/b5jJWYHgkVdoE/x7gq3zZdtcl/cG14ss=&quot; /&gt;
  &lt;add key=&quot;Authority&quot; value=&quot;gwmdemo.onmicrosoft.com&quot; /&gt;
  &lt;add key=&quot;AppRedirectUrl&quot; value=&quot;https://localhost:44322/Pages/Default.aspx&quot; /&gt;
  &lt;add key=&quot;ResourceUrl&quot; value=&quot;http://gwmdemo.cloudapp.net/&quot; /&gt;
&lt;/appSettings&gt;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Save and close the web.config file.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Tip</strong></th>
</tr>
<tr>
<td>
<p>Do not leave the web.config file open when you run the Visual Studio debugger (F5). The Office Developer Tools for Visual Studio change the&nbsp;<span class="keyword">ClientId</span>&nbsp;value (not the&nbsp;<span class="keyword">ida:ClientID</span>)
 every time you press F5. This requires you to respond to a prompt to reload the web.config file, if it is open, before debugging can execute.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li></ol>
</div>
</div>
<p><a name="sectionSection3"></a></p>
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection3">
<p>You can test the sample with the Visual Studio debugger.</p>
<div class="subSection">
<ol>
<li>
<p>Press F5 in Visual Studio.</p>
</li><li>
<p>The first time that you use F5, you may be prompted to login to the Office 365 Developer Site that you are using. Use the site administrator credentials.</p>
</li><li>
<p>The first time that you use F5, you are prompted to grant permissions to the app. Click&nbsp;<span class="ui">Trust It</span>.</p>
</li><li>
<p>The app will launch and SharePoint will redirect to the Default.aspx page at your &quot;localhost:<em>&lt;port&gt;</em>&quot; domain. After a brief delay while the access token is being obtained, the Default.aspx page opens. Verify that the SAP data appears.</p>
</li></ol>
</div>
</div>
