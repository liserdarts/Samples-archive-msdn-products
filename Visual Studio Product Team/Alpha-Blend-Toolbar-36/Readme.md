# Alpha Blend Toolbar
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
* 2011-09-11 08:59:21
## Description

<h1><span style="font-size:large">Introduction</span></h1>
<p>A Visual Studio Addin which adds a new toolbar that contains commands with alpha-blended command icons.</p>
<p>&nbsp;</p>
<h1><span style="font-size:large">Getting Started</span></h1>
<p>To build and run this sample, you must have Visual Studio 2010 installed. Unzip the AlphaBlendToolbar.zip file into your Visual Studio Projects directory (My Documents\Visual Studio 2010\Projects) and open the AlphaBlendToolbar.sln solution.</p>
<h1><span style="font-size:large">Building the Sample</span></h1>
<p>To build the sample, make sure the AlphaBlendToolbar solution is open and use the Build | Build Solution menu command.</p>
<h1><span style="font-size:large">Running the Sample</span></h1>
<p>To run this sample, copy both the AlphaBlendToolbar.Addin file and the newly-built AlphaBlendToolbar.dll file into your Visual Studio Addins directory (My Documents\Visual Studio 2010\Addins) and then open a new instance of Visual Studio 2010. Next, run
 the Tools | Add-in Manager menu command. Check the checkbox next to AlphaBlendToolbar and hit OK. You should see a new toolbar with two command buttons on it. The interesting thing about this sample is that the command button icons have alpha-transparency.</p>
<h1><span style="font-size:large">Source Code Overview</span></h1>
<p>The Connect class's OnConnection method performs a number of steps:</p>
<ul>
<li>Creates a toolbar called &quot;AlphaToolbar&quot; if one does not already exist </li><li>Retrieves or creates the &quot;AlphaButton&quot; command if it does not exist </li><li>Retrieves or creates the &quot;OmegaButton&quot; command if it does not exist </li><li>Adds the two commands to the toolbar </li></ul>
<p>We create the new toolbar by calling the Microsoft.VisualStudio.CommandBars.CommandsBars.Add() method, specifying the name, position, and permanence. Next we either find or create the alpha command. When we call EnvDTE80.Commands2.AddNamedCommand2() method,
 it creates a new command which persists between sessions of Visual Studio, which is why we try to retrieve the existing command before creating a new one. If we do need to create the command, we first create a bitmap from the embedded resource &quot;alpha.png&quot;.
 This bitmap can be passed directly to the AddNamedCommand2 method to specify the icon image. In this case, the file is a 32-bit ARGB image, meaning that it has an alpha channel to support pixel transparency. In previous versions of Visual Studio, we would
 have had to load a separate transparency mask image in order to support transparency, but in Visual Studio 2010 this transparency is supported by default.</p>
<h1><span style="font-size:large">Project Files</span></h1>
<ul>
<li>alpha.png - A Portable Network Graphic (PNG) file which is used for the alpha button icon.&nbsp;
</li><li>AlphaBlendToolbar.AddIn - An XML file which defines the load behavior of the addin.
</li><li>Connect.cs - Contains the code for the addin, especially the OnConnection method and implementation of the IDTCommandTarget interface to respond to the toolbar commands.
</li><li>omega.png - A Portable Network Graphic (PNG) file which is used for the omega button icon.
</li></ul>
