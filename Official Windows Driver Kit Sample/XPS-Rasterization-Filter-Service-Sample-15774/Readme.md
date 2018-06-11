# XPS Rasterization Filter Service Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:25:50
## Description

<div id="mainSection">
<p>This sample implements an XPSDrv filter that rasterizes fixed pages in an XPS document. Hardware vendors can modify this sample to build an XPSDrv filter that produces bitmap images for their printers or other display devices. The sample uses the XPS Rasterization
 Service in Windows&nbsp;7. The sample does not run in versions of Windows before Windows&nbsp;7.
</p>
<p>This document describes the contents and use of the XPS Rasterization Service Filter sample included in the Windows&nbsp;7 WDK. This document will serve as a point-of-reference to gather and draft the information required for the MSDN entries to accompany the
 sample prior to flowing this text and information into the appropriate MSDN format.</p>
<p>The XPS Rasterization Service creates rasterizer objects for use by XPSDrv filters. A rasterizer object takes an XPS Object Model (XPS OM) page object and creates a bitmap of a specified region of the page. The sample implements an XPSDrv filter (xpsrasfilter.dll)
 that can be inserted into the XPS Filter Pipeline. For each fixed page in an XPS document, the sample filter does the following:</p>
<ul>
<li>Uses the XPS rasterization service to create a rasterizer object for the fixed page.
</li><li>Partitions the fixed page into several horizontal bands. </li><li>Uses the rasterizer object to render each horizontal band as a bitmap image. </li></ul>
<p>The Print Filter Pipeline is part of the XPS Print Path <a href="print.windows_print_path_overview">
Windows Print Path Overview</a>. Fixed pages are sent as an XPS data stream from the XPS Spooler to the print filter pipeline. The print filter pipeline manager takes the XPS fixed page, calls each filter in the order defined in the pipeline configuration file,
 and then sends either Fixed Page OM objects or a data stream to each filter as required. The filters process the data and return either Fixed Page OM objects or a data stream back to the print filter pipeline manager. (See MSDN entry for Filter Pipeline Interfaces
 items IXpsDocumentProvider, IXpsDocumentConsumer, IPrintWriteStream, and IPrintReadStream.)</p>
<p>As a print filter pipeline service, the XPS Rasterization Service can be loaded into the filter pipeline when the pipeline is initialized by adding a filter service provider tag to the configuration XML file (for example, &lt;FilterServiceProvider dll=&quot;XpsRasterService.dll&quot;/&gt;).
 The service is then available to be called by the filters when they are initialized and called by the print filter pipeline manager.
</p>
<p>The XPS Rasterization Service operates as follows:</p>
<ul>
<li>The calling filter initializes an instance of the rasterizer by passing in the XPS OM for the fixed page.
</li><li>The calling filter calls the RasterizeRect method of the rasterizer to render a specified rectangle area of the fixed page.
</li><li>RasterizeRect writes the WIC (Windows Imaging Component) bitmap data to memory. (The address is specified as a parameter to RasterizeRect.)
</li></ul>
<p>The default parameters in this sample are as follows:</p>
<ul>
<li>Letter-sized physical page (can override in print ticket). </li><li>0.25-inch margins (creating an 8-inch by 10.5-inch imageable area). </li><li>Scaling is set to FitApplicationBleedSizeToImageableSize. </li><li>•Destination resolution set to 96 dpi (can override in print ticket). </li></ul>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>To build a driver solution using Windows&nbsp;8.1 driver kit (Windows Driver Kit (WDK)) and Visual Studio&nbsp;2013, perform the following steps.</p>
<dl><dd>
<p>1. Open the solution file in Visual Studio&nbsp;2013</p>
</dd><dd>
<p>2. Add all non-binary files (usually located in the \install directory of the sample) to the Package project</p>
<dl><dd>
<p>a. In the <b>Solution Explorer</b>, right click <b>Driver Files</b></p>
</dd><dd>
<p>b. Select <b>Add</b>, then click <b>Existing Item</b></p>
</dd><dd>
<p>c. Navigate to the location to which you downloaded the sample, and select all the files in the install directory, or the equivalent set of non-binary files such as INFs, INIs, GPD, PPD files, etc.</p>
</dd><dd>
<p>d. Click <b>Add</b></p>
</dd></dl>
</dd><dd>
<p>3. Configure these files to be added into the driver package</p>
<dl><dd>
<p>a. In the <b>Solution Explorer</b>, right click the Package project and select
<b>Properties</b></p>
</dd><dd>
<p>b. In the left pane, click <b>Configuration Properties</b> &gt; <b>Driver Install</b> &gt;
<b>Package Files</b>.</p>
</dd><dd>
<p>c. In the right pane, use the ellipsis button (...) to browse to the set of files that needs to be added to the driver package. All the data files that you added in
<b>Step 2-c</b>, except the INF file, should be added.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This configuration is per-architecture, so this configuration must be repeated for each architecture that will be built.</p>
</dd><dd>d. Click <b>OK</b> </dd></dl>
</dd><dd>
<p>4. Open the INF file and edit it to match the built output</p>
<dl><dd>
<p>a. Open the INF file</p>
</dd><dd>
<p>b. In the Version section, add a reference to a catalog file like this: CatalogFile=XpsDrvSmpl.cat
</p>
</dd><dd>
<p>c. In the SourceDisksFiles section, change the location of the DLL files you are building, to =1. This indicates that there is no architecture specific directory in this driver. If you ship multiple architectures simultaneously, you will need to collate
 the driver INF manually. </p>
</dd></dl>
</dd></dl>
<p></p>
<p>At this point, Microsoft Visual Studio 2012 will be able to build a driver package and output the files to disk. In order to configure driver signing and deployment, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
<p>For more information about how to build a driver solution using Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;If you compile your sample driver with Visual Studio version 10, or 11 with the _DEBUG flag set, then you should not use CComVariant on the following two XPS Print Filter Pipeline properties:</p>
<ul>
<li>XPS_FP_USER_TOKEN </li><li>XPS_FP_PRINTER_HANDLE </li></ul>
There is a known issue with the current implementation of the Print Filter Pipeline, where the variant type for these two properties is set to VT_BYREF. And as a result of this known issue, any filter binary that is compiled with the _DEBUG flag set will experience
 the ATLASSERT() failure. This is because when you use the CComVariant, its destructor checks the returned value from the Clear() function, as shown:
<p></p>
<p></p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>C&#43;&#43;</th>
</tr>
<tr>
<td>
<pre>~CComVariant() throw()
{
   HRESULT hr = Clear();
   ATLASSERT(SUCCEEDED(hr));
   (hr);
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>When you compile this sample driver with Visual Studio version 9, you don't experience this problem because the destructor for CComVariant doesn't perform this check on the returned value from the Clear() function.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>The sample runs on Windows starting with Windows&nbsp;Vista with Service Pack&nbsp;2 (SP2) With Platform Update for Windows Vista (KB971644).</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;If you want to use this sample with Windows Vista SP2 Platform Update, follow these steps:</p>
<ol>
<li>Build the sample using Windows&nbsp;7 as the target operating system. </li><li>
<p>Make the following edits in the Xpsrassmpl.inf file:</p>
<ul>
<li>
<p>Change the Manufacturer section to read:</p>
<pre class="syntax"><code>[Manufacturer]%Microsoft%=Microsoft,NTx86.6.1,NTia64.6.1,NTamd64.6.1,NTx86.6.0,NTia64.6.0,NTamd64.6.0
</code></pre>
</li><li>
<p>Change the Manufacturer section to read:</p>
<pre class="syntax"><code>[Microsoft.NTx86.6.0]
&quot;XPSRas WDK Sample Driver&quot; = INSTALL_FILTER
[Microsoft.NTia64.6.0]
&quot;XPSRas WDK Sample Driver&quot; = INSTALL_FILTER
[Microsoft.NTamd64.6.0]
&quot;XPSRas WDK Sample Driver&quot; = INSTALL_FILTER
</code></pre>
</li></ul>
</li><li>Install the driver on Windows Vista SP2 with Platform Update. </li></ol>
<p>After successfully building the filter, add a local printer with the port set to &quot;FILE&quot;. In the &quot;Install the printer driver&quot; dialog, click the &quot;Have Disk...&quot; button and browse to &lt;sample dir&gt;\install\xpsrassmpl.inf. &quot;XPSRas WDK Sample Driver&quot; will
 appear in the printers list. Select this driver and finish the installation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; When the output files print to this driver, they are made up of a series of per-band TIFFs instead of a flavor of PDL. Because the file contains a series of complete TIFFs and not a single, multi-page TIFF, opening the file in
 an image viewer will only display the first band of the first page. More details can be found in the file bitmaphandler.cpp included with the sample's source code.</p>
<h2><a id="Operation"></a><a id="operation"></a><a id="OPERATION"></a>Operation</h2>
<p>The sample filter demonstrates a minimum implementation of a print pipeline filter that uses the XPS Rasterization Service. This filter performs the following tasks:</p>
<ul>
<li>Converts the passed in IFixedPage object into an IXpsOMPage. </li><li>Calls XPS Rasterization Service to create rasterized bands (WIC bitmaps). </li><li>Uses WIC to encode each band as an individual TIFF. </li><li>Returns each TIFF as a data stream to the Print Filter Manager, which in turn sends it to the designated port.
</li></ul>
<p>For use outside of this sample, we strongly recommend that users work directly with the WIC bitmaps returned from the XPS Rasterization Service. The calls into WIC to encode each band as a TIFF are provided solely for demonstration purposes and do not represent
 a recommended use. Furthermore, using the sample as-is in a larger application, and trying to work with the stream of concatenated per-band TIFFs, would be inefficient and unnecessarily difficult.</p>
<p>For maximum flexibility and reuse, each filter in a printer driver should perform a specific print processing function. For example, a filter might perform color conversion, while another might apply a watermark. For an example of an XPSDrv driver that uses
 multiple pipeline filters, refer to the XPS Driver Sample (XPSDrvSmpl) in the WDK. In particular, compare the pipeline configuration XML files, xdsmpl-PipelineConfig.xml and xpsrassmpl-PipelineConfig.xml, for the two samples. Notice that the configuration
 file for the XPS Rasterization Service sample requires the &lt;FilterServiceProvider.../&gt; tag.</p>
<p>The maximum memory size for rasterized bands in the sample is set to 64 megabytes. This can be changed by editing
<i>rasinterface.h</i> and changing the following line of code:</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>C&#43;&#43;</th>
</tr>
<tr>
<td>
<pre>const static LONG ms_targetBandSize = 1024 * 1024 * 64;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>Recompile the DLL and reload the printer driver (if already installed) to implement the change.</p>
</div>
