# Office 365: Use the SAP Gateway to Microsoft in a task pane app for Office
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* OAuth 2.0.
* apps for Office
* SAP Gateway (GWM)
## Topics
* Authentication
* Active Directory
* integrating SAP
* Authorization
* Windows web services
* data and storage
## IsPublished
* True
## ModifiedDate
* 2014-10-22 11:43:38
## Description

<div class="WordSection1">
<div>
<div>
<p><strong><span style="font-size:small">Office 365 and Office 2013: Use the SAP Gateway to Microsoft in a task pane app for Office</span></strong></p>
<p><span style="font-size:x-small"><strong>Summary:&nbsp;</strong>&nbsp;</span>This sample surfaces SAP data in an app for Office using the SAP Gateway to Microsoft (GWM). Authentication and authorization to GWM is provided using a OAuth 2.0 Authorization Code
 Grant flow through Microsoft Azure Active Directory. The languages used are C#, ASP.NET markup, and JavaScript. The tools used to setup the sample are Visual Studio and the Azure Portal.</p>
<p>The sample includes a remote ASP.NET page that makes a REST/OData call to a SAP endpoint by means of the GWM.</p>
<p><span style="font-size:x-small">&nbsp;</span></p>
<div>
<div class="introduction">
<p><strong>Last modified: </strong>October 20, 2014</p>
<a name="O15Readme_Prereq">
<h2 class="heading">Prerequisites</h2>
</a>
<div class="section" id="sectionSection0"><a name="O15Readme_Prereq">
<p>This sample requires the following:</p>
</a>
<ul>
<a name="O15Readme_Prereq">
<li>
<p>Microsoft Visual Studio 2013 Update 2 or later.</p>
</a></li><li><a name="O15Readme_Prereq">
<p>Office Developer Tools for Visual Studio 2013 March, 2014, version or later. (This is included in Update 2 of Visual Studio 2013.)</p>
</a></li><li><a name="O15Readme_Prereq"></a>
<p><a name="O15Readme_Prereq">A Office 365 Developer Site in an Office 365 domain that is associated with a Azure AD tenancy. See
</a><a href="http://msdn.microsoft.com/en-us/library/office/fp179924(v=office.15).aspx" target="_blank">Sign up for an Office 365 Developer Site, set up your tools and environment, and start deploying apps</a> or
<a href="http://msdn.microsoft.com/en-us/library/office/jj692554(v=office.15).aspx" target="_blank">
How to: Create a Developer Site within your existing Office 365 subscription</a>.</p>
</li><li>
<p>A Gateway for Microsoft (GWM) deployed and configured in Microsoft Azure. See the document
<strong>SAP Gateway to Microsoft Implementation Guide</strong>.</p>
</li><li>
<p>An organization account in Microsoft Azure. See <a href="http://msdn.microsoft.com/en-us/library/office/dn605894(v=office.15).aspx#bk_CreateOrganizationAccount" target="_blank">
Create an organizational user account</a>.</p>
</li><li>
<p>A SAP OData endpoint with sample data in it. See the document <strong>SAP Gateway to Microsoft Implementation Guide</strong>.</p>
</li></ul>
</div>
<a name="O15Readme_components">
<h2 class="heading">Key components</h2>
<div class="section" id="sectionSection1">
<p>The Visual Studio solution contains the following:</p>
<p><strong>OfficeAppTaskPaneWeb</strong> project, which contains the following components:</p>
<ul>
<li>
<p><strong>OfficeAppTaskPane</strong> project, which contains the app's manifest configured to support hosting the app in Excel 2013, Excel Online, and Word 2013 or later.</p>
</li><li>
<p><strong>OfficeAppTaskPaneWeb</strong> project, which contains the following components:</p>
<ul>
<li>
<p>Default.aspx page and associated C# code-behind page.</p>
</li><li>
<p>Authenticate.aspx page and associated C# code-behind page</p>
</li><li>
<p>A C# file that defines the data model class.</p>
</li><li>
<p>A C# file that defines the class for requesting the data, deserializing the JSON response, and converting it to an array.</p>
</li><li>
<p>A C# class file that defines a helper class for handling Azure AD authorization.</p>
</li></ul>
</li></ul>
</div>
</a><a name="O15Readme_config">
<h2 class="heading">Register and modify the sample for your development environment</h2>
</a>
<div class="section" id="sectionSection2"><a name="O15Readme_config">
<p>The following procedures need to be carried out before the sample can work with your Azure AD tenancy and SAP endpoint.</p>
<h3 class="procedureSubHeading">Modify Visual Studio solution and the code and markup in the application</h3>
</a>
<div class="subSection"><a name="O15Readme_config"></a>
<ol>
<a name="O15Readme_config">
<li>
<p>Unzip the sample and open the *.sln file in Visual Studio.</p>
</li><li>
<p>Open the web.config file in the <span class="ui">OfficeAppTaskPaneWeb</span> project and change the &quot;localhost:
<em>&lt;port&gt;</em>&quot; part of the value of the <span class="keyword">AppHostName</span> key in the
<span class="keyword">appSettings</span> section to the value of the <span class="keyword">
SSL URL</span> property of the <span class="ui">OfficeAppTaskPaneWeb</span> project. (It may already be the same.)</p>
</li><li>
<p>Set the value of the <span class="keyword">Authority</span> key to the Office 365 domain of your organizational account. For example,
<span class="code">my_domain.onmicrosoft.com</span>.</p>
</li><li>
<p>Set the value of the <span class="keyword">ResourceUrl</span> key to the APP ID URI of your GWM application as it is registered in Azure AD. Obtain this value from your GWM administrator. The following is an example:</p>
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
<p>Open the DataModel.cs class and change the fields of the <span class="code">
DataModel</span> class to match the data that you will be getting from the SAP OData endpoint. For example, if you are getting event data from SAP with name, date, and location values, you can create fields in the class for Name, Date, and Location. Delete
 fields from the class that do not apply to your data.</p>
</a></li><li><a name="O15Readme_config">
<p>Open the DataGetter.cs class. In the <span class="code">GetDataMatrix</span> method, find the line
<span class="code">&quot;return item.Brand &#43; &quot; &quot; &#43; item.Model &#43; &quot; &quot; &#43; item.Price;&quot;</span>. Change the property names to match the property names you used in the DataModel class; for example
<span class="code">&quot;return item.Name &#43; &quot; &quot; &#43; item.Date &#43; &quot; &quot; &#43; item.Location;&quot;</span>. (You can, of course, display more or fewer properties.)</p>
</a></li><li><a name="O15Readme_config">GetData</a>
<ul>
<a name="O15Readme_config">https://gwmdemo.cloudapp.net:8081/perf/sap/opu/odata/sap/ZCAR_POC_SRV/</a>
<li><a name="O15Readme_config"></a>
<p><a name="O15Readme_config">ContosoMotorCollection?$top=5&amp;$skip=1 is the OData object query string. Replace ContosoMotorCollection with the name of your GWM OData entity, followed by any other OData query parameters you require. For more information
 about OData URI conventions, see </a><a href="http://www.odata.org/documentation/odata-version-2-0/uri-conventions/" target="_blank">URI Conventions</a>.</p>
</li></ul>
</li></ol>
</div>
<h3 class="procedureSubHeading">Register the ASP.NET application with Azure AD</h3>
<div class="subSection">
<ol>
<li>
<p>Login into <a href="https://manage.windowsazure.com/" target="_blank">Azure Management portal</a> with your Azure administrator account.</p>
</li><li>
<p>Choose <span class="ui">Active Directory</span> on the left side.</p>
</li><li>
<p>Click on your directory.</p>
</li><li>
<p>Choose <span class="ui">APPLICATIONS</span> (on the top navigation bar).</p>
</li><li>
<p>Choose <span class="ui">ADD</span> on the toolbar at the bottom of the screen.</p>
</li><li>
<p>On the dialog that opens, choose <span class="ui">Add an application my organization is developing</span>.</p>
</li><li>
<p>On the <span class="ui">ADD APPLICATION</span> dialog, give the application a name that is appropriate for the kind of data the sample will be getting from SAP. For the continuing example, use
<span class="input">ContosoAutomobileCollection</span>.</p>
</li><li>
<p>Choose <span class="ui">Web Application And/Or Web API</span> as the application type, and then click the right arrow button.</p>
</li><li>
<p>On the second page of the dialog, use the SSL debugging URL from the ASP.NET project in the Visual Studio solution as the value for
<span class="ui">SIGN-ON URL</span>. You can find the URL using the following steps. (<span class="label">Note:</span> You need to register the app initially with the debugging URL so that you can run the Visual Studio debugger (F5). When your app is ready
 for staging, you will re-register it with its staging Azure Web Site URL. Modify the app and stage it to Azure and Office 365.)</p>
<ol>
<li>
<p>Highlight the &quot;Web&quot; project (OfficeAppTaskPaneWeb) in <span class="ui">Solution Explorer</span>.</p>
</li><li>
<p>In the <span class="ui">Properties</span> window, copy the value of the <span class="ui">
SSL URL</span> property. An example is <span class="input">https://localhost:44300/</span>.</p>
</li><li>
<p>Paste it into the <span class="ui">SIGN-ON URL</span> on the <span class="ui">
ADD APPLICATION</span> dialog.</p>
</li></ol>
</li><li>
<p>For the <span class="ui">APP ID URI</span>, give the application a unique URI, such as the application name appended to the end of the SSL URL; for example
<span class="input">https://localhost:44300/ContosoAutomobileCollection</span>.</p>
</li><li>
<p>Click the checkmark button. The Azure dashboard for the application opens with a success message.</p>
</li><li>
<p>Choose <span class="ui">CONFIGURE</span> on the top of the page.</p>
</li><li>
<p>Scroll to the <span class="ui">CLIENT ID</span> and make a copy of it. You will need it for a later procedure.</p>
</li><li>
<p>In the <span class="ui">keys</span> section, create a key. It won't appear initially. Click
<span class="ui">SAVE</span> at the bottom of the page and the key will be visible. Make a copy of it. You will need it for a later procedure.</p>
</li><li>
<p>Scroll to <span class="ui">permissions to other applications</span> and select your GWM service application. In the continuing example, it is named
<strong>ContosoAutomobileCollection</strong>.</p>
</li><li>
<p>Open the <span class="ui">Delegated Permissions</span> drop down list and enable the boxes for the permissions to the GWM service that your app for SharePoint will need.</p>
</li><li>
<p>Click <span class="ui">Save</span> at the bottom of the screen.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">Configure the web.config with the Azure AD client ID and client key</h3>
<div class="subSection">
<ol>
<li>
<p>In Visual Studio, return to the web.config file. Insert the client ID that you saved from your Azure AD directory in the earlier procedure as the value of the
<span class="keyword">ida:ClientID</span> key. Leave the casing and punctuation exactly as you copied it and be careful not to include a space character at the beginning or end of the value. For the
<span class="keyword">ida:ClientKey</span> key use the <em>key</em> that you saved from the directory. Again, be careful not to introduce any space characters or change the value in anyway. The
<span class="code">&lt;appSettings&gt;</span> section should now look something like the following. (The
<span class="keyword">ClientId</span> key may have a GUID or no value at all.)</p>
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
  &lt;add key=&quot;ida:ClientID&quot; value=&quot;03db7336-ef93-4915-902d-b2c696d3dc9c&quot; /&gt;
  &lt;add key=&quot;ida:ClientKey&quot;
    value=&quot;0IRfXe&#43;tAvGrPf1Xs4pfmPPTzFo2sOSMVW8ukkIuZhw=&quot; /&gt;
  &lt;add key=&quot;Authority&quot; value=&quot;gwmdemo.onmicrosoft.com&quot; /&gt;
  &lt;add key=&quot;ResourceUrl&quot; value=&quot;http://gwmdemo.cloudapp.net/&quot; /&gt;
  &lt;add key=&quot;AppHostName&quot; value=&quot;https://localhost:44324/&quot; /&gt;
&lt;/appSettings&gt;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Save and close the web.config file.</p>
</li></ol>
</div>
</div>
<a name="sectionSection3"></a>
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection3">
<p>You can test the sample with the Visual Studio debugger.</p>
<div class="subSection">
<ol>
<li>
<p>Click <span class="ui">Start</span>, or press F5 in Visual Studio.</p>
</li><li>
<p>The first time that you use F5, you may be prompted to login to the Office 365 Developer Site that you are using. Use the site administrator credentials.</p>
</li><li>
<p>The first time that you use F5, you are prompted to grant permissions to the app. Click
<span class="ui">Trust It</span>.</p>
</li><li>
<p>After a brief delay while the access token is being obtained, the app's Default.aspx page opens in a task pane. Click the
<span class="ui">Get data from SAP</span> button to verify that the SAP data is inserted.</p>
</li><li>
<p>To switch between using an Office Online and Office desktop client, click the &quot;Office&quot; project in the
<span class="ui">Solution Explorer</span> to display the <span class="ui">Properties</span> pane.</p>
</li><li>
<p>In <span class="ui">Start Action</span>, select <span class="ui">Internet Explorer</span> to use an Office Online client, or select
<span class="ui">Office Desktop Client</span>, then select <span class="ui">Start Document</span> to specify the kind of Office document to test with.</p>
</li></ol>
</div>
</div>
<p>&nbsp;</p>
</div>
</div>
<div>
<div>
<div>
<div></div>
</div>
</div>
</div>
</div>
</div>
</div>
