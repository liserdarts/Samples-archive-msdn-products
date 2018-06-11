<%@ Page language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <title>Instructions</title>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/Instructions.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".ms-siteicon-a img").attr('src', '../Images/Instructions/instructionPageIcon.png');

        });
    </script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Instructions
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderId="PlaceHolderMain" runat="server">
    <h1>Please read the instructions for different modes in the game.</h1>
 <div id="instructions" class="instructions">
	<div class="header"></div>
		<div class="main">
        

<table class="tableinstructions">
    <tr>
    <td align="left" valign="top">
    		<p>MathTech offers two modes of play – <strong>Equation Mode</strong> and <strong>Answer Mode</strong>. Each mode has three levels. The player must complete all the three levels to win the game</p>
    	<table class="innertableInstructionTable">
          <tr>
            <td class="innertableInstructionTableData">
			<p><strong>How do you play the<br /> Equation Mode?</strong></p>
    
			<p>Click on the <img src="../images/answer-icon-small.jpg" width="61" height="25" /> icon.</p>

			<p><strong>You will get an algebraic equation to solve.</strong></p>

			<p>Choose the correct answer <stron>from the four options</stron>, for the given equation. With every correct answer a new equation will be generated.</p>

            </td>
            <td class="innertableInstructionTableData">
			<p><strong>How do you play the<br /> Answer Mode?</strong></p>
			<p>Click on the <img src="../images/equation-icon-small.jpg" width="61" height="25" /> icon.</p>

			<p><strong>You will get an answer for an algebraic equation.</strong></p>
            <p>Choose the values of the <strong>operands</strong>, and the <strong>operator</strong> to form an equation by clicking the given options. With every correct equation, a new answer will be generated.</p>
            </td>
            </tr>
            </table>
    </td>
    </tr>
    
            <tr>
            <td class="InnerLowerTable">
            <p><img src="../images/paper.png" width="45" height="45" /><br /><br />
           <bold> A correct answer <strong>adds +1</strong> to your <img src="../images/happy-face-small.png" width="30" height="27" /> counter </bold></p>
            <p> A wrong answer <strong>adds +1</strong> to your <img src="../images/sad-face-small.png" width="30" height="27" /> counter</bold></p>
            <p> <strong>Three</strong> correct answers in 40 seconds, will get you to the next level!<br />
            <strong>Every correct answer will add 5 seconds! to the timer</strong></p>
            <p><em>* The game resets, when you do not give three correct answers within the given time.</em></p>
            
            </td>
</tr>
</table>
</div>
<table class="instructionNavTable">
            <tr>
                <td align="center" valign="middle" class="tmode"><a href="../Pages/Default.aspx">
                    <img src="../images/navigation-buttons/home.png" width="135" height="78" border="0" /></a></td>
                <td align="center" valign="middle" class="emode"><a href="../Pages/Instructions.aspx">
                    <img src="../images/navigation-buttons/selected/instructions.png" width="135" height="78" border="0" /></a></td>
                <td align="center" valign="middle" class="amode"><a href="../Pages/Equation.aspx">
                    <img src="../images/navigation-buttons/equation.png" width="135" height="78" border="0" /></a></td>
                <td align="center" valign="middle" class="amode"><a href="../Pages/Answer.aspx">
                    <img src="../images/navigation-buttons/answer.png" width="135" height="78" border="0" /></a></td>
            </tr>
            <tr>
                <td colspan="4" align="right" valign="middle" class="mode"><a href="../Lists/Score" target="_blank" style="color:#ffffff;">View Scores</a></td>
            </tr>
        </table>
    </div><br />
 
</asp:Content>
