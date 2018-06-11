# Excel 2010: Work with Several Date Functions Using Excel.WorksheetFunctionDates
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Worksheet function
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 02:19:47
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>WorksheetFunction
</strong>object including several date functions in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Work with the WorksheetFunction object, focusing on date functions.</span></p>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the TestWorksheetFunctionDates procedure, and then press F8 to single-step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestWorksheetFunctionDates()
  ' Determine the date a specified number of workdays in the future:
  Dim newDate As Date
 
  ' Look at a calendar to verify the results.
 
  ' Calculate the date 10 work days from the current date.
  ' This doesn't take holidays into account:
  newDate = WorksheetFunction.WorkDay(#11/20/2011#, 10)
  Debug.Print newDate
 
  ' To take holidays into account, you can specify an array
  ' containing a list of holiday dates, or you can specify
  ' a range containing holiday dates. The ordering of the dates
  ' doesn't matter, but you must specify the full date, including
  ' the year. Obviously, you would want to include all the holidays
  ' in your actual work calendar in this array or range:
  Dim holidays As Variant
  holidays = Array(#11/24/2011#, #12/25/2011#)
  newDate = WorksheetFunction.WorkDay(#11/20/2011#, 10, holidays)
  ' Once you take Thanksgiving Day in the US into account, ten
  ' work days pushes you out to Dec 5 2011:
  Debug.Print newDate
 
  ' What if your business doesn't use traditional weekends?
  ' The WorksheetFunction.Workday_Intl function allows you to specify
  ' not only holidays, but also specific weekend days. The default for the
  ' third parameter is 1 (Saturday/Sunday), but you can specify many
  ' predefined combinations of dates, or any custom combination. See the documentation
  ' for the Workday_Intl function for full details on the options:
 
  holidays = Array(#11/24/2011#, #12/25/2011#)
  ' Assume Saturday/Sunday weekends:
  newDate = WorksheetFunction.WorkDay_Intl(#11/20/2011#, 10, 1, holidays)
  Debug.Print newDate
  ' Use Sunday/Monday as weekend dates:
  newDate = WorksheetFunction.WorkDay_Intl(#11/20/2011#, 10, 2, holidays)
  Debug.Print newDate
 
  ' Use Friday through Monday as weekend dates. Note that the custom
  ' weekend date parameter includes 1 for each weekend date, and the string
  ' starts with Monday. In other words, specify 1 for weekend dates, 0 for
  ' work dates, in the format MTWTFSS. The string &quot;1000111&quot; represents
  ' weeked days of Friday through Monday:
  newDate = WorksheetFunction.WorkDay_Intl(#11/20/2011#, 10, &quot;1000111&quot;, holidays)
  Debug.Print newDate
 
  ' The WorksheetFunction.NetworkDays and WorksheetFunction.NetworkDays_Intl
  ' functions work like the WorkDay and WorkDay_Intl functions,
  ' except that they return the number of work days between two dates.
  ' This example demonstrates the NetworkDays_Intl function. Find the
  ' number of work days between #11/1/2011# and #1/1/2012#, taking into
  ' account holiday, and treating Friday through Monday as weekend days:
  Dim workDays As Integer
  holidays = Array(#11/24/2011#, #12/25/2011#, #1/1/2012#)
  workDays = WorksheetFunction.NetworkDays_Intl(#11/1/2011#, #1/1/2012#, &quot;1000111&quot;, holidays)
  Debug.Print &quot;There are &quot; &amp; workDays &amp; &quot; workdays between 11/1 and 1/1&quot;
 
  ' Try out the WorksheetFunction.WeekNum function. which returns
  ' the week number containing the specified date. Note that this function
  ' considers the week containing Jan 1 to be the first week of the year.
  ' This differs from a standard European method of calculating the
  ' week number for a date, so the function returns dates that are
  ' incorrect for countries that use the differing standard.
  ' Indicate the first day of the week in the second parameter (1
  ' for Sunday, the default value; 2 for Monday):
 
  Dim weekNum As Integer
  weekNum = WorksheetFunction.weekNum(Date, 1)
  Debug.Print &quot;The current week number is: &quot; &amp; weekNum
 
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestWorksheetFunctionDates()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Determine&nbsp;the&nbsp;date&nbsp;a&nbsp;specified&nbsp;number&nbsp;of&nbsp;workdays&nbsp;in&nbsp;the&nbsp;future:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;newDate&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Date</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Look&nbsp;at&nbsp;a&nbsp;calendar&nbsp;to&nbsp;verify&nbsp;the&nbsp;results.</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Calculate&nbsp;the&nbsp;date&nbsp;10&nbsp;work&nbsp;days&nbsp;from&nbsp;the&nbsp;current&nbsp;date.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;doesn't&nbsp;take&nbsp;holidays&nbsp;into&nbsp;account:</span>&nbsp;
&nbsp;&nbsp;newDate&nbsp;=&nbsp;WorksheetFunction.WorkDay(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">20</span>/<span class="visualBasic__number">2011</span>#,&nbsp;<span class="visualBasic__number">10</span>)&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;newDate&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;To&nbsp;take&nbsp;holidays&nbsp;into&nbsp;account,&nbsp;you&nbsp;can&nbsp;specify&nbsp;an&nbsp;array</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;containing&nbsp;a&nbsp;list&nbsp;of&nbsp;holiday&nbsp;dates,&nbsp;or&nbsp;you&nbsp;can&nbsp;specify</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;range&nbsp;containing&nbsp;holiday&nbsp;dates.&nbsp;The&nbsp;ordering&nbsp;of&nbsp;the&nbsp;dates</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;doesn't&nbsp;matter,&nbsp;but&nbsp;you&nbsp;must&nbsp;specify&nbsp;the&nbsp;full&nbsp;date,&nbsp;including</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;year.&nbsp;Obviously,&nbsp;you&nbsp;would&nbsp;want&nbsp;to&nbsp;include&nbsp;all&nbsp;the&nbsp;holidays</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;in&nbsp;your&nbsp;actual&nbsp;work&nbsp;calendar&nbsp;in&nbsp;this&nbsp;array&nbsp;or&nbsp;range:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;holidays&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>&nbsp;
&nbsp;&nbsp;holidays&nbsp;=&nbsp;Array(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">24</span>/<span class="visualBasic__number">2011</span>#,&nbsp;#<span class="visualBasic__number">12</span>/<span class="visualBasic__number">25</span>/<span class="visualBasic__number">2011</span>#)&nbsp;
&nbsp;&nbsp;newDate&nbsp;=&nbsp;WorksheetFunction.WorkDay(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">20</span>/<span class="visualBasic__number">2011</span>#,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;holidays)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Once&nbsp;you&nbsp;take&nbsp;Thanksgiving&nbsp;Day&nbsp;in&nbsp;the&nbsp;US&nbsp;into&nbsp;account,&nbsp;ten</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;work&nbsp;days&nbsp;pushes&nbsp;you&nbsp;out&nbsp;to&nbsp;Dec&nbsp;5&nbsp;2011:</span>&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;newDate&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;What&nbsp;if&nbsp;your&nbsp;business&nbsp;doesn't&nbsp;use&nbsp;traditional&nbsp;weekends?</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;WorksheetFunction.Workday_Intl&nbsp;function&nbsp;allows&nbsp;you&nbsp;to&nbsp;specify</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;not&nbsp;only&nbsp;holidays,&nbsp;but&nbsp;also&nbsp;specific&nbsp;weekend&nbsp;days.&nbsp;The&nbsp;default&nbsp;for&nbsp;the</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;third&nbsp;parameter&nbsp;is&nbsp;1&nbsp;(Saturday/Sunday),&nbsp;but&nbsp;you&nbsp;can&nbsp;specify&nbsp;many</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;predefined&nbsp;combinations&nbsp;of&nbsp;dates,&nbsp;or&nbsp;any&nbsp;custom&nbsp;combination.&nbsp;See&nbsp;the&nbsp;documentation</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;the&nbsp;Workday_Intl&nbsp;function&nbsp;for&nbsp;full&nbsp;details&nbsp;on&nbsp;the&nbsp;options:</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;holidays&nbsp;=&nbsp;Array(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">24</span>/<span class="visualBasic__number">2011</span>#,&nbsp;#<span class="visualBasic__number">12</span>/<span class="visualBasic__number">25</span>/<span class="visualBasic__number">2011</span>#)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Assume&nbsp;Saturday/Sunday&nbsp;weekends:</span>&nbsp;
&nbsp;&nbsp;newDate&nbsp;=&nbsp;WorksheetFunction.WorkDay_Intl(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">20</span>/<span class="visualBasic__number">2011</span>#,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">1</span>,&nbsp;holidays)&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;newDate&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;Sunday/Monday&nbsp;as&nbsp;weekend&nbsp;dates:</span>&nbsp;
&nbsp;&nbsp;newDate&nbsp;=&nbsp;WorksheetFunction.WorkDay_Intl(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">20</span>/<span class="visualBasic__number">2011</span>#,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">2</span>,&nbsp;holidays)&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;newDate&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;Friday&nbsp;through&nbsp;Monday&nbsp;as&nbsp;weekend&nbsp;dates.&nbsp;Note&nbsp;that&nbsp;the&nbsp;custom</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;weekend&nbsp;date&nbsp;parameter&nbsp;includes&nbsp;1&nbsp;for&nbsp;each&nbsp;weekend&nbsp;date,&nbsp;and&nbsp;the&nbsp;string</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;starts&nbsp;with&nbsp;Monday.&nbsp;In&nbsp;other&nbsp;words,&nbsp;specify&nbsp;1&nbsp;for&nbsp;weekend&nbsp;dates,&nbsp;0&nbsp;for</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;work&nbsp;dates,&nbsp;in&nbsp;the&nbsp;format&nbsp;MTWTFSS.&nbsp;The&nbsp;string&nbsp;&quot;1000111&quot;&nbsp;represents</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;weeked&nbsp;days&nbsp;of&nbsp;Friday&nbsp;through&nbsp;Monday:</span>&nbsp;
&nbsp;&nbsp;newDate&nbsp;=&nbsp;WorksheetFunction.WorkDay_Intl(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">20</span>/<span class="visualBasic__number">2011</span>#,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__string">&quot;1000111&quot;</span>,&nbsp;holidays)&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;newDate&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;WorksheetFunction.NetworkDays&nbsp;and&nbsp;WorksheetFunction.NetworkDays_Intl</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;functions&nbsp;work&nbsp;like&nbsp;the&nbsp;WorkDay&nbsp;and&nbsp;WorkDay_Intl&nbsp;functions,</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;except&nbsp;that&nbsp;they&nbsp;return&nbsp;the&nbsp;number&nbsp;of&nbsp;work&nbsp;days&nbsp;between&nbsp;two&nbsp;dates.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;example&nbsp;demonstrates&nbsp;the&nbsp;NetworkDays_Intl&nbsp;function.&nbsp;Find&nbsp;the</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;number&nbsp;of&nbsp;work&nbsp;days&nbsp;between&nbsp;#11/1/2011#&nbsp;and&nbsp;#1/1/2012#,&nbsp;taking&nbsp;into</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;account&nbsp;holiday,&nbsp;and&nbsp;treating&nbsp;Friday&nbsp;through&nbsp;Monday&nbsp;as&nbsp;weekend&nbsp;days:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;workDays&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;holidays&nbsp;=&nbsp;Array(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">24</span>/<span class="visualBasic__number">2011</span>#,&nbsp;#<span class="visualBasic__number">12</span>/<span class="visualBasic__number">25</span>/<span class="visualBasic__number">2011</span>#,&nbsp;#<span class="visualBasic__number">1</span>/<span class="visualBasic__number">1</span>/<span class="visualBasic__number">2012</span>#)&nbsp;
&nbsp;&nbsp;workDays&nbsp;=&nbsp;WorksheetFunction.NetworkDays_Intl(#<span class="visualBasic__number">11</span>/<span class="visualBasic__number">1</span>/<span class="visualBasic__number">2011</span>#,&nbsp;#<span class="visualBasic__number">1</span>/<span class="visualBasic__number">1</span>/<span class="visualBasic__number">2012</span>#,&nbsp;<span class="visualBasic__string">&quot;1000111&quot;</span>,&nbsp;holidays)&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;There&nbsp;are&nbsp;&quot;</span>&nbsp;&amp;&nbsp;workDays&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;workdays&nbsp;between&nbsp;11/1&nbsp;and&nbsp;1/1&quot;</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;out&nbsp;the&nbsp;WorksheetFunction.WeekNum&nbsp;function.&nbsp;which&nbsp;returns</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;week&nbsp;number&nbsp;containing&nbsp;the&nbsp;specified&nbsp;date.&nbsp;Note&nbsp;that&nbsp;this&nbsp;function</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;considers&nbsp;the&nbsp;week&nbsp;containing&nbsp;Jan&nbsp;1&nbsp;to&nbsp;be&nbsp;the&nbsp;first&nbsp;week&nbsp;of&nbsp;the&nbsp;year.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;differs&nbsp;from&nbsp;a&nbsp;standard&nbsp;European&nbsp;method&nbsp;of&nbsp;calculating&nbsp;the</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;week&nbsp;number&nbsp;for&nbsp;a&nbsp;date,&nbsp;so&nbsp;the&nbsp;function&nbsp;returns&nbsp;dates&nbsp;that&nbsp;are</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;incorrect&nbsp;for&nbsp;countries&nbsp;that&nbsp;use&nbsp;the&nbsp;differing&nbsp;standard.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Indicate&nbsp;the&nbsp;first&nbsp;day&nbsp;of&nbsp;the&nbsp;week&nbsp;in&nbsp;the&nbsp;second&nbsp;parameter&nbsp;(1</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;Sunday,&nbsp;the&nbsp;default&nbsp;value;&nbsp;2&nbsp;for&nbsp;Monday):</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;weekNum&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;weekNum&nbsp;=&nbsp;WorksheetFunction.weekNum(<span class="visualBasic__keyword">Date</span>,&nbsp;<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;The&nbsp;current&nbsp;week&nbsp;number&nbsp;is:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;weekNum&nbsp;
&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25951" href="/site/view/file/25951/1/Excel.WorksheetFunctionDates.txt">Excel.WorksheetFunctionDates.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25952" href="/site/view/file/25952/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
