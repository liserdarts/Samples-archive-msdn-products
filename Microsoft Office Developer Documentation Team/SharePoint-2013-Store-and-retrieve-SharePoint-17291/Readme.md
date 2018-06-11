# SharePoint 2013: Store and retrieve SharePoint list items on a Windows Phone
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Mobile
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:57:26
## Description

<p><span style="font-size:small">One of the most important considerations in the development of Windows Phone apps is the management of state information, both for the overall application and for individual pages or data items within the application. Developers
 of Windows Phone apps must assume that users of their apps can lose connectivity to network resources (such as SharePoint lists). The development infrastructure for Windows Phone apps provides mechanisms for handling state information at various stages in
 the life cycle of an app. In a Windows Phone app that gets data from a SharePoint list, the data used on the phone from session to session can be retrieved from SharePoint Server if the server is available. But continuous connectivity to a SharePoint Server
 may not be available for a Windows Phone device because of variations in service coverage and other factors. To provide users of your app with access to data when connectivity with SharePoint Server is lost, or to save data to persistent storage between sessions
 of the app regardless of server availability, you can take advantage of the <strong>
Closing</strong> and <strong>Launching</strong> events of the <strong>PhoneApplicationService</strong> class.</span></p>
<p><span style="font-size:small">After implementing handlers for the <strong>Closing</strong> and
<strong>Launching</strong> events in your app, SharePoint list data that was retrieved from the server when connectivity was available can be displayed in your app even if connectivity to the server is lost in a subsequent session of the app, because the list
 items are retrieved from local persistent storage on the phone. However, list items that are displayed offline can't be edited and saved to the server unless connectivity is restored. In the following procedure, you add a mechanism to your app to store edited
 versions of list items locally when connectivity is unavailable. When connectivity to the server is again available, you can retrieve these edited list items and save your changes to the server.</span></p>
<p><span style="font-size:small">A specific instance of the <strong>EditItemViewModel</strong> class represents a SharePoint list item that is being edited on the phone. A list item that has been edited can be considered metaphorically as a &quot;draft item&quot; before
 changes are saved to the server. In the code in this class, the <strong>AddDraftItem</strong> method adds a specific instance of the
<strong>EditItemViewModel</strong> class (that is, a draft item) as a value to a <strong>
Dictionary</strong> object, associating the <strong>EditItemViewModel</strong> in the
<strong>Dictionary</strong> with a key based on the identifier for the given list item.</span></p>
<p style="padding-left:30px"><strong><span style="font-size:small">Note</span></strong><br>
<span style="font-size:small">An identifier is assigned by SharePoint Server to each item in a list. In a project based on the Windows Phone SharePoint List Application template, that identifier is stored in the ID property of the ViewModel class, such as
<strong>EditItemViewModel</strong> or <strong>DisplayItemViewModel</strong>, that represents the list item.</span></p>
<p><span style="font-size:small">The <strong>RemoveDraftItem</strong> method removes an
<strong>EditItemViewModel</strong> from the <strong>Dictionary</strong> object based on a specified identifier. Both of these methods use the
<strong>GetDraftItemCollection</strong> method to retrieve the <strong>Dictionary</strong> object containing the
<strong>EditItemViewModel</strong> objects from isolated storage and both methods use the
<strong>SaveDrafts</strong> method to save the modified <strong>Dictionary</strong> object (with a draft item either added to it or removed from it) back to isolated storage. The
<strong>GetDraftItemCollection</strong> method first determines whether a &quot;Drafts&quot;
<strong>Dictionary</strong> object has been saved to isolated storage. If so, the method returns that
<strong>Dictionary</strong> object; otherwise, the method initializes and returns a new
<strong>Dictionary</strong> object. The <strong>Drafts</strong> property of the class provides access to the
<strong>Dictionary</strong> of draft items by returning a list (that is, an object based on the
<strong>List&lt;T&gt;</strong> generic) of draft items as <strong>EditItemViewModel</strong> objects. The
<strong>GetDraftItemById</strong> method returns a given draft item from the <strong>
Dictionary</strong> object based on a specified identifier value.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2010 Express with the new SharePoint templates</span>
</li><li><span style="font-size:small">&nbsp;</span><span style="font-size:small">An installation of SharePoint 2013 with administrative privileges</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">SPListAppLocalStorage project, which contains the App.xaml.cs file and the CreateProductOrdersList.ps1 Windows PowerShell script.</span>
<ul>
<li><span style="font-size:small">&nbsp;</span><span style="font-size:small"><strong>App.xaml.cs</strong>&nbsp;&nbsp; This file is autogenerated by the Windows Phone SharePoint List Application template .The App.xaml file represents the overall Windows app.
 The associated code-behind file, App.xaml.cs, includes procedural code to handle life-cycle events for the app. App.xaml.cs contains references to SharePoint Server and the list title.</span>
</li><li><span style="font-size:small"><strong>CreateProductOrdersList.ps1</strong>&nbsp;&nbsp; You can run this Windows PowerShell script from the SharePoint Management Shell to create the SharePoint list on which this project is based.</span>
</li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">This sample assumes that you are working in a Windows Phone app project that was created from the Windows Phone SharePoint List Application template and that your app is based on a Product Orders list created from the Custom
 List template on the server and containing the columns and field types shown in Table 1.</span></p>
<p><strong><span style="font-size:small">Table 1. Sample Product Orders list</span></strong></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:601px; height:212px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Column</span></strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Type</span></strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Required </span>
</strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small">Product (i.e., Title)</span></td>
<td><span style="font-size:small">Single line of text (Text)</span></td>
<td><span style="font-size:small">Yes</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">Description </span>
</span></td>
<td><span style="font-size:small">Single line of text (Text)</span></td>
<td><span style="font-size:small">No </span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">Quantity
</span></span></span></span></td>
<td><span style="font-size:small">Number</span></td>
<td><span style="font-size:small">Yes </span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">Order Date </span>
</span></td>
<td><span style="font-size:small">Date and Time (DateTime) </span></td>
<td><span style="font-size:small">No </span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">Fulfillment Date</span>
</span></td>
<td><span style="font-size:small">Date and Time (DateTime) </span></td>
<td><span style="font-size:small">No </span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">Contact Number </span>
</span></td>
<td><span style="font-size:small">Single line of text (Text)</span></td>
<td><span style="font-size:small">No </span></td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:small">&nbsp;</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span style="font-size:small"><br>
<br>
Follow these steps to configure the sample.</span></p>
<ol>
<li><span style="font-size:small">Update the value of <strong>TargetSiteUrl</strong> in the App.xaml.cs file of the SPListAppLocalStorage solution with the URL of the home page of your SharePoint website.</span>
</li><li><span style="font-size:small">Update the value of the <strong>&lt;List Title&gt;</strong> element in the App.xaml.cs file of the SPListAppLocalStorage solution with the title of the target SharePoint list.</span>
</li></ol>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">&bull;&nbsp;Press the F5 key to build and run the app.</span></p>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</span></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:601px; height:212px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Problem </span>
</strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Solution</span></strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small">While running the SharePoint List wizard from Visual Studio 2010 Express, an error may occur if developer does not have sufficient privilege on SharePoint site.</span></td>
<td><span style="font-size:small">Give sufficient privilege to the user account with which developer is running the wizard.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">Form-based authentication error. <br>
<br>
<img id="60943" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-a619f634/image/file/60943/1/fig1.png" alt="" width="182" height="339"></span></td>
<td><span style="font-size:small">Form-based authentication is not enabled by default. To enable basic form-based authentication for the web application, follow these steps.</span>
<ul>
<li><span style="font-size:small">Navigate to Central Administration and ensure you have administrator rights on the server.</span>
</li><li><span style="font-size:small">Under <strong>Application Management</strong>, choose<strong>Manage Web Applications</strong>.</span>
</li><li><span style="font-size:small">Choose your web application (on which you have your SharePoint site, which you are accessing from your mobile app).</span>
</li><li><span style="font-size:small">From the ribbon, choose <strong>Authentication Providers</strong>.</span>
</li><li><span style="font-size:small">In the <strong>Authentication Provider </strong>
dialog box, choose <strong>Default</strong> to edit the authentication.</span> </li><li><span style="font-size:small">In the <strong>Edit Authentication Model </strong>
window under <strong>Claims Authentication Types</strong>, choose <strong>Basic Authentication</strong>.</span>
</li></ul>
</td>
</tr>
</tbody>
</table>
<h1><br>
<br>
<span style="font-size:small">&nbsp;</span><br>
<br>
<br>
</h1>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/jj163143.aspx" target="_blank"><span style="font-size:small">How to: Store and retrieve SharePoint list items on a Windows Phone</span>
</a></li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163259.aspx" target="_blank">How to: Create a Windows Phone SharePoint 2013 list app</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163786.aspx" target="_blank">Overview of Windows Phone SharePoint 2013 application templates in Visual Studio</a></span>
</li></ul>
