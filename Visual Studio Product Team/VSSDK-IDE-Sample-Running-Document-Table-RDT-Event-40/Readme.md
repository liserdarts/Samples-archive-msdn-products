# VSSDK IDE Sample: Running Document Table (RDT) Event Explorer
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
* 2011-03-01 05:34:31
## Description

<h1><span style="font-size:large">Introduction</span></h1>
<p>This sample demonstrates how to create an explorer that logs Running Document Table (RDT) events. Selecting an event from the grid displays its properties in the Properties Window.</p>
<h1><br>
<span style="font-size:large">Goals</span></h1>
<ul>
<li>Provides a tool to explore the RDT that follows recommended design patterns </li><li>Exposes properties in the Properties window based on the selected item </li><li>RDT events are captured on a grid </li><li>Logged events are filtered by Tools/Options page options </li><li>The user controls the display via a toolbar </li><li>Tool window and dialog page share a singleton instance of options via automation
</li></ul>
<p>This sample has a package (RdtEventExplorerPkg) and a tool window (RDTEventWindowPane). The tool window hosts a UserControl (RdtEventControl). The options (Options) are set by a dialog page (RdtEventOptionsDialog) and filter the RDT events (derived from
 GenericEvent).</p>
<p>The explorer window hosts a toolbar and displays all unfiltered RDT events in a grid. Selecting an event in the grid displays its properties in the Properties window.</p>
<h1><br>
<span style="font-size:large">To start the sample</span></h1>
<ol>
<li>Open the RdtEventExplorer.sln solution. </li><li>Press F5 to build the sample, register it in the experimental instance, and launch Visual Studio from the experimental instance.
</li></ol>
<h1><br>
<span style="font-size:large">To see the sample's functionality:</span></h1>
<ul>
<li>On the <strong>View </strong>menu, point to <strong>Other Windows</strong> and then click
<strong>RdtEventExplorer</strong>. The <strong>Rdt Event Explorer </strong>tool window appears.
</li><li>On the <strong>Tools </strong>menu, click <strong>Options </strong>and then navigate to the
<strong>RDT Event Explorer </strong>page. </li><li>Set <strong>OptBeforeFirstDocumentLock </strong>to False, and then click <strong>
OK</strong>. </li><li>Open a new or existing Visual Studio project. The RDT events show up in the RDT Event Explorer grid. No OptBeforeFirstDocumentLock events appear.
</li><li>Press F4 to open the <strong>Properties </strong>window. </li><li>Select an event in the grid. The properties of that event appear in the <strong>
Properties </strong>window. </li><li>On the toolbar in the RDT Event Explorer, click <strong>Clear</strong>. Events are cleared from the grid.
</li><li>To open a MyC file...
<ul>
<li>On the <strong>File </strong>menu, select <strong>Open File</strong>. </li><li>Find the tflow.myc file and click <strong>OK</strong>. You now have an open code window in which you can see a MyC file. All the identifiers are colored as you described on the Language Tokens page of the Language Service Wizard.
</li></ul>
</li></ul>
