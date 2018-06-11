# Windows Forms Control Installer - VS 2013
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2013
## Topics
* Extensibility
## IsPublished
* True
## ModifiedDate
* 2014-05-29 07:04:10
## Description

<div id="longDesc">
<h1><span>Introduction</span></h1>
<p>This sample demonstrates how to create a Visual Studio package (VSPackage) that loads custom Windows Forms controls into the Toolbox. The code defines two toolbox items: MyCustomTextBox, which is a normal Windows Forms control, and MyCustomTextBoxWithPopup,
 which provides a custom ToolboxItem that pops up a dialog box when the item is added.</p>
<h1><span>Getting Started</span></h1>
<p>To build and run this sample, you must have Visual Studio 2013 installed, as well as the Visual Studio SDK. Unzip the zipfile into your Visual Studio Projects directory (My Documents\Visual Studio 2013\Projects) and open the WinformsControlsInstaller.sln
 solution.</p>
<h1><span>Building the Sample</span></h1>
<p>To build the sample, make sure the solution is open and then use the Build | Build Solution menu command.</p>
<h1><span>Running the Sample</span></h1>
<p>To run the sample, hit F5 or choose the Debug | Start Debugging menu command. A new instance of Visual Studio will launch under the experimental hive. The experimental hive is a special &quot;sandbox&quot; instance of Visual Studio that allows you to develop and test
 packages without affecting your main instance of Visual Studio. Once loaded, create a new Windows Forms Application in C#, Visual Basic, or C&#43;&#43;.</p>
<p>After your project is created, open the toolbox via the View | Toolbox menu command. If your Windows Forms Designer is open, you should see a new tab at the bottom of the toolbox called &quot;MyNewTab&quot; containing two new toolbox items. Double-click on each of
 them to add them to your form.</p>
<h1><span>Project Files</span></h1>
<ul>
<li>WinformsControlsInstallerPackage.cs/vb - Defines the Visual Studio Package, the MyCustomTextBox class, the MyCustomTextBoxWithPopup class, and the MyToolboxItem class.&nbsp;
</li></ul>
</div>
