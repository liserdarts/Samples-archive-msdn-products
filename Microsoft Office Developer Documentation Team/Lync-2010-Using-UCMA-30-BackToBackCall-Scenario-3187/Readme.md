# Lync 2010: Using UCMA 3.0 BackToBackCall: Scenario Overview
## Requires
* Visual Studio 2008
## License
* Apache License, Version 2.0
## Technologies
* Unified Communications Managed API (UCMA) 3.0
* Microsoft Lync Server 2010
## Topics
* Helpdesk
* BackToBackCall class
## IsPublished
* True
## ModifiedDate
* 2011-07-27 11:29:20
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This is the first in a series of four articles on how to use the Microsoft
</span><br>
<span style="font-size:small">Unified Communications Managed API (UCMA) 3.0 <a href="http://msdn.microsoft.com/en-us/library/microsoft.rtc.collaboration.backtobackcall_di_2_ucma3coremref.aspx">
BackToBackCall</a> </span><br>
<span style="font-size:small">class. The <span class="label">BackToBackCall</span> class, which is new in UCMA
</span><br>
<span style="font-size:small">3.0, is the heart of a Helpdesk-type application. By using this class, you can
</span><br>
<span style="font-size:small">create an application that presents a single Helpdesk entry point to your
</span><br>
<span style="font-size:small">customers, which provides anonymity to the Helpdesk agents. A customer&rsquo;s call
</span><br>
<span style="font-size:small">can be routed to a single agent or to a conference consisting of multiple
</span><br>
<span style="font-size:small">agents.</span></p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><span style="font-size:small">One scenario that is supported by the BackToBackCall class is the Incoming-Idle scenario. Another scenario, Click-to-Call, is not discussed in this series of articles. In the Incoming-Idle scenario, a back-to-back (B2B) user
 agent waits for an incoming call to arrive. When the incoming call is received, the back-to-back user agent completes an outgoing call to an agent or a conference with multiple agents. In this scenario the outgoing call to the agent is in the Idle state (the
 call has not yet been established) when the incoming call arrives. The incoming call and the outgoing call form the two call legs of the back-to-back call, with the back-to-back user agent (represented by a BackToBackCall object) in the middle.</span>&nbsp;<em>&nbsp;&nbsp;</em></p>
<h1>More Information</h1>
<p><span style="font-size:small">To read the technical article, see <a href="http://msdn.microsoft.com/en-us/library/hh219349.aspx">
Using UCMA 3.0 BackToBackCall: Scenario Overview (Part 1 of 4)</a></span><em><span style="font-size:small">.</span></em></p>
