Save Analytic Report Sample for PerformancePoint® Services for Microsoft® SharePoint Server® 2010
Provided by: Paul Thrasher, Josh Unger, and Diane Diaz,  Microsoft Corporation.
Updated: August 2011

The Save Analytic Report Sample shows how to create a "Save as a Favorite" feature that enables users to save a copy of a PerformancePoint analytic report in its current navigated state. Users can save the report to a PerformancePoint Content List within the current site.

Important:  This sample is for demonstration purposes only and is not supported by Microsoft. It was designed and tested on a single-server farm configuration that uses one administrative user account. When developing your own implementation, be sure to follow coding best practices for security and performance. 


The Save Analytic Report Sample is packaged as a SharePoint feature in a Microsoft Visual Studio® 2010 solution. It contains the following key components:
  * A button that is enabled on the Web Parts Page ribbon when the dashboard page contains a PerformancePoint analytic report. The button uses ECMAScript (JScript, JavaScript) to call the ClientConnectionManager object (from the PerformancePoint JavaScript object model (JSOM)) and to retrieve information about the PerformancePoint Web Parts on the page.
  * A SharePoint modal dialog box (ASPX page) that displays a DropDownList control for the analytic reports that are on the page, TextBox controls for the name, description, and display folder of the selected report, and a DropDownList control for the PerformancePoint Content Lists that the user has access to within the site. The C# code-behind class makes calls to the PerformancePoint server-side API. It uses the BIMonitoringServiceApplicationProxy.GetAnalyticReportView method to retrieve the report in its current navigated state from the back-end database, and it uses the SPDataStore.CreateReportView method to save a copy of the report to a list.


Sample Language Implementations
This sample is available in the following language implementations:
  * C#


Solution Files
The following solution files contain the core logic for the sample:
  * SaveReport.aspx.cs is the code-behind class for the SharePoint modal dialog box. It uses PerformancePoint and SharePoint server-side APIs.
  * SaveReportFavoriteButtonScript.js contains the functions that are used by the ribbon button. It uses the PerformancePoint JSOM and opens a SharePoint modal dialog box.
  

Prerequisites
Before you can test the Save Analytic Report Sample, you must install and configure the following: 
  * Microsoft SharePoint Server 2010 with Enterprise Client Access License
  * PerformancePoint Services for Microsoft SharePoint Server 2010
  * Microsoft Visual Studio 2010

Note:  These instructions assume that you are running Visual Studio on the computer that is running SharePoint Server and that you have sufficient permissions to deploy the feature and to view and save a report.


Testing the Sample
To test the Save Analytic Report Sample, you must deploy it from Visual Studio 2010 and then open a dashboard page that contains an analytic report.

To test the Save Analytic Report Sample
==========================================
  1. In Visual Studio, open SaveReportFavorite.sln from the PPSvcs2010SaveReportSample folder.
  2. In the Properties window for the SaveReportFavorite project, type the absolute address of your development test site for the Site URL property. Be sure to include a closing forward slash. Example: http://contoso/BICenter/
     (To open the Properties window, click the View menu, and then click Properties Window.)
  3. Press F5 to deploy the feature.
  4. In Internet Explorer, open a dashboard page that contains an analytic report.
  5. Navigate on the report that you want to save. For example, drill down on a member.
  6. On the ribbon, click the Page tab, and then click the Save Analytic Report button in the PerformancePoint group.
  7. In the Save Analytic Report window, select the report that you navigated on.
  8. Type a new name, description, and display folder name.
  9. Select the list that you want to save the report to.
 10. Click OK, and then close the window.
 11. Browse to the list that you saved the report to.
 12. Open the report's context menu, and then click Edit in Dashboard Designer or Display Report.


Additional Resources
For more information, visit the PerformancePoint Services Resource Center (http://msdn.microsoft.com/en-us/sharepoint/gg176656.aspx) and the SharePoint Developer Center (http://msdn.microsoft.com/sharepoint) on the Microsoft Developer Network (MSDN).
To browse the SharePoint 2010 SDK samples, go to MSDN Samples Gallery (http://code.msdn.microsoft.com/site/search?query=sharepoint%202010&f%5B0%5D.Value=sharepoint%202010&f%5B0%5D.Type=SearchText&ac=1) and MSDN Archive (http://archive.msdn.microsoft.com/Project/ProjectDirectory.aspx?TagName=SharePoint). We will continue to post new code samples on MSDN Code Gallery between SDK releases.


© 2011 Microsoft Corporation. All Rights Reserved.






