# TBEdit (2005)
## Requires
* Visual Studio 2005
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2005 SDK
## Topics
* Visual Studio Editor
* VSX
## IsPublished
* True
## ModifiedDate
* 2011-03-01 12:07:22
## Description

<h1><span style="font-size:large">Introduction</span></h1>
<p>The sample implements a standard editor factory, that uses a VSTextBuffer&nbsp;object for the DocData object,&nbsp;a UserControl derived class, that implements IVsWindowPane, IOleCommandTarget, and IVsMultiViewDocumentView is used for the DocView object.</p>
<p>This sample implements a custom editor for .TBEDIT files. The editor&nbsp;is a minimal implementation deriving from UserControl, IVsWindowPane,&nbsp; IOleCommandTarget, and IVsMultiViewDocumentView. The editor supports&nbsp;several logical views (one per
 tab).&nbsp;</p>
<p>To test the sample, build and register the TBEdit project, and launch&nbsp;DevEnv.exe under the Experimental registry hive. Create a file named&nbsp;test.tbedit, and then open with or drag into the IDE. You should see&nbsp;the TBEditor displayed with a Design,
 Layout and Preview tab.&nbsp;</p>
<p>Note the TBEditor sample does not support document persistence outside&nbsp;of what is provide by the VsTextBuffer by default. No attempt has been&nbsp;made to actually persist the data in the VsTextBuffer into elements&nbsp;within the TBEditor control.<br>
&nbsp;&nbsp;</p>
<h1><br>
<span style="font-size:large">Things to note</span></h1>
<ul>
<li>The ProvideEditorExtension and ProvideEditorLogicalView attributes on the&nbsp;TBEdit package object.&nbsp;
</li><li>The EditorFactory.CreateEditorInstance implementation where we create the&nbsp;DocData (VsTextBuffer) and DocView (TBEditor) objects.
</li><li>The TBEditor's IVsMultiViewDocumentView implementation. </li></ul>
