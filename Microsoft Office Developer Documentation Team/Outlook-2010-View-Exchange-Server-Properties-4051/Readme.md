# Outlook 2010: View Exchange Server Properties Using Outlook.ExchangeAccounts
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* Exchange Server 2010
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2012-07-16 02:43:35
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates new and enhanced properties that provide information about the Exchange Server to which an account is connected in Microsoft Outlook 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Outlook 2010 provides support for having multiple Exchange Server accounts in a single profile. The new Accounts collection off of the Session object provides access to Account objects. In turn, these instances have been enhanced
 with new properties that provide information about the Exchange Server to which the&nbsp; the account is connected. In order to test this code, you need Outlook 2010 with at least one Exchange account (two or more preferred). Open the VBA editor and paste
 this code into the existing ThisOutlookSession module. Open the Immediate window and with the cursor inside this method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub GetMultipleExchangeAccounts()
    
    Dim host As Outlook.Application
    Set host = ThisOutlookSession.Application
        
    ' Check for Outlook 2010
    If Left(host.Version, 2) = &quot;14&quot; Then
        Dim acct As Outlook.Account
        Dim col As New VBA.Collection
        
        ' Enumerate the list of accounts looking for
        ' those that are Exchange Server accounts
        ' If an account is found, add it to a
        ' collection to be used later.
        For Each acct In host.Session.Accounts
            If acct.AccountType = olExchange Then
                col.Add acct
            End If
        Next
        
        ' Did the code find any accounts of the type olExchange?
        If col.Count &gt; 0 Then
            For Each acct In col
                Debug.Print &quot;ExchangeMailboxServerName: &quot; &amp; acct.ExchangeMailboxServerName
                Debug.Print &quot;  AutoDiscoverConnectionMode: &quot; &amp; acct.AutoDiscoverConnectionMode
                ' The following property returns a fairly large XML document
                ' uncomment if you wish to see this data.
                ' Debug.Print &quot;  AutoDiscoverXml: &quot; &amp; acct.AutoDiscoverXml
                Debug.Print &quot;  CurrentUser: &quot; &amp; acct.CurrentUser
                Debug.Print &quot;  DeliveryStore: &quot; &amp; acct.DeliveryStore
                Debug.Print &quot;  ExchangeConnectionMode: &quot; &amp; acct.ExchangeConnectionMode
                Debug.Print &quot;  ExchangeMailboxServerVersion: &quot; &amp; acct.ExchangeMailboxServerVersion
            Next
        Else
            MsgBox &quot;You do not have any accounts configured to use an Exchange Server.&quot;
        End If
    Else
        MsgBox &quot;This code only works with Outlook 2010.&quot;
    End If
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;GetMultipleExchangeAccounts()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;host&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Outlook.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;host&nbsp;=&nbsp;ThisOutlookSession.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Check&nbsp;for&nbsp;Outlook&nbsp;2010</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Left(host.Version,&nbsp;<span class="visualBasic__number">2</span>)&nbsp;=&nbsp;<span class="visualBasic__string">&quot;14&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;acct&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Outlook.Account&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;col&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;VBA.Collection&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Enumerate&nbsp;the&nbsp;list&nbsp;of&nbsp;accounts&nbsp;looking&nbsp;for</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;those&nbsp;that&nbsp;are&nbsp;Exchange&nbsp;Server&nbsp;accounts</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;an&nbsp;account&nbsp;is&nbsp;found,&nbsp;add&nbsp;it&nbsp;to&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;collection&nbsp;to&nbsp;be&nbsp;used&nbsp;later.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;acct&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;host.Session.Accounts&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;acct.AccountType&nbsp;=&nbsp;olExchange&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;col.Add&nbsp;acct&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Did&nbsp;the&nbsp;code&nbsp;find&nbsp;any&nbsp;accounts&nbsp;of&nbsp;the&nbsp;type&nbsp;olExchange?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;col.Count&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;acct&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;ExchangeMailboxServerName:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;acct.ExchangeMailboxServerName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;AutoDiscoverConnectionMode:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;acct.AutoDiscoverConnectionMode&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;following&nbsp;property&nbsp;returns&nbsp;a&nbsp;fairly&nbsp;large&nbsp;XML&nbsp;document</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;uncomment&nbsp;if&nbsp;you&nbsp;wish&nbsp;to&nbsp;see&nbsp;this&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Debug.Print&nbsp;&quot;&nbsp;&nbsp;AutoDiscoverXml:&nbsp;&quot;&nbsp;&amp;&nbsp;acct.AutoDiscoverXml</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;CurrentUser:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;acct.CurrentUser&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;DeliveryStore:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;acct.DeliveryStore&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;ExchangeConnectionMode:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;acct.ExchangeConnectionMode&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;ExchangeMailboxServerVersion:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;acct.ExchangeMailboxServerVersion&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;You&nbsp;do&nbsp;not&nbsp;have&nbsp;any&nbsp;accounts&nbsp;configured&nbsp;to&nbsp;use&nbsp;an&nbsp;Exchange&nbsp;Server.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;This&nbsp;code&nbsp;only&nbsp;works&nbsp;with&nbsp;Outlook&nbsp;2010.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26105" href="/site/view/file/26105/1/Outlook.ExchangeAccounts.txt">Outlook.ExchangeAccounts.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26106" href="/site/view/file/26106/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905455">Outlook Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
