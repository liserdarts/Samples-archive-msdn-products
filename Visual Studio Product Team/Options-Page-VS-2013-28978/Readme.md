# Options Page - VS 2013
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2013
## Topics
* Extensibility
## IsPublished
* True
## ModifiedDate
* 2014-05-29 06:59:07
## Description

<p>This sample demonstrates how to add custom options pages into the standard Visual Studio Options dialog. The sample adds two options pages and demonstrates how to customize the presentation and properties' persistence.&nbsp;<br>
<br>
</p>
<h3>Goals</h3>
<ul>
<li>Integrate custom options pages into Visual Studios Options dialog. </li><li>Supports properties persistence. </li><li>Supports custom user control as a UI for the property page. </li></ul>
<p><br>
The sample solution includes only one project (OptionsPage), which contains classes that provide a Visual Studio Package and custom Options Pages that will be integrated into the Visual Studio IDE. The OptionsPagePackage class uses ProvideOptionsPages attribute
 to provide custom options pages.<br>
<br>
To implement an options page, the class should be a Microsoft.VisualStudio.Shell.DialogPage. Within the sample we have two pages: OptionsPageGeneral and OptionsPageCustom. Both pages allow the user to provide custom properties. OptionsPageGeneral uses a standard
 Property editor control for presentation. OptionsPageCustom uses a custom control (OptionsCompositeControl) for the UI.<br>
<br>
The ProvideProfile attribute is used to provide persistence for the package. The DesignerSerializationVisibility attribute is used to allow persistence for each property of the options page.<br>
<br>
</p>
<h3>To start the sample:</h3>
<ol>
<li>Build the solution </li><li>Open Visual Studio under experimental hive </li><li>Check new category and new options pages within Visual Studio Options dialog. (Menu [Tools].[Options].[My Managed Options (C#)]).
</li></ol>
<p>&nbsp;</p>
<h3>Screenshot</h3>
<p><img src="file://tg-server-hp/PartnerDrops/VSSDKSamples/2013/VSSDK_IDE_Sample_Options_Page/description/Example.OptionsPage.jpg" alt=""></p>
<p><img id="115706" src="/vstudio/site/view/file/115706/1/Example.OptionsPages.png" alt="" width="1212" height="897"></p>
