Change Analytic Report Data Source Sample for PerformancePoint® Services for Microsoft® SharePoint Server® 2010
Provided by: Paul Thrasher, Josh Unger, and Diane Diaz, Microsoft Corporation
Updated: August 2011

Copyright © 2011 Microsoft Corporation. All Rights Reserved.


The Change Analytic Report Data Source Sample enables users to change the data source that a PerformancePoint analytic report points to. Users can select a target data source that exists in the same site as the report. For the report to work correctly, the target data source must have the same structure as the original data source.

Important:  This sample is for demonstration purposes only and is not supported by Microsoft. It was designed and tested on a single-server farm configuration that uses one administrative user account. When developing your own implementation, be sure to follow coding best practices for security and performance. 


The Change Analytic Report Data Source Sample is packaged as a SharePoint feature in a Microsoft Visual Studio 2010 solution. It contains the following key components:
  * A button that is enabled on the PerformancePoint Content List ribbon. The button's functionality is defined in JavaScript (ECMAScript) and uses the SharePoint client-side object model to get the selected list items.
  * A SharePoint modal dialog box (.aspx page) that displays a DropDownList control for the available Analysis Services data sources within the site and TextBox controls for the selected reports on the current page. The C# code-behind class calls the PerformancePoint server-side API. It replaces the reference to the location of the report's data source, which is stored in the ReportView.CustomData property.


Sample Language Implementations
This sample is available in the following language implementations:
  * C#


Solution Files
The following solution files contain the core logic for the sample:
  * ChangeDataSource.aspx.cs is the code-behind class for the SharePoint modal dialog box. It uses PerformancePoint and SharePoint server-side APIs.
  * Elements.xml contains the functions that are used by the ribbon button. It uses the PerformancePoint JSOM and opens a SharePoint modal dialog box.


Prerequisites
Before you can test the Change Analytic Report Data Source Sample, you must install and configure the following: 
  * Microsoft SharePoint Server 2010 with Enterprise Client Access License
  * PerformancePoint Services for Microsoft SharePoint Server 2010
  * Microsoft Visual Studio 2010

Note:  These instructions assume that you are running Visual Studio on the computer that is running SharePoint Server and that you have sufficient permissions to deploy the feature and to access and create PerformancePoint ADOMD data sources.


Testing the Sample
To test the Change Analytic Report Data Source Sample, you must deploy it from Visual Studio 2010 and then open a PerformancePoint Content List that contains an analytic report. For the report to work correctly, the target data source must have the same structure as the original data source.

To test the Change Analytic Report Data Source Sample
========================================================
  1. In Visual Studio, open the ChangeDataSource.sln file in the PPSvcs2010ChangeDataSrcSample folder.
  2. In the Properties window for the ChangeDataSource project, type the absolute address of your development test site for the Site URL property. Be sure to include a closing forward slash. Example: http://contoso/BICenter/
     (To open the Properties window, click the View menu, and then click Properties Window.)
  3. Press F5 to deploy the feature.
  4. In Internet Explorer, open a PerformancePoint Content List that contains an analytic report.
  5. Select at least one analytic report.
  6. On the ribbon, click the List tab, and then click the Change Data Source button in the PerformancePoint group.
  7. In the Change Analytic Report Data Source window, select the target data source.
  8. Click OK, and then close the window.
  9. In the list, open the report's context menu, and then click Edit in Dashboard Designer.
 10. In Dashboard Designer, click the Properties tab for the report. The name of new data source is displayed in the Related Data Sources pane. 


Additional Resources
For more information, visit the PerformancePoint Services Resource Center (http://msdn.microsoft.com/en-us/sharepoint/gg176656.aspx) and the SharePoint Developer Center (http://msdn.microsoft.com/sharepoint) on the Microsoft Developer Network (MSDN).
To browse the SharePoint 2010 SDK samples, go to MSDN Samples Gallery (http://code.msdn.microsoft.com/site/search?query=sharepoint%202010&f%5B0%5D.Value=sharepoint%202010&f%5B0%5D.Type=SearchText&ac=1) and MSDN Archive (http://archive.msdn.microsoft.com/Project/ProjectDirectory.aspx?TagName=SharePoint). We will continue to post new code samples on MSDN Code Gallery between SDK releases.

