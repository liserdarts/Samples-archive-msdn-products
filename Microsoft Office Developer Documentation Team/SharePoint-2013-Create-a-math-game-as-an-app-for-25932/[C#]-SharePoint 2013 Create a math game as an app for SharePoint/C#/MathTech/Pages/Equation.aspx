<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.js"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link rel="Stylesheet" type="text/css" href="../Content/jquery-ui.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/Equation.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>
<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Solve it!
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderId="PlaceHolderMain" runat="server">
  
    <h2>Let's solve some algebraic equations.</h2>
    <div id="equationMode" class="equation1">

<div class="header"></div>

<div class="main">
    <!--Table contains different operand values (c1,c2,c3) -->
<table class="EquationMain">
<tr>
    <td align="center" valign="middle">  
        <label id="optionResultbtn1" class="equationModeOptions" onclick="resultSelected(1)"/>       
    </td>
            <td align="center" valign="middle">
                    <label  id="optionResultbtn2" class="equationModeOptions" onclick="resultSelected(2)"/>
            </td>
            <td align="center" valign="middle">
                    <label id="optionResultbtn3" class="equationModeOptions"  onclick="resultSelected(3)"/>
            </td>
             <td align="center" valign="middle">
                    <label id="optionResultbtn4" class="equationModeOptions" onclick="resultSelected(4)"/>
            </td>
            
            
</tr>
</table>
    <!--Table contains the equation (a+b=c) -->
<table class="EquationMain2">
<tr>
<td align="center" valign="middle">
               <label id="operand1valueLabel" class="equationModeOperands" >12</label>
            </td>
            <td align="center" valign="middle">
                <label id="operatorValueLabel" class="equationModeOperator">+</label>
                
            </td>
            <td align="center" valign="middle">
                <label id="operand1valueLabe2" class="equationModeOperands">2</label>
            </td>
    <td align="center" valign="middle" class="amode"><img src="../images/equals.png" width="70" height="70" /></td>
            <td align="center" valign="middle">
                <label id="displayResultLabel" class="equationModeAnswer">?</label>
            </td>


</tr>
</table>
    <!-- Table contains the timer and the  smilies.
   -->
<table class="EquationMain3">
                <tr>
                    <td valign="middle" align="left">
                       <label id="Timer"></label>
                     </td>
                     <td align="center" valign="middle">

                    </td>
                    <td align="center" valign="middle">
                       <img src="../images/smileys/happy.png" width="20" height="20" id="happyEnoticonEquation"/><label id="correctCounterLabel" style="width: 300px; height: 300px; float: none; color: white;">0</label>

                    </td>
                    <td align="center" valign="middle">
                       <img src="../images/smileys/sad.png" width="20" height="20" id="sadEnoticonEquation"/><label id="wrongCounterLabel" style="width: 300px; height: 300px; float: none; color: black;">0</label>

                    </td>
                </tr>
            </table>
   
</div>


<!-- Table contains navigations to diffrent modes in the games.
   -->
<table class="EquationNav">
<tr>
<td align="center" valign="middle" class="tmode"><a href="../Pages/Default.aspx"><img src="../images/navigation-buttons/home.png" width="135" height="78" border="0" /></a></td>
<td align="center" valign="middle" class="emode"><a href="../Pages/Instructions.aspx"><img src="../images/navigation-buttons/instructions.png" width="135" height="78" border="0" /></a></td>
<td align="center" valign="middle" class="amode"><a href="../Pages/Equation.aspx"><img src="../images/navigation-buttons/selected/equation.png" width="135" height="78" border="0" /></a></td>
<td align="center" valign="middle" class="amode"><a href="../Pages/Answer.aspx"><img src="../images/navigation-buttons/answer.png" width="135" height="78" border="0" /></a></td>
</tr>
<tr>
    <td colspan="4" align="right" valign="middle" class="mode"><a href="../Lists/Score" target="_blank" style="color:#ffffff;">View Scores</a></td>
</tr>
</table>
</div>
<div id="dialogDiv" title="">
  <p id="dialogDivMessage"></p>
</div>


    <!--
   -->
</asp:Content>

