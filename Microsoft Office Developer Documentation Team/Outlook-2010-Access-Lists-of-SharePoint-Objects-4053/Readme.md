# Outlook 2010: Access Lists of SharePoint Objects Using Outlook.PickerDialog
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* PickerDialog object
## IsPublished
* True
## ModifiedDate
* 2011-08-04 04:35:32
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the new <strong>PickerDialog
</strong>object in Microsoft Outlook 2010, to access a SharePoint server to get a list of objects such as users, distribution lists, and so forth.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">This demo uses the new Office 2010 PickerDialog from Outlook 2010. The code accesses a SharePoint server to get a list of 'people'. These people can be users, distribution lists, etc.</span></p>
<p><span style="font-size:small">This code only looks for users. Assuming the user picks at least one user, the code enumerates the results and creates an e-mail message adding each selected user's display name to the mail message&rsquo;s Recipients collection.
 It then displays the mail message using the Outlook user interface for mail messages.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Public Sub PickerDialogDemo()
    Dim dlg As Office.PickerDialog
    Set dlg = Application.PickerDialog
    dlg.Title = &quot;Select a user to e-mail&quot;

    ' PEOPLEDATAHANDLER.DLL is an in-process COM DLL that provides 
    ' the data handler implementation used in this example.
    ' Currently this is the only documented handler available for use. 
    Dim peopleDataHandlerServerCLSID As String
    peopleDataHandlerServerCLSID = &quot;{000CDF0A-0000-0000-C000-000000000046}&quot;
    dlg.DataHandlerId = peopleDataHandlerServerCLSID
    
    Dim pickerProps As Office.PickerProperties
    Set pickerProps = dlg.Properties
    Dim pickerProp As Office.PickerProperty
    Dim pickerID As String
    pickerID = &quot;SiteUrl&quot;
    Dim sharepointURL As String
    ' Change the following URL to a valid
    ' SharePoint server URL to which you have access rights.
    ' This example was tested against a SharePont 2010 Server.
    sharepointURL = &quot;http://my&quot;
    Set pickerProp = pickerProps.Add(pickerID, sharepointURL, _
        Office.MsoPickerField.msoPickerFieldText)
    
    Dim dlgResults As Office.PickerResults
    ' When the code shows the dialog, search for at least one user
    ' and then add at least one user before closing the PickerDialog.
    &lsquo; What does that True parameter mean?
    Set dlgResults = dlg.Show(True)
    
    If Not dlgResults Is Nothing Then
        ' The user selected at least one item so the
        ' code will create a new MailItem.
        Dim newMail As Outlook.MailItem
        Set newMail = Application.CreateItem(olMailItem)
        newMail.Subject = &quot;PickerDialog Demo&quot;
        
        Dim result As Office.PickerResult
        For Each result In dlgResults
            ' If the result is a user, add them as a recipient
            ' to the new mail message.
            If result.Type = &quot;User&quot; Then
                newMail.Recipients.Add result.DisplayName
            End If
        Next
        ' Validate the users&rsquo; display names added to the list
        ' of mail recipients.
        newMail.Recipients.ResolveAll
        
        ' Retrieve a reference to the Inspector for the mail message
        ' and display it. Note that you might need to navigate away from
        ' the VBA editor to see the message.
        Dim mailInspector As Outlook.Inspector
        Set mailInspector = newMail.GetInspector
        mailInspector.Display
    Else
        ' If you click Cancel, the code falls through here.
    End If
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;PickerDialogDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dlg&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Office.PickerDialog&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;dlg&nbsp;=&nbsp;Application.PickerDialog&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;dlg.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Select&nbsp;a&nbsp;user&nbsp;to&nbsp;e-mail&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;PEOPLEDATAHANDLER.DLL&nbsp;is&nbsp;an&nbsp;in-process&nbsp;COM&nbsp;DLL&nbsp;that&nbsp;provides&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;data&nbsp;handler&nbsp;implementation&nbsp;used&nbsp;in&nbsp;this&nbsp;example.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Currently&nbsp;this&nbsp;is&nbsp;the&nbsp;only&nbsp;documented&nbsp;handler&nbsp;available&nbsp;for&nbsp;use.&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;peopleDataHandlerServerCLSID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;peopleDataHandlerServerCLSID&nbsp;=&nbsp;<span class="visualBasic__string">&quot;{000CDF0A-0000-0000-C000-000000000046}&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;dlg.DataHandlerId&nbsp;=&nbsp;peopleDataHandlerServerCLSID&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pickerProps&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Office.PickerProperties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pickerProps&nbsp;=&nbsp;dlg.Properties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pickerProp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Office.PickerProperty&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pickerID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pickerID&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SiteUrl&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sharepointURL&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;the&nbsp;following&nbsp;URL&nbsp;to&nbsp;a&nbsp;valid</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;SharePoint&nbsp;server&nbsp;URL&nbsp;to&nbsp;which&nbsp;you&nbsp;have&nbsp;access&nbsp;rights.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;example&nbsp;was&nbsp;tested&nbsp;against&nbsp;a&nbsp;SharePont&nbsp;2010&nbsp;Server.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sharepointURL&nbsp;=&nbsp;<span class="visualBasic__string">&quot;http://my&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pickerProp&nbsp;=&nbsp;pickerProps.Add(pickerID,&nbsp;sharepointURL,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Office.MsoPickerField.msoPickerFieldText)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dlgResults&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Office.PickerResults&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;When&nbsp;the&nbsp;code&nbsp;shows&nbsp;the&nbsp;dialog,&nbsp;search&nbsp;for&nbsp;at&nbsp;least&nbsp;one&nbsp;user</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;then&nbsp;add&nbsp;at&nbsp;least&nbsp;one&nbsp;user&nbsp;before&nbsp;closing&nbsp;the&nbsp;PickerDialog.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lsquo;&nbsp;What&nbsp;does&nbsp;that&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;parameter&nbsp;mean?&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;dlgResults&nbsp;=&nbsp;dlg.Show(<span class="visualBasic__keyword">True</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;dlgResults&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;user&nbsp;selected&nbsp;at&nbsp;least&nbsp;one&nbsp;item&nbsp;so&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;code&nbsp;will&nbsp;create&nbsp;a&nbsp;new&nbsp;MailItem.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;newMail&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Outlook.MailItem&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;newMail&nbsp;=&nbsp;Application.CreateItem(olMailItem)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newMail.Subject&nbsp;=&nbsp;<span class="visualBasic__string">&quot;PickerDialog&nbsp;Demo&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;result&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Office.PickerResult&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;result&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;dlgResults&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;result&nbsp;is&nbsp;a&nbsp;user,&nbsp;add&nbsp;them&nbsp;as&nbsp;a&nbsp;recipient</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;the&nbsp;new&nbsp;mail&nbsp;message.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;result.Type&nbsp;=&nbsp;<span class="visualBasic__string">&quot;User&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newMail.Recipients.Add&nbsp;result.DisplayName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Validate&nbsp;the&nbsp;users&rsquo;&nbsp;display&nbsp;names&nbsp;added&nbsp;to&nbsp;the&nbsp;list</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;of&nbsp;mail&nbsp;recipients.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newMail.Recipients.ResolveAll&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;Inspector&nbsp;for&nbsp;the&nbsp;mail&nbsp;message</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;display&nbsp;it.&nbsp;Note&nbsp;that&nbsp;you&nbsp;might&nbsp;need&nbsp;to&nbsp;navigate&nbsp;away&nbsp;from</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;VBA&nbsp;editor&nbsp;to&nbsp;see&nbsp;the&nbsp;message.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;mailInspector&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Outlook.Inspector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;mailInspector&nbsp;=&nbsp;newMail.GetInspector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mailInspector.Display&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you&nbsp;click&nbsp;Cancel,&nbsp;the&nbsp;code&nbsp;falls&nbsp;through&nbsp;here.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26109" href="/site/view/file/26109/1/Outlook.PickerDialog.txt">Outlook.PickerDialog.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26110" href="/site/view/file/26110/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905455">Outlook Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
