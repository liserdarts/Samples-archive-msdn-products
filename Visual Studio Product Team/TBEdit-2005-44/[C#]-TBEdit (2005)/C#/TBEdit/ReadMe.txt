TBEdit Sample:

   Demo's implementing a custom editor that hosts multiple logical views on a
   TabControl.  
   
Introduction:

   The sample implements a standard editor factory, that uses a VSTextBuffer
   object for the DocData object, and a UserControl derived object for the
   DocView object.
   
   This sample implements a custom editor for .TBEDIT files. The editor
   is a minimal implementation deriving from UserControl, IVsWindowPane,
   IOleCommandTarget, and IVsMultiViewDocumentView. The editor supports
   several logical views (one per tab).
   
   To test the sample, build and register the TBEdit project, and launch
   DevEnv.exe under the Experimental registry hive. Create a file named
   test.tbedit, and then open with or drag into the IDE. You should see
   the TBEditor displayed with a Design, Layout and Preview tab.
   
   Note the TBEditor sample does not support document persistence outside
   of what is provide by the VsTextBuffer by default. No attempt has been
   made to actually persist the data in the VsTextBuffer into elements 
   within the TBEditor control.
   
Things to note:

   The ProvideEditorExtension and ProvideEditorLogicalView attributes on the 
   TBEdit package object.
   
   The EditorFactory.CreateEditorInstance implementation where we create the
   DocData (VsTextBuffer) and DocView (TBEditor) objects.
   
   The TBEditor's IVsMultiViewDocumentView implementation.
    