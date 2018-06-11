# [MS-OXOCAL]: Using the RecurrencePattern structure to derive a valid recurrence
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Office Outlook 2007
* Microsoft Office Outlook 2003
* Microsoft Exchange Server 2003
* Microsoft Exchange Server 2007
* Microsoft Exchange Server 2010
* Microsoft Outlook 2010
## Topics
* Calendaring
* Appointment and Meeting object
* recurrence
* Appointment and Meeting Object Protocol
* Interoperability
## IsPublished
* True
## ModifiedDate
* 2011-09-30 03:07:15
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This code sample shows you how to find a valid recurrence in a daily recurrence pattern by using the
<strong>FirstDateTime </strong>field of the <strong>RecurrencePattern</strong> structure in the Appointment and Meeting Object Protocol, as specified in
<a href="http://msdn.microsoft.com/en-us/library/cc425490(EXCHG.80).aspx">[MS-OXOCAL]</a>.</span></p>
<h1><span>Building the sample </span></h1>
<p><span style="font-size:small">To build the sample by using Microsoft Visual Studio 2008 (preferred method):</span></p>
<ol>
<li><span style="font-size:small">Open Windows Explorer and navigate to the&nbsp; directory.</span>
</li><li><span style="font-size:small">Double-click the icon for the .sln (solution) file to open the file in Visual Studio.</span>
</li><li><span style="font-size:small">In the <strong>Build </strong>menu, select <strong>
Build Solution</strong>. The application will be built in the default \Debug or \Release directory.</span>
</li></ol>
<h1><span style="font-size:20px">Description </span></h1>
<p><span style="font-size:small">This code sample derives the<strong> FirstDateTime
</strong>field for a daily, weekly, or monthly/yearly recurrence.</span></p>
<h1><span>Source code files </span></h1>
<ul>
<li><span style="font-size:small">Calculating FirstDateTime.cs&nbsp; &mdash; Contains the sample code.</span>
</li><li><span style="font-size:small">Calculating FirstDateTime.csproj &mdash; Visual Studio project file.</span>
</li><li><span style="font-size:small">Calculating FirstDateTime.sln &mdash; Visual Studio solution file.</span>
</li></ul>
<h1>More information</h1>
<p><span style="font-size:small">For more information, see the following resources:</span></p>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/hh487300(EXCHG.140).aspx">Working with the RecurrencePattern structure in [MS-OXOCAL]</a></span></p>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/cc425490(EXCHG.80).aspx">[MS-OXOCAL]: Appointment and Meeting Object Protocol Specification</a></span></p>
<p><span style="font-size:small"><a href="http://blogs.msdn.com/b/openspecification/archive/2011/07/28/ms-oxocal-how-to-calculate-the-firstdatetime-for-monthly-and-yearly-recurring-appointments-for-the-hebrew-calendar.aspx">MS-OXOCAL - How to calculate the
 FirstDateTime for monthly and yearly recurring<br>
appointments for the Hebrew calendar.</a></span></p>
<p>&nbsp;</p>
