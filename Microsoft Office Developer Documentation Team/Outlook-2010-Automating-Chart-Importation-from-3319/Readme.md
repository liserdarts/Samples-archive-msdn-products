# Outlook 2010: Automating Chart Importation from Excel to an Email Message
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Office
* Outlook 2010
* Excel 2010
* Word 2010
## Topics
* Ribbon Extensibility
* Automation
## IsPublished
* True
## ModifiedDate
* 2011-06-07 10:39:21
## Description

<h1><span style="font-size:medium">Introduction</span></h1>
<p><span style="font-family:verdana,geneva; font-size:small">This C# sample&nbsp;is an&nbsp;Outloook 2010 add-in that automates opening an Excel workbook, copying a chart from the workbook, and pasting the chart into an Outlook email message. This sample accompanies
 the Visual How To <a href="http://msdn.microsoft.com/en-us/library/hh227293.aspx" target="_blank">
Automating Chart Importation from Excel to an Email Message in Outlook 2010</a> in the MSDN Library.</span></p>
<p><span style="font-family:verdana,geneva; font-size:small">The add-in provides a custom user interface in Outlook for you to select and copy a chart from an Excel workbook to an email message in compose mode.&nbsp;The&nbsp;Visual How To that accompanies this
 sample&nbsp;assumes you are familiar with C# and creating add-ins for Outlook.</span></p>
<h1><span style="font-size:medium">Source Code Files</span></h1>
<p><span style="font-size:small">The Outlook add-in solution is named ImportExcelChartToOutlook_AddIn_CS. The solution contains the following noteworthy classes:</span></p>
<ul>
<li><span style="font-size:small">ThisAddIn.cs&mdash;Initializes the add-in, declares instance variables, and adds a custom event handler for a new inspector. Also cleans up instance variables for the add-in upon shutdown.</span>
</li><li><span style="font-size:small">Ribbon1.cs&mdash;Provides a custom ribbon for mail inspectors in compose mode. Specifies the ribbon XML in Ribbon1.xml, and implements various methods to load the custom ribbon and display it in the appropriate context. Also
 implements the button callback method to open a workbook and copy a chart to a message.</span>
</li></ul>
<h1><span style="font-size:small">To build the add-in</span></h1>
<ol>
<li><span style="font-size:small">Open Windows Explorer and navigate to the folder where you unzipped the solution.</span>
</li><li><span style="font-size:small">Double-click the ImportExcelChartToOutlook_AddIn_CS.sln (solution) file to open the file in Visual Studio.</span>
</li><li><span style="font-size:small">In the Build menu, select Build Solution. The application will be built in the default \Debug or \Release directory.</span>
</li></ol>
<p><span style="font-size:small"><strong>To run the add-in</strong></span></p>
<ol>
<li><span style="font-size:small">Close Outlook if it is already running.</span> </li><li><span style="font-size:small">Follow the steps in the last section to build the sample if you have not already done so.</span>
</li><li><span style="font-size:small">In Visual Studio 2010, click the Debug menu, and then click Start Without Debugging. This starts Outlook.</span>
</li><li><span style="font-size:small">In Outlook, create a new email message.</span> </li><li><span style="font-size:small">On the custom ribbon tab, MyTab, click Copy Excel Chart.</span>
</li></ol>
