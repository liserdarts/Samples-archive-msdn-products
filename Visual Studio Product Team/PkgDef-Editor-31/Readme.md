# PkgDef Editor
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010 SDK
## Topics
* Visual Studio Editor
* VSX
## IsPublished
* True
## ModifiedDate
* 2011-02-28 01:48:41
## Description

<h1><span style="font-size:large">Summary</span></h1>
<p>This sample demonstrates how to create a package of Editor Extensions that provide across-the-board support of a language without implementing a full-blown language service.</p>
<p>Most features are implemented as some form of <strong>ITagger</strong>.<br>
<br>
In this sample we implement support for <em>.pkgdef</em> files, which are used to register VSPackages and other items with Visual Studio or Visual Studio Shell applications. Included are:</p>
<ul>
<li>Colorizing of keys, value names, values, comments, string tokens, and guids </li><li>Syntax validation (showing errors with squiggles and QuickInfo) </li><li>Token validation (showing errors with squiggles and QuickInfo) </li><li>Token completion (when a &lsquo;$&rsquo; is typed) and QuickInfo </li><li>Supports the continuation line syntax (i.e., &lsquo;\&rsquo; at the end of a line)
</li><li>Highlights matching braces and quotes </li><li>Outline collapsing of values beneath sections (keys) </li><li>List errors in the Error List window (and allows double click jump to the associated line)
</li></ul>
<h1>&nbsp;<span style="font-size:large">Requirements</span></h1>
<ul>
<li><a class="externalLink" href="http://www.microsoft.com/visualstudio/en-us/try/default.mspx#download">Visual Studio 2010
</a></li><li><a class="externalLink" href="http://www.microsoft.com/downloads/details.aspx?FamilyID=cb82d35c-1632-4370-acfb-83c01c2ece24&displaylang=en">Visual Studio 2010 SDK
</a></li></ul>
<h1><span style="font-size:large">Getting Started</span></h1>
<ol>
<li>Download and unzip the sample </li><li>Open the solution file </li><li>Build the solution </li><li>Run solution in the Visual Studio experimental instance by pressing F5 </li></ol>
<h1><span style="font-size:large">Test the Functionality</span></h1>
<ol>
<li>On the <strong>File</strong> menu, click <strong>Open</strong> </li><li>Browse to the <strong>TestPkgDefs</strong> sub-directory within the solution.
</li><li>Select and <strong>Open</strong> one of the files. </li><li>A new tab opens with the contents of the file colorized. (Yes, there are errors in
<em>blog.pkgdef</em>) </li><li>Try typing and hovering over various language elements. </li></ol>
<h1><span style="font-size:large">Files</span></h1>
<ul>
<li><strong>PkgDefLanguage.cs</strong> &ndash; implements <strong>PkgDefTaggerProvider</strong>, which provides the core of the language processing, handling continuation lines and returning tags for each language element
</li><li><strong>PkgDefLanguageTokens.cs</strong> &ndash; enumerates the token types and some associated string constants
</li><li><strong>ErrorTagger.cs</strong> &ndash; implements an <strong>ITaggerProvider</strong> for tags of type
<strong>ErrorTag</strong>; uses the <strong>PkgDefTaggerProvider</strong> for parsing and filters and translates the tokens appropriately; also implements a buffer change event handler that watches for changes and idle time (see
<strong>BufferIdleEventUtil.cs</strong>) to update the <strong>Error List</strong> window with error tasks
</li><li><strong>Classifier\Classifier.cs</strong> &ndash; implements an <strong>ITaggerProvider</strong> for tags of type
<strong>ClassificationTag</strong>; uses the <strong>PkgDefTaggerProvider</strong> for parsing and filters and translates the tokens appropriately
</li><li><strong>Classifier\ClassificationFormat.cs</strong> &ndash; defines the <strong>
EditorFormatDefinition</strong>s that are associated with each tag returned by the classifier
</li><li><strong>Classifier\ClassificationType.cs</strong> &ndash; defines the Content Type and File Extensions supported by the classifier, along with the classification types
</li><li><strong>OutlineTagger.cs</strong> &ndash; implements an <strong>ITaggerProvider</strong> for tags of type
<strong>IOutliningRegionTag</strong> using its own parser that only looks for top-level regions to collapse; also uses a buffer change event handler (see
<strong>BufferIdleEventUtil.cs</strong>) to scan the entire document during idle time (rather than try to track the possible effect of each change on each region)
</li><li><strong>BufferIdleEventUtil.cs</strong> &ndash; implements a buffer change event tracker with a timer than accumulates changes until the user stops typing
</li><li><strong>BraceMatching.cs</strong> &ndash; implements an <strong>IViewTaggerProvider</strong> for tags of type
<strong>TextMarkerTag</strong> that monitors changes in the typing caret, watching for braces and quotes, so that it can highlight the matching item
</li><li><strong>Intellisense\QuickInfoController.cs </strong>&ndash; implements a <strong>
IIntellisenseController</strong> needed to translate mouse hover events into QuickInfo sessions
</li><li><strong>Intellisense\QuickInfoControllerProvider.cs </strong>&ndash; implements the factory for the
<strong>IIntellisenseController</strong> for pkgdef files </li><li><strong>Intellisense\PkgDefTokenQuickInfoSource.cs</strong> &ndash; provides QuickInfo data for pkgdef tokens (string substitution)
</li><li><strong>Intellisense\CompletionController</strong>.cs &ndash; implements an <strong>
IOleCommandTarget</strong> that watches for appropriate typing events to initiate (tokens start with &lsquo;$&rsquo;), filter, and finish token completion
</li><li><strong>Intellisense\CompletionSource.cs</strong> &ndash; provides the actual list of completion items<strong>&nbsp;</strong>
</li></ul>
