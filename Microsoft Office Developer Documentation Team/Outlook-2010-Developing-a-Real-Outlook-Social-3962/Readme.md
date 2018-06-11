# Outlook 2010: Developing a Real Outlook Social Connector Provider
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Office Outlook 2007
* Outlook 2010
* Office 2010
* Microsoft Office Outlook 2003
## Topics
* Outlook Social Connector
* OSC provider
## IsPublished
* True
## ModifiedDate
* 2011-08-01 03:44:54
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to develop a real provider for the Microsoft Outlook Social Connector (OSC) by using the OSC Provider Proxy Library. This sample accompanies a series of four Visual How-To articles,
<a href="http://msdn.microsoft.com/en-us/library/gg585217.aspx">Developing a Real Outlook Social Connector Provider</a>, in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Microsoft Outlook Social Connector (OSC) provides a communication hub for personal and professional communications. Just by selecting an Outlook item such as an email or meeting request and clicking the sender or a recipient of that item, users can see,
 in the People Pane, activities, photos, and status updates for the person on their favorite social networks.</p>
<p>The OSC obtains social network data by calling an OSC provider, which behaves like a translation layer between Outlook and the social network. The OSC provider model is open, and you can develop a custom OSC provider by implementing the required OSC provider
 extensibility interfaces. To retrieve social network data, the OSC makes calls to the OSC provider through these interface members. The OSC provider communicates with the social network and returns the social network data to the OSC as a string or as XML that
 conforms to the Outlook Social Connector XML schema.</p>
<p>This sample solution shows the procedures to create a custom OSC provider for OfficeTalk. OfficeTalk is a social network in a private corporate environment and is not publicly available. Nonetheless, it is a good example of the kind of social network that
 you might want to develop a custom OSC provider for. You can use the procedures for creating the OSC provider for OfficeTalk to create a custom OSC provider for any social network.</p>
<p>The OfficeTalk provider uses the Outlook Social Connector Provider Proxy Library to simplify the implementation of the OSC provider extensibility interfaces. The OSC Provider Proxy Library implements all of the OSC provider extensibility interface members.
 These interface members, in turn, call a consolidated set of abstract and virtual methods that provide the social network data that the OSC requires.</p>
<p>The sample solution contains two projects:</p>
<ul>
<li>OSCProvider&mdash;This project is an unmodified version of the OSC Provider Proxy Library that is used to simplify the creation of the OfficeTalk OSC provider.
</li><li>OfficeTalkOSCProvider&mdash;This project includes the source code files that are specific to the OfficeTalk OSC provider.
</li></ul>
<p>The OfficeTalkOSCProvider project includes the following source code files:</p>
<ul>
<li>OfficeTalkHelper&mdash;This class contains helper methods that are used throughout the sample solution.
</li><li>OTProvider&mdash;This is a partial class that contains the OSC Provider Proxy Library override methods that return information about the OSC provider, information about the social network, and information for the currently logged-on user.
</li><li>OTProvider_Activities&mdash;This is a partial class that contains the OSC Provider Proxy Library override methods that return activity information.
</li><li>OTProvider_Friends&mdash;This is a partial class that contains the OSC Provider Proxy Library override methods that return friends information.
</li></ul>
