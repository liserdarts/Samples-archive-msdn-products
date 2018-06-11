# Outlook 2010: Time-Reporting Tool Based on the Outlook 2010 Calendar
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Office
* Outlook 2010
## Topics
* calendars
* time reporting
* appointments
* time tracking
## IsPublished
* True
## ModifiedDate
* 2011-07-25 09:44:47
## Description

<p><span style="font-size:small">This code sample creates&nbsp;an add-in to report on time spent in tasks that are tracked in the Microsoft Outlook 2010 calendar. This add-in uses categories that you assign to appointment items for time-tracking purposes. This
 sample accompanies the article <a href="http://msdn.microsoft.com/en-us/library/hh144973.aspx">
Creating a Simple Time-Reporting Tool Based on the Outlook 2010 Calendar</a> in the MSDN Library.</span></p>
<p><span style="font-size:small">This add-in is meant to demonstrate some key operations:</span></p>
<ul>
<li><span style="font-size:small">How to get a set of appointments from the calendar, including recurring appointments, for a specific time frame.</span>
</li><li><span style="font-size:small">How to get the set of categories that are available in the Outlook session.</span>
</li><li><span style="font-size:small">How to get the set of categories that are assigned to an appointment item.</span><span style="font-size:small">&nbsp;</span>
</li></ul>
<p><span style="font-size:small">This add-in reports the time that is spent in appointments, based on the categories assigned to them. This methodology assumes that you create appointments in your default calendar to reflect the time spent on various tasks.
 It also assumes that you assign categories to those appointments to signify the categories of time you want to include in the report. The report is written to a file in CSV format.</span></p>
<p><span style="font-size:small">The add-in has a few classes of interest:</span></p>
<ul>
<li><span style="font-size:small">The <span style="font-family:courier new,courier">
RequestSummaryForm </span>class, which is a Windows Form.</span> </li><li><span style="font-size:small">The <span style="font-family:courier new,courier">
ScheduleReportGenerator </span>class, which contains the reporting logic.</span> </li><li><span style="font-size:small">The <span style="font-family:courier new,courier">
ScheduleItem </span>and <span style="font-family:courier new,courier">ScheduleReport
</span>classes, which are defined inside the <span style="font-family:courier new,courier">
ScheduleReportGenerator </span>class. These two classes encapsulate some of the reporting data and logic.</span>
</li></ul>
<p><span style="font-size:small">This code sample uses a C# Outlook add-in project written in Visual Studio 2010 and assumes you are already familiar with C# and creating custom forms and add-ins for Outlook.</span></p>
