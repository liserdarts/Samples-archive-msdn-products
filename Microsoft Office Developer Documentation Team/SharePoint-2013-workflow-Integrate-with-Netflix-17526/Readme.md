# SharePoint 2013 workflow: Integrate with Netflix
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* Web Services
* Workflows
## IsPublished
* False
## ModifiedDate
* 2013-03-08 04:24:26
## Description

<p><span style="font-size:small">This sample addresses a scenario in which a user creates a workflow-driven app for SharePoint 2013 that contains lists of movie titles from Netflix. Each time the user adds a new movie title, the workflow populates a list field,
 &quot;TopMovies&quot; with the top movies obtained using the Netflix OData query.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">SharePoint 2013</span> </li><li><span style="font-size:small"><span style="font-size:small">&#65279;Workflow Manager Client 1.0</span></span>
</li><li><span style="font-size:small">Visual Studio 2012, either the Ultimate or Professional version</span>
</li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li></ul>
<h1>Create and build the sample</h1>
<p><span style="font-size:small">The following steps and procedures enable you to create, build, and run the sample. The sample solution file is provided for reference.</span><br>
<span style="font-size:small"><br>
<strong>Important</strong></span><br>
<span style="font-size:small">After creating up your Visual Studio workflow project, you must set the Site URL property to the server on which the workflow will run.</span></p>
<h2>To create the app and workflow</h2>
<ol>
<li><span style="font-size:small">Using Visual Studio 2012, create an App for SharePoint 2013 project, and name it
<strong>Netflix</strong>.</span> </li><li>
<p><span style="font-size:small">In the Netflix app, add a list and name it <strong>
MovieList</strong>. For list type, choose <strong>Default (Blank)</strong>.</span></p>
<p><span style="font-size:small">a)&nbsp;Set the list column1 to <strong>name=&quot;Title&quot;; type=&rdquo;single line of text&rdquo;; required=&rdquo;true&rdquo;</strong>.</span></p>
<p><span style="font-size:small">b)&nbsp;Set the list column2 to <strong>name=&quot;TopMovies&quot;; type=&rdquo;single line of text&rdquo;</strong>.</span></p>
</li><li>
<p><span style="font-size:small">Add a workflow to the app project, and complete the workflow creation wizard:</span></p>
<p><span style="font-size:small">a)&nbsp;Name the workflow <strong>NetflixWorkflow</strong>, and for workflow type, choose List workflow.</span></p>
<p><span style="font-size:small">b)&nbsp;Ensure that the <strong>Associate the workflow
</strong>check box is selected.</span></p>
<p><span style="font-size:small">c)&nbsp;In the drop-down list of workflows to associate with the list, choose
<strong>MovieList</strong>.</span></p>
<p><span style="font-size:small">d)&nbsp;In the second drop-down list, choose <strong>
Workflow History </strong>as the history list (if any).</span></p>
<p><span style="font-size:small">e)&nbsp;In the third drop-down list, choose <strong>
Workflow Tasks </strong>as the task list (if any).</span></p>
<p><span style="font-size:small">f)&nbsp;For workflow start, choose <strong>A user manually starts the workflow</strong>.</span></p>
<p><span style="font-size:small">g)&nbsp;When you finish the workflow creation wizard, the workflow SPI is created and the workflow.xaml file is opened in the Workflow Designer.</span></p>
</li><li>
<p><span style="font-size:small">In the Workflow Designer, create the following variables for Sequence scope:</span></p>
<p><span style="font-size:small">a)&nbsp;<em>topItems</em> (Int32)</span></p>
<p><span style="font-size:small">b)&nbsp;<em>videoStr</em> (String)</span></p>
<p><span style="font-size:small">c)&nbsp;<em>searchKeyword</em> (String)</span></p>
<p><span style="font-size:small">d)&nbsp;<em>title</em> (String)</span></p>
<p><span style="font-size:small">e)&nbsp;<em>videoDV</em> (DynamicValue)</span></p>
</li><li><span style="font-size:small">Add the <strong>LookupCurrentSPListItemStringProperty</strong> activity; set its
<strong>PropertyName</strong> to &quot;Title&quot; and its <strong>Result</strong> field to &quot;searchKeyboard&quot;.</span>
</li><li><span style="font-size:small">Add a <strong>WriteLIne</strong> activity; set the
<strong>Text</strong> value to &quot;Keyword is:&quot; &#43; <em>searchKeyword</em>.</span> </li><li><span style="font-size:small">Add the <strong>Assign</strong> activity; set <strong>
To</strong> as <em>topItems</em> and <strong>Value</strong> to 3.</span> </li><li><span style="font-size:small">Add the <strong>HttpGet</strong> activity; set <strong>
ResponseContent</strong> to <em>videoDV</em>, and set <strong>Uri</strong> to &quot;http://odata.netflix.com/Catalog/Titles?$filter=substringof('&quot; &amp; searchKeyword &amp; &quot;', Name)&amp;$format=json&amp;$top=&quot; &amp; topItems &amp; &quot;&amp;$select=Name&quot;</span>
</li><li>
<p><span style="font-size:small">Add the <strong>While</strong> activity and set it to loop through the output from
<strong>HttpGet</strong> activity, as follows:</span></p>
<p><span style="font-size:small">a)&nbsp;Create a variable for the <strong>while</strong> scope; name it
<em>item</em> (Int32) and set default to 0.</span></p>
<p><span style="font-size:small">b)&nbsp;Set a condition such that <em>item &lt; topItems</em>.</span></p>
<p><span style="font-size:small">c)&nbsp;In the body, use <strong>GetDynamicProperty</strong> activity to extract the
<em>Title</em> property from <em>videoDV</em>, and then use <em>videoStr</em> to hold the concatenated extracted movie titles.</span></p>
<blockquote dir="ltr" style="margin-right:0px">
<p><span style="font-size:small">i.&nbsp;Add a <strong>Sequence</strong> activity.</span></p>
<p><span style="font-size:small">ii.&nbsp;Add a <strong>GetDynamicValueProperty&lt;String&gt;
</strong>activity.</span></p>
<p><span style="font-size:small">iii.&nbsp;Set <strong>PropertyName</strong> to &quot;d(&quot; &amp; item.ToString() &amp; &quot;)/Name&quot;.</span></p>
<p><span style="font-size:small">iv.&nbsp;Set <strong>Result</strong> to <em>title</em>.</span></p>
<p><span style="font-size:small">v.&nbsp;Set <strong>Source</strong> to <em>videoDV</em>.</span></p>
<p><span style="font-size:small">vi.&nbsp;Add an <strong>Assign</strong> activity; set
<strong>To</strong> to <em>videoStr</em>; set <strong>Value</strong> to videoStr &amp; title &amp; &quot;,&quot;</span></p>
<p><span style="font-size:small">vii.&nbsp;Add an <strong>Assign</strong> activity; set
<strong>To</strong> to item; set <strong>Value</strong> to item &#43; 1.</span></p>
</blockquote>
<p><span style="font-size:small">d)&nbsp;Add a <strong>SetField</strong> activity; set
<strong>FieldName</strong> to <em>TopMovies</em>; set <strong>FieldValue</strong> to
<em>videoStr</em>.</span></p>
<p><span style="font-size:small">e)&nbsp;Add a <strong>WriteLine</strong> activity; set
<strong>Text</strong> to &quot;After:::&quot; &#43; videoStr.</span></p>
</li><li><span style="font-size:small">In the designer, open the <strong>Pages</strong> node, and then open Home.aspx. In the PlaceHolderMain part, add the following HTML: &lt;a href=&quot;../lists/MovieList&quot;&gt; MovieList &lt;/a&gt;</span>
</li><li><span style="font-size:small">Open the file workflow.xaml and set breakpoints at the
<strong>HttpGet</strong> and <strong>SetField</strong> activities.</span> </li></ol>
<h1>Debug the sample</h1>
<p><span style="font-size:small">To debug and test the workflow sample, do the following:</span></p>
<ol>
<li><span style="font-size:small">Start the Test Service Host console.</span><br>
<span style="font-size:small">The Home.aspx page should start to load.</span> </li><li><span style="font-size:small">Click <strong>MovieList</strong> to navigate to the list page.</span>
</li><li><span style="font-size:small">Click <strong>Add a new item </strong>to add a list item. Enter
<strong>Happiness</strong> in the title box, and then click <strong>Save</strong>.</span>
</li><li><span style="font-size:small">Right-click the list item and choose <strong>Workflow</strong>.</span>
</li><li><span style="font-size:small">Choose the <strong>NetflixWorkflow &ndash; WorkflowStart
</strong>association to run the workflow. Both of the breakpoints that you set should halt execution at the appropriate points. Press
<strong>F10/F5 </strong>to continue.</span> </li><li><span style="font-size:small">Observe that the test from the <strong>WriteLine</strong> activities are displayed in Test Service Host console.</span>
</li><li><span style="font-size:small">Refresh the SharePoint <strong>MovieList</strong> webpage, and observe that the
<strong>TopMovies</strong> column has updated text reading &quot;Happiness, The Inn of the Sixth Happiness.&quot;</span>
</li><li><span style="font-size:small">Close the web browser to stop debugging, and close the Test Service Host process.</span>
</li><li><span style="font-size:small">Verify that the app gets retracted from the SharePoint server.</span>
</li></ol>
<h1>Change log</h1>
<p><span style="font-size:small">First release.&nbsp;July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<span style="font-size:small">
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163917.aspx" target="_blank">Get started with workflows in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ffaccd6b-426d-4ca0-b62f-bc7b14641a49" target="_blank">SharePoint 2013 workflow samples</a></span>
</span></li></ul>
