# Menu and commands - VS 2013
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2013
## Topics
* Extensibility
## IsPublished
* True
## ModifiedDate
* 2014-05-29 08:48:23
## Description

<p>This sample demonstrates how to create menu and command items and visualize them inside Visual Studio&rsquo;s menus and toolbars.
<br>
<br>
</p>
<h3>Goals</h3>
<ul>
<li>Adding a menu item / command to Visual Studio and handling it </li><li>Placing commands in various places (Solution Explorer toolbar, custom toolbar, Tools menu, editor context menu)
</li><li>Dynamic text in menu items </li><li>Associating a keybinding (keyboard shortcut) to a menu item </li></ul>
<p><br>
The main focus of this sample is the VSCT file containing the definition of these elements. The code is minimal. The event handler functions that are called when the user executes the commands simply write a message on the Output window. The only exceptions
 are the callback for the menu items with dynamic properties (text or visibility). In this case, the properties will be changed according to some logic.<br>
<br>
This sample is organized into four main areas:</p>
<ol>
<li>How to create simple menu and command items. </li><li>How to place them inside other elements provided by other packages (for example, default Visual Studio menus or toolbars) or by this same package.
</li><li>How to modify the text or the visibility of a command at runtime. </li><li>How to associate a keyboard accelerator to a command. </li></ol>
<p><br>
Inside the VSCT file, the command definition section defines a new toolbar, some menu groups, and a few commands. This section contains all the interesting parts about areas 1 and 3. The button subsection of the command definition section includes the usage
 of different visibility flags. These flags allow us to tell Visual Studio that we want to programmatically change a specific set of command properties when our package is loaded.<br>
<br>
The second section in the VSCT file, the command placement section, is of interest for area 2 listed above. In this section, you can see how to place a command inside a menu group or a menu group inside a menu.<br>
<br>
The last section, the key binding section, allows associations between commands and keyboard accelerators.<br>
<br>
<br>
</p>
<h3>To start the sample:</h3>
<ol>
<li>Open the MenuAndCommands.sln solution. </li><li>Build the solution. </li><li>Press F5 register it in the experimental instance and launch Visual Studio from the experimental instance.
</li></ol>
<p>&nbsp;</p>
<h3>To start the sample functionality:</h3>
<ul>
<li>On the <strong>View</strong> menu, click <strong>Output</strong> to display the
<strong>Output</strong> window. </li><li>On the <strong>Tools</strong> menu, click <strong>C# Command Sample</strong>. A message appears in the
<strong>Output</strong> window. </li><li>On the <strong>Tools</strong> menu, click <strong>C# Text Changes</strong>. </li><li>Click the <strong>Tools</strong> menu again and note that the text for that menu item has changed to indicate how many times you chose the command.
</li><li>On the <strong>Tools</strong> menu, click <strong>C# Dynamic Visibility 1</strong>.
</li><li>Click <strong>Tools</strong> menu again and note that the menu item has disappeared and been replaced by a
<strong>C# Dynamic Visibility 2</strong> command. Click it and <strong>C# Dynamic Visibility 1</strong> returns.
</li></ul>
<p>&nbsp;</p>
<h3>Screenshot</h3>
<h3><img src="file://tg-server-hp/PartnerDrops/VSSDKSamples/2013/VSSDK_IDE_Sample_Menu_And_Commands/description/menuandcommands.png" alt=""></h3>
<p><img id="115741" src="/vstudio/site/view/file/115741/1/Exammple.MenuAndCommands.png" alt="" width="1213" height="898"></p>
