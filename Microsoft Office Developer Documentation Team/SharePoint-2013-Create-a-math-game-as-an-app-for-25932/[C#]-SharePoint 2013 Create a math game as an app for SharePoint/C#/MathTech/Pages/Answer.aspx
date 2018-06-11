<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
   
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
        
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.js"></script>


    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link rel="Stylesheet" type="text/css" href="../Content/jquery-ui.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/Answer.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>


</asp:Content>
<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Fill in the blanks!
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderId="PlaceHolderMain" runat="server">

    <h1>Create an algebraic equation to match the given "answer".</h1>
    
   <div id="answerMode" class="answer1">

        <div class="header"></div>
       
        <div class="main">
<div class="main" id="message"  style="display:none;"  > hey</div>
<table class="AnswerMain1">
<tr>
   <td class="TdataQ2">
     <label id="operandOption1" class="answernModeOperands" onclick="operandSelected(1);" />
   </td>
   <td class="TdataQ2">
     <label id="operandOption2" class="answernModeOperands" onclick="operandSelected(2);" />
	</td>
    <td class="TdataQ2">
     <label id="additionOperatorBtn" class="answernModeOperators" onclick="operatorSelected(0);" />
          <img src="../images/addition.jpg" width="50" height="50" border="0" alt="+">
    </td>
    <td class="TdataQ2">
     	<label id="operandOption5" class="answernModeOperands" onclick="operandSelected(5);" />
    </td>
   	<td class="TdataQ2">
       	<label id="operandOption6" class="answernModeOperands" onclick="operandSelected(6);" />
    </td>	          
</tr>
<tr>
    <td class="TdataQ2">
       <label id="operandOption3" class="answernModeOperands" onclick="operandSelected(3);"/>
	</td>      
     <td class="TdataQ2">
       	<label id="operandOption4" class="answernModeOperands" onclick="operandSelected(4);" />
	 </td>       
     <td class="TdataQ2">
         <label id="subtractionOperatorBtn" class="answernModeOperators" onclick="operatorSelected(1);" />
           <img src="../images/substractions.jpg" width="50" height="50" border="0" />
     </td>            
     <td class="TdataQ2">
       	<label id="operandOption7" class="answernModeOperands" onclick="operandSelected(7);" />
     </td>
     <td class="TdataQ2">
       	<label id="operandOption8" class="answernModeOperands" onclick="operandSelected(8);" />
     </td>                 
</tr>
<tr>            
   <td></td>     
   <td></td>    
   <td class="TdataQ2">
        <label id="multiplicationOperatorBtn" class="answernModeOperators" onclick="operatorSelected(2);"/>
           <img src="../images/multiplication.jpg" width="50" height="50" border="0" />
    </td>         
    <td></td>    
    <td></td>        
</tr>    
</table>         
 <!-- Equation -->               
            <table class="AnswerMain2">
                <tr>
                    <td class="TdataQ2">
                        <label id="operand1InTheEquationlbl" class="answerModeOptionSelected">?</label>
                    </td>
                    <td class="TdataQ2">
                        <label id="operatorInTheEquationlbl" class="answerModeOptionSelected">?</label>

                    </td>
                    <td class="TdataQ2">
                        <label id="operand2InTheEquationlbl" class="answerModeOptionSelected">?</label>
                    </td>
                    <td class="TdataQ2">
                        <img src="../images/equals-smaller.png" alt="" width="50" height="43" />
                    </td>
                    <td class="TdataQ2">
                        <label id="answerLbl" class="answerModeOptionSelected"></label>
                    </td>


                </tr>
</table>
<!-- Table for the timer and scores-->
            <table class="AnswerMain3">
                <tr>
                    <td class="TdataQ">
                       <label id="Timer"></label>
                     </td>
                    
                    <td class="TdataQ">
                       <img src="../images/smileys/happy.png" width="20" height="20" id="happyEnoticonAnswer"/><label id="correctCounterLbl" style="width: 300px; height: 300px; float: none; color: white;">0</label>

                    </td>
                    <td class="TdataQ">
                       <img src="../images/smileys/sad.png" width="20" height="20" id="sadEnoticonAnswer"/><label id="wrongCounterLbl" style="width: 300px; height: 300px; float: none; color: black;">0</label>

                    </td>
                    <td class="tmode"></td>
                        
                </tr>
            </table>     
        </div>

  

        <table class="AnswerNav">
            <tr>
                <td class="tmode"><a href="../Pages/Default.aspx">
                    <img src="../images/navigation-buttons/home.png" width="135" height="78" border="0" /></a></td>
                <td class="emode"><a href="../Pages/Instructions.aspx">
                    <img src="../images/navigation-buttons/instructions.png" width="135" height="78" border="0" /></a></td>
                <td class="amode"><a href="../Pages/Equation.aspx">
                    <img src="../images/navigation-buttons/equation.png" width="135" height="78" border="0" /></a></td>
                <td class="amode"><a href="../Pages/Answer.aspx">
                    <img src="../images/navigation-buttons/selected/answer.png" width="135" height="78" border="0" /></a></td>
              
            </tr>
            <tr>
                <td colspan="4" align="right" valign="middle" class="mode"><a href="../Lists/Score" target="_blank" style="color:#ffffff;">View Scores</a></td>
            </tr>
        </table>
    </div>
    <div id="dialogDiv" title="">
        <p id="dialogDivMessage"></p>
    </div>
</asp:Content>

