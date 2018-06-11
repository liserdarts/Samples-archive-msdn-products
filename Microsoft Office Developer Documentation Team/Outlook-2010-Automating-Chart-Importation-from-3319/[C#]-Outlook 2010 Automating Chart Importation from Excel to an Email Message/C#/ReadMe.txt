Add-in Name: ImportExcelChartToOutlook_AddIn_CS

This Visual How To shows how to use a Microsoft Outlook add-in to access the Excel and Microsoft Word object models, to automate opening an Excel workbook, copying a chart from the workbook, and pasting the chart into an Outlook email message.

This add-in is available in the following language implementations:
C#


Files:
========

ThisAddIn.cs 
Initializes the add-in, declares instance variables, and adds a custom event handler for a new inspector. Also cleans up instance variables for the add-in upon shutdown.

Ribbon1.cs
Provides a custom ribbon for mail inspectors in compose mode. Specifies the ribbon XML in Ribbon1.xml, and implements various methods to load the custom ribbon and display it in the appropriate context. Also implements the button callback method to open a workbook and copy a chart to a message.

 
To build the add-in using Visual Studio 2010:
==================================

     1. Open Windows Explorer and navigate to the folder ImportExcelChartToOutlook_AddIn_CS.
     2. Double-click the icon for the ImportExcelChartToOutlook_AddIn_CS.sln (solution) file to open the file in Visual Studio.
     3. In the Build menu, select Build Solution. The application will be built in the default \Debug or \Release directory.


To run the add-in:
==================================

     1. Close Outlook if it is already running.
     2. Follow the steps in the last section to build the sample if you have not already done so. 
     3. In Visual Studio 2010, click the Debug menu, select Start Without Debugging. This starts Outlook.
     4. In Outlook, create a new email message.
     5. On the custom ribbon tab, MyTab, click Copy Excel Chart.


To disable the add-in:
==================================

     1. In Outlook, click File, Options, and then Add-Ins.
     2. Adjacent to Manage: COM Add-ins, click Go.
     3. Remove the check for ImportExcelChartToOutlook_AddIn_CS.





