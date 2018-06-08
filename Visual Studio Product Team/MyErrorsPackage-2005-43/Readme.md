# MyErrorsPackage (2005)
## Requires
* Visual Studio 2005
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2005 SDK
## Topics
* Visual Studio Editor
* VSX
## IsPublished
* True
## ModifiedDate
* 2011-03-01 12:06:45
## Description

<h1><span style="font-size:large">Introduction</span></h1>
<p><span style="font-size:x-small">This sample illustrates how to use an ErrorListProvider to participate in&nbsp;the ErrorList ToolWindow.&nbsp;</span><br>
<span style="font-size:x-small">&nbsp;&nbsp;&nbsp;</span><br>
<span style="font-size:x-small">Build and register the sample, and run DevEnv.exe under the VS Experimental&nbsp;hive. Select the &quot;My Errors Window&quot; menu item, under the View.Other windows menu.&nbsp;Add a few strings to the listbox with the Add Error button.
 Note the&nbsp;items added to the Error List toolwindow. Double click one of the items in&nbsp;&nbsp; the Error List toolwindow, and note the toolwindow is activated and the&nbsp;corresponding item selected in the toolwindow's listbox.</span><br>
&nbsp;&nbsp;</p>
<h1><span style="font-size:large">The MyToolWindow class</span></h1>
<p>Note that the ErrorListProvider object is actually created in the&nbsp;OnToolWindowCreated override. This is neccessary to ensure that the&nbsp;ErrorListProvider is initialized with a valid IServiceProvider, and&nbsp;the OnToolWindowCreated override is a
 good place to do this, as we can be&nbsp;certain that the toolwindow has been sited at this point.</p>
<h1><br>
<span style="font-size:large">The MyControl class</span></h1>
<p>Thic class implements the actual window pane for the toolwindow, and when&nbsp;you add additional strings to the listbox, a corresponding string is added&nbsp;to the Error List toolwindow, by way of creating an ErrorTask object and&nbsp;adding it through
 the ErrorListProvider in the parent MyToolWindow object.&nbsp;Note the btnAddError_Click event also sinks the Navigate event, so that&nbsp;we can set the focus to the listbox and select the item corresponding to the&nbsp;ErrorTask that is dbl clicked on in
 the Error List toolwindow.<br>
&nbsp;&nbsp;&nbsp;</p>
