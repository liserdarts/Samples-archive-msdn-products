# Combo Box - VS 2013
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
* 2014-05-29 08:43:22
## Description

<div id="longDesc">
<h2>Summary</h2>
<p><span id="ctl00_ctl00_Content_TabContentPanel_Content_wikiSourceLabel">This sample demonstrates how to create combo boxes on Visual Studio&rsquo;s toolbars. Four types of combo boxes that can be created are shown in this sample.
<br>
<br>
</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h2>Goals</h2>
<ul>
<li>Adding a Drop Down Combo to Visual Studio and handling it </li><li>Adding an Index Combo to Visual Studio and handling it </li><li>Adding a MRU Combo to Visual Studio and handling it </li><li>Adding a Dynamic Combo to Visual Studio and handling it </li><li>Controling the programmatic name of the combo box commands by placing the commands within a menu (&quot;Tools&quot; in our case) of the main menu bar.
</li></ul>
<p><br>
The main focus of this sample is the VSCT file containing the definition of these combo boxes and the command handling logic for managing these combo boxes. The C# code is minimal. The event handler functions that are called when the user selects or enters
 items into a combo box simply display a message box.<br>
<br>
The interesting aspect of managing a combo box is that in general you need to handle 2 commands. There is the main command for the combo box that can return the current value for the combo as well as accept the new input from the user. In addition there is
 a second command that is used to retrieve the list of items to be displayed in the list associated with the combo box. This second command is referred to as a &quot;GetList&quot; command. IOleCommandTarget::Exec is called on this second command with a non-null &quot;out&quot;
 parameter, through which the list of items is returned as an array of strings. Even though Exec is called on this command, it is best to think of this Exec call as an extended IOleCommandTarget::QueryStatus call. This is necessary because IOleCommandTarget::QueryStatus
 method is only able to return a single &quot;out&quot; parameter, but in the case of the combo box we need two pieces of information: the current value and the list of all choices to fill the list.<br>
<br>
There are four styles of Combo Boxes:</p>
<ol>
<li>DropDownCombo. A DropDownCombo does not let the user type into the combo box; they can only pick from the list. The string value of the element selected is returned. For example, this type of combo could be used for the &quot;Solution Configurations&quot; on the
 &quot;Standard&quot; toolbar. </li><li>IndexCombo. An IndexCombo is the same as a DropDownCombo in that it is a &quot;pick from list&quot; only combo. The difference is an IndexCombo returns the selected value as an index into the list (0 based). For example, this type of combo could be used for the &quot;Solution
 Configurations&quot; on the &quot;Standard&quot; toolbar. </li><li>MRUCombo. An MRUCombo allows the user to type into the edit box. The history of strings entered is automatically persisted by the IDE on a per-user/per-machine basis. For example, this type of combo is used for the &quot;Find&quot; combo on the &quot;Standard&quot; toolbar.
</li><li>DynamicCombo. A DynamicCombo allows the user to type into the edit box or pick from the list. The list of choices is usually fixed and is managed by the command handler for the command. For example, this type of combo is used for the &quot;Zoom&quot; combo on the
 &quot;Class Designer&quot; toolbar. </li></ol>
<p><br>
Inside the VSCT file of this sample, the command definition section defines a new toolbar, a new toolbar group, and an example of each type of combo.<br>
<br>
NOTE: We deliberatly define our toolbar group with a main menu location as its parent (in this case Tools menu -- &quot;guidSHLMainMenu:IDM<em>VS</em>MENU_TOOLS&quot;). Doing this makes sure that our commands have a Programatic name that begins with &quot;Tools.&quot;; also our
 commands will be organized into this &quot;Tools&quot; category in the Add Command dialog accessible from the Tools&rarr;Customize dialog. Our combo box commands are defined with the CommandWellOnly flag which will make our combo box commands not actually instantiated
 in the main menu UI. If the user customizes our commands onto the main menu, then they will be visible.<br>
<br>
Within a VSCT file, combo boxes are defined in a Combos section. The following strings can be supplied with a command:<br>
<br>
</p>
<ol>
<li>Button Text (required) -- displayed as label of the command on a toolbar if IconAndText flag is specified. If any of the following optional strings are missing then Button Text is used instead.
</li><li>Menu Text (optional) -- displayed as label of the command on a menu if IconAndText flag is specified.
</li><li>Tooltip Text (optional) -- displayed when mouse hovers on command </li><li>Command Well Name (optional) -- displayed as name of command in command well.
</li><li>Canonical Name (optional) -- English programmatic name of command used in Command Window and DTE.ExecuteCommand. In localized command/menu (CTO) resources, always provide the English canonical name so macros can be language independent.
</li><li>Localized Canonical Name (optional) -- Localized programmatic name of command used in Command Window, DTE.ExecuteCommand, and Tools.Options &quot;Environment/Keyboard&quot; page.
</li></ol>
<p><br>
The second section in the VSCT file, the command placement section, is used to actually place toolbar group with our combo boxes on our Toolbar.<br>
<br>
To run this sample you need to turn on the &quot;Combo Box Sample&quot; Toolbar. This can be done via the Tools&rarr;Customize... command or by right-clicking on the toolbar are in the IDE and selecting the toolbar whose name is &quot;Combo Box Sample&quot;. In addition you can
 use the Command Window (use View.OtherWindows.CommandWindow Ctrl-Alt-A command to display this window) to programmatically execute these Combo box commands. For Example, type the following in the Command Window:<br>
<br>
&gt;Tools.DropDownCombo Apples<br>
&gt;Tools.IndexCombo Tigers<br>
&gt;Tools.IndexCombo 2<br>
&gt;Tools.MRUCombo Hello<br>
&gt;Tools.DynamicCombo 34<br>
&gt;Tools.DynamicCombo ZoomToFit<br>
<br>
<br>
</p>
<h2>To start the sample:</h2>
<ol>
<li>Open the ComboBox.sln solution. </li><li>Press F5 to build the sample, register it in the experimental instance, and launch Visual Studio from the experimental instance. If this doesn't work (i.e. it complains it can't find the executable), you need to go into Project&rarr;Properties, select the
 Debug tab and enter the correct path to Visual Studio (devenv.exe) into the text box.&nbsp;
</li></ol>
</div>
