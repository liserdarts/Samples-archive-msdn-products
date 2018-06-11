# SharePoint 2010: Creating a Custom Web Part Editor in SharePoint 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* Web Parts
## IsPublished
* True
## ModifiedDate
* 2011-06-06 03:05:46
## Description

<h1>Introduction</h1>
<p style="text-align:left"><span style="font-size:small">The Microsoft SharePoint 2010 Web Part framework enables the user to configure a Web Part through Web Part properties. A default user interface (UI) is given for each Web Part property that is exposed
 to the user in the <strong>Edit Tool </strong>pane of the Web Part. For example, a property of type
<strong>String </strong>is rendered as a <strong>TextBox</strong>, and an <strong>
Enum </strong>is rendered as a <strong>DropDownList</strong>.</span></p>
<p><span style="font-size:small">This default rendering by the framework does not leave much scope for customization because the user will not be able to get a handle to the controls that are rendered. For example, the following cannot be achieved:</span></p>
<ul>
<li><span style="font-size:small"><strong>Control over UI</strong>: Numeric and string types are rendered as text boxes, enumeration types are rendered as a drop&ndash;down menus, and Boolean types are rendered as check boxes. For example, if the requirement
 is to show an Enumeration type as radio buttons instead of a drop&ndash;down list, it cannot be done.</span>
</li><li><span style="font-size:small"><strong>Events</strong>: Because there is no handle to the controls that are rendered, the events raised by the controls cannot be handled. For example, if a drop&ndash;down list has to be enabled or disabled based on selecting
 a check box, it cannot be done</span> </li><li><span style="font-size:small"><strong>Validation</strong>: If user input must be validated, the framework does not provide a way to easily attach a Microsoft ASP.NET validation control to the input control that is generated (for example, to validate a string
 input for a valid email through an ASP.NET RegEx validation control).</span> </li></ul>
<p><span style="font-size:small">The framework provides scope for overcoming all of these limitations through custom editor parts. Custom editor parts enable you to offer users functionality that can be achieved through, for example, ASP.NET user controls.</span></p>
<p><span style="font-size:small">A Web Part can implement custom editor parts that can be loaded when the Web Part is in edit mode, to expose a custom UI, event handling, and input validation. A Web Part can have more than one custom editor part associated
 with it.</span></p>
<h1>More Information</h1>
<p><span style="font-size:small">For more information see the technical article associated with this sample,
<a href="http://msdn.microsoft.com/en-us/library/hh228018.aspx">Creating a Custom Web Part Editor in SharePoint 2010</a></span><em><span style="font-size:small">.</span></em></p>
