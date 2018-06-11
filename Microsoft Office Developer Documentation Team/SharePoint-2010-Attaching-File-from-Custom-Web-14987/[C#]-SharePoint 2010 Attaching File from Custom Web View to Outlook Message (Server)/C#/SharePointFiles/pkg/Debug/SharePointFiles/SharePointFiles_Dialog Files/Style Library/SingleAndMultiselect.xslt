<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>

	<xsl:param name="qs-hidedocs"/>
  <xsl:param name="qs-location"/>

  <!--Contains our extra values - useful for determining if we are in our custom outlook form-->
	<xsl:variable name="Extensions">
		<xsl:value-of select="/PageData/Extensions"/>
	</xsl:variable>

  <!--main html page-->
  <xsl:template match="PageData">
		<xsl:text disable-output-escaping="yes"><![CDATA[<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">]]></xsl:text>
		<html dir="ltr">
			<head>
				<meta http-equiv="X-UA-Compatible" content="IE8"/>
				<meta name="GENERATOR" content="Microsoft SharePoint"/>
				<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <META HTTP-EQUIV="MSThemeCompatible" CONTENT="yes" />
				<meta http-equiv="Expires" content="0"/>
				<title id="onetidTitle">SharePoint Libraries and Files</title>
				<link rel="stylesheet" type="text/css" href="/_layouts/1033/styles/corev4.css"/>
				<link rel="stylesheet" type="text/css" href="/Style%20Library/cfd.css"/>
        <script type="text/javascript" src="/_layouts/1033/init.js"></script>
				<script type="text/javascript" src="/_layouts/1033/core.js"></script>
				<script type="text/javascript" src="/Style%20Library/jquery-1.4.1.js"></script>

				<script language="JavaScript">
          <xsl:choose>
            <xsl:when test="contains($Extensions, 'multiselect')">
              var allowMultiSelect = true;
              var $lastSelectedTrIndex = 0;

              function selectAll(e) {

              // Unselect everthing
              jQuery('#FileDialogViewTable').find('tr').toggleClass('s4-itm-selected', false);

              if (jQuery('#selectAll').is(':checked')) {
              // Select all files
              jQuery('#FileDialogViewTable').find('tr[fileattribute=file]').toggleClass('s4-itm-selected', true);
              }
              updateDiblyCue();
              }

              function highlightTRforHover(td) {
              $tr = jQuery(td).closest('tr');
              $tr.addClass('s4-itm-hover');
              }


              function dehighlightTRforHover(td) {
              $tr = jQuery(td).closest('tr');
              $tr.removeClass('s4-itm-hover');
              }

              function toggleVersionDropdownEvent(e) {
              toggleVersionDropdown(e.target);
              }

              function toggleVersionDropdown(td) {
              $tr = jQuery(td).closest('tr');
              // Ensure dropdown only contracts if the root file is selected.  Without this check, the toggle could get called on a version itself, which would collapse the group and hide the currently focussed item.
              if ($tr.attr('fileattribute') == 'file')
              {
              $id = $tr.attr('id');
              jQuery('#FileDialogViewTable').find('tr[fileattribute=Version]').filter('[id=' + $id + ']').toggleClass('hideversion');
              }
              }

              function toggleAttachmentDropdownEvent(e) {
              toggleAttachmentDropdown(e.target);
              }

              function toggleAttachmentDropdown(td) {
              $tr = jQuery(td).closest('tr');
              if ($tr.attr('fileattribute') == 'file') {
              $id = $tr.attr('id');
              jQuery('#FileDialogViewTable').find('tr[fileattribute=Attachment]').filter('[id=' + $id + ']').toggleClass('hideversion');
              }
              }

              /* Toggle selected TR */

              function toggleSelectedTREvent(e) {
              toggleSelectedTR(e.target);
              }

              function toggleSelectedTR(td) {

              // A file or folder has been selected or deselected.  Begin by toggling the class
              $tr = jQuery(td).closest('tr');
              $tr.toggleClass('s4-itm-selected');



              if (allowMultiSelect) {
                // MULTI SELECT - appropriate action based on what has just been selected

                // If the TR is a folder and is now selected, we need to unselect anything else
                if ($tr.hasClass('s4-itm-selected') &amp;&amp; $tr.attr('fileattribute') == 'folder') 
                {
                  jQuery('#FileDialogViewTable').find('tr').toggleClass('s4-itm-selected', false);
                  $tr.toggleClass('s4-itm-selected'); // Reinstate the toggle we just removed on the selected folder
                }

                // If this TR is a file, we need to ensure no folders are selected
                if ($tr.attr('fileattribute') == 'file') 
                {
                  jQuery('#FileDialogViewTable').find('tr[fileattribute=folder]').toggleClass('s4-itm-selected', false);
                }

                  // We no longer know the state of the selectAll, so lets clear it
                  jQuery('#selectAll').removeAttr('checked');
               
              }
              else 
              {
              // SINGLE SELECT - unselect everything else
              jQuery('#FileDialogViewTable').find('tr').toggleClass('s4-itm-selected', false);
              $tr.toggleClass('s4-itm-selected'); // Reinstate the toggle we just removed on the new selection
              }

              // Keep a handle to the last selected item.  We need this to support Shift+Click
              if ($tr.hasClass('s4-itm-selected')) {
              $lastSelectedTrIndex = $tr.index();
              }

              updateDiblyCue();
              }

              function basicTRSelectionFromCFD(e) {
              // SINGLE SELECT - unselect everything else
              jQuery('#FileDialogViewTable').find('tr').toggleClass('s4-itm-selected', false);
              $tr = jQuery(e.target).closest('tr');
              $tr.toggleClass('s4-itm-selected'); // Reinstate the toggle we just removed on the new selection
              }

              function checkedInFileSelectionFromCFD(e) {
              jQuery("div.needscheckout").show();
              window.event.cancelBubble = true;
              }

              function updateDiblyCue() {

              // Check or uncheck the checkbox values based on the revised selection
              jQuery('#FileDialogViewTable').find('input[type=checkbox]').not('#selectAll').each(function () {
              this.checked = $(this).closest('tr').hasClass('s4-itm-selected');
              });

              // Update the DOM to reflect what is selected
              jQuery('#DiblyCue').text('');
              jQuery('#FileDialogViewTable').find('tr.s4-itm-selected').each(function () {
              $PreferredFileInformation = $(this).closest('tr').attr('PreferredFileInformation');
              jQuery('#DiblyCue').append($PreferredFileInformation + '#');
              });
              }

              function startUp() {

              if (allowMultiSelect) {

              jQuery('#FileDialogViewTable').find('td').not("[special='versiondropdown']").not("[special='attachmentdropdown']").click(toggleSelectedTREvent);
              jQuery('#FileDialogViewTable').find("td[special='versiondropdown']").click(toggleVersionDropdownEvent);

              }
              else {
              jQuery('#FileDialogViewTable').find('tr').not('.ms-viewheadertr').click(basicTRSelectionFromCFD);
              jQuery('#FileDialogViewTable').find("tr[special='needscheckout']").dblclick(checkedInFileSelectionFromCFD);
              }

              jQuery('#selectAll').click(selectAll);
              }

              jQuery(document).ready(startUp);
            </xsl:when>
            <xsl:otherwise>

            </xsl:otherwise>
          </xsl:choose>
					</script>

			</head>

      <xsl:element name="body">
        <xsl:attribute name="serverType">OWS</xsl:attribute>
      
          <xsl:attribute name="class">body-banner</xsl:attribute>          

        <input type="hidden" name="folderLocation" id="folderLocation">
          <xsl:attribute name="value">
            <xsl:value-of select="/PageData/ServerRelativeUrl" />
            <xsl:text>/</xsl:text>
            <xsl:copy-of select="$qs-location" />
          </xsl:attribute>
        </input>
        
          <div id="tab-container">
              <xsl:element name="div">
                <xsl:attribute name="class">
                  <xsl:if test="/PageData/ViewMode = 'Library' or /PageData/ViewMode = 'OutlookLibrary'">selected</xsl:if>
                </xsl:attribute>
                <a id="Browse" href="javascript:" onclick="javascript:return OnClickFilter(this,event);" SortingFields="View=">Browse</a>
              </xsl:element>
          </div>
        

        <!-- Banner -->
        <table class="ms-fileDlgBannerTbl" cellpadding="0" cellspacing="0" border="0" width="100%">
          <tr>
            <td style="height:0px; vertical-align:top; white-space:nowrap">
              <div class="s4-title" style="border-bottom:1px solid #E2E2E2">
                <div class="s4-title-inner">
                  <table class="s4-titletable" cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                      <td class="s4-titlelogo">
                        <image id="onetidFileDlgImg" src="/_layouts/images/siteIcon.png"/>

                      </td>
                      <td width="100%">
                            <table class="ms-fileDlgTitleTbl" cellpadding="0" cellspacing="0" border="0">
                              <tr>
                                <td valign="top" class="ms-titlearea">
                                  <span class="ms-fileDlgSiteTitle" id="SpanForDebugTrace">
                                    <xsl:value-of select="Title"/>
                                  </span>
                                </td>
                              </tr>
                              <tr>
                                <td style="height:100%; vertical-align:top;" class="s4-titletext">
                                  <h1 class="ms-fileDlgListTitle" style="font-size:1.7em">
                                    <xsl:value-of select="Description"/>
                                  </h1>
                                </td>
                              </tr>
                            </table>
                      </td>
                      <td>
                      </td>
                    </tr>
                  </table>
                </div>
              </div>
            </td>
          </tr>
          <tr>
            <td>
              <table class="ms-listviewtable" id="FileDialogViewTable" onmouseover="EnsureSelectionHandler(event,this,0)" width="100%" border="0" rules="rows" cellspacing="0" cellpadding="1">
                <thead>
                  <xsl:apply-templates select="Headers"/>
                </thead>
                <tbody>
                  <xsl:apply-templates select="ListContent/ContentItem"/>
                  <xsl:if test="count(ListContent/ContentItem) = 0">
                    <tr>
                      <td style="display:none"></td>
                      <td></td>
                      <td colspan="{count(Headers/Header) - 2}">
                        (There are no items matching the current selection)
                      </td>
                    </tr>
                  </xsl:if>
                  <tr>
                    <td colspan="{count(Headers/Header)}">
                    </td>
                  </tr>
                </tbody>
              </table>
              <div id="DiblyCue"/>
            </td>
          </tr>
        </table>
      </xsl:element> <!-- close of body element-->
		</html>
	</xsl:template>

	<xsl:template match="ContentItem">

    <!--Set the id of the td to the url you want to navigate to-->
		<xsl:variable name="id">
				<xsl:value-of select="FileUrl"/>
		</xsl:variable>

		<xsl:variable name="PreferredFileInformation">
			<xsl:value-of select="FileAttribute"/>;<xsl:value-of select="$id"/>;<xsl:value-of select="Property[@Name='Name']/@Value"/>;<xsl:value-of select="Property[@Name='ows__dlc_DocId']/@Value"/>;<xsl:value-of select="Property[@Name='ows__UIVersionString']/@Value"/>;
		</xsl:variable>

		<xsl:element name="tr">

			<xsl:attribute name="fileattribute">
						<xsl:value-of select="FileAttribute"/>
			</xsl:attribute>

      <xsl:attribute name="id">
				<xsl:value-of select="$id"/>
			</xsl:attribute>
			<xsl:attribute name="PreferredFileInformation">
				<xsl:value-of select="$PreferredFileInformation"/>
			</xsl:attribute>
			<xsl:attribute name="class">ms-itmhover</xsl:attribute>

			<xsl:element name="td">
				<xsl:attribute name="class">ms-vb-itmcbx ms-vb-firstCell</xsl:attribute>
				<xsl:if test="not(contains($Extensions, 'multiselect'))">
					<xsl:attribute name="style">display:none</xsl:attribute>
				</xsl:if>
				<xsl:if test="FileAttribute = 'file'">
					<input type="checkbox" class="s4-itm-cbx"/>
				</xsl:if>
			</xsl:element>

			<td class="ms-vb-icon">
            <img border="0" src="/_layouts/images/{Property[@Name='DocIcon']/@Value}"/>            
			</td>

			<xsl:variable name="StyleForName">
				<xsl:if test="/PageData/ViewMode = 'RecentSites'">white-space:nowrap;</xsl:if>
			</xsl:variable>

			<td class="ms-vb2" style="padding-left: 4px; padding-bottom:5px;{$StyleForName}">
				<xsl:value-of select="Property[@Name='Name']/@Value"/>
			</td>

				<td class="ms-vb2" style="padding-left: 4px; white-space:nowrap;">
					<xsl:value-of select="Property[@Name='ows__dlc_DocId']/@Value"/>
				</td>

				<xsl:choose>
					<xsl:when test="count(Versions/Version)&gt;0">
						<!-- In Outlook/ custom hosting and there are versions-->
						<td special="versiondropdown" class="ms-vb2 version-topup" style="padding-left: 4px" title="There are {count(Versions/Version) + 1}  versions.">
							<xsl:value-of select="Property[@Name='ows__UIVersionString']/@Value"/>
							<span>
								(<xsl:value-of select="count(Versions/Version) + 1"/>)
							</span>
							<img src="/_layouts/images/ecbarw.png" class="hide"/>
						</td>
					</xsl:when>
					<xsl:when test="count(Versions/Version)=0 and contains($Extensions, 'versions')">
						<!-- In Outlook/ custom hosting and there are no versions -->
						<td class="ms-vb2" style="padding-left: 4px" title="There are no previous versions">
							<xsl:value-of select="Property[@Name='ows__UIVersionString']/@Value"/>
						</td>
					</xsl:when>
					<xsl:otherwise>
						<td class="ms-vb2" style="padding-left: 4px">
							<!-- In classic CFD or custom hosting where versions cannot be selected -->
							<xsl:value-of select="Property[@Name='ows__UIVersionString']/@Value"/>
						</td>
					</xsl:otherwise>
				</xsl:choose>

				<td class="ms-vb2" style="padding-left: 4px">
					<xsl:value-of select="Property[@Name='Editor']/@Value"/>
				</td>

        <td class="ms-vb2" style="padding-left: 4px" title="{Property[@Name='Last_x0020_Modified_Original']/@Value}">
          <xsl:value-of select="Property[@Name='Last_x0020_Modified']/@Value"/>
        </td>

		</xsl:element>
		<xsl:apply-templates select="Versions/Version"/>
	</xsl:template>



	<xsl:template match="Version">

		<xsl:variable name="tbartopupclass">
			<xsl:if test="position() = last()">-end</xsl:if>
		</xsl:variable>

		<tr class="ms-itmhover hideversion" fileattribute="Version" id="{ancestor::ContentItem/FileUrl}"
				PreferredFileInformation="file;{@Url};{ancestor::ContentItem/Property[@Name='Name']/@Value};{ancestor::ContentItem/Property[@Name='ows__dlc_DocId']/@Value};{@VersionLabel}">

			<td class="ms-vb-itmcbx ms-vb-firstCell">
				<input type="checkbox" class="s4-itm-cbx"/>
			</td>

			<td class="ms-vb-icon tbar{$tbartopupclass}">
			</td>

			<td class="ms-vb2" style="padding-left: 4px;">
				<img src="/_layouts/images/versions.gif" style="vertical-align:middle;padding-right:6px;"/>
				<i>
					Version <xsl:value-of select="@VersionLabel"/>
				</i>
				<xsl:choose>
					<xsl:when test="string-length(@CheckInComment)&gt;20">
						<span style="padding-left: 6px" title="{@CheckInComment}">
							(<xsl:value-of select="substring(@CheckInComment,0, 20)"/>...)
						</span>
					</xsl:when>
					<xsl:when test="string-length(@CheckInComment)&gt;0 and string-length(@CheckInComment)&lt;21">
						<span style="padding-left: 6px">
							(<xsl:value-of select="@CheckInComment"/>)
						</span>
					</xsl:when>
				</xsl:choose>
			</td>

			<xsl:if test="/PageData/Headers/Header[ColumnId='ows__dlc_DocId']">
				<td class="ms-vb2" style="padding-left: 4px; white-space:nowrap;">
				</td>
			</xsl:if>

			<xsl:if test="/PageData/Headers/Header[ColumnId='ows__UIVersionString']">
				<td class="ms-vb2" style="padding-left: 4px">
					<xsl:value-of select="@VersionLabel"/>
				</td>
			</xsl:if>

			<xsl:if test="/PageData/Headers/Header[ColumnId='ParentFolderName']">
				<td class="ms-vb2" style="padding-left: 4px">
				</td>
			</xsl:if>

			<xsl:if test="/PageData/Headers/Header[ColumnId='Editor']">
				<td class="ms-vb2" style="padding-left: 4px">
          <xsl:value-of select="@CreatedBy"/>
				</td>
			</xsl:if>

			<xsl:if test="/PageData/Headers/Header[ColumnId='Last_x0020_Modified']">
        <td class="ms-vb2" style="padding-left: 4px" title="{@CreatedTooltip}">
          <xsl:value-of select="@Created"/>
        </td>
			</xsl:if>
		</tr>
	</xsl:template>

	<xsl:template match="Headers">
		<xsl:if test="child::node()">
			<tr class="ms-viewheadertr ms-vhltr" vAlign="top" id="headerTr">

				<xsl:element name="th">
					<xsl:attribute name="class">ms-vh-icon</xsl:attribute>
					<xsl:attribute name="scope">col</xsl:attribute>
					<xsl:if test="not(contains($Extensions, 'multiselect'))">
						<xsl:attribute name="style">display:none</xsl:attribute>
					</xsl:if>
					<INPUT id="selectAll" class="s4-selectAllCbx" title="Select or deselect all items" type="checkbox"/>
				</xsl:element>

				<xsl:apply-templates select="Header"/>

				<xsl:if test="/PageData/Action='Save' and /PageData/ViewMode='Library' ">
					<th class="ms-vh2" scope="col">
						<div class="ms-vh-div" style="padding-left:4px;">Action</div>
					</th>
				</xsl:if>
			</tr>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Header">
				<th class="ms-vh2" scope="col">
					<!-- Everything else -->
							<div>
								<xsl:value-of select="ColumnTitle"/>
							</div>
				</th>
	</xsl:template>

	<xsl:template name="LocationLink">
		<xsl:choose>
			<xsl:when test="substring (substring (Property[@Name='ParentFolderName']/@Value, 12),0,3) = ' -'">
				<xsl:value-of select="substring(Property[@Name='ParentFolderName']/@Value,0,12)"/>
			</xsl:when>
			<xsl:otherwise>
				<!--<xsl:value-of select="substring(Property[@Name='ParentFolderName']/@Value,12)"/>/<xsl:value-of select="substring(substring (Property[@Name='ParentFolderName']/@Value,12),0,3)"/>-->
				<xsl:value-of select="Property[@Name='ParentFolderName']/@Value"/>
			</xsl:otherwise>
		</xsl:choose>    
	</xsl:template>
</xsl:stylesheet>