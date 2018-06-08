# VSSDK IDE Sample: Tool WPF Tool Windows
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010 SDK
## Topics
* VSX
* Visual Studio Shell
## IsPublished
* True
## ModifiedDate
* 2011-03-03 12:29:57
## Description

<h2>Summary</h2>
<p>This sample demonstrates how to create a package that provides tool windows which host Windows Forms controls and WPF controls.
<br>
<br>
</p>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">&nbsp;</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h2>Goals</h2>
<ul>
<li>Exposing properties in the Properties window based on the selected item </li><li>Tool window toolbars </li><li>Tool window with visibility controlled by a UI Context (solution loaded) </li><li>Tool window docked to another window as default start position </li><li>Usage of tool window events </li></ul>
<p><br>
This sample has a package (PackageToolWindow) and two tool windows (PersistedWindowPane and DynamicWindowPane). Each of the tool windows hosts a UserControl (PersistedWindowControl and DynamicWindowControl).<br>
<br>
<br>
The first window is persisted (its hidden/shown state is preserved when Visual Studio is restarted). It hosts a toolbar and demonstrates how to display properties in the Properties window based on the current selection inside the tool window.<br>
<br>
<br>
The second window has dynamic visibility (based on a UI Context). When a solution exists, the window is displayed. When no solution exists, it is hidden. Note that if one manually shows/hides the tool window, this mechanism would be disabled. To restore it,
 one can create a solution, show the tool window, close the solution, and finally hide the tool window. The window also provides a view helper which enables it to subscribe to tool window events. These include events such as moved, resized, shown, hidden, and
 so on.<br>
<br>
</p>
<h2>To start the sample</h2>
<ol>
<li>Build the solution </li><li>Open Visual Studio under experimental hive by pressing F5 </li></ol>
<p>&nbsp;</p>
<h2>To test the samples functionality</h2>
<ol>
<li>On the <strong>View</strong> menu, click <strong>Output</strong> to display the
<strong>Output</strong> window. </li><li>On the <strong>View</strong> menu, point to <strong>Other Windows</strong>, and then click
<strong>Persisted Window</strong> to display the tool window with persisted state. The
<strong>Persisted Tool Window</strong> appears as a tabbed window docked with <strong>
Solution Explorer</strong>. </li><li>Move the <strong>Persisted Tool Window</strong> to dock on the left side of the Visual Studio integrated development environment (IDE).
</li><li>Exit Visual Studio. Press F5 again to start Visual Studio from the experimental instance. The
<strong>Persisted Tool Window</strong> appears where it was when you exited Visual Studio.
</li><li>On the <strong>View</strong> menu, click <strong>Properties Window</strong> to display the
<strong>Properties</strong> window. </li><li>Click any of the window titles listed in the <strong>Persisted Tool Window</strong>. Note that the
<strong>Persisted Tool Window</strong> displays the titles of all tool windows in the IDE and might include some that are not visible. The
<strong>Properties</strong> window displays data about the selected tool window. </li><li>Close one of the tool windows listed in the <strong>Persisted Tool Window</strong> and click the
<strong>Refresh</strong> icon in the toolbar. The window titles list is updated to indicate that the window is no longer visible.
</li><li>On the <strong>View</strong> menu, point to <strong>Other Windows</strong>, and then click
<strong>Dynamic Visibility Window</strong>. The <strong>Dynamic Visibility Window</strong> appears.
</li><li>Hide the <strong>Dynamic Visibility Window</strong> by closing it. </li><li>Open or create a new solution. The <strong>Dynamic Tool Window</strong> appears.
</li><li>On the <strong>File</strong> menu, click <strong>Close Solution</strong>. The
<strong>Dynamic Tool Window</strong> disappears. </li></ol>
<p><br>
<br>
</p>
<h3>Additional Resources</h3>
<h4>Unit Tests:</h4>
<ul>
<li>Verify that the package can be created and sited and that it implements IVsPackage.
</li></ul>
<ul>
<li>For each tool window verify that executing the command to show the window completes with no errors.
</li></ul>
<ul>
<li>Verify that when populated with 2 frames, the WindowList class can produce a list of selectable objects.
</li></ul>
<ul>
<li>Verify that when each of the events are called on WindowStatus, the class produces the expected behavior (state change, event generation,...).
</li></ul>
