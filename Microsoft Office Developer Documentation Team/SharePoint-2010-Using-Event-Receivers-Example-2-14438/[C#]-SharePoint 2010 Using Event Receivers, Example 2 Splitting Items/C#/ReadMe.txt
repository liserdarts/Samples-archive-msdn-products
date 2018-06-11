SharePoint 2010: Using Event Receivers, Example 2: Splitting Items
==================================================================

In this example, the objective is to add an item to a list and have that item split into two new items. For example, when a user adds an Xbox 360 bundle, you might want to prevent that item from being added and instead add two other items to the list: "Xbox 360" and "Kinect."

An even better example is a scenario in which you upload a .zip file and then have the contents of the file extracted with each of its files added as a separate item.

Event receivers in Microsoft SharePoint Foundation 2010 enable your custom code to respond when specific actions occur on a SharePoint object. This code sample is one of five that are discussed in the article Using Event Receivers in SharePoint Foundation 2010 (Part 2 of 2) [http://msdn.microsoft.com/en-us/library/gg981880.aspx#UsingEventReceiversInSPFPart2_SplittingItems]. Read the article for detailed instructions.

Building the Sample
===================

To build the sample by using Microsoft Visual Studio:

	1. Open Windows Explorer and navigate to your download directory.
	2. Double-click the icon for the .sln (solution) file to open the file in Visual Studio.
	3. In the Properties pane of Visual Studio, change the value of Site URL to the absolute address of your development test site—for example, "http://MyDevServer/". Be sure to include the closing forward slash.
	4. In the Build menu, select Build Solution. The application will be built in the default \Debug or \Release directory.

More Information
================

To learn about the event model in SharePoint Foundation 2010 and how to use event receivers in your code, read Using Event Receivers in SharePoint Foundation 2010 (Part 1 of 2) [http://msdn.microsoft.com/en-us/library/gg749858.aspx].
