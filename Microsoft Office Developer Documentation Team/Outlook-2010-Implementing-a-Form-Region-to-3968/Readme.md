# Outlook 2010: Implementing a Form Region to Display Email Headers
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* Form Regions
* Internet headers
* email headers
## IsPublished
* True
## ModifiedDate
* 2011-08-02 09:19:53
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to customize the mail inspector in Microsoft Outlook 2010 so that with the click of a button, Outlook can display the Internet headers of the current message. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg622917.aspx">Implementing a Form Region to Display Email Headers in Outlook 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Internet headers provide detailed information about an email message. Such information includes the servers that processed the message before it was finally delivered to the recipient. By default, some information is exposed in the email client, and other
 information is hidden. For example, Microsoft Outlook 2010 displays the date and time that a message is received. However, the Internet headers reveal the actual date and time that the sender sent the message. This Visual How To shows how to programmatically
 obtain header information about an email message. To conveniently display such information, the Visual How To uses an adjoining form region. When you open an email message in Outlook, you will see the form region at the bottom of the mail inspector, and can
 click a button to view the Internet headers of that message.</p>
<p>The sample provided uses a C# Outlook 2010 add-in that customizes the standard email message form with an adjoining form region. To use this sample, you should already be familiar with C# and creating add-ins for Outlook.</p>
<p>The Outlook add-in solution is named OutlookDisplayHeaders_CS. It contains an adjoining form region that has the name Display Internet Headers and is represented by the form region class
<strong>FormRegion1</strong>.</p>
<p><strong>FormRegion1 </strong>provides a button and a text box. The button is labeled
<strong>Show Headers</strong>. When you click the <strong>Show Headers</strong> button, the Internet headers of the currently displayed email message are shown in the text box.</p>
