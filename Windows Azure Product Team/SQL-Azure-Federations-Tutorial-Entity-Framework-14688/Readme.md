# SQL Azure Federations Tutorial -- Entity Framework
## Requires
* Visual Studio 2010
## License
* MS-LPL
## Technologies
* SQL Azure
## Topics
* Federation
* Federations
## IsPublished
* True
## ModifiedDate
* 2012-01-24 07:47:18
## Description

<h1>Introduction</h1>
<p><em>Federations in SQL Azure are a way to achieve greater scalability and performance from the database tier of your&nbsp;application through horizontal partitioning (or sharding). Tables within a database are aplit by row and portioned across multiple databases
 (shards or federation members).</em></p>
<p><em>This sample is a winform application that demonstrates how to use ADO.NET Entity Framework to access, split and drop a SQL Azure federation. This sample also provides some scripts for creating a federation and populating some sample data into the database.
 The tutorial instructions will be found <a href="http://msdn.microsoft.com/en-us/library/windowsazure/hh778419.aspx">
here</a>.</em></p>
<h1>Prerequisites</h1>
<ul>
<li>Get a Windows&nbsp;Azure subscription. For more information, see Getting Started with SQL Azure at
<a href="http://social.technet.microsoft.com/wiki/contents/articles/getting-started-with-sql-azure-using-the-windows-azure-platform-management-portal.aspx">
http://social.technet.microsoft.com/wiki/contents/articles/getting-started-with-sql-azure-using-the-windows-azure-platform-management-portal.aspx</a>.
</li><li>Create a SQL&nbsp;Azure server. For the instructions, see How to Create a SQL Azure Server at
<a href="http://social.technet.microsoft.com/wiki/contents/articles/how-to-create-a-sql-azure-server.aspx">
http://social.technet.microsoft.com/wiki/contents/articles/how-to-create-a-sql-azure-server.aspx</a>.
</li><li>Configure the&nbsp;SQL Azure Server firewall. For the instructions, see How to: Configure the SQL Azure Firewall at
<a href="http://msdn.microsoft.com/en-us/library/windowsazure/ee621783.aspx">http://msdn.microsoft.com/en-us/library/windowsazure/ee621783.aspx</a>.
</li><li>Install Visual&nbsp;Studio 2010. </li></ul>
<h1>Building the Sample</h1>
<p><strong>To run the script to create a federation</strong></p>
<ol>
<li>Open the SetupFederation\CreateFederation.cmd file from the folder where you extract the sample in Notepad.
</li><li>Enter your SQL Azure server and account information (line 2 ~ line 5). </li><li>Save the file. </li><li>Run the CreateFederation.cmd file. </li></ol>
<p><strong>To run the sample from Visual Studio 2010</strong></p>
<ol>
<li>Open Visual Studio 2010 as an administrator. </li><li>Browse to the folder where you extracted the sample and open CustomerFederation.sln.
</li><li>Modify the app.config file with your SQL Azure server and account information.
</li><li>Press F6 to build the solution. </li><li>Press F5 to debug the solution. </li></ol>
<h1>Source Code Files</h1>
<ul>
<li>SetupFederation&nbsp;- contains the scripts for creating a federation and populate some sample&nbsp;data.
</li><li><em>FederationsForm - contains the winform project.</em> </li></ul>
<h1>More Information</h1>
<p><em>For more information, see Federations in SQL Azure at <a href="http://msdn.microsoft.com/en-us/library/windowsazure/hh597452.aspx">
http://msdn.microsoft.com/en-us/library/windowsazure/hh597452.aspx,and</a> Federations: Building Scalable, Elastic, and Multi-tenant Database Solutions with SQL Azure at
<a href="http://social.technet.microsoft.com/wiki/contents/articles/2281.aspx">http://social.technet.microsoft.com/wiki/contents/articles/2281.aspx</a>.</em></p>
