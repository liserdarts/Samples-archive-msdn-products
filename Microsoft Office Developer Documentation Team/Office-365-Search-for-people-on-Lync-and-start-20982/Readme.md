# Office 365: Search for people on Lync and start conversations
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* Lync
## Topics
* Search
* Contacts
* collaboration
## IsPublished
* True
## ModifiedDate
* 2013-02-28 02:02:38
## Description

<p id="header"><span class="label">Summary:</span> Learn how to use the Lync 2013 API to search for people in Lync 2013 and start IM conversations with them.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p>The Lync 2013 API includes a set of managed classes with methods that you can use to add collaboration functionality to your application. Using this Lync API, you can find users, start conversations, share resources, and initiate a meet-now meeting. This
 code sample shows you how to use the Lync 2013 API to search for people in Lync 2013 and start IM conversations with them.</p>
</div>
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<div>The Windows Forms application consists of the following forms:</div>
<ul>
<li>
<div><strong>Login form</strong> - Before searching for people in Lync, you need to sign in to the Lync client by using this form.</div>
</li><li>
<div><strong>Search form</strong> - After signing in to the Lync client, use this form to search for people or contacts in Lync and start conversations with them. This form is shown in the following figure.</div>
</li></ul>
<div><img id="76694" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76694/1/o365_searchpeoplelyncreadmescreen2.jpg" alt="Lync people search window with empty result list" width="698" height="564"></div>
<div>&nbsp;</div>
<div>For more information about the Lync 2013 API, see <a href="http://msdn.microsoft.com/library/c46a20f0-2d18-4aa8-b7fb-980fd88135c9.aspx" target="_blank">
Lync 2013 SDK documentation</a>.</div>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>Microsoft Visual Studio 2012.</div>
</li><li>
<div>Windows 7 or Windows 8.</div>
</li><li>
<div>Microsoft Office 2013.</div>
</li><li>
<div>Lync 2013 SDK.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<div>The sample contains the following:</div>
<ul>
<li>
<div>The 0365_Lyncpeoplesearch_cs project.</div>
</li><li>
<div>The login.cs file.</div>
</li><li>
<div>The login.Designer.cs file, which includes the custom sign-in form that contains the C# code that uses the Lync 2013 API to sign a user in to Lync.</div>
</li><li>
<div>The Search.cs file.</div>
</li><li>
<div>The Search.Designer.cs file, which declares the custom sign-in form that contains the C# code that uses the Lync 2013 API to search for users and start IM conversations.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample app.</div>
<ol>
<li>
<div>Extract O365_lyncpeoplesearch_cs.zip into a folder.</div>
</li><li>
<div>Open O365_lyncpeoplesearch_cs.sln by using Visual Studio 2012 as an administrator.</div>
</li><li>
<div>Add a reference to the Microsoft.Lync.Model assembly, which you can find in C:\Program Files (x86)\Microsoft Office 2013\LyncSDK\Assemblies\Desktop.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Press F5 to build and run the sample.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<ol>
<li>
<p>If you are not signed in to Lync, click the <span class="ui">Sign In to Lync</span> button, and then go to step 2. If you are already signed in, go to step 3.</p>
<div><img id="76695" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76695/1/o365_searchpeoplelyncreadmescreen2.jpg" alt="Lync people search window with empty result list" width="698" height="564"></div>
<div>&nbsp;</div>
</li><li>
<p>Enter your credentials on the <span class="ui">Login</span> form, and click the
<span class="ui">Sign In</span> button. Use your Office 365 user name. For example: &quot;&lt;YourName.CompanyName.onmicrosoft.com&gt;&quot;.</p>
<div><img id="76696" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76696/1/o365_searchpeoplelyncreadmescreen7.jpg" alt="Custom Lync sign in window" width="471" height="275"></div>
<div>&nbsp;</div>
</li><li>
<p>Enter the text (person's name) to search for, and click the <span class="ui">
Search</span> button.</p>
<div><img id="76697" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76697/1/o365_searchpeoplelyncreadmescreen3.jpg" alt="Lync people search window with search name typed" width="699" height="564"></div>
<div>&nbsp;</div>
<p>The search form shows a list of contacts that match the search text.</p>
<div><img id="76698" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76698/1/o365_searchpeoplelyncreadmescreen4.jpg" alt="Lync people search window with search results" width="698" height="565"></div>
<div>&nbsp;</div>
</li><li>
<p>Select the contact that you want to start a conversation with. Then, in the <span class="ui">
Message</span> box, type the message you want to send, and click the <span class="ui">
Start Conversation</span> button.</p>
<div><img id="76699" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76699/1/o365_searchpeoplelyncreadmescreen5.jpg" alt="Lync people search window with selected user" width="693" height="560"></div>
<div>&nbsp;</div>
<p>The IM conversation window opens, enabling you to start conversing with the contact.</p>
<div><img id="76700" src="http://i1.code.msdn.s-msft.com/office-365-search-for-c6e1b24c/image/file/76700/1/o365_searchpeoplelyncreadmescreen6.jpg" alt="Lync conversation window with selected user" width="433" height="430"></div>
</li></ol>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<div>Version</div>
</th>
<th>
<div>Date</div>
</th>
</tr>
<tr>
<td>
<div>First version</div>
</td>
<td>
<div>February 28, 2013</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/library/c46a20f0-2d18-4aa8-b7fb-980fd88135c9.aspx" target="_blank">Lync 2013 SDK documentation</a></div>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
