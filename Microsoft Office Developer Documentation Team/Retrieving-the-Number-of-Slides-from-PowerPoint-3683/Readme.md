# Retrieving the Number of Slides from PowerPoint 2010 by Using Open XML SDK 2.0
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010
* Microsoft Office 2010
* 2007 Microsoft Office System
## Topics
* Open XML
## IsPublished
* True
## ModifiedDate
* 2011-06-30 02:23:40
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Use the strongly typed classes in the Open XML SDK 2.0 to retrieve the number of slides in a PowerPoint 2010 presentation, without loading the presentation into Microsoft PowerPoint.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">The Office Open XML file formats make it possible to retrieve information about PowerPoint presentations, but doing so requires some effort. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Office
 Open XML file formats. The SDK simplifies the tasks of working with, for example, information about slides within a presentation. The code sample included with this article shows how to the use the SDK to retrieve the number of slides in a presentation (either
 including hidden slides or not), without requiring you to open the presentation in Microsoft PowerPoint.</span></p>
<p><span style="font-size:small">The sample provided with this article includes the code necessary to retrieve the count of slides in a PowerPoint 2007 (or later) presentation. The following sections walk you through the code, in explicit detail.</span><br>
<span style="font-size:small">Set Up References</span><br>
<span style="font-size:small">In order to use the code from the Open XML SDK 2.0, you must add a few references to your project. The sample project already includes these references, but in your own code, you would need to explicitly reference the following
 assemblies:</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp;&nbsp; &bull;&nbsp;WindowsBase (this reference may already be set for you, depending on the type of project you create)</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp;&nbsp; &bull;&nbsp;DocumentFormat.OpenXml (installed by the Open XML SDK 2.0)</span></p>
