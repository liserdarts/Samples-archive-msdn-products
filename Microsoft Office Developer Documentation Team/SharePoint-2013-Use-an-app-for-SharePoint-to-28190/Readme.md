# SharePoint 2013: Use an app for SharePoint to create a custom form
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* C#
* UI Design
* Cloud
* App model
* apps for SharePoint
* forms
* sites and content
## IsPublished
* True
## ModifiedDate
* 2014-04-15 01:46:54
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Use an app for SharePoint to create a custom form</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> Learn how to use CSOM to create a custom site creation form for a SharePoint site.
</p>
</div>
<div>
<p><b>Last modified: </b>April 07, 2014</p>
<p><b>In this article</b> <br>
<a href="#sectionSection0">Description of the sample</a> <br>
<a href="#sectionSection1">Prerequisites</a> <br>
<a href="#sectionSection2">Configure the sample</a> <br>
<a href="#sectionSection3">Run and test the sample</a> <br>
<a href="#sectionSection4">Change log</a> <br>
<a href="#sectionSection5">Related content</a> <br>
</p>
<p>This sample demonstrates how to use CSOM to create a custom site creation form.</p>
</div>
<h1>Description of the sample</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample app for SharePoint demonstrates using the Client-Side Object Model (CSOM) that a site administrator can use to specify a custom site creation form that includes more fields than are available on the SharePoint form out-of-the-box.</p>
<p>The sample creates a Team site that is created as a subsite at the specified location. Because the app for SharePoint needs the ability to create subsites and site collections anywhere in the tenancy, the app requires Full Control of the entire tenancy.
 The app makes app-only calls to SharePoint so that it can work with tenant objects or sites outside the context. You can configure both of these settings in the Permissions tab of
<b><span class="keyword">AppManifest.xml</span></b>.</p>
<div>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Note</b> </th>
</tr>
<tr>
<td>
<p>As a best practice, request only the minimum permissions that your app needs in order to work. Avoid requesting tenancy permissions in apps for SharePoint—especially Full Control. The &quot;tenancy&quot; permissions scope exists specifically to support scenarios such
 as site provisioning.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>The app enables administrators to dynamically configure site creation settings using a Library in the app web instead of a list. Using a library makes it possible to display each option with an icon in the site collection form. Each row in the library represents
 a site provisioning option for an end user. </p>
<b>
<div class="caption">Table 1. Columns in the custom form</div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
<tbody>
<tr>
<th>
<p>Column</p>
</th>
<th>
<p>Description</p>
</th>
</tr>
<tr>
<td>
<p>Title</p>
</td>
<td>
<p>The title of the configuration option</p>
</td>
</tr>
<tr>
<td>
<p>Site Template</p>
</td>
<td>
<p>The WebTemplate name to be used during provisioning (e.g., <span>STS#0</span> for Team site)</p>
</td>
</tr>
<tr>
<td>
<p>Base Path</p>
</td>
<td>
<p>The absolute URL where the option gets provisioned.</p>
</td>
</tr>
<tr>
<td>
<p>Site Type</p>
</td>
<td>
<p>Choice of &quot;Subsite&quot; or &quot;Site Collection&quot; </p>
</td>
</tr>
<tr>
<td>
<p>MasterPage URL</p>
</td>
<td>
<p>The URL of the masterpage file to apply (leave blank if you don't want to provision branding)</p>
</td>
</tr>
<tr>
<td>
<p>Storage Maximum Limit</p>
</td>
<td>
<p>The storage quota in MB (applicable only to site collections)</p>
</td>
</tr>
<tr>
<td>
<p>UserCode Maximum Limit</p>
</td>
<td>
<p>The user code quota in points (applicable only to site collections)</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>After the user submits the site creation form, how the app provisions the site depends on whether the site is a Subsite or a Site Collection. Both need to execute app-only calls, since the app likely provisions with a different context from where the custom
 form is hosted. For example, a site collection requires the context of a tenant administration site. The
<b><span class="keyword">TokenHelper.cs</span></b> file contains a <b><span class="keyword">GetAppOnlyAccessToken</span></b> method that gets the access token for a specific site that is different from the context of the form.</p>
<p></p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2013</p>
</li><li>
<p>Microsoft Office Developer Tools for Visual Studio</p>
</li><li>
<p>A SharePoint 2013 development environment</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>This sample includes code you can use to provision a subsite or a site collection.</p>
<p>To provision a subsite, establish context with the parent site that will host the new subsite.</p>
<div>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Note</b> </th>
</tr>
<tr>
<td>
<p>This sample requires the master page to exist in the Master Page Gallery of the root web in the site collection in order for branding to be applied. This approach only requires you to set the
<b><span class="keyword">MasterUrl</span></b> and <b><span class="keyword">CustomMasterUrl</span></b> properties.</p>
</td>
</tr>
</tbody>
</table>
</div>
<div><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th>C# </th>
<th></th>
</tr>
<tr>
<td colspan="2">
<pre>    var parentSite = new Uri(selectedConfig.BasePath);  //Static for the example tenant.
    var token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, parentSite.Authority, null).AccessToken;
    using (var clientContext = TokenHelper.GetClientContextWithAccessToken(parentSite.ToString(), token))
    {
        var properties = new WebCreationInformation()
        {
            Url = txtUrl.Text,
            Title = txtTitle.Text,
            Description = txtDescription.Text,
            WebTemplate = selectedConfig.SiteTemplate,
            UseSamePermissionsAsParentSite = false
        };

        //Create and load the new web.
        Web newWeb = clientContext.Web.Webs.Add(properties);
        clientContext.Load(newWeb, w =&gt; w.Title);
        clientContext.ExecuteQuery();

        //TODO: Set additional owners.

        //apply the masterpage to the site (if applicable)
        if (!String.IsNullOrEmpty(selectedConfig.MasterUrl))
        {
            newWeb.MasterUrl = selectedConfig.MasterUrl;
            newWeb.CustomMasterUrl = selectedConfig.MasterUrl;
        }

        /**************************************************************************************/
        /*   Placeholder area for updating additional settings and features on the new site   */
        /**************************************************************************************/

        //update the web with the new settings
        newWeb.Update();
        clientContext.ExecuteQuery();
    }

    return webUrl;
}
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>To provision a site collection, establish context with the tenant administration site and use a
<span><b>TenantAdministration</b></span> object for creation. It can take time to establish context with the tenant administration site; you can asynchronously perform other operations while site creation operation is running before trying to apply the master
 page. </p>
<div><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th>C# </th>
<th></th>
</tr>
<tr>
<td colspan="2">
<pre>private byte[] GetMasterPageFile(string masterUrl)
{
    byte[] mpBytes = null;

    //get the siteurl of the masterpage
    string siteUrl = masterUrl.Substring(0, masterUrl.IndexOf(&quot;/_catalogs&quot;));

    var siteUri = new Uri(siteUrl);  //static for my tenant
    var token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, siteUri.Authority, null).AccessToken;
    using (var clientContext = TokenHelper.GetClientContextWithAccessToken(siteUri.ToString(), token))
    {
        string relativeMasterUrl = masterUrl.Substring(8);
        relativeMasterUrl = relativeMasterUrl.Substring(relativeMasterUrl.IndexOf(&quot;/&quot;));
        File file = clientContext.Web.GetFileByServerRelativeUrl(relativeMasterUrl);
        var stream = file.OpenBinaryStream();
        clientContext.ExecuteQuery();
        using (stream.Value)
        {
            mpBytes = new Byte[stream.Value.Length];
            stream.Value.Read(mpBytes, 0, mpBytes.Length);
        }
    }

    return mpBytes;
}

private string CreateSiteCollection(SSConfig selectedConfig)
{
    string webUrl = &quot;&quot;;

    //Create site collection using the Tenant object.
    var tenantAdminUri = new Uri(TENANT_ADMIN_URL);  //static for my tenant
    var token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, tenantAdminUri.Authority, null).AccessToken;
    using (var clientContext = TokenHelper.GetClientContextWithAccessToken(tenantAdminUri.ToString(), token))
    {
        var tenant = new Tenant(clientContext);
        webUrl = String.Format(&quot;{0}{1}&quot;, selectedConfig.BasePath, txtUrl.Text);
        var properties = new SiteCreationProperties()
        {
            Url = webUrl,
            Owner = &quot;ridize@richdizzcom.onmicrosoft.com&quot;,
            Title = txtTitle.Text,
            Template = selectedConfig.SiteTemplate,
            StorageMaximumLevel = Convert.ToInt32(selectedConfig.StorageMaximumLevel),
            UserCodeMaximumLevel = Convert.ToDouble(selectedConfig.UserCodeMaximumLevel)
        };
        SpoOperation op = tenant.CreateSite(properties);
        clientContext.Load(tenant);
        clientContext.Load(op, i =&gt; i.IsComplete);
        clientContext.ExecuteQuery();

        //check if site creation operation is complete
        while (!op.IsComplete)
        {
            //wait 30seconds and try again
            System.Threading.Thread.Sleep(30000);
            op.RefreshLoad();
            clientContext.ExecuteQuery();
        }
    }

    //Get the newly created site collection.
    var siteUri = new Uri(webUrl);  //static for my tenant
    token = TokenHelper.GetAppOnlyAccessToken(SHAREPOINT_PID, siteUri.Authority, null).AccessToken;
    using (var clientContext = TokenHelper.GetClientContextWithAccessToken(siteUri.ToString(), token))
    {
        var newWeb = clientContext.Web;
        clientContext.Load(newWeb);
        clientContext.ExecuteQuery();

        //update description
        newWeb.Description = txtDescription.Text;

        //TODO: Set additional site collection administrators.

        //apply the masterpage to the site (if applicable)
        if (!String.IsNullOrEmpty(selectedConfig.MasterUrl))
        {
            //Get the masterpage bytes from its existing location
            byte[] masterBytes = GetMasterPageFile(selectedConfig.MasterUrl);
            string newMasterUrl = String.Format(&quot;{0}{1}/_catalogs/masterpage/ssp.master&quot;, selectedConfig.BasePath, txtUrl.Text);
                    
            //upload to masterpage gallery of new web and set
            List list = newWeb.Lists.GetByTitle(&quot;Master Page Gallery&quot;);
            clientContext.Load(list, i =&gt; i.RootFolder);
            clientContext.ExecuteQuery();
            FileCreationInformation fileInfo = new FileCreationInformation();
            fileInfo.Content = masterBytes;
            fileInfo.Url = newMasterUrl;
            Microsoft.SharePoint.Client.File masterPage = list.RootFolder.Files.Add(fileInfo);
            string relativeMasterUrl = newMasterUrl.Substring(8);
            relativeMasterUrl = relativeMasterUrl.Substring(relativeMasterUrl.IndexOf(&quot;/&quot;));

            //we can finally set the masterurls on the newWeb
            newWeb.MasterUrl = relativeMasterUrl;
            newWeb.CustomMasterUrl = relativeMasterUrl;
        }

        /**************************************************************************************/
        /*   Placeholder area for updating additional settings and features on the new site   */
        /**************************************************************************************/

 

        //update the web with the new settings
        newWeb.Update();
        clientContext.ExecuteQuery();
    }

    return webUrl;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The site creation form will load in the <b><span class="keyword">Start a Site</span></b> dialog box, so the app must communicate back to the SharePoint page that hosts the dialog. The communication is required to make the dialog page visible, resize it,
 close, it, and navigate away from it. For cross-domain communication, the app uses the HTML5
<b><span class="keyword">postMessage</span></b> cross-domain API, where SharePoint listens for
<b><span class="keyword">MakePageVisible</span></b>, <b><span class="keyword">Resize</span></b>,
<b><span class="keyword">CloseDialog</span></b>, and <b><span class="keyword">NavigateParent</span></b> messages from the page within the dialog IFRAME.</p>
<p>To launch the site creation form from the <b><span class="ui">add site</span></b> link on the &quot;sites&quot; page, configure it as the
<b><span class="keyword">Start a Site</span></b> form in the tenant administration site. The maximum character length is 255, and the URL must contain the
<b><span class="keyword">SPHostUrl</span></b> and <b><span class="keyword">SPAppWebUrl</span></b> parameters. The app requires these parameters to get context and access tokens from SharePoint.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>Press <b><span class="keyword">F5</span></b> to compile and run the sample.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection4" name="collapseableSection"><b>
<div class="caption"></div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>April 7, 2014</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection5" name="collapseableSection">
<ul>
<li>
<p><a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=2c8011b0-441a-4bab-bf19-6b1009c7d8dd" target="_blank">SharePoint 2013 and SharePoint Online solution pack for branding and site provisioning</a>
</p>
</li><li>
<p><a href="http://www.microsoft.com/en-us/download/details.aspx?id=35588" target="_blank">SharePoint Online Management Shell download</a>
</p>
</li></ul>
</div>
</div>
</div>
