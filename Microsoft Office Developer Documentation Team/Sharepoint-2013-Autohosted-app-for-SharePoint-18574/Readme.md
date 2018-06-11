# Sharepoint 2013: Autohosted app for SharePoint that includes a SQL Azure DB
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* False
## ModifiedDate
* 2013-03-20 11:15:24
## Description

<h1>Description</h1>
<p><span style="font-size:small">This sample app for SharePoint shows how to include an ASP.NET web application and a SQL Azure database in an autohosted app.&nbsp;</span></p>
<p><span style="font-size:small">It shows how to add a table to a SQL Azure database and populate it with sample data.&nbsp;It also shows how to use the SQL SELECT and INSERT commands to read and add data to the database.</span></p>
<p><span style="font-size:small">The default.aspx page of the app appears after you install and launch the app. It opens with a form that users can use to identify themselves.&nbsp;</span></p>
<div><strong><span style="font-size:small">Figure 1. Default.aspx page in the app</span></strong></div>
<div><span style="font-size:small"><img id="65679" src="/site/view/file/65679/1/fig1.png" alt="" width="181" height="108">&nbsp;</span></div>
<p><span style="font-size:small">The database is populated with sample data for fictional users &ldquo;Bob&rdquo; and &ldquo;Mary.&rdquo; When the
<strong>Show Favorite URLs </strong>button is selected for one of these users, a list of the user&rsquo;s favorite URLs appears above the form.</span></p>
<p><strong><span style="font-size:small">Figure 2. Displaying the user&rsquo;s favorite URLs</span></strong></p>
<div><span style="font-size:small"><img id="65680" src="/site/view/file/65680/1/fig2.png" alt="" width="345" height="283"></span></div>
<p><span style="font-size:small">If the user tries any name that is not already in the database, which initially is any user other than &ldquo;Bob&rdquo; or &ldquo;Mary,&rdquo; then a registration form appears and the user is prompted to enter a name and some
 favorite URLs.&nbsp;</span></p>
<p><strong><span style="font-size:small">Figure 3. The registration form</span></strong></p>
<div><span style="font-size:small"><img id="65681" src="/site/view/file/65681/1/fig3.png" alt="" width="533" height="262"></span></div>
<p><span style="font-size:small">Choosing the <strong>Register </strong>button adds the new user and favorites to the database and also displays them on the page. Since the new user is now in the database, on subsequent runs of the app, the user's name will
 be recognized.&nbsp;&nbsp;</span></p>
<h1>Deviations from good practices</h1>
<p><span style="font-size:small">The sample is focused on demonstrating an autohosted app with an included SQL Azure database, so it does not conform to all the good practices that should be used in a production app. Among other things, note the following.</span></p>
<ul>
<li><span style="font-size:small">The app does not provide any protection from SQL injection attacks.&nbsp;</span>
</li><li><span style="font-size:small">The app has no exception handling.</span> </li><li><span style="font-size:small">The app uses C# code on the server to toggle the visibility of forms. Performance might be better if JavaScript was used for this purpose, thereby reducing requests to the server.</span>
</li></ul>
<h1>Prerequisites</h1>
<ul>
<li><span style="font-size:small">Visual Studio 2012 and SharePoint development tools in Visual Studio 2012.</span>
</li><li><span style="font-size:small"><a href="http://www.iis.net/download/WebDeploy">Web Deploy 2.0</a> installed on the computer with Visual Studio. The version of Visual Studio and its SharePoint tools available for SharePoint 2013 should install this automatically.</span>
</li><li><span style="font-size:small">A SharePoint Online (Office 365) developer site. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/fp179924(office.15).aspx">Sign up for an Office 365 Developer Site</a>. Autohosted apps in SharePoint 2013 can be installed only on SharePoint Online websites. This may remain true for some time after the release
 of SharePoint 2013.</span> </li></ul>
<h1>Important contents</h1>
<ul>
<li><span style="font-size:small">FavoriteURLsApp project, which contains the AppManifest.xml file.</span>
</li><li><span style="font-size:small">FavoriteURLsWeb project.</span>
<ul>
<li><span style="font-size:small">Default.aspx file, which contains the HTML and ASP.NET controls for the user interface of the app.</span>
</li><li><span style="font-size:small">Default.aspx.cs file, which contains the C# code that uses reads and writes to the SQL Azure database.</span>
</li><li><span style="font-size:small">Web.config, web.debug.config, and web.release,config files. (The web.config file that is packaged with the app is a merger of web.config and either web.debug.config or web.release.config.)</span>
</li></ul>
</li><li><span style="font-size:small">FavoriteURLsDB project.</span>
<ul>
<li><span style="font-size:small">UserFavorites.sql, which creates a database table.</span>
</li><li><span style="font-size:small">Script.PostDeployment1.sql, which populates the table with sample data.</span>
</li></ul>
</li></ul>
<h1>Configuration instructions</h1>
<p><span style="font-size:small">Open the FavoriteURLsApp.sln file in Visual Studio 2013. In the&nbsp;<strong>Properties</strong> pane of Visual Studio, change the
<strong>Site URL </strong>property of the app for SharePoint project in Visual Studio to the absolute URL of your SharePoint 2013 developer test site on SharePoint Online. For example, &quot;https://microsoft555.sharepoint.com/&quot;.&nbsp;</span></p>
<h1>Build instructions</h1>
<ol>
<li><span style="font-size:small">Choose the <strong>FavoriteURLsApp</strong> <em>
project</em> in <strong>Solution Explorer </strong>(not the top node for the whole Visual Studio
<em>solution</em>). On the menu bar, choose <strong>Publish</strong>. (Do <em>not</em> press the F5 key.)
</span></li><li><span style="font-size:small">In the <strong>Publish</strong> dialog box, choose the
<strong>Finish</strong> button. The resulting app package file (which has the Windows Azure Web Sites package and the SQL DACPAC inside) has an .app extension and is saved in the app.publish subfolder of the bin\Debug folder of the Visual Studio project.</span>
</li></ol>
<h1>Deploying and testing the sample</h1>
<ol style="padding-left:30px">
<li><span style="font-size:small">Sign&nbsp;into SharePoint Online as a tenant administrator.</span>
</li><li><span style="font-size:small">At the top of the page, choose <strong>Admin</strong>,
<strong>SharePoint</strong>.</span> </li><li><span style="font-size:small">On the <strong>SharePoint Administration Center</strong> page, choose
<strong>apps</strong>, and then choose <strong>App Catalog</strong>. If you haven&rsquo;t already created an app catalog site collection, you will be prompted to create one.</span>
</li><li><span style="font-size:small">After the app catalog site collection is created, open it, and select
<strong>Apps for SharePoint</strong>. </span></li><li><span style="font-size:small">On the <strong>App Catalog</strong> page, choose the
<strong>new item </strong>link.</span> </li><li><span style="font-size:small">On the <strong>Add a document </strong>form, browse to your app for SharePoint package and choose the
<strong>OK</strong> button. A property form for new items opens.</span> </li><li><span style="font-size:small">Fill out the form as needed and choose the <strong>
Save</strong> button. The app for SharePoint is saved in the catalog.</span> </li><li><span style="font-size:small">Browse to any website in the tenancy and choose
<strong>Site Contents </strong>to open the <strong>Site Contents </strong>page. </span>
</li><li><span style="font-size:small">Choose <strong>add an app</strong>, and on the <strong>
Your Apps </strong>page, find the app. If there are too many to scroll through, you can enter any part of the app title (<strong>Favorite URLs</strong>) into the search box.</span>
</li><li><span style="font-size:small">When you find the app, choose the <strong>Details</strong> link beneath it, and then on the app details page that opens, choose
<strong>Add It</strong>.</span> </li><li><span style="font-size:small">You are prompted to grant permissions to the app. Choose
<strong>Trust It</strong>.</span> </li><li><span style="font-size:small">The <strong>Site Contents </strong>page opens and the app is listed. For a short time, a message below the title indicates that it is being added. When this message disappears, you can choose the app icon to launch the app.
 (You may need to refresh the page to make the message disappear.) </span></li><li><span style="font-size:small">Exercise the app:</span> </li></ol>
<div style="padding-left:30px">
<div>
<div style="padding-left:30px"><span style="font-size:small">a.&nbsp;Press the <strong>
Show Favorite URLs </strong>button without a entering a name to see what kind of error you get.</span><br>
<span style="font-size:small">b.&nbsp;Enter &ldquo;Mary&rdquo; or &ldquo;Bob&rdquo; in the Name box of the start page and choose the
<strong>Show Favorite URLs </strong>button to display a list of URLs.</span><br>
<span style="font-size:small">c.&nbsp;Enter a different name to see how unknown users are prompted to register.</span><br>
<span style="font-size:small">d.&nbsp;Register the new user and add some URLs to the form. Then press
<strong>Register</strong> to see the new user&rsquo;s favorites displayed.</span><br>
<span style="font-size:small">e.&nbsp;Close the app and then relaunch it. Enter the new user&rsquo;s name and press
<strong>Show Favorite URLs </strong>button to verify that the new user was added permanently to the database.</span><span style="font-size:small">&nbsp;</span></div>
</div>
</div>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp179902.aspx" target="_blank">How to: Create an autohosted app that includes a SQL&nbsp;Server database</a></span></p>
<h1>Contact info</h1>
<p><span style="font-size:small"><a href="mailto:DocThis@microsoft.com">DocThis@microsoft.com</a></span></p>
