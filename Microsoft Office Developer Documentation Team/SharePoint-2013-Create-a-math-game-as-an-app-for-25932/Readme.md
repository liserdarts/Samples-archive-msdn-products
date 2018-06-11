# SharePoint 2013: Create a math game as an app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* jQuery
* Javascript
* Visual Studio 2012
* apps for SharePoint
## Topics
* jQuery
* Javascript
## IsPublished
* True
## ModifiedDate
* 2014-06-11 02:40:56
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Create a math game as an app for SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p>This sample app demonstrates how to use JavaScript in an app for SharePoint to develop a math game.</p>
</div>
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p>The solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution contains a custom JavaScript file, and custom pages for individual modes in the game.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either of the following:</p>
<ul>
<li>
<p>SharePoint Server 2013 configured to host apps, and with a developer site collection already created; or,</p>
</li><li>
<p>Access to an Office 365 developer site configured to host apps.</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong>Default.aspx</strong> webpage, which is used to present the visualized data of the Home page.</p>
</li><li>
<p>The <strong>Answer.aspx</strong>, and <strong>Equation.aspx</strong> webpages are used to present the visualized data of the Answer, and Equation mode of the game.</p>
</li><li>
<p>The <strong>Instructions.aspx</strong> webpage, which is used to present the visualized data for the Instruction page.</p>
</li><li>
<p>The <strong>Answer.js</strong>, <strong>Equation.js</strong>, and <strong>Instructions.js</strong> file in the
<strong>scripts</strong> folder, contains the logic for different modes in the game using the JavaScript implementation of the client object model (JSOM). The game is also fully functional and implemented in pure JavaScript, CSS, and HTML.</p>
</li><li>
<p>The <strong>jquery-ui.js</strong> file in the <strong>scripts</strong> folder and
<strong>jquery-ui.css</strong> file in the <strong>content</strong> folder, which are used to show jQuery-style dialogs.</p>
</li><li>
<p>The <strong>Score</strong> list definition and instance, which is used to save wins and losses for the user.</p>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint and have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the <span class="ui">SP_MathTech_js.sln</span> file using Visual Studio 2012.</p>
</li><li>
<p>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 developer site collection or Office 365 developer site to the
<span class="keyword">Site URL</span> property.</p>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 developer site collection or Office 365 developer site if you are prompted to do so by the browser.</p>
</li></ol>
<p>The following images depict the game user interface at different modes:</p>
<div class="caption"><strong>Figure 1. Game user interface at startup</strong></div>
<br>
<img id="116659" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-5d2a7a27/image/file/116659/1/sp15con_create_a_math_game_as_an_app_for_sharepoint2013_fig1.png" alt="Figure 1" width="360" height="402">
<p>&nbsp;</p>
<div class="caption"><strong>Figure 2. Game user interface for Answer mode</strong></div>
<br>
<img id="116660" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-5d2a7a27/image/file/116660/1/sp15con_create_a_math_game_as_an_app_for_sharepoint2013_fig2.png" alt="Figure 2" width="360" height="357">
<p>&nbsp;</p>
<div class="caption"><strong>Figure 3. Game user interface for Equation mode</strong></div>
<br>
<img id="116661" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-5d2a7a27/image/file/116661/1/sp15con_create_a_math_game_as_an_app_for_sharepoint2013_fig3.png" alt="Figure 3" width="360" height="419">
<p>&nbsp;</p>
<div class="caption"><strong>Figure 4. Time's up message</strong></div>
<br>
<img id="116662" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-5d2a7a27/image/file/116662/1/sp15con_create_a_math_game_as_an_app_for_sharepoint2013_fig34.png" alt="Figure 4" width="360" height="337">
<p>&nbsp;</p>
<div class="caption"><strong>Figure 5. Game user interface at stage conclusion</strong></div>
<br>
<img id="116663" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-5d2a7a27/image/file/116663/1/sp15con_create_a_math_game_as_an_app_for_sharepoint2013_fig5.png" alt="Figure 5" width="360" height="329"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: September, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
</div>
