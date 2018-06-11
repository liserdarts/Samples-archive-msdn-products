# PowerPoint 2010: Add Series of Application-Level Events Using PPT.NewEvents
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* events
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 01:05:51
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to add a series of new Application-level events in Microsoft PowerPoint 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">PowerPoint 2010 adds a series of new Application-level events. Unfortunately, PowerPoint still makes it exceedingly difficult to trap and handle application-level events, so the simplest solution is to create a managed add-in
 to demonstrate the new events.</span></p>
<p><span style="font-size:small">To see the events in action, follow these steps.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">1. In Visual Studio 2010, create a new PowerPoint 2010 add-in project (use the video here to help you get started: http://msdn.microsoft.com/en-us/vsto/cc338006).

2. (C# only) In the ThisAddIn.cs class, in the ThisAddIn_Startup method, add the following code:

// C#:
Application.PresentationBeforeClose &#43;= new PowerPoint.EApplication_PresentationBeforeCloseEventHandler(Application_PresentationBeforeClose);
Application.PresentationCloseFinal &#43;= new PowerPoint.EApplication_PresentationCloseFinalEventHandler(Application_PresentationCloseFinal);
Application.ProtectedViewWindowActivate &#43;= new PowerPoint.EApplication_ProtectedViewWindowActivateEventHandler(Application_ProtectedViewWindowActivate);
Application.ProtectedViewWindowBeforeClose &#43;= new PowerPoint.EApplication_ProtectedViewWindowBeforeCloseEventHandler(Application_ProtectedViewWindowBeforeClose);
Application.ProtectedViewWindowBeforeEdit &#43;= new PowerPoint.EApplication_ProtectedViewWindowBeforeEditEventHandler(Application_ProtectedViewWindowBeforeEdit);
Application.ProtectedViewWindowDeactivate &#43;= new PowerPoint.EApplication_ProtectedViewWindowDeactivateEventHandler(Application_ProtectedViewWindowDeactivate);
Application.ProtectedViewWindowOpen &#43;= new PowerPoint.EApplication_ProtectedViewWindowOpenEventHandler(Application_ProtectedViewWindowOpen);

3. In the ThisAddIn.vb or ThisAddIn.cs class, add the following statements at the top of the file:

' Visual Basic:
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.PowerPoint

// C#:
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;

4. In the ThisAddIn.vb or ThisAddIn.cs class, add the following code, which traps and displays information about each of the new Application-level events:

' Visual Basic:
  Private Sub Application_PresentationBeforeClose(
    ByVal Pres As Presentation, ByRef Cancel As Boolean) _
    Handles Application.PresentationBeforeClose

    Cancel = (MessageBox.Show(&quot;PresentationBeforeClose&quot;, &quot;Event&quot;,
                              MessageBoxButtons.OKCancel) = DialogResult.Cancel)
  End Sub

  Private Sub Application_PresentationCloseFinal(
    ByVal Pres As Presentation) Handles Application.PresentationCloseFinal
    MessageBox.Show(&quot;PresentationCloseFinal&quot;, &quot;Event&quot;)
  End Sub

  Private Sub Application_ProtectedViewWindowActivate(
    ByVal ProtViewWindow As ProtectedViewWindow) Handles Application.ProtectedViewWindowActivate
    MessageBox.Show(&quot;ProtectedViewWindowActivate: &quot; &amp; ProtViewWindow.Caption, &quot;Event&quot;)
  End Sub

  Private Sub Application_ProtectedViewWindowBeforeClose(
    ByVal ProtViewWindow As ProtectedViewWindow,
    ByVal ProtectedViewCloseReason As PpProtectedViewCloseReason,
    ByRef Cancel As Boolean) Handles Application.ProtectedViewWindowBeforeClose

    Dim reason As String = &quot;none&quot;

    Select Case ProtectedViewCloseReason
      Case PowerPoint.PpProtectedViewCloseReason.ppProtectedViewCloseEdit
        reason = &quot;Edit&quot;
      Case PowerPoint.PpProtectedViewCloseReason.ppProtectedViewCloseForced
        reason = &quot;Forced&quot;
      Case PowerPoint.PpProtectedViewCloseReason.ppProtectedViewCloseNormal
        reason = &quot;Normal&quot;
    End Select
    Cancel = (MessageBox.Show(String.Format(&quot;ProtectedViewWindowBeforeClose: {0} ({1})&quot;,
      ProtViewWindow.Caption, reason), &quot;Event&quot;, MessageBoxButtons.OKCancel) = DialogResult.Cancel)
  End Sub

  Private Sub Application_ProtectedViewWindowBeforeEdit(
    ByVal ProtViewWindow As ProtectedViewWindow,
    ByRef Cancel As Boolean) Handles Application.ProtectedViewWindowBeforeEdit

    Cancel = (MessageBox.Show(&quot;ProtectedViewWindowBeforeEdit&quot; &amp; ProtViewWindow.Caption,
                              &quot;Event&quot;, MessageBoxButtons.OKCancel) = DialogResult.Cancel)
  End Sub

  Private Sub Application_ProtectedViewWindowDeactivate(
    ByVal ProtViewWindow As ProtectedViewWindow) Handles Application.ProtectedViewWindowDeactivate
    MessageBox.Show(&quot;ProtectedViewWindowDeactivate&quot; &amp; ProtViewWindow.Caption, &quot;Event&quot;)
  End Sub

  Private Sub Application_ProtectedViewWindowOpen(
    ByVal ProtViewWindow As ProtectedViewWindow) Handles Application.ProtectedViewWindowOpen
    MessageBox.Show(&quot;ProtectedViewWindowOpen&quot; &amp; ProtViewWindow.Caption, &quot;Event&quot;)
  End Sub

// C#:
        void Application_ProtectedViewWindowOpen(PowerPoint.ProtectedViewWindow ProtViewWindow)
        {
          MessageBox.Show(&quot;ProtectedViewWindowOpen: &quot; &#43; ProtViewWindow.Caption, &quot;Event&quot;);
        }

        void Application_ProtectedViewWindowDeactivate(PowerPoint.ProtectedViewWindow ProtViewWindow)
        {
          MessageBox.Show(&quot;ProtectedViewWindowDeactivate: &quot; &#43; ProtViewWindow.Caption, &quot;Event&quot;);
        }

        void Application_ProtectedViewWindowBeforeEdit(PowerPoint.ProtectedViewWindow ProtViewWindow, ref bool Cancel)
        {
          Cancel = (MessageBox.Show(&quot;ProtectedViewWindowBeforeEdit: &quot; &#43; ProtViewWindow.Caption,
                                    &quot;Event&quot;, MessageBoxButtons.OKCancel) == DialogResult.Cancel);
        }

        void Application_ProtectedViewWindowBeforeClose(PowerPoint.ProtectedViewWindow ProtViewWindow, PowerPoint.PpProtectedViewCloseReason ProtectedViewCloseReason, ref bool Cancel)
        {
          string reason = &quot;none&quot;;

          switch(ProtectedViewCloseReason)
          {
            case PpProtectedViewCloseReason.ppProtectedViewCloseEdit:
              reason = &quot;Edit&quot;;
              break;
            case PpProtectedViewCloseReason.ppProtectedViewCloseForced:
              reason = &quot;Forced&quot;;
              break;
            case PpProtectedViewCloseReason.ppProtectedViewCloseNormal:
              reason = &quot;Normal&quot;;
              break;
          }
          Cancel = (MessageBox.Show(String.Format(&quot;ProtectedViewWindowBeforeClose: {0} ({1})&quot;,
            ProtViewWindow.Caption, reason), &quot;Event&quot;, MessageBoxButtons.OKCancel) == DialogResult.Cancel);
        }

        void Application_ProtectedViewWindowActivate(PowerPoint.ProtectedViewWindow ProtViewWindow)
        {
          MessageBox.Show(&quot;ProtectedViewWindowActivate: &quot; &#43; ProtViewWindow.Caption, &quot;Event&quot;);
        }

        void Application_PresentationCloseFinal(PowerPoint.Presentation Pres)
        {
          MessageBox.Show(&quot;PresentationCloseFinal&quot;, &quot;Event&quot;);
        }

        void Application_PresentationBeforeClose(PowerPoint.Presentation Pres, ref bool Cancel)
        {
          Cancel = (MessageBox.Show(&quot;PresentationBeforeClose&quot;, &quot;Event&quot;,
                                    MessageBoxButtons.OKCancel) == DialogResult.Cancel);
        }

5. Run the sample add-in, which starts PowerPoint for you.
6. In PowerPoint, select File and then Open. In the Open dialog box, locate an existing PowerPoint presentation, but don't open it yet. Click the arrow next to the Open button, and select Open in Protected View from the menu (this opens the presentation in Protected View--most of the new events deal with Protected View).
7. With the presentation open in Protected View, note the ProtectedViewWindowOpen and then ProtectedViewWindowActivate events that occur.
8. Click the Enable Editing button above the design window, and note that you can cancel the ProtectedViewWindowBeforeEdit event. Click the Cancel button, verify that you don't go into edit mode, and try again, this time clicking OK. Note the ProtectedViewWindowBeforeClose and ProtectedViewWindowDeactivate events.
9. From the menu, select File and then Close. Note the cancellable PresentationBeforeClose event, and then the PresentationCloseFinal events.
10. Quit PowerPoint to return to Visual Studio 2010.</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__number">1</span>.&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;Visual&nbsp;Studio&nbsp;<span class="visualBasic__number">2010</span>,&nbsp;create&nbsp;a&nbsp;new&nbsp;PowerPoint&nbsp;<span class="visualBasic__number">2010</span>&nbsp;add-in&nbsp;project&nbsp;(use&nbsp;the&nbsp;video&nbsp;here&nbsp;to&nbsp;help&nbsp;you&nbsp;get&nbsp;started:&nbsp;http://msdn.microsoft.com/en-us/vsto/cc338006).&nbsp;
&nbsp;
<span class="visualBasic__number">2</span>.&nbsp;(C#&nbsp;only)&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;the&nbsp;ThisAddIn.cs&nbsp;class,&nbsp;in&nbsp;the&nbsp;ThisAddIn_Startup&nbsp;method,&nbsp;add&nbsp;the&nbsp;following&nbsp;code:&nbsp;
&nbsp;
//&nbsp;C#:&nbsp;
Application.PresentationBeforeClose&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_PresentationBeforeCloseEventHandler(Application_PresentationBeforeClose);&nbsp;
Application.PresentationCloseFinal&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_PresentationCloseFinalEventHandler(Application_PresentationCloseFinal);&nbsp;
Application.ProtectedViewWindowActivate&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_ProtectedViewWindowActivateEventHandler(Application_ProtectedViewWindowActivate);&nbsp;
Application.ProtectedViewWindowBeforeClose&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_ProtectedViewWindowBeforeCloseEventHandler(Application_ProtectedViewWindowBeforeClose);&nbsp;
Application.ProtectedViewWindowBeforeEdit&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_ProtectedViewWindowBeforeEditEventHandler(Application_ProtectedViewWindowBeforeEdit);&nbsp;
Application.ProtectedViewWindowDeactivate&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_ProtectedViewWindowDeactivateEventHandler(Application_ProtectedViewWindowDeactivate);&nbsp;
Application.ProtectedViewWindowOpen&nbsp;&#43;=&nbsp;new&nbsp;PowerPoint.EApplication_ProtectedViewWindowOpenEventHandler(Application_ProtectedViewWindowOpen);&nbsp;
&nbsp;
<span class="visualBasic__number">3</span>.&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;the&nbsp;ThisAddIn.vb&nbsp;or&nbsp;ThisAddIn.cs&nbsp;class,&nbsp;add&nbsp;the&nbsp;following&nbsp;statements&nbsp;at&nbsp;the&nbsp;top&nbsp;of&nbsp;the&nbsp;file:&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;Visual&nbsp;Basic:</span>&nbsp;
<span class="visualBasic__keyword">Imports</span>&nbsp;System.Windows.Forms&nbsp;
<span class="visualBasic__keyword">Imports</span>&nbsp;Microsoft.Office.Interop.PowerPoint&nbsp;
&nbsp;
//&nbsp;C#:&nbsp;
using&nbsp;System.Windows.Forms;&nbsp;
using&nbsp;Microsoft.Office.Interop.PowerPoint;&nbsp;
&nbsp;
<span class="visualBasic__number">4</span>.&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;the&nbsp;ThisAddIn.vb&nbsp;or&nbsp;ThisAddIn.cs&nbsp;class,&nbsp;add&nbsp;the&nbsp;following&nbsp;code,&nbsp;which&nbsp;traps&nbsp;and&nbsp;displays&nbsp;information&nbsp;about&nbsp;each&nbsp;of&nbsp;the&nbsp;new&nbsp;Application-level&nbsp;events:&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;Visual&nbsp;Basic:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_PresentationBeforeClose(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;Pres&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Presentation,&nbsp;<span class="visualBasic__keyword">ByRef</span>&nbsp;Cancel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>)&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.PresentationBeforeClose&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MessageBox.Show(<span class="visualBasic__string">&quot;PresentationBeforeClose&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBoxButtons.OKCancel)&nbsp;=&nbsp;DialogResult.Cancel)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_PresentationCloseFinal(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;Pres&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Presentation)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.PresentationCloseFinal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;PresentationCloseFinal&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_ProtectedViewWindowActivate(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;ProtViewWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.ProtectedViewWindowActivate&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowActivate:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;ProtViewWindow.Caption,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_ProtectedViewWindowBeforeClose(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;ProtViewWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;ProtectedViewCloseReason&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;PpProtectedViewCloseReason,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByRef</span>&nbsp;Cancel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.ProtectedViewWindowBeforeClose&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;reason&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;none&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;ProtectedViewCloseReason&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PowerPoint.PpProtectedViewCloseReason.ppProtectedViewCloseEdit&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Edit&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PowerPoint.PpProtectedViewCloseReason.ppProtectedViewCloseForced&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Forced&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PowerPoint.PpProtectedViewCloseReason.ppProtectedViewCloseNormal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Normal&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MessageBox.Show(<span class="visualBasic__keyword">String</span>.Format(<span class="visualBasic__string">&quot;ProtectedViewWindowBeforeClose:&nbsp;{0}&nbsp;({1})&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProtViewWindow.Caption,&nbsp;reason),&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>,&nbsp;MessageBoxButtons.OKCancel)&nbsp;=&nbsp;DialogResult.Cancel)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_ProtectedViewWindowBeforeEdit(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;ProtViewWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByRef</span>&nbsp;Cancel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.ProtectedViewWindowBeforeEdit&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowBeforeEdit&quot;</span>&nbsp;&amp;&nbsp;ProtViewWindow.Caption,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>,&nbsp;MessageBoxButtons.OKCancel)&nbsp;=&nbsp;DialogResult.Cancel)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_ProtectedViewWindowDeactivate(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;ProtViewWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.ProtectedViewWindowDeactivate&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowDeactivate&quot;</span>&nbsp;&amp;&nbsp;ProtViewWindow.Caption,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Application_ProtectedViewWindowOpen(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;ProtViewWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;Application.ProtectedViewWindowOpen&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowOpen&quot;</span>&nbsp;&amp;&nbsp;ProtViewWindow.Caption,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
//&nbsp;C#:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_ProtectedViewWindowOpen(PowerPoint.ProtectedViewWindow&nbsp;ProtViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowOpen:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ProtViewWindow.Caption,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_ProtectedViewWindowDeactivate(PowerPoint.ProtectedViewWindow&nbsp;ProtViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowDeactivate:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ProtViewWindow.Caption,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_ProtectedViewWindowBeforeEdit(PowerPoint.ProtectedViewWindow&nbsp;ProtViewWindow,&nbsp;ref&nbsp;bool&nbsp;Cancel)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowBeforeEdit:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ProtViewWindow.Caption,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>,&nbsp;MessageBoxButtons.OKCancel)&nbsp;==&nbsp;DialogResult.Cancel);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_ProtectedViewWindowBeforeClose(PowerPoint.ProtectedViewWindow&nbsp;ProtViewWindow,&nbsp;PowerPoint.PpProtectedViewCloseReason&nbsp;ProtectedViewCloseReason,&nbsp;ref&nbsp;bool&nbsp;Cancel)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;none&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;switch(ProtectedViewCloseReason)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;case&nbsp;PpProtectedViewCloseReason.ppProtectedViewCloseEdit:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Edit&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;case&nbsp;PpProtectedViewCloseReason.ppProtectedViewCloseForced:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Forced&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;case&nbsp;PpProtectedViewCloseReason.ppProtectedViewCloseNormal:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Normal&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MessageBox.Show(<span class="visualBasic__keyword">String</span>.Format(<span class="visualBasic__string">&quot;ProtectedViewWindowBeforeClose:&nbsp;{0}&nbsp;({1})&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProtViewWindow.Caption,&nbsp;reason),&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>,&nbsp;MessageBoxButtons.OKCancel)&nbsp;==&nbsp;DialogResult.Cancel);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_ProtectedViewWindowActivate(PowerPoint.ProtectedViewWindow&nbsp;ProtViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;ProtectedViewWindowActivate:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;ProtViewWindow.Caption,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_PresentationCloseFinal(PowerPoint.Presentation&nbsp;Pres)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(<span class="visualBasic__string">&quot;PresentationCloseFinal&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;Application_PresentationBeforeClose(PowerPoint.Presentation&nbsp;Pres,&nbsp;ref&nbsp;bool&nbsp;Cancel)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MessageBox.Show(<span class="visualBasic__string">&quot;PresentationBeforeClose&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBoxButtons.OKCancel)&nbsp;==&nbsp;DialogResult.Cancel);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
<span class="visualBasic__number">5</span>.&nbsp;Run&nbsp;the&nbsp;sample&nbsp;add-in,&nbsp;which&nbsp;starts&nbsp;PowerPoint&nbsp;for&nbsp;you.&nbsp;
<span class="visualBasic__number">6</span>.&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;PowerPoint,&nbsp;select&nbsp;File&nbsp;and&nbsp;then&nbsp;Open.&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;the&nbsp;Open&nbsp;dialog&nbsp;box,&nbsp;locate&nbsp;an&nbsp;existing&nbsp;PowerPoint&nbsp;presentation,&nbsp;but&nbsp;don<span class="visualBasic__com">'t&nbsp;open&nbsp;it&nbsp;yet.&nbsp;Click&nbsp;the&nbsp;arrow&nbsp;next&nbsp;to&nbsp;the&nbsp;Open&nbsp;button,&nbsp;and&nbsp;select&nbsp;Open&nbsp;in&nbsp;Protected&nbsp;View&nbsp;from&nbsp;the&nbsp;menu&nbsp;(this&nbsp;opens&nbsp;the&nbsp;presentation&nbsp;in&nbsp;Protected&nbsp;View--most&nbsp;of&nbsp;the&nbsp;new&nbsp;events&nbsp;deal&nbsp;with&nbsp;Protected&nbsp;View).</span>&nbsp;
<span class="visualBasic__number">7</span>.&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;the&nbsp;presentation&nbsp;open&nbsp;in&nbsp;<span class="visualBasic__keyword">Protected</span>&nbsp;View,&nbsp;note&nbsp;the&nbsp;ProtectedViewWindowOpen&nbsp;and&nbsp;then&nbsp;ProtectedViewWindowActivate&nbsp;events&nbsp;that&nbsp;occur.&nbsp;
<span class="visualBasic__number">8</span>.&nbsp;Click&nbsp;the&nbsp;Enable&nbsp;Editing&nbsp;button&nbsp;above&nbsp;the&nbsp;design&nbsp;window,&nbsp;and&nbsp;note&nbsp;that&nbsp;you&nbsp;can&nbsp;cancel&nbsp;the&nbsp;ProtectedViewWindowBeforeEdit&nbsp;event.&nbsp;Click&nbsp;the&nbsp;Cancel&nbsp;button,&nbsp;verify&nbsp;that&nbsp;you&nbsp;don<span class="visualBasic__com">'t&nbsp;go&nbsp;into&nbsp;edit&nbsp;mode,&nbsp;and&nbsp;try&nbsp;again,&nbsp;this&nbsp;time&nbsp;clicking&nbsp;OK.&nbsp;Note&nbsp;the&nbsp;ProtectedViewWindowBeforeClose&nbsp;and&nbsp;ProtectedViewWindowDeactivate&nbsp;events.</span>&nbsp;
<span class="visualBasic__number">9</span>.&nbsp;From&nbsp;the&nbsp;menu,&nbsp;select&nbsp;File&nbsp;and&nbsp;then&nbsp;Close.&nbsp;Note&nbsp;the&nbsp;cancellable&nbsp;PresentationBeforeClose&nbsp;event,&nbsp;and&nbsp;then&nbsp;the&nbsp;PresentationCloseFinal&nbsp;events.&nbsp;
<span class="visualBasic__number">10</span>.&nbsp;Quit&nbsp;PowerPoint&nbsp;to&nbsp;return&nbsp;to&nbsp;Visual&nbsp;Studio&nbsp;<span class="visualBasic__number">2010</span>.</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26158" href="/site/view/file/26158/1/PPT.NewEvents.txt">PPT.NewEvents.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26159" href="/site/view/file/26159/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
