AllowParams Sample:

   Demo's usage of the ParametersDescription property on OleMenuCommand's in
   order to support the ALLOWPARAMS flag specified on the command defined in
   the package's .CTC resource.
   
Introduction:

   This sample illustrates how to leverage the ParametersDescription property
   to allow for passing arguments with a command, through the Command Window.
   Additionally, this sample also allows for enabling intellisense support for
   command arguments, by setting different ParameterDescription strings on the
   OleMenuCommand.
   
Details:

   Supporting the ALLOWPARAMS flag was first discussed by Dr. eX at 
   http://blogs.msdn.com/dr._ex/archive/2005/03/16/396877.aspx.
   
   The Managed Package Framwork contains support for setting parameter
   descriptor strings for a command, by way of the OleMenuCommand's
   ParametersDescription property. This property is not well documented,
   but this package allows for experimenting with different parameter
   descriptor strings.
   
   This package supports two commands that can both be invoked from the Command
   Window.

   AllowParams.TestCommand: This command simply displays the string(s) passed
                         to the TestCommand by way of the Command Window.
                         
   AllowParams.SetParamDescription: This command sets the ParameterDescription 
                         string on the OleMenuCommand for the above mentioned
                         TestCommand. This can also be set from the AllowParams
                         Settings page in the Tools.Options dialog. If this
                         command is invoked without any arguments, the command
                         will display the AllowParams Settings page in the 
                         Tools.Options dialog.


   The Parameter Description string consists of a series of parameter
   descriptors separated by spaces. Each parameter descriptor is either '*' or
   a series of parameter types separated by '|'. Each parameter type is a
   single character that corresponds to a type of argument that is valid for
   that parameter. When a parameter descritor specified two or more parameter
   types combined with '|', this means that nay of the specified types is valid
   for that parameter, and that autocompletion should present all the lists
   merged toegether.
   
   If a parameter descriptor is '*', this indicates zero or more occurances of
   the previous parameter areallowedw with the sample autocompletion for each.
   Only the last parameter descriptor is allowed to be '*', and there must be 
   at least one preceding parameter descriptor. 
   
   The following are currently valid parameter types:
   
      '~' - No autocompletion for this parameter
      '$' - This parameter is the rest of the input line 
            (no autocompletion).
      'a' – An alias.
      'c' – The canonical name of a command.
      'd' – A filename from the file system.
      'p' – The filename from a project in the current solution.
      'u' – A URL.
      '|' – Combines two parameter types for the same parameter.
      '*' – Indicates zero or more occurrences of the previous parameter.

   Some example ParameterDescription strings:
   
      "p p" – Command accepts two filenames 
      "u d" – Command accepts one URL and one filename argument.
      "u *" – Command accepts zero or more URL arguments.

 
