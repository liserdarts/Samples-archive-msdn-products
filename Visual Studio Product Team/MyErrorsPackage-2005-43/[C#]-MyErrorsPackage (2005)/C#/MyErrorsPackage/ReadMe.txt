MyErrorsPackage Sample:

   Demo's creation of an ErrorListProvider from a ToolWindow. 
   
Introduction:

   This sample illustrates how to use an ErrorListProvider to participate in
   the ErrorList ToolWindow. 
   
   Build and register the sample, and run DevEnv.exe under the VS Experimental 
   hive. Select the "My Errors Window" menu item, under the View.Other Windows
   menu. Add a few strings to the listbox with the Add Error button. Note the 
   items added to the Error List toolwindow. Double click one of the items in
   the Error List toolwindow, and note the toolwindow is activated and the
   corresponding item selected in the toolwindow's listbox.
   
The MyToolWindow class:

   Note that the ErrorListProvider object is actually created in the 
   OnToolWindowCreated override. This is neccessary to ensure that the
   ErrorListProvider is initialized with a valid IServiceProvider, and
   the OnToolWindowCreated override is a good place to do this, as we can be 
   certain that the toolwindow has been sited at this point.
   
The MyControl class:

   Thic class implements the actual window pane for the toolwindow, and when
   you add additional strings to the listbox, a corresponding string is added
   to the Error List toolwindow, by way of creating an ErrorTask object and
   adding it through the ErrorListProvider in the parent MyToolWindow object.
   
   Note the btnAddError_Click event also sinks the Navigate event, so that 
   we can set the focus to the listbox and select the item corresponding to the
   ErrorTask that is dbl clicked on in the Error List toolwindow.
    