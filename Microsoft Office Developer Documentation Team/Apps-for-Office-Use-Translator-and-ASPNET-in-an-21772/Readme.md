# Apps for Office: Use Microsoft Translator and ASP.NET in an app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* ASP.NET
* Word 2013
* Excel 2013
* apps for Office
## Topics
* Web Services
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:48:13
## Description

<div id="header">The app in the code sample translates the currently selected text in a Word or Excel file into the specified language using the Microsoft Translator service. The user selects a word in a document, selects the source language (the language
 that the word is written in), the target language (the language to translate the word to), and then chooses the
<span class="ui">Translate</span> button. The translation then appears in the UI of the app.</div>
<div id="mainSection">
<div id="mainBody">
<div class="section" id="sectionSection0">
<p>&nbsp;<img src="/site/view/file/80250/1/image.png" alt=""></p>
<p>This sample demonstrates two specific techniques: how to add an ASP.NET web service in an app for Office and how to use the Microsoft Translator service. In particular, the ASP.NET web service stores the
<span class="parameter">clientID</span> and <span class="parameter">clientSecret</span> parameters that the Microsoft Translator service requires to obtain an access token (which, in turn, is required for calling the Microsoft Translator service for a translation).
 The Translate.asmx file in the code sample handles HTTP requests coming from the app (Home.js), sends the translation request to the Microsoft Translator service, and then returns the results to the app. The AdminAccess.cs file in the App_Code folder is used
 to broker access tokens from the Microsoft Translator service.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Word 2013 or Excel 2013</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Internet Explorer 9 or Internet Explorer 10</p>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection2">
<p>&nbsp;</p>
<ul>
<li>
<p>CodeSample_TranslateASPNET project</p>
<ul>
<li>
<p>CodeSample_TranslateASPNET.xml manifest file</p>
</li></ul>
</li><li>
<p>CodeSample_TranslateASPNETWeb project</p>
<ul>
<li>
<p>Home.html file, which contains the HTML control for the app's user interface.</p>
</li><li>
<p>Home.js file, which contains the event handler for the <span><span class="keyword">Office.initialize</span></span> event of the app, handles the button click event for the app's button, calls the ASP.NET web service, parses the response, and updates the
 UI with the output.</p>
</li><li>
<p><strong>Translate.asmx</strong> file, which contains the web service on the server that handles the HTTP request from the app, stores the developer's client ID and client secret, and sends translation requests to the Microsoft Translator service.</p>
</li><li>
<p><strong>AdminAccess.cs</strong> code file, which contains the <span class="code">
AdmAuthentication</span> and <span class="code">AdmAccess</span> classes that update the access token for the server to communicate with the Microsoft Translator service.</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the Translator app, get a client ID and client secret from the Azure DataMarket.</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/hh454950.aspx" target="_blank">
Obtaining an access token</a>.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your manifest file parses correctly.</p>
<p>If the app raises a &quot;400: Bad request&quot; exception, ensure that you have replaced the constants in the Translate.asmx file with your own client ID and client secret values. Also, ensure that the HTTP request to the Microsoft Translator service has an Authorization
 header with a value set to &quot;Bearer&quot;, a space, and a current access token for the Microsoft Translator service.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: April 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/hh454950.aspx" target="_blank">Obtaining an access token</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ff512421.aspx" target="_blank">Translate Method</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms150046.aspx" target="_blank">HttpUtility.ParseQueryString Method</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ie/ms535874(v=vs.85).aspx" target="_blank">XMLHttpRequest object (Internet Explorer)</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp123513.aspx" target="_blank">Reading and writing data to the active selection in a document or spreadsheet</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
