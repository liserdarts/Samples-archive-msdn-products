# Creating a Custom Editor Control Using IFieldEditor Interface in SharePoint 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010
* SharePoint Server 2010
## Topics
* custom controls
## IsPublished
* True
## ModifiedDate
* 2012-05-15 10:55:16
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample accompanies the article &quot;<a href="http://msdn.microsoft.com/en-us/library/jj126861">Creating a Custom Editor Control by Using the IFieldEditor Interface in SharePoint 2010</a>.&rdquo; The article describes how to
 use the <strong>IFieldEditor</strong> interface to create a custom editor control in SharePoint 2010 by using Microsoft Visual Studio 2010. The custom editor control in this sample calculates the interest for a given amount and displays the sum of principal
 and interest for that amount</span>.</p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><span style="font-size:small">When implemented by a class that inherits from the
<strong>UserControl</strong> class, the <strong>IFieldEditor</strong> interface assists in the rendering of a field property editor control on a new column creation page or a column edit page, which in Windows SharePoint Services 3.0 are the New Site Column,
 Change Site Column, Create Column, and Change Column pages.</span></p>
<p><span style="font-size:small">The <strong>IFieldEditor</strong> type exposes the
<strong>DisplayAsNewSection</strong>, <strong>InitializeWithField</strong>, and <strong>
OnSaveChange</strong> members. These members determine whether the field property editor should be in a special section on the page, initialize the editor on page load, and validates the editor when the page is saved, respectively.</span></p>
<h1>More Information</h1>
<p><span style="font-size:small">For more information about the <strong>IFieldEditor</strong> interface, see
<a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.webcontrols.ifieldeditor">
IFieldEditor Interface</a> and <a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.webcontrols.ifieldeditor_members">
IFieldEditor Members</a>.</span></p>
