# SharePoint 2013: Small business invoicing manager app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-05-03 04:41:43
## Description

<div id="header">Demonstrates how to use JavaScript and jQuery in an app for SharePoint that manages customers, their purchase orders, and their invoices.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p><span class="label">Provided by: </span><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a>.</p>
<p>The solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client-side object model to read, create, update, and delete data from lists based on user actions. The lists
 included in this solution represent Customers, their Purchase Orders, and their Invoices. The lists are related to each other through lookup fields, and the user interface (UI) ensures that all data operations synchronize the list items to ensure the relationships
 are maintained.</p>
<p>The UI is implemented with simple HTML elements and Cascading Style Sheet (CSS) styles to present a modern look and feel.</p>
<p>JavaScript and jQuery are used to control all aspects of the UI, and the solution contains no server-side code.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either one of the following:</p>
<ul>
<li>
<p>Access to an Office 365 Developer Site configured to host apps (recommended)</p>
</li><li>
<p>SharePoint Server 2013 (RTM) configured to host apps, and with a Developer Site collection already created</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection1">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The Default.aspx webpage, which presents the customers, purchase orders, and invoices.</p>
</li><li>
<p>The App.js file in the scripts folder, which retrieves and manages customer, purchase order, and invoice data by using the JavaScript implementation of the client object model (JSOM). The App.js file also contains the UI logic that is implemented in Default.aspx.</p>
</li><li>
<p>The App.css file in the contents folder, which contains style definitions used by the elements in Default.aspx.</p>
</li><li>
<p>Three list definitions and instances&mdash;one for customers, one for purchase orders, and one for invoices. The lists are linked together by lookup fields, and the customer list includes some sample data to help you get started.</p>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection2">
<p>Follow these steps to configure the sample.</p>
<div class="subSection">
<ol>
<li>
<p>Open the SP_SmallBizInvoicing_js.sln file using Visual Studio 2012.</p>
</li><li>
<p>In the <span class="ui">Properties</span> window, add the full URL to your Office 365 Developer Site or SharePoint Server 2013 Developer Site collection to the
<span><span class="keyword">Site URL</span></span> property.</p>
<p>You may be prompted to provide credentials if you have added a URL to an Office 365 Developer Site, as shown in Figure 1. No other configuration is necessary.</p>
<div class="caption"><strong>Figure 1. Office 365 connection dialog box</strong></div>
<br>
<img src="/site/view/file/81635/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1 class="heading">Build, run, and test the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to build and run the sample:</p>
<div class="subSection">
<ol>
<li>
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</li><li>
<p>Once the sample is built, press F5 to run the app. Sign in to your SharePoint Server 2013 or Office 365 Developer Site if you are prompted to do so.</p>
</li><li>
<p>When the app opens, the start screen resembles Figure 2. When the customer clicks the
<span class="ui">Customers</span> tile, four sample customers are listed. The data for these four customers was deployed with the solution. Likewise, when you click the
<span class="ui">Purchase Orders</span> tile, instead of customer data you will see all existing purchase orders. Similarly, Invoices are listed when you click the
<span class="ui">Invoices</span> tile. (Note that no purchase orders are included initially; later in this walkthrough we'll create both.)</p>
<div class="caption"><strong>Figure 2. Start screen for the app</strong></div>
<br>
<img src="/site/view/file/81636/1/image.png" alt=""> </li><li>
<p>From the <span class="ui">Customer</span> tile, you can click the <span class="ui">
&#43;New Customer</span> link to bring up the <span class="ui">Add New Customer</span> form, as shown in
<span class="ui">Figure 3</span>, where you can enter data about the new customer.</p>
<p>After a new customer is entered, that customer appears in the list that appears when you click the
<span class="ui">Customers</span> tile (as in Figure 2, above). In addition, by clicking the customer record, you can open the
<span class="ui">Edit Customer</span> form (identical to Figure 3, but with customer data exposed for editing). The
<span class="ui">Edit Customer</span> form also lets you view that customer's existing purchase orders and invoices and add a new purchase order for that customer.</p>
<div class="caption"><strong>Figure 3. New Customer entry form</strong></div>
<br>
<img src="/site/view/file/81637/1/image.png" alt=""> </li><li>
<p>To add a new purchase order for a customer, click <span class="ui">Add New Purchase Order</span>, which opens the form shown in
<span class="ui">Figure 4</span> on which the <span class="ui">Customer</span> field is prepopulated with customer lookup data. Note that normally you would hide the initial part of the lookup data; it is included here to illustrate how the lists are linked.</p>
<p>Click <span class="ui">Save</span> to commit the purchase order data to the appropriate SharePoint list and save the link to the customer. The new purchase order is now shown as a link when you click the
<span class="ui">Purchase Orders</span> tile. You can then click that link to reopen the purchase order record in the
<span class="ui">Edit Purchase Order</span> form (identical to Figure 4 except that editable data is shown).</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>From the <span class="ui">Edit Purchase Order</span> form, you can upload a file and bind it as an attachment to the purchase order list item. You can also delete attachments, and you can view attachments by clicking their document link.</p>
</td>
</tr>
</tbody>
</table>
</div>
<div class="caption"><strong>Figure 4. Add New Purchase Order form</strong></div>
<br>
<img src="/site/view/file/81638/1/image.png" alt=""> </li><li>
<p>On the <span class="ui">Edit Purchase Order</span> form is a button labeled <span class="ui">
Raise Invoice for this PO</span>. Clicking this button opens the <span class="ui">
Add New Invoice</span> form, shown in <span class="ui">Figure 5</span>. Entering a new invoice record automatically associates the invoice with the purchase order from which you opened the form.</p>
<p>Note that the <span class="ui">Customer</span> field and the <span class="ui">
PO Number</span> field are prepopulated with the appropriate lookup data. Also keep in mind that in your own app, you would likely hide the initial parts of this lookup data. It has been added here to illustrate how the lists are linked.</p>
<p>When you click <span class="ui">Save</span>, the new invoice is added to the appropriate SharePoint list, associated with the proper purchase order, and then added to the list of invoices that appear on the invoices section of the app.</p>
<div class="caption"><strong>Figure 5. Add New Invoice form</strong></div>
<br>
<img src="/site/view/file/81639/1/image.png" alt="">
<p>Of course, you can also edit invoice data by opening the invoice record. The <span class="ui">
Edit Invoice</span> form is similar to the <span class="ui">Add New Invoice</span> form (Figure 5) except that it displays invoice data for the appropriate record.</p>
<p>In addition, as with purchase orders, you can upload and attach files to invoice list items in SharePoint. A link on the list item indicates the presence of an attachment. You can click the link to open the attachment, or you can delete the attachment.</p>
</li><li>
<p><span class="ui">Figure 6</span> displays the app's <span class="ui">Reports</span> form rendering prepopulated data as a bar chart and shows the following:</p>
<ul>
<li>
<p>The <span class="ui">Total Invoices</span> bar shows the invoice totals for all invoices that have a status of either
<span class="ui">Open</span> or <span class="ui">Paid</span>.</p>
</li><li>
<p>The <span class="ui">Total Received</span> bar shows the sum of invoices with the status of
<span class="ui">Paid</span>. Note that in Figure 6, no invoices have yet been paid.</p>
</li><li>
<p>The <span class="ui">Amount Becoming Due</span> bar reflects invoices that remain
<span class="ui">Open</span> and where the invoice date plus the number of days allotted for payment is greater than or equal to the current date.</p>
</li><li>
<p>The <span class="ui">Amount Overdue</span> bar indicates the <span class="ui">
Open</span> invoices where the invoice date plus the number of days allotted for payment is less than the current date.</p>
</li></ul>
<div class="caption"><strong>Figure 6. Report form</strong></div>
<br>
<img src="/site/view/file/81640/1/image.png" alt=""> </li><li>
<p>The app ensures data integrity with SharePoint lists, as when it checks whether dependent data exists before allowing the user to delete a list item, such as a customer purchase order.
<span class="ui">Figure 7</span>, for example, shows an alert message when the user tries to delete a customer record. The message warns that the customer cannot be deleted because there is at least one associated purchase order. The user must first delete
 a customer's purchase orders before deleting the customer to avoid orphaned purchase order list items.</p>
<p>Similar alerts are raised with other dependent data, such as with invoices that are associated with a purchase order.</p>
<div class="caption"><strong>Figure 7. Alert messages in the app</strong></div>
<br>
<img src="/site/view/file/81641/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection4">
<p>Make sure that you have SharePoint Server 2013 configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps. Also ensure that you are using the released versions
 of Visual Studio 2012 and Office Developer Tools for Visual Studio 2012.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection5">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<td>
<p>First release</p>
</td>
<td>
<p>April 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj692554.aspx" target="_blank">How to: Provision a Developer Site using your existing Office 365 subscription</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/office/apps/fp160950.aspx" target="_blank">Build apps for Office and SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/site/search?f%5B0%5D.Type=Technology&f%5B0%5D.Value=apps%20for%20SharePoint" target="_blank">Apps for Office and SharePoint samples</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/Apps-for-SharePoint-sample-64c80184" target="_blank">Apps for SharePoint sample pack</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
