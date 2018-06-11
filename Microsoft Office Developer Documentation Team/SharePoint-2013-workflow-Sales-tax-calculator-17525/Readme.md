# SharePoint 2013 workflow: Sales tax calculator
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* Web Services
* Workflows
## IsPublished
* True
## ModifiedDate
* 2013-03-08 04:26:06
## Description

<p><span style="font-size:small">The sales tax workflow starts automatically when a new item is added to the Product Catalog list in SharePoint. The workflow reads the ZIP Code for locations in which the product will be sold. It also reads the base price. The
 workflow then looks up sales tax rates (based on ZIP Code), calculates the total price, including sales tax, and then updates the
<strong>total price </strong>field in the SharePoint list.</span></p>
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
<span style="font-size:small">After creating up your Visual Studio workflow project, you must set the
<strong>Site URL </strong>property to the server on which the workflow will run.</span></p>
<p><span style="font-size:small">To create the app and workflow</span></p>
<ol>
<li><span style="font-size:small">Using Visual Studio 2012, create an App for SharePoint 2013 project and name it
<strong>SalesTax</strong>.</span> </li><li>
<p><span style="font-size:small">In the SalesTax app, add a list and name it <strong>
ProductCatalog</strong>; for list type, choose <strong>Default (Blank)</strong>.</span></p>
<p><span style="font-size:small">a)&nbsp;Set the list column1 to <strong>name=&quot;Title&quot;; type=&rdquo;single line of text&rdquo;; required=&rdquo;true&rdquo;</strong>.</span></p>
<p><span style="font-size:small">b)&nbsp;Set the list column2 to <strong>name=&quot;BasePrice&quot;; type=&rdquo;single line of text&rdquo;; required=&rdquo;true&rdquo;</strong>.</span></p>
<p><span style="font-size:small">c)&nbsp;Set the list column3 to <strong>name=&quot;Location1&quot;; type=&rdquo;single line of text&rdquo;; required=&rdquo;true&rdquo;</strong>.</span></p>
<p><span style="font-size:small">d)&nbsp;Set the list column4 to <strong>name=&quot;Location1Price&quot;; type=&rdquo;number&rdquo;</strong>.</span></p>
<p><span style="font-size:small">e)&nbsp;Set the list column5 to <strong>name=&quot;Location2&quot;; type=&rdquo;single line of text&rdquo;; required=&rdquo;true&rdquo;</strong>.</span></p>
<p><span style="font-size:small">f)&nbsp;Set the list column6 to <strong>name=&quot;Location2Price&quot;; type=&rdquo;number&rdquo;</strong>.</span></p>
<p><span style="font-size:small">g)&nbsp;Set the list column7 to <strong>name=&quot;Location3&quot;; type=&rdquo;single line of text&rdquo;; required=&rdquo;true&rdquo;</strong>.</span></p>
<p><span style="font-size:small">h)&nbsp;Set the list column8 to <strong>name=&quot;Location3Price&quot;; type=&rdquo;number&rdquo;</strong>.</span></p>
</li><li>
<p><span style="font-size:small">Add a workflow to the app project, and complete the workflow creation wizard:</span></p>
<p><span style="font-size:small">a)&nbsp;Name the workflow <strong>SalesTaxWorkflow</strong>, and for workflow type, choose List workflow.</span></p>
<p><span style="font-size:small">b)&nbsp;Ensure that the <strong>Associate the workflow
</strong>check box is selected.</span></p>
<p><span style="font-size:small">c)&nbsp;In the drop-down list of workflows to associate with the list, choose
<strong>ProductCatalog</strong>.</span></p>
<p><span style="font-size:small">d)&nbsp;In the second drop-down list, choose <strong>
Workflow History </strong>as the history list (if any).</span></p>
<p><span style="font-size:small">e)&nbsp;In the third drop-down list, choose <strong>
Workflow Tasks </strong>as the task list (if any).</span></p>
<p><span style="font-size:small">f)&nbsp;For workflow start, choose <strong>A user manually starts the workflow</strong>.</span></p>
<p><span style="font-size:small">g)&nbsp;When you finish the workflow creation wizard, the workflow SPI is created, and the workflow.xaml file is opened in the Workflow Designer.</span></p>
</li><li>
<p><span style="font-size:small">In the Workflow Designer, create the following variables for the Sequence scope:</span></p>
<p><span style="font-size:small">a)&nbsp;<em>TitleResult</em> (String)</span></p>
<p><span style="font-size:small">b)&nbsp;<em>BasePriceResult</em> (Double)</span></p>
<p><span style="font-size:small">c)&nbsp;<em>SalesTaxResult</em> (String)</span></p>
<p><span style="font-size:small">d)&nbsp;<em>LocationResult</em> (String)</span></p>
<p><span style="font-size:small">e)&nbsp;<em>UriForSalesTaxService</em> (String)</span></p>
<p><span style="font-size:small">f)&nbsp;<em>ParseNumberResult</em> (Double)</span></p>
<p><span style="font-size:small">g)&nbsp;<em>Number</em> (Int32) (default value is 3</span></p>
</li><li><span style="font-size:small">Add the <strong>LookupCurrentSPListItemStringProperty</strong> activity; set its
<strong>PropertyName</strong> to <strong>Title</strong>, and set its <strong>Result</strong> field to
<strong>TitleResult</strong>.</span> </li><li><span style="font-size:small">Add the <strong>LookupCurrentSPListItemDoubleProperty</strong> activity; set its
<strong>PropertyName</strong> to <strong>BasePrice</strong> and set its <strong>Result</strong> field to
<strong>BasePriceResult</strong>.</span> </li><li>
<p><span style="font-size:small">Add the <strong>While</strong> activity, and set it to loop through the output from the
<strong>HttpGet</strong> activity, as follows:</span></p>
<p><span style="font-size:small">a)&nbsp;Set a condition such that <em>number</em> &gt; 0.</span></p>
<p><span style="font-size:small">b)&nbsp;In the body, do the following:</span></p>
<blockquote dir="ltr" style="margin-right:0px">
<p><span style="font-size:small">i.&nbsp;Add a Sequence activity.</span></p>
<p><span style="font-size:small">ii.&nbsp;Add a <strong>LookupCurrentSPListItemStringProperty</strong> activity.</span></p>
<p><span style="font-size:small">iii.&nbsp;Set <strong>PropertyName</strong> to &ldquo;Location&rdquo; &#43; Number.Tostring()</span></p>
<p><span style="font-size:small">iv.&nbsp;Set <strong>Result</strong> to <em>LocationResult</em>.</span></p>
<p><span style="font-size:small">v.&nbsp;Add an <strong>Assign</strong> activity.</span></p>
<p><span style="font-size:small">vi.&nbsp;Set <strong>To</strong> to <em>UrlForSalesTaxService</em>.</span></p>
<p><span style="font-size:small">vii.&nbsp;Set <strong>Value</strong> to &quot;http://localhost:15604/salestax/&quot; &#43; LocationResult.</span></p>
<p><span style="font-size:small">viii.&nbsp;Add an <strong>HttpGet</strong> activity.</span></p>
<p><span style="font-size:small">ix.&nbsp;Set <strong>ResponseContent</strong> to
<em>SalesTaxResult</em>.</span></p>
<p><span style="font-size:small">x.&nbsp;Set <strong>Uri</strong> to <em>UriForSalesTaxService</em>.</span></p>
<p><span style="font-size:small">xi.&nbsp;Add a <strong>ParseNumber&lt;Double&gt;
</strong>activity.</span></p>
<p><span style="font-size:small">xii.&nbsp;Set <strong>Result</strong> to <em>ParseNumberResult</em>.</span></p>
<p><span style="font-size:small">xiii.&nbsp;Set <strong>Value</strong> to <em>SalesTaxResult</em>.</span></p>
<p><span style="font-size:small">xiv.&nbsp;Add a <strong>SetField</strong> activity.</span></p>
<p><span style="font-size:small">xv.&nbsp;Set <strong>FieldName</strong> to &ldquo;Location&rdquo; &#43; Number.Tostring() &#43; &ldquo;Price&rdquo;.</span></p>
<p><span style="font-size:small">xvi.&nbsp;Set <strong>FieldValue</strong> to FieldValue &ndash; BasePriceResult * (1 &#43; ParseNumberResult).</span></p>
<p><span style="font-size:small">xvii.&nbsp;Add an <strong>Assign</strong> activity.</span></p>
<p><span style="font-size:small">xviii.&nbsp;Set <strong>To</strong> to <em>Number</em>.</span></p>
<p><span style="font-size:small">xix.&nbsp;Set <strong>Value</strong> to <em>Number</em> - 1.</span></p>
</blockquote>
</li><li><span style="font-size:small">In the designer, open the <strong>Pages</strong> node, and then open Home.aspx. In the
<strong>PlaceHolderMain</strong> part, add the following HTML: &lt;a href=&quot;../lists/ProductCatalog&quot;&gt;Product Catalog&lt;/a&gt;.</span>
</li><li><span style="font-size:small">Open the file workflow.xaml, and set breakpoints at the
<strong>HttpGet</strong> and <strong>SetField</strong> activities.</span> </li><li><span style="font-size:small">Run the web service project named SalesTaxCalcService to retrieve tax information based on location. This project is located in the sample project folder.</span>
</li></ol>
<h1>Debug and test the sample</h1>
<p><span style="font-size:small">To debug and test the workflow app sample, do the following:</span></p>
<ol>
<li><span style="font-size:small">Start the Test Service Host console.</span><br>
<span style="font-size:small">The Home.aspx page should start to load.</span> </li><li><span style="font-size:small">Click <strong>Product Catalog </strong>to navigate to the list page.</span>
</li><li><span style="font-size:small">Click <strong>Add a new item </strong>to add a list item.</span><br>
<span style="font-size:small">c)&nbsp;Fill in the fields <strong>Title</strong>, <strong>
BasePrice</strong>, <strong>Location1</strong>, <strong>Location2</strong>, and <strong>
Location3</strong>.</span><br>
<span style="font-size:small">d)&nbsp;Click <strong>Save</strong>.</span> </li><li><span style="font-size:small">Right-click the list item, and choose <strong>Workflow</strong>.</span>
</li><li><span style="font-size:small">Choose the <strong>SalesTaxWorkflow &ndash; WorkflowStart
</strong>association to run the workflow. Both breakpoints that you set should halt execution at the appropriate points. Press
<strong>F10/F5 </strong>to continue.</span> </li><li><span style="font-size:small">Observe that the tests from <strong>WriteLine</strong> activities are displayed in the Test Service Host console.</span>
</li><li><span style="font-size:small">Refresh the SharePoint <strong>ProductCatalog</strong> webpage, and confirm that the values in the
<strong>Location1Price</strong>, <strong>Location2Price</strong>, <strong>Location3Price</strong> have been updated.</span>
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
