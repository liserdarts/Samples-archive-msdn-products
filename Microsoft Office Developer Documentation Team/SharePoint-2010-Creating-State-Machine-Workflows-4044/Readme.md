# SharePoint 2010: Creating State Machine Workflows in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* State Machine Workflow
## IsPublished
* True
## ModifiedDate
* 2011-08-04 12:44:55
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the State Machine Workflow template in Microsoft Visual Studio 2010 to build a Microsoft SharePoint 2010 state machine workflow. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg508985.aspx">Creating SharePoint 2010 State Machine Workflows in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Visual Studio 2010 provides a State Machine Workflow template that enables you to build workflow solutions for Microsoft SharePoint 2010 by using a graphical design surface. Unlike sequential workflows, which transition from activity to activity,
 state machine workflows transition from state to state.</p>
<p>This sample and the accompanying article demonstrate the following steps for creating and deploying a state machine workflow to a SharePoint 2010 site:</p>
<ol>
<li>Creating a prerequisite document library named <strong>Projects</strong>. </li><li>Adding state activities for <strong>stateInProgress</strong>, <strong>stateReview</strong>, and
<strong>stateFinished</strong>. </li><li>Configuring state initialization and events for each state. </li><li>Configuring code that determines state transitions. </li></ol>
<p>The State Machine Workflow project in Visual Studio 2010 provides a graphical design surface in which a workflow can be built.</p>
<ul>
<li>The <strong>Initial State</strong> has one activity of <strong>SetState</strong> that takes the workflow directly to the
<strong>stateInProgress</strong> state. </li><li>The <strong>stateInProgress</strong> state has a task activity that generates a new task titled &quot;Finish Document&quot; and assigns the task to a specified user.
</li><li>The <strong>stateInProgress</strong> state has an <strong>onTaskChanged</strong> activity that is invoked when the task changes. The
<strong>inTaskChanged</strong> activity has an <strong>IfElse</strong> statement that compares the &quot;percent complete&quot; of the task to 1.0 (100%).
</li><li>If the condition is true, the workflow transitions to the next state (<strong>stateReview</strong>). If it is false, the workflow remains at this state.
</li><li>In the <strong>stateReview</strong> state, a task is generated with a title of &quot;Review Document&quot; as part of the state initialization, and again this task is assigned to a specified user.
</li><li>The <strong>stateReview</strong> state is invoked when the task changes, and an
<strong>If.. Else</strong> statement compares the &quot;percent complete&quot; of this task with 100 percent. As before, when the task is 100 percent complete, the workflow continues.
</li><li>When the task is 100 percent complete, a nested <strong>If.. Else</strong> statement is reached within the
<strong>stateReview</strong> state. The code compares the text that appears in the task description to the string &quot;&lt;DIV&gt;Approved&lt;/DIV&gt;&quot;.
</li><li>If the string matches, the stage is considered complete and the workflow continues to
<strong>StateFinished</strong>. If the string does not match, the state returns to
<strong>stateInProgress</strong>. </li></ul>
