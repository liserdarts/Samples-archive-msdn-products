# PixLib sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* display
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:48:17
## Description

<div id="mainSection">
<p>The PixLib sample demonstrates how to implement the <b>CPixel</b> class for use by a display driver.</p>
<p>For more information, see the WDK documentation topic, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff540585">
CPixel Support Methods for Lightweight MIP Maps</a>.</p>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>PixLib is a helper library for display drivers and cannot be installed or executed independently.</p>
<h2><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h2>
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>Pixel.cpp</td>
<td>Implements utility methods in the CPixel class</td>
</tr>
<tr>
<td>Pixel.hpp</td>
<td>Header file for methods in the CPixel class</td>
</tr>
<tr>
<td>Pixlib.cpp</td>
<td>Very small file that simply lists Microsoft DirectX and PixLib #include files</td>
</tr>
<tr>
<td>Pixlib.sln</td>
<td>Visual Studio solution file </td>
</tr>
<tr>
<td>PixLib.vcxproj</td>
<td>Visual Studio project file</td>
</tr>
<tr>
<td>PixLib.vcxproj.Filters</td>
<td>Visual Studio project filters file</td>
</tr>
</tbody>
</table>
</div>
