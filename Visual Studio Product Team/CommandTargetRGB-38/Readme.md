# CommandTargetRGB
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
* 2011-02-28 02:10:28
## Description

<h1><span style="font-size:large">Introduction</span></h1>
<p>A Visual Studio Package which provides a multi-instance tool window called &quot;Red Green Blue&quot; which hosts a toolbar with three buttons that will change the tool window's background color and move the toolbar in the frame.</p>
<h1><span style="font-size:large">Getting Started</span></h1>
<p>To build and run this sample, you must have Visual Studio 2010 installed, as well as the Visual Studio SDK. Unzip the CommandTargetRGB.zip file into your Visual Studio Projects directory (My Documents\Visual Studio 2010\Projects) and open the CommandTargetRGB.sln
 solution.</p>
<h1><span style="font-size:large">Building the Sample</span></h1>
<p>To build the sample, make sure the CommandTargetRGB solution is open and then use the Build | Build Solution menu command.</p>
<h1><span style="font-size:large">Running the Sample</span></h1>
<p>To run the sample, hit F5 or choose the Debug | Start Debugging menu command. A new instance of Visual Studio will launch under the experimental hive. The experimental hive is a special &quot;sandbox&quot; instance of Visual Studio that allows you to develop and test
 packages without affecting your main instance of Visual Studio. Once loaded, choose the View | Other Windows | Red Green Blue menu command. A new tool window called &quot;Red Green Blue&quot; will open with a red background and a toolbar with three commands. Because
 this is a multi-instance tool window, each time you choose the Red Green Blue menu command, a new tool window will appear.</p>
<p>The three commands are called Red, Green, and Blue, and they each change the background color of the tool window they are contained in. To demonstrate the flexibility of this toolbar, each command also docks the toolbar to the top, bottom, or left side of
 the tool window.</p>
<h1><span style="font-size:large">Source Code Overview</span></h1>
<p>The source code in this sample demonstrates several techniques you can use to write your own packages:</p>
<ul>
<li>How to create a multi-instance tool window for your package </li><li>How to style the content of a WPF control in a tool window </li><li>How to implement IOleCommandTarget on a tool window in order to respond to toolbar commands
</li><li>How to programmatically add a ToolBarTray to a WPF grid </li></ul>
<p>One of the most interesting aspects of the code for this sample is how the toolbar is created. The RGBToolWindow class uses the IVsUIShell4.CreateToolbarTray method to programmatically create a toolbar tray, passing itself as the IOleCommandTarget for the
 toolbar. Since each tool window is an independent instance of the RGBToolWindow class, each tool window can respond to its three toolbar commands independently without affecting the other instances of the Red Green Blue tool window.</p>
<h1><span style="font-size:large">Project Files</span></h1>
<ul>
<li>CommandTargetRGB.vsct - Defines the menu item, toolbar, and toolbar commands for the sample.
</li><li>CommandTargetRGBPackage.cs/vb - Implements the Visual Studio Package, which creates and responds to the &quot;Red Green Blue&quot; menu command.
</li><li>RGBControl.xaml - Defines the XAML layout for the WPF RGBControl. </li><li>RGBControl.xaml.cs/vb - Implements the code-behind for the RGBControl. </li><li>RGBToolWindow.cs/vb - Defines the tool window pane, creates the toolbar, and responds to IOleCommandTarget commands.
</li></ul>
