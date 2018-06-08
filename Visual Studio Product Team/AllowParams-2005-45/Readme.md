# AllowParams (2005)
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
* 2011-03-01 12:06:01
## Description

<h2>Introduction</h2>
<p>This sample illustrates how to leverage the ParametersDescription property&nbsp;to allow for passing arguments with a command, through the Command Window.&nbsp;Additionally, this sample also allows for enabling intellisense support for command arguments,
 by setting different ParameterDescription strings on the&nbsp;OleMenuCommand.</p>
<h2>Details</h2>
<p>Supporting the ALLOWPARAMS flag was first discussed by Dr. eX at&nbsp;<a href="http://blogs.msdn.com/dr._ex/archive/2005/03/16/396877.aspx">http://blogs.msdn.com/dr._ex/archive/2005/03/16/396877.aspx</a>.<br>
&nbsp;&nbsp;&nbsp;<br>
The Managed Package Framwork contains support for setting parameter&nbsp;descriptor strings for a command, by way of the OleMenuCommand's&nbsp;&nbsp;&nbsp; ParametersDescription property. This property is not well documented,&nbsp;but this package allows for
 experimenting with different parameter&nbsp;descriptor strings.<br>
&nbsp;&nbsp;&nbsp;<br>
This package supports two commands that can both be invoked from the Command&nbsp;Window.</p>
<ol>
<li>AllowParams.TestCommand: This command simply displays the string(s) passed&nbsp;to the TestCommand by way of the Command Window.
</li><li>AllowParams.SetParamDescription: This command sets the ParameterDescription&nbsp;string on the OleMenuCommand for the above mentioned&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TestCommand. This can also be set from the AllowParams&nbsp;Settings page in the Tools.Options
 dialog. If this&nbsp;command is invoked without any arguments, the command&nbsp;will display the AllowParams Settings page in the&nbsp;Tools.Options dialog.
</li></ol>
<p>The Parameter Description string consists of a series of parameter&nbsp;descriptors separated by spaces. Each parameter descriptor is either '*' or&nbsp;a series of parameter types separated by '|'. Each parameter type is a&nbsp;single character that corresponds
 to a type of argument that is valid for&nbsp;that parameter. When a parameter descritor specified two or more parameter&nbsp;types combined with '|', this means that nay of the specified types is valid&nbsp;for that parameter, and that autocompletion should
 present all the lists&nbsp;merged toegether.<br>
&nbsp;&nbsp;&nbsp;<br>
If a parameter descriptor is '*', this indicates zero or more occurances of&nbsp;the previous parameter areallowedw with the sample autocompletion for each.&nbsp;&nbsp; Only the last parameter descriptor is allowed to be '*', and there must be&nbsp;at least
 one preceding parameter descriptor.&nbsp;<br>
&nbsp;&nbsp;&nbsp;<br>
The following are currently valid parameter types:&nbsp;</p>
<ul>
<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; '~' - No autocompletion for this parameter </li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; '$' - This parameter is the rest of the input line
</li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (no autocompletion).
</li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 'a' &ndash; An alias. </li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 'c' &ndash; The canonical name of a command. </li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 'd' &ndash; A filename from the file system. </li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 'p' &ndash; The filename from a project in the current solution.
</li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 'u' &ndash; A URL. </li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; '|' &ndash; Combines two parameter types for the same parameter.
</li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; '*' &ndash; Indicates zero or more occurrences of the previous parameter.
</li></ul>
<p>Some example ParameterDescription strings:&nbsp;</p>
<ul>
<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;p p&quot; &ndash; Command accepts two filenames </li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;u d&quot; &ndash; Command accepts one URL and one filename argument.
</li><li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;u *&quot; &ndash; Command accepts zero or more URL arguments.
</li></ul>
