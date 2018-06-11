# Shared Projects and Universal Windows Apps
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio Extensions
* Visual Studio 2013
## Topics
* Universal apps
* Shared Projects
## IsPublished
* True
## ModifiedDate
* 2014-04-16 07:43:46
## Description

<h1 style="margin-top:20px">Introduction</h1>
<p>Earlier this month at the BUILD conference, we <a href="http://blogs.windows.com/windows/b/buildingapps/archive/2014/04/02/extending-platform-commonality-through-universal-windows-apps.aspx">
talked about universal Windows apps</a>, which enable developers to more easily target Windows Phone 8.1 and Windows 8.1 Store apps from the same source code base. To support universal Windows apps in the
<a href="http://www.microsoft.com/en-us/download/details.aspx?id=42307">Visual Studio 2013 update RC</a>, we made changes to the core project system that make it much easier to create projects that share some files and not others, and can build for Windows
 Phone 8.1 and Windows 8.1 from the same project. The changes we&rsquo;ve made to the project system are also available to Visual Studio extension developers. In this post, we&rsquo;ll walk through how to take advantage of universal Windows apps and shared
 projects from a VS extension.</p>
<h1>Building the Sample</h1>
<p>The walkthrough includes the following steps:</p>
<ul>
<li>Create a Visual Studio extension. </li><li>Work on the platform projects (the Windows Store application project and the Windows Phone application project.
</li><li>Work on the shared project (the project that is shared between the Windows Store application project and the Windows Phone application project.
</li></ul>
<h2>Prerequisites</h2>
<p>You need to install the following components to complete this walkthrough:</p>
<ul>
<li>Visual Studio 2013 </li><li>Visual Studio 2013 SDK </li><li>Visual Studio 2013 Update 2 </li></ul>
<h1>Description</h1>
<h2>Create a Visual Studio extension</h2>
<ul>
<li>Create a project named <strong>SampleVSPackage</strong> with the Visual Studio Package template (in the
<strong>New Project</strong> dialog, C# / Extensibility / Visual Studio Package).
</li><li>
<p>Select <strong>Menu Command</strong> on the Visual Studio Package Wizard <strong>
Select VSPackage Options</strong> page.</p>
<p><img alt="Select VSPackage Options" id="112835" src="/site/view/file/112835/1/01.png" width="400"></p>
</li><li>
<p>Start debugging (F5). A second instance of Visual Studio comes up. This instance is known as the experimental instance. In the experimental instance, you should see a new menu item
<strong>My Command name</strong> on the <strong>Tools</strong> menu.</p>
<p><img alt="My Command name" id="112836" src="/site/view/file/112836/1/02.jpg" width="400"></p>
</li><li>Create a C# Universal Project in the experimental instance. There are 3 projects in the Solution Explorer. HubApp.Windows and HubApp.WindowsPhone are the platform projects, and HubApp.Shared is the project that is shared between the Windows Store app and
 the Windows Phone app. </li><li>Stop debugging. The experimental instance disappears. </li></ul>
<h2>Navigate the shared project</h2>
<ul>
<li>In the <strong>SampleVSPackage</strong> project, add a reference to Microsoft.VisualStudio.Shell.Interop.DesignTime.12.1.dll (in the
<strong>Extensions</strong> section). </li><li>Open SampleVSPackagePackage.cs and add the following <strong>using</strong> statements:
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Internal.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.IO;</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;Microsoft.VisualStudio.PlatformUI;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Internal.VisualStudio.PlatformUI;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.IO;</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>
<p>Remove the existing code from the <strong>MenuItemCallback</strong> method:</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">void MenuItemCallback(object sender, EventArgs e)
{
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">void</span>&nbsp;MenuItemCallback(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
{&nbsp;
}</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>Create a helper method to write the text to the general output pane.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void Output(string text)
{
    var output = (IVsOutputWindowPane)this.GetService(typeof(SVsGeneralOutputWindowPane));
    output.OutputStringThreadSafe(text);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Output(<span class="cs__keyword">string</span>&nbsp;text)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;output&nbsp;=&nbsp;(IVsOutputWindowPane)<span class="cs__keyword">this</span>.GetService(<span class="cs__keyword">typeof</span>(SVsGeneralOutputWindowPane));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;output.OutputStringThreadSafe(text);&nbsp;
}</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>Walk the solution to find the shared project. The following method finds the first shared project in the solution by using the shared project capability.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private IVsHierarchy FindSharedProject()
{
    var sln = (IVsSolution)this.GetService(typeof(SVsSolution));
    Guid empty = Guid.Empty;
    IEnumHierarchies enumHiers;
    ErrorHandler.ThrowOnFailure(sln.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION, ref empty, out enumHiers));
    foreach (IVsHierarchy hier in ComUtilities.EnumerableFrom(enumHiers))
    {
        if (PackageUtilities.IsCapabilityMatch(hier, &quot;SharedAssetsProject&quot;))
        {
            return hier;
        }
    }
    
    return null;
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;IVsHierarchy&nbsp;FindSharedProject()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;sln&nbsp;=&nbsp;(IVsSolution)<span class="cs__keyword">this</span>.GetService(<span class="cs__keyword">typeof</span>(SVsSolution));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Guid&nbsp;empty&nbsp;=&nbsp;Guid.Empty;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;IEnumHierarchies&nbsp;enumHiers;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ErrorHandler.ThrowOnFailure(sln.GetProjectEnum((<span class="cs__keyword">uint</span>)__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION,&nbsp;<span class="cs__keyword">ref</span>&nbsp;empty,&nbsp;<span class="cs__keyword">out</span>&nbsp;enumHiers));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(IVsHierarchy&nbsp;hier&nbsp;<span class="cs__keyword">in</span>&nbsp;ComUtilities.EnumerableFrom(enumHiers))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(PackageUtilities.IsCapabilityMatch(hier,&nbsp;<span class="cs__string">&quot;SharedAssetsProject&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;hier;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>In the <strong>MenuItemCallback</strong> method, output the caption of the shared project.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void MenuItemCallback(object sender, EventArgs e)
{
    var sharedHier = this.FindSharedProject();
    string sharedCaption = HierarchyUtilities.GetHierarchyProperty&lt;string&gt;(sharedHier, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Caption);
    this.Output(string.Format(&quot;Found shared project: {0}\n&quot;, sharedCaption));
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MenuItemCallback(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;sharedHier&nbsp;=&nbsp;<span class="cs__keyword">this</span>.FindSharedProject();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;sharedCaption&nbsp;=&nbsp;HierarchyUtilities.GetHierarchyProperty&lt;<span class="cs__keyword">string</span>&gt;(sharedHier,&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Root,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_Caption);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;Found&nbsp;shared&nbsp;project:&nbsp;{0}\n&quot;</span>,&nbsp;sharedCaption));&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>Get the active platform project. The shared project is a pure container; it does not build or produce outputs. The following method uses the new property __VSHPROPID7.VSHPROPID_SharedItemContextHierarchy to get the active platform project.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public IVsHierarchy GetActiveProjectContext(IVsHierarchy hierarchy)
{
    IVsHierarchy activeProjectContext;
    if (HierarchyUtilities.TryGetHierarchyProperty(hierarchy, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID7.VSHPROPID_SharedItemContextHierarchy, out activeProjectContext))
    {
        return activeProjectContext;
    }
    else
    {
        return null;
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;IVsHierarchy&nbsp;GetActiveProjectContext(IVsHierarchy&nbsp;hierarchy)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;IVsHierarchy&nbsp;activeProjectContext;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(HierarchyUtilities.TryGetHierarchyProperty(hierarchy,&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Root,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID7.VSHPROPID_SharedItemContextHierarchy,&nbsp;<span class="cs__keyword">out</span>&nbsp;activeProjectContext))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;activeProjectContext;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>In the <strong>MenuItemCallback</strong> method, output the caption of the active platform project.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void MenuItemCallback(object sender, EventArgs e)
{
    &hellip;&hellip;&hellip;&hellip;
    var activePlatformHier = this.GetActiveProjectContext(sharedHier);
    string activeCaption = HierarchyUtilities.GetHierarchyProperty&lt;string&gt;(activePlatformHier, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Caption);
    this.Output(string.Format(&quot;The active platform project: {0}\n&quot;, activeCaption));
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MenuItemCallback(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&hellip;&hellip;&hellip;&hellip;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;activePlatformHier&nbsp;=&nbsp;<span class="cs__keyword">this</span>.GetActiveProjectContext(sharedHier);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;activeCaption&nbsp;=&nbsp;HierarchyUtilities.GetHierarchyProperty&lt;<span class="cs__keyword">string</span>&gt;(activePlatformHier,&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Root,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_Caption);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;The&nbsp;active&nbsp;platform&nbsp;project:&nbsp;{0}\n&quot;</span>,&nbsp;activeCaption));&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>Get all the platform projects. The following method gets the IVsSharedAssetsProject from the shared project IVsHierarchy, and then finds all the platform projects.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public IEnumerable&lt;IVsHierarchy&gt; EnumImportingProjects(IVsHierarchy hierarchy)
{
    IVsSharedAssetsProject sharedAssetsProject;
    if (HierarchyUtilities.TryGetHierarchyProperty(hierarchy, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID7.VSHPROPID_SharedAssetsProject, out sharedAssetsProject)
        &amp;&amp; sharedAssetsProject != null)
    {
        foreach (IVsHierarchy importingProject in sharedAssetsProject.EnumImportingProjects())
        {
            yield return importingProject;
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;IEnumerable&lt;IVsHierarchy&gt;&nbsp;EnumImportingProjects(IVsHierarchy&nbsp;hierarchy)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;IVsSharedAssetsProject&nbsp;sharedAssetsProject;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(HierarchyUtilities.TryGetHierarchyProperty(hierarchy,&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Root,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID7.VSHPROPID_SharedAssetsProject,&nbsp;<span class="cs__keyword">out</span>&nbsp;sharedAssetsProject)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;sharedAssetsProject&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(IVsHierarchy&nbsp;importingProject&nbsp;<span class="cs__keyword">in</span>&nbsp;sharedAssetsProject.EnumImportingProjects())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;yield&nbsp;<span class="cs__keyword">return</span>&nbsp;importingProject;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">Note: If you have opened a C&#43;&#43; universal project in the experimental instance, the code above throws an exception. This is a known issue. To avoid the exception, replace the foreach block above with the following:</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var importingProjects = sharedAssetsProject.EnumImporingProjects();
for (int i = 0; i &lt; importingProjects.Count; &#43;&#43;i)
{
    yield return importingProjects.Item(i);
}
</pre>
<div class="preview">
<pre class="csharp">var&nbsp;importingProjects&nbsp;=&nbsp;sharedAssetsProject.EnumImporingProjects();&nbsp;
<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;importingProjects.Count;&nbsp;&#43;&#43;i)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;yield&nbsp;<span class="cs__keyword">return</span>&nbsp;importingProjects.Item(i);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>
<p>In the <strong>MenuItemCallback</strong> method, output the caption of each platform project.</p>
<p>Note: Only the platform projects that are loaded appear in this list.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void MenuItemCallback(object sender, EventArgs e)
{
    &hellip;&hellip;&hellip;&hellip;&hellip;
    this.Output(&quot;Platform projects:\n&quot;);
    foreach (IVsHierarchy platformHier in this.EnumImportingProjects(sharedHier))
    {
        string platformCaption = HierarchyUtilities.GetHierarchyProperty&lt;string&gt;(platformHier, (uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_Caption);
        this.Output(string.Format(&quot; * {0}\n&quot;, platformCaption));
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MenuItemCallback(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&hellip;&hellip;&hellip;&hellip;&hellip;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__string">&quot;Platform&nbsp;projects:\n&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(IVsHierarchy&nbsp;platformHier&nbsp;<span class="cs__keyword">in</span>&nbsp;<span class="cs__keyword">this</span>.EnumImportingProjects(sharedHier))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;platformCaption&nbsp;=&nbsp;HierarchyUtilities.GetHierarchyProperty&lt;<span class="cs__keyword">string</span>&gt;(platformHier,&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Root,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_Caption);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;&nbsp;*&nbsp;{0}\n&quot;</span>,&nbsp;platformCaption));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>Press F5 to launch the experimental instance. Open or create a C# Universal Project in the experimental instance, go to the
<strong>Tools</strong> menu and click <strong>My Command name</strong>, and check the text in the general output pane.
</li></ul>
<h2>Manage the shared items in the platform project</h2>
<ul>
<li>
<p>Find the shared items in the platform project. The items in the shared project appear in the platform project as shared items. You can&rsquo;t see them in the Solution Explorer, but you can walk the project hierarchy to find them.</p>
<p>The following method walks the hierarchy, outputs the caption of each item, and collects all the shared items. The shared items are identified by the new property __VSHPROPID7.VSHPROPID_IsSharedItem.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void InspectHierarchyItems(IVsHierarchy hier, uint itemid, int level, List&lt;uint&gt; sharedItemIds)
{
    string caption = HierarchyUtilities.GetHierarchyProperty&lt;string&gt;(hier, itemid, (int)__VSHPROPID.VSHPROPID_Caption);
    this.Output(string.Format(&quot;{0}{1}\n&quot;, new string('\t', level), caption));
    bool isSharedItem;
    if (HierarchyUtilities.TryGetHierarchyProperty(hier, itemid, (int)__VSHPROPID7.VSHPROPID_IsSharedItem, out isSharedItem)
        &amp;&amp; isSharedItem)
    {
        sharedItemIds.Add(itemid);
    }
    uint child;
    if (HierarchyUtilities.TryGetHierarchyProperty(hier, itemid, (int)__VSHPROPID.VSHPROPID_FirstChild, Unbox.AsUInt32, out child)
        &amp;&amp; child != (uint)VSConstants.VSITEMID.Nil)
    {
        this.InspectHierarchyItems(hier, child, level &#43; 1, sharedItemIds);
        while (HierarchyUtilities.TryGetHierarchyProperty(hier, child, (int)__VSHPROPID.VSHPROPID_NextSibling, Unbox.AsUInt32, out child)
            &amp;&amp; child != (uint)VSConstants.VSITEMID.Nil)
        {
            this.InspectHierarchyItems(hier, child, level &#43; 1, sharedItemIds);
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;InspectHierarchyItems(IVsHierarchy&nbsp;hier,&nbsp;<span class="cs__keyword">uint</span>&nbsp;itemid,&nbsp;<span class="cs__keyword">int</span>&nbsp;level,&nbsp;List&lt;<span class="cs__keyword">uint</span>&gt;&nbsp;sharedItemIds)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;caption&nbsp;=&nbsp;HierarchyUtilities.GetHierarchyProperty&lt;<span class="cs__keyword">string</span>&gt;(hier,&nbsp;itemid,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_Caption);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;{0}{1}\n&quot;</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">string</span>(<span class="cs__string">'\t'</span>,&nbsp;level),&nbsp;caption));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;isSharedItem;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(HierarchyUtilities.TryGetHierarchyProperty(hier,&nbsp;itemid,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID7.VSHPROPID_IsSharedItem,&nbsp;<span class="cs__keyword">out</span>&nbsp;isSharedItem)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;isSharedItem)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sharedItemIds.Add(itemid);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">uint</span>&nbsp;child;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(HierarchyUtilities.TryGetHierarchyProperty(hier,&nbsp;itemid,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_FirstChild,&nbsp;Unbox.AsUInt32,&nbsp;<span class="cs__keyword">out</span>&nbsp;child)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;child&nbsp;!=&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Nil)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.InspectHierarchyItems(hier,&nbsp;child,&nbsp;level&nbsp;&#43;&nbsp;<span class="cs__number">1</span>,&nbsp;sharedItemIds);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(HierarchyUtilities.TryGetHierarchyProperty(hier,&nbsp;child,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_NextSibling,&nbsp;Unbox.AsUInt32,&nbsp;<span class="cs__keyword">out</span>&nbsp;child)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;child&nbsp;!=&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Nil)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.InspectHierarchyItems(hier,&nbsp;child,&nbsp;level&nbsp;&#43;&nbsp;<span class="cs__number">1</span>,&nbsp;sharedItemIds);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>In the <strong>MenuItemCallback</strong> method, add the following code to walk the platform project hierarchy items.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void MenuItemCallback(object sender, EventArgs e)
{
    &hellip;&hellip;&hellip;&hellip;
    this.Output(&quot;Walk the active platform project:\n&quot;);
    var sharedItemIds = new List&lt;uint&gt;();
    this.InspectHierarchyItems(activePlatformHier, (uint)VSConstants.VSITEMID.Root, 1, sharedItemIds);
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MenuItemCallback(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&hellip;&hellip;&hellip;&hellip;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__string">&quot;Walk&nbsp;the&nbsp;active&nbsp;platform&nbsp;project:\n&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;sharedItemIds&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;<span class="cs__keyword">uint</span>&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.InspectHierarchyItems(activePlatformHier,&nbsp;(<span class="cs__keyword">uint</span>)VSConstants.VSITEMID.Root,&nbsp;<span class="cs__number">1</span>,&nbsp;sharedItemIds);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>
<p>Read the shared items.</p>
<p>The shared items appear in the platform project as hidden linked files, and you can read all the properties as ordinary linked files.</p>
<p>The following method reads the full path of the first shared item.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void MenuItemCallback(object sender, EventArgs e)
{
    &hellip;&hellip;&hellip;&hellip;
    var sharedItemId = sharedItemIds[0];
    string fullPath;
    ErrorHandler.ThrowOnFailure(((IVsProject)activePlatformHier).GetMkDocument(sharedItemId, out fullPath));
    this.Output(string.Format(&quot;Shared item full path: {0}\n&quot;, fullPath));
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MenuItemCallback(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&hellip;&hellip;&hellip;&hellip;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;sharedItemId&nbsp;=&nbsp;sharedItemIds[<span class="cs__number">0</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;fullPath;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ErrorHandler.ThrowOnFailure(((IVsProject)activePlatformHier).GetMkDocument(sharedItemId,&nbsp;<span class="cs__keyword">out</span>&nbsp;fullPath));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;Shared&nbsp;item&nbsp;full&nbsp;path:&nbsp;{0}\n&quot;</span>,&nbsp;fullPath));&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>
<p>Modify the shared items</p>
<p>You can&rsquo;t modify shared items in a platform project. To change the shared items, you need to go back to the shared project that is the actual owner of these items. You can navigate to the shared project by getting the __VSHPROPID7.VSHPROPID_SharedProjectHierarchy
 on the shared item in the platform project. You can get the corresponding item ID in the shared project with IVsProject.IsDocumentInProject, giving it the shared item&rsquo;s full path. Then you can modify the shared item. The change will eventually be propagated
 to the platform projects.</p>
<p>The following code shows how to navigate to the shared project to rename a shared item.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">HierarchyUtilities.TryGetHierarchyProperty(activePlatformHier, sharedItemId, (int)__VSHPROPID7.VSHPROPID_SharedProjectHierarchy, out sharedHier);

uint itemIdInSharedHier;
int found;
VSDOCUMENTPRIORITY[] priority = new VSDOCUMENTPRIORITY[1];
if (ErrorHandler.Succeeded(((IVsProject)sharedHier).IsDocumentInProject(fullPath, out found, priority, out itemIdInSharedHier))
    &amp;&amp; found != 0)       
{
    var newName = DateTime.Now.Ticks.ToString() &#43; Path.GetExtension(fullPath);
    ErrorHandler.ThrowOnFailure(sharedHier.SetProperty(itemIdInSharedHier, (int)__VSHPROPID.VSHPROPID_EditLabel, newName));
    this.Output(string.Format(&quot;Renamed {0} to {1}\n&quot;, fullPath, newName));
}
</pre>
<div class="preview">
<pre class="csharp">HierarchyUtilities.TryGetHierarchyProperty(activePlatformHier,&nbsp;sharedItemId,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID7.VSHPROPID_SharedProjectHierarchy,&nbsp;<span class="cs__keyword">out</span>&nbsp;sharedHier);&nbsp;
&nbsp;
<span class="cs__keyword">uint</span>&nbsp;itemIdInSharedHier;&nbsp;
<span class="cs__keyword">int</span>&nbsp;found;&nbsp;
VSDOCUMENTPRIORITY[]&nbsp;priority&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;VSDOCUMENTPRIORITY[<span class="cs__number">1</span>];&nbsp;
<span class="cs__keyword">if</span>&nbsp;(ErrorHandler.Succeeded(((IVsProject)sharedHier).IsDocumentInProject(fullPath,&nbsp;<span class="cs__keyword">out</span>&nbsp;found,&nbsp;priority,&nbsp;<span class="cs__keyword">out</span>&nbsp;itemIdInSharedHier))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;found&nbsp;!=&nbsp;<span class="cs__number">0</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;newName&nbsp;=&nbsp;DateTime.Now.Ticks.ToString()&nbsp;&#43;&nbsp;Path.GetExtension(fullPath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ErrorHandler.ThrowOnFailure(sharedHier.SetProperty(itemIdInSharedHier,&nbsp;(<span class="cs__keyword">int</span>)__VSHPROPID.VSHPROPID_EditLabel,&nbsp;newName));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;Renamed&nbsp;{0}&nbsp;to&nbsp;{1}\n&quot;</span>,&nbsp;fullPath,&nbsp;newName));&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>
<p>Listen to the item change events. Although the shared items are immutable via user gestures or APIs in platform projects, the actual changes in the shared project are propagated to the platform projects, and the same events are fired by each platform Project.</p>
<p>This event handler outputs the message when a project item is being renamed. You should see messages from both of the platform projects when item change is propagated to those projects.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private void OnItemRenamed(EnvDTE.ProjectItem projItem, string oldName)
{
     this.Output(string.Format(&quot;[Event] Renamed {0} to {1} in project {2}\n&quot;,
         oldName, Path.GetFileName(projItem.get_FileNames(1)), projItem.ContainingProject.Name));
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OnItemRenamed(EnvDTE.ProjectItem&nbsp;projItem,&nbsp;<span class="cs__keyword">string</span>&nbsp;oldName)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Output(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;[Event]&nbsp;Renamed&nbsp;{0}&nbsp;to&nbsp;{1}&nbsp;in&nbsp;project&nbsp;{2}\n&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oldName,&nbsp;Path.GetFileName(projItem.get_FileNames(<span class="cs__number">1</span>)),&nbsp;projItem.ContainingProject.Name));&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>This code signs up for the DTE ProjectItemsEvents.ItemRenamed event. Insert the following code in the
<strong>MenuItemCallback</strong> method before modifying the property.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var dte = (EnvDTE.DTE)this.GetService(typeof(EnvDTE.DTE));
var dteEvents = (EnvDTE80.Events2)dte.Events;
dteEvents.ProjectItemsEvents.ItemRenamed &#43;= this.OnItemRenamed;
</pre>
<div class="preview">
<pre class="csharp">var&nbsp;dte&nbsp;=&nbsp;(EnvDTE.DTE)<span class="cs__keyword">this</span>.GetService(<span class="cs__keyword">typeof</span>(EnvDTE.DTE));&nbsp;
var&nbsp;dteEvents&nbsp;=&nbsp;(EnvDTE80.Events2)dte.Events;&nbsp;
dteEvents.ProjectItemsEvents.ItemRenamed&nbsp;&#43;=&nbsp;<span class="cs__keyword">this</span>.OnItemRenamed;&nbsp;
</pre>
</div>
</div>
</div>
</li></ul>
<ul>
<li>Open or create a C# Universal Project in the experimental instance, go to the
<strong>Tools</strong> menu and click <strong>My Command name</strong>, and check the text in the general output pane. The name of the first item in the shared project (we expect it to be the App.xaml file) should be changed, and you should see that the events
 fired. In this case, since renaming App.xaml causes App.xaml.cs to be renamed as well, you should see four events (two for each platform project). DTE events do not track the items in the shared project at this time. To see the events in the shared project,
 you must use IVsTrackProjectDocumentsEvents2, but that is beyond the scope of this walkthrough.
</li></ul>
<hr style="margin-top:30px">
<p>Tom Li, Software Development Engineer, Visual Studio Platform</p>
